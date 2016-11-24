using System;
using System.IO;
namespace GXPEngine
{
	public class LevelLoader : GameObject
	{
		int rcounter = 0;
		string[] rline;

		public Player player1;
		public Player player2;
		AntiGoal notGoal;
		public Goal blueGoal;
		public Goal redGoal;
		public Puck puck;
		int timeGet;
		Puck extraPuck;
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
				 "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0",
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


			((MyGame)game).background = new Sprite("testbackground.png");
			AddChildAt(((MyGame)game).background, 0);
			((MyGame)game).background.SetXY(0, 0);

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
								player1.alpha = 0.5f;
								player1.SetXY(column * TILESIZE, ((float)row + 0.5f) * TILESIZE);
								break;

							case 2:
								player2 = new Player(Key.A, Key.D, Key.W, Key.S, "red");
								AddChildAt(player2,5199);
								player2.color = 0xFF4040;
								player2.alpha = 0.5f;
								player2.SetXY(column * TILESIZE, ((float)row + 0.5f) * TILESIZE);
								break;

							case 3:
								puck = new Puck(0x505050);
								AddChildAt(puck, 1000);
								puck.color = 0x505050;
								puck.SetXY((column * TILESIZE) + TILESIZE/2.0f, row * (TILESIZE));
								puck.Impulse(0.0f, 6.0f * objectScaleY);
								break;

							case 4:
								blueGoal = new Goal("blue");
								AddChildAt(blueGoal,700);
								blueGoal.SetXY((column * TILESIZE), ((float)row + 0.5f) * TILESIZE);
								blueGoal.scaleX = 0.5f;
								blueGoal.scaleY = 1.0f;

								AntiGoal notBlue = new AntiGoal();
								AddChild(notBlue);
								notBlue.SetXY(blueGoal.x + 10, blueGoal.y);
								notBlue.scaleX = 0.8f;
								notBlue.scaleY = 1.5f;

								AnimationSprite blueGoalSprite = new AnimationSprite("testgoals.png", 2, 1);
								blueGoalSprite.SetOrigin(blueGoalSprite.width / 2, blueGoalSprite.height / 2);
								notBlue.AddChild(blueGoalSprite);
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

								AnimationSprite redGoalSprite = new AnimationSprite("testgoals.png", 2, 1);
								redGoalSprite.SetOrigin(redGoalSprite.width / 2, redGoalSprite.height / 2);
								notRed.AddChild(redGoalSprite);
								redGoalSprite.currentFrame = 0;
								redGoalSprite.scaleX = 0.75f;
								redGoalSprite.scaleY = 0.55f;
								redGoalSprite.SetXY( -2.5f, -13.0f);
								break;

							case 6:
								blueCounter = new ScoreBoard("blue");
								AddChildAt(blueCounter, 100);
								//blueCounter.color = 0x9090FF;
								blueCounter.scaleX = 1.0f / 1.9f;
								blueCounter.scaleY = 1.0f / 1.9f;
								blueCounter.SetXY((column * (TILESIZE * 0.925f)) + TILESIZE / 2.0f, row * (TILESIZE * 1.55f));

								blueCounter2 = new ScoreBoard("blue2");
								AddChildAt(blueCounter2, 100);
								blueCounter2.scaleX = objectScaleX / 1.9f;
								blueCounter2.scaleY = objectScaleY / 1.9f;
								blueCounter2.SetXY((column * (TILESIZE * 0.89f)) + TILESIZE / 2.0f, row * (TILESIZE * 1.55f));

								blueSupporter = new AnimationSprite("Audience_Sprite.png", 3, 1);
								AddChildAt(blueSupporter, 0);
								blueSupporter.currentFrame = 1;
								blueSupporter.SetXY((column * TILESIZE), row * TILESIZE);
								break;

							case 7:
								redCounter = new ScoreBoard("red");
								AddChildAt(redCounter, 100);
								//redCounter.color = 0xF00000;
								redCounter.scaleX = 0.6f;
								redCounter.scaleY = 0.6f;
								redCounter.SetXY((column * TILESIZE) - 10.0f, (row * TILESIZE) + 12.0f);

								redCounter2 = new ScoreBoard("red2");
								AddChildAt(redCounter2, 100);
								redCounter2.scaleX = 0.6f;
								redCounter2.scaleY = 0.6f;
								redCounter2.SetXY((column * TILESIZE) - 38.0f, (row * TILESIZE) + 12.0f);

								redSupporter = new AnimationSprite("Audience_Sprite.png", 3, 1);
								AddChildAt(redSupporter, 0);
								redSupporter.SetXY((column * TILESIZE), row * TILESIZE);
								break;

							case 8:
								timeCounter = new ScoreBoard("timer1");
								AddChildAt(timeCounter, 948999);
								timeCounter.scaleX = 0.9f;
								timeCounter.scaleY = objectScaleY / 1.15f;
								timeCounter.SetXY(((column + 1.55f) * TILESIZE), (row + 1.55f) * TILESIZE);

								timeCounter = new ScoreBoard("timer2");
								AddChildAt(timeCounter, 948999);
								timeCounter.scaleX = 1.0f / 1.15f;
								timeCounter.scaleY = 1.0f / 1.15f;
								timeCounter.SetXY(((column + 0.65f) * TILESIZE), (row + 1.55f) * TILESIZE);

								timeCounter = new ScoreBoard("timer3");
								AddChildAt(timeCounter, 948999);
								timeCounter.scaleX = 1.0f / 1.15f;
								timeCounter.scaleY = 1.0f / 1.15f;
								timeCounter.SetXY(((column - 0.5f) * TILESIZE), (row + 1.55f) * TILESIZE);

								timeCounter = new ScoreBoard("null");
								AddChildAt(timeCounter, 948999);
								timeCounter.currentFrame = 10;
								timeCounter.scaleX = 1.0f / 1.15f;
								timeCounter.scaleY = 1.0f / 1.15f;
								timeCounter.SetXY(((column + 0.04f) * TILESIZE), (row + 1.55f) * TILESIZE);

								scoreBoard = new Sprite("Scoreboard.png");
								scoreBoard.SetOrigin(scoreBoard.width / 2, 0);
								AddChildAt(scoreBoard, 13);
								scoreBoard.scaleX = 1.0f / 3.0f;
								scoreBoard.scaleY = 1.0f / 3.0f;
								scoreBoard.SetXY((column * TILESIZE)+ TILESIZE / 2.0f, row * TILESIZE);

								redSupporter = new AnimationSprite("Audience_Sprite.png", 3, 1);
								AddChildAt(redSupporter, 0);
								redSupporter.SetXY((column * TILESIZE), row * TILESIZE);
								break;

							case 9:
								redSupporter = new AnimationSprite("Audience_Sprite.png", 3, 1);
								AddChildAt(redSupporter, 0);
								redSupporter.SetXY((column * TILESIZE), row * TILESIZE);
								break;

							case 10:
								blueSupporter = new AnimationSprite("Audience_Sprite.png", 3, 1);
								AddChildAt(blueSupporter, 0);
								blueSupporter.currentFrame = 1;
								blueSupporter.SetXY((column * TILESIZE), row * TILESIZE);
								break;
								
							default:
								Console.WriteLine("Something Else: {0}", Level[row, column]);
								break;
						}
					}
					Console.WriteLine();
				}

				lowerGlass = new Sprite("lowerbarrier.png");
				AddChildAt(lowerGlass, 7299);
				lowerGlass.SetXY(0, game.height - 36);

				if (puck != null)
				{
					puckReflection = new Sprite("testpuck.png");
					puck.AddChildAt(puckReflection, 1);
					puckReflection.SetXY(0, 100);
					puckReflection.alpha = 0.25f;
					puckReflection.color = 0x505050;
				}

				powerUp = new PowerUps();
				AddChildAt(powerUp, 5690);
				powerUp.SetXY(500, 500);
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
							puck.SetXY(((column + ((((MyGame)game).ScoreRed - ((MyGame)game).ScoreBlue) * 0.5f)) * TILESIZE) + TILESIZE / 2.0f, row * (TILESIZE * 0.84f));
							puck.Impulse(0.0f, 6.0f * objectScaleY);
							break;
						case 4:
							blueGoal.SetXY(column * TILESIZE, ((float)row + 0.5f) * TILESIZE);
							break;
						case 5:
							redGoal.SetXY(column * TILESIZE, ((float)row + 0.5f) * TILESIZE);
							break;
						case 6:
							blueCounter.SetXY((column * (TILESIZE * 0.925f)) + TILESIZE / 2.0f, row * (TILESIZE * 1.55f));
							break;
						case 7:
							redCounter.SetXY((column * (TILESIZE * 0.979f)) + TILESIZE / 2.0f, row * (TILESIZE * 1.55f));
							break;
						case 8:
							scoreBoard.SetXY((column * TILESIZE)+ TILESIZE / 2.0f, row * TILESIZE);
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
			if (puckReflection != null)
			{
				puckReflection.SetXY(-16.0f, -6.0f);
			}

			/*if (extraPuckReflection != null && parent != null)
			{
				extraPuckReflection.SetXY(parent.x - 9.5f, parent.y - 9.5f);
			}*/

			if (timeGet < (Time.now / 1000))
			{
				player1.SpeedMultiplierX = 0.4f;
				player1.SpeedMultiplierY = 0.4f;
				player1.speedLimit = 12.0f;
				timeGet = 0;
				player2.SpeedMultiplierX = 0.4f;
				player2.SpeedMultiplierY = 0.4f;
				player2.speedLimit = 12.0f;
			}


			if (Input.GetKeyDown(Key.ONE))
			{
				//((MyGame)game).addedPuck = new Sound("");

				extraPuck = new Puck(0xE000FF);
				AddChild(extraPuck);
				extraPuck.scaleX = objectScaleX / 1.5f;
				extraPuck.scaleY = objectScaleY / 1.5f;
				extraPuck.color = 0xE000FF;
				extraPuck.SetXY(puck.x, puck.y + 11.0f);

			}

			if (Input.GetKeyDown(Key.TWO))
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

			if (Input.GetKeyDown(Key.THREE))
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

			if (Input.GetKeyDown(Key.FOUR) && (player1.SpeedMultiplierX < 0.5f && player2.SpeedMultiplierX < 0.5f))
			{
				((MyGame)game).goFast.Play();
				timeGet = (Time.now / 1000) + 3;
				player1.SpeedMultiplierX *= 1.5f;
				player1.SpeedMultiplierY *= 1.5f;
				player1.speedLimit *= 2.0f;
				player2.SpeedMultiplierX *= 1.5f;
				player2.SpeedMultiplierY *= 1.5f;
				player2.speedLimit *= 2.0f;
			}
		
			//Console.WriteLine(timeGet);
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
