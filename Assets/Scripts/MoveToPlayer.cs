using UnityEngine;

public class MoveToPlayer : MonoBehaviour {

    private GameObject player;
    private float speed;
    private Vector3 direction;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        speed = player.GetComponent<Player>().zSpeed * 0.6f;
        direction = speed * (player.transform.position - transform.position).normalized;
    }

    void FixedUpdate()
    {
        transform.position += direction;
        if (transform.position.z < 0f && (GetComponent<AudioSource>() == null || GetComponent<AudioSource>().isPlaying == false))
            Destroy(gameObject);
    }
}
