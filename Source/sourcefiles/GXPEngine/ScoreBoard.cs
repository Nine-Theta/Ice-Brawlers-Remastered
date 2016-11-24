using System;
using GXPEngine;

namespace GXPEngine
{
	public class ScoreBoard : AnimationSprite
	{
		int timeGet1;
		int timeGet2;
		int timeGet3;

		string numberColour = "null";

		public ScoreBoard(string rColour) : base("Score_Numbers_New_Glow.png", 11, 1)
		{
			SetOrigin(width / 2, height / 2);
			numberColour = rColour;
			timeGet1 = (Time.now /1000) + 2;
			timeGet2 = (Time.now / 10000) + 2;
			timeGet3 = (Time.now / 100000) + 2;
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

			if (numberColour == "timer1")
			{
				Console.WriteLine(this.currentFrame);

				if (this.currentFrame == 0)
				{
					this.SetFrame(9);
				}

				if (timeGet1 == (Time.now / 1000) + 1)
				{
					timeGet1 = (Time.now / 1000) + 2;
					if (this.currentFrame == 0)
					{
						//this.SetFrame = 9;
					}
					else {
						this.currentFrame -= 1;
					}
				}
			}

			if (numberColour == "timer2")
			{
				if (timeGet2 == (Time.now / 10000) + 1)
				{
					timeGet1 = (Time.now / 10000) + 2;
					this.currentFrame -= 1;
				}
			}

			if (numberColour == "timer3")
			{
				if (timeGet3 == (Time.now / 100000) + 1)
				{
					timeGet3 = (Time.now / 100000) + 2;
					this.currentFrame -= 1;
				}
			}


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
