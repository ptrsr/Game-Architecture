/*
 * Created by SharpDevelop.
 * User: Eelco Jannink
 * Date: 16-5-2017
 * Time: 18:34
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;


static public class FrameCounter
{

	static int _count = 0;
    static int _last = 0;

	static public void Update() 
	{
        _count++;

        int current = (int)Math.Floor(Time.Real);
        if (current > _last)
        {
            Console.WriteLine(_count);
            _last = current;
            _count = 0;
        }
	}
}

