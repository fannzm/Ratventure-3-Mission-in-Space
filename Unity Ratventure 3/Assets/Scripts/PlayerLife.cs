using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerLife : MonoBehaviour
{
    private PlayerScore playerScore; // Referenz auf das PlayerScore-Script
    private Animator anim;

    public int MaximumHealth = 5;
    public int CurrentHealth;
    private float speed = 5f;
    private bool hasShield = false;
    private float shieldDuration = 5f; // Dauer des Schilds

    public HealthBar healthBar;
    private bool isDead = false; // Um Mehrfach-Tod zu verhindern

    private PlayerAttack playerAttack; // Referenz auf das PlayerAttack-Skript

    void Start()
    {
        CurrentHealth = MaximumHealth;
        anim = GetComponent<Animator>();

        // Hole das PlayerScore-Script
        playerScore = FindObjectOfType<PlayerScore>(); // Sucht das PlayerScore-Script im Spiel
        if (playerScore == null)
        {
            Debug.LogError("PlayerScore Script nicht gefunden!");
        }

        // Health Bar initialisieren
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(MaximumHealth);
        }

        // Referenz auf PlayerAttack holen
        playerAttack = GetComponent<PlayerAttack>();
        if (playerAttack == null)
        {
            Debug.LogError("PlayerAttack Script nicht gefunden!");
        }
    }

    public void TakeDamage(int damage, string damageSource)
    {
        if (isDead) return; // Wenn bereits tot, keine weitere Aktion
        if (hasShield)
        {
            Debug.Log("Shield ist aktiv, kein Schaden erhalten!");
            return; // Spieler erleidet keinen Schaden
        }
        CurrentHealth -= damage;

        Debug.Log($"Schaden genommen: {damage} | Quelle: {damageSource} | Aktuelle Gesundheit: {CurrentHealth}");

        // Health Bar aktualisieren
        if (healthBar != null)
        {
            healthBar.SetHealth(CurrentHealth);
        }

        // Prüfen, ob der Spieler tot ist
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;

        isDead = true; // Spieler ist jetzt tot
        Debug.Log("Spieler ist gestorben.");

        // Setze isAlive im PlayerAttack-Skript auf false
        if (playerAttack != null)
        {
            playerAttack.SetAliveState(false);
        }

        // Blockiere die Bewegungen des Spielers
        GetComponent<PlayerMovement>().enabled = false; // Falls du ein PlayerMovement-Skript hast, deaktiviere es

        // Tod-Animation (falls Animator vorhanden)
        if (anim != null)
        {
            anim.SetTrigger("Death");
        }

        // Lade die Szene nach dem Tod des Spielers
        SceneManager.LoadScene(2);
    }

    public void Heal(int amount)
    {
        if (isDead) return; // Keine Heilung, wenn der Spieler tot ist

        CurrentHealth += amount;

        // Gesundheit darf nicht über Maximum steigen
        if (CurrentHealth > MaximumHealth)
        {
            CurrentHealth = MaximumHealth;
        }

        // Health Bar aktualisieren
        if (healthBar != null)
        {
            healthBar.SetHealth(CurrentHealth);
        }

        Debug.Log($"Heilung erhalten: {amount} | Aktuelle Gesundheit: {CurrentHealth}");
    }

    public void IncreaseSpeed(float multiplier)
    {
        speed *= multiplier;
        Debug.Log("Speed increased!");
    }

    public void ActivateShield()
    {
        if (hasShield) return; // Wenn der Spieler bereits ein Schild hat, nichts tun

        hasShield = true;
        Debug.Log("Shield aktiviert!");

        // Schild für eine gewisse Zeit aktivieren
        StartCoroutine(ShieldDuration());
    }

    private IEnumerator ShieldDuration()
    {
        yield return new WaitForSeconds(shieldDuration);

        // Nach der Dauer das Schild deaktivieren
        hasShield = false;
        Debug.Log("Shield deaktiviert.");
    }
}
