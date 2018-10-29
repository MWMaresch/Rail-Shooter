using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixDepth : MonoBehaviour {
    

	void FixedUpdate () {
        GetComponent<SpriteRenderer>().sortingOrder = (int)Mathf.Floor(-transform.position.z);
        if (transform.position.z < 23.2f)
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, (transform.position.z-18f) / 7f);
        }

    }
}
