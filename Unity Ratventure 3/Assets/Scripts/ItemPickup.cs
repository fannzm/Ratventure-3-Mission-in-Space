using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public int scoreValue = 10; // Punkte, die das Item gibt

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Prüft, ob der Player das Item berührt
        {
            PlayerScore playerScore = other.GetComponent<PlayerScore>(); // Referenz auf PlayerScore holen
            if (playerScore != null)
            {
                playerScore.currentScore += scoreValue; // Punkte hinzufügen
            }

            Destroy(gameObject); // Item entfernen
        }
    }
}
