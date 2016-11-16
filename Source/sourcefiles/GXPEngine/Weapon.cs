using System;
namespace GXPEngine
{
	public class Weapon : AnimationSprite
	{
		public static bool Firing = false;
		public static int Bullets = 50;
		public static float WeaponX;
		public static float WeaponY;

		bool WeaponInputLeft = false;
		bool WeaponInputRight = false;
		bool WeaponInputUp = false;
		bool WeaponInputDown = false;

		public Weapon() : base("Blaster.png", 4, 1)
		{
			SetOrigin(width / 2, height / 2);
			scale = 0.5f;
			color = 0xFFFFC8;
		}

		public void Update()
		{
			SouthCheck();

			if (WeaponInputLeft & !WeaponInputRight & !WeaponInputUp & !WeaponInputDown)
			{
				this.SetFrame(1);
				x = 0f;
				y = 64f;
			}

			if (WeaponInputRight & !WeaponInputLeft & !WeaponInputUp & !WeaponInputDown)
			{
				this.SetFrame(3);
				x = 0f;
				y = -64f;
			}

			if (WeaponInputUp & !WeaponInputLeft & !WeaponInputRight & !WeaponInputDown)
			{
				this.SetFrame(0);
				x = 64f;
				y = 0f;
			}

			if (WeaponInputDown & !WeaponInputLeft & !WeaponInputUp & !WeaponInputRight)
			{
				this.SetFrame(2);
				x = -64f;
				y = 0f;
			}

			WeaponX = x;
			WeaponY = y;

			FireBullet();
			//((MyGame)game).currentLevel.player.x

		}

		private void SouthCheck()
		{

			if (((MyGame)game).SouthPaw == false)
			{
				if (Input.GetKeyDown(Key.RIGHT))
				{
					WeaponInputLeft = true;
				}
				else {
					WeaponInputLeft = false;
				}

				if (Input.GetKeyDown(Key.LEFT))
				{
					WeaponInputRight = true;
				}
				else {
					WeaponInputRight = false;
				}

				if (Input.GetKeyDown(Key.UP))
				{
					WeaponInputUp = true;
				}
				else {
					WeaponInputUp = false;
				}

				if (Input.GetKeyDown(Key.DOWN))
				{
					WeaponInputDown = true;
				}
				else {
					WeaponInputDown = false;
				}
			}

			if (((MyGame)game).SouthPaw == true)
			{
				if (Input.GetKeyDown(Key.D))
				{
					WeaponInputLeft = true;
				}
				else {
					WeaponInputLeft = false;
				}

				if (Input.GetKeyDown(Key.A))
				{
					WeaponInputRight = true;
				}
				else {
					WeaponInputRight = false;
				}

				if (Input.GetKeyDown(Key.W))
				{
					WeaponInputUp = true;
				}
				else {
					WeaponInputUp = false;
				}

				if (Input.GetKeyDown(Key.S))
				{
					WeaponInputDown = true;
				}
				else {
					WeaponInputDown = false;
				}
			}
		}

		private void FireBullet()
		{
			if (Bullets > 0)
			{
				if (((MyGame)game).currentLevel.LvlStorage.HostileEnviroment(Level.CurrentLevel))
				{
					if (WeaponInputLeft & !WeaponInputRight & !WeaponInputUp & !WeaponInputDown)
					{
						Firing = true;
						Bullets -= 1;
						Bullet newRightBullet = new Bullet();
						MyGame myGame = (MyGame)game;
						if (myGame != null)
						{
							myGame.currentLevel.AddChild(newRightBullet);
							newRightBullet.SetXY(Player.playerX + (WeaponX / 2), Player.playerY + (WeaponY / 2));
							newRightBullet.RightBullet = true;
						}
					}
					else {
						Firing = false;
					}

					if (WeaponInputRight & !WeaponInputLeft & !WeaponInputUp & !WeaponInputDown)
					{
						Firing = true;
						Bullets -= 1;
						Bullet newLeftBullet = new Bullet();
						MyGame myGame = (MyGame)game;
						if (myGame != null)
						{
							myGame.currentLevel.AddChild(newLeftBullet);
							newLeftBullet.SetXY(Player.playerX + (WeaponX / 2), Player.playerY + (WeaponY / 2));
							newLeftBullet.LeftBullet = true;
						}
					}
					else {
						Firing = false;
					}

					if (WeaponInputUp & !WeaponInputLeft & !WeaponInputRight & !WeaponInputDown)
					{
						Firing = true;
						Bullets -= 1;
						Bullet newUpBullet = new Bullet();
						MyGame myGame = (MyGame)game;
						if (myGame != null)
						{
							myGame.currentLevel.AddChild(newUpBullet);
							newUpBullet.SetXY(Player.playerX + (WeaponX / 2), Player.playerY + (WeaponY / 2));
							newUpBullet.UpBullet = true;
						}
					}
					else {
						Firing = false;
					}

					if (WeaponInputDown & !WeaponInputLeft & !WeaponInputUp & !WeaponInputRight)
					{
						Firing = true;
						Bullets -= 1;
						Bullet newDownBullet = new Bullet();
						MyGame myGame = (MyGame)game;
						if (myGame != null)
						{
							myGame.currentLevel.AddChild(newDownBullet);
							newDownBullet.SetXY(Player.playerX + (WeaponX / 2), Player.playerY + (WeaponY / 2));
							newDownBullet.DownBullet = true;
						}
					}
					else {
						Firing = false;
					}
				}
			}
		}
	}
}

