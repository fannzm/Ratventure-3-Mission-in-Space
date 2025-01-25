using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerNameInput : MonoBehaviour
{
    public TMP_InputField playerNameInputField; // Das InputField für den Spielernamen
    public Button playButton; // Der Play-Button

    private void Start()
    {
        // Button zunächst deaktivieren
        playButton.interactable = false;

        // InputField überwachen
        playerNameInputField.onValueChanged.AddListener(OnNameInputChanged);
    }

    private void OnNameInputChanged(string input)
    {
        // Button aktivieren, wenn das InputField nicht leer ist
        playButton.interactable = !string.IsNullOrEmpty(input);
    }

    void OnPlayButtonClicked()
    {
        string playerName = playerNameInputField.text;
        PlayerPrefs.SetString("PlayerName", playerName);

        // Alle Spieler in einer Liste speichern
        string allPlayers = PlayerPrefs.GetString("AllPlayers", "");
        if (string.IsNullOrEmpty(allPlayers))
        {
            allPlayers = playerName; // Wenn keine Spieler vorhanden sind, setze den ersten Spieler
        }
        else
        {
            allPlayers += "," + playerName; // Füge den neuen Spielernamen hinzu
        }

        PlayerPrefs.SetString("AllPlayers", allPlayers);
        PlayerPrefs.Save(); // Speichern

        // Spiel starten
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }



}
