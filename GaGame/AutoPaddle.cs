/*
 * User: Eelco
 * Date: 5/13/2017
 * Time: 6:09 PM
 */
using System;
using System.Drawing;
using System.Windows.Forms;

public class AutoPaddleComponent : PaddleComponent
{
    protected override void Update(float step)
    {
		_velocity.Y = 0; // no move 
		if (Ball.Position.Y+8 > Position.Y+32 + 8 ) _velocity.Y = +_Speed;
		if (Ball.Position.Y+8 < Position.Y+32 - 8 ) _velocity.Y = -_Speed;

        // move
        Position.Add(_velocity * step);
		
		// collisions & resolve
		if( Intersects(Ball.Position, Ball.Size ) ) {
			if(Ball.Velocity.X > 0 ) {
                Ball.Position.X = Position.X - Ball.Size.X;
			} else if(Ball.Velocity.X < 0 ) {
                Ball.Position.X = Position.X + _size.X;
			}
            Ball.Velocity.X = -Ball.Velocity.X;
			Ball.Velocity.Y = (Ball.Center.Y - Center.Y ) / 32 + ( (float)(Game.Random.NextDouble())-0.5f ) * 10.0f; // curve randomly
		
		}
		
		// collisions
		if(Position.Y < 0 ) Position.Y = 0;
		if( Position.Y > 416 ) Position.Y = 416;
	}	
}

