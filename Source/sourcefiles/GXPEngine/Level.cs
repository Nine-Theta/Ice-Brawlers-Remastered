using System;
using GXPEngine;

namespace GXPEngine
{
	public class Level : GameObject
	{
		public Player player { get; private set; }
		public LevelStorage LvlStorage = new LevelStorage();
		public Enemy newEnemy;
		public MenuText start;
		public MenuText options;
		public MenuText exit;
		public Pointer pointer;
		public SouthPaw southPawControls;
		public static int NextLevel = 1;
		public static int CurrentLevel = 102;
		public static int BorderIncreaseX = 2;
		public static int BorderIncreaseY = 5;
		public static int EnemySpawnAmount = 2;
		public static int EnemyAlive = 1;
		Random EnemySpawnRand = new Random();
		private const int HEIGHT = 35;
		private const int WIDTH = 61;
		private const int TILESIZE = 32;

		private int[,] LevelLayout = new int[HEIGHT, WIDTH]{
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,2,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}
		};

		public Level()
		{
			//PlayerTest player1 = new PlayerTest();
			//AddChild(player1);

			GenerateLevel();
		}

		private void GenerateLevel()
		{
			//((MyGame)game).currentLevel.player.x
			EnemySpawnAmount = LvlStorage.EnemyAmount(CurrentLevel);
			EnemyAlive = EnemySpawnAmount;
			
			for (int row = 0; row < HEIGHT; row++)
			{
				for (int column = 0; column < WIDTH; column++)
				{
					int tile = LvlStorage.LevelCaller(CurrentLevel)[row, column];

					switch (tile)
					{
						case 0:
							break;
						case 1:
							Wall borderWall = new Wall();
							AddChild(borderWall);
							borderWall.SetXY((column) * TILESIZE, (row) * TILESIZE);
							break;
						case 2:
							player = new Player();
							AddChild(player);
							player.SetXY(column * TILESIZE, row * TILESIZE);
							break;
						case 3:
							Enemy TestEnemy = new Enemy();
							AddChild(TestEnemy);
							TestEnemy.SetXY(column * TILESIZE, row * TILESIZE);
							break;
						case 4:
							Wall ShopWall = new Wall();
							AddChild(ShopWall);
							ShopWall.SetFrame(1);
							ShopWall.SetXY(column * TILESIZE, row * TILESIZE);
							break;
						case 5:
							ShopKeep theShopKeep = new ShopKeep();
							AddChild(theShopKeep);
							theShopKeep.SetXY(column * TILESIZE, row * TILESIZE);
							break;
						case 6:
							Coin currency = new Coin();
							AddChild(currency);
							currency.SetFrame(0);
							currency.scale = 0.75f;
							currency.SetXY(column* TILESIZE, row* TILESIZE);
							break;
						case 7:
							pointer = new Pointer();
							AddChild(pointer);
							pointer.SetXY(column * TILESIZE, row * TILESIZE);
							pointer.scale = 0.75f;
							break;
						case 8:
							ShopText shopText1 = new ShopText();
							AddChild(shopText1);
							shopText1.SetFrame(0);
							shopText1.SetXY((column * TILESIZE)+3, row * TILESIZE);
							break;
						case 9:
							ShopText shopText2 = new ShopText();
							AddChild(shopText2);
							shopText2.SetFrame(1);
							shopText2.SetXY((column * TILESIZE)+4, row * TILESIZE);
							break;
						case 10:
							ShopText shopText3 = new ShopText();
							AddChild(shopText3);
							shopText3.SetFrame(2);
							shopText3.SetXY(column * TILESIZE, row * TILESIZE);
							break;
						case 11:
							BorderPrices borderPrices = new BorderPrices();
							AddChild(borderPrices);
							borderPrices.SetXY((column * TILESIZE)+16, row * TILESIZE);
							break;
						case 12:
							start = new MenuText();
							AddChild(start);
							start.SetFrame(1);
							//start.TextType = "Start";
							start.SetXY((column * TILESIZE), row * TILESIZE);
							break;
						case 13:
							options = new MenuText();
							AddChild(options);
							options.SetFrame(2);
							//options.TextType = "options"
							options.SetXY((column * TILESIZE)-6, row * TILESIZE);
							break;
						case 14:
							exit = new MenuText();
							AddChild(exit);
							exit.SetFrame(4);
							exit.SetXY((column * TILESIZE)-1, row * TILESIZE);
							break;
						case 15:
							southPawControls = new SouthPaw();
							AddChild(southPawControls);
							southPawControls.SetXY((column * TILESIZE), row * TILESIZE);
							break;
						case 16:
							ShopText shopText4 = new ShopText();
							AddChild(shopText4);
							shopText4.SetFrame(3);
							shopText4.SetXY((column * TILESIZE)-4, row * TILESIZE);
							break;
						case 17:
							ExitPlate Exit = new ExitPlate();
							AddChild(Exit);
							Exit.SetXY(column * TILESIZE, row * TILESIZE);
							break;
						case 18:
							Counter playerCoinCounterTh = new Counter();
							AddChild(playerCoinCounterTh);
							playerCoinCounterTh.SetFrame(playerCoinCounterTh.Thousands);
							playerCoinCounterTh.CountPart = "Thousand";
							playerCoinCounterTh.scale = 0.5f;
							playerCoinCounterTh.SetXY(column * TILESIZE, row * TILESIZE);
							break;
						case 19:
							Counter playerCoinCounterHu = new Counter();
							AddChild(playerCoinCounterHu);
							playerCoinCounterHu.SetFrame(playerCoinCounterHu.Hundreds);
							playerCoinCounterHu.CountPart = "Hundred";
							playerCoinCounterHu.scale = 0.5f;
							playerCoinCounterHu.SetXY(column * TILESIZE, row * TILESIZE);
							break;
						case 20:
							Counter playerCoinCounterTe = new Counter();
							AddChild(playerCoinCounterTe);
							playerCoinCounterTe.SetFrame(playerCoinCounterTe.Tens);
							playerCoinCounterTe.CountPart = "Ten";
							playerCoinCounterTe.scale = 0.5f;
							playerCoinCounterTe.SetXY(column * TILESIZE, row * TILESIZE);
							break;
						case 21:
							Counter playerCoinCounterOn = new Counter();
							AddChild(playerCoinCounterOn);
							playerCoinCounterOn.SetFrame(playerCoinCounterOn.Ones);
							playerCoinCounterOn.CountPart = "One";
							playerCoinCounterOn.scale = 0.5f;
							playerCoinCounterOn.SetXY(column * TILESIZE, row * TILESIZE);
							break;
						case 22:
							Counter bulletCounterTh = new Counter();
							AddChild(bulletCounterTh);
							bulletCounterTh.SetFrame(bulletCounterTh.BulletThousands);
							bulletCounterTh.CountPart = "Thousand";
							bulletCounterTh.Currency = "bullets";
							bulletCounterTh.scale = 0.5f;
							bulletCounterTh.SetXY(column * TILESIZE, row * TILESIZE);
							break;
						case 23:
							Counter bulletCounterHu = new Counter();
							AddChild(bulletCounterHu);
							bulletCounterHu.SetFrame(bulletCounterHu.BulletHundreds);
							bulletCounterHu.CountPart = "Hundred";
							bulletCounterHu.Currency = "bullets";
							bulletCounterHu.scale = 0.5f;
							bulletCounterHu.SetXY(column * TILESIZE, row * TILESIZE);
							break;
						case 24:
							Counter bulletCounterTe = new Counter();
							AddChild(bulletCounterTe);
							bulletCounterTe.SetFrame(bulletCounterTe.BulletTens);
							bulletCounterTe.CountPart = "Ten";
							bulletCounterTe.Currency = "bullets";
							bulletCounterTe.scale = 0.5f;
							bulletCounterTe.SetXY(column * TILESIZE, row * TILESIZE);
							break;
						case 25:
							Counter bulletCounterOn = new Counter();
							AddChild(bulletCounterOn);
							bulletCounterOn.SetFrame(bulletCounterOn.BulletOnes);
							bulletCounterOn.CountPart = "One";
							bulletCounterOn.Currency = "bullets";
							bulletCounterOn.scale = 0.5f;
							bulletCounterOn.SetXY(column * TILESIZE, row * TILESIZE);
							break;
						case 26:
							Gun GunIcon = new Gun();
							AddChild(GunIcon);
							GunIcon.SetXY(column * TILESIZE, (row * TILESIZE) + 12);
							break;
					}
				}
			}
			/*player = new Player();
			AddChild(player);

			Wall wall;
			for (int i = 0; i < (game.width / 32) + 1; i++)
			{
				wall = new Wall(0 + i * 32, 0);
				AddChild(wall);
			}

			for (int i = 0; i < (game.width / 32) + 1; i++)
			{
				wall = new Wall(0 + i * 32, game.height);
				AddChild(wall);
			}

			for (int i = 0; i < (game.height / 32) + 1; i++)
			{
				wall = new Wall(0, 0 + i * 32);
				AddChild(wall);
			}

			for (int i = 0; i < (game.height / 32) + 1; i++)
			{
				wall = new Wall(game.width, 0 + i * 32);
				AddChild(wall);
			}*/
		}

		public void Update()
		{
			if (LvlStorage.HostileEnviroment(CurrentLevel))
			{
				if (EnemySpawnAmount > 0)
				{
					int EnemySpawnPointX = EnemySpawnRand.Next(-32, game.width + 32);
					int EnemySpawnPointY = EnemySpawnRand.Next(-32, game.height + 32);
					int EnemyStartFrame = EnemySpawnRand.Next(0, 4);
					newEnemy = new Enemy();
					AddChild(newEnemy);
					newEnemy.SetFrame(EnemyStartFrame);
					newEnemy.SetXY(EnemySpawnPointX, EnemySpawnPointY);
					if ((newEnemy.x > 0 & newEnemy.x < game.width) && (newEnemy.y > 0 & newEnemy.y < game.height))
					{
						newEnemy.Destroy();
						EnemySpawnAmount += 1;
					}

					if (player != null && player.HitTest(newEnemy))
					{
						newEnemy.Destroy();
						newEnemy = null;
						GC.Collect();
					}
					else {
						EnemySpawnAmount -= 1;
					}
				}
			}

			if (CurrentLevel == 101)
			{

			}

			if (Input.GetKeyDown(Key.P))
			{
				Enemy TestEnemy = new Enemy();
				AddChild(TestEnemy);
				TestEnemy.SetXY(320,320);
			}
		}
	}
}
