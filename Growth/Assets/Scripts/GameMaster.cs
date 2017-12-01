using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {

    public GameObject StartButton;
    public GameObject ExitButton;
    public GameObject ScoreRef;

    public bool isShowing;
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
        if (isShowing)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GrowthScene");
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            ToggleMenu();
        }
    }
}
