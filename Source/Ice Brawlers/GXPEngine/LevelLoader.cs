using System;
using System.IO;
namespace GXPEngine
{
	public class LevelLoader : GameObject
	{
		int rcounter = 0;
		string[] rline;
		int powerCooldown;
		Random rand = new Random();
		int victoryWait;

		public Player player1;
		public Player player2;
		AntiGoal notGoal;
		public Goal blueGoal;
		public Goal redGoal;
		public Puck puck;
		public int timeGetBlue;
		public int timeGetRed;
		public Puck extraPuck;
		public Sprite scoreBoard;
		public Sprite puckReflection;
		public ScoreBoard blueCounter;
		public ScoreBoard blueCounter2;
		public ScoreBoard redCounter;
		public ScoreBoard redCounter2;
		public ScoreBoard timeCounter;
		AnimationSprite timeDivider;
		public AnimationSprite redSupporter;
		public AnimationSprite blueSupporter;
		public Sprite lowerGlass;
		public PowerUps powerUp;
		public AnimationSprite bluePower;
		public AnimationSprite redPower;

		public float objectScaleX;
		public float objectScaleY;

		private const int HEIGHT = 19;
		private const int WIDTH = 26;
		private const int TILESIZE = 64;

		string[] Layout = {
				 "//This file contains the layout of the game Ice Brawlers." ,
				 "//You may edit file below to change the layout of the main level." ,
				 "//In case you mess up this file, you can just delete it, and it will be reset upon restarting the game." ,
				 "//" ,
				 "9,9,9,9,9,9,9,9,9,9,9,9,8,10,10,10,10,10,10,10,10,10,10,10,10,10",
				 "9,9,9,9,9,9,9,9,9,9,9,7,9,10,10,6,10,10,10,10,10,10,10,10,10,10",
				 "9,9,9,9,9,9,9,9,9,9,9,9,9,10,10,10,10,10,10,10,10,10,10,10,10,10",
				 "9,9,9,9,9,9,9,9,9,9,9,9,9,10,10,10,10,10,10,10,10,10,10,10,10,10",
				 "0,0,0,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0", //top border
				 "0,0,0,0,0,0,0,0,0,0,12,0,0,0,0,11,0,0,0,0,0,0,0,0,0,0",
				 "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0",
				 "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0",
				 "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0",
				 "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0",
				 "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0",
				 "0,0,0,5,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,4,0,0,0",
				 "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0",
				 "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0",
				 "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0",
				 "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0",
				 "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0",
				 "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0",
				 "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0"
		};

		protected override Core.Collider createCollider()
		{
			return null;//base.createCollider();
		}

		public LevelLoader(float rScaleX, float rScaleY)
		{
			//objectScaleX = rScaleX / 1.5f;
			//objectScaleY = rScaleY / 1.5f;


			((MyGame)game).background = new Sprite("assets/sprites/testbackground.png");
			AddChildAt(((MyGame)game).background, 0);
			((MyGame)game).background.SetXY(0, 0);

			Sprite fullAudience = new Sprite("assets/sprites/fullaudience.png");
			AddChildAt(fullAudience, 0);
			fullAudience.SetXY(0, 0);


			((MyGame)game).countDown.Play();

			//((MyGame)game).backgroundMusic.Play();

			if (!File.Exists("LevelLayout.txt"))
			{
				//File.Create("LevelLayout.txt");
				File.WriteAllLines("LevelLayout.txt", Layout);
			}

			if (File.Exists("LevelLayout.txt"))
			{
				//FileStream file = new FileStream("LevelLayout.txt", FileMode.Open, FileAccess.Read);
				StreamReader input = new StreamReader("LevelLayout.txt");

				int[,] Level = new int[HEIGHT, WIDTH];
				int counter = 0;


				while (true)
				{
					string line = input.ReadLine();
					//Console.WriteLine("Line read: " + line);
					if (line == null)
						break;
					else if (line.Substring(0, 2) == "//")  // comment
						continue;
					else {// assume it is a string of integers separated by commas
						string[] symbols = line.Split(new char[] { ',' });
						if (symbols.Length != WIDTH || counter >= HEIGHT)
							throw new Exception();
						for (int i = 0; i < symbols.Length; i++)
						{
							//Console.Write(symbols[i] + " - ");
							Level[counter, i] = int.Parse(symbols[i]);
						}
						counter++;
						//Console.WriteLine();
					}

				}
				//Console.WriteLine("End of file!");
				//Console.WriteLine("Level info:");
				for (int row = 0; row < HEIGHT; row++)
				{
					for (int column = 0; column < WIDTH; column++)
					{
						//Console.Write(Level[row, column] + ",");

						int tile = Level[row, column];

						switch (tile)
						{
							case 0:
								break;

							case 1:
								player1 = new Player(Key.LEFT, Key.RIGHT, Key.UP, Key.DOWN, "blue");
								AddChildAt(player1, 5199);
								player1.color = 0x4040FF;
								player1.alpha = 0.0f;
								player1.SetXY(column * TILESIZE, ((float)row + 0.5f) * TILESIZE);
								break;

							case 2:
								player2 = new Player(Key.A, Key.D, Key.W, Key.S, "red");
								AddChildAt(player2,5199);
								player2.color = 0xFF4040;
								player2.alpha = 0.0f;
								player2.SetXY(column * TILESIZE, ((float)row + 0.5f) * TILESIZE);
								break;

							case 3:
								puck = new Puck(0x505050);
								AddChildAt(puck, 1000);
								puck.color = 0x505050;
								puck.SetXY((column * TILESIZE) + TILESIZE/2.0f, (row + 2) * (TILESIZE));
								puck.Impulse(0.0f, 6.0f);
								break;

							case 4:
								blueGoal = new Goal("blue");
								AddChildAt(blueGoal,0);
								blueGoal.SetXY((column * TILESIZE), ((float)row + 0.5f) * TILESIZE);
								blueGoal.scaleX = 0.5f;
								blueGoal.scaleY = 1.0f;

								AntiGoal notBlue = new AntiGoal();
								AddChildAt(notBlue,109);
								notBlue.SetXY(blueGoal.x + 10, blueGoal.y);
								notBlue.scaleX = 0.8f;
								notBlue.scaleY = 1.5f;

								AnimationSprite blueGoalSprite = new AnimationSprite("assets/sprites/testgoals.png", 2, 1);
								blueGoalSprite.SetOrigin(blueGoalSprite.width / 2, blueGoalSprite.height / 2);
								notBlue.AddChildAt(blueGoalSprite,-1);
								blueGoalSprite.currentFrame = 1;
								blueGoalSprite.scaleX = 0.75f;
								blueGoalSprite.scaleY = 0.55f;
								blueGoalSprite.SetXY( +2.5f, -13.0f);
								break;

							case 5:
								redGoal = new Goal("red");
								AddChildAt(redGoal,800);
								redGoal.SetXY((column * TILESIZE), ((float)row + 0.5f) * TILESIZE);
								redGoal.scaleX = 0.5f;
								redGoal.scaleY = 1.0f;

								AntiGoal notRed = new AntiGoal();
								AddChild(notRed);
								notRed.SetXY(redGoal.x - 10, redGoal.y);
								notRed.scaleX = 0.8f;
								notRed.scaleY = 1.5f;

								AnimationSprite redGoalSprite = new AnimationSprite("assets/sprites/testgoals.png", 2, 1);
								redGoalSprite.SetOrigin(redGoalSprite.width / 2, redGoalSprite.height / 2);
								notRed.AddChildAt(redGoalSprite,0);
								redGoalSprite.currentFrame = 0;
								redGoalSprite.scaleX = 0.75f;
								redGoalSprite.scaleY = 0.55f;
								redGoalSprite.SetXY( -2.5f, -13.0f);
								break;

							case 6:
								blueCounter = new ScoreBoard("blue");
								AddChildAt(blueCounter, 100);
								//blueCounter.color = 0x9090FF;
								blueCounter.scaleX = 0.675f;
								blueCounter.scaleY = 0.675f;
								blueCounter.SetXY((column * TILESIZE) + 2.0f, (row * TILESIZE) + 25.0f);

								blueCounter2 = new ScoreBoard("blue2");
								AddChildAt(blueCounter2, 100);
								blueCounter2.scaleX = 0.675f;
								blueCounter2.scaleY = 0.675f;
								blueCounter2.SetXY((column * TILESIZE) - 31.0f, (row * TILESIZE) + 25.0f);

								blueSupporter = new AnimationSprite("assets/sprites/Audience_Sprite.png", 3, 1);
								AddChildAt(blueSupporter, 0);
								blueSupporter.currentFrame = 1;
								blueSupporter.SetXY((column * TILESIZE), row * TILESIZE);
								break;

							case 7:
								redCounter = new ScoreBoard("red");
								AddChildAt(redCounter, 100);
								//redCounter.color = 0xF00000;
								redCounter.scaleX = 0.675f;
								redCounter.scaleY = 0.675f;
								redCounter.SetXY((column * TILESIZE) - 31.0f, (row * TILESIZE) + 25.0f);

								redCounter2 = new ScoreBoard("red2");
								AddChildAt(redCounter2, 100);
								redCounter2.scaleX = 0.675f;
								redCounter2.scaleY = 0.675f;
								redCounter2.SetXY((column * TILESIZE) - 64.0f, (row * TILESIZE) + 25.0f);

								redSupporter = new AnimationSprite("assets/sprites/Audience_Sprite.png", 3, 1);
								AddChildAt(redSupporter, 0);
								redSupporter.SetXY((column * TILESIZE), row * TILESIZE);
								break;

							case 8:
								timeCounter = new ScoreBoard("timer1");
								AddChildAt(timeCounter, 948999);
								timeCounter.scaleX = 1.1f;
								timeCounter.scaleY = 1.1f;
								timeCounter.SetXY((column * TILESIZE) + 95.0f, (row * TILESIZE) + 90.0f);

								timeCounter = new ScoreBoard("timer2");
								AddChildAt(timeCounter, 948999);
								timeCounter.scaleX = 1.1f;
								timeCounter.scaleY = 1.1f;
								timeCounter.SetXY((column * TILESIZE) + 45.0f, (row * TILESIZE) + 90.0f);

								timeCounter = new ScoreBoard("timer3");
								AddChildAt(timeCounter, 948999);
								timeCounter.scaleX = 1.1f;
								timeCounter.scaleY = 1.1f;
								timeCounter.SetXY((column * TILESIZE) - 26.5f, (row * TILESIZE) + 90.0f);

								timeCounter = new ScoreBoard("null");
								AddChildAt(timeCounter, 948999);
								timeCounter.currentFrame = 10;
								timeCounter.scaleX = 1.1f;
								timeCounter.scaleY = 1.1f;
								timeCounter.SetXY((column * TILESIZE) + 7.5f, (row * TILESIZE) + 90.0f);

								scoreBoard = new Sprite("assets/sprites/Scoreboard.png");
								scoreBoard.SetOrigin(scoreBoard.width / 2, 0);
								AddChildAt(scoreBoard, 13);
								scoreBoard.scaleX = 0.4f;
								scoreBoard.scaleY = 0.4f;
								scoreBoard.SetXY((column * TILESIZE) + 32.0f, (row * TILESIZE));

								redSupporter = new AnimationSprite("assets/sprites/Audience_Sprite.png", 3, 1);
								AddChildAt(redSupporter, 0);
								redSupporter.SetXY((column * TILESIZE), row * TILESIZE);
								break;

							case 9:
								redSupporter = new AnimationSprite("assets/sprites/Audience_Sprite.png", 3, 1);
								AddChildAt(redSupporter, 0);
								redSupporter.SetXY((column * TILESIZE), row * TILESIZE);
								break;

							case 10:
								blueSupporter = new AnimationSprite("assets/sprites/Audience_Sprite.png", 3, 1);
								AddChildAt(blueSupporter, 0);
								blueSupporter.currentFrame = 1;
								blueSupporter.SetXY((column * TILESIZE), row * TILESIZE);
								break;

							case 11:
								bluePower = new AnimationSprite("assets/sprites/powerups.png", 2, 5);
								AddChild(bluePower);
								bluePower.SetOrigin(bluePower.width / 2, bluePower.height / 2);
								bluePower.scale = 0.3f;
								bluePower.currentFrame = 8;
								bluePower.SetXY((column * TILESIZE), (row - 0.65f) * TILESIZE);
								break;

							case 12:
								redPower = new AnimationSprite("assets/sprites/powerups.png", 2, 5);
								AddChild(redPower);
								redPower.SetOrigin(redPower.width / 2, redPower.height / 2);
								redPower.scale = 0.3f;
								redPower.currentFrame = 8;
								redPower.SetXY((column * TILESIZE), (row - 0.65f) * TILESIZE);
								break;

							default:
								Console.WriteLine("Something Else: {0}", Level[row, column]);
								break;
						}
					}
					Console.WriteLine();
				}

				lowerGlass = new Sprite("assets/sprites/lowerbarrier.png");
				AddChildAt(lowerGlass, 729);
				lowerGlass.SetXY(0, game.height - 36);

				if (puck != null)
				{
					puckReflection = new Sprite("assets/sprites/testpuck.png");
					puck.AddChildAt(puckReflection, 1);
					puckReflection.SetXY(0, 100);
					puckReflection.alpha = 0.25f;
					puckReflection.color = 0x505050;
				}

				/*powerUp = new PowerUps();
				AddChildAt(powerUp, 5690);
				powerUp.SetXY(500, 500);*/
			}
		}

		public void LevelReset()
		{
			if (puck != null)
				puck.Reset();

			StreamReader input = new StreamReader("LevelLayout.txt");

			int[,] Level = new int[HEIGHT, WIDTH];
			int counter = 0;

			while (true){
				string line = input.ReadLine();
				if (line == null)
					break;
				else if (line.Substring(0, 2) == "//")  // comment
					continue;
				else {// assume it is a string of integers separated by commas
					string[] symbols = line.Split(new char[] { ',' });
					if (symbols.Length != WIDTH || counter >= HEIGHT)
						throw new Exception();
					for (int i = 0; i < symbols.Length; i++){
						Level[counter, i] = int.Parse(symbols[i]);
					}
					counter++;
				}
			}
			for (int row = 0; row < HEIGHT; row++){
				for (int column = 0; column < WIDTH; column++){
					int tile = Level[row, column];
					switch (tile){
						case 0:
							break;
						case 1:
							player1.SetXY(column * TILESIZE, ((float)row + 0.5f) * TILESIZE);
							break;
						case 2:
							player2.SetXY(column * TILESIZE, ((float)row + 0.5f) * TILESIZE);
							break;
						case 3:
							puck.SetXY(((column + ((((MyGame)game).ScoreRed - ((MyGame)game).ScoreBlue) * 0.5f)) * TILESIZE) + TILESIZE / 2.0f, row * (TILESIZE));
							puck.Impulse(0.0f, 6.0f
							            );
							break;
						case 4:
							blueGoal.SetXY(column * TILESIZE, ((float)row + 0.5f) * TILESIZE);
							break;
						case 5:
							redGoal.SetXY(column * TILESIZE, ((float)row + 0.5f) * TILESIZE);
							break;
						default:
							Console.Write("Something Else");
							break;
					}
				}
			}
			while (extraPuck != null){
				extraPuck.Reset();
				extraPuck = null;
			}
			player1.Reset();
			player2.Reset();
			blueGoal.Reset();
			redGoal.Reset();
			player1.InversedControls = false;
			player2.InversedControls = false;
		}

		void Update()
		{
			if (((MyGame)game).seconds <= 0 && !((MyGame)game).startLock)
			{
				if (((MyGame)game).ScoreBlue > ((MyGame)game).ScoreRed)
				{
					AnimationSprite BlueVictory = new AnimationSprite("assets/sprites/signs.png", 3, 2);
					BlueVictory.SetOrigin(BlueVictory.width / 2, BlueVictory.height / 2);
					AddChild(BlueVictory);
					BlueVictory.SetXY(game.width / 2, game.height / 2);
					BlueVictory.currentFrame = 4;
					((MyGame)game).scoreYell.Play();
					victoryWait = (Time.now / 1000) + 2;
				}

				if (((MyGame)game).ScoreBlue < ((MyGame)game).ScoreRed)
				{
					AnimationSprite RedVictory = new AnimationSprite("assets/sprites/signs.png", 3, 2);
					RedVictory.SetOrigin(RedVictory.width / 2, RedVictory.height / 2);
					AddChild(RedVictory);
					RedVictory.SetXY(game.width / 2, game.height / 2);
					RedVictory.currentFrame = 3;
					((MyGame)game).scoreYell.Play();
					victoryWait = (Time.now / 1000) + 2;
				}

			}
			else {
				victoryWait = (Time.now / 1000 + 2);
			}

			if (victoryWait < Time.now / 1000)
			{
				((MyGame)game).start = new StartMenu();
				AddChild(((MyGame)game).start);
				this.Destroy();
			}

			if (powerCooldown < Time.now / 10000)
			{
				int spawnpoint = rand.Next(1, 5);

				powerCooldown = (Time.now / 10000) + 3;
				powerUp = new PowerUps();
				AddChild(powerUp);
				powerUp.SetXY(game.width / 1.395f , game.height / 2.768f);

				switch (spawnpoint)
				{
					case 1:
						powerUp.SetXY(game.width / 4.125f, game.height / 2.775f);
						break;
					case 2:
						powerUp.SetXY(game.width / 4.425f, game.height / 1.13f);
						break;
					case 3:
						powerUp.SetXY(game.width / 1.395f, game.height / 2.768f);
						break;
					case 4:
						powerUp.SetXY(game.width / 1.362f, game.height / 1.129f);
						break;
					default:
						break;
				}
			}


			if (puckReflection != null)
			{
				puckReflection.SetXY(-16.0f, -6.0f);
			}

			/*if (extraPuckReflection != null && parent != null)
			{
				extraPuckReflection.SetXY(parent.x - 9.5f, parent.y - 9.5f);
			}*/

			if (timeGetBlue < (Time.now / 1000) && player1.usedPowerUp == true)
			{
				player1.SpeedMultiplierX = 0.4f;
				player1.SpeedMultiplierY = 0.4f;
				player1.speedLimit = 12.0f;
				bluePower.currentFrame = 8;
				bluePower.rotation = 0;
				player1.rotateBluePow = 0;
				player2.InversedControls = false;
				player1.hasPowerUp = false;
				player1.usedPowerUp = false;
			}

			if (timeGetRed < (Time.now / 1000) && player2.usedPowerUp == true)
			{
				redPower.currentFrame = 8;
				player2.SpeedMultiplierX = 0.4f;
				player2.SpeedMultiplierY = 0.4f;
				player2.speedLimit = 12.0f;
				player2.rotateRedPow = 0;
				redPower.rotation = 0;
				player1.InversedControls = false;
				player2.hasPowerUp = false;
				player2.usedPowerUp = true;
			}

			/*
			if (Input.GetKeyDown(Key.A))
			{
				((MyGame)game).addedPuck.Play();
				extraPuck = new Puck(0xE000FF);
				AddChild(extraPuck);
				extraPuck.color = 0xE000FF;
				extraPuck.SetXY(puck.x, puck.y + 11.0f);

			}

			if (Input.GetKeyDown(Key.S))
			{
				if (player1.InversedControls == false)
				{
					player1.InversedControls = true;
					((MyGame)game).reverseControls.Play();
				}
				else {
					player1.InversedControls = false;
				}

				if (player2.InversedControls == false)
				{
					player2.InversedControls = true;
				}
				else {
					player2.InversedControls = false;
				}
			}

			if (Input.GetKeyDown(Key.Q))
			{
				if (player1.ForceMultiplier != 20.0f)
					player1.ForceMultiplier = 20.0f;
				else
					player1.ForceMultiplier = 1.0f;

				if (player2.ForceMultiplier != 20.0f)
					player2.ForceMultiplier = 20.0f;
				else
					player2.ForceMultiplier = 1.0f;
			}

			if (Input.GetKeyDown(Key.W) && (player1.SpeedMultiplierX < 0.5f && player2.SpeedMultiplierX < 0.5f))
			{
				((MyGame)game).goFast.Play();
				timeGetBlue = (Time.now / 1000) + 3;
				player1.SpeedMultiplierX *= 1.5f;
				player1.SpeedMultiplierY *= 1.5f;
				player1.speedLimit *= 2.0f;
				player2.SpeedMultiplierX *= 1.5f;
				player2.SpeedMultiplierY *= 1.5f;
				player2.speedLimit *= 2.0f;
			}*/
		
			//Console.WriteLine(timeGetBlue);
		}



		//private int[,] LevelLayout = new int[HEIGHT, WIDTH]{
		/*
	int[,] Level = new int[19, 26]{
		{ 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
		{ 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
		{ 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
		{ 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
		{ 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
		{ 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
		{ 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
		{ 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
		{ 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
		{ 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
		{ 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
		{ 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
		{ 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
		{ 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
		{ 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
		{ 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
		{ 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
		{ 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
		{ 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }
	};
row<rr		*/



		/*
		String input = File.ReadAllText(@"c:\myfile.txt");

		int i = 0, j = 0;
		int[,] result = new int[10, 10];
		foreach (var row in input.Split('\n'))
		{
			j = 0;
			foreach (var col in row.Trim().Split(' '))
			{
				result[i, j] = int.Parse(col.Trim());
				j++;
			}
			i++;
		}
		*/


		/*using (StreamWriter rfile = new StreamWriter(@"C:\Games\LevelLayout.txt"))

		foreach (string line in Layout)
		{
			if (!line.Contains("null"))
			{
				rfile.WriteLine(line);
			}
		}

		/*if (!File.Exists(@"C:\Games\LevelLayout.txt"))
		{
			File.WriteAllLines(@"C:\Games\LevelLayout.txt", Layout);
		}*/

		//System.IO.StreamReader file = new System.IO.StreamReader((@"C:\Games\LevelLayout.txt"));

		/*
		while ((rline = file.ReadLine()) != null)
		{
			//System.IO.

			if (!rline.Contains("//"))
			{

					//Level[rcounter, i] = int.Parse(rline);
					Console.WriteLine(rline);

				rcounter++;
			}
		}*/
		//file.Close();


		//System.Console.WriteLine("There were {0} lines.", rcounter);



		/*StreamReader file = new StreamReader((@"D:\test.txt"));
		while ((rline = file.ReadLine()) != null)
		{
			Console.WriteLine(rline);
			rcounter++;
		}

		file.Close();
		Console.WriteLine("There were {0} lines.", rcounter);

		Console.ReadLine();
		*/
	}
}
