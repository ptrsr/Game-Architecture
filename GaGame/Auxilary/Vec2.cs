/*
 * Saxion, Game Architecture
 * User: Eelco Jannink
 * Date: 5/22/2016
 * Time: 12:00 PM
 */
using System;
using System.Diagnostics;


public class Vec2
{
	private float x = 0;
	private float y = 0;
	
	public Vec2() : this( 0, 0 ) {}
	public Vec2( float x, float y )
	{
		this.x = x;
		this.y = y;
	}
	
	public float X {
		get { return x; }
		set { x = value; }
	}

	public float Y {
		get { return y; }
		set { y = value; }
	}
	
	public void Add( Vec2 other) 
	{
		Debug.Assert( other != null );
		
		this.x += other.x;
		this.y += other.y;
	}		
	
	public float Distance( Vec2 one, Vec2 other )
	{
		Debug.Assert( one != null );
		Debug.Assert( other != null );
		
		Vec2 difference = one - other;
		return (float) Math.Sqrt( difference.x*difference.x + difference.y*difference.y );
	}
	
	public float Distance2( Vec2 one, Vec2 other )
	{
		Debug.Assert( one != null );
		Debug.Assert( other != null );
		
		Vec2 difference = one - other;
		return difference.x*difference.x + difference.y*difference.y;
	}
	
	public float Dot( Vec2 one, Vec2 other )
	{
		Debug.Assert( one != null );
		Debug.Assert( other != null );
		
		return one.x*other.x + one.y*other.y;
	}
	
	public void Sub( Vec2 other) 
	{
		Debug.Assert( other != null );

		this.x -= other.x;
		this.y -= other.y;
	}		
	
	// operator overloading, be carefull about semantics
	static public Vec2 operator +( Vec2 one, Vec2 other) 
	{
		Debug.Assert( one != null );
		Debug.Assert( other != null );
		
		return new Vec2( one.x+other.x, one.y+other.y );
	}
	
	static public Vec2 operator -( Vec2 one, Vec2 other) 
	{
		Debug.Assert( one != null );
		Debug.Assert( other != null );
		
		return new Vec2( one.x-other.x, one.y-other.y );
	}

	static public Vec2 operator *( Vec2 one, float value )
	{
		Debug.Assert( one != null );

		return new Vec2( one.x*value, one.y*value );
	}
	static public Vec2 operator *( float value, Vec2 one )
	{
		Debug.Assert( one != null );

		return new Vec2( one.x*value, one.y*value );
	}
	static public Vec2 operator /( Vec2 one, float value )
	{
		Debug.Assert( one != null );

		return new Vec2( one.x/value, one.y/value );
	}
	static public Vec2 operator /( float value, Vec2 one )
	{
		Debug.Assert( one != null );

		return new Vec2( one.x/value, one.y/value );
	}
	
	override public string ToString() 
	{
		return "("+x+","+y+")";
	}

}

