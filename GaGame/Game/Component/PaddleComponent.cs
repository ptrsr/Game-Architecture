/*
 * User: Eelco
 * Date: 5/13/2017
 * Time: 2:01 PM
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using GaGame;

public class PaddleComponent : Component
{
	protected Vec2 _velocity = null;
	
	protected BallComponent _ball = null;

	public const float _Speed = 200.0f;
    protected Vec2 _size;
	
	public PaddleComponent()
	{
		_velocity = new Vec2( 0, 0 );
	}

    protected override void Start()
    {
        ImageComponent image = Parent.AddComponent<ImageComponent>();
        image.SetImage("paddle.png");
        _size = image.Size;

        _ball = FindObjectByTag(Tags.Tag.Ball).GetComponent<BallComponent>();

        Input.Bind(Keys.Up, MoveUp);
        Input.Bind(Keys.Down, MoveDown);
    }

    protected override void Update(float step)
    {
		// move
		Position.Add( _velocity * step );
		
		// collisions & resolve
		if( Intersects( _ball.Position, _ball.Size ) ) {
			if(_ball.Velocity.X > 0 ) {
                _ball.Position.X = Position.X - _ball.Size.X;
			} else if(_ball.Velocity.X < 0 ) {
                _ball.Position.X = Position.X + _size.X;
			}
            _ball.Velocity.X = -_ball.Velocity.X;
		}
		
		// collisions
		if(Position.Y < 0 ) Position.Y = 0;
		if(Position.Y > 416 ) Position.Y = 416;

        _velocity.Y = 0; // no move 
    }

    private void MoveUp()
    {
        _velocity.Y = -_Speed;
    }

    private void MoveDown()
    {
        _velocity.Y = _Speed;
    }

	public bool Intersects( Vec2 otherPosition, Vec2 otherSize ) {
		return
		    this.Position.X < otherPosition.X+otherSize.X && this.Position.X + _size.X > otherPosition.X &&
		    this.Position.Y < otherPosition.Y+otherSize.Y && this.Position.Y + _size.Y > otherPosition.Y;
	}
	
	public Vec2 Center {
		get {
			return Position + 0.5f * _size;
		}
	}	

	public BallComponent Ball
    {
        get { return _ball;  }
        set { _ball = value; }
    }

}


