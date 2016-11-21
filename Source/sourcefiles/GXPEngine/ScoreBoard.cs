using System;
using GXPEngine;

namespace GXPEngine
{
	public class ScoreBoard : AnimationSprite
	{
		int timeGet;

		string numberColour = "null";

		public ScoreBoard(string rColour) : base("Score_Numbers_New_Glow.png", 11, 1)
		{
			SetOrigin(width / 2, height / 2);
			numberColour = rColour;
			timeGet = (Time.now /1000) + 2;
		}

		//disable collision (for scenery)
		protected override Core.Collider createCollider()
		{
			return null;//base.createCollider();
		}

		void Update()
		{
			/*if (timeGet == (Time.now / 1000) + 1)
			{
				timeGet = (Time.now / 1000) + 2;
				this.NextFrame();
			}*/

			//Console.WriteLine("{0} {1}", Time.now / 1000, timeGet);

			if (numberColour == "blue"){
				if (this.currentFrame == 10)
				currentFrame = 0;
			}

			if (numberColour == "blue2"){
				this.currentFrame = ((MyGame)game).ScoreBlue / 10;
			}

			if (numberColour == "red"){
				if (this.currentFrame == 10)
					currentFrame = 0;
			}

			if (numberColour == "red2"){
				this.currentFrame = ((MyGame)game).ScoreRed / 10;
			}

		}

	}
}
