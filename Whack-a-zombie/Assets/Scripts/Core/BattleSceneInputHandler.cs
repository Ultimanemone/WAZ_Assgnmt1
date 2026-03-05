using Tanker_Assgnmt2.Actors;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tanker_Assgnmt2.Core
{
    public class BattleSceneInputHandler : MonoBehaviour
    {
        private static BattleSceneInputHandler _instance;


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
            if (Keyboard.current.escapeKey.wasPressedThisFrame)// && BattleSceneManager.instance.state != BattleSceneState.Hitlag)
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

            if (Mouse.current.leftButton.wasPressedThisFrame
                && (BattleSceneManager.instance.state == BattleSceneState.Playing
                || BattleSceneManager.instance.state == BattleSceneState.Results))
            {
                Vector2 worldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

                Collider2D[] hits = Physics2D.OverlapPointAll(worldPos);

                if (hits.Length > 0)
                {
                    foreach (Collider2D col in hits)
                    {
                        if (col.TryGetComponent(out Threat threat))
                        {
                            BattleSceneManager.instance.Hit(threat);
                        }
                    }
                }
                else
                {
                    BattleSceneManager.instance.Miss();
                }
            }
        }

        private void FixedUpdate()
        {
            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {

            }
        }
    }
}