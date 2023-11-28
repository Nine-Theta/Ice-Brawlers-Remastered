using System;
namespace GXPEngine
{
	public class AntiGoal : Sprite
	{
		public AntiGoal() : base("assets/sprites/square.png")
		{
			SetOrigin(width / 2, height / 2);
			alpha = 0.0f;
			color = 0x00FF00;
		}

		void Update()
		{
			//Console.WriteLine(IsHittingWall());

			foreach (GameObject other in GetCollisions())
			{
				if (other is Player)
				{
					Player player = other as Player;
					//player.Impulse(player.SpeedX, player.SpeedY);
					//player.x -= player.SpeedX*2;
					//player.y -= player.SpeedY*2;

					//this.x -= this.SpeedX;
					//this.y -= this.SpeedY;
				}
			}

		}
	}
}
