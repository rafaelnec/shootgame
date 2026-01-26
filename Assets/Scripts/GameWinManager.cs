using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameWinManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerScore;
    [SerializeField] private TextMeshProUGUI highScore;

    [SerializeField] private float creditSceneDelay;

    private ScoreManager _scoreManager;

    private void Awake()
    {
        _scoreManager = GetComponent<ScoreManager>();
        Invoke("LoadNextScene", creditSceneDelay);
    }

    void Start()
    {
        playerScore.text = _scoreManager.GetFinalScore().score.ToString().PadLeft(12, '0');
        highScore.text = _scoreManager.GetHighScore().ToString().PadLeft(12, '0');
    }

    private void LoadNextScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Credits");
    }

}
