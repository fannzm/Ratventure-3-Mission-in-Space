using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectiles : MonoBehaviour
{
    private Vector2 direction; // Flugrichtung des Projektils
    private float speed; // Geschwindigkeit des Projektils
    private int damage; // Schaden, den das Projektil verursacht
    private Rigidbody2D rb; // Rigidbody für Bewegung (falls 2D)

    public Animator projectileAnimator; // Animator für das Projektil, um seine Animation zu steuern

    public void Initialize(Vector2 targetPosition, float projectileSpeed, int damage)
    {
        // Berechne Richtung zum Ziel (Spieler)
        direction = (targetPosition - (Vector2)transform.position).normalized;
        speed = projectileSpeed;
        this.damage = damage;
    }

    void Start()
    {
        // Kein Rigidbody mehr nötig, da die Bewegung manuell durch Setzen der Velocity durchgeführt wird
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * speed; // Setzt die Bewegung des Projektils
        }

        if (projectileAnimator != null)
        {
            projectileAnimator.SetTrigger("Shoot"); // Animation starten
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Überprüfen, ob das Projektil den Spieler oder Hindernis trifft
        if (collision.CompareTag("Player"))
        {
            // Schaden beim Spieler verursachen
            PlayerLife playerLife = collision.GetComponent<PlayerLife>();
            if (playerLife != null)
            {
                playerLife.TakeDamage(damage, "EnemyProjectile");
            }

            // Projektil zerstören
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Obstacle") || collision.CompareTag("Wall"))
        {
            // Projektil zerstören, wenn es ein Hindernis trifft
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // Optional: Projektil nach einer bestimmten Zeit zerstören
        Destroy(gameObject, 5f); // Zerstöre das Projektil nach 5 Sekunden
    }
}
