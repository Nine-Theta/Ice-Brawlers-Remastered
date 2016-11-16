//reference

/*using System;
using GXPEngine;
namespace GXPEngine
{
	public class PlayerTest : Sprite
	{
		float SpeedX = 0.0f;
		float SpeedY = 0.0f;
		public PlayerTest() : base("triangle.png")
		{
			SetOrigin(width / 2, height / 2);
			x = 400;
			y = 300;
			Sprite.rotation = 4;

		}

		void Update()
		{
			x = x + SpeedX;
			y = y + SpeedY;
			if (Input.GetKey(Key.LEFT)) { SpeedX = (SpeedX - 1.1f) * 1.1f;} 
			if (Input.GetKey(Key.RIGHT)) { SpeedX = (SpeedX + 1.1f) * 1.1f; }
			if (Input.GetKey(Key.DOWN)) { SpeedY = (SpeedY + 1.1f) *1.1f; }
			if (Input.GetKey(Key.UP)) { SpeedY = (SpeedY - 1.1f) * 1.1f; }
			if (SpeedX >= 9) SpeedX = 7;
			if (SpeedX <= -9) SpeedX = -7;
			if (SpeedY >= 9) SpeedY = 7;
			if (SpeedY <= -9) SpeedY = -7;


			Move(SpeedX, SpeedY);

			SpeedX = SpeedX * 0.9f; if ((SpeedX <= 1) & (SpeedX >= -1)) SpeedX = 0;
			SpeedY = SpeedY * 0.9f; if ((SpeedY <= 1) & (SpeedY >= -1)) SpeedY = 0;

		}
	}
}*/