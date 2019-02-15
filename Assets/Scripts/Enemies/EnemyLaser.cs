using UnityEngine;

public class EnemyLaser : MonoBehaviour {

    //public GameObject crosshair;
    public float speed;
    public Vector3 targetPosition;

    private float lifeTime = 7f;
    private Vector3 direction;
    private SpriteRenderer spriteRenderer;
    private Vector3 startPos;
    // Use this for initialization
    void Start () {
        transform.LookAt(targetPosition);
        direction = transform.forward;
        spriteRenderer = GetComponent<SpriteRenderer>();
        //move it down a bit to align with the sprite's gun
        transform.position += new Vector3(0f, -0.2f, 0f);
        transform.forward = -Camera.main.transform.forward;
        startPos = transform.position;
        //crosshair.GetComponent<EnemyCrosshair>().parentLaser = gameObject;
        //Instantiate(crosshair, targetPosition, transform.rotation);
    }
	
	// FixedUpdate is called once every 16ms
	void FixedUpdate ()
    {
        spriteRenderer.color = Color.Lerp(Color.red, Color.white, (targetPosition - transform.position).magnitude * 0.1f);
        transform.position += direction * speed;
        lifeTime -= Time.fixedDeltaTime;
        if (lifeTime <= 0f)
            Destroy(gameObject);
        else if (transform.position.z < 0f)
            Destroy(gameObject);
    }
}
