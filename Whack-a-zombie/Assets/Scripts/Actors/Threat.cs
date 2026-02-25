using Aimer_Assgnmt1.Core;
using System.Collections;
using UnityEngine;

namespace Aimer_Assgnmt1.Actors
{
    public class Threat : MonoBehaviour
    {
        private float lifetime;

        public void Init(Vector3 pos, float lifetime)
        {
            gameObject.SetActive(true);
            transform.position = pos;
            this.lifetime = lifetime;
        }

        public void Kill()
        {
            GetComponent<Animator>().Play("Kill");
            StartCoroutine(ReturnCR());
        }

        private IEnumerator ReturnCR()
        {
            yield return new WaitForSeconds(0.5f);
            ThreatPool.Return(this);
            yield break;
        }

        private void FixedUpdate()
        {
            if (lifetime <= 0)
            {
                BattleSceneManager.instance.ResetCombo();
                Kill();
            }
            lifetime -= Time.deltaTime;
        }
    }
}
