using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public float spawnDelay;
    public GameObject enemy;
    public float randPosX;
    public float randPosY;


    private float spawnDelayTimer;

	// Use this for initialization
	void Start () {
        spawnDelayTimer = spawnDelay;
	}

    // FixedUpdate is called once every 16ms
    void FixedUpdate () {
        spawnDelayTimer -= Time.fixedDeltaTime;
        if (spawnDelayTimer <= 0f)
        {
            spawnDelayTimer = spawnDelay;
            Instantiate(enemy, new Vector3(Random.Range(-randPosX, randPosX) + transform.position.x, Random.Range(-randPosY, randPosY) + transform.position.y, transform.position.z), new Quaternion(0,0,0,0));
        }
	}
}
