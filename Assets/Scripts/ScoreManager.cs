using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int currentScore;
    public int highScore;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void UpdateScores(int scoreIn)
    {
        currentScore = scoreIn;
        if (currentScore > highScore)
        {
            highScore = currentScore;
        }
    }
}
