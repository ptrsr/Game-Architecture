/*
 * User: Eelco
 * Date: 5/13/2017
 * Time: 2:27 PM
 */
using System;
using System.Collections.Generic;
using System.Windows.Forms;

public class Window : Form
{

	private Game _game;
	
	public Window( Game game, int width, int height )
	{
		_game = game;
		BackColor = System.Drawing.Color.Black;
		SuspendLayout();
			Name = "GaGame";
			ClientSize = new System.Drawing.Size( width, height );
			DoubleBuffered = true; // avoid flickering
		ResumeLayout();	
		Show();
		Input.Key.Init( this );
	}
	
	override
	protected void OnPaint( PaintEventArgs e )  // adapter for caching repaints for updates
	{
		_game.Update( e.Graphics );
	}
			
}
