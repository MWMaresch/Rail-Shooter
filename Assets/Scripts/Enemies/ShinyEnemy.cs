using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShinyEnemy : Enemy
{

    public float speedX;
    public float speedY;
    public float speedZ;
    public GameObject drop;

    private float timer;

    // Use this for initialization
    public override void Start () {
        timer = 0;
	}

    public override void FixedUpdate() {
        timer += Time.deltaTime;
        transform.position += new Vector3(speedX, Mathf.Sin(timer * 5f) * speedY, -speedZ);

    }

    public override void Explode()
    {
        Instantiate(drop, transform.position, transform.rotation);
        base.Explode();
    }
}
