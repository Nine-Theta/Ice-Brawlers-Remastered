using System;
using GXPEngine;
namespace GXPEngine
{
	public class Player : AnimationSprite
	{
		public static float playerX { get; private set; }
		public static float playerY { get; private set; }
		public static int PlayerMoney = 5;
		public static bool Alive = true;
		public static bool ShopInteract = false;
		float LocationX = 32.0f;
		float LocationY = 32.0f;
		float SpeedMult = 1.0f;
		float SPEEDX = 4.0f;
		float SPEEDY = 4.0f;
		bool GoesLeft = false;
		bool GoesRight = false;
		bool GoesUp = false;
		bool GoesDown = false;
		bool WentLeft = false;
		bool WentRight = false;
		bool WentUp = false;
		bool WentDown = false;

		bool PlayerInputLeft = false;
		bool PlayerInputRight = false;
		bool PlayerInputUp = false;
		bool PlayerInputDown = false;

		PlayerHitBox playerHitBoxLeft;
		PlayerHitBox playerHitBoxRight;
		PlayerHitBox playerHitBoxDown;
		PlayerHitBox playerHitBoxUp;
		Weapon playerWeapon;

		public Player() : base("Player.png", 4, 1)
		{
			SetOrigin(width / 2, height / 2);
			scale = 0.5f;
			LocationX = 960.0f;
			LocationY = 540.0f;

			playerWeapon = new Weapon();
			AddChild(playerWeapon);
			playerWeapon.SetXY(64f, 0f);

			playerHitBoxRight = new PlayerHitBox(this);
			AddChild(playerHitBoxRight);
			playerHitBoxRight.SetXY(40f, 0f);

			playerHitBoxLeft = new PlayerHitBox(this);
			AddChild(playerHitBoxLeft);
			playerHitBoxLeft.SetXY(-40f, 0f);

			playerHitBoxDown = new PlayerHitBox(this);
			AddChild(playerHitBoxDown);
			playerHitBoxDown.SetXY(0f, 40f);

			playerHitBoxUp = new PlayerHitBox(this);
			AddChild(playerHitBoxUp);
			playerHitBoxUp.SetXY(0f, -40f);

		}
		void Update()
		{
			SouthCheck();
			Movement();
			Movement2();

			x = LocationX;
			y = LocationY;

			playerX = x;
			playerY = y;

			if (playerHitBoxUp.IsHittingShop & Input.GetKeyDown(Key.ENTER))
			{
				ShopInteract = true;
			}
			else {
				ShopInteract = false;
			}

			//Console.WriteLine("{0} {1} {2}", x, y, Alive);
		}

		public void OnCollision(GameObject other)
		{
			if (other is Enemy)
			{
				Alive = false;
				this.Destroy();
			}
		}

		private void SouthCheck()
		{

			if (((MyGame)game).SouthPaw == true)
			{
				if (Input.GetKey(Key.LEFT))
				{
					PlayerInputLeft = true;
				}
				else{
					PlayerInputLeft = false;
				}

				if (Input.GetKey(Key.RIGHT))
				{
					PlayerInputRight = true;
				}
				else{
					PlayerInputRight = false;
				}

				if (Input.GetKey(Key.UP))
				{
					PlayerInputUp = true;
				}
				else{
					PlayerInputUp = false;
				}

				if (Input.GetKey(Key.DOWN))
				{
					PlayerInputDown = true;
				}
				else{
					PlayerInputDown = false;
				}
			}

			if (((MyGame)game).SouthPaw == false)
			{
				if (Input.GetKey(Key.A))
				{
					PlayerInputLeft = true;
				}
				else {
					PlayerInputLeft = false;
				}

				if (Input.GetKey(Key.D))
				{
					PlayerInputRight = true;
				}
				else {
					PlayerInputRight = false;
				}

				if (Input.GetKey(Key.W))
				{
					PlayerInputUp = true;
				}
				else {
					PlayerInputUp = false;
				}

				if (Input.GetKey(Key.S))
				{
					PlayerInputDown = true;
				}
				else {
					PlayerInputDown = false;
				}
			}
		}

		/// <summary>
		/// Checks which direction the player is going to, and which direction it last went.
		/// </summary>
		private void Movement()
		{
			if (Input.GetKey(Key.LEFT_SHIFT)){
				SpeedMult = 2.0f;
			}
			else {
				SpeedMult = 1.0f;
			}

			//Left arrow key pressed//
			if (PlayerInputLeft == true && !playerHitBoxLeft.IsHittingWall)
			{
				this.SetFrame(3);
				GoesLeft = true;
				WentLeft = true;
				WentUp = false; WentDown = false; WentRight = false;
			}
			else
			{
				GoesLeft = false;
			}

			//Right arrow key pressed//
			if (PlayerInputRight == true && !playerHitBoxRight.IsHittingWall)
			{
				this.SetFrame(1);
				GoesRight = true;
				WentRight = true;
				WentUp = false; WentDown = false; WentLeft = false;
			}
			else
			{
				GoesRight = false;
			}

			//Up arrow key pressed//
			if (PlayerInputUp == true && !playerHitBoxUp.IsHittingWall)
			{
				this.SetFrame(0);
				GoesUp = true;
				WentUp = true;
				WentDown = false; WentLeft = false; WentRight = false;
			}
			else
			{
				GoesUp = false;
			}

			//Down arrow key pressed//
			if (PlayerInputDown == true && !playerHitBoxDown.IsHittingWall)
			{
				this.SetFrame(2);
				GoesDown = true;
				WentDown = true;
				WentUp = false; WentLeft = false; WentRight = false;
			}
			else
			{
				GoesDown = false;
			}
			//Console.WriteLine("{0},{1},{2}.{3}", playerHitBoxLeft.IsHittingWall, playerHitBoxRight.IsHittingWall, playerHitBoxUp.IsHittingWall, playerHitBoxDown.IsHittingWall);

		}

		/// <summary>
		/// The actual Movement of the player
		/// </summary>
		private void Movement2()
		{
			if ((GoesLeft == true) && (GoesUp == false) && (GoesDown == false))
			{
				LocationX -= SPEEDX * SpeedMult;
			}

			else if (GoesRight == true && GoesUp == false && GoesDown == false)
			{
				LocationX += SPEEDX * SpeedMult;
			}

			else if (GoesUp == true && GoesLeft == false && GoesRight == false)
			{
				LocationY -= SPEEDY * SpeedMult;
			}

			else if (GoesDown == true && GoesLeft == false && GoesRight == false)
			{
				LocationY += SPEEDY * SpeedMult;
			}

			if ((WentUp == true) && (GoesUp == false))
			{
				if ((y / 32.0f) > Math.Floor(y / 32.0f))
				{ LocationY -= SPEEDY; }
			}

			else if ((WentDown == true) && (GoesDown == false))
			{
				if ((y / 32.0f) > Math.Floor(y / 32.0f))
				{ LocationY += SPEEDY; }
			}

			else if ((WentLeft == true) && (GoesLeft == false))
			{
				if ((x / 32.0f) > Math.Floor(x / 32.0f))
				{ LocationX -= SPEEDX; }
			}

			else if ((WentRight == true) && (GoesRight == false))
			{
				if ((x / 32.0f) > Math.Floor(x / 32.0f))
				{ LocationX += SPEEDX; }
			}
		}
	}
}

/*
using System;
using GXPEngine;
namespace GXPEngine
{
	public class Player : Sprite
	{

		float LocationX = 32.0f;
		float LocationY = 32.0f;
		float SPEEDX = 4.0f;
		float SPEEDY = 4.0f;
		bool GoesLeft = false;
		bool GoesRight = false;
		bool GoesUp = false;
		bool GoesDown = false;
		bool WentLeft = false;
		bool WentRight = false;
		bool WentUp = false;
		bool WentDown = false;
		PlayerHitBox playerHitBoxLeft;
		PlayerHitBox playerHitBoxRight;
		PlayerHitBox playerHitBoxDown;
		PlayerHitBox playerHitBoxUp;

		public Player() : base("square.png")
		{
			SetOrigin(width / 2, height / 2);
			x = 256; y = 128;
			scale = 0.5f;


			playerHitBoxRight = new PlayerHitBox(this);
			AddChild(playerHitBoxRight);
			playerHitBoxRight.SetXY(48f, 0f);

			playerHitBoxLeft = new PlayerHitBox(this);
			AddChild(playerHitBoxLeft);
			playerHitBoxLeft.SetXY(-48f, 0f);

			playerHitBoxDown = new PlayerHitBox(this);
			AddChild(playerHitBoxDown);
			playerHitBoxDown.SetXY(0f, 48f);

			playerHitBoxUp = new PlayerHitBox(this);
			AddChild(playerHitBoxUp);
			playerHitBoxUp.SetXY(0f, -48f);
		}
		void Update()
		{
			
			Movement();
			Movement2();

			x = LocationX;
			y = LocationY;

			Console.WriteLine("{0} {1}", x, y);
		}

		/// <summary>
		/// Checks which direction the player is going to, and which direction it last went.
		/// </summary>
		private void Movement()
		{
			//Left arrow key pressed//
			if (Input.GetKey(Key.LEFT) & !playerHitBoxLeft.IsHittingWall)
			{
				GoesLeft = true;
				WentLeft = true;
				WentUp = false; WentDown = false; WentRight = false;
			}
			else
			{
				GoesLeft = false;
			}

			//Right arrow key pressed//
			if (Input.GetKey(Key.RIGHT) & !playerHitBoxRight.IsHittingWall)
			{
				GoesRight = true;
				WentRight = true;
				WentUp = false; WentDown = false; WentLeft = false;
			}
			else
			{
				GoesRight = false;
			}

			//Up arrow key pressed//
			if (Input.GetKey(Key.UP) & !playerHitBoxUp.IsHittingWall)
			{
				GoesUp = true;
				WentUp = true;
				WentDown = false; WentLeft = false; WentRight = false;
			}
			else
			{
				GoesUp = false;
			}

			//Down arrow key pressed//
			if (Input.GetKey(Key.DOWN) & !playerHitBoxDown.IsHittingWall)
			{
				GoesDown = true;
				WentDown = true;
				WentUp = false; WentLeft = false; WentRight = false;
			}
			else
			{
				GoesDown = false;
			}
			Console.WriteLine("{0},{1},{2}.{3}", playerHitBoxLeft.IsHittingWall, playerHitBoxRight.IsHittingWall, playerHitBoxUp.IsHittingWall, playerHitBoxDown.IsHittingWall);

		}

		/// <summary>
		/// The actual Movement of the player
		/// </summary>
		private void Movement2()
		{
			if (GoesLeft == true)
			{
				GoesUp = false;
				GoesDown = false;
				GoesRight = false;
				LocationX -= SPEEDX;
			}

			else if (GoesRight == true)
			{
				GoesUp = false;
				GoesDown = false;
				GoesLeft = false;
				LocationX += SPEEDX;
			}

			else if (GoesUp == true)
			{
				GoesLeft = false;
				GoesDown = false;
				GoesRight = false;
				LocationY -= SPEEDY;
			}

			else if (GoesDown == true)
			{
				GoesUp = false;
				GoesLeft = false;
				GoesRight = false;
				LocationY += SPEEDY;
			}

			if ((WentUp == true) & (GoesUp == false))
			{
				if ((y / 32.0f) > Math.Floor(y / 32.0f))
				{ LocationY -= SPEEDY; }
			}

			else if ((WentDown == true) & (GoesDown == false))
			{
				if ((y / 32.0f) > Math.Floor(y / 32.0f))
				{ LocationY += SPEEDY; }
			}

			else if ((WentLeft == true) & (GoesLeft == false))
			{
				if ((x / 32.0f) > Math.Floor(x / 32.0f))
				{ LocationX -= SPEEDX; }
			}

			else if ((WentRight == true) & (GoesRight == false))
			{
				if ((x / 32.0f) > Math.Floor(x / 32.0f))
				{ LocationX += SPEEDX; }
			}
		}

	}
}
*/
