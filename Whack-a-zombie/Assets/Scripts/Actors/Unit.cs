using UnityEngine;

namespace WAZ_Assgnmt1.Actors
{
    public enum UnitType
    {
        None,
        Walker,
        Crawler,
        Flier,
        Blocker
    }

    public class Unit : MonoBehaviour
    {
        public virtual float cost { get; }
        public virtual int point { get; }
        public virtual int hp { get; protected set; }



        protected float timer = 0f;
        protected float killTime = 7f;
        public void KillTimer()
        {
            UnitType unitType = UnitType.None;
            if (GetType() == typeof(Walker)) unitType = UnitType.Walker;
            if (GetType() == typeof(Crawler)) unitType = UnitType.Crawler;
            if (GetType() == typeof(Flier)) unitType = UnitType.Flier;
            if (GetType() == typeof(Blocker)) unitType = UnitType.Blocker;
            if (unitType == UnitType.None) return;
            UnitPool.ReturnUnit(unitType, this);
        }

        private void FixedUpdate()
        {
            timer += Time.deltaTime;
            if (timer > killTime) KillTimer ();
        }
    }
}
