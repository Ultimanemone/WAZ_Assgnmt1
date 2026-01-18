using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WAZ_Assgnmt1.Actors
{
    public static class UnitPool
    {
        private static Queue<Walker> _walkerPool;
        private static Queue<Crawler> _crawlerPool;
        private static Queue<Flier> _flierPool;
        private static Queue<Blocker> _blockerPool;
        private static Transform _unitRoot;
        private const int poolSize = 20;

        public static void Init(List<Unit> unitPrefabs, Transform unitRoot)
        {
            _unitRoot = unitRoot;
            foreach (var unitPrefab in unitPrefabs)
            {
                if (unitPrefab.GetType() == typeof(Walker)) MakePool((Walker)unitPrefab, out _walkerPool);
                if (unitPrefab.GetType() == typeof(Crawler)) MakePool((Crawler)unitPrefab, out _crawlerPool);
                if (unitPrefab.GetType() == typeof(Flier)) MakePool((Flier)unitPrefab, out _flierPool);
                if (unitPrefab.GetType() == typeof(Blocker)) MakePool((Blocker)unitPrefab, out _blockerPool);
            }
        }

        public static bool TryGetUnit(UnitType unitType, out Unit unit)
        {
            if (unitType == UnitType.Walker && _walkerPool.TryDequeue(out var walker))
            {
                unit = walker;
                return true;
            }

            if (unitType == UnitType.Crawler && _crawlerPool.TryDequeue(out var crawler))
            {
                unit = crawler;
                return true;
            }

            if (unitType == UnitType.Flier && _flierPool.TryDequeue(out var flier))
            {
                unit = flier;
                return true;
            }

            if (unitType == UnitType.Blocker && _blockerPool.TryDequeue(out var blocker))
            {
                unit = blocker;
                return true;
            }

            unit = null;
            return false;
        }

        public static void ReturnUnit(UnitType unitType, Unit unit)
        {
            if (unitType == UnitType.Walker) _walkerPool.Enqueue((Walker)unit);
            if (unitType == UnitType.Crawler) _crawlerPool.Enqueue((Crawler)unit);
            if (unitType == UnitType.Flier) _flierPool.Enqueue((Flier)unit);
            if (unitType == UnitType.Blocker) _blockerPool.Enqueue((Blocker)unit);
            unit.gameObject.SetActive(false);
        }

        private static void MakePool<T>(T unit, out Queue<T> pool) where T : Unit
        {
            pool = new Queue<T>();
            for (int i = 0; i < poolSize; i++)
            {
                T obj = UnityEngine.Object.Instantiate(unit, _unitRoot);
                obj.gameObject.SetActive(false);
                pool.Enqueue(obj);
            }
        }
    }
}