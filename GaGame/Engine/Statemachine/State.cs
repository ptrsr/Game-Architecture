using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace GaGame
{
    public abstract class State
    {
        protected World _world = null;

        public virtual void Update(Graphics graphics) { }

        public virtual void Activate()
        {
            if (_world == null)
                _world = ServiceLocator.Locate<World>();

        }

        public virtual void DeActivate() { }
    }
}
