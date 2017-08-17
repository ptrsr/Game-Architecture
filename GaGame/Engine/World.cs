using System.Drawing;
using System.Collections.Generic;
using System.Diagnostics;

namespace GaGame
{
    class World
    {
        private List<GameObject> _children;
        private List<GameObject> _allObjects;

        public World()
        {
            _children   = new List<GameObject>();
            _allObjects = new List<GameObject>();
        }

        public void Add(GameObject obj, bool isNew = false)
        {
            Debug.Assert(obj.Parent == null && !_children.Contains(obj));
            _children.Add(obj);

            if (isNew)
                _allObjects.Add(obj);
        }

        public void Remove(GameObject obj)
        {
            Debug.Assert(obj.Parent == null && _children.Contains(obj));
            _children.Remove(obj);
        }

        public void Delete(GameObject obj)
        {
            _allObjects.Remove(obj);
        }

        public void Update(float step)
        {
            foreach (GameObject obj in _children)
                obj.Update(step);
        }

        public void Render(Graphics graphics)
        {
            foreach (GameObject obj in _children)
                obj.Render(graphics, new Vec2(0, 0));
        }

        public List<GameObject> AllObjects()
        {
            return new List<GameObject>(_allObjects);
        }
    }
}
