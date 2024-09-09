using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;
    public float maxHealth = 50f; // Valoarea maximă a sănătății

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
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");

        // Verifică dacă GameController există și apelează metoda corespunzătoare
        if (gameController != null)
        {
            gameController.PlayerWinsRound(); // Player câștigă runda
        }

        // Dezactivează inamicul
        gameObject.SetActive(false);
    }

    // Metodă pentru resetarea sănătății
    public void ResetHealth()
    {
        health = maxHealth;
    }
}
