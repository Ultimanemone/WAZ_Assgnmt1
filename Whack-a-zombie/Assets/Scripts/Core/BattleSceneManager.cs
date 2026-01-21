using System.Collections;
using UnityEngine;
using WAZ_Assgnmt1.Actors;

namespace WAZ_Assgnmt1.Core
{
    public enum BattleSceneState
    {
        Playing,
        Paused,
        Hitlag,
        Settings
    }

    public class BattleSceneManager : MonoBehaviour
    {
        [SerializeField] private AnimationCurve spawnCurve;
        [SerializeField] private GameObject pausedMenu;
        [SerializeField] private GameObject settingsMenu;

        public static BattleSceneManager instance { get; private set; }
        public BattleSceneState state { get; private set; }
        public float timer { get; private set; }
        private const float timeLimit = 67f;
        private float score = 0f;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            timer = 0f;
            pausedMenu.SetActive(false);
            settingsMenu.SetActive(false);
            state = BattleSceneState.Playing;
        }

        private void FixedUpdate()
        {
            timer += Time.deltaTime;
        }

        public void TogglePauseMenu()
        {
            bool pauseState = GameManager.instance.isPaused;
            pausedMenu.SetActive(!pauseState);

            if (!pauseState) state = BattleSceneState.Paused;
            else state = BattleSceneState.Playing;

            GameManager.instance.SetPaused(!pauseState);
        }

        public void ToggleSettings()
        {
            if (state == BattleSceneState.Paused)
            {
                state = BattleSceneState.Settings;
                pausedMenu.SetActive(false);
                settingsMenu.SetActive(true);
            }
            else
            {
                state = BattleSceneState.Paused;
                pausedMenu.SetActive(true);
                settingsMenu.SetActive(false);
            }
        }

        public void QuitToTitle()
        {
            GameManager.instance.LoadScene("Title");
        }

        public void Hitlag(float duration)
        {
            Time.timeScale = 0f;
            StartCoroutine(HitlagCR(duration));
        }

        private IEnumerator HitlagCR(float duration)
        {
            yield return new WaitForSeconds(duration);
            Time.timeScale = 1f;
        }

        public float GetSpawnCD()
        {
            return spawnCurve.Evaluate(timer);
        }

        public void Kill(Threat unit)
        {
            score += 5;
            unit.Despawn();
        }
    }
}