using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaGame
{
    class Settings
    {
        protected int fps = 60;                  public int FPS { get { return fps; } }

        protected int windowWidth  = 640;        public int Window_Width { get { return windowWidth; } }
        protected int windowHeight = 480;        public int Window_Height { get { return windowHeight; } }

        public Vec2 Center { get { return new Vec2(windowWidth, windowHeight) / 2; } }

        // paddle
        protected int paddleDistance = 16;       public int Paddle_Distance { get { return paddleDistance; } }
        protected float paddleSpeed  = 200;      public float Paddle_Speed { get { return paddleSpeed; } }

        // ball
        protected float ballSpeed    = 400;      public float Ball_Speed { get { return ballSpeed; } }
        protected float bounceRandom = 300;      public float Bounce_Random { get { return bounceRandom; } }

        // boost
        protected float boostRespawn = 2;         public float Boost_Respawn { get { return boostRespawn; } }
    }
}
