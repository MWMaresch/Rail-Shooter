using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed;
    public Transform crosshair;
    public Camera cam;

    private float curSpeedH;
    private Vector3 prevPosition;
    private bool mouseEnabled = false;

    // Use this for initialization
    void Start ()
    {
        //this will eventually be moved to the main menu or something else that starts much earlier
        Application.targetFrameRate = 60;
        Debug.Log("target fps is: " + Application.targetFrameRate);
    }
    // Update is called once per frame
    void Update()
    {
        //for now, because menus haven't been made yet, to switch to mouse controls, we press a button to toggle it
        if (Input.GetButtonUp("ToggleMouse")) //right now it's the 'T' key
        {
            if (mouseEnabled)
                mouseEnabled = false;
            else
                mouseEnabled = true;
        }

        //move the crosshair
        if (mouseEnabled)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = -5f;
            mousePos = cam.ScreenToWorldPoint(mousePos);
            crosshair.transform.position = new Vector3(-mousePos.x, -mousePos.y+2f, -5f);
        }
        else
            crosshair.transform.position += new Vector3(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed);

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
}
