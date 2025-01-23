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

    public void OnPlayButtonClicked()
    {
        // Spielernamen speichern
        string playerName = playerNameInputField.text;
        PlayerPrefs.SetString("PlayerName", playerName);
        Debug.Log(name);

        // Spiel starten (z. B. neue Szene laden)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);// Ersetze "GameScene" mit dem Namen deiner Spielszene
    }
  

}
