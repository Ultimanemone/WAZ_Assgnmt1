using UnityEngine;
using WAZ_Assgnmt1.Core;

namespace WAZ_Assgnmt1.Actors
{
    public class ThreatSpawner : MonoBehaviour
    {
        [SerializeField] private Threat threatPrefab;
        [SerializeField] private Transform unitRoot;
        private RectTransform spawnArea;
        private float spawnTimer;
        private float spawnCooldown;

        private void Awake()
        {
            spawnArea = GetComponent<RectTransform>();
            spawnCooldown = 7.1f;
            spawnTimer = 0f;
            ThreatPool.Init(threatPrefab, unitRoot);
        }

        private void FixedUpdate()
        {
            if (spawnTimer > spawnCooldown)
            {
                    spawnTimer = 0f;
                spawnCooldown = BattleSceneManager.instance.GetSpawnCD() * Random.Range(0.85f, 1.1f);
                Spawn();
            }
            spawnTimer += Time.deltaTime;
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

            Vector3 direction = spawnPos.normalized;
            Vector3 velocity = (2f - threat.transform.position.magnitude) * direction * Time.deltaTime;

            threat.velocity = velocity;
            threat.transform.position = spawnPos;
            threat.gameObject.SetActive(true);

            Debug.Log($"Threat deployed");
        }
    }
}
