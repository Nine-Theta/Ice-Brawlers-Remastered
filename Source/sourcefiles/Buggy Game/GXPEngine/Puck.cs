using System;
namespace GXPEngine
{
	public class Puck : Sprite
	{
		float SpeedX;
		float SpeedY;
		float Friction = 0.981f;

		public Puck() : base("circle.png")
		{
			scale = 0.5f;
			SetOrigin(width / 2, height / 2);

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

			x += SpeedX;
			if (CheckCollisions()){
				x -= SpeedX;
				SpeedX *= -1;
			}

			y += SpeedY;
			x += SpeedX;
			if (CheckCollisions()){
				y -= SpeedY;
				SpeedY *= -1;
			}

			SpeedX *= Friction;
			SpeedY *= Friction;

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
