using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    //all behaviour in this class is for every type of enemy.
    //they'll explode when they die, they have limited health,
    //they take damage and they flash red when they're hit
    //all of this can be overriden per enemy as needed

    public GameObject smallExplosion;
    public GameObject deathExplosion;
    public int maxHealth;

    protected int health;
    protected float hitColorTimer;

    // Use this for initialization
    public virtual void Start () {
        health = maxHealth;
        hitColorTimer = 0f;
    }

    public virtual void FixedUpdate()
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
    }

    public virtual void Explode()
    {
        Instantiate(deathExplosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public virtual void TakeDamage(float knockbackX, float knockbackY)
    {
        Instantiate(smallExplosion, transform.position, transform.rotation);
    }

    public virtual void OnTriggerEnter(Collider other)
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
