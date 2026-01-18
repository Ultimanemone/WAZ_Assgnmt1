using UnityEngine;

namespace WAZ_Assgnmt1.Actors
{
    public class Walker : Unit
    {
        public override float cost { get { return 1f; } }
        public override int point { get { return 5; } }
        public override int hp
        {
            get
            {
                return 2;
            }
            protected set
            {
                hp = value;
            }
        }

        private bool right;

        private void Awake()
        {
            right = GetComponent<RectTransform>().localPosition.x > 0;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
