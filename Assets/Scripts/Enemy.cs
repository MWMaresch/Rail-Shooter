using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private GameObject player;
    private Vector3 direction;
    private float curShootTimer;

    public float targetDistance;
    public float speed;
    public float minPlayerDistance;
    public float shootTimer;
    public GameObject projectile;

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
        curShootTimer = shootTimer;
    }

    // FixedUpdate is called once every 16ms
    void FixedUpdate()
    {
        //every set amount of time, shoot at the player
        curShootTimer -= Time.fixedDeltaTime;
        if (curShootTimer <= 0)
        {
            Instantiate(projectile, transform.position, transform.rotation);
            curShootTimer = shootTimer;
        }

        //move towards the player, but not if we're too close already
        direction = (player.transform.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance > targetDistance)
        {
            if (transform.position.z < player.transform.position.z + minPlayerDistance)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, player.transform.position.z + minPlayerDistance);
            }
            float curSpeed = Math.Min(speed, speed * distance * 0.1f);
            transform.position += direction * curSpeed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if we get hit, we're dead
        if (other.gameObject.tag == "PlayerWeapon")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
