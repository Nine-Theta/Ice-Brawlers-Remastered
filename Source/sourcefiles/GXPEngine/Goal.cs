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
			scaleY = 0.5f;



			if (rSide == "blue")
			{
				this.color = 0x4040FF;
				this.sideColour = "blue";
				AntiGoal notBlue = new AntiGoal();
				AddChild(notBlue);
				notBlue.SetXY(20, 0);
				notBlue.scaleX = 1.5f;
				notBlue.scaleY = 1.5f;

				AnimationSprite blueGoalSprite = new AnimationSprite("testgoals.png", 2,1);
				blueGoalSprite.SetOrigin(blueGoalSprite.width / 2, blueGoalSprite.height / 2);
				notBlue.AddChild(blueGoalSprite);
				blueGoalSprite.currentFrame = 1;
				blueGoalSprite.scaleX = 0.75f;
				blueGoalSprite.scaleY = 0.55f;
				blueGoalSprite.x -= 5.0f;
				blueGoalSprite.y -= 13.0f;
			}
			if (rSide == "red")
			{
				this.color = 0xFF4040;
				this.sideColour = "red";
				AntiGoal notRed = new AntiGoal();
				AddChild(notRed);
				notRed.SetXY(-20, 0);
				notRed.scaleX = 1.5f;
				notRed.scaleY = 1.5f;

				AnimationSprite redGoalSprite = new AnimationSprite("testgoals.png", 2, 1);
				redGoalSprite.SetOrigin(redGoalSprite.width / 2, redGoalSprite.height / 2);
				notRed.AddChild(redGoalSprite);
				redGoalSprite.currentFrame = 0;
				redGoalSprite.scaleX = 0.75f;
				redGoalSprite.scaleY = 0.55f;
				redGoalSprite.x -= 5.0f;
				redGoalSprite.y -= 13.0f;
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

			//Console.WriteLine(this.y);

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
