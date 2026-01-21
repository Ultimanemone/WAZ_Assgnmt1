using UnityEngine;

namespace WAZ_Assgnmt1.Actors
{
    public class Threat : MonoBehaviour
    {
        public Vector3 velocity;

        public void Despawn()
        {
            ThreatPool.Return(this);
        }

        private void FixedUpdate()
        {
            transform.position += velocity;
        }
    }
}
