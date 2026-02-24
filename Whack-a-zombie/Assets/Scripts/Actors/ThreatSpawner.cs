using Aimer_Assgnmt1.Core;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace Aimer_Assgnmt1.Actors
{
    public class ThreatSpawner : MonoBehaviour
    {
        [SerializeField] private Threat threatPrefab;
        [SerializeField] private Transform unitRoot;
        private RectTransform spawnArea;
        private float timer = 0f;
        private float interval = 14.4f; // 14.4 ticks per beat
        private float intervalTime;
        private float cooldown;

        private void Awake()
        {
            spawnArea = GetComponent<RectTransform>();
            ThreatPool.Init(threatPrefab, unitRoot);
        }

        private void Start()
        {
            intervalTime = interval * Time.fixedDeltaTime;
            cooldown = 6.25f;
        }

        private void FixedUpdate()
        {
            timer += Time.fixedDeltaTime;

            if (timer >= cooldown && BattleSceneManager.instance.state == BattleSceneState.Playing)
            {
                timer -= cooldown;
                float temp = BattleSceneManager.instance.GetSpawnCD();
                cooldown = Mathf.Round(temp / intervalTime) * intervalTime;
                Spawn();
            }
        }

        public void Spawn()
        {
            Rect rect = spawnArea.rect;

            float x;
            if (Random.value >= 0.5) x = Random.Range(rect.xMin, -1f);
            else x = Random.Range(1f, rect.xMax);
            float y = Random.Range(rect.yMin, rect.yMax);

            Vector3 spawnPos = spawnArea.TransformPoint(x, y, 0f);

            ThreatPool.TryGet(out Threat threat);

            threat.Init(spawnPos, 3f);

            Debug.Log($"Threat deployed");
        }
    }
}
