using System;
namespace GXPEngine
{
	public class Pointer : AnimationSprite
	{
		public static bool BoughtBorderUpgrade = false;
		public static bool ExitShopInterface = false;
		private float minY;
		private float maxY;

		public Pointer() : base("Enemy.png", 4, 1)
		{
			SetOrigin(width / 2, height / 2);
			scale = 0.5f;
			rotation = 0.0f;

			if (Level.CurrentLevel == 101)
			{
				minY = 160.0f;
				maxY = 672.0f;
			}

			if (Level.CurrentLevel == 102)
			{
				minY = 512.0f;
				maxY = 640.0f;
			}

			if (Level.CurrentLevel == 103)
			{
				minY = 128.0f;
				maxY = 256.0f;
			}

		}

		private void Update()
		{
			this.SetFrame(this._currentFrame + 1);

			ExitShopInterface = false;

			if (this.y > minY)
			{
				if (Input.GetKeyDown(Key.UP))
				{
					this.y -= 64.0f;
				}
			}
			if (this.y < maxY)
			{
				if (Input.GetKeyDown(Key.DOWN))
				{
					this.y += 64.0f;
				}
			}

			if (Level.CurrentLevel == 101)
			{
				if ((Input.GetKeyDown(Key.ENTER) || Input.GetKeyDown(Key.SPACE)))
				{
					if (this.y == 160.0f && Player.PlayerMoney >= 5)
					{
						Player.PlayerMoney -= 5;
						Weapon.Bullets += 10;
					}

					if (this.y == 224.0f && Player.PlayerMoney >= 40)
					{
						Player.PlayerMoney -= 40;
						Weapon.Bullets += 100;
					}

					if (this.y == 288.0f && Player.PlayerMoney >= BorderPrices.BorderPrice(Level.NextLevel))
					{
						Player.PlayerMoney -= BorderPrices.BorderPrice(Level.NextLevel);
						Level.NextLevel += 1;
						BoughtBorderUpgrade = true;
					}

					if (this.y == 672.0f)
					{
						ExitShopInterface = true;
					}
				}
				//Console.WriteLine("{0} {1} {2} {3}", Player.PlayerMoney, Weapon.Bullets, BorderPrices.BorderPrice(Level.NextLevel), Level.NextLevel );
			}

		}
	}
}
