using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerLife : MonoBehaviour
{
    private PlayerScore playerScore; // Referenz auf das PlayerScore-Script
    private Animator anim;

    public int MaximumHealth = 5;
    public int CurrentHealth;
    public float speed = 7f;
    private bool hasShield = false;
    private float shieldDuration = 5f; // Dauer des Schilds

    public HealthBar healthBar;
    private bool isDead = false; // Um Mehrfach-Tod zu verhindern

    private PlayerAttack playerAttack; // Referenz auf das PlayerAttack-Skript
    private float originalSpeed; // Ursprüngliche Geschwindigkeit

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

        originalSpeed = speed;
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

    public void ActivateSpeedUp()
    {
        StartCoroutine(SpeedUpDuration());
    }


    public void ActivateShield()
    {
        if (hasShield) return; // Wenn der Spieler bereits ein Schild hat, nichts tun

        hasShield = true;
        Debug.Log("Shield aktiviert!");

        // Schild für eine gewisse Zeit aktivieren
        StartCoroutine(ShieldDuration());
    }

    public void ItemPickup()
    {
        if (playerScore != null)
        {
            playerScore.currentScore += 50; // Erhöhe den Score um 10 Punkte
            if (playerScore.mainMenu != null)
            {
                playerScore.mainMenu.UpdateScore(playerScore.currentScore); // Aktualisiere den Score im MainMenu
            }

            Debug.Log("Capsule eingesammelt! Score erhöht um 50 Punkte.");
        }
        else
        {
            Debug.LogError("PlayerScore-Skript nicht gefunden! Score kann nicht erhöht werden.");
        }
    }

    public void EnemieManager()
    {
        if (playerScore != null)
        {
            playerScore.currentScore += 200; // Erhöhe den Score um 10 Punkte
            if (playerScore.mainMenu != null)
            {
                playerScore.mainMenu.UpdateScore(playerScore.currentScore); // Aktualisiere den Score im MainMenu
            }

            Debug.Log("Enenemy getötet! Score erhöht um 200 Punkte.");
        }
        else
        {
            Debug.LogError("PlayerScore-Skript nicht gefunden! Score kann nicht erhöht werden.");
        }
    }

    private IEnumerator ShieldDuration()
    {
        yield return new WaitForSeconds(shieldDuration);

        // Nach der Dauer das Schild deaktivieren
        hasShield = false;
        Debug.Log("Shield deaktiviert.");
    }

    private IEnumerator SpeedUpDuration()
    {
        IncreaseSpeed(2f);  // Verdopple die Geschwindigkeit

        yield return new WaitForSeconds(3f);  // Warten für 3 Sekunden

        speed = originalSpeed;  // Stelle die ursprüngliche Geschwindigkeit wieder her
        Debug.Log("Speed is back to normal.");
    }
}
