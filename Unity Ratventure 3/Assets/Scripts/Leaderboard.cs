using UnityEngine;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    public TMP_Text leaderboardText;

    void Start()
    {
        DisplayLeaderboard(); // Leaderboard beim Start der Szene anzeigen
    }

    void DisplayLeaderboard()
    {
        leaderboardText.text = ""; // Reset des Texts

        // Alle gespeicherten Spieler durchgehen
        string allPlayers = PlayerPrefs.GetString("AllPlayers", "");
        if (string.IsNullOrEmpty(allPlayers))
        {
            leaderboardText.text = "Kein Spieler gespeichert!";
            return;
        }

        foreach (string playerName in allPlayers.Split(','))
        {
            if (!string.IsNullOrEmpty(playerName)) // Leere Einträge ignorieren
            {
                int score = PlayerPrefs.GetInt(playerName, 0); // Score des Spielers abrufen
                leaderboardText.text += $"{playerName}: {score}\n"; // Anzeige des Spielers und Score
            }
        }
    }
}



