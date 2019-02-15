using UnityEngine;

public class LockOnEnemy : Enemy {

    public float waitTime;
    public float shootDelay;
    public float shootDistance;
    public float acceleration;
    public float maxSpeed;
    public GameObject projectile;
    public GameObject muzzleFlash;

    private float shootTimer;
    private Vector3 direction;
    private Vector3 destination;
    private Vector3 pPos;
    private Vector3 velocity;
    private GameObject player;

    // Use this for initialization
    public override void Start ()
    {
        base.Start();
        //pointsForDestroy = 50;
        //pointsForHit = 2;
        player = GameObject.FindGameObjectWithTag("Player");
        pPos = player.transform.position;
        destination = new Vector3(pPos.x, pPos.y, pPos.z + shootDistance);
        direction = (destination - transform.position).normalized;
        shootTimer = 0;
        col = Color.magenta;
    }


    public override void FixedUpdate()
    {
        base.FixedUpdate();
        pPos = player.transform.position;

        shootTimer -= Time.fixedDeltaTime;
        // if we're close to our destination OR if it's been a long time since we last shot and we're far enough
        if (Vector3.Distance(transform.position, destination) <= 5f)// || (shootTimer < -1.5f && transform.position.z >= destination.z))
        {
            if (shootTimer <= 0)
            {
                FireFiveShots();
                shootTimer = shootDelay;
            }
            destination = new Vector3(pPos.x, pPos.y, pPos.z + shootDistance)
                + new Vector3(Random.Range(-3,3), Random.Range(-3, 3), Random.Range(-1, 1));
            //we add a bit of randomness so multiple enemies don't all go to the same spot
        }
        direction = (destination - transform.position).normalized;
        velocity += direction * acceleration;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        transform.position += velocity;
    }

    private void FireFiveShots()
    {
        Instantiate(muzzleFlash, new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z - 0.2f), transform.rotation);

        projectile.GetComponent<EnemyLaser>().targetPosition = pPos;
        Instantiate(projectile, transform.position, transform.rotation);
        projectile.GetComponent<EnemyLaser>().targetPosition = pPos + new Vector3(1,0,0);
        Instantiate(projectile, transform.position, transform.rotation);
        projectile.GetComponent<EnemyLaser>().targetPosition = pPos + new Vector3(-1, 0, 0);
        Instantiate(projectile, transform.position, transform.rotation);
        projectile.GetComponent<EnemyLaser>().targetPosition = pPos + new Vector3(0, 1, 0);
        Instantiate(projectile, transform.position, transform.rotation);
        projectile.GetComponent<EnemyLaser>().targetPosition = pPos + new Vector3(0, -1, 0);
        Instantiate(projectile, transform.position, transform.rotation);


        shootTimer = shootDelay + Random.Range(-0.2f, 0.2f);
        GetComponent<AudioSource>().Play();

    }


}
