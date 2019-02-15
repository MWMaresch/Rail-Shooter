using UnityEngine;

public class RegularEnemy : Enemy {

    private GameObject player;
    private Vector3 direction;
    private bool reachedPlayer;
    
    public float rammingDistance;
    public float speed;

	// Use this for initialization
	public override void Start ()
    {
        base.Start();
        //pointsForDestroy = 60;
        //pointsForHit = 5;
        player = GameObject.FindWithTag("Player");
        reachedPlayer = false;
    }
    
    // FixedUpdate is called once every 16ms
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (!reachedPlayer)
        {

            //move towards the player, but not if we're too close already
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance > rammingDistance)
            {
                direction = (player.transform.position - transform.position).normalized;
                transform.position += direction * speed;
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
