using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Animator anim;

    public int MaximumHealth = 5;
    public int CurrentHealth;
    private float speed = 5f;
    private bool hasShield = false;

    public HealthBar healthBar;
    private bool isDead = false; // Um Mehrfach-Tod zu verhindern

    void Start()
    {
        CurrentHealth = MaximumHealth;
        anim = GetComponent<Animator>();

        // Health Bar initialisieren
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(MaximumHealth);
        }
    }

    public void TakeDamage(int damage, string damageSource)
    {
        if (isDead) return; // Wenn bereits tot, keine weitere Aktion

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

        // Blockiere die Bewegungen des Spielers (Beispiel, falls du Bewegungslogik hast)
        // Optional: Stelle sicher, dass alle Player-bezogenen Scripts hier gestoppt werden
        GetComponent<PlayerMovement>().enabled = false; // Falls du ein PlayerMovement-Skript hast, deaktiviere es

        // Tod-Animation (falls Animator vorhanden)
        if (anim != null)
        {
            anim.SetTrigger("Death");
        }
        SceneManager.LoadScene(2);
        // Szene nach kurzer Verzögerung neu laden
        //Invoke(nameof(ReloadScene), 0.5f); // 2 Sekunden Verzögerung
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

    // Funktion zum Erhöhen der Geschwindigkeit
    public void IncreaseSpeed(float multiplier)
    {
        speed *= multiplier;
        // Hier kannst du auch visuelle oder andere Rückmeldungen geben, dass die Geschwindigkeit erhöht wurde
        Debug.Log("Speed increased!");
    }

    // Funktion für das Aktivieren des Schilds
    public void ActivateShield()
    {
        hasShield = true;
        // Du kannst auch ein Schild-Sprite oder eine andere visuelle Rückmeldung anzeigen
        Debug.Log("Shield activated!");
    }

 
}



