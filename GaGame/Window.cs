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

	private Game game;
	
	public Window( Game aGame )
	{
		game = aGame;
		BackColor = System.Drawing.Color.Black;
		SuspendLayout();
			Name = "GaGame";
			ClientSize = new System.Drawing.Size( 640, 480 );
			DoubleBuffered = true; // avoid flickering
		ResumeLayout();	
		Show();
		Input.Key.Init( this );
	}
	
	override
	protected void OnPaint( PaintEventArgs e )  // adapter for caching repaints for updates
	{
		game.Update( e.Graphics );
	}
			
}
