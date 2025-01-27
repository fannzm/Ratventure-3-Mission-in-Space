using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectiles : MonoBehaviour
{
    private UnityEngine.Vector2 direction; // Flugrichtung des Projektils
    private float speed; // Geschwindigkeit des Projektils
    private int damage; // Schaden, den das Projektil verursacht
    private Rigidbody2D rb; // Rigidbody für Bewegung (falls 2D)

    public Animator projectileAnimator; // Animator für das Projektil, um seine Animation zu steuern

    public void Initialize(UnityEngine.Vector2 playerPosition, UnityEngine.Vector2 playerVelocity, float projectileSpeed, int damage)
    {
        // Position des Projektils
        UnityEngine.Vector2 projectilePosition = transform.position;

        // Vorhersage: Errechne die zukünftige Position des Spielers
        float distance = UnityEngine.Vector2.Distance(playerPosition, projectilePosition);
        float timeToTarget = distance / projectileSpeed;
        UnityEngine.Vector2 futurePosition = playerPosition + playerVelocity * timeToTarget;

        // Berechne die Richtung zum vorhergesagten Punkt
        direction = (futurePosition - projectilePosition).normalized;
        speed = projectileSpeed;
        this.damage = damage;
    }

    void Start()
    {
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
        if (collision.CompareTag("Player"))
        {
            PlayerLife playerLife = collision.GetComponent<PlayerLife>();
            if (playerLife != null)
            {
                playerLife.TakeDamage(damage, "EnemyProjectile");
            }
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Obstacle") || collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        Destroy(gameObject, 3f); // Zerstöre das Projektil nach 3 Sekunden
    }
}
