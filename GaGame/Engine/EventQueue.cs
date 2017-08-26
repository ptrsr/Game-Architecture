using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace GaGame
{
    public abstract class GameObjectEvent
    {
        public abstract void Resolve();
    }

    public class EventQueue
    {
        private GameObjectEvent[] _queue;

        private uint _head;
        private uint _tail;

        public EventQueue(uint length)
        {
            Debug.Assert(length > 0);
            _queue = new GameObjectEvent[length];
            _head = 0; _tail = 0;
        }

        public void SendEvent(GameObjectEvent newEvent)
        {
            _queue[_head] = newEvent;
            _head++;
            _head %= (uint)_queue.Length;
        }

        public void Resolve()
        {
            while(_head != _tail)
            {
                _queue[_tail].Resolve();
                _tail++;
                _tail %= (uint)_queue.Length;
            }
        }
    }
}
