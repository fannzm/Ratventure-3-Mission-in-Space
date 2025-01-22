using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Transform player; // Referenz auf den Spieler
    public float attackRange = 2f; // Entfernung, bei der der Feind angreifen soll
    public Animator animator; // Animator-Komponente für die Animation
    public GameObject projectilePrefab; // Projektil, das der Feind abfeuert
    public Transform firePoint; // Ort, von dem aus das Projektil abgefeuert wird
    public float attackCooldown = 2f; // Zeit zwischen Angriffen

    private float lastAttackTime = 0f;

    void Update()
    {
        if (player != null)
        {
            // Berechne die Entfernung auf der x-Achse
            float distanceX = Mathf.Abs(player.position.x - transform.position.x);

            // Wenn der Spieler in Reichweite ist
            if (distanceX <= attackRange && Time.time >= lastAttackTime + attackCooldown)
            {
                Attack();
            }
        }
    }

    void Attack()
    {

        // Angriffsanimation abspielen
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

        // Projektil abfeuern
        if (projectilePrefab != null && firePoint != null)
        {
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        }

        // Cooldown aktualisieren
        lastAttackTime = Time.time;
    }
}

