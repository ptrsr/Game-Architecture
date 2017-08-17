/*
 * User: Eelco
 * Date: 5/13/2017
 * Time: 2:01 PM
 */
using System;
using System.Drawing;
using System.Windows.Forms;


public class Booster
{
	private string name;
	private Image image;

	private Vec2 position = null; // making clear no default value, needs constructor action.
	private bool active = true;
	
	public Booster( string pName, float pX, float pY, string pImageFile )
	{
		name = pName;
		image = Image.FromFile( pImageFile );
		position = new Vec2( pX, pY );
		
	}
	
	public void Update( Graphics graphics )
	{
		// input

		// move
		
		// collisions & resolve
		//Console.WriteLine( active );
		//if( active && Intersects( ball.Position, ball.Size ) ) {
		//	active = false;
		//	ball.Boost();
		//	Time.Timeout( "Deboosting", 0.5f, DeBoost );
		//}

		// Render
		graphics.DrawImage( image, position.X, position.Y );
	}	
	
	
	// Event handlers

	private void DeBoost( Object sender,  Time.TimeoutEvent timeout )
	{
		//ball.DeBoost();
		active = true;
		//Console.WriteLine( "Deboosting "+name );
	}	

	// Tools
	public bool Intersects( Vec2 otherPosition, Vec2 otherSize ) {
		return
		    this.position.X < otherPosition.X+otherSize.X && this.position.X + this.Size.X > otherPosition.X &&
		    this.position.Y < otherPosition.Y+otherSize.Y && this.position.Y + this.Size.Y > otherPosition.Y;
	}
	
	
	public void Reset() 
	{
		position.X = 320-8;
		position.Y = 240-8;
	}
	
	public Vec2 Center {
		get {
			return position + 0.5f * Size;
		}
	}	
	public Vec2 Position {
		get { 
			return position;
		}
	}
	public Vec2 Size {
		get { 
			return new Vec2( image.Width, image.Height ); 
		}
	}
}

