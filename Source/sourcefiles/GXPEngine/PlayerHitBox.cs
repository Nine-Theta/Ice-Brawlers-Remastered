using System;
using GXPEngine;
namespace GXPEngine
{
	public class PlayerHitBox : Sprite
	{
		private Player player;
		public bool IsHittingWall { get; private set; }
		public bool IsHittingShop { get; private set; }

		public PlayerHitBox(Player player) : base ("square.png")
		{
			SetOrigin(width / 2, height / 2);
			scale = 0.25f;
			alpha = 0.0f;
			color = 0x64FF00;

			this.player = player;

			IsHittingWall = false;
			IsHittingShop = false;
		}

		public void Update()
		{
			//Console.WriteLine("On Update Time: " + Time.time);
			IsHittingWall = false;
		}

		/// <summary>
		/// What happens when the player collides with a wall
		/// </summary>
		public void OnCollision(GameObject other)
		{
			if (other is Wall)
			{
				//Console.WriteLine("On Collision Time: " + Time.time);
				IsHittingWall = other is Wall;
			}
			if (other is ShopKeep)
			{
				IsHittingWall = other is ShopKeep;
				IsHittingShop = other is ShopKeep;
			}
		}
	}
}
