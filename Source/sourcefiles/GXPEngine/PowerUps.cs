using System;
using GXPEngine;

namespace GXPEngine
{
	public class PowerUps : AnimationSprite
	{
		public bool extraPuckBlue = false;
		public bool extraPuckRed = false;
		public bool inversedBlue = false;
		public bool inversedRed = false;
		public bool powerShotBlue = false;
		public bool powerShotRed = false;
		public bool speedBoostBlue = false;
		public bool speedBoostRed = false;

		int timeWait;

		Random rand = new Random();

		public PowerUps() : base("question.png", 8,1)
		{
			
		}

		void Update()
		{
			if (timeWait < Time.now / 10)
			{
				this.NextFrame();
				timeWait = (Time.now / 10) + 8;
			}

			foreach (GameObject other in GetCollisions())
			{
				if (other is Player)
				{
					Player player = other as Player;

					int whichPower = rand.Next(1,5);

					switch (whichPower)
					{
						case 1:
							if (player.colour == "blue")
								extraPuckBlue = true;
							if (player.colour == "red")
								extraPuckRed = true;
							break;
						case 2:
							if (player.colour == "blue")
								inversedRed = true;
							if (player.colour == "red")
								inversedBlue = true;
							break;
						case 3:
							if (player.colour == "blue")
								powerShotBlue = true;
							if (player.colour == "red")
								powerShotRed = true;
							break;
						case 4:
							if (player.colour == "blue")
								speedBoostBlue = true;
							if (player.colour == "red")
								speedBoostRed = true;
							break;
						default:
							break;
					}
				}
			}
		}
	}
}
