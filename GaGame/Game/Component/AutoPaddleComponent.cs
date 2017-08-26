/*
 * User: Eelco
 * Date: 5/13/2017
 * Time: 6:09 PM
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using GaGame;
using System.Diagnostics;

public class AutoPaddleComponent : PaddleComponent
{
    private GameObject _ball = null;


    protected override void Start()
    {
        base.Start();
        _ball = ServiceLocator.Locate<World>().FindObjectByTag(Tags.Tag.Ball);
        Debug.Assert(_ball != null);
    }

    protected override void Update(float step)
    {
		_velocity.Y = 0; // no move 
		if      (_ball.Position.Y > Position.Y + 4) _velocity.Y = +_speed;
		else if (_ball.Position.Y < Position.Y - 4) _velocity.Y = -_speed;

        // move
        Position.Add(_velocity * step);
	}	
}

