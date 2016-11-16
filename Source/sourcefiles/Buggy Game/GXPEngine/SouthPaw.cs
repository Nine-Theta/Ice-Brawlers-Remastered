using System;
namespace GXPEngine
{
	public class SouthPaw : AnimationSprite
	{
		public SouthPaw() : base("SOUTH_PAW.png", 2, 1)
		{
			SetOrigin(width / 2, height / 2);
			this.y = 128.0f;
		}

		public void Update()
		{
			if (Level.CurrentLevel == 103)
			{
				if (this.currentFrame == 1)
				{
					if ((Input.GetKeyDown(Key.ENTER) || Input.GetKeyDown(Key.SPACE)))
					{
						this.SetFrame(0);
					}
				}
				else if ((Input.GetKeyDown(Key.ENTER) || Input.GetKeyDown(Key.SPACE)))
					{
						this.SetFrame(1);
					}

				//Console.WriteLine("{0} {1}", this.currentFrame, Input.GetKeyDown(Key.ENTER));
			}
		}
	}
}
