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
        player = GameObject.FindGameObjectWithTag("Player");
        direction = speed * (player.transform.position - transform.position).normalized;
        timer = 0;
    }

    public override void FixedUpdate()
    {
        timer += Time.deltaTime;
        transform.position += direction + new Vector3(Mathf.Sin(timer * 4f) * speed, Mathf.Cos(timer * 4f) * speed, 0);

    }
}
