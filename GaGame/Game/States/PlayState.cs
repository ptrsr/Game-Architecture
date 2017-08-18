using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GaGame
{
    class PlayState : State
    {
        public override void Activate()
        {
            base.Activate();
            Time.Update();
        }

        public override void Update(Graphics graphics)
        {
            Input.Resolve();

            Time.Update();
            FrameCounter.Update();

            _world.Update(Time.Step);
            _world.Render(graphics);
        }
    }
}
