using UnityEngine;

public class ObjectSpawner : MonoBehaviour {

    public SpawnData[] spawns = new SpawnData[1];

    private float spawnTimer = 0f;
    private int curObjIndex = 0;
    //when a wave of enemies needs to be taken out before more enemies spawn,
    //change this to false
    public bool canSpawn { get; set; }
    public bool loopSpawns = false;

	// Use this for initialization
	void Start () {
        canSpawn = true;
    }

    // FixedUpdate is called once every 16ms
    void FixedUpdate () {
        if (curObjIndex == 0 || canSpawn)
            spawnTimer += Time.fixedDeltaTime;
        if (curObjIndex < spawns.Length)
        {
            if (spawnTimer > spawns[curObjIndex].spawnTime)
            {
                Instantiate(spawns[curObjIndex].gameObj, spawns[curObjIndex].position, new Quaternion(0, 0, 0, 0));
                curObjIndex++;
            }            
        }
        else if (loopSpawns)
        {
            curObjIndex = 0;
            spawnTimer = 0f;
        }
	}    
}
