using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Transform player; // Referenz auf den Spieler, nun dynamisch über den Tag
    public float attackRange = 2f; // Entfernung, bei der der Feind angreifen soll
    public Animator animator; // Animator-Komponente für die Animation
    public GameObject projectilePrefab; // Projektil, das der Feind abfeuert
    public Transform firePoint; // Ort, von dem aus das Projektil abgefeuert wird
    public float attackCooldown = 2f; // Zeit zwischen Angriffen
    public float projectileSpeed = 5f; // Geschwindigkeit des Projektils
    public int damage = 2; // Schaden, den das Projektil verursacht

    private float lastAttackTime = 0f;




    void Start()
    {
       
        player = GameObject.FindGameObjectWithTag("Player").transform; // Finde den Player über Tag

       
    }


    void Update()
    {
        float distanceX = 0;
        if (distanceX <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
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
            // Erstelle das Projektil und initialisiere es
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

            // Initialisiere das Projektil mit der Position und Geschwindigkeit des Spielers
            EnemyProjectiles enemyProjectile = projectile.GetComponent<EnemyProjectiles>();
            if (enemyProjectile != null)
            {
                Rigidbody2D playerRigidbody = player.GetComponent<Rigidbody2D>(); // Hole den Rigidbody des Spielers
                Vector2 playerVelocity = playerRigidbody != null ? playerRigidbody.velocity : Vector2.zero;

                // Zielposition ist die des Spielers, Geschwindigkeit und Schaden werden gesetzt
                enemyProjectile.Initialize((Vector2)player.position, playerVelocity, projectileSpeed, damage);
            }

            if (enemyProjectile.projectileAnimator != null)
            {
                enemyProjectile.projectileAnimator.SetTrigger("Shoot"); // Setzt den Trigger für die Projektil-Animation
            }
        }

        // Cooldown aktualisieren
        lastAttackTime = Time.time;
    }
}