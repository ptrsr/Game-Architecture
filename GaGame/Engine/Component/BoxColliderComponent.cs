using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;

namespace GaGame
{
    class BoxColliderComponent : Component
    {
        private Rectangle   _rectangle;
        private Vec2        _size;

        protected override void Start()
        {
            ImageComponent imageComponent = Parent.GetComponent<ImageComponent>();
            _rectangle = new Rectangle();

            if (imageComponent != null)
                _size = imageComponent.Size;

            Size = _size;
        }

        public bool Collides(BoxColliderComponent other)
        {
            Debug.Assert(other != null && Box != null);
            return other.Box.IntersectsWith(Box);
        }

        public Rectangle Box
        {
            get
            {
                _rectangle.X = (int)Position.X - ((int)Size.X / 2);
                _rectangle.Y = (int)Position.Y - ((int)Size.Y / 2);

                return _rectangle;
            }
        }

        public Vec2 Size
        {
            get { return _size;  }
            set
            {
                _size = value;
                _rectangle.Width  = (int)_size.X;
                _rectangle.Height = (int)_size.Y;
            }
        }
    }
}
