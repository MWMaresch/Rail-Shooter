﻿using UnityEngine;

public class ShinyEnemy : Enemy
{

    public float speedX;
    public float speedY;
    public float speedZ;
    public GameObject drop;

    private float timer;

    // Use this for initialization
    public override void Start ()
    {
        base.Start();
        //pointsForDestroy = 200;
        //pointsForHit = 0;
        timer = 0;
	}

    public override void FixedUpdate() {
        base.FixedUpdate();
        timer += Time.fixedDeltaTime;
        transform.position += new Vector3(speedX, Mathf.Sin(timer * 5f) * speedY, -speedZ);
        col = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

    }

    public override void Explode()
    {
        Instantiate(drop, transform.position, transform.rotation);
        base.Explode();
    }
}
