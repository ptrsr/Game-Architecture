using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using Tags;

namespace GaGame
{
    public class GameObject
    {
        private string _name = null;
        private Tag _tag = Tag.Null;

        private Vec2   _pos  = null;

        private GameObject _parent = null;
        private World _world = null;

        private List<GameObject> _children  = null;
        private List<Component> _components = null;


        public delegate void Renderer(Graphics graphics, Vec2 pos);
        public Renderer OnRender = null;

        public GameObject(string name, Tag tag = Tag.Null)
        {
            _children = new List<GameObject>();
            _components = new List<Component>();

            _name = name;
            _tag  = tag;

            _pos  = new Vec2();

            _world = ServiceLocator.Locate<World>();
            _world.Add(this, true);
        }

        public void Update(float step)
        {
            for (int i = _components.Count - 1; i >= 0; i--)
                _components[i].Execute(step);

            foreach (GameObject child in _children)
                child.Update(step);
        }

        public void Render(Graphics graphics, Vec2 pos)
        {
            if (OnRender != null)
                OnRender(graphics, pos + _pos);

            foreach (GameObject child in _children)
                child.Render(graphics, pos + _pos);
        }

        public void Delete()
        {
            foreach (GameObject child in _children)
                child.Parent = Parent;

            Parent = null;
            _world.Delete(this);
        }

        #region Component management
        public T AddComponent<T>() where T : Component, new()
        {
            T component = new T();
            component.Parent = this;

            _components.Add(component);

            return component;
        }

        public void AddComponent(Component component)
        {
            component.Parent = this;
            _components.Add(component);
        }

        public Component RemoveComponent<T>(int index = 0) where T : Component
        {
            IEnumerable<T> list = _components.OfType<T>();

            if (list.Count() == 0 || index > list.Count() - 1)
                return null;

            T component  = list.ElementAt(index);
            component.Parent = null;

            _components.Remove(component);
            
            return component;
        }

        public T GetComponent<T>(int index = 0) where T : Component
        {
            IEnumerable<T> list = _components.OfType<T>();

            if (list.Count() == 0 || index > list.Count() - 1)
                return null;

            return _components.OfType<T>().ElementAt(index);
        }
        #endregion

        #region Get / Set

        public string Name
        {
            get { return _name;  }
            set { _name = value; }
        }

        public Tag Tag
        {
            get { return _tag;  }
            set { _tag = value; }
        }

        public Vec2 Position
        {
            get { return _pos;  }
            set { _pos = value; }
        }

        public GameObject Parent
        {
            get { return _parent; }
            set
            {
                if (_parent != null)
                    _parent.Children.Remove(this);

                if (value != null)
                    value.Children.Add(this);
                else
                    _world.Add(this);

                _parent = value;
            }
        }

        public List<GameObject> Children
        {
            get { return _children; }
        }

        #endregion
    }
}
