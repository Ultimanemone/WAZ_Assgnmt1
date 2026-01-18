using UnityEngine;

namespace WAZ_Assgnmt1.Core
{
    public class TitleInputManager : MonoBehaviour
    {
        [SerializeField] private GameObject title;
        [SerializeField] private GameObject settings;
        private bool settingsState;

        private void Awake()
        {
            settings.SetActive(false);
            settingsState = false;
        }

        private void Update()
        {
            if (UnityEngine.InputSystem.Keyboard.current.escapeKey.wasPressedThisFrame && settingsState)
            {
                ToggleSettings();
            }
        }

        public void ToggleSettings()
        {
            settingsState = !settingsState;
            settings.SetActive(settingsState);
            title.SetActive(!settingsState);
        }

        public void LoadScene(string scene)
        {
            GameManager.instance.LoadScene(scene);
        }

        public void SetPaused(bool paused)
        {
            GameManager.instance.SetPaused(paused);
        }

        public void QuitGame()
        {
            GameManager.instance.QuitGame();
        }
    }
}