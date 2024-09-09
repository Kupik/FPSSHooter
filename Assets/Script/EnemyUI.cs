using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    public Slider healthSlider; // Referință la Slider-ul din UI pentru inamic
    public Target enemy; // Referință la scriptul Target (care gestionează sănătatea inamicului)

    void Start()
    {
        if (healthSlider == null)
        {
            Debug.LogError("Slider-ul nu este setat în EnemyUI!");
        }
        if (enemy == null)
        {
            Debug.LogError("Scriptul Target nu este setat în EnemyUI!");
        }
    }

    void Update()
    {
        // Actualizează valoarea Slider-ului în funcție de sănătatea inamicului
        if (enemy != null && healthSlider != null)
        {
            healthSlider.value = enemy.health; // Accesăm sănătatea din componenta Target
        }
    }
}
