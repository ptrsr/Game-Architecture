using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tags;

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

        protected virtual void OnDetach() { }

        public GameObject Parent
        {
            get { return _parent; }
            set
            {
                if (_parent != null)
                    OnDetach();

                _started = false;
                _parent = value;
            }
        }

        public GameObject FindObjectByTag(Tag tag)
        {
            return ServiceLocator.Locate<World>().AllObjects().Find(x => x.Tag == tag);
        }

        public Vec2 Position
        {
            get { return Parent.Position;  }
            set { Parent.Position = value; }
        }
    }
}
