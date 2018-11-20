using System;
using UnityEngine;


// Custom serializable class
[Serializable]
public class SpawnData
{
    public GameObject gameObj;
    public float spawnTime = 1;
    public Vector3 position = Vector3.zero;
}
