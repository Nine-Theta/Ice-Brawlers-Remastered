using System;
namespace GXPEngine
{
	public class Puck : Sprite
	{
		float SpeedX;
		float SpeedY;
		float Friction = 0.981f;
		float speedLimit = 20.0f;

		public Puck() : base("circle.png")
		{
			scale = 0.5f;
			SetOrigin(width / 2, height / 2);

		}

		public void Reset()
		{
			SpeedX = 0.0f;
			SpeedY = 0.0f;
			SetXY(game.width / 2, game.height / 2);
		}

		public void Impulse(float ImpulseX, float ImpulseY)
		{
			SpeedX += ImpulseX;
			SpeedY += ImpulseY;
		}

		void Update()
		{
			if (this.x - width / 2 < 0){
				SpeedX = Mathf.Abs(SpeedX);
			}
			if (this.x + width / 2 > game.width){
				SpeedX = -Mathf.Abs(SpeedX);
			}
			if (this.y - height / 2 < 0){
				SpeedY = Mathf.Abs(SpeedY);
			}
			if (this.y + height / 2 > game.height){
				SpeedY = -Mathf.Abs(SpeedY);
			}

			if (this.x - width / 2 < 0-game.width)
			{
				this.Destroy();
			}
			if (this.x + width / 2 > game.width*2)
			{
				this.Destroy();
			}
			if (this.y - height / 2 < 0 - game.height)
			{
				this.Destroy();
			}
			if (this.y + height / 2 > game.height*2)
			{
				this.Destroy();
			}

			x += SpeedX;
			if (CheckCollisions()){
				x -= SpeedX;
				SpeedX *= -1;
			}

			y += SpeedY;
			if (CheckCollisions()){
				y -= SpeedY;
				SpeedY *= -1;
			}

			SpeedX *= Friction;
			SpeedY *= Friction;

			if (SpeedX > speedLimit)
			{
				SpeedX = speedLimit;
			}
			if (SpeedX < -speedLimit)
			{
				SpeedX = -speedLimit;
			}

			if (SpeedY > speedLimit)
			{
				SpeedY = speedLimit;
			}

			if (SpeedY < -speedLimit)
			{
				SpeedY = -speedLimit;
			}

		}

		bool CheckCollisions()
		{
			bool hit = false;
			foreach (GameObject other in GetCollisions())
			{
				if (other is Player)
				{
					Player player = other as Player;
					hit = true;
				}
				if (other is Puck)
				{
					Puck puck = other as Puck;
					hit = true;
					puck.Impulse(this.SpeedX, this.SpeedY);
					puck.x += this.SpeedX;
					puck.y += this.SpeedY;
				}
				if (other is Goal)
				{
					Goal goal = other as Goal;
					if (goal.sideColour == "blue")
					{
						((MyGame)game).ScoreBlue += 1;
						((MyGame)game).Resetti();
						((MyGame)game).blueBoard.NextFrame();
					}
					if (goal.sideColour == "red")
					{
						((MyGame)game).ScoreRed += 1;
						((MyGame)game).Resetti();
						((MyGame)game).redBoard.NextFrame();
					}
				}
			}
			return hit;
		}

		/*
		bool CheckCollisions()
		{
			bool hit = false;
			foreach (GameObject other in GetCollisions())
			{
				if (other is Player)
				{
					Player player = other as Player;
					hit = true;
				}
			}
			return hit;
		}*/
	}
}
