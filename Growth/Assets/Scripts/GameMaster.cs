using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {

    public GameObject StartButton;
    public GameObject ExitButton;
    public GameObject Blur;
    public GameObject MainMenuCam;
    public GameObject ScoreRef;

    private bool isShowing = true;
    private bool gameStarted = false;
    private int score = 0;

    public void UpdateScore()
    {
        ScoreRef.GetComponent<Text>().text = "Score: " + score;
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScore();
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScore();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToggleMenu()
    {
        isShowing = !isShowing;
        StartButton.SetActive(isShowing);
        ExitButton.SetActive(isShowing);
        Blur.SetActive(isShowing);
        MainMenuCam.SetActive(isShowing);
        if (isShowing && gameStarted)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (!gameStarted)
            {
                gameStarted = true;
            }
            ToggleMenu();
        }
    }
}
