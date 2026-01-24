using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI playerScore;
    [SerializeField] private TextMeshProUGUI highScore;

    private ScoreManager _scoreManager;

    private void Awake()
    {
        _scoreManager = GetComponent<ScoreManager>();
    }

    void Start()
    {
        playerScore.text = _scoreManager.GetFinalScore().score.ToString().PadLeft(16, '0');
        highScore.text = _scoreManager.GetHighScore().ToString().PadLeft(16, '0');
    }

    public void PlayerAgainButtonOnClick()
    {
        this.gameObject.SetActive(false);
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        Time.timeScale = 1f;
    }

    public void MainMenuButtonOnClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }

    public void ExitButtonOnClick()
    {
        Application.Quit();
    }

}
