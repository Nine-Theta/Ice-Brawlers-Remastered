using System;
using GXPEngine;

namespace GXPEngine
{
	public class Player : Sprite
	{
		public float SpeedX, SpeedY;
		public float SpeedMultiplierX = 0.4f;
		public float SpeedMultiplierY = 0.4f;
		float Friction = 0.8f;
		public float speedLimit = 12.0f;
		public float ForceMultiplier = 1.0f;
		public string colour;
		//bool hittingGoal = false;
		public bool touchingGoal = false;
		public bool InversedControls = false;
		public bool hasPowerUp = false;
		public float StartX, StartY;
		Random puckRange = new Random();

		int timeWaitBlue;
		int timeWaitRed;

		AnimationSprite playerBlue;
		AnimationSprite playerRed;
		AnimationSprite reflectionBlue;
		AnimationSprite reflectionRed;

		int left, right, up, down;

		public Player(int rLeft, int rRight, int rUp, int rDown, string rColour) : base("colors.png")
		{
			SetOrigin(width / 2, height / 2);

			this.left = rLeft;
			this.right = rRight;
			this.up = rUp;
			this.down = rDown;

			colour = rColour;

			playerBlue = new AnimationSprite("BluePlayerAnim.png", 4, 5);
			playerRed = new AnimationSprite("RedPlayerAnim.png", 4, 5);
			reflectionBlue = new AnimationSprite("BluePlayerAnim.png", 4, 5);
			reflectionRed = new AnimationSprite("RedPlayerAnim.png", 4, 5);

			if (colour == "blue")
			{
				this.AddChildAt(playerBlue, 999999);
				playerBlue.SetOrigin(playerBlue.width / 2, playerBlue.height / 2);
				playerBlue.SetXY(-8, -32);

				this.AddChild(reflectionBlue);
				reflectionBlue.SetOrigin(reflectionBlue.width / 2, reflectionBlue.height / 2);
				reflectionBlue.SetXY(-8, 72);
				reflectionBlue.scaleY = -0.75f;
				reflectionBlue.alpha = 0.125f;
			}
			if (colour == "red")
			{
				this.AddChildAt(playerRed, 9999999);
				playerRed.SetOrigin(playerRed.width / 2, playerRed.height / 2);
				playerRed.SetXY(-8, -32);
				playerRed.scaleX = -1.0f;

				this.AddChild(reflectionRed);
				reflectionRed.SetOrigin(reflectionRed.width / 2, reflectionRed.height / 2);
				reflectionRed.SetXY(-8, 72);
				reflectionRed.scaleY = -0.75f;
				reflectionRed.alpha = 0.125f;

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

		void Update()
		{

			if (InversedControls == false)
			{
				if (Input.GetKey(left))
				{ SpeedX -= SpeedMultiplierX; }
				if (Input.GetKey(right))
				{ SpeedX += SpeedMultiplierX; }
				if (Input.GetKey(up))
				{ SpeedY -= SpeedMultiplierY; }
				if (Input.GetKey(down))
				{ SpeedY += SpeedMultiplierY; }
			}
			else {
				if (Input.GetKey(right))
				{ SpeedX -= SpeedMultiplierX; }
				if (Input.GetKey(left))
				{ SpeedX += SpeedMultiplierX; }
				if (Input.GetKey(down))
				{ SpeedY -= SpeedMultiplierY; }
				if (Input.GetKey(up))
				{ SpeedY += SpeedMultiplierY; }
			}

			if (this.x - width / 2 <= 64.0f - (this.y * 0.091f))
			{
				SpeedX = 1.0f + Mathf.Abs(SpeedX / 2.0f);
			}
			if (this.x + width / 2 >= game.width - 64.0f + (this.y * 0.091f))
			{
				SpeedX = -1.0f - Mathf.Abs(SpeedX / 2.0f);
			}
			if (this.y - height / 2 <= 0 + (game.height * 0.20f))
			{
				SpeedY = 1.0f + Mathf.Abs(SpeedY / 2.0f);
			}
			if (this.y + height / 2 >= game.height)
			{
				SpeedY = -1.0f - Mathf.Abs(SpeedY / 2.0f);
			}

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

			x += SpeedX;
			y += SpeedY;

			SpeedX *= Friction;
			SpeedY *= Friction;

			if (SpeedX < 0.1f && SpeedX > -0.1f)
			{
				SpeedX = 0.0f;
			}

			if (SpeedY < 0.1f && SpeedY > -0.1f)
			{
				SpeedY = 0.0f;
			}

			if (colour == "blue")
			{
				if (SpeedX > 3.0f)
				{
					if (playerBlue.currentFrame < 11)
						playerBlue.SetFrame(12);
					
					playerBlue.scaleX = -1.0f;
					reflectionBlue.scaleX = -1.0f;
					if (timeWaitBlue < Time.now / 10)
					{
						playerBlue.NextFrame();
						timeWaitBlue = (Time.now / 10) + 15;
						if (playerBlue.currentFrame == 19)
							playerBlue.currentFrame = 12;
					}
				}
				if (SpeedX < -3.0f || SpeedY > 3.0f || SpeedY < -3.0f)
					{
					if (playerBlue.currentFrame < 11)
						playerBlue.SetFrame(12);
					
						playerBlue.scaleX = 1.0f;
						reflectionBlue.scaleX = 1.0f;
						if (timeWaitBlue < Time.now / 10)
						{
							playerBlue.NextFrame();
							timeWaitBlue = (Time.now / 10) + 15;
							if (playerBlue.currentFrame == 19)
								playerBlue.currentFrame = 12;
						}
					}
				if (SpeedX > -3.0f && SpeedX < 3.0f && SpeedY > -3.0f && SpeedY < 3.0f)
				{
					if (playerBlue.currentFrame > 11)
						playerBlue.SetFrame(0);
					
					if (timeWaitBlue < Time.now / 10)
					{
						playerBlue.NextFrame();
						timeWaitBlue = (Time.now / 10) + 15;
						if (playerBlue.currentFrame == 10)
							playerBlue.currentFrame = 0;
					}
				}
				if (((MyGame)game).scoredBlue == true)
				{
					playerBlue.SetFrame(11);
				}

				reflectionBlue.currentFrame = playerBlue.currentFrame;
			}

			if (colour == "red")
			{
				if (SpeedX > 3.0f)
				{
					if (playerRed.currentFrame < 11)
						playerRed.SetFrame(12);

					playerRed.scaleX = -1.0f;
					reflectionRed.scaleX = -1.0f;
					if (timeWaitRed < Time.now / 10)
					{
						playerRed.NextFrame();
						timeWaitRed = (Time.now / 10) + 15;
						if (playerRed.currentFrame == 19)
							playerRed.currentFrame = 12;
					}
				}
				if (SpeedX < -3.0f || SpeedY > 3.0f || SpeedY < -3.0f)
				{
					if (playerRed.currentFrame < 11)
						playerRed.SetFrame(12);

					playerRed.scaleX = 1.0f;
					reflectionRed.scaleX = 1.0f;
					if (timeWaitRed < Time.now / 10)
					{
						playerRed.NextFrame();
						timeWaitRed = (Time.now / 10) + 15;
						if (playerRed.currentFrame == 19)
							playerRed.currentFrame = 12;
					}
				}
				if (SpeedX > -3.0f && SpeedX < 3.0f && SpeedY > -3.0f && SpeedY < 3.0f)
				{
					if (playerRed.currentFrame > 11)
						playerRed.SetFrame(0);

					if (timeWaitRed < Time.now / 10)
					{
						playerRed.NextFrame();
						timeWaitRed = (Time.now / 10) + 15;
						if (playerRed.currentFrame == 10)
							playerRed.currentFrame = 0;
					}
				}
				if (((MyGame)game).scoredRed == true)
				{
					playerRed.SetFrame(11);
				}
				reflectionRed.currentFrame = playerRed.currentFrame;
			}

			if (this.SpeedMultiplierX < 0.4f)
				this.SpeedMultiplierX = 0.4f;
			if (this.SpeedMultiplierY < 0.4f)
				this.SpeedMultiplierY = 0.4f;
			Friction = 0.97f;
			this.touchingGoal = false;

			//Console.WriteLine(SpeedMultiplierX);

			foreach (GameObject other in GetCollisions())
			{
				if (other is Puck)
				{
					//int _puckRange = puckRange.Next(-2, 2);

					Puck puck = other as Puck;
					puck.Impulse((this.SpeedX * ForceMultiplier) - this.Friction, (this.SpeedY * ForceMultiplier) - this.Friction);
					puck.x += this.SpeedX;
					puck.y += this.SpeedY;
					this.ForceMultiplier = 1.0f;
					//puck.SpeedMultiplierX *= -0.1f;
					//.SpeedMultiplierY *= -0.1f;
				}

				if (other is AntiGoal)
				{
					AntiGoal notGoal = other as AntiGoal;
					this.Impulse(this.SpeedX, this.SpeedY);
					this.SpeedX -= SpeedX * 1.75f;
					this.SpeedY -= SpeedY * 1.75f;
					this.SpeedMultiplierX = -0.1f;
					this.SpeedMultiplierY = -0.1f;
					this.Friction = 0.8f;
					this.touchingGoal = true;
					//goal.Impulse(this.SpeedX, this.SpeedY);
					//goal.x += this.SpeedX * 1.1f;
					//goal.y += this.SpeedY * 1.1f;


					//this.Impulse(-(this.SpeedX*1.1f), -(this.SpeedY*1.1f));
					//this.x -= this.SpeedX * 10.0f;
					//this.y -= this.SpeedY * 10.0f;
				}

				if (other is Player)
				{
					Player player = other as Player;
					Console.WriteLine(!this.matrix.Equals(player));

					if (player.touchingGoal == false)
					{
						player.Impulse(this.SpeedX, this.SpeedY);
						player.x += this.SpeedX;
						player.y += this.SpeedY;
						this.SpeedMultiplierX *= -0.1f;
						this.SpeedMultiplierY *= -0.1f;
						this.SpeedX -= SpeedX * 1.75f;
						this.SpeedY -= SpeedY * 1.75f;
						//this.Impulse(-player.SpeedX, -player.SpeedY);
					}
					else {
						player.Impulse(-this.SpeedX * 0.5f, -this.SpeedY * 0.5f);
						this.SpeedMultiplierX *= 0.25f;
						this.SpeedMultiplierY *= 0.25f;
						this.SpeedX -= SpeedX * 25.5f;
						this.SpeedY -= SpeedY * 25.5f;
						this.Impulse(-this.SpeedX * 43, -this.SpeedY * 44);
					}

				}
			}

			//Console.WriteLine("{0} {1} {2}", SpeedX, SpeedY, Math.Pow(2.0, 3.0));
			//Console.WriteLine(this.matrix.Equals(player));
		}
	}
}
