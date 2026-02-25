using Aimer_Assgnmt1.Actors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Aimer_Assgnmt1.Core
{
    public enum BattleSceneState
    {
        Playing,
        Paused,
        Results,
        Settings
    }

    public class BattleSceneManager : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _spawnCurve;
        [SerializeField] private GameObject _pausedMenu;
        [SerializeField] private GameObject _settingsMenu;
        [SerializeField] private GameObject _resultScreen;
        [SerializeField] private GameObject _stats;
        [SerializeField] private AudioClip shootSfx;
        [SerializeField] private List<AudioClip> hitSfx;
        [SerializeField] private AudioClip reloadSfx;
        [SerializeField] private GameObject cursor;
        [SerializeField] private Image cursorAim;
        [SerializeField] private Image cursorReload;

        public static BattleSceneManager instance { get; private set; }
        public BattleSceneState state { get; private set; }
        public float Time
        {
            get { return Mathf.Min(_timer - 7f, GameManager.instance.runDuration); }
        }
        private float TimeLimit
        {
            get
            {
                return GameManager.instance.runDuration;
            }
        }
        private float _timer;

        public int score { get; private set; }
        public int combo { get; private set; }
        public int hit { get; private set; }
        public int miss { get; private set; }
        public float Accuracy
        {
            get
            {
                if ((hit + miss) < 1) return 0;
                return (float)hit / (hit + miss);
            }
        }
        private const float _reloadTime = 0.864f;
        private float _reloadCounter = 0f;
        private bool _isReloaded;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            _pausedMenu.SetActive(false);
            _settingsMenu.SetActive(false);
            _resultScreen.SetActive(false);
        }

        private void Start()
        {
            GameManager.instance.PlayMusic(true);
            _timer = 0f;
            state = BattleSceneState.Playing;
            score = combo = 0;
            _isReloaded = true;
        }

        private void FixedUpdate()
        {
            _timer += UnityEngine.Time.deltaTime;
            _reloadCounter += UnityEngine.Time.deltaTime;

            if (_reloadCounter > _reloadTime && !_isReloaded)
            {
                GameManager.instance.PlaySFX(reloadSfx);
                _isReloaded = true;
            }

            if (Time >= TimeLimit)
            {
                Finish();
            }
        }

        private void Update()
        {
            if (state == BattleSceneState.Playing)
            {
                Cursor.visible = false;
                cursor.transform.localScale = Vector3.one * Screen.width / 1960f;
                cursor.SetActive(true);
                if (_isReloaded)
                {
                    cursorReload.gameObject.SetActive(false);
                    cursorAim.gameObject.SetActive(true);
                }
                if (!_isReloaded)
                {
                    cursorAim.gameObject.SetActive(false);
                    cursorReload.gameObject.SetActive(true);
                    float prog = _reloadCounter / _reloadTime;
                    cursorReload.fillAmount = prog;
                    cursorReload.color = new Color(prog, Mathf.Sin(prog * Mathf.PI), Mathf.Max(0, 1 - prog * 2));
                }
                cursor.transform.position = Mouse.current.position.ReadValue();
            }
            else
            {
                Cursor.visible = true;
                cursor.SetActive(false);
            }
        }

        public void TogglePauseMenu()
        {
            bool pauseState = GameManager.instance.isPaused;
            _pausedMenu.SetActive(!pauseState);

            if (!pauseState)
            {
                state = BattleSceneState.Paused;
                _stats.SetActive(false);
            }
            else
            {
                state = BattleSceneState.Playing;
                _stats.SetActive(true);
            }

            GameManager.instance.ToggleMusic();
            GameManager.instance.SetPaused(!pauseState);
        }

        public void ToggleSettings()
        {
            if (state == BattleSceneState.Paused)
            {
                state = BattleSceneState.Settings;
                _pausedMenu.SetActive(false);
                _settingsMenu.SetActive(true);
            }
            else
            {
                state = BattleSceneState.Paused;
                _pausedMenu.SetActive(true);
                _settingsMenu.SetActive(false);
            }
        }

        public void QuitToTitle()
        {
            GameManager.instance.LoadScene("Title");
        }

        public void Hitlag(float duration)
        {
            UnityEngine.Time.timeScale = 0f;
            StartCoroutine(HitlagCR(duration));
        }

        private IEnumerator HitlagCR(float duration)
        {
            yield return new WaitForSeconds(duration);
            UnityEngine.Time.timeScale = 1f;
        }

        public float GetSpawnCD()
        {
            float res = _spawnCurve.Evaluate(Time);
            return res;
        }

        public void Hit(Threat threat)
        {
            if (_isReloaded)
            {
                if (state == BattleSceneState.Playing)
                {
                    score += 5 + combo;
                    ++combo;
                    ++hit;
                    threat.Kill();
                    GameManager.instance.PlaySFX(hitSfx[Random.Range(0, hitSfx.Count - 1)]);
                }
                else
                {
                    GameManager.instance.PlaySFX(shootSfx, 0.7f);
                }
                // GameManager.instance.PlaySFX(shootSfx, 0.5f);
                _isReloaded = false;
                _reloadCounter = 0;
            }
        }

        public void Miss()
        {
            if (_isReloaded)
            {
                if (state == BattleSceneState.Playing)
                {
                    combo = 0;
                    ++miss;
                }
                GameManager.instance.PlaySFX(shootSfx);
                _isReloaded = false;
                _reloadCounter = 0;
            }
        }

        public void ResetCombo()
        {
            combo = 0;
        }

        public void Finish()
        {
            state = BattleSceneState.Results;
            ResultData.instance.UpdateFinalStats();
            _pausedMenu.SetActive(false);
            _settingsMenu.SetActive(false);
            _stats.SetActive(false);
            _resultScreen.SetActive(true);
        }
    }
}