using UnityEngine.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public int score;
    public TextMeshProUGUI scoreText;
    public GameObject difficultyPopup; // Referenz zum Pop-up
    public Button difficultyButton; // Referenz zum Difficulty Button

    private void Start()
    {
        score = 0;
        score = PlayerPrefs.GetInt("Score_" + PlayerPrefs.GetString("PlayerName", "Unknown"), 0);
        UpdateScore(0);

        // Deaktiviere das Pop-up zu Beginn
        difficultyPopup.SetActive(false);

        // Füge Listener für den Button hinzu
        difficultyButton.onClick.AddListener(OpenDifficultyPopup);
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

    // Methode, um das Schwierigkeits-Pop-up zu öffnen
    public void OpenDifficultyPopup()
    {
        difficultyPopup.SetActive(true); // Pop-up aktivieren
    }

    public DifficultyManager difficultyManager; // Referenz auf den DifficultyManager

    public void SetDifficultyEasy()
    {
        difficultyManager.SetEasyDifficulty();  // Aufruf der richtigen Methode
        difficultyPopup.SetActive(false); // Pop-up schließen
    }

    public void SetDifficultyNormal()
    {
        difficultyManager.SetNormalDifficulty();  // Aufruf der richtigen Methode
        difficultyPopup.SetActive(false); // Pop-up schließen
    }

    public void SetDifficultyHard()
    {
        difficultyManager.SetHardDifficulty();  // Aufruf der richtigen Methode
        difficultyPopup.SetActive(false); // Pop-up schließen
    }

}
