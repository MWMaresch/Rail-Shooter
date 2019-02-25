using UnityEngine;

public class FodderEnemy : Enemy
{

    private GameObject player;
    private float shootTimer;

    public float minShootDistance;
    public float speed;
    public float shootDelay;
    public GameObject projectile;
    public GameObject muzzleFlash;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        player = GameObject.FindWithTag("Player");
        shootTimer = shootDelay;
    }

    // FixedUpdate is called once every 16ms
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        transform.position += Vector3.back * speed;

        //every set amount of time, shoot at the player
        shootTimer -= Time.fixedDeltaTime;
        if (shootTimer <= 0 && transform.position.z > player.transform.position.z + minShootDistance)
        {
            Instantiate(muzzleFlash, new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z - 0.2f), transform.rotation);
            projectile.GetComponent<EnemyLaser>().targetPosition = player.transform.position;
            Instantiate(projectile, transform.position, transform.rotation);
            shootTimer = shootDelay + Random.Range(-0.2f, 0.2f);
            GetComponent<AudioSource>().Play();
        }
        else if (transform.position.z < Camera.main.transform.position.z)
        {
            Destroy(gameObject);
        }
    }
}
