using UnityEngine;

public class DestroyAfterAnimation : MonoBehaviour {
    private float animTime;
    private bool hasSound;
    private bool hasAnimation;
	// Use this for initialization
	void Start () {
        if (GetComponent<AudioSource>() != null)
            hasSound = true;
        else
            hasSound = false;
        if (GetComponent<Animator>() != null)
            hasAnimation = true;
        else
            hasAnimation = false;
        if (hasAnimation)
            animTime = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        else
            animTime = 0.02f;
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
