/*
 * Saxion, Game Architecture
 * User: Eelco Jannink
 * Date: 5/22/2016
 * Time: 1:27 PM
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;


static public class Time
{
	private static Stopwatch stopwatch = Stopwatch.StartNew(); // for timing 
	
	//private float lastTime = Time;
	private static float now = 0.0f;
	private static float step = 0.0f; // note: now it's a dynamic step size, you may want to have a fixxed timeStep !
	
	static SortedSet<TimeoutEvent> timeouts = new SortedSet<TimeoutEvent>();
	
	
	static public void Update() {
		float lastTime = now;
		now = (float)stopwatch.ElapsedTicks / Stopwatch.Frequency; // convert to float
		step = now - lastTime;

		// check for timeouts and deliver for all needed
		while( timeouts.Count > 0 && timeouts.Min.Raise() ) { // get all timeouts
			timeouts.Remove( timeouts.Min ); // take earliest timeout from sorted list
		}
		//Console.WriteLine( "Time "+Time.Step );
	}
	
	static public float Now { // in secs
		get { return now; } // consistent time in all the game;
	}

	static public float Real { // in secs
		get { return (float)stopwatch.ElapsedTicks / Stopwatch.Frequency; } // consistent time in all the game, convert to float;
	}

	static public float Step { // in secs
		get { return step; } // consistent timeStep for all the game
	}		
	
	static public void Timeout( string pName, float pInterval, EventHandler<TimeoutEvent> pHandler ) 
	{
		timeouts.Add( new TimeoutEvent( pName, pInterval, pHandler ) ); // 0.0f is fony
	}
	
	public class TimeoutEvent : IComparable 
	{
		private string name;
		private float interval;
		private float timeout;
		private EventHandler<TimeoutEvent> handler;
		
		public TimeoutEvent( string pName, float pInterval, EventHandler<TimeoutEvent> pHandler) 
		{
			name = pName;
			interval = pInterval;
			timeout = Time.Now + interval;
			handler = pHandler;
			//private void handler( Object sender,  Time.TimeoutEvent timeout ); // signature must match
		}
		
		public bool Raise()
		{
			if( timeout <= Time.Now ) {
				handler( this, this );
				return true;
			} 
			return false;
		}
		
		public int CompareTo( Object obj ) // should be improved for never being equal for Set
		{
			Debug.Assert( obj != null );
			TimeoutEvent other = obj as TimeoutEvent;
			return timeout.CompareTo( other.timeout );
		}
		
		override public string ToString()
		{
			return "TimeoutEvent "+name+" : timesout at "+timeout+" after interval "+interval;
		}
	}	
}

