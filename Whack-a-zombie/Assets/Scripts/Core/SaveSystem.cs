using System.Collections.Generic;
using UnityEngine;

namespace Aimer_Assgnmt1.Core
{
    [System.Serializable]
    public class ScoreData
    {
        public int score;
        public float time;
        public int hits;
        public float accuracy;

        public ScoreData(int score, float time, int hits, float accuracy)
        {
            this.score = score;
            this.time = time;
            this.hits = hits;
            this.accuracy = accuracy;
        }
    }

    [System.Serializable]
    public class ScoreListWrapper
    {
        public List<ScoreData> scores = new List<ScoreData>();
    }

    public class SaveSystem
    {
        public const string Key = "Highscores";
        private SaveSystem _instance;
        public SaveSystem Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SaveSystem();
                }
                return _instance;
            }
        }
        private SaveSystem() { }

        public static void SaveScores(List<ScoreData> scores)
        {
            ScoreListWrapper wrapper = new ScoreListWrapper();
            wrapper.scores = scores;

            string json = JsonUtility.ToJson(wrapper);
            PlayerPrefs.SetString("Scores", json);
            PlayerPrefs.Save();
        }

        public static List<ScoreData> LoadScores()
        {
            if (!PlayerPrefs.HasKey("Scores"))
                return new List<ScoreData>();

            string json = PlayerPrefs.GetString("Scores");
            ScoreListWrapper wrapper = JsonUtility.FromJson<ScoreListWrapper>(json);

            return wrapper.scores;
        }
    }
}