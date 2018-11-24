using UnityEngine;

public class LockOnEnemy : Enemy {

    public float waitTime;
    public float shootDelay;
    public float shootDistance;
    public float speed;
    public GameObject projectile;
    public GameObject muzzleFlash;

    private bool isShooting;
    private bool reachedShootDistance;
    private float waitTimer;
    private float shootTimer;
    private Vector3 direction;
    private Vector3 shootPosition;
    private Vector3 pPos;
    private GameObject player;

    // Use this for initialization
    public override void Start ()
    {
        base.Start();
        pointsForDestroy = 50;
        pointsForHit = 2;
        player = GameObject.FindGameObjectWithTag("Player");
        pPos = player.transform.position;
        shootPosition = new Vector3(pPos.x, pPos.y, pPos.z + shootDistance);
        direction = (shootPosition - transform.position).normalized;
        waitTimer = 0;
        shootTimer = 0;
        isShooting = false;
        col = Color.magenta;
        reachedShootDistance = false;
    }


    public override void FixedUpdate()
    {
        base.FixedUpdate();
        pPos = player.transform.position;

        if (!reachedShootDistance)
        {
            if (Vector3.Distance(transform.position, shootPosition) > speed * 2)
                transform.position += direction * speed;
            else
                reachedShootDistance = true;
        }
        else
        {
            waitTimer -= Time.fixedDeltaTime;
            if (waitTimer <= 0)
            {
                isShooting = !isShooting;
                waitTimer = waitTime;
                direction = (pPos - transform.position).normalized;
            }

            if (isShooting)
            {
                shootTimer -= Time.fixedDeltaTime;
                if (shootTimer <= 0f)
                {
                    FireFiveShots();
                }
            }
            else
            {
                transform.position += new Vector3(direction.x * speed, direction.y * speed, 0);
            }
        }
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
