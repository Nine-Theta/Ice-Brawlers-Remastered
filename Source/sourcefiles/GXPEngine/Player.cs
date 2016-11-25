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
		public bool usedPowerUp = false;
		string whichPower;
		public float StartX, StartY;
		Random puckRange = new Random();

		public int rotateBluePow;
		public int rotateRedPow;

		int timeWaitBlue;
		int timeWaitRed;

		AnimationSprite playerBlue;
		AnimationSprite playerRed;
		AnimationSprite reflectionBlue;
		AnimationSprite reflectionRed;

		int left, right, up, down;

		public Player(int rLeft, int rRight, int rUp, int rDown, string rColour) : base("assets/sprites/colors.png")
		{
			alpha = 0.0f;
			SetOrigin(width / 2, height / 2);

			this.left = rLeft;
			this.right = rRight;
			this.up = rUp;
			this.down = rDown;

			colour = rColour;

			playerBlue = new AnimationSprite("assets/sprites/BluePlayerAnimNew.png", 4, 5);
			playerRed = new AnimationSprite("assets/sprites/RedPlayerAnimNew.png", 4, 5);
			reflectionBlue = new AnimationSprite("assets/sprites/BluePlayerAnimNew.png", 4, 5);
			reflectionRed = new AnimationSprite("assets/sprites/RedPlayerAnimNew.png", 4, 5);



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
				((MyGame)game).channelOne = ((MyGame)game).skating.Play();
				((MyGame)game).channelOne.Volume = 0.0f;
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
				((MyGame)game).channelTwo = ((MyGame)game).skating.Play();
				((MyGame)game).channelTwo.Volume = 0.0f;

			}
		}

		public void PlayerPower(string rPower)
		{
			this.hasPowerUp = true;

			whichPower = rPower;

			if (colour == "blue")
			{
				switch (rPower)
				{
					case "puck":
						((MyGame)game).loader.bluePower.currentFrame = 1;
						break;
					case "inverse":
						((MyGame)game).loader.bluePower.currentFrame = 3;
						break;
					case "shot":
						((MyGame)game).loader.bluePower.currentFrame = 0;
						break;
					case "speed":
						((MyGame)game).loader.bluePower.currentFrame = 2;
						break;
				}
			}

			if (colour == "red")
			{
				switch (rPower)
				{
					case "puck":
						((MyGame)game).loader.redPower.currentFrame = 5;
						break;
					case "inverse":
						((MyGame)game).loader.redPower.currentFrame = 7;
						break;
					case "shot":
						((MyGame)game).loader.redPower.currentFrame = 4;
						break;
					case "speed":
						((MyGame)game).loader.redPower.currentFrame = 6;
						break;
				}
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
			if (((MyGame)game).startLock == false)
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

				if (hasPowerUp == true)
				{
					if (colour == "blue")
					{
						if (Input.GetKeyDown(Key.A) || Input.GetKeyDown(Key.W))
						{
							switch (whichPower)
							{
								case "puck":
									((MyGame)game).addedPuck.Play();
									((MyGame)game).loader.extraPuck = new Puck(0xE000FF);
									((MyGame)game).loader.AddChild(((MyGame)game).loader.extraPuck);
									((MyGame)game).loader.extraPuck.color = 0x0000FF;
									((MyGame)game).loader.extraPuck.SetXY(((MyGame)game).loader.puck.x, ((MyGame)game).loader.puck.y + 11.0f);
									((MyGame)game).loader.bluePower.currentFrame = 8;
									this.hasPowerUp = false;
									break;
								case "inverse":
									((MyGame)game).loader.player2.InversedControls = true;
									((MyGame)game).loader.timeGetBlue = (Time.now / 1000) + 4;
									((MyGame)game).reverseControls.Play();
									((MyGame)game).loader.bluePower.currentFrame = 7;
									rotateBluePow = -4;
									usedPowerUp = true;
									break;
								case "shot":
									((MyGame)game).loader.player1.ForceMultiplier = 20.0f;
									rotateBluePow = -3;
									break;
								case "speed":
									((MyGame)game).goFast.Play();
									((MyGame)game).loader.timeGetBlue = (Time.now / 1000) + 3;
									((MyGame)game).loader.player1.SpeedMultiplierX *= 1.5f;
									((MyGame)game).loader.player1.SpeedMultiplierY *= 1.5f;
									((MyGame)game).loader.player1.speedLimit *= 2.0f;
									rotateBluePow = -5;
									usedPowerUp = true;
									break;
							}
						}
					}

					if (colour == "red")
					{
						if (Input.GetKeyDown(Key.LEFT_SHIFT) || Input.GetKeyDown(Key.LEFT_CTRL))
						{
							switch (whichPower)
							{
								case "puck":
									((MyGame)game).addedPuck.Play();
									((MyGame)game).loader.extraPuck = new Puck(0xE000FF);
									((MyGame)game).loader.AddChild(((MyGame)game).loader.extraPuck);
									((MyGame)game).loader.extraPuck.color = 0x0000FF;
									((MyGame)game).loader.extraPuck.SetXY(((MyGame)game).loader.puck.x, ((MyGame)game).loader.puck.y + 11.0f);
									((MyGame)game).loader.redPower.currentFrame = 8;
									this.hasPowerUp = false;
									break;
								case "inverse":
									((MyGame)game).loader.player2.InversedControls = true;
									((MyGame)game).loader.timeGetRed = (Time.now / 1000) + 4;
									((MyGame)game).reverseControls.Play();
									((MyGame)game).loader.redPower.currentFrame = 3;
									rotateRedPow = -4;
									usedPowerUp = true;
									break;
								case "shot":
									((MyGame)game).loader.player1.ForceMultiplier = 20.0f;
									rotateRedPow = -3;
									break;
								case "speed":
									((MyGame)game).goFast.Play();
									((MyGame)game).loader.timeGetRed = (Time.now / 1000) + 3;
									((MyGame)game).loader.player1.SpeedMultiplierX *= 1.5f;
									((MyGame)game).loader.player1.SpeedMultiplierY *= 1.5f;
									((MyGame)game).loader.player1.speedLimit *= 2.0f;
									rotateRedPow = -5;
									usedPowerUp = true;
									break;
							}
						}
					}
				}


				((MyGame)game).loader.bluePower.rotation += rotateBluePow;
				((MyGame)game).loader.redPower.rotation += rotateRedPow;

				if (this.x - width / 2 <= 72.0f - (this.y * 0.08f))
				{
					SpeedX = 1.0f + Mathf.Abs(SpeedX / 2.0f);
				}
				if (this.x + width / 2 >= game.width - 72.0f + (this.y * 0.08f))
				{
					SpeedX = -1.0f - Mathf.Abs(SpeedX / 2.0f);
				}
				if (this.y - height / 2 <= 48.0f + (game.height * 0.16f))
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
						((MyGame)game).channelOne.Mute = false;

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
					if (SpeedX < -3.0f || SpeedY > -3.0f || SpeedY < 3.0f)
					{

						((MyGame)game).channelOne.Mute = false;

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
						if (((MyGame)game).channelOne != null)
							((MyGame)game).channelOne.Mute = true;

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
						((MyGame)game).channelTwo.Mute = false;

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
						((MyGame)game).channelTwo.Mute = false;

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
						((MyGame)game).channelTwo.Mute = true;

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
						if (ForceMultiplier > 2.0f)
						{
							if (colour == "blue")
							{
								((MyGame)game).loader.bluePower.currentFrame = 8;
								((MyGame)game).loader.bluePower.rotation = 0;
								this.rotateBluePow = 0;
							}

							if (colour == "red")
							{
								((MyGame)game).loader.redPower.currentFrame = 8;
								((MyGame)game).loader.redPower.rotation = 0;
								this.rotateRedPow = 0;
							}
							this.hasPowerUp = false;
							((MyGame)game).boomShot.Play();
							this.ForceMultiplier = 1.0f;
						}
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
			}

			//Console.WriteLine("{0} {1} {2}", SpeedX, SpeedY, Math.Pow(2.0, 3.0));
			//Console.WriteLine(this.matrix.Equals(player));
		}
	}
}
