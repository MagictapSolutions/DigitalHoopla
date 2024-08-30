using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    public static Leaderboard leaderboard;

    private List<Score> scores = new List<Score>();

    public List<Text> name_Txt = new List<Text>();

    public List<Text> score_Txt = new List<Text>();

    public void AddScore(string name, int score)
    {
        scores.Add(new Score(name, score));
        SaveScores();
    }

    public void SaveScores()
    {


        string scoresFile = Path.Combine(Application.persistentDataPath, "scores.txt");
        string scoresJson = JsonUtility.ToJson(scores); 

            File.WriteAllText(scoresFile, JsonUtility.ToJson(scores));

        DisplayLeaderboard();
    }

    public void DisplayLeaderboard()
    {
        string scoresFile = Path.Combine(Application.persistentDataPath, "scores.txt");

        // Check if the scores file exists
        if (File.Exists(scoresFile))
        {
            // Read the scores from the file
            string scoresJson = File.ReadAllText(scoresFile);
            scores = JsonUtility.FromJson<List<Score>>(scoresJson);

            // Get the top 10 scorers.
            List<Score> topScorers = scores.OrderByDescending(s => s.score).Take(10).ToList();

            // Print the top 10 scorers.
            Debug.Log("Top 10 scorers:");
            for (int i = 0; i < topScorers.Count; i++)
            {
                Debug.Log(topScorers[i].name + ": " + topScorers[i].score);
            }
        }
        else
        {
            Debug.Log("No scores found.");
        }
    }






    public void SortScores()
    {
        scores.Sort((a, b) => b.score - a.score);
    }


    private class Score
    {
        public string name;
        public int score;

        public Score(string name, int score)
        {
            this.name = name;
            this.score = score;
        }
    }
}
