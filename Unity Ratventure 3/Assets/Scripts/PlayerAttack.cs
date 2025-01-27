using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject laserPrefab; // Das Laserobjekt (Prefab)
    public Transform laserSpawnPoint; // Wo der Laser erscheinen soll
    private bool isAlive = true; // Zustand des Players

    void Update()
    {
        // Überprüfen, ob der Spieler lebt und die Space-Taste gedrückt wurde
        if (isAlive && Input.GetKeyDown(KeyCode.Space))
        {
            FireLaser();
        }
    }

    void FireLaser()
    {
        if (laserPrefab != null && laserSpawnPoint != null)
        {
            // Laserobjekt erstellen
            GameObject laser = Instantiate(laserPrefab, laserSpawnPoint.position, laserSpawnPoint.rotation);

            // Zugriff auf das PlayerLaser-Skript und Zustand übergeben
            PlayerLaser laserScript = laser.GetComponent<PlayerLaser>();
            if (laserScript != null)
            {
                laserScript.SetLaserState(isAlive); // Zustand des Spielers an den Laser weitergeben
            }

            Rigidbody2D playerRb = GetComponent<Rigidbody2D>();
            if (laserScript != null && playerRb != null)
            {
                laserScript.SetInitialVelocity(playerRb.velocity);
            }
        }
    
    }

    // Methode, um den Zustand des Players zu ändern
    public void SetAliveState(bool state)
    {
        isAlive = state;
    }
}
