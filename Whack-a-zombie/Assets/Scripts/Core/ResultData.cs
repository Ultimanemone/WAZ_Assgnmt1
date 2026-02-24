using Aimer_Assgnmt1.Core;
using TMPro;
using UnityEngine;

namespace Aimer_Assgnmt1
{
    public class ResultData : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI score;
        [SerializeField] private TextMeshProUGUI combo;
        [SerializeField] private TextMeshProUGUI hit;
        [SerializeField] private TextMeshProUGUI miss;
        [SerializeField] private TextMeshProUGUI accuracy;
        private float comboMax;

        public static ResultData instance;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
        }

        private void Update()
        {
            comboMax = Mathf.Max(comboMax, BattleSceneManager.instance.combo);
        }

        public void UpdateFinalStats()
        {
            score.text = BattleSceneManager.instance.score.ToString();
            combo.text = comboMax.ToString();
            hit.text = BattleSceneManager.instance.hit.ToString();
            miss.text = BattleSceneManager.instance.miss.ToString();
            accuracy.text = $"{Mathf.Round(BattleSceneManager.instance.Accuracy * 10000f) / 100f}% Accuracy";
        }
    }
}
