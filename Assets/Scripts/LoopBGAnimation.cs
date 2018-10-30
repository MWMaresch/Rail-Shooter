using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopBGAnimation : MonoBehaviour {
    
    public float speed;

    private float sizeX;

	// Use this for initialization
	void Start ()
    {
        sizeX = 0.5f*GetComponent<SpriteRenderer>().bounds.size.x;
        transform.position = new Vector3(-sizeX, transform.position.y, transform.position.z);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position += new Vector3(speed, 0f);
        if (transform.position.x > sizeX)
            transform.position = new Vector3(-sizeX, transform.position.y, transform.position.z);
        else if (transform.position.x < -sizeX)
            transform.position = new Vector3(sizeX, transform.position.y, transform.position.z);

    }
}
