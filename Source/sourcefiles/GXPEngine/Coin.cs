using System;
using GXPEngine;

namespace GXPEngine
{
	public class Coin : AnimationSprite
	{
		public Coin() : base("SpaceEuro.png", 3, 1)
		{
			SetOrigin(width / 2, height / 2);
			scale = 0.5f; 
		}

		public void OnCollision(GameObject other)
		{
			if (other is Player)
			{
				Player.PlayerMoney += 1;
				this.Destroy();
			}
		}
	}
}
