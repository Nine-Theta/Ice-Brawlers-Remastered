using System;
using System.Drawing;
using GXPEngine;

public class MyGame : Game //MyGame is a Game
{
	public int ScoreRed;
	public int ScoreBlue;
	public LevelLoader loader;
	Sprite background;

	public float mainScaleX;
	public float mainScaleY;
	//public Sprite extraPuckReflection;

	Random Rand = new Random();

	//initialize game here
	public MyGame() : base(1200, 900, false)
	{
		mainScaleX = game.width / 800.0f;
		mainScaleY = game.height / 600.0f;

		loader = new LevelLoader(mainScaleX, mainScaleY);
		AddChild(loader);

		background = new Sprite("background.png");
		AddChildAt(background,0);
		background.SetXY(0, game.height * 0.16f);
		background.scaleX = mainScaleX;
		background.scaleY = (game.height * 0.84f) / 600.0f;
	}

	public void Resetti()
	{
		background.scaleX = mainScaleX;
		background.scaleY = mainScaleY;

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
