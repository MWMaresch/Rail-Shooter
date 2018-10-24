using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterAnimation : MonoBehaviour {
    private float animTime;
	// Use this for initialization
	void Start () {
		animTime = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
    }
	
	void FixedUpdate () {
        animTime -= Time.deltaTime;
        if (animTime < 0)
            Destroy(gameObject);
	}
}
