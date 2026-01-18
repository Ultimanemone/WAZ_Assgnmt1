using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using WAZ_Assgnmt1.Core;

namespace WAZ_Assgnmt1.Actors
{
    public class UnitSpawner : MonoBehaviour
    {
        [SerializeField] private List<Unit> unitPrefabs;
        [SerializeField] private Transform unitRoot;
        private RectTransform spawnArea;
        private float spawnTimer;
        private float spawnCooldown;

        private void Awake()
        {
            spawnArea = GetComponent<RectTransform>();
            spawnCooldown = 7.1f;
            spawnTimer = 0f;
            UnitPool.Init(unitPrefabs, unitRoot);
        }

        private void FixedUpdate()
        {
            if (spawnTimer > spawnCooldown)
            {
                Spawn(GetUnitByCredit(BattleSceneManager.instance.GetSpawnCredits() * Random.Range(0.85f, 1.15f)));
                spawnTimer = 0f;
                spawnCooldown = BattleSceneManager.instance.GetSpawnCD() * Random.Range(0.85f, 1.1f);
            }
            spawnTimer += Time.deltaTime;
        }

        public void Spawn(List<UnitType> units)
        {
            Rect rect = spawnArea.rect;

            List<UnitType> fliers = new List<UnitType>();
            foreach (UnitType unit in units)
            {
                if (unit.GetType() == typeof(Flier))
                {
                    fliers.Add(unit);
                    continue;
                }

                float x = Random.Range(rect.xMin, rect.xMax);
                float y = 0f;//Random.Range(rect.yMin, rect.yMax);

                Vector3 spawnPos = spawnArea.TransformPoint(x, y, 0f);

                UnitPool.TryGetUnit(unit, out Unit unitObj);
                unitObj.gameObject.SetActive(true);
                unitObj.transform.position = spawnPos;
            }

            if (fliers.Count > 0)
            {
                float x = Random.Range(rect.xMin, rect.xMax);
                float y = Random.Range(rect.yMin, rect.yMax);

                Vector3 spawnPos = spawnArea.TransformPoint(x, y, 0f);
                UnitPool.TryGetUnit(UnitType.Flier, out Unit unitObj1);
                unitObj1.gameObject.SetActive(true);
                unitObj1.transform.position = spawnPos;

                for (int i = 1; i < fliers.Count; i++)
                {
                    UnitPool.TryGetUnit(UnitType.Flier, out Unit unitObj2);
                    unitObj2.gameObject.SetActive(true);
                    // randomize location around the first one
                }
            }

            Debug.Log($"Spawned units: {string.Concat(units)}");
        }

        private List<UnitType> GetUnitByCredit(float credits)
        {
            List<UnitType> result = new();
            float remaining = credits;

            bool includeFlier = Random.value < 0.4f;

            // Find the Flier prefab
            Unit flierPrefab = unitPrefabs.First(u => u is Flier);

            if (includeFlier)
            {
                float required = 3 * flierPrefab.cost;

                if (required <= remaining)
                {
                    for (int i = 0; i < 3; i++)
                        result.Add(UnitType.Flier);

                    remaining -= required;
                }
            }

            while (true)
            {
                var affordable = unitPrefabs
                    .Where(u => u.cost <= remaining)
                    .ToList();

                if (affordable.Count == 0)
                    break;

                Unit chosen = affordable[Random.Range(0, affordable.Count)];
                result.Add(GetType(chosen));
                remaining -= chosen.cost;
            }

            return result;
        }

        private UnitType GetType(Unit unit)
        {
            if (unit.GetType() == typeof(Walker)) return UnitType.Walker;
            else if (unit.GetType() == typeof(Crawler)) return UnitType.Crawler;
            else if (unit.GetType() == typeof(Flier)) return UnitType.Flier;
            else return UnitType.Blocker;
        }
    }
}
