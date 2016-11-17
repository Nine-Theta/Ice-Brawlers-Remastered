using System;
using GXPEngine;

namespace GXPEngine
{
	public class Goal : Sprite
	{
		float SpeedX;
		float SpeedY;
		float Friction = 0.8f;

		public string sideColour = "null";

		public Goal(string rSide) : base("checkers.png")
		{
			SetOrigin(width / 2, height / 2);
			alpha = 0.5f;

			if (rSide == "blue")
			{
				this.color = 0x4040FF;
				this.sideColour = "blue";
			}
			if (rSide == "red")
			{
				this.color = 0xFF4040;
				this.sideColour = "red";
			}
		}

		public void Reset()
		{
			SpeedX = 0.0f;
			SpeedY = 0.0f;
		}

		public void Impulse(float ImpulseX, float ImpulseY)
		{
			SpeedX += ImpulseX;
			SpeedY += ImpulseY;
		}

		public bool IsHittingWall()
		{
			bool wallHit = false;
			if ((this.x - width / 2 <= 0) || (this.x + width / 2 >= game.width) || (this.y - height / 2 <= 0) || (this.y + height / 2 >= game.height))
			{
				wallHit = true;
			}
			return wallHit;
		}

		void Update()
		{
			if (this.x - width / 2 <= 0)
			{
				SpeedX = Mathf.Abs(1.0f);
			}
			if (this.x + width / 2 >= game.width)
			{
				SpeedX = - Mathf.Abs(1.0f);
			}
			if (this.y - height / 2 <= 0)
			{
				SpeedY = Mathf.Abs(1.0f);
			}
			if (this.y + height / 2 >= game.height)
			{
				SpeedY = - Mathf.Abs(1.0f);
			}

			x += SpeedX;
			y += SpeedY;

			SpeedX *= Friction;
			SpeedY *= Friction;

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
