using System;
using System.Drawing;
using GXPEngine;

public class MyGame : Game //MyGame is a Game
{
	StartMenu start;

	public int ScoreRed;
	public int ScoreBlue;
	public LevelLoader loader;
	public Sprite background;

	public bool scored = false;
	public int shakeCounter = 0;

	int timeGet;
	bool getTime1, getTime2;
	Random rand = new Random();
	float shakeAmount = 5.0f;
	int rumble1, rumble2, rumble3, rumble4, rumble5, rumble6;

	public float mainScaleX;
	public float mainScaleY;
	//public Sprite extraPuckReflection;

	Random Rand = new Random();

	//initialize game here
	public MyGame() : base(1200, 900, false)
	{
		start = new StartMenu();
		AddChild(start);

		mainScaleX = game.width / 800.0f;
		mainScaleY = game.height / 600.0f;
	}

	public void Resetti()
	{
		background.scaleX = mainScaleX / 2.0f;
		background.scaleY = mainScaleY / 2.0f;

		loader.LevelReset();
	}


	void ShakeItUp()
	{
		if (shakeCounter > 0 && getTime2 == false)
		{
			rumble1 = rand.Next(-2, 3);
			rumble2 = rand.Next(-2, 3);
			rumble3 = rand.Next(-2, 3);
			rumble4 = rand.Next(-2, 3);
			rumble5 = rand.Next(-2, 3);
			rumble6 = rand.Next(-2, 3);
			Console.WriteLine("{0} {1} {2} {3}", rumble1, rumble2, rumble3, rumble4);

			Console.WriteLine("CallCheck 1");
			if (shakeCounter > 0 && background != null)
			{
				background.x += shakeAmount * rumble1;
				background.y += shakeAmount * rumble2;
				loader.x += shakeAmount * rumble3;
				loader.y += shakeAmount* rumble4;
				loader.scoreBoard.x += shakeAmount * rumble5;
				loader.scoreBoard.y += shakeAmount * rumble6;
				timeGet = (Time.now / 10) + 3;
				getTime1 = true;
				getTime2 = true;
			}
		}

		if (timeGet < Time.now / 10 && getTime1 == true)
		{
			if (shakeCounter > 0 && background != null)
			{
				background.x -= shakeAmount * 2 * rumble1;
				background.y -= shakeAmount * 2 * rumble2;
				loader.x -= shakeAmount * 2 * rumble3;
				loader.y -= shakeAmount * 2 * rumble4;
				loader.scoreBoard.x -= shakeAmount * 2 * rumble5;
				loader.scoreBoard.y -= shakeAmount * 2 * rumble6;
				Console.WriteLine("CallCheck 3");
				timeGet = (Time.now / 10) + 3;
				getTime1 = false;
				//timeGet = (Time.now / 1000) + 1;
			}
		}

		if (timeGet < Time.now / 10 && getTime2 == true)
		{
			if (shakeCounter > 0 && background != null)
			{
				background.x += shakeAmount * rumble1;
				background.y += shakeAmount * rumble2;
				loader.x += shakeAmount * rumble3;
				loader.y += shakeAmount * rumble4;
				loader.scoreBoard.x += shakeAmount * rumble5;
				loader.scoreBoard.y += shakeAmount * rumble6;
				Console.WriteLine("CallCheck 3");
				getTime2 = false;
				scored = false;
				shakeCounter -= 1;
				//timeGet = (Time.now / 1000) + 1;
			}
		}
	}

	//update game here
	void Update()
	{
		ShakeItUp();


		//Console.WriteLine(timeGet);

		if (Input.GetKeyDown(Key.R))
		{
			Resetti();
		}
	}

	//system starts here
	static void Main()
	{
		new MyGame().Start();

	}
}
