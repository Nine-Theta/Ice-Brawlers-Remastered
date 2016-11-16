using System;
using GXPEngine;

namespace GXPEngine
{
	public class ScoreBoard : AnimationSprite
	{
		public ScoreBoard() : base("numbers.png", 10, 1)
		{
			SetOrigin(width / 2, height / 2);
		}
	}
}
