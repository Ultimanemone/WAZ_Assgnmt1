using UnityEngine;

namespace WAZ_Assgnmt1.Actors
{
    public class Blocker : Unit
    {
        public override float cost { get { return 2.5f; } }
        public override int point { get { return 20; } }
        public override int hp
        {
            get
            {
                return 5;
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
