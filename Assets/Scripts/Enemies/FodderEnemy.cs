using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FodderEnemy : Enemy {

    private GameObject player;
    private Vector3 direction;

    public float speed;    
    public GameObject drop;

    private float timer;

    // Use this for initialization
    public override void Start()
    {
        pointsForDestroy = 50;
        pointsForHit = 0;
        health = 1;
        player = GameObject.FindGameObjectWithTag("Player");
        direction = speed * (player.transform.position - transform.position).normalized;
        timer = 0;
        GetComponent<SpriteRenderer>().color = Color.green;
    }

    public override void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        transform.position += direction + new Vector3(Mathf.Sin(timer * 4f) * speed, Mathf.Cos(timer * 4f) * speed, 0);

    }
}
