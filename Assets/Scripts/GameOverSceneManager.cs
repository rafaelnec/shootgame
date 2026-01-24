using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameOverSceneManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _playerCurrentScore;
    [SerializeField] private TMP_Text _playerHighScore;

    private EndScoreManager _scoreManager;

    void Start()
    {
        _scoreManager = FindFirstObjectByType<EndScoreManager>();
        if (EndScoreManager.Instance != null)
        {
            Debug.Log("Update Scores");
            UpdateScores();
        }
    }

    private void UpdateScores()
    {
        _playerCurrentScore.SetText($"Your Score: {_scoreManager.GetCurrentScore().ToString()}");
        _playerHighScore.SetText($"Your Score: {_scoreManager.GetHighScore().ToString()}");
    }
}
