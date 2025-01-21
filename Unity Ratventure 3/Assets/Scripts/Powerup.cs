using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public enum PowerupType
    {
        SpeedUp,
        Shield,
        ExtraLife
    }

    public PowerupType powerupType;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var playerController = other.GetComponent<PlayerLife>();

            switch (powerupType)
            {
                case PowerupType.SpeedUp:
                    // Beispiel: Geschwindigkeit erhöhen
                    playerController.IncreaseSpeed(2f); // Beispiel-Funktion
                    break;
                case PowerupType.Shield:
                    // Beispiel: Schild aktivieren
                    playerController.ActivateShield();
                    break;
                case PowerupType.ExtraLife:
                    // Beispiel: Extra Leben hinzufügen
                    playerController.Heal(1); // Macht das gleiche wie Heal, aber du könntest auch eine spezifische "Extra Life"-Methode erstellen
                    break;
            }

            Destroy(gameObject); // Powerup nach dem Einsammeln zerstören
        }
    }
}

