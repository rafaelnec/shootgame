using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    [Header("Game")]
    private static GameController _instance;
    private int _countToNextSubLevel = 0;

    [SerializeField] private GameLevelSettings gameLevelSettings;    
    [SerializeField] private int gameLevel = 1;
    [SerializeField] private int subLevel = 1;
    [SerializeField] private GameObject gameOver;
    
    public UnityEvent<int> LoadSubLevel;
    public UnityEvent<int> LoadNextGameLevel;

    [Header("Players")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Player player;
    [SerializeField] private int totalScore = 0;

    void SetSingleton()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }

        _instance = this;
    }

    private void Awake()
    {
        SetSingleton();
    }

    void Start()
    {
        player.PlayerScored.AddListener(AddScore);
        LoadSubLevel.Invoke(subLevel);
    }

    public void LevelListner()
    {
        _countToNextSubLevel++;
        if (_countToNextSubLevel == subLevel)
        {
            if(subLevel == 9) NextGameLevel();
            subLevel = subLevel < 9 ? subLevel + 1 : 1;
            _countToNextSubLevel = 0;
            LoadSubLevel.Invoke(subLevel);
        }
        
    }

    public void NextGameLevel()
    {
        if (gameLevel < gameLevelSettings.GameData.Count)
        {
            gameLevel += 1;
            LoadNextGameLevel.Invoke(gameLevel);
        } else
        {
            GameOver();
        }
    }

    public void AddScore(int score)
    {
        totalScore += score * gameLevel;
        scoreText.text = totalScore.ToString().PadLeft(16, '0');
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        gameOver.GetComponent<ScoreManager>().AddScore("Player", totalScore);
        Time.timeScale = 0f;

    }
}
