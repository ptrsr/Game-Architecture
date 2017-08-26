using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace GaGame
{
    public class Events
    {

        public class CollisionEvent : GameObjectEvent
        {
            GameObject _one;
            GameObject _two;

            public CollisionEvent(GameObject one, GameObject two)
            {
                _one = one;
                _two = two;

                Debug.Assert(one != null && two != null);
            }

            public override void Resolve()
            {
                _one.OnCollision(_two);
                _two.OnCollision(_one);
            }
        }

        public delegate void Score(Tags.Tag _score);
        public static event Score score;

        public class ScoreEvent : GameObjectEvent
        {
            Tags.Tag _score;

            public ScoreEvent(Tags.Tag score)
            {
                _score = score;
            }

            public override void Resolve()
            {
                score(_score);
                reset(false);
            }
        }

        public delegate void Reset(bool hard);
        public static event Reset reset;

        public class ResetEvent : GameObjectEvent
        {


            public ResetEvent() { }

            public override void Resolve()
            {
                Time.Reset();
                reset(true);
            }
        }
    }
}
