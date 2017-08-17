/*
 * User: Eelco
 * Date: 5/13/2017
 * Time: 6:09 PM
 */
using System;
using System.Drawing;
using System.Windows.Forms;

public class CurvedPaddle : PaddleComponent
{	
	
	public CurvedPaddle( string pName, float pX, float pY, string pImageFile )
	{
	}

    protected override void Update(float step)
    {
		//// input
		
		//_velocity.Y = 0; // no move 
		//if ( Input.Key.Pressed( Keys.Up ) ) _velocity.Y = -_Speed;
		//if ( Input.Key.Pressed( Keys.Down ) ) _velocity.Y = _Speed;
		
		//// move
		//position.Add( _velocity );
		
		//// collisions & resolve
		//if( Intersects( ball.Position, ball.Size ) ) {
		//	if( ball.Velocity.X > 0 ) {
		//		ball.Position.X = position.X - ball.Size.X;
		//	} else if( ball.Velocity.X < 0 ) {
		//		ball.Position.X = position.X + Size.X;
		//	}
		//	ball.Velocity.X = -ball.Velocity.X;
		//	ball.Velocity.Y = ( ball.Center.Y - Center.Y ) / 64 + ( (float)(Game.Random.NextDouble())-0.5f )/1.0f;
		//}
		
		//// collisions
		//if( position.Y < 0 ) position.Y = 0;
		//if( position.Y > 416 ) position.Y = 416;
		
		//// render
		//graphics.DrawImage( image, position.X, position.Y );
	}	
}