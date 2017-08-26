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
using System.Diagnostics;

public class Game
{
	[STAThread] // needed to use wpf Keyboard.isKeyPressed when single threaded !
	static
	public void Main()
    {
		Console.WriteLine( "Starting Game, close with Escape");

        Settings settings = new Settings();
        ServiceLocator.Provide(settings);

        Game game;
			game = new Game(settings.Window_Width, settings.Window_Height);
				game.Build(settings);
				game.Run();
			game.Close();
		Console.WriteLine( "Closed window");

	}

    private Settings     _settings;

    private Window       _window;
    private World        _world;
    private StateMachine _stateMachine;
    private EventQueue   _eventQueue;

	public Game(int width, int height)
	{
		_window = new Window( this, width, height );
	}	

	private void Build(Settings settings) 
	{
        _settings = settings;
        _fps      = settings.FPS;

        Debug.Assert(_fps > 0);

        // init eventqueue
        _eventQueue = new EventQueue(10);
        ServiceLocator.Provide(_eventQueue);

        // init world
        _world = new World();
        ServiceLocator.Provide(_world);

        // init statemachine
        _stateMachine = new StateMachine();
        ServiceLocator.Provide(_stateMachine);

        // init random
        ServiceLocator.Provide(new Random());

        // bind keys to state machine
        Input.Bind(Keys.Space, _stateMachine.Pauze, Input.KeyAttack.Down);
        Input.Bind(Keys.P,     _stateMachine.Pauze, Input.KeyAttack.Down);
        Input.Bind(Keys.R,     Reset,               Input.KeyAttack.Down);

        // ball
        GameObject ball = new GameObject("ball", Tag.Ball);
        ball.AddComponent<BallComponent>();

        // Left paddle
        GameObject leftPaddle = new GameObject("leftPaddle", Tag.Player1);
        leftPaddle.Position = new Vec2(settings.Paddle_Distance, settings.Center.Y);
        leftPaddle.AddComponent<PaddleComponent>();

        // right paddle
        GameObject rightPaddle = new GameObject("rightPaddle", Tag.Player2);
        rightPaddle.Position = new Vec2(settings.Window_Width - settings.Paddle_Distance, settings.Center.Y);
        rightPaddle.AddComponent<AutoPaddleComponent>();

        // left score
        GameObject leftScore = new GameObject("leftScore", Tag.Score1);
        leftScore.Position = new Vec2(settings.Window_Width * 0.33f, settings.Window_Height * 0.1f);
        leftScore.AddComponent<ScoreComponent>();

        // right score
        GameObject rightScore = new GameObject("rightScore", Tag.Score2);
        rightScore.Position = new Vec2(settings.Window_Width * 0.66f, settings.Window_Height * 0.1f);
        rightScore.AddComponent<ScoreComponent>();

        // booster up
        GameObject upBooster = new GameObject("upBooster", Tag.Booster);
        upBooster.Position = new Vec2(settings.Center.X, settings.Window_Height * 0.25f);
        upBooster.AddComponent<BoostComponent>();

        // booster up
        GameObject downBooster = new GameObject("downBooster", Tag.Booster);
        downBooster.Position = new Vec2(settings.Center.X, settings.Window_Height * 0.75f);
        downBooster.AddComponent<BoostComponent>();

        // call start function of all components 
        _world.Update(0);
    }
	
	private void Run() {
		bool running = true;
		while( running ) { // gameloop
			Application.DoEvents(); // empty forms event queue

			// can close
			if( Input.Key.Enter(Keys.Escape))
				running = false;
			
			_window.Refresh(); // use refresh for a frame based update, async

		}
	}

    private void Reset()
    {
        Debug.Assert(_eventQueue != null);
        _eventQueue.SendEvent(new Events.ResetEvent());
    }

    int _fps = 60;

	public void Update( Graphics graphics )
	{
        Debug.Assert(_stateMachine != null);
        _stateMachine.CurrentState.Update(graphics);

        int milisec = (int)((1.0f / _fps) * 1000);
        Thread.Sleep(milisec);
	}
	
    private void Close() {
		_window.Close();
	}
}

