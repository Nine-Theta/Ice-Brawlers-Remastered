using System;
using System.Drawing;
using GXPEngine;

public class MyGame : Game //MyGame is a Game
{
	public int ScoreRed;
	public int ScoreBlue;

	float bob;
	Player player1;
	Player player2;
	Goal blueGoal;
	Goal redGoal;
	Puck puck;
	Puck extraPuck;
	Sprite background;
	public ScoreBoard blueBoard;
	public ScoreBoard redBoard;

	Random Rand = new Random();

	//initialize game here
	public MyGame () : base(1200, 900, false)
	{
		background = new Sprite("background.png");
		AddChild(background);
		background.scale = 1.5f;

		player1 = new Player(Key.LEFT, Key.RIGHT, Key.UP, Key.DOWN);
		AddChild(player1);
		player1.color = 0x4040FF;
		player1.SetXY(950, 450);

		player2 = new Player(Key.A, Key.D, Key.W, Key.S);
		AddChild(player2);
		player2.color = 0xFF4040;
		player2.SetXY(250, 450);

		puck = new Puck();
		AddChild(puck);
		puck.color = 0x505050;
		puck.SetXY(game.width / 2, game.height/2);

		blueGoal = new Goal("blue");
		AddChild(blueGoal);
		blueGoal.SetXY(1050, 450);
		blueGoal.scaleY = 4.0f;

		redGoal = new Goal("red");
		AddChild(redGoal);
		redGoal.SetXY(150, 450);
		redGoal.scaleY = 4.0f;

		blueBoard = new ScoreBoard();
		AddChild(blueBoard);
		blueBoard.color = 0x0000F0;
		blueBoard.SetXY(1100, 50);

		redBoard = new ScoreBoard();
		AddChild(redBoard);
		redBoard.color = 0xF00000;
		redBoard.SetXY(100, 50);
	}

	public void Resetti()
	{
		puck.Reset();
		player1.Reset();
		player1.SetXY(950, 450);
		player2.Reset();
		player2.SetXY(250, 450);
		blueGoal.Reset();
		blueGoal.SetXY(1050, 450);
		redGoal.Reset();
		redGoal.SetXY(150, 450);
		extraPuck.Destroy();
	}
	
	//update game here
	void Update ()
	{
		if (Input.GetKey(Key.ONE))
		{
			extraPuck = new Puck();
			AddChild(extraPuck);
			extraPuck.color = 0xE000FF;
			extraPuck.SetXY(game.width / 2, game.height/2);
		}
		if (Input.GetKeyDown(Key.TWO))
		{
			if (player2.InversedControls == false)
			{
				player2.InversedControls = true;
			}
			else
			{
				player2.InversedControls = false;
			}
		}


		if (Input.GetKeyDown(Key.R))
		{
			Resetti();
		}
		Console.WriteLine(bob);
	}
	
	//system starts here
	static void Main() 
	{
		new MyGame().Start();

	}
}
