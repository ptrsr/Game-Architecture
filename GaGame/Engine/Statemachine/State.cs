using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;

namespace GaGame
{
    public abstract class State
    {
        protected World      _world      = null;
        protected EventQueue _eventQueue = null;

        public virtual void Update(Graphics graphics) { }

        public virtual void Activate()
        {
            if (_world == null)
                _world = ServiceLocator.Locate<World>();

            if (_eventQueue == null)
                _eventQueue = ServiceLocator.Locate<EventQueue>();

            Debug.Assert(_world != null && _eventQueue != null);

        }

        public virtual void DeActivate() { }
    }
}
