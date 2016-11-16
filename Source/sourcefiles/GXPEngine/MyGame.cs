using System;
using System.Drawing;
using GXPEngine;

public class MyGame : Game //MyGame is a Game
{	Player player1;
	Player player2;
	Puck puck;
	Sprite background;

	//initialize game here
	public MyGame () : base(1200, 900, false)
	{
		background = new Sprite("background.png");
		AddChild(background);
		background.scale = 1.5f;

		player1 = new Player(Key.LEFT, Key.RIGHT, Key.UP, Key.DOWN);
		AddChild(player1);
		player1.color = 0x4040FF;
		player1.SetXY(1000, 450);

		player2 = new Player(Key.A, Key.D, Key.W, Key.S);
		AddChild(player2);
		player2.SetXY(200, 450);

		puck = new Puck();
		AddChild(puck);
		player2.color = 0xFF4040;
		puck.SetXY(game.width / 2, game.height);
	}
	
	//update game here
	void Update ()
	{
		//empty
	}
	
	//system starts here
	static void Main() 
	{
		new MyGame().Start();
	}
}
