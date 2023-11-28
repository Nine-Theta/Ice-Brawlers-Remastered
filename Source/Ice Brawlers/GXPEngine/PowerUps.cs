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

		public PowerUps() : base("assets/sprites/question.png", 8,1)
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
					if (player.hasPowerUp == false)
					{
						int whichPower = rand.Next(1, 5);
						switch (whichPower)
						{
							case 1:
								player.PlayerPower("puck");
								break;
							case 2:
								player.PlayerPower("inverse");
								break;
							case 3:
								player.PlayerPower("shot");
								break;
							case 4:
								player.PlayerPower("speed");
								break;
							default:
								break;
						}

						this.Destroy();
						((MyGame)game).pickUp.Play();
					}
				}
			}
		}
	}
}
