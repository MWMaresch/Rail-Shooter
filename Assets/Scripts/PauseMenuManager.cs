using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour {

    public GameObject menu;

    private bool isPaused;

	// Use this for initialization
	void Start ()
    {
        menu.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Escape"))
            TogglePause();
    }

    public void TogglePause()
    {
        if (!isPaused)
        {
            menu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            menu.SetActive(false);
            Time.timeScale = 1;
        }
        isPaused = !isPaused;
        GetComponent<AudioSource>().Play();
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
