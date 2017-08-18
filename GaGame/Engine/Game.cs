/*
 * Saxion, Game Architecture
 * User: Eelco Jannink
 * Date: 19-5-2016
 * Time: 16:55
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using GaGame;
using Tags;

public class Game
{
	[STAThread] // needed to use wpf Keyboard.isKeyPressed when single threaded !
	static
	public void Main() {
		Console.WriteLine( "Starting Game, close with Escape");
		Game game;
			game = new Game();
				game.Build();
				game.Run();
			game.Close();
		Console.WriteLine( "Closed window");

	}

	static private Random _random = new Random( 0 ); // seed for repeatability.
	private Window _window;
    private World  _world;
    private StateMachine _stateMachine;

	public Game()
	{
		_window = new Window( this );
	}	

	private void Build() 
	{
        // init world
        _world = new World();
        ServiceLocator.Provide(_world);

        // init statemachine
        _stateMachine = new StateMachine();
        ServiceLocator.Provide(_stateMachine);

        Input.Bind(Keys.Space, _stateMachine.Pauze);
        Input.Bind(Keys.P,     _stateMachine.Pauze);

        // ball
        GameObject ball = new GameObject("ball", Tag.Ball);
        ball.AddComponent<BallComponent>();
        Time.Timeout("Reset", 1.0f, ball.GetComponent<BallComponent>().Restart);

        // Left paddle
        GameObject leftPaddle = new GameObject("leftPaddle", Tag.Player1);
        leftPaddle.Position = new Vec2(10, 208);
        leftPaddle.AddComponent<PaddleComponent>();

        // right paddle
        GameObject rightPaddle = new GameObject("rightPaddle", Tag.Player2);
        rightPaddle.Position = new Vec2(622, 208);
        rightPaddle.AddComponent<AutoPaddleComponent>();

        // left score
        GameObject leftScore = new GameObject("leftScore", Tag.Score1);
        leftScore.Position = new Vec2(240, 10);
        leftScore.AddComponent<ScoreComponent>();

        // right score
        GameObject rightScore = new GameObject("rightScore", Tag.Score2);
        rightScore.Position = new Vec2(340, 10);
        rightScore.AddComponent<ScoreComponent>();

        //booster1 = new Booster( "Booster", 304, 96, "booster.png", ball );
        //booster2 = new Booster( "Booster", 304, 384, "booster.png", ball );
    }
	
	public void Run() {
		bool running = true;
		while( running ) { // gameloop
			Application.DoEvents(); // empty forms event queue

			// can close
			if( Input.Key.Enter(Keys.Escape))
				running = false;
			
			_window.Refresh(); // use refresh for a frame based update, async

		}
	}

    const int _fps = 120;

	public void Update( Graphics graphics )
	{
        _stateMachine.CurrentState.Update(graphics);

        int milisec = (int)((1.0f / _fps) * 1000);

        Thread.Sleep(milisec);
	}
	
    public void Close() {
		_window.Close();
	}
	
	static public Random Random {
		get {
			return _random;
		}
	}

}

