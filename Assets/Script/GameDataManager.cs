using System.IO;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    private string filePath;

    void Start()
    {
        // Setează calea fișierului
        filePath = Path.Combine(Application.persistentDataPath, "game_data.txt");
    }

    public void SaveGameData(int playerScore, int enemyScore, bool playerWonMatch)
    {
        // Creează un șir de caractere pentru datele de salvare
        string data = $"Player Score: {playerScore}\nEnemy Score: {enemyScore}\nPlayer Won Match: {playerWonMatch}";

        // Scrie datele în fișier
        File.WriteAllText(filePath, data);
        Debug.Log("Game data saved to " + filePath);
    }

    public string LoadGameData()
    {
        if (File.Exists(filePath))
        {
            // Citește datele din fișier
            return File.ReadAllText(filePath);
        }
        else
        {
            return "No data found";
        }
    }
}
