using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    private static GameController _instance;

    [SerializeField]
    private Player player;
    public TextMeshProUGUI scoreText;
    [SerializeField]
    private int totalScore = 0;
    [SerializeField]
    private int gameLevel = 1;

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
    }

    public void NextLevel()
    {
        gameLevel += 1;
    }

    public void AddScore(int score)
    {
        totalScore += score * gameLevel;
        scoreText.text = totalScore.ToString().PadLeft(16, '0');
    }

    public void NewGameButtonOnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
}
