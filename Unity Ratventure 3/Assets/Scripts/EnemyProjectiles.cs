using System.Collections;
using System.Collections.Generic;
using System.Numerics;
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
        if (rb != null)
        {
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            if (player != null)
            {
                UnityEngine.Vector2 targetDirection = ((UnityEngine.Vector2)player.position - (UnityEngine.Vector2)transform.position).normalized;
                direction = UnityEngine.Vector2.Lerp(direction, targetDirection, Time.deltaTime * 2);
                rb.velocity = direction * speed;
            }
        }

        Destroy(gameObject, 2f);
    }


}
