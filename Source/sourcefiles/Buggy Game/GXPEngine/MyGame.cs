using System;
using System.Drawing;
using GXPEngine;

//TODO: Remove most of the statics
//TODO: Make an End-Game
//TODO: Add Music & SoundFX
//TODO: Optimize the hell out of this code
//DONE-ish: Make a functioning start screen

public class MyGame : Game //MyGame is a Game
{
	//initialize game here
	//private Wall wall;
	//private Player playerChar;

	public Level currentLevel { get; private set; }
	public bool SouthPaw = false;
	private Level ShopLevel;
	private Level ShopInterface;

	public MyGame() : base(1920, 1080, false)
	{
		currentLevel = new Level();
		AddChild(currentLevel);
		//CreateNewLevel();
	}

	//update game here
	public void Update()
	{
		if (Player.Alive == false || Level.EnemyAlive == 0)
		{
			currentLevel.Destroy();
			CreateShop();
		}
		if (Level.CurrentLevel == 100 && Player.ShopInteract == true)
		{
			ShopLevel.Destroy();
			Level.CurrentLevel = 101;			ShopInterface = new Level();
			AddChild(ShopInterface);
			Player.ShopInteract = false;
		}

		if (Level.CurrentLevel == 101 && Pointer.ExitShopInterface == true)
		{
			ShopInterface.Destroy();
			Pointer.ExitShopInterface = false;
			CreateShop();
		}

		if (Level.CurrentLevel == 100 && ExitPlate.ExitShop == true)
		{
			ExitPlate.ExitShop = false;
			ShopLevel.Destroy();
			CreateNewLevel();
		}

		if (Level.CurrentLevel == 102)
		{
			if (currentLevel.exit.currentFrame == 5 && (Input.GetKeyDown(Key.ENTER) || Input.GetKeyDown(Key.SPACE)))
			{
				Environment.Exit(0);
			}

			if (currentLevel.start.currentFrame == 1 && (Input.GetKeyDown(Key.ENTER) || Input.GetKeyDown(Key.SPACE)))
			{
				currentLevel.Destroy();
				CreateNewLevel();
			}

			if (Level.CurrentLevel == 102 && currentLevel.options.currentFrame == 3 && (Input.GetKeyDown(Key.ENTER) || Input.GetKeyDown(Key.SPACE)))
			{
				currentLevel.Destroy();
				Level.CurrentLevel = 103;
				currentLevel = new Level();
				AddChild(currentLevel);
			}
		}

		if (Level.CurrentLevel == 103)
		{
			if (currentLevel.southPawControls.currentFrame == 0)
			{
					this.SouthPaw = false;
			}

			if (currentLevel.southPawControls.currentFrame == 1)
			{
					this.SouthPaw = true;
			}

			if (currentLevel.pointer.y == currentLevel.exit.y && (Input.GetKeyDown(Key.ENTER) || Input.GetKeyDown(Key.SPACE)))
			{
				currentLevel.Destroy();
				Level.CurrentLevel = 102;
				currentLevel = new Level();
				AddChild(currentLevel);
			}

			//Console.WriteLine("{0}", SouthPaw);
		}
	}

	private void CreateShop()
	{
		currentLevel.Destroy();
		Level.CurrentLevel = 100;
		ShopLevel = new Level();
		AddChild(ShopLevel);
		Player.Alive = true;
	}
	private void CreateNewLevel()
	{
		currentLevel.Destroy();
		Level.CurrentLevel = Level.NextLevel;
		currentLevel = new Level();
		AddChild(currentLevel);
	}
	static void Main()
	{
		new MyGame().Start();
	}
}
