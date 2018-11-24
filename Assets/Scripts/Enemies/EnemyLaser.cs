using UnityEngine;

public class EnemyLaser : MonoBehaviour {

    public GameObject crosshair;
    public float speed;
    public Vector3 targetPosition;

    //private GameObject player;
    private float lifeTime = 7f;
    private Vector3 direction;
    // Use this for initialization
    void Start () {
        //player = GameObject.FindWithTag("Player");
        //Vector3 v = player.GetComponent<Player>().velocity * 10;
        transform.LookAt(targetPosition);
        direction = transform.forward;
        //move it down a bit to align with the sprite's gun
        transform.position += new Vector3(0f, -0.2f, 0f);
        transform.forward = -Camera.main.transform.forward;
        crosshair.GetComponent<EnemyCrosshair>().parentLaser = gameObject;
        Instantiate(crosshair, targetPosition, transform.rotation);
    }
	
	// FixedUpdate is called once every 16ms
	void FixedUpdate ()
    {
        transform.position += direction * speed;
        lifeTime -= Time.fixedDeltaTime;
        if (lifeTime <= 0f)
            Destroy(gameObject);
        else if (transform.position.z < 0f)
            Destroy(gameObject);
    }
}
