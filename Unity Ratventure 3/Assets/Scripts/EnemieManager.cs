using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieManager : MonoBehaviour
{
    private Animator anim;
    public int Damage = 3;

    public int health = 5; // Lebenspunkte des Gegners
    public float lifetime = 15f; // Feste Zeit, nach der der Gegner automatisch zerstört wird
    private float spawnTime; // Zeit, zu der der Gegner gespawnt wurde

    public int scoreValue = 20; // Punkte, die der Player bekommt, wenn er den Enemy tötet


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        spawnTime = Time.time; // Speichert die Zeit des Spawns
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= spawnTime + lifetime)
        {
            Destroy(gameObject);
        }
    }

    // Alte Methode ohne "source", damit der Code in PlayerLaser weiterhin funktioniert
    public void TakeDamage(int damage)
    {
        TakeDamage(damage, "Unknown"); // Standardwert "Unknown" falls keine Quelle angegeben wird
    }

    // Neue Methode mit "source", die die Punkte nur vergibt, wenn es ein "PlayerLaser" ist
    public void TakeDamage(int damage, string source)
    {
        health -= damage;

        if (health <= 0)
        {
            if (source == "PlayerLaser") // Nur wenn der Player den Enemy tötet, gibt es Punkte!
            {
                PlayerScore playerScore = FindObjectOfType<PlayerScore>(); // Holt den Score-Manager
                if (playerScore != null)
                {
                    playerScore.currentScore += scoreValue; // Punkte hinzufügen
                }
            }

            Die();
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Die();
        }
    }

    private void Die()
    {
        anim.SetTrigger("Death");
        Destroy(gameObject, 2f);
    }
}

