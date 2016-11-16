using System;

namespace GXPEngine
{
	public class MenuText : AnimationSprite
	{
		//private string TextType = "Menu";

		public MenuText() : base("MenuText.png", 6, 1)
		{
			SetOrigin(width / 2, height / 2);
		}

		//((MyGame)game).currentLevel.player.x;

		void Update()
		{
			if (((MyGame)game).currentLevel.pointer != null)
			{
				if (((MyGame)game).currentLevel.pointer.y == this.y)
				{
					if (this.currentFrame == 0)
					{
						this.currentFrame = 1;
					}
				}
				else if (this.currentFrame == 1)
				{
					this.currentFrame = 0;
				}

				if (((MyGame)game).currentLevel.pointer.y == this.y)
				{
					if (this.currentFrame == 2)
					{
						this.currentFrame = 3;
					}
				}
				else if (this.currentFrame == 3)
				{
					this.currentFrame = 2;
				}

				if (((MyGame)game).currentLevel.pointer.y == this.y)
				{
					if (this.currentFrame == 4)
					{
						this.currentFrame = 5;
					}
				}
				else if (this.currentFrame == 5)
				{
					this.currentFrame = 4;
			}
			}

			//Console.WriteLine("{0}", this.y);

			/*if (this == ((MyGame)game).currentLevel.start)
			{
				if (this.currentFrame == 1 && (Input.GetKeyDown(Key.DOWN) || Input.GetKeyDown(Key.S)))
				{
					this.SetFrame(0);
					((MyGame)game).currentLevel.options.SetFrame(3);
					Console.WriteLine("startdown");
				}

				if (this.currentFrame == 1 && Input.GetKeyDown(Key.ENTER))
				{
				}
				//Console.WriteLine("{0} {1}", this._currentFrame, this.currentFrame);
			}

			if (/*TextType == "Options" &&*//* this == ((MyGame)game).currentLevel.options)
			{
				if (this.currentFrame == 3 && (Input.GetKeyDown(Key.DOWN) || Input.GetKeyDown(Key.S)))
				{ 
					
					this.SetFrame(2);
					((MyGame)game).currentLevel.exit.SetFrame(5);
					Console.WriteLine("optionsdown");
				}

				if (this.currentFrame == 3 && (Input.GetKeyDown(Key.UP) || Input.GetKeyDown(Key.W)))
				{
					this.SetFrame(2);
					((MyGame)game).currentLevel.start.SetFrame(1);
					Console.WriteLine("optionsup");
				}

				if (this.currentFrame == 3 && Input.GetKeyDown(Key.ENTER))
				{
				}
			}

			if (this == ((MyGame)game).currentLevel.exit)
			{
				if (this.currentFrame == 5 && (Input.GetKeyDown(Key.UP) || Input.GetKeyDown(Key.W)))
				{
					this.SetFrame(4);
					((MyGame)game).currentLevel.options.SetFrame(3);
					Console.WriteLine("exitup");
				}

				if (this.currentFrame == 5 && Input.GetKeyDown(Key.ENTER))
				{
				}
			}*/
			//Console.WriteLine("{0} {1}", this._currentFrame, this.currentFrame);
		}
	}
}
