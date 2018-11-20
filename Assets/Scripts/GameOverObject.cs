using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverObject : MonoBehaviour {

    private float alpha;

	// Use this for initialization
	void Start () {
        alpha = 0f;
	}
	
	void FixedUpdate ()
    {
        alpha += 0.01f;
        //fade the screen black
        if (alpha < 1)
        {
            GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, alpha);
        }
        //after a few seconds, restart the level
        else if (alpha >= 2f)
        {
            SceneManager.LoadScene(0);
        }		
	}
}
