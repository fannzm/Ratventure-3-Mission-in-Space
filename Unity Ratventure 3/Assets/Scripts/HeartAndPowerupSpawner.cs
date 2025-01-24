using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartAndPowerupSpawner : MonoBehaviour
{
    public GameObject heartPrefab;         // Herz-Prefab
    public GameObject[] powerupPrefabs;    // Array mit Powerup-Prefabs
    public float spawnInterval = 3f;       // Spawn-Intervall in Sekunden
    public float spawnHeightOffset = 1f;   // Zusätzlicher Offset über dem Spieler
    public GameObject Player;              // Referenz zum Spieler

    public float minY = -5f; // Untere Begrenzung
    public float maxY = 5f;  // Obere Begrenzung

    private void Start()
    {
        // Beginnt den Spawn-Mechanismus nach einem bestimmten Intervall
        InvokeRepeating("SpawnItem", 0f, spawnInterval);
    }

    void SpawnItem()
    {
        // X-Position: Spawnt rechts außerhalb des sichtbaren Bereichs der Kamera
        float xPosition = Player.transform.position.x + Camera.main.orthographicSize * Camera.main.aspect + 6f;

        // Zufällige Y-Position basierend auf der Kamera und dem Spieler
        float yPosition = Random.Range(
            Camera.main.transform.position.y - Camera.main.orthographicSize,
            Camera.main.transform.position.y + Camera.main.orthographicSize
        );

        // Höhe des Spieler anpassen und Offset hinzufügen
        float playerYPosition = Player.transform.position.y; // Spieler-Position
        yPosition = Mathf.Max(yPosition, playerYPosition + spawnHeightOffset); // Höher als der Spieler

        // Y-Position innerhalb des Sichtbereichs der Kamera clamping
        yPosition = Mathf.Clamp(yPosition, Camera.main.transform.position.y - Camera.main.orthographicSize, Camera.main.transform.position.y + Camera.main.orthographicSize);

        // Zufällig entscheiden, ob es ein Herz oder ein Powerup ist
        bool spawnHeart = Random.Range(0f, 1f) > 0.5f; // 50% Chance für Herz oder Powerup

        // Wählen, ob ein Herz oder Powerup gespawnt wird
        GameObject itemToSpawn = spawnHeart ? heartPrefab : powerupPrefabs[Random.Range(0, powerupPrefabs.Length)];

        // Spawn-Position erstellen
        Vector2 spawnPosition = new Vector2(xPosition, yPosition);

        // Objekt spawnen
        GameObject spawnedItem = Instantiate(itemToSpawn, spawnPosition, Quaternion.identity);

        // Zerstörung des Objekts nach einer bestimmten Zeit (z.B. 10 Sekunden)
        Destroy(spawnedItem, 10f);
    }
}
