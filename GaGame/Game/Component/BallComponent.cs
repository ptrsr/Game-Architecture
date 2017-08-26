using System;
using System.Drawing;
using System.Windows.Forms;
using GaGame;
using System.Diagnostics;

namespace GaGame
{
    public class BallComponent : Component
    {
        private Vec2 _velocity        = null;
        private Vec2 _size            = null;
        private Vec2 _playfieldSize   = null;

        public float _speed;
        public float _boostTime = 1.0f;

        protected override void Start()
        {
            // settings
            Settings settings = ServiceLocator.Locate<Settings>();
            Debug.Assert(settings != null);

            _playfieldSize = settings.Center * 2;
            _speed = settings.Ball_Speed;

            // set the image
            ImageComponent image = Parent.AddComponent<ImageComponent>();
            image.SetImage("ball.png");
            Debug.Assert(image.Size != new Vec2(0, 0));
            _size = image.Size;

            // add boxCollider
            Parent.AddComponent<BoxColliderComponent>();

            // setup ball in start position
            _velocity = new Vec2(0, 0);
            Reset(false);

            // setup event handling
            Events.reset += Reset;
        }

        protected override void Update(float step)
        {
            Position.Add(_velocity * step);

            // Y bounds reflect
            if (Position.Y < 0 + Size.Y / 2)
            {
                Position.Y = Size.Y / 2;
                _velocity.Y = -_velocity.Y;
            }
            if (Position.Y > _playfieldSize.Y - Size.Y / 2)
            { 
                Position.Y = _playfieldSize.Y - Size.Y / 2;
                _velocity.Y = -_velocity.Y;
            }

            if (Position.X < 0)
                ServiceLocator.Locate<EventQueue>().SendEvent(new Events.ScoreEvent(Tags.Tag.Score2));

            if (Position.X > _playfieldSize.X)
                ServiceLocator.Locate<EventQueue>().SendEvent(new Events.ScoreEvent(Tags.Tag.Score1));
        }

        public override void OnCollision(GameObject other)
        {
            if (other.Tag == Tags.Tag.Player1 || other.Tag == Tags.Tag.Player2)
            { // bounce
                int offset = (int)(other.GetComponent<BoxColliderComponent>().Size.X / 2) + (int)_size.X / 2;

                if (other.Tag == Tags.Tag.Player2)
                    offset = -offset;

                Position.X = other.Position.X + offset;

                Velocity.X = -Velocity.X;

                _velocity.Y = (float)(ServiceLocator.Locate<Random>().NextDouble() - 0.5) * 2 * ServiceLocator.Locate<Settings>().Bounce_Random;

                _velocity *= (1.0f / _velocity.Clone().Abs().Length()) * _velocity.Length();
            }

            if (other.Tag == Tags.Tag.Booster)
            { // boost
                Boost();
                Time.Timeout(other.Name, _boostTime, DeBoost);
            }
        }

        public void Reset(bool hard)
        {
            Position = _playfieldSize / 2;
            _velocity = new Vec2(0, 0);

            Time.Timeout("ball restart", 1, Go);
        }

        private void Go()
        {
            Random random = ServiceLocator.Locate<Random>();
            Vec2 newVelocity = new Vec2(((float)random.NextDouble() - 0.5f) * 2, ((float)random.NextDouble() - 0.5f) * 2);

            _velocity = (1.0f / newVelocity.Clone().Abs().Length()) * newVelocity * _speed;
        }

        public void Boost()
        {
            _velocity *= 2.0f;
        }

        public void DeBoost()
        {
            _velocity /= 2.0f;
        }

        public Vec2 Velocity
        {
            get {  return _velocity; }
        }

        public Vec2 Size
        {
            get { return _size; }
        }
    }
}
