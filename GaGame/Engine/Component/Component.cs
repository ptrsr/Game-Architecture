using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tags;
using System.Drawing;

namespace GaGame
{
    abstract public class Component
    {
        private GameObject _parent;
        private bool _started = false;

        protected virtual void Start() { }
        protected virtual void Update(float step) { }

        public void Execute(float step)
        {
            if (!_started)
            {
                Start();
                _started = true;
            }

            Update(step);
        }

        public virtual void OnCollision(GameObject other) { }
        public virtual void OnRender(Graphics graphics, Vec2 pos) { }


        public GameObject Parent
        {
            get { return _parent; }
            set
            {
                _started = false;
                _parent = value;
            }
        }

        public Vec2 Position
        {
            get { return Parent.Position;  }
            set { Parent.Position = value; }
        }
    }
}
