using System;
using GXPEngine;

namespace GXPEngine
{
	public class PowerUps : AnimationSprite
	{
		public bool extraPuck = false;
		public bool inversedBlue = false;
		public bool inversedRed = false;
		public bool powerShotBlue = false;
		public bool powerShotRed = false;
		public bool speedBoostBlue = false;
		public bool speedBoostRed = false;

		public PowerUps() : base("PowerUps.png", 2,5)
		{
		}
	}
}
