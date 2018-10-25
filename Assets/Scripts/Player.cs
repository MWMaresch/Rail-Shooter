using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed;
    public Transform crosshair;
    public Camera cam;
    public GameObject laser;
    public GameObject smallExplosion;

    private float curSpeedH;
    private Vector3 prevPosition;
    private bool mouseEnabled = false;
    private bool shooting = false;
    private float damageTimer;

    // Use this for initialization
    void Start ()
    {
        //this will eventually be moved to the main menu or something else that starts much earlier
        Application.targetFrameRate = 180;
        //Debug.Log("target fps is: " + Application.targetFrameRate);
        //Screen.SetResolution(1440, 810, false, 180);
        //480, 960, 1440
        //270, 540, 810
    }
    //Update is called once per frame
    private void Update()
    {
        if (mouseEnabled)
        {
            Vector3 mousePos = Input.mousePosition;
            //this value must be the distance between the camera and the crosshair
            mousePos.z = 5f;
            mousePos = cam.ScreenToWorldPoint(mousePos);
            crosshair.transform.position = new Vector3(mousePos.x, mousePos.y, mousePos.z);
        }
        //for now, because menus haven't been made yet, to switch to mouse controls, we press a button to toggle it
        if (Input.GetButtonDown("ToggleMouse")) 
        {
            if (mouseEnabled)
                mouseEnabled = false;
            else
                mouseEnabled = true;
        }
        if (Input.GetButtonDown("Fire1"))
            shooting = true;
        //button presses are detected in update instead of FixedUpdate for two reasons:
        //1. less input delay at higher refresh rates
        //2. for some reason, they sometimes trigger twice when put in Update instead
    }
    // FixedUpdate is called once every 16ms
    void FixedUpdate()
    {
        if (damageTimer > 0)
        {
            if (GetComponent<Renderer>().enabled)
                GetComponent<Renderer>().enabled = false;
            else
                GetComponent<Renderer>().enabled = true;
            damageTimer -= Time.fixedDeltaTime;
        }
        else if (!GetComponent<Renderer>().enabled)
            GetComponent<Renderer>().enabled = true;
        //move the crosshair

        if(!mouseEnabled)
            crosshair.transform.position += new Vector3(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed);

        if (shooting)
        {
            //only shoot once per button press
            shooting = false;
            Instantiate(laser, transform.position, transform.rotation);
            GetComponent<AudioSource>().Play();
        }

        //here we check to make sure the ship doesn't go out of bounds, and if it does, we stop it
        if (crosshair.transform.position.x < -4.5f)
            crosshair.transform.position = new Vector3(-4.5f, crosshair.transform.position.y, crosshair.transform.position.z);
        else if (crosshair.transform.position.x > 4.5f)
            crosshair.transform.position = new Vector3(4.5f, crosshair.transform.position.y, crosshair.transform.position.z);
        if (crosshair.transform.position.y < -1.5f)
            crosshair.transform.position = new Vector3(crosshair.transform.position.x, -1.5f, crosshair.transform.position.z);
        else if (crosshair.transform.position.y > 3.5f)
            crosshair.transform.position = new Vector3(crosshair.transform.position.x, 3.5f, crosshair.transform.position.z);
        if (transform.position.y < -1f)
            transform.position = new Vector3(transform.position.x, -1f, transform.position.z);

        //move the ship, and rotate it depending on how much it moved
        prevPosition = transform.position;
        transform.position += 0.1f * new Vector3(crosshair.transform.position.x - transform.position.x, crosshair.transform.position.y - 0.4f - transform.position.y);
        curSpeedH = transform.position.x - prevPosition.x;
        //if (damageTimer <= 0)
            transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, 0.1f * (float)Math.Sin(damageTimer * 25f) - curSpeedH, transform.rotation.w);
        //else
            //transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, 0.1f*(float)Math.Sin(damageTimer * 25f), transform.rotation.w);
    }

    public void TakeDamage(float knockbackX, float knockbackY)
    {
        Instantiate(smallExplosion, transform.position, transform.rotation);
        transform.position += new Vector3(knockbackX, knockbackY, 0);
        //temporary indicator that we're hit
        GetComponent<Renderer>().enabled = false;
        damageTimer = 1f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (damageTimer <= 0f)
        {
            if (other.tag == "EnemyWeapon")
            {
                TakeDamage(other.transform.forward.x, other.transform.forward.y);
                Destroy(other.gameObject);
            }
            else if (other.gameObject.tag == "Enemy")
            {
                other.GetComponent<Enemy>().Explode();
                TakeDamage(transform.position.x - other.transform.position.x,transform.position.y - other.transform.position.y);
            }
        }
    }
}
