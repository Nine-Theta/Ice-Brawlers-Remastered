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
		Puck extraPuck;
		Sprite scoreBoard;
		public Sprite puckReflection;
		public ScoreBoard blueCounter;
		public ScoreBoard blueCounter2;
		public ScoreBoard redCounter;
		public ScoreBoard redCounter2;

		public float objectScaleX;
		public float objectScaleY;

		private const int HEIGHT = 19;
		private const int WIDTH = 26;
		private const int TILESIZE = 48;

		string[] Layout = {
				 "//This file contains the layout of the game Ice Brawlers." ,
				 "//You may edit file below to change the layout of the main level." ,
				 "//In case you mess up this file, you can just delete it, and it will be reset upon restarting the game." ,
				 "//" ,
				 "0,0,0,0,0,0,0,0,0,0,0,0,8,0,0,0,0,0,0,0,0,0,0,0,0,0",
				 "0,0,0,0,0,0,0,0,0,0,7,0,0,0,0,0,6,0,0,0,0,0,0,0,0,0",
				 "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0",
				 "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0",
				 "0,0,0,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0", //top border
				 "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0",
				 "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0",
				 "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0",
				 "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0",
				 "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0",
				 "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0",
				 "0,0,5,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,4,0,0",
				 "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0",
				 "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0",
				 "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0",
				 "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0",
				 "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0",
				 "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0",
				 "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0"
		};

		public LevelLoader(float rScaleX, float rScaleY)
		{
			objectScaleX = rScaleX / 1.5f;
			objectScaleY = rScaleY / 1.5f;


			((MyGame)game).background = new Sprite("testbackground.png");
			AddChildAt(((MyGame)game).background, 0);
			((MyGame)game).background.SetXY(0, -64);
			((MyGame)game).background.scaleX = rScaleX / 2.0f;
			((MyGame)game).background.scaleY = rScaleY / 2.0f;

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
								AddChildAt(player1, 2);
								player1.color = 0x4040FF;
								player1.scaleX = objectScaleX;
								player1.scaleY = objectScaleY;
								player1.SetXY(column * TILESIZE, row * (TILESIZE ));
								break;

							case 2:
								player2 = new Player(Key.A, Key.D, Key.W, Key.S, "red");
								AddChild(player2);
								player2.color = 0xFF4040;
								player2.scaleX = objectScaleX;
								player2.scaleY = objectScaleY;
								player2.SetXY(column * TILESIZE, row * (TILESIZE ));
								break;

							case 3:
								puck = new Puck(0x505050);
								AddChildAt(puck, 2);
								puck.color = 0x505050;
								puck.scaleX = objectScaleX / 1.5f;
								puck.scaleY = objectScaleY / 1.5f;
								puck.SetXY((column * TILESIZE) + TILESIZE/2.0f, row * (TILESIZE));
								puck.Impulse(0.0f, 6.0f * objectScaleY);
								break;

							case 4:
								blueGoal = new Goal("blue");
								AddChild(blueGoal);
								blueGoal.SetXY(column * TILESIZE, row * (TILESIZE));
								blueGoal.scaleX = objectScaleX / 2.0f;
								blueGoal.scaleY = objectScaleY;
								break;

							case 5:
								redGoal = new Goal("red");
								AddChild(redGoal);
								redGoal.SetXY(column * TILESIZE, row * (TILESIZE));
								redGoal.scaleX = objectScaleX / 2.0f;
								redGoal.scaleY = objectScaleY ;
								break;

							case 6:
								blueCounter = new ScoreBoard("blue");
								AddChildAt(blueCounter, 10);
								//blueCounter.color = 0x9090FF;
								blueCounter.scaleX = objectScaleX / 1.9f;
								blueCounter.scaleY = objectScaleY / 1.9f;
								blueCounter.SetXY((column * (TILESIZE * 0.925f)) + TILESIZE / 2.0f, row * (TILESIZE * 1.55f));

								blueCounter2 = new ScoreBoard("blue2");
								AddChildAt(blueCounter2, 10);
								blueCounter2.scaleX = objectScaleX / 1.9f;
								blueCounter2.scaleY = objectScaleY / 1.9f;
								blueCounter2.SetXY((column * (TILESIZE * 0.89f)) + TILESIZE / 2.0f, row * (TILESIZE * 1.55f));
								break;

							case 7:
								redCounter = new ScoreBoard("red");
								AddChildAt(redCounter, 10);
								//redCounter.color = 0xF00000;
								redCounter.scaleX = objectScaleX / 1.9f;
								redCounter.scaleY = objectScaleY / 1.9f;
								redCounter.SetXY((column * (TILESIZE * 0.979f)) + TILESIZE / 2.0f, row * (TILESIZE * 1.55f));

								redCounter2 = new ScoreBoard("red2");
								AddChildAt(redCounter2, 10);
								redCounter2.scaleX = objectScaleX / 1.9f;
								redCounter2.scaleY = objectScaleY / 1.9f;
								redCounter2.SetXY((column * (TILESIZE * 0.9229f)) + TILESIZE / 2.0f, row * (TILESIZE * 1.55f));
								break;

							case 8:
								scoreBoard = new Sprite("Scoreboard.png");
								scoreBoard.SetOrigin(scoreBoard.width / 2, 0);
								AddChildAt(scoreBoard, 2);
								scoreBoard.scaleX = objectScaleX / 3.0f;
								scoreBoard.scaleY = objectScaleY / 3.0f;
								scoreBoard.SetXY((column * TILESIZE)+ TILESIZE / 2.0f, row * TILESIZE);
								break;
								
							default:
								Console.Write("Something Else: {0}", Level[row, column]);
								break;
						}
					}
					Console.WriteLine();
				}


				if (puck != null)
				{
					puckReflection = new Sprite("testpuck.png");
					puck.AddChildAt(puckReflection, 1);
					puckReflection.scale = 1.1f;
					puckReflection.SetXY(0, 100);
					puckReflection.alpha = 0.25f;
					puckReflection.color = 0x505050;
				}
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
							player1.scaleX = objectScaleX;
							player1.scaleY = objectScaleY;
							player1.SetXY(column * TILESIZE, row * (TILESIZE ));
							break;
						case 2:
							player2.scaleX = objectScaleX;
							player2.scaleY = objectScaleY;
							player2.SetXY(column * TILESIZE, row * (TILESIZE ));
							break;
						case 3:
							puck.scaleX = objectScaleX / 1.5f;
							puck.scaleY = objectScaleY / 1.5f;
							puck.SetXY(((column + ((((MyGame)game).ScoreRed - ((MyGame)game).ScoreBlue) * 0.5f)) * TILESIZE) + TILESIZE / 2.0f, row * (TILESIZE * 0.84f));
							puck.Impulse(0.0f, 6.0f * objectScaleY);
							break;
						case 4:
							blueGoal.SetXY(column * TILESIZE, row * TILESIZE );
							blueGoal.scaleX = objectScaleX / 2.0f;
							blueGoal.scaleY = objectScaleY;
							break;
						case 5:
							redGoal.SetXY(column * TILESIZE, row * (TILESIZE ));
							redGoal.scaleX = objectScaleX / 2.0f;
							redGoal.scaleY = objectScaleY;
							break;
						case 6:
							blueCounter.scaleX = objectScaleX / 1.9f;
							blueCounter.scaleY = objectScaleY / 1.9f;
							blueCounter.SetXY((column * (TILESIZE * 0.925f)) + TILESIZE / 2.0f, row * (TILESIZE * 1.55f));
							break;
						case 7:
							redCounter.scaleX = objectScaleX / 1.9f;
							redCounter.scaleY = objectScaleY / 1.9f;
							redCounter.SetXY((column * (TILESIZE * 0.979f)) + TILESIZE / 2.0f, row * (TILESIZE * 1.55f));
							break;
						case 8:
							scoreBoard.scaleX = objectScaleX / 3.0f;
							scoreBoard.scaleY = objectScaleY / 3.0f;
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
				puckReflection.SetXY(-16.0f, -16.0f);
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

			if (Input.GetKeyDown(Key.TWO))
			{
				if (player1.InversedControls == false)
				{
					player1.InversedControls = true;
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

			if (Input.GetKeyDown(Key.FOUR))
			{
				for (int timeGet = (Time.now / 1000) + 2; timeGet == (Time.now / 1000) + 5;)
				{
					player1.SpeedMultiplierX *= 2.0f;
					player1.SpeedMultiplierY *= 2.0f;
					player1.speedLimit *= 2.0f;
				}
			}
		


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
