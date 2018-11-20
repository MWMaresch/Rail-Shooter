using UnityEngine;

public class EnemyCrosshair : MonoBehaviour {

    public GameObject parentLaser;
    public RuntimeAnimatorController closeAnim;

    private bool changedAnim;
    private float alpha;

	// Use this for initialization
	void Start () {
        changedAnim = false;
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        alpha = 0f;
    }
	
	void FixedUpdate ()
    {
        if (parentLaser == null || parentLaser.transform.position.z < 20f)
            Destroy(gameObject);
        else if (!changedAnim)
        {
            if (parentLaser.transform.position.z < 30f)
            {
                //GetComponent<AudioSource>().Play(); //right now, this sounds terrible
                GetComponent<SpriteRenderer>().color = Color.white;
                GetComponent<Animator>().runtimeAnimatorController = closeAnim;
                changedAnim = true;
            }
            else if (alpha < 0.5f)
            {
                alpha += 0.05f;
                GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, alpha);
            }
        }
	}
}
