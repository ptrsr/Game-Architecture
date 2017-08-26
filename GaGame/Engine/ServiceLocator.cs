using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaGame
{
    class ServiceLocator
    {
        private static readonly ServiceLocator _instance = new ServiceLocator();

        private Dictionary<Type, object> _objects;

        private ServiceLocator()
        {
            _objects = new Dictionary<Type, object>();
        }

        public static void Provide<T>(T item)
        {
            object temp;
            if (_instance._objects.TryGetValue(typeof(T), out temp))
                _instance._objects[typeof(T)] = item;
            else
                _instance._objects.Add(typeof(T), item);
        }

        public static T Locate<T>()
        {
            object item = null;
            _instance._objects.TryGetValue(typeof(T), out item);
            return (T)item;
        }
    }
}
