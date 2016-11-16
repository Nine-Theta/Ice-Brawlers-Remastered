using System;
namespace GXPEngine
{
	public class Counter : AnimationSprite
	{
		public int Thousands = 0;
		public int Hundreds = 0;
		public int Tens = 0;
		public int Ones = 0;
		public int BulletThousands = 0;
		public int BulletHundreds = 0;
		public int BulletTens = 0;
		public int BulletOnes = 0;
		public string CountPart = "zeroes";
		public string Currency = "coins";

		public Counter() : base("Counter.png", 10, 1)
		{
			SetOrigin(0.0f, height / 2);

			/*PlayerCoins = Player.PlayerMoney;
			for (int Th = PlayerCoins; Th >= 1000; Th -= 1000)
			{
				PlayerCoins -= 1000;
				Thousands += 1;
			}

			for (int Hu = PlayerCoins; Hu >= 100; Hu -= 100)
			{
				PlayerCoins -= 100;
				Hundreds += 1;
			}

			for (int Te = PlayerCoins; Te >= 10; Te -= 10)
			{
				PlayerCoins -= 10;
				Tens += 1;
			}

			for (int On = PlayerCoins; On >= 1; On -= 1)
			{
				PlayerCoins -= 1;
				Ones += 1;
			}*/
		}

		private void Update()
		{
			if (this.Currency == "coins")
			{
				if (Player.PlayerMoney > 1000)
				{
					Thousands = Player.PlayerMoney / 1000;
				}
				else {
					Thousands = 0;
				}

				if (Player.PlayerMoney >= 100)
				{
					Hundreds = (Player.PlayerMoney - (Thousands * 1000)) / 100;
					Console.WriteLine("Is this called?");
				}
				else {
					Hundreds = 0;
				}

				if (Player.PlayerMoney >= 10)
				{
					Tens = ((Player.PlayerMoney - (Thousands * 1000)) - (Hundreds * 100)) / 10;
				}
				else
				{
					Tens = 0;
				}

				if (Player.PlayerMoney >= 1)
				{
					Ones = ((Player.PlayerMoney - (Thousands * 1000)) - (Hundreds * 100)) - (Tens * 10);
				}
				else {
					Ones = 0;
				}

				if (CountPart == "Thousand")
				{
					this.SetFrame(Thousands);
				}

				if (CountPart == "Hundred")
				{
					this.SetFrame(Hundreds);
				}

				if (CountPart == "Ten")
				{
					this.SetFrame(Tens);
				}

				if (CountPart == "One")
				{
					this.SetFrame(Ones);
				}
			}

			if (this.Currency == "bullets")
			{
				if (Weapon.Bullets > 1000)
				{
					BulletThousands = Weapon.Bullets / 1000;
				}
				else {
					BulletThousands = 0;
				}

				if (Weapon.Bullets >= 100)
				{
					BulletHundreds = (Weapon.Bullets - (BulletThousands * 1000)) / 100;
					//Console.WriteLine("Is this called?");
				}
				else {
					BulletHundreds = 0;
				}

				if (Weapon.Bullets >= 10)
				{
					BulletTens = ((Weapon.Bullets - (BulletThousands * 1000)) - (BulletHundreds * 100)) / 10;
				}
				else
				{
					BulletTens = 0;
				}

				if (Weapon.Bullets >= 1)
				{
					BulletOnes = ((Weapon.Bullets - (BulletThousands * 1000)) - (BulletHundreds * 100)) - (BulletTens * 10);
				}
				else {
					BulletOnes = 0;
				}

				if (CountPart == "Thousand")
				{
					this.SetFrame(BulletThousands);
				}

				if (CountPart == "Hundred")
				{
					this.SetFrame(BulletHundreds);
				}

				if (CountPart == "Ten")
				{
					this.SetFrame(BulletTens);
				}

				if (CountPart == "One")
				{
					this.SetFrame(BulletOnes);
				}
			}

			Console.WriteLine("{0} {1} {2} {3} {4}", Player.PlayerMoney, Thousands, Hundreds, Tens, Ones);
		}
	}
}
