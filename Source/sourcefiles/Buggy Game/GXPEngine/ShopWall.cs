using System;
namespace GXPEngine
{
	public class ShopWall : Sprite
	{
		public ShopWall() : base("checkers.png")
		{
			SetOrigin(width / 2, height / 2);
			scale = 0.5f;


			this.x = x;
			this.y = y;
		}
	}
}
