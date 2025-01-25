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
        score = PlayerPrefs.GetInt("Score_" + PlayerPrefs.GetString("PlayerName", "Unknown"), 0);
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
        SavePlayerScore(score);
    }

    // Speichere den Score, der als Parameter übergeben wird
    public void SavePlayerScore(int score)
    {
        string playerName = PlayerPrefs.GetString("PlayerName", "Unknown");
        PlayerPrefs.SetInt("Score_" + playerName, score); // Speichert den Score mit dem Spielernamen
        PlayerPrefs.Save(); // Speichern
        Debug.Log($"Score gespeichert: {score} für Spieler {playerName}"); // Debug-Ausgabe
    }

    public void RestartGame()
    {
        string playerName = PlayerPrefs.GetString("PlayerName", "Unknown");

        // Setze den Score zurück und überschreibe den alten Wert, falls der Spielername bereits existiert
        PlayerPrefs.SetInt("Score_" + playerName, 0); // Überschreibe den Score für diesen Spieler
        PlayerPrefs.Save(); // Speichern
        // Lädt die aktuelle Szene neu
        SceneManager.LoadScene("SampleScene");
    }

    public void GoToMainMenu()
    {
        // Die Main Menu Scene laden (ersetze "MainMenu" durch den Namen deiner Hauptmenüszene)
        SceneManager.LoadScene("MainMenu");
    }
}
