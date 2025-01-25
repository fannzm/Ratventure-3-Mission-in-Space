using UnityEngine;
using TMPro;
using System.Collections.Generic;

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

        // Eine Liste von Spielernamen und Scores erstellen
        List<PlayerScoreData> playerScores = new List<PlayerScoreData>();
        foreach (string playerName in allPlayers.Split(','))
        {
            if (!string.IsNullOrEmpty(playerName)) // Leere Einträge ignorieren
            {
                int score = PlayerPrefs.GetInt("Score_" + playerName, 0); // Score des Spielers abrufen
                playerScores.Add(new PlayerScoreData(playerName, score));
            }
        }

        // Sortiere die Liste nach Score (absteigend)
        playerScores.Sort((x, y) => y.score.CompareTo(x.score));

        // Zeige nur die Top 5 Spieler an
        int rank = 1;
        foreach (var player in playerScores)
        {
            if (rank > 5) break; // Nur die ersten 5 Spieler anzeigen
            leaderboardText.text += $"{rank}. {player.name}: {player.score}\n";
            rank++;
        }
    }
}

// Hilfsklasse für Spieler und ihren Score
public class PlayerScoreData
{
    public string name;
    public int score;

    public PlayerScoreData(string name, int score)
    {
        this.name = name;
        this.score = score;
    }
}




