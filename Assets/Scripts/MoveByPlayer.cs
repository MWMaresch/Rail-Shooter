using UnityEngine;

public class MoveByPlayer : MonoBehaviour {

    private float speed;

	// Use this for initialization
	void Start () {
        speed = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().zSpeed * 0.6f;
    }
	
	void FixedUpdate () {
        transform.position -= new Vector3(0, 0, speed);
        if (transform.position.z < 0f && (GetComponent<AudioSource>()==null || GetComponent<AudioSource>().isPlaying == false))
            Destroy(gameObject);
	}
}
