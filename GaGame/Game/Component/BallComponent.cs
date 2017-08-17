using System;
using System.Drawing;
using System.Windows.Forms;
using GaGame;

namespace GaGame
{
    public class BallComponent : Component
    {
        private Vec2 _velocity = null;
        private bool _pausing = true;
        private Vec2 _size = null;

        private ScoreComponent _leftScore;
        private ScoreComponent _rightScore;

        public readonly Vec2 _speed = new Vec2( 400.0f, 400.0f);

        protected override void Start()
        {
            ImageComponent image = Parent.AddComponent<ImageComponent>();
            image.SetImage("ball.png");
            _size = image.Size;

            _leftScore  = FindObjectByTag(Tags.Tag.Score1).GetComponent<ScoreComponent>();
            _rightScore = FindObjectByTag(Tags.Tag.Score2).GetComponent<ScoreComponent>();

            _velocity = new Vec2(0, 0);
            Position = new Vec2( 312, 232 ); // center of form
            Reset();
        }

        protected override void Update(float step)
        {
            // input
            if (Input.Key.Enter(Keys.P))
            {
                _pausing = !_pausing; // toggle
                Console.WriteLine("Pausing " + _pausing);
            }

            // move
            if (!_pausing)
            {
                Position.Add(_velocity * step);
            }

            // collisions & resolve

            // Y bounds reflect
            if (Position.Y < 0)
            {
                Position.Y = 0;
                _velocity.Y = -_velocity.Y;
            }
            if (Position.Y > 480 - 16)
            { // note: non maintainable literals here, who did this
                Position.Y = 480 - 16;
                _velocity.Y = -_velocity.Y;
            }

            if (Position.X < 0)
            {
                _rightScore.IncScore();
                Reset();
            }
            if (Position.X > 640 - 16)
            { // note: bad literals detected
                _leftScore.IncScore();
                Reset();
            }
        }

        public bool Intersects(Vec2 otherPosition, Vec2 otherSize)
        {
            return
                Position.X < otherPosition.X + otherSize.X && Position.X + _size.X > otherPosition.X &&
                Position.Y < otherPosition.Y + otherSize.Y && Position.Y + _size.Y > otherPosition.Y;
        }

        public void Boost()
        {
            _velocity = _velocity * 2.0f;
        }

        public void DeBoost()
        {
            _velocity = _velocity / 2.0f;
        }


        public void Reset()
        {
            Position.X = 320 - 8;
            Position.Y = 240 - 8;
            //velocity.X = 0.5f;
            _velocity.X = _speed.X;
            _velocity.Y = (float)(Game.Random.NextDouble() - 0.5) * 2.0f * _speed.Y;
            _pausing = true;
            Time.Timeout("Reset", 1.0f, Restart);   // restart after 1 sec.
        }

        public Vec2 Center
        {
            get { return Position + 0.5f * _size; }
        }

        public Vec2 Velocity
        {
            get {  return _velocity; }
        }

        public Vec2 Size
        {
            get { return _size; }
        }

        public void Restart(Object sender, Time.TimeoutEvent timeout)
        {
            _pausing = false;
            Console.WriteLine("Restart");
        }
    }
}
