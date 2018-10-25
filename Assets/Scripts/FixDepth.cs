using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixDepth : MonoBehaviour {
    
	void FixedUpdate () {
        GetComponent<SpriteRenderer>().sortingOrder = (int)Mathf.Floor(-transform.position.z);
	}
}
