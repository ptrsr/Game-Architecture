using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaGame
{
    public abstract class State
    {
        public virtual void Update() { }

        public virtual void Activate() { }
        public virtual void DeActivate() { }
    }
}
