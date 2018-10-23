using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    //public Camera cam;
    private GameObject crosshair;
    private float lifeTime = 3f;
	// Use this for initialization
	void Start ()
    {
        crosshair = GameObject.FindGameObjectWithTag("Crosshair");
        Vector3 crosshairScreenPos = Camera.main.WorldToScreenPoint(crosshair.transform.position);
        Vector3 aimPos = Camera.main.ScreenToWorldPoint(new Vector3(crosshairScreenPos.x, crosshairScreenPos.y, 50f));
        transform.LookAt(aimPos);
        //move it forward a bit so it doesn't clip through the ship
        transform.position += transform.forward * 3f;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        transform.position += transform.forward * 1.0f;
        lifeTime -= Time.fixedDeltaTime;
        if (lifeTime <= 0f)
            Destroy(gameObject);
    }
}
