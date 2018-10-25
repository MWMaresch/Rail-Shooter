using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterAnimation : MonoBehaviour {
    private float animTime;
    private bool hasSound;
	// Use this for initialization
	void Start () {
        if (GetComponent<AudioSource>() != null)
            hasSound = true;
		animTime = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
    }
	
	void FixedUpdate () {
        animTime -= Time.deltaTime;
        if (animTime < 0)
        {
            if (hasSound)
            {
                //only destroy it if it's not playing a sound
                if (!GetComponent<AudioSource>().isPlaying)
                    Destroy(gameObject);
            }
            else
                Destroy(gameObject);
            GetComponent<Renderer>().enabled = false;
        }
	}
}
