using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private GameObject player;
    private Vector3 direction;
    private float curShootTimer;
    private bool reachedPlayer;

    public float targetDistance;
    public float speed;
    public float minPlayerDistance;
    public float shootTimer;
    public GameObject projectile;
    public GameObject deathExplosion;

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
        curShootTimer = shootTimer;
        reachedPlayer = false;
    }
    
    // FixedUpdate is called once every 16ms
    void FixedUpdate()
    {
        if (!reachedPlayer)
        {

            //move towards the player, but not if we're too close already
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance > targetDistance)
            {
                direction = (player.transform.position - transform.position).normalized;
                transform.position += direction * speed;

                //every set amount of time, shoot at the player
                curShootTimer -= Time.fixedDeltaTime;
                if (curShootTimer <= 0)
                {
                    Instantiate(projectile, transform.position, transform.rotation);
                    curShootTimer = shootTimer;
                }
            }
            else
            {
                reachedPlayer = true;
            }
        }
        else
        {
            transform.position += direction * speed;
            if (transform.position.z < Camera.main.transform.position.z)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Explode()
    {
        Instantiate(deathExplosion, transform.position, transform.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if we get hit, we're dead
        if (other.gameObject.tag == "PlayerWeapon")
        {
            Instantiate(deathExplosion, transform.position, transform.rotation);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
