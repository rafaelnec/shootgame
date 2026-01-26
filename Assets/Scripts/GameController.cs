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
    [SerializeField] private AudioClip gameOverSound;
    [SerializeField] private GameObject gameWin;
    [SerializeField] private AudioClip gameWinSound;
    
    public UnityEvent<int> LoadSubLevel;
    public UnityEvent LoadBigBossLevel;
    public UnityEvent<int> LoadNextGameLevel;

    [Header("Players")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Player player;
    [SerializeField] private int totalScore = 0;
    [SerializeField] private GameObject playerGameOverPrefab;

    [Header("Pause")]
    [SerializeField] private GameObject pausePanel;
    private bool _isPaused = false;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausePanel.SetActive(!_isPaused);
            _isPaused = !_isPaused;
        }
    }

    void Start()
    {
        player.PlayerScored.AddListener(AddScore);
        Debug.Log("Game Level: " + gameLevel);
        LoadNextGameLevel.Invoke(gameLevel);
        if (gameLevel < gameLevelSettings.GameData.Count) LoadSubLevel.Invoke(subLevel);
        else LoadBigBossLevel.Invoke();
    }

    public void LevelListner()
    {
        _countToNextSubLevel++;
        if (_countToNextSubLevel == subLevel)
        {
            if(subLevel == 9) NextGameLevel();
            if (gameLevel < gameLevelSettings.GameData.Count) {
                subLevel = subLevel < 9 ? subLevel + 1 : 1;
                _countToNextSubLevel = 0;
                LoadSubLevel.Invoke(subLevel);
            } else {
                LoadBigBossLevel.Invoke();
            }
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
        scoreText.text = totalScore.ToString().PadLeft(16, ' ');
    }

    public int GetScore()
    {
        return totalScore;
    }

    public void GameOver()
    {
        Instantiate(playerGameOverPrefab, player.transform.position, Quaternion.identity);
        
        if (gameOverSound != null)
        {
            AudioSource.PlayClipAtPoint(gameOverSound, Camera.main.transform.position);
        }
        player.gameObject.SetActive(false);
        Invoke("CallGameOverWithDelay", 0.5f);      

    }

    public void CallGameOverWithDelay()
    {
        Time.timeScale = 0f;
        gameOver.SetActive(true);
        gameOver.GetComponent<ScoreManager>().AddScore("Player", totalScore);
        Time.timeScale = 0f;
    }

    public void GameWin()
    {
        if (gameWinSound != null)
        {
            AudioSource.PlayClipAtPoint(gameWinSound, Camera.main.transform.position);
        }
        player.gameObject.SetActive(false);
        Invoke("CallGameWinWithDelay", 0.5f);      

    }

    public void CallGameWinWithDelay()
    {
        gameWin.SetActive(true);
        gameWin.GetComponent<ScoreManager>().AddScore("Player", totalScore);
    }

    
    
}
