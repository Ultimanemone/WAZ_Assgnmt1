using UnityEngine;

namespace WAZ_Assgnmt1.Actors
{
    public class Flier : Unit
    {
        public override float cost { get { return 0.2f; } }
        public override int point { get { return 1; } }
        public override int hp
        {
            get
            {
                return 1;
            }
            protected set
            {
                hp = value;
            }
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
