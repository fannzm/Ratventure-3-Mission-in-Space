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

    public void TakeDamage(int damage)
    {
        health -= damage; // Reduziert die Lebenspunkte
        if (health <= 0)
        {
            Destroy(gameObject); // Zerstört den Gegner, wenn seine Lebenspunkte 0 erreichen
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

