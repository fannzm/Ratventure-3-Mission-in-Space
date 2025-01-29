using System.Collections;
using System.Collections.Generic;
using UnityEngine; // Für Unity-spezifische Funktionen und Klassen

public class PlayerLaser : MonoBehaviour
{
    public float damage = 3f; // Schaden, den der Laser verursacht
    public Animator laserAnimator; // Animator für die Laseranimation
    public float laserDuration = 1f; // Wie lange der Laser aktiv bleibt
    public float laserSpeed = 7f; // Geschwindigkeit des Lasers

    private UnityEngine.Vector2 initialVelocity; // Spieler-Geschwindigkeit beim Erstellen des Lasers

    // Setzt die Spieler-Geschwindigkeit
    public void SetInitialVelocity(UnityEngine.Vector2 velocity)
    {
        initialVelocity = velocity;
    }

    void Start()
    {
        // Animation des Lasers starten
        if (laserAnimator != null)
        {
            laserAnimator.SetTrigger("Attack");
        }

        Rigidbody2D rb = GetComponent<Rigidbody2D>(); // Hole den Rigidbody des Lasers

        if (rb != null)
        {
            // Addiere die Spieler-Geschwindigkeit zur Laser-Geschwindigkeit
            rb.velocity = (UnityEngine.Vector2)(transform.right * laserSpeed) + initialVelocity;
        }

        Destroy(gameObject, laserDuration); // Zerstöre den Laser nach einer bestimmten Zeit
    }

    public void SetLaserState(bool isAlive)
    {
        // Übergibt den isAlive-Wert an den Animator
        if (laserAnimator != null)
        {
            laserAnimator.SetBool("isAlive", isAlive);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Überprüfen, ob das Objekt ein Gegner ist
        if (collision.CompareTag("Enemies"))
        {
            // Verweis auf das EnemieManager-Skript
            EnemieManager enemyManager = collision.GetComponent<EnemieManager>();
            if (enemyManager != null)
            {
                enemyManager.TakeDamage((int)damage); // Schaden zufügen
            }

            // Optional: Laser deaktivieren, wenn er trifft
            Destroy(gameObject);
        }

        // Überprüfen, ob das Objekt ein Gegner ist
        if (collision.CompareTag("Projectiles"))
        {
            // Optional: Verhindern, dass das Projektil nach der Zerstörung weiter bewegt
            Destroy(collision.gameObject); // Zerstört das Projektil, wenn es vom Laser getroffen wird

            // Optional: Laser deaktivieren, wenn er trifft
            Destroy(gameObject);
        }

    }


}
