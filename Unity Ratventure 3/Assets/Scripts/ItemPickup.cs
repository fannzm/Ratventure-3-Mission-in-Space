using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Prüft, ob der Player das Item berührt
        {
            // Rufe die ItemPickup-Methode in PlayerLife auf
            PlayerLife playerLife = other.GetComponent<PlayerLife>();
            if (playerLife != null)
            {
                playerLife.ItemPickup(); // Triggert die Methode in PlayerLife
            }
            else
            {
                Debug.LogError("PlayerLife-Skript nicht gefunden!");
            }

            // Zerstöre das eingesammelte Item
            Destroy(gameObject);
        }
    }
}
