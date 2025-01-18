using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteSpawner : MonoBehaviour
{
    public Camera Camera; // Referenz zur Kamera
    public GameObject Player; // Referenz zum Spieler
    public GameObject[] MeteoritePrefabs; // Array von Meteoriten-Prefabs

    public float SpawnRateMin = 0.5f; // Minimale Spawn-Rate
    public float SpawnRateMax = 3f; // Maximale Spawn-Rate

    public float MeteoriteSpeedMin = 3f; // Minimale Geschwindigkeit der Meteoriten
    public float MeteoriteSpeedMax = 6f; // Maximale Geschwindigkeit der Meteoriten

    public float RotationSpeedMin = -30f; // Minimale Rotationsgeschwindigkeit
    public float RotationSpeedMax = 30f;  // Maximale Rotationsgeschwindigkeit

    private float _nextSpawnTime; // Nächste Spawnzeit

    private float elapsedTime = 0f; // Zeit, die seit Spielbeginn vergangen ist
    private float difficultyFactor = 2f; // Skaliert dynamisch mit der Zeit

    public int Damage = 1;

    private void DetermineNextSpawnTime()
    {
        // Setzt die nächste Spawnzeit zufällig zwischen Min und Max
        _nextSpawnTime = Time.time + Random.Range(SpawnRateMin, SpawnRateMax);
    }

    private void Start()
    {
        DetermineNextSpawnTime();
    }

        private void UpdateDifficulty()
        {
            // Verwende difficultyFactor, um die Schwierigkeit zu skalieren
            difficultyFactor = Mathf.Clamp(1f + elapsedTime / 10f, 1f, 3f); // Zum Beispiel von 1x bis 3x nach 30 Sekunden

            // Setze die Meteoriten-Geschwindigkeit basierend auf dem Difficulty-Faktor
            MeteoriteSpeedMin = Mathf.Clamp(3f * difficultyFactor, 3f, 10f); // Geschwindigkeit wird mit der Zeit erhöht
            MeteoriteSpeedMax = Mathf.Clamp(6f * difficultyFactor, 6f, 15f);

            // Setze die Rotationsgeschwindigkeit basierend auf dem Difficulty-Faktor
            RotationSpeedMin = Mathf.Clamp(-30f * difficultyFactor, -30f, -60f);
            RotationSpeedMax = Mathf.Clamp(30f * difficultyFactor, 30f, 60f);

            // Auch die Spawn-Rate könnte angepasst werden, falls gewünscht
            SpawnRateMin = Mathf.Clamp(1.5f / difficultyFactor, 0.5f, 3f);
            SpawnRateMax = Mathf.Clamp(3f / difficultyFactor, 1f, 5f);
        }

    private void Update()
    {

        elapsedTime += Time.deltaTime; // Zeit hochzählen
        UpdateDifficulty(); // Schwierigkeit anpassen

        // Spawnt einen Meteoriten, wenn die Zeit erreicht ist
        if (Time.time >= _nextSpawnTime)
        {
            SpawnMeteorite();
            DetermineNextSpawnTime();
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var playerController = other.GetComponent<PlayerLife>();
            playerController.TakeDamage(Damage);
            Destroy(gameObject);
        }
    }

    private void SpawnMeteorite()
    {
        // Zufälliges Prefab aus dem Array auswählen
        int prefabIndexToSpawn = Random.Range(0, MeteoritePrefabs.Length);
        GameObject prefabToSpawn = MeteoritePrefabs[prefabIndexToSpawn];

        // Instantiate des Prefabs
        GameObject meteorite = Instantiate(prefabToSpawn);

        // X-Position: Spawnt rechts außerhalb des sichtbaren Bereichs der Kamera
        float xPosition = Player.transform.position.x + Camera.orthographicSize * Camera.aspect + 6f;

        // Y-Position: Zufällig innerhalb des vertikalen sichtbaren Bereichs der Kamera
        float yPosition = Random.Range(Camera.transform.position.y - Camera.orthographicSize, Camera.transform.position.y + Camera.orthographicSize);

        // Meteoriten an der berechneten Position platzieren
        meteorite.transform.position = new Vector3(xPosition, yPosition, 0);

        // Geschwindigkeit zufällig einstellen
        float speed = Random.Range(MeteoriteSpeedMin, MeteoriteSpeedMax);

        // Meteoriten in Richtung des Spielers bewegen
        Rigidbody2D rb = meteorite.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = new Vector2(-speed, 0); // Bewegung von rechts nach links
        }

        // Leichte Rotation hinzufügen
        float rotationSpeed = Random.Range(RotationSpeedMin, RotationSpeedMax);
        Rigidbody2D meteoriteRb = meteorite.GetComponent<Rigidbody2D>();
        if (meteoriteRb != null)
        {
            meteoriteRb.angularVelocity = rotationSpeed; // Rotationsgeschwindigkeit
        }

        Destroy(meteorite, 10f); // Meteorite wird nach 10 Sekunden zerstört
    }

}