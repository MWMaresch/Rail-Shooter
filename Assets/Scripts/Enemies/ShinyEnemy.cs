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
        health = 1;
        timer = 0;
        GetComponent<SpriteRenderer>().color = Color.magenta;
	}

    public override void FixedUpdate() {
        timer += Time.deltaTime;
        transform.position += new Vector3(speedX, Mathf.Sin(timer * 5f) * speedY, -speedZ);
        GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        /*if (colorToggle)
            GetComponent<SpriteRenderer>().color = Color.cyan;
        else
            GetComponent<SpriteRenderer>().color = Color.black;
        colorToggle = !colorToggle;*/

    }

    public override void Explode()
    {
        Instantiate(drop, transform.position, transform.rotation);
        base.Explode();
    }
}
