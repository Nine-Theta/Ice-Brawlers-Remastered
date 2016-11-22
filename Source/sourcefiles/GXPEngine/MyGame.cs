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


	//update game here
	void Update()
	{
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
