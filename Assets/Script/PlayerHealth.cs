using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;
    public float maxHealth = 100f; // Valoarea maximă a sănătății

    private GameController gameController; // Referință la GameController

    void Start()
    {
        // Obține referința la GameController
        gameController = FindObjectOfType<GameController>();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die(); // Gestionează moartea jucătorului
        }
    }

    void Die()
    {
        Debug.Log("Player died!");

        // Verifică dacă GameController există și apelează metoda corespunzătoare
        if (gameController != null)
        {
            gameController.EnemyWinsRound(); // Inamicul câștigă runda
        }

        // Poți adăuga și alte funcționalități, cum ar fi oprirea jocului sau resetarea nivelului
        gameObject.SetActive(false); // Dezactivează jucătorul, dar nu îl distruge
    }

    // Metodă pentru resetarea sănătății
    public void ResetHealth()
    {
        health = maxHealth;
    }
}
