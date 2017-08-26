using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GaGame
{
    class PauzeState : State
    {
        public override void Activate()
        {
            base.Activate();
            Time.Update();
            Time.Pauze();
        }

        public override void Update(Graphics graphics)
        {
            Input.Resolve();
            _eventQueue.Resolve();
            _world.Render(graphics);
        }

        public override void DeActivate()
        {
            Time.Continue();
        }
    }
}
