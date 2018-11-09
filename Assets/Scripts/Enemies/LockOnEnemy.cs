using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnEnemy : Enemy {

    public float waitTime;
    public float shootDelay;
    public float shootDistance;
    public float speed;
    public GameObject projectile;
    public GameObject muzzleFlash;

    private bool isShooting;
    private float waitTimer;
    private float shootTimer;
    private Vector3 direction;
    private GameObject player;
    private GameObject water;

    // Use this for initialization
    public override void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        water = GameObject.FindGameObjectWithTag("Water");
        direction = new Vector3();
        waitTimer = 0;
        shootTimer = 0;
        isShooting = false;
        base.Start();
    }


    public override void FixedUpdate()
    {
        if (hitColorTimer > 0f)
        {
            hitColorTimer -= Time.fixedDeltaTime;
            if (GetComponent<Renderer>().material.color == Color.red)
                GetComponent<Renderer>().material.color = Color.magenta;
            else
                GetComponent<Renderer>().material.color = Color.red;
        }
        else
            GetComponent<Renderer>().material.color = Color.magenta;


        if (transform.position.z > shootDistance)
        {

            direction = (player.transform.position - transform.position).normalized;
            transform.position += direction * speed;
        }
        else
        {
            waitTimer -= Time.fixedDeltaTime;
            if (waitTimer <= 0)
            {
                isShooting = !isShooting;
                waitTimer = waitTime;
                direction = (player.transform.position - transform.position).normalized;
            }

            if (isShooting)
            {
                shootTimer -= Time.fixedDeltaTime;
                if (shootTimer <= 0f)
                {
                    Instantiate(muzzleFlash, new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z - 0.2f), transform.rotation);
                    Instantiate(projectile, transform.position, transform.rotation);
                    shootTimer = shootDelay + Random.Range(-0.2f, 0.2f);
                    GetComponent<AudioSource>().Play();
                }
            }
            else
            {
                transform.position += new Vector3(direction.x * speed, direction.y * speed, 0);
                if (transform.position.y <= water.transform.position.y)
                {
                    direction = new Vector3(direction.x, Mathf.Abs(direction.y), 0);
                }
            }
        }
    }


}
