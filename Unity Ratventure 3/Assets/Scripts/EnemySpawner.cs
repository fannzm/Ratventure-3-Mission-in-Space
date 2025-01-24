using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject[] EnemyPrefabs; // Verschiedene Gegner-Varianten
    public Transform player; // Referenz zum Spieler
    public float spawnInterval = 50f; // Distanz, nach der ein Gegner spawnen soll (in Metern)
    public float spawnDistanceAhead = 5f; // Entfernung vor dem Spieler, bei der der Gegner spawnt
    public float difficultyScaling = 1.2f; // Skalierung der Gegner-Stärke (Gesundheit, Schaden)

    public float SpawnRateMin = 1f; // Minimale Spawn-Rate
    public float SpawnRateMax = 3f; // Maximale Spawn-Rate
    public int MaxEnemies = 6; // Maximale Anzahl aktiver Meteoriten
    private float _nextSpawnTime; // Nächste Spawnzeit

    // Begrenzung der Y-Position für die Gegner
    public float minY = -5f; // Untere Begrenzung
    public float maxY = 5f;  // Obere Begrenzung

    private float lastSpawnDistance = 0f; // Zuletzt erreichte Distanz des Spielers
    private int waveLevel = 1; // Aktuelle Welle

    void Start()
    {
        lastSpawnDistance = player.position.x; // Setze die Startposition des Spielers als Referenz
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // Berechne die Distanz, die der Spieler zurückgelegt hat
            float playerDistanceTravelled = player.position.x - lastSpawnDistance;

            if (playerDistanceTravelled >= spawnInterval)
            {
                SpawnEnemyBasedOnProgress(); // Gegner spawnen, wenn die Distanz erreicht ist
                lastSpawnDistance = player.position.x; // Aktualisiere die letzte Position des Spielers
            }

            yield return null; // Warten bis zum nächsten Frame
        }
    }

    void SpawnEnemyBasedOnProgress()
    {
        if (EnemyPrefabs.Length == 0)
        {
            Debug.LogError("Keine Enemy-Prefabs zugewiesen!");
            return;
        }

        // Wähle einen zufälligen Gegner aus
        GameObject enemyPrefab = EnemyPrefabs[UnityEngine.Random.Range(0, EnemyPrefabs.Length)];

        // Generiere eine Position für den Gegner, 5 Meter vor dem Spieler
        float xOffset = UnityEngine.Random.Range(-spawnDistanceAhead, spawnDistanceAhead); // X-Abstand variiert etwas
        float yOffset = UnityEngine.Random.Range(minY, maxY); // Y-Abstand für zufällige Position im erlaubten Bereich
        Vector3 spawnPosition = new Vector3(player.position.x + spawnDistanceAhead + xOffset, Mathf.Clamp(player.position.y + yOffset, minY, maxY), 0);

        // Spawn Gegner
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        // Gegnerstärke skalieren je nach Welle
        EnemieManager enemyScript = enemy.GetComponent<EnemieManager>();
        if (enemyScript != null)
        {
            enemyScript.health = Mathf.RoundToInt(enemyScript.health * Mathf.Pow(difficultyScaling, waveLevel));
        }

        // Passe die Angriffswerte basierend auf der Welle an
        EnemyAttack enemyAttack = enemy.GetComponent<EnemyAttack>();
        if (enemyAttack != null)
        {
            enemyAttack.damage = Mathf.RoundToInt(enemyAttack.damage * Mathf.Pow(difficultyScaling, waveLevel)); // Gegner macht mehr Schaden
            enemyAttack.attackCooldown = Mathf.Clamp(enemyAttack.attackCooldown - 0.1f * waveLevel, 0.5f, 2f); // Schnellere Angriffe
            enemyAttack.projectileSpeed += waveLevel * 0.5f; // Schnellere Projektile
        }

        // Steigere die Welle für den nächsten Spawn
        waveLevel++;
    }

    private void DetermineNextSpawnTime()
    {
        // Prüfen, ob die maximale Anzahl aktiver Meteoriten erreicht ist
        _nextSpawnTime = Time.time + UnityEngine.Random.Range(SpawnRateMin, SpawnRateMax);
    }
}
