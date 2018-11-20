using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public Text scoreText;
    public Text timeText;

    private string scoreString;
    private string timeString;
    private int score;
    private TimeSpan time;

	// Use this for initialization
	void Start () {
        scoreString = "SCORE:000000";
        timeString = "TIME:";
        score = 0;
        time = TimeSpan.Zero;
    }
	
	// Update is called once per 16ms
    // actually 62.5fps
	void FixedUpdate () {
        if (Time.timeScale != 0)
        {
            time += new TimeSpan(0, 0, 0, 0, 16);
        }
        UpdateTimeString();
        scoreText.text = scoreString;
        timeText.text = timeString;
	}

    public void UpdateTimeString()
    {
        timeString = "TIME:";
        if (time.Minutes.ToString().Length < 2)
            timeString += "0";
        timeString += time.Minutes.ToString() + ":";
        if (time.Seconds.ToString().Length < 2)
            timeString += "0";
        timeString += time.Seconds.ToString() + ":";
        string ms = time.Milliseconds.ToString();
        if (ms.Length < 2)
            timeString += "0";
        else if (ms.Length > 2)
            ms = ms.Substring(0,2);
        timeString += ms;
    }

    public void AddScore(int points)
    {
        score += points;
        scoreString = "SCORE:";
        if (score.ToString().Length < 6)
        {
            for (int chr = 0; chr < 6 - score.ToString().Length; chr++)
            {
                scoreString += "0";
            }
        }
        scoreString += score.ToString();
    }
}
