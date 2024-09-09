using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject winRoundPanel;
    public GameObject loseRoundPanel;
    public GameObject winMatchPanel;
    public GameObject loseMatchPanel;
    public Button continueButtonWin;
    public Button continueButtonLose;

    public Button newMatchButtonWin;
    public Button newMatchButtonLose;

    public Text playerScoreText;
    public Text enemyScoreText;

    public Text playerWinScoreText;
    public Text enemyWinScoreText;
    public Text playerLoseScoreText;
    public Text enemyLoseScoreText;

    private int playerScore = 0;
    private int enemyScore = 0;
    private const int maxRounds = 4;

    public GameObject player;
    public GameObject[] enemies;

    public Transform playerSpawnPoint;
    public Transform[] enemySpawnPoints;

    private PlayerHealth playerHealth;
    private ScoreManager scoreManager;  // Înlocuiește GameDataManager cu ScoreManager

    void Start()
    {
        winRoundPanel.SetActive(false);
        loseRoundPanel.SetActive(false);
        winMatchPanel.SetActive(false);
        loseMatchPanel.SetActive(false);

        continueButtonWin.onClick.AddListener(OnContinueWin);
        continueButtonLose.onClick.AddListener(OnContinueLose);

        newMatchButtonWin.onClick.AddListener(OnNewMatch);
        newMatchButtonLose.onClick.AddListener(OnNewMatch);

        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
        }

        scoreManager = ScoreManager.Instance;

        // Încarcă scorurile la început
        scoreManager.LoadGameData(out playerScore, out enemyScore);
        UpdateScoreText();
    }

    public void PlayerWinsRound()
    {
        playerScore++;
        UpdateScoreText();

        if (playerScore >= maxRounds)
        {
            playerWinScoreText.text = "" + playerScore;
            enemyWinScoreText.text = "" + enemyScore;
            winMatchPanel.SetActive(true);
            loseMatchPanel.SetActive(false);
            scoreManager.SaveGameData(playerScore, enemyScore, true);
        }
        else
        {
            playerWinScoreText.text = "" + playerScore;
            enemyWinScoreText.text = "" + enemyScore;
            winRoundPanel.SetActive(true);
        }
    }

    public void EnemyWinsRound()
    {
        enemyScore++;
        UpdateScoreText();

        if (enemyScore >= maxRounds)
        {
            playerLoseScoreText.text = "" + playerScore;
            enemyLoseScoreText.text = "" + enemyScore;
            loseMatchPanel.SetActive(true);
            winMatchPanel.SetActive(false);
            scoreManager.SaveGameData(playerScore, enemyScore, false);
        }
        else
        {
            playerLoseScoreText.text = "" + playerScore;
            enemyLoseScoreText.text = "" + enemyScore;
            loseRoundPanel.SetActive(true);
        }
    }

    public void OnContinueWin()
    {
        winRoundPanel.SetActive(false);
        SaveScores();
        ResetScene();
    }

    public void OnContinueLose()
    {
        loseRoundPanel.SetActive(false);
        SaveScores();
        ResetScene();
    }

    public void OnNewMatch()
    {
        SaveScores();
        playerScore = 0;
        enemyScore = 0;
        UpdateScoreText();

        winMatchPanel.SetActive(false);
        loseMatchPanel.SetActive(false);
        StartNewMatch();
    }

    void SaveScores()
    {
        scoreManager.SaveGameData(playerScore, enemyScore, true);
    }

    void UpdateScoreText()
    {
        playerScoreText.text = "" + playerScore;
        enemyScoreText.text = "" + enemyScore;
    }

    void StartNewRound()
    {
        Debug.Log("Runda a fost reîncepută.");

        if (player != null)
        {
            player.SetActive(true);
            player.transform.position = playerSpawnPoint.position;
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.ResetHealth();
            }
        }

        foreach (var enemy in enemies)
        {
            if (enemy != null)
            {
                Transform spawnPoint = enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length)];
                enemy.transform.position = spawnPoint.position;
                enemy.SetActive(true);

                Target enemyTarget = enemy.GetComponent<Target>();
                if (enemyTarget != null)
                {
                    enemyTarget.ResetHealth();
                }
            }
        }
    }

    void StartNewMatch()
    {
        Debug.Log("Un nou meci a început.");
        StartNewRound();
    }

    void ResetScene()
    {
        // Folosește UnityEngine.SceneManagement pentru a reseta scena
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
