﻿using UnityEngine;

public class PlayerWeapon : MonoBehaviour {

    public Sprite splash;

    public int damage;
    public float speed;
    public bool destroyOnHit = true;

    //public Camera cam;
    private GameObject crosshair;
    public float lifeTime = 3f;
    private Vector3 direction;
    private bool splashed = false;
    private GameObject player;
    // Use this for initialization
    protected virtual void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        crosshair = GameObject.FindGameObjectWithTag("Crosshair");
        Vector3 crosshairScreenPos = Camera.main.WorldToScreenPoint(crosshair.transform.position);
        Vector3 aimPos = Camera.main.ScreenToWorldPoint(new Vector3(crosshairScreenPos.x, crosshairScreenPos.y, 200f));
        transform.LookAt(aimPos);
        direction = transform.forward + player.GetComponent<Player>().velocity * 0.5f;
        transform.forward = -Camera.main.transform.forward;
        transform.position += direction *2f;
    }

    // Update is called once per frame
    protected virtual void FixedUpdate ()
    {
        if (!splashed)
            transform.Rotate(new Vector3(0, 0, 1f), 45f);

        transform.position += direction * speed;
        lifeTime -= Time.fixedDeltaTime;
        if (lifeTime <= 0f)
            Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!splashed && other.gameObject.tag == "Water")
        {
            splashed = true;
            GetComponent<AudioSource>().Play();
            GetComponent<Animator>().enabled = true;
            lifeTime = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
            transform.position = new Vector3(transform.position.x, -2.6f, transform.position.z);
            transform.rotation = new Quaternion(0, 0, 0, 0);
            direction = new Vector3(0, 0, -GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().zSpeed * 0.6f);
        }
    }

    public virtual void HitEnemy(GameObject other)
    {
        if (destroyOnHit)
            Destroy(gameObject);
    }
}
