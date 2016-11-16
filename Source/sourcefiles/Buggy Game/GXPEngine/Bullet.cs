using System;
namespace GXPEngine
{
	public class Bullet : AnimationSprite
	{
		float Speed = 0.981f;
		bool Fired = false;
		public bool RightBullet = false;
		public bool LeftBullet = false;
		public bool UpBullet = false;
		public bool DownBullet = false;

		public Bullet() : base("Bullet.png", 2, 1)
		{
			SetOrigin(width / 2, height / 2);
			scale = 0.15f;
			color = 0x646464;

		}

		public void Update()
		{
			this.SetFrame(this._currentFrame += 1);
			              
			if (RightBullet == true){
				x += Speed * Time.deltaTime;
				rotation = 90;
				LeftBullet = false;
				UpBullet = false;
				DownBullet = false;
			}

			if (LeftBullet == true)
			{
				x -= Speed * Time.deltaTime;
				rotation = -90;
				RightBullet = false;
				UpBullet = false;
				DownBullet = false;
			}

			if (UpBullet == true)
			{
				y -= Speed * Time.deltaTime;
				rotation = 0;
				LeftBullet = false;
				RightBullet = false;
				DownBullet = false;
			}

			if (DownBullet == true)
			{
				y += Speed * Time.deltaTime;
				rotation = 180;
				LeftBullet = false;
				UpBullet = false;
				RightBullet = false;
			}
				
			Console.WriteLine("{0} {1} {2}", y, x, Speed);

			if (this.x > (game.width + 32f) || this.x <  -32f|| this.y > (game.height + 32f) || this.y < -32f)
			{
				
			}
		}

		public void OnCollision(GameObject other)
		{
			if (other is Wall)
			{
				this.Destroy();
			}
		}
	}
}
