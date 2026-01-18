using UnityEngine;

namespace WAZ_Assgnmt1.Core
{
    public class BattleSceneInputManager : MonoBehaviour
    {
        private BattleSceneInputManager _instance;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
        }

        private void Update()
        {
            if (UnityEngine.InputSystem.Keyboard.current.escapeKey.wasPressedThisFrame && BattleSceneManager.instance.state != BattleSceneState.Hitlag)
            {
                if (BattleSceneManager.instance.state == BattleSceneState.Settings)
                {
                    BattleSceneManager.instance.ToggleSettings();
                }
                else
                {
                    BattleSceneManager.instance.TogglePauseMenu();
                }
            }
        }
    }
}