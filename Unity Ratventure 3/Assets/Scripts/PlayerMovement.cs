using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    public float speed = 7f; // Bewegungsgeschwindigkeit des Spielers
    public float minY = -5f; // Untere Begrenzung der Y-Achse
    public float maxY = 5f;  // Obere Begrenzung der Y-Achse
    private float minX; // Minimaler X-Wert (Begrenzung für Rückwärtsbewegung)

    private Transform playerTransform;

    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        playerTransform = transform;
        minX = playerTransform.position.x; // Setze minX auf die Startposition des Spielers
    }

    private void Update()
    {
        // Spielerbewegung auf der X- und Y-Achse
        float dirX = Input.GetAxis("Horizontal");
        float dirY = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(dirX * speed, dirY * speed);

        // Spielerposition entlang der Y-Achse begrenzen
        ClampPosition();
        // Vorwärtsbewegung (positiv) basierend auf Benutzereingabe
        float move = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        // Berechne die neue Position des Spielers
        float newPosX = playerTransform.position.x + move;

        // Begrenze die Rückwärtsbewegung: Spieler kann nicht weiter als minX zurück
        if (newPosX < minX)
        {
            newPosX = minX; // Stoppe den Spieler an der dynamischen Grenze
        }

        // Setze die neue Position des Spielers
        playerTransform.position = new Vector3(newPosX, playerTransform.position.y, playerTransform.position.z);

        // Verschiebe die Rückwärtsgrenze basierend auf dem Fortschritt des Spielers
        // Beispiel: Verschiebe minX alle 10 Einheiten des Fortschritts
        if (newPosX > minX + 15f) // Wenn der Spieler mehr als 10 Einheiten vorwärts geht
        {
            UpdateMinX(newPosX);
        }
    }
    // Diese Methode verschiebt minX basierend auf dem Fortschritt des Spielers
    public void UpdateMinX(float playerProgress)
    {
        minX = playerProgress; // Verschiebe die Grenze nach vorne, basierend auf dem Fortschritt
    }
    private void ClampPosition()
    {
        // Begrenze die Y-Position des Spielers
        Vector3 clampedPosition = transform.position;
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);

        // Setze die Position des Spielers zurück
        transform.position = new Vector3(transform.position.x, clampedPosition.y, transform.position.z);
    }

}

