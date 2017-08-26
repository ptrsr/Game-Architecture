using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;

namespace GaGame
{
    public class ImageComponent : Component
    {
        protected Image _image = null;

        public void SetImage(string filename)
        {
            _image = Image.FromFile(filename);
        }

        public override void OnRender(Graphics graphics, Vec2 pos)
        {
            Debug.Assert(_image != null);
            graphics.DrawImage(_image, pos.X - _image.Width / 2, pos.Y - _image.Height / 2);
        }


        public Vec2 Size
        {
            get { return new Vec2(_image.Width, _image.Height); }
        }
    }
}
