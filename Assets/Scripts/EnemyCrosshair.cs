﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCrosshair : MonoBehaviour {

    public GameObject parentLaser;


	// Use this for initialization
	void Start () {
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
    }
	
	void FixedUpdate ()
    {
        if (parentLaser == null || parentLaser.transform.position.z < 20f)
            Destroy(gameObject);
        else if (parentLaser.transform.position.z < 30f)
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            //GetComponent<AudioSource>().Play(); //right now, this sounds terrible
	}
}
