using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public GameObject player;

    private Vector3 startPos;    

	// Use this for initialization
	void Start () {
        startPos = transform.position;
	}
	
	void FixedUpdate () {
        transform.position = new Vector3(startPos.x + player.transform.position.x * 0.4f, startPos.y + player.transform.position.y * 0.4f, startPos.z);
        transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, player.transform.position.x * -0.005f, transform.rotation.w);
	}
}
