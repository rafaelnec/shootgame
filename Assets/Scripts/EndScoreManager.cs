using UnityEngine;

public class EndScoreManager : MonoBehaviour
{
    public static EndScoreManager Instance;

    private int currentScore;
    private int highScore;

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

    public int GetCurrentScore()
    {
        return currentScore;
    }
    
    public int GetHighScore()
    {
        return highScore;
    }
}
