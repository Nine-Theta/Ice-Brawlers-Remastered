using System;
namespace GXPEngine
{
	public class ShopText : AnimationSprite
	{
		public ShopText() : base("ShopText.png", 4, 1)
		{
			SetOrigin(0.0f, height / 2);
		}
	}
}
