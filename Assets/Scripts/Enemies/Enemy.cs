using UnityEngine;

public class Enemy : MonoBehaviour {

    //all behaviour in this class is for every type of enemy.
    //they'll explode when they die, they have limited health,
    //they take damage and they flash red when they're hit
    //all of this can be overriden per enemy as needed

    public GameObject smallExplosion;
    public GameObject deathExplosion;
    public int maxHealth;

    protected int pointsForHit;
    protected int pointsForDestroy;
    protected int health;
    protected float hitColorTimer;
    protected Color col;

    private GameObject water;
    // Use this for initialization
    public virtual void Start ()
    {
        pointsForHit = 25;
        pointsForDestroy = 100;
        col = Color.white;
        water = GameObject.FindGameObjectWithTag("Water");
        health = maxHealth;
        hitColorTimer = 0f;
    }

    public virtual void FixedUpdate()
    {
        if (transform.position.y <= water.transform.position.y)
            transform.position = new Vector3(transform.position.x, water.transform.position.y, transform.position.z);

        if (hitColorTimer > 0f)
        {
            hitColorTimer -= Time.fixedDeltaTime;
            if (GetComponent<Renderer>().material.color == Color.red)
                GetComponent<Renderer>().material.color = col;
            else
                GetComponent<Renderer>().material.color = col;
        }
        else
            GetComponent<Renderer>().material.color = col;
    }
    
    public virtual void Explode()
    {
        GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().AddScore(pointsForDestroy);
        Instantiate(deathExplosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public virtual void TakeDamage(float knockbackX, float knockbackY)
    {
        GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().AddScore(pointsForHit);
        Instantiate(smallExplosion, transform.position, transform.rotation);
        hitColorTimer = 0.5f;
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerWeapon" && health > 0)
        {
            Destroy(other.gameObject);
            health--;
            if (health > 0)
                TakeDamage((transform.position.x - other.transform.position.x), (transform.position.y - other.transform.position.y));
            else
                Explode();
        }
    }
}
