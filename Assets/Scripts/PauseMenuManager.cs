using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour {

    private bool isPaused;

	// Use this for initialization
	void Start () {
        isPaused = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Escape"))
        {
            if (!isPaused)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
            isPaused = !isPaused;
            GetComponent<AudioSource>().Play();
        }

    }
}
