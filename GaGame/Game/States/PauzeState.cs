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
        public override void Update(Graphics graphics)
        {
            Input.Resolve();

            FrameCounter.Update();

            _world.Render(graphics);
        }
    }
}
