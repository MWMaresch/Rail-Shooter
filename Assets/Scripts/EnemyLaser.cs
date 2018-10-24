using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour {


    private GameObject player;
    private float lifeTime = 7f;
    // Use this for initialization
    void Start () {
        player = GameObject.FindWithTag("Player");
        transform.LookAt(player.transform);
        //moving it forward at first prevents it from clipping into its own ship too much
        //also move it down a bit to align with the sprite's gun
        transform.position += new Vector3(transform.forward.x, transform.forward.y - 0.2f, transform.forward.z);
    }
	
	// FixedUpdate is called once every 16ms
	void FixedUpdate () {
        transform.position += transform.forward * 0.2f;
        lifeTime -= Time.fixedDeltaTime;
        if (lifeTime <= 0f)
            Destroy(gameObject);
        else if (transform.position.z < 0f)
            Destroy(gameObject);
    }
}
