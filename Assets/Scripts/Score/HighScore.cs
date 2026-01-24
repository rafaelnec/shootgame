using System;
using System.Collections.Generic;

[Serializable]
public class HighScoreEntry
{
    public string playerName;
    public int score;

    public HighScoreEntry(string name, int points)
    {
        playerName = name;
        score = points;
    }
}

[Serializable]
public class HighScoreTable
{
    public List<HighScoreEntry> highScores = new List<HighScoreEntry>();
}