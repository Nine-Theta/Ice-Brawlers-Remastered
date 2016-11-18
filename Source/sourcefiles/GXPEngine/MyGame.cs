using System;
using System.Drawing;
using GXPEngine;

public class MyGame : Game //MyGame is a Game
{
	public int ScoreRed;
	public int ScoreBlue;

	Player player1;
	Player player2;
	Goal blueGoal;
	Goal redGoal;
	Puck puck;
	Puck extraPuck;
	Sprite background;
	public ScoreBoard blueBoard;
	public ScoreBoard redBoard;
	public Sprite puckReflection;
	//public Sprite extraPuckReflection;

	Random Rand = new Random();

	//initialize game here
	public MyGame() : base(1200, 900, false)
	{
		//LevelLoader loader = new LevelLoader();
		//AddChild(loader);

		background = new Sprite("background.png");
		AddChild(background);
		background.scale = 1.5f;

		player1 = new Player(Key.LEFT, Key.RIGHT, Key.UP, Key.DOWN, "blue");
		AddChildAt(player1, 2);
		player1.color = 0x4040FF;
		player1.SetXY(950, 450);

		player2 = new Player(Key.A, Key.D, Key.W, Key.S, "red");
		AddChild(player2);
		player2.color = 0xFF4040;
		player2.SetXY(250, 450);

		puck = new Puck(0x505050);
		AddChildAt(puck, 2);
		puck.color = 0x505050;
		puck.SetXY(game.width / 2, 100);
		puck.Impulse(0.0f, 7.0f);

		blueGoal = new Goal("blue");
		AddChild(blueGoal);
		blueGoal.SetXY(1050, 450);
		blueGoal.scaleX = 0.5f;
		blueGoal.scaleY = 3.0f;

		redGoal = new Goal("red");
		AddChild(redGoal);
		redGoal.SetXY(150, 450);
		redGoal.scaleX = 0.5f;
		redGoal.scaleY = 3.0f;

		blueBoard = new ScoreBoard("blue");
		AddChild(blueBoard);
		blueBoard.color = 0x0000F0;
		blueBoard.SetXY(1100, 50);

		redBoard = new ScoreBoard("red");
		AddChild(redBoard);
		redBoard.color = 0xF00000;
		redBoard.SetXY(100, 50);
	}

	public void Resetti()
	{
		puck.Reset();
		while (extraPuck != null)
		{
			extraPuck.Reset();
			extraPuck = null;
		}
		player1.Reset();
		player1.SetXY(950, 450);
		player2.Reset();
		player2.SetXY(250, 450);
		blueGoal.Reset();
		blueGoal.SetXY(1050, 450);
		redGoal.Reset();
		redGoal.SetXY(150, 450);
		player1.InversedControls = false;
		player2.InversedControls = false;
	}

	//update game here
	void Update()
	{
		if (puckReflection != null)
		{
			puckReflection.SetXY(puck.x - 9.5f, puck.y - 9.5f);
		}

		/*if (extraPuckReflection != null && parent != null)
		{
			extraPuckReflection.SetXY(parent.x - 9.5f, parent.y - 9.5f);
		}*/

		if (Input.GetKeyDown(Key.ONE))
		{
			extraPuck = new Puck(0xE000FF);
			AddChild(extraPuck);
			extraPuck.color = 0xE000FF;
			extraPuck.SetXY(game.width / 2, game.height / 2);
		}

		if (Input.GetKeyDown(Key.TWO)){
			if (player1.InversedControls == false)
			{
				player1.InversedControls = true;
			}
			else {
				player1.InversedControls = false;
			}

			if (player2.InversedControls == false){
				player2.InversedControls = true;
			}
			else{
				player2.InversedControls = false;
			}
		}

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
