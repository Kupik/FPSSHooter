using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Asigură-te că obiectul nu este distrus între scene
        }
        else
        {
            Destroy(gameObject);  // Distruge instanțele suplimentare ale acestui obiect
        }
    }

    public void SaveGameData(int playerScore, int enemyScore, bool playerWon)
    {
        PlayerPrefs.SetInt("PlayerScore", playerScore);
        PlayerPrefs.SetInt("EnemyScore", enemyScore);
        PlayerPrefs.SetInt("PlayerWon", playerWon ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void LoadGameData(out int playerScore, out int enemyScore)
    {
        playerScore = PlayerPrefs.GetInt("PlayerScore", 0);
        enemyScore = PlayerPrefs.GetInt("EnemyScore", 0);
    }
}
