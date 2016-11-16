using System;
namespace GXPEngine
{
	public class BorderPrices : AnimationSprite
	{
		public BorderPrices() : base("PriceNumbers.png", 12, 1)
		{
			SetOrigin(0.0f, (height - 8) / 2);
		}

		public static int BorderPrice(int pNextLevel)
		{
			switch (pNextLevel)
			{
				case 1:
					return 1;
				case 2:
					return 1;
				case 3:
					return 2;
				case 4:
					return 3;
				case 5:
					return 5;
				case 6:
					return 8;
				case 7:
					return 13;
				case 8:
					return 21;
				case 9:
					return 34;
				case 10:
					return 55;
				case 11:
					return 89;
				case 12:
					return 134;
				default:
					return 4;
			}
		}
		private void Update()
		{
			this.SetFrame(Level.NextLevel - 1);
		}
	}
}
