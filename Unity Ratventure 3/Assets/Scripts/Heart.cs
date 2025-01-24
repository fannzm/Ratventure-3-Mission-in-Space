using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public int healingAmount = 1; // Menge an Heilung, die das Herz gibt


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Prüft, ob der Spieler mit dem Herz kollidiert
        {
            var playerController = other.GetComponent<PlayerLife>();
            if (playerController != null)
            {
                playerController.Heal(healingAmount); // Spieler heilt sich
                Destroy(gameObject); // Zerstöre das Herz nach dem Einsammeln
            }
        }
    }
}

