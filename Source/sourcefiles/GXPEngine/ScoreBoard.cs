using System;
using GXPEngine;

namespace GXPEngine
{
	public class ScoreBoard : AnimationSprite
	{
		int timeGet1;
		int timeGet2;
		int timeGet3;

		//int seconds = 500;
		int ones, tens, hundreds;

		public bool timeEnd = false;

		string numberColour = "null";

		public ScoreBoard(string rColour) : base("assets/sprites/Score_Numbers_New_Glow.png", 11, 1)
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
			if (numberColour == "timer1" || numberColour == "timer2" || numberColour == "timer3")
			{
				if (((MyGame)game).seconds < 500 && ((MyGame)game).seconds > 459)
					((MyGame)game).seconds = 459;
				if (((MyGame)game).seconds < 400 && ((MyGame)game).seconds > 359)
					((MyGame)game).seconds = 359;
				if (((MyGame)game).seconds < 300 && ((MyGame)game).seconds > 259)
					((MyGame)game).seconds = 259;
				if (((MyGame)game).seconds < 200 && ((MyGame)game).seconds > 159)
					((MyGame)game).seconds = 159;
				if (((MyGame)game).seconds < 100 && ((MyGame)game).seconds > 59)
					((MyGame)game).seconds = 59;
				if (((MyGame)game).seconds == 0)
				{ }

				hundreds = ((MyGame)game).seconds / 100;
				tens = (((MyGame)game).seconds - (hundreds * 100)) / 10;
				ones = (((MyGame)game).seconds - (hundreds * 100)) - (tens * 10);

				/*if (timeGet == (Time.now / 1000) + 1)
				{
					timeGet = (Time.now / 1000) + 2;
					this.NextFrame();
				}*/

				if (numberColour == "timer1")
				{
					Console.WriteLine(this.currentFrame);

					//this.currentFrame = ((MyGame)game).seconds;
					this.SetFrame(ones);

					if (timeGet1 == (Time.now / 1000) + 1)
					{
						timeGet1 = (Time.now / 1000) + 2;
						if (((MyGame)game).seconds > 0)
						{
							((MyGame)game).seconds -= 1;
						}
					}
				}

				if (numberColour == "timer2")
				{
					this.SetFrame(tens);
				}

				if (numberColour == "timer3")
				{
					this.SetFrame(hundreds);
				}

				if (((MyGame)game).seconds == 0 && !((MyGame)game).startLock)
				{
					timeEnd = true;
				}

				if (((MyGame)game).seconds == 0 && ((MyGame)game).startLock)
				{
					((MyGame)game).seconds = 500;
					if (((MyGame)game).toggleMusicOn = true){
						((MyGame)game).backgroundMusic.Play();
					}
					((MyGame)game).startLock = false;
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
