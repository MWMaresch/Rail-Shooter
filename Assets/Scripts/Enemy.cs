using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private GameObject player;
    private Vector3 direction;
    private float shootTimer;
    private bool reachedPlayer;
    private int health;
    private float hitColorTimer;

    public int maxHealth;
    public float minShootDistance;
    public float rammingDistance;
    public float speed;
    public float shootDelay;
    public GameObject projectile;
    public GameObject smallExplosion;
    public GameObject deathExplosion;

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
        shootTimer = shootDelay;
        reachedPlayer = false;
        health = maxHealth;
        hitColorTimer = 0f;
    }
    
    // FixedUpdate is called once every 16ms
    void FixedUpdate()
    {
        if (hitColorTimer > 0f)
        {
            hitColorTimer -= Time.fixedDeltaTime;
            if (GetComponent<Renderer>().material.color == Color.red)
                GetComponent<Renderer>().material.color = Color.white;
            else
                GetComponent<Renderer>().material.color = Color.red;
        }
        else
            GetComponent<Renderer>().material.color = Color.white;

        if (!reachedPlayer)
        {

            //move towards the player, but not if we're too close already
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance > rammingDistance)
            {
                direction = (player.transform.position - transform.position).normalized;
                transform.position += direction * speed;

                //every set amount of time, shoot at the player
                shootTimer -= Time.fixedDeltaTime;
                if (shootTimer <= 0 && distance < minShootDistance)
                {
                    Instantiate(projectile, transform.position, transform.rotation);
                    shootTimer = shootDelay + UnityEngine.Random.Range(-0.2f, 0.2f);
                    GetComponent<AudioSource>().Play();
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
        Destroy(gameObject);
    }

    public void TakeDamage(float knockbackX, float knockbackY)
    {
        Instantiate(smallExplosion, transform.position, transform.rotation);
        //transform.position += new Vector3(knockbackX, knockbackY, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if we get hit, we're dead
        if (other.gameObject.tag == "PlayerWeapon")
        {
            TakeDamage((transform.position.x - other.transform.position.x), (transform.position.y - other.transform.position.y));
            Destroy(other.gameObject);
            health--;
           // GetComponent<Renderer>().material.color = Color.red;
            hitColorTimer = 0.5f;
            if (health <= 0)
                Explode();
        }
    }
}
