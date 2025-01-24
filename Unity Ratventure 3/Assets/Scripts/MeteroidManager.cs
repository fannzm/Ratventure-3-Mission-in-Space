using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteSpawner : MonoBehaviour
{
    public Camera Camera; // Referenz zur Kamera
    public Transform player; // Referenz zum Spieler
    public GameObject[] MeteoritePrefabs; // Array von Meteoriten-Prefabs

    public float SpawnRateMin = 0.5f; // Minimale Spawn-Rate
    public float SpawnRateMax = 3f; // Maximale Spawn-Rate
    public int MaxMeteorites = 15; // Maximale Anzahl aktiver Meteoriten

    public float MeteoriteSpeedMin = 3f; // Minimale Geschwindigkeit der Meteoriten
    public float MeteoriteSpeedMax = 6f; // Maximale Geschwindigkeit der Meteoriten

    public float RotationSpeedMin = -30f; // Minimale Rotationsgeschwindigkeit
    public float RotationSpeedMax = 30f;  // Maximale Rotationsgeschwindigkeit

    private float _nextSpawnTime; // Nächste Spawnzeit
    private float elapsedTime = 0f; // Zeit, die seit Spielbeginn vergangen ist
    private float difficultyFactor = 4f; // Skaliert dynamisch mit der Zeit

    private int activeMeteorites = 0; // Aktuelle Anzahl aktiver Meteoriten
    public int Damage = 1;

    private void Start()
    {
        if (Camera == null)
        {
            Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        DetermineNextSpawnTime();
    }

    private void UpdateDifficulty()
    {
        difficultyFactor = Mathf.Clamp(1f + elapsedTime / 10f, 1f, 3f);

        MeteoriteSpeedMin = Mathf.Clamp(3f * difficultyFactor, 3f, 10f);
        MeteoriteSpeedMax = Mathf.Clamp(6f * difficultyFactor, 6f, 15f);

        RotationSpeedMin = Mathf.Clamp(-30f * difficultyFactor, -30f, -60f);
        RotationSpeedMax = Mathf.Clamp(30f * difficultyFactor, 30f, 60f);

        SpawnRateMin = Mathf.Clamp(2f / difficultyFactor, 2f, 3f);
        SpawnRateMax = Mathf.Clamp(4f / difficultyFactor, 3f, 7f);
    }

    private void DetermineNextSpawnTime()
    {
        // Prüfen, ob die maximale Anzahl aktiver Meteoriten erreicht ist
        _nextSpawnTime = Time.time + UnityEngine.Random.Range(SpawnRateMin, SpawnRateMax);
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        UpdateDifficulty();

        // Nur spawn, wenn weniger Meteoriten als die Maximalzahl existieren
        if (Time.time >= _nextSpawnTime && activeMeteorites < MaxMeteorites)
        {
            SpawnMeteorite();
            DetermineNextSpawnTime();
        }
    }

    private void SpawnMeteorite()
    {
        if (activeMeteorites >= MaxMeteorites)
        {
            Debug.Log("MaxMeteorites erreicht. Kein neuer Spawn.");
            return; // Verhindert das Spawnen eines neuen Meteoriten
        }

        if (MeteoritePrefabs.Length == 0) return;

        // Zufälliges Prefab auswählen und instanziieren
        int prefabIndexToSpawn = UnityEngine.Random.Range(0, MeteoritePrefabs.Length);
        GameObject prefabToSpawn = MeteoritePrefabs[prefabIndexToSpawn];
        GameObject meteorite = Instantiate(prefabToSpawn);

        // Position des Meteoriten setzen
        float xPosition = player.position.x + Camera.orthographicSize * Camera.aspect + 6f;
        float yPosition = UnityEngine.Random.Range(Camera.transform.position.y - Camera.orthographicSize, Camera.transform.position.y + Camera.orthographicSize);

        meteorite.transform.position = new Vector3(xPosition, yPosition, 0);

        // Geschwindigkeit zufällig einstellen
        float speed = UnityEngine.Random.Range(MeteoriteSpeedMin, MeteoriteSpeedMax);
        Rigidbody2D rb = meteorite.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = new Vector2(-speed, 0);
            rb.angularVelocity = UnityEngine.Random.Range(RotationSpeedMin, RotationSpeedMax);
        }

        // Anzahl der aktiven Meteoriten erhöhen
        activeMeteorites++;

        // Meteoriten nach links hinaus gehen lassen und zerstören, wenn sie den Bildschirm verlassen
        StartCoroutine(CheckMeteoritePosition(meteorite));
    }

    private IEnumerator CheckMeteoritePosition(GameObject meteorite)
    {
        while (meteorite != null)
        {
            if (meteorite.transform.position.x < Camera.transform.position.x - Camera.orthographicSize * Camera.aspect)
            {
                Destroy(meteorite); // Zerstöre den Meteoriten, wenn er den Bildschirm verlassen hat
                activeMeteorites--; // Verringere die Anzahl der aktiven Meteoriten
                yield break; // Beende die Coroutine
            }

            yield return null; // Warten, bis der nächste Frame kommt
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var playerController = other.GetComponent<PlayerLife>();
            playerController.TakeDamage(Damage, "Meteorite");
            Destroy(gameObject);
        }
    }
}
