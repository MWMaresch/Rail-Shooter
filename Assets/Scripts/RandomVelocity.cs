using UnityEngine;

public class RandomVelocity : MonoBehaviour {

    private Vector3 velocity;

	// Use this for initialization
	void Start () {
        velocity.x = Random.Range(-0.2f, 0.2f);
        velocity.y = Random.Range(-0.2f, 0.2f);
    }
	
	void FixedUpdate () {
        transform.position += velocity;
	}
}
