using UnityEngine;

public class FodderEnemy : Enemy {

    private GameObject player;
    private Vector3 direction;
    private float shootTimer;
    private bool reachedPlayer;

    public float minShootDistance;
    public float rammingDistance;
    public float speed;
    public float shootDelay;
    public GameObject projectile;
    public GameObject muzzleFlash;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        pointsForDestroy = 60;
        pointsForHit = 5;
        player = GameObject.FindWithTag("Player");
        shootTimer = shootDelay;
        reachedPlayer = false;
        col = Color.green;
    }

    // FixedUpdate is called once every 16ms
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (!reachedPlayer)
        {

            //move towards the player, but not if we're too close already
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance > 0)
            {
                //direction = (player.transform.position - transform.position).normalized;
                transform.position += Vector3.back * speed;

                //every set amount of time, shoot at the player
                shootTimer -= Time.fixedDeltaTime;
                if (shootTimer <= 0 && distance < minShootDistance)
                {
                    Instantiate(muzzleFlash, new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z - 0.2f), transform.rotation);
                    projectile.GetComponent<EnemyLaser>().targetPosition = player.transform.position;
                    Instantiate(projectile, transform.position, transform.rotation);
                    shootTimer = shootDelay + Random.Range(-0.2f, 0.2f);
                    GetComponent<AudioSource>().Play();
                }
            }
            else
            {
                reachedPlayer = true;
            }
        }
        else
        {
            transform.position += direction * speed;
            if (transform.position.z < Camera.main.transform.position.z)
            {
                Destroy(gameObject);
            }
        }
    }
}
