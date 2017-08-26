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
    // settings
	protected Vec2 _velocity;
    protected Vec2 _size;
    protected float _speed;
	
    protected override void Start()
    {
        // apply settings
        _velocity = new Vec2(0, 0);
        _speed = ServiceLocator.Locate<Settings>().Paddle_Speed;

        ImageComponent image = Parent.AddComponent<ImageComponent>();
        image.SetImage("paddle.png");
        _size = image.Size;

        // add collider
        Parent.AddComponent<BoxColliderComponent>();

        // bind controls
        Input.Bind(Keys.Up, MoveUp);
        Input.Bind(Keys.Down, MoveDown);
    }

    protected override void Update(float step)
    {
		// move
		Position.Add( _velocity * step );
		
		// collisions
		if(Position.Y < 0 ) Position.Y = 0;
		if(Position.Y > 416 ) Position.Y = 416;

        _velocity.Y = 0; // no move 
    }

    private void MoveUp()
    {
        _velocity.Y = -_speed;
    }

    private void MoveDown()
    {
        _velocity.Y = _speed;
    }
}


