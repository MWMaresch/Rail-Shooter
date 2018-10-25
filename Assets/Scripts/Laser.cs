using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    //public Camera cam;
    private GameObject crosshair;
    private float lifeTime = 3f;
    private Vector3 direction;
    // Use this for initialization
    void Start ()
    {
        crosshair = GameObject.FindGameObjectWithTag("Crosshair");
        Vector3 crosshairScreenPos = Camera.main.WorldToScreenPoint(crosshair.transform.position);
        Vector3 aimPos = Camera.main.ScreenToWorldPoint(new Vector3(crosshairScreenPos.x, crosshairScreenPos.y, 50f));
        transform.LookAt(aimPos);
        direction = transform.forward;
        transform.forward = -Camera.main.transform.forward;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        transform.Rotate(new Vector3(0,0,1f), 45f);
        //Debug.Log("rot z is " + transform.rotation.z);
        transform.position += direction;
        lifeTime -= Time.fixedDeltaTime;
        if (lifeTime <= 0f)
            Destroy(gameObject);
    }
}
