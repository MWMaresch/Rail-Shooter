using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FodderEnemy : Enemy {

    private GameObject player;
    private Vector3 direction;
    private float changeDirTimer;

    public float minYPosition;
    public float speed;
    public float changeDirDelay;

    // Use this for initialization
    public override void Start () {
        minYPosition = GameObject.FindGameObjectWithTag("Water").transform.position.y;
        player = GameObject.FindWithTag("Player");
        direction = (player.transform.position - transform.position).normalized + new Vector3(Random.Range(-speed, speed), Random.Range(-speed, speed), 0f);
        changeDirTimer = changeDirDelay;
        base.Start();
    }
	
	public override void FixedUpdate () {
        base.FixedUpdate();
        changeDirTimer -= Time.fixedDeltaTime;
        if (changeDirTimer <= 0)
        {
            direction = (player.transform.position - transform.position).normalized + new Vector3(Random.Range(-speed, speed), Random.Range(-speed, speed), 0f);
            direction.z = -speed * 0.5f;
            changeDirTimer = changeDirDelay;
        }
        transform.position += direction * speed;
        if (transform.position.y < minYPosition)
        {
            direction.y = Mathf.Abs(direction.y);
            transform.position += new Vector3(0f, direction.y, 0f);
        }
        if (transform.position.z < Camera.main.transform.position.z)
        {
            Destroy(gameObject);
        }
    }
}
