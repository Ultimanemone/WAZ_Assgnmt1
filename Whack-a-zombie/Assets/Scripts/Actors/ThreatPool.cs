using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace WAZ_Assgnmt1.Actors
{
    public static class ThreatPool
    {
        private static Queue<Threat> _threatPool;
        private static Threat _threatPrefab;
        private static Transform _threatRoot;
        private const int poolSize = 20;

        public static void Init(Threat threatPrefab, Transform threatRoot)
        {
            _threatPrefab = threatPrefab;
            _threatRoot = threatRoot;
            _threatPool = new Queue<Threat>();
            for (int i = 0; i < poolSize; i++)
            {
                Threat threat = InstantiateNew();
                threat.gameObject.SetActive(false);
                _threatPool.Enqueue(threat);
            }
        }

        public static bool TryGet(out Threat threat)
        {
            if (_threatPool.Count < 1)
            {
                threat = InstantiateNew();
                return true;
            }
            else
            {
                threat = _threatPool.Dequeue();
                return true;
            }
        }

        public static void Return(Threat threat)
        {
            threat.gameObject.SetActive(false);
            _threatPool.Enqueue(threat);
        }

        private static Threat InstantiateNew()
        {
            return UnityEngine.Object.Instantiate(_threatPrefab, _threatRoot);
        }
    }
}