using TMPro;
using UnityEngine;

namespace Aimer_Assgnmt1.Core
{
    public class BattleStats : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timer;
        [SerializeField] private TextMeshProUGUI score;
        [SerializeField] private TextMeshProUGUI combo;
        [SerializeField] private TextMeshProUGUI accuracy;

        private void Update()
        {
            timer.text = FormatTime(BattleSceneManager.instance.Time);
            score.text = $"{BattleSceneManager.instance.score.ToString()} Score";
            combo.text = $"{BattleSceneManager.instance.combo.ToString()} Combo";
            accuracy.text = $"{Mathf.Round(BattleSceneManager.instance.Accuracy * 10000f) / 100f}% Hit";
        }

        private string FormatTime(float timeInSeconds)
        {
            bool isNegative = timeInSeconds < 0f;
            timeInSeconds = Mathf.Abs(timeInSeconds);

            int minutes = Mathf.FloorToInt(timeInSeconds / 60f);
            int seconds = Mathf.FloorToInt(timeInSeconds % 60f);
            int milliseconds = Mathf.FloorToInt((timeInSeconds * 100f) % 100f);

            string formatted = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);

            return isNegative ? "-" + formatted : formatted;
        }
    }
}