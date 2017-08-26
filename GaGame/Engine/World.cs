using System.Drawing;
using System.Collections.Generic;
using System.Diagnostics;
using Tags;

namespace GaGame
{
    public class World
    {
        private List<GameObject> _children;
        private List<GameObject> _allObjects;

        private EventQueue _eventqueue;

        public World()
        {
            _children   = new List<GameObject>();
            _allObjects = new List<GameObject>();

            _eventqueue = ServiceLocator.Locate<EventQueue>();
            Debug.Assert(_eventqueue != null);
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

        public void ResolveCollisions()
        {
            List<BoxColliderComponent> colliders = new List<BoxColliderComponent>();

            foreach (GameObject obj in _allObjects)
            {
                if (!obj.Active)
                    continue;

                BoxColliderComponent box = obj.GetComponent<BoxColliderComponent>();

                if (box != null)
                    colliders.Add(box);
            }

            for (int i = 0; i < colliders.Count; i++)
                for (int j = i + 1; j < colliders.Count; j++)
                    if (colliders[i].Collides(colliders[j]))
                        _eventqueue.SendEvent(new Events.CollisionEvent(colliders[i].Parent, colliders[j].Parent));
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

        public GameObject FindObjectByTag(Tag tag)
        {
            return AllObjects().Find(x => x.Tag == tag);
        }
    }
}
