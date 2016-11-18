﻿using System;
using GXPEngine;

namespace GXPEngine
{
	public class ScoreBoard : AnimationSprite
	{
		string numberColour = "null";

		public ScoreBoard(string rColour) : base("01_UI_Scoreboard_Numbers.png", 11, 2)
		{
			SetOrigin(width / 2, height / 2);
			numberColour = rColour;

		}

		//disable collision (for scenery)
		protected override Core.Collider createCollider()
		{
			return null;//base.createCollider();
		}

		void Update()
		{
			if (numberColour == "blue")
			{

			}

			if (numberColour == "red")
			{

			}
		}

	}
}
