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
    public static Vec2 zero { get { return new Vec2(0, 0); } }

    public float x = 0;
    public float y = 0;

    public int iX { get { return (int)Math.Floor(x); } }
    public int iY { get { return (int)Math.Floor(y); } }

    public float X
    {
        get { return x;  }
        set { x = value; }
    }

    public float Y
    {
        get { return y; }
        set { y = value; }
    }


    public Vec2(float pX = 0, float pY = 0)
    {
        x = pX;
        y = pY;
    }

    public Vec2 Add(Vec2 other)
    {
        x += other.x;
        y += other.y;
        return this;
    }

    public Vec2 Sub(Vec2 other)
    {
        x -= other.x;
        y -= other.y;
        return this;
    }

    public Vec2 Scale(float scalar)
    {
        x *= scalar;
        y *= scalar;
        return this;
    }

    public float Length()
    {
        return (float)Math.Sqrt(x * x + y * y);
    }

    public Vec2 Normalize()
    {
        if (x == 0 && y == 0)
        {
            return this;
        }
        else
        {
            return Scale(1 / Length());
        }
    }

    public Vec2 Clone()
    {
        return new Vec2(x, y);
    }

    public Vec2 Abs()
    {
        x = Math.Abs(x);
        y = Math.Abs(y);

        return this;
    }

    public float GetAngleDegrees()
    {
        return (float)(GetAngleRadians() * 180 / Math.PI);
    }

    public float GetAngleRadians()
    {
        return (float)Math.Atan2(y, x);
    }

    public float DistanceTo(Vec2 other)
    {
        return (float)Math.Sqrt((other.x - x) * (other.x - x) + (other.y - y) * (other.y - y));
    }

    public bool Equals(Vec2 other)
    {
        return other != null && x == other.x && y == other.y;
    }

    public override string ToString()
    {
        return String.Format("({0}, {1})", x, y);
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
	
}

