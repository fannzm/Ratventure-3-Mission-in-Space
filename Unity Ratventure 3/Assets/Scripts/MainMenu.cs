using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    //public void PlayGame()
    //{SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    //}
    public int score;

    public TextMeshProUGUI scoreText;

    private void Start ()
    {
        score = 0;
        UpdateScore(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }
}
