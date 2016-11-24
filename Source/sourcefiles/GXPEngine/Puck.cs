using System;
namespace GXPEngine
{
	public class Puck : Sprite
	{
		float SpeedX;
		float SpeedY;
		float Friction = 0.985f;
		float speedLimit = 20.0f;

		public Puck(int rColour) : base("testpuck.png")
		{
			SetOrigin(width / 2, height / 2);

			if (rColour == 0x505050)
			{
				
			}

			/*if (rColour == 0xE000FF)
			{
				((MyGame)game).extraPuckReflection = new Sprite("circle.png");
				((MyGame)game).AddChildAt(((MyGame)game).extraPuckReflection, 1);
				((MyGame)game).extraPuckReflection.scale = 0.55f;
				((MyGame)game).extraPuckReflection.alpha = 0.25f;
				((MyGame)game).extraPuckReflection.color = 0xE000FF;
			}*/
		}

		public void Reset()
		{
			if (this.color == 0xE000FF)
			{
				this.Destroy();
			}
			else {
				SpeedX = 0.0f;
				SpeedY = 0.0f;
			}

		}

		public void Impulse(float ImpulseX, float ImpulseY)
		{
			SpeedX += ImpulseX;
			SpeedY += ImpulseY;
		}

		void Update()
		{
			if (this.x - width / 2 < 64.0f - (this.y * 0.083f)){
				SpeedX = Mathf.Abs(SpeedX);
			}
			if (this.x + width / 2 > game.width - 64.0f + (this.y * 0.083f)){
				SpeedX = -Mathf.Abs(SpeedX);
			}
			if (this.y - height / 2 < 0 + (game.height * 0.26f)){
				SpeedY = Mathf.Abs(SpeedY);
			}
			if (this.y + height / 2 > game.height){
				SpeedY = -Mathf.Abs(SpeedY);
			}

			if (this.x - width / 2 < 0 - game.width){
				this.Destroy();
			}
			if (this.x + width / 2 > game.width * 2){
				this.Destroy();
			}
			if (this.y - height / 2 < 0 - game.height){
				this.Destroy();
			}
			if (this.y + height / 2 > game.height * 2){
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

			if (SpeedX < 0.1f && SpeedX > -0.1f)
			{
				SpeedX = 0.0f;
			}

			if (SpeedY < 0.1f && SpeedY > -0.1f)
			{
				SpeedY = 0.0f;
			}

			if (Input.GetKeyDown(Key.R))
			{
				if (this.color == 0xE000FF)
				{
					this.Destroy();
				}
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

				if (other is AntiGoal)
				{
					AntiGoal notGoal = other as AntiGoal;
					hit = true;
					this.Impulse(this.SpeedX * 0.99f, this.SpeedY * 0.99f);
					this.SpeedX -= this.SpeedX * 1.5f;
					this.SpeedY -= this.SpeedY * 1.5f;
				}

				if (other is Goal)
				{
					Goal goal = other as Goal;
					if (goal.sideColour == "blue")
					{
						((MyGame)game).scoreYell.Play();
						((MyGame)game).ScoreRed += 1;
						((MyGame)game).Resetti();
						((MyGame)game).loader.redCounter.NextFrame();
						((MyGame)game).scoredRed = true;
						((MyGame)game).shakeCounter = 5;
					}
					if (goal.sideColour == "red")
					{
						((MyGame)game).scoreYell.Play();
						((MyGame)game).ScoreBlue += 1;
						((MyGame)game).Resetti();
						((MyGame)game).loader.blueCounter.NextFrame();
						((MyGame)game).scoredBlue = true;
						((MyGame)game).shakeCounter = 5;
					}

					if (this.color == 0xE000FF)
					{
						this.Destroy();
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
