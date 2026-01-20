using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{

    [SerializeField]
    private Player player;
    public TextMeshProUGUI scoreText;
    [SerializeField]
    private int totalScore = 0;
    [SerializeField]
    private int gameLevel = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextLevel()
    {
        gameLevel += 1;
    }

    public void AddScore(int score)
    {
        Debug.Log("Adding Score: " + score);
        totalScore += score * gameLevel;
        scoreText.text = totalScore.ToString().PadLeft(16, '0');
    }
}
