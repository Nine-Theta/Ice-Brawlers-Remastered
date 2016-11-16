using System;
namespace GXPEngine
{
	public class ExitPlate : Sprite
	{
		public static bool ExitShop = false;

		public ExitPlate() : base("Exit.png")
		{
			SetOrigin(width / 2, height / 2);
			scale = 0.5f;
		}

		private void OnCollision(GameObject other)
		{
			if (other is Player)
			{
				ExitShop = true;
			}

			//Console.WriteLine("{0}", ExitShop);
		}
	}
}
