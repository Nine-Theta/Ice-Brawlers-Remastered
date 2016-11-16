using System;
using GXPEngine;

namespace GXPEngine
{
	public class Player : Sprite
	{
		float SpeedX, SpeedY;
		float SpeedMultiplier = 0.4f;
		float Friction = 0.97f;


		int left, right, up, down;

		public Player(int rLeft, int rRight, int rUp, int rDown) : base("colors.png")
		{
			SetOrigin(width / 2, height / 2);

			this.left = rLeft;
			this.right = rRight;
			this.up = rUp;
			this.down = rDown;
		}

		public void Impulse(float ImpulseX, float ImpulseY)
		{
			SpeedX = ImpulseX + 1.0f;
			SpeedY = ImpulseY + 1.0f;
		}

		void Update()
		{
			if (Input.GetKey(left))
			{ SpeedX -= SpeedMultiplier; }
			if (Input.GetKey(right))
			{ SpeedX += SpeedMultiplier; }
			if (Input.GetKey(up))
			{ SpeedY -= SpeedMultiplier; }
			if (Input.GetKey(down))
			{ SpeedY += SpeedMultiplier; }

			if (this.x - width / 2 <= 0){
				SpeedX = 1.0f + Mathf.Abs(SpeedX / 2.0f);
			}
			if (this.x + width/2 >= game.width){
				SpeedX = -1.0f -Mathf.Abs(SpeedX/2.0f);
			}
			if (this.y -height/2 <= 0){
				SpeedY = 1.0f + Mathf.Abs(SpeedY/2.0f);
			}
			if (this.y + height/2 >= game.height){
				SpeedY = -1.0f -Mathf.Abs(SpeedY/2.0f);
			}

			if (SpeedX > 12.0f){
				SpeedX = 12.0f;
			}
			if (SpeedX < -12.0f){
				SpeedX = -12.0f;
			}

			if (SpeedY > 12.0f){
				SpeedY = 12.0f;
			}

			if (SpeedY < -12.0f){
				SpeedY = -12.0f;
			}

			x += SpeedX;
			y += SpeedY;

			SpeedX *= Friction;
			SpeedY *= Friction;

			if (SpeedX < 0.1f && SpeedX > -0.1f){
				SpeedX = 0.0f;
			}

			if (SpeedY < 0.1f && SpeedY > -0.1f){
				SpeedY = 0.0f;
			}

			foreach (GameObject other in GetCollisions())
			{
				if (other is Puck)
				{
					Puck puck = other as Puck;
					puck.Impulse(this.SpeedX - this.Friction, this.SpeedY - this.Friction);
					puck.x += this.SpeedX;
					puck.y += this.SpeedY;
				}

				if (other is Player)
				{
					Player player = other as Player;
					player.Impulse(this.SpeedX, this.SpeedY);
					//player.x += this.SpeedX;
					//player.y += this.SpeedY;
					this.x -= SpeedX;
					this.y -= SpeedY;
					//this.Impulse(-player.SpeedX, -player.SpeedY);

				}
			}

			Console.WriteLine("{0} {1} {2}", SpeedX, SpeedY, Math.Pow(2.0, 3.0));
		}
	}
}
