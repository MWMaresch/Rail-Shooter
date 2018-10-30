using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed;
    public Transform crosshair;
    public Camera cam;
    public GameObject laser;
    public GameObject smallImpact;
    public GameObject muzzleFlash;

    private float curSpeedH;
    private Vector3 prevPosition;
    private bool mouseEnabled = true;
    private bool shooting = false;
    private bool autoShooting = false;
    private float lastShootTime;
    private float damageTimer;
    private Renderer muzzleFlashRend;
    private Vector3 cameraOrigPos;

    private float screenSnapFraction;

    // Use this for initialization
    void Start ()
    {
        cameraOrigPos = Camera.main.transform.position;
        lastShootTime = 999f;
        muzzleFlashRend = muzzleFlash.GetComponent<Renderer>();
        muzzleFlashRend.enabled = false;

        //this will eventually be moved to the main menu or something else that starts much earlier
        Application.targetFrameRate = 60;
        screenSnapFraction = Screen.width / 480f;
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
            Vector3 mousePos = new Vector3 (Mathf.RoundToInt(Input.mousePosition.x / screenSnapFraction) * screenSnapFraction, Mathf.RoundToInt(Input.mousePosition.y / screenSnapFraction) * screenSnapFraction, 5f);
            
            //this value must be the distance between the camera and the crosshair
            //mousePos.z = 5f;
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
        if (Input.GetButton("Fire1"))
            autoShooting = true;
        else
            autoShooting = false;

        //button presses are detected in update instead of FixedUpdate for two reasons:
        //1. less input delay at higher refresh rates
        //2. for some reason, they sometimes trigger twice when put in Update instead
    }
    // FixedUpdate is called once every 16ms
    void FixedUpdate()
    {
        //timer for muzzle flash
        lastShootTime += Time.fixedDeltaTime;
        if (lastShootTime > 0.06f)
            muzzleFlashRend.enabled = false;

        //timer for damage flickering
        if (damageTimer > 0)
        {
            Camera.main.transform.position += 0.2f * new Vector3(UnityEngine.Random.Range(-damageTimer, damageTimer), UnityEngine.Random.Range(-damageTimer, damageTimer), 0f);

            if (GetComponent<Renderer>().enabled)
                GetComponent<Renderer>().enabled = false;
            else
                GetComponent<Renderer>().enabled = true;
            damageTimer -= Time.fixedDeltaTime;
        }
        else if (!GetComponent<Renderer>().enabled)
        {
            GetComponent<Renderer>().enabled = true;
            Camera.main.transform.position = cameraOrigPos;
        }

        //move the crosshair
        if (!mouseEnabled)
        {
            crosshair.transform.position += new Vector3(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed);
        }
        crosshair.LookAt(cam.transform);

        //here we check to make sure the ship and crosshair don't go out of bounds, and if they do, we stop it
        Vector3 screenCH = cam.WorldToScreenPoint(crosshair.transform.position);
        if (screenCH.x < 0)
            crosshair.transform.position = new Vector3(cam.ScreenToWorldPoint(new Vector3(0f, screenCH.y, screenCH.z)).x, crosshair.transform.position.y, crosshair.transform.position.z);
        else if (screenCH.x > Screen.width)
            crosshair.transform.position = new Vector3(cam.ScreenToWorldPoint(new Vector3(Screen.width, screenCH.y, screenCH.z)).x, crosshair.transform.position.y, crosshair.transform.position.z);

        if (screenCH.y < 0)
            crosshair.transform.position = new Vector3(crosshair.transform.position.x, cam.ScreenToWorldPoint(new Vector3(screenCH.x, 0f, screenCH.z)).y, crosshair.transform.position.z);
        else if (screenCH.y > Screen.height)
            crosshair.transform.position = new Vector3(crosshair.transform.position.x, cam.ScreenToWorldPoint(new Vector3(screenCH.x, Screen.height, screenCH.z)).y, crosshair.transform.position.z);


        //rotate the crosshair with the camera to prevent aliasing
        crosshair.rotation = cam.transform.rotation;

        if ((shooting && lastShootTime > 0.02f) || (autoShooting && lastShootTime > 0.15f))
        {
            muzzleFlash.transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, UnityEngine.Random.Range(-1f, 1f), transform.rotation.w);
            lastShootTime = 0f;
            shooting = false;
            Instantiate(laser, transform.position, transform.rotation);
            GetComponent<AudioSource>().Play();
            muzzleFlashRend.enabled = true;
        }

        
        if (transform.position.y < -2.5f)
            transform.position = new Vector3(transform.position.x, -2.5f, transform.position.z);

        //move the ship, and rotate it depending on how much it moved
        prevPosition = transform.position;
        transform.position += 0.1f * new Vector3(crosshair.transform.position.x - transform.position.x, crosshair.transform.position.y - 0.4f - transform.position.y);
        curSpeedH = transform.position.x - prevPosition.x;
        transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, 0.2f * damageTimer * (float)Math.Sin(damageTimer * 30f) - curSpeedH, transform.rotation.w);
    }

    public void TakeDamage(float knockbackX, float knockbackY)
    {
        transform.position += new Vector3(knockbackX, knockbackY, 0);
        //indicator that we're hit
        GetComponent<Renderer>().enabled = false;
        damageTimer = 1f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (damageTimer <= 0f)
        {
            if (other.tag == "EnemyWeapon")
            {
                Instantiate(smallImpact, transform.position, transform.rotation);
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
