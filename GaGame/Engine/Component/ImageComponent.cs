using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GaGame
{
    public class ImageComponent : Component
    {
        protected Image _image = null;

        protected override void Start()
        {
            Parent.OnRender += Render;
        }

        public void SetImage(string filename)
        {
            _image = Image.FromFile(filename);
        }

        public virtual void Render(Graphics graphics, Vec2 pos)
        {
            graphics.DrawImage(_image, pos.X, pos.Y);
        }


        public Vec2 Size
        {
            get { return new Vec2(_image.Width, _image.Height); }
        }
    }
}
