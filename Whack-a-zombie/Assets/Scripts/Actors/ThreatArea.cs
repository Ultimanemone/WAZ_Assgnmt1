using UnityEngine;

namespace WAZ_Assgnmt1.Actors
{
    public class ThreatArea : MonoBehaviour
    {
        void OnTriggerExit(Collider other)
        {
            other.GetComponent<Threat>()?.Despawn();
        }

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
