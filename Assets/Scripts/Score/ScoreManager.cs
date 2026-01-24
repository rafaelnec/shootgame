using UnityEngine;
using System.IO;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    private HighScoreTable scoreTable;
    private string savePath;
    private HighScoreEntry scoreEntry;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            savePath = Path.Combine(Application.persistentDataPath, "savefile.json");
            LoadScores();
        }
    }

    public void AddScore(string playerName, int score)
    {
        scoreEntry = new HighScoreEntry(playerName, score);
        scoreTable.highScores.Add(scoreEntry);
        scoreTable.highScores.Sort((a, b) => b.score.CompareTo(a.score));
        SaveScores();
    }

    public HighScoreEntry GetFinalScore()
    {
        return scoreEntry;
    }

    public void SaveScores()
    {
        string json = JsonUtility.ToJson(scoreTable, true); 
        File.WriteAllText(savePath, json);
    }

    public void LoadScores()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            scoreTable = JsonUtility.FromJson<HighScoreTable>(json);
        }
        else
        {
            scoreTable = new HighScoreTable();
        }
    }

    public float GetHighScore()
    {
        if (scoreTable.highScores.Count > 0)
        {
            return scoreTable.highScores[0].score;
        }
        return 0f;
    }
}