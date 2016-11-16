using System;
using GXPEngine;
namespace GXPEngine
{
	public class Wall : AnimationSprite
	{
		public Wall(/*float x, float y*/) : base("Walls.png", 3, 1)
		{
			SetOrigin(width / 2, height / 2);

			scaleX = 0.5f;
			scaleY = 0.5f;

			this.x = x;
			this.y = y;
		}
		void Update()
		{

		}
	}
}
