using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemieManager : MonoBehaviour
{
    private Animator anim;
    public int Damage = 3;

    public int health = 5; // Lebenspunkte des Gegners
    public float lifetime = 15f; // Feste Zeit, nach der der Gegner automatisch zerstört wird
    private float spawnTime; // Zeit, zu der der Gegner gespawnt wurde
    private bool isDead=false;


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
        if (isDead) return;
       
        health -= damage;

        if (health <= 0)
        {

            Die();

            PlayerLife playerLife = FindObjectOfType<PlayerLife>();
            if (playerLife != null)
            {
                playerLife.EnemieManager(); // Triggert die Methode in PlayerLife
            }
            else
            {
                Debug.LogError("PlayerLife-Skript nicht gefunden!");
            }

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
        if (isDead) return; // Verhindert, dass der Gegner nach dem ersten Tod nochmal stirbt

        isDead = true; // Markiert den Gegner als tot
        anim.SetTrigger("Death");
        Destroy(gameObject, 2f);
    }
}

