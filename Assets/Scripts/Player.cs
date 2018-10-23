using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed;
    public Transform crosshair;
    public Camera cam;
    public GameObject laser;

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
        if (mouseEnabled)
        {
            Vector3 mousePos = Input.mousePosition;
            //this value must be the distance between the camera and the crosshair
            mousePos.z = 5f;
            mousePos = cam.ScreenToWorldPoint(mousePos);
            crosshair.transform.position = new Vector3(mousePos.x, mousePos.y, mousePos.z);
        }
        else
            crosshair.transform.position += new Vector3(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed);

        if (shooting)
        {
            //only shoot once per button press
            shooting = false;
            Instantiate(laser, transform.position, transform.rotation);
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
        transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, -curSpeedH, transform.rotation.w);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (damageTimer <= 0f)
        {
            if (other.tag == "Enemy")
            {
                transform.position += new Vector3(other.transform.forward.x * 10f, other.transform.forward.y * 10f, 0f);
                Destroy(other);
                //temporary indicator that we're hit
                GetComponent<Renderer>().enabled = false;
                damageTimer = 1f;
                //transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, other.transform.forward.x * -2, transform.rotation.w);
            }
        }
    }
}
