using UnityEngine;

namespace WAZ_Assgnmt1.Actors
{
    public class Crawler : Unit
    {
        public override float cost { get { return 1.5f; } }
        public override int point { get { return 10; } }
        public override int hp
        {
            get
            {
                return 3;
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
