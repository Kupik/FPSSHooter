using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Slider healthSlider; // Referință la Slider-ul din UI
    public PlayerHealth playerHealth; // Referință la scriptul PlayerHealth

    void Start()
    {
        if (healthSlider == null)
        {
            Debug.LogError("Slider-ul nu este setat în PlayerUI!");
        }
        if (playerHealth == null)
        {
            Debug.LogError("PlayerHealth-ul nu este setat în PlayerUI!");
        }
    }

    void Update()
    {
        // Actualizează valoarea Slider-ului în funcție de sănătatea jucătorului
        if (playerHealth != null && healthSlider != null)
        {
            healthSlider.value = playerHealth.health;
        }
    }
}
