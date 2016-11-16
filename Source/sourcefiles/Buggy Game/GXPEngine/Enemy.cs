using System;
using GXPEngine;
namespace GXPEngine
{
	public class Enemy : AnimationSprite //It was about time I added an enemy
	{
		float Speed = 0.1f;
		//float RotationSpeed = 1.0f;
		float A = 0.0f;
		float A2 = 0.0f;
		float B = 0.0f;
		float B2 = 0.0f;
		float C = 0.0f;
		float C2 = 0.0f;
		public bool EnemyIsAlive = true;
		//bool TouchingOtherEnemy = true;
		Random DestroyRand = new Random();

		public Enemy() : base ("Enemy.png", 4, 1)
		{
			SetOrigin(width / 2, height / 2);
			scale = 0.5f;
			rotation = -90;
		}

		public void Update() //Where does Pre-programmed end, and AI start? (Hint: It's not here)
		{
			this.SetFrame(this.currentFrame += 1);

			//((MyGame)game).currentLevel.player.x;

			this.Move(Speed * Time.deltaTime, 0.0f);

					//Should use Mathf next time 
					//This is next time
			A = (this.x - Player.playerX);
			B = (this.y - Player.playerY);
			A2 = ((this.x - Player.playerX) * (this.x - Player.playerX));
			B2 = ((this.y - Player.playerY) * (this.y - Player.playerY));
			C2 = A2 + B2;
			C = Mathf.Sqrt(C2);

			if (this.y >= Player.playerY)
			{
				rotation = ((-180f / Mathf.PI) * ((float)Math.Asin((double)(A / C)))) - 90;
			}

			if (this.y < Player.playerY)
			{
				rotation = ((180f / Mathf.PI) * ((float)Math.Asin((double)(A / C)))) +90;
			}

			//TouchingOtherEnemy = false;
			//Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7}", A, B, C, A2, B2, C2, rotation, this.x);

		}

		public void OnCollision(GameObject other)
		{
			//Console.WriteLine("On Collision Time: " + Time.time);
			if (other is Bullet)
			{
				Coin newCoin = new Coin();
				MyGame myGame = (MyGame)game;
				myGame.currentLevel.AddChild(newCoin);
				newCoin.SetXY(this.x, this.y);
				//EnemyDestroyed = true;
				Level.EnemyAlive -= 1;
				this.Destroy();
			}

			if (other is Player)
			{
				//this.Destroy();
			}

			if (other is Enemy)
			{
				//Enemy otherEnemy = other as Enemy;
				//TouchingOtherEnemy = true;
			}

			if	(other is Wall)
			{
				//this.Move(Speed * 2.0f, -1.0f);
			}
			 
			//Console.WriteLine("{0}", TouchingOtherEnemy);
		}
	}
}

/*
public void Update()
		{
			if (rotation >= 360.0f){
				rotation = 0.0f;
			}

			if (this.x > Player.playerX){
				this.x -= Speed;
				if (rotation < 180.0f){
					rotation += RotationSpeed;

				}
				else {
					rotation -= RotationSpeed;
				}
			}

			if (this.x < Player.playerX){
				this.x += Speed;
				if (rotation < 0.0f){
					rotation += RotationSpeed;
				}
				else{
					rotation -= RotationSpeed;
				}
			}

			if (this.y > Player.playerY){
				this.y -= Speed;
				if (rotation < 270.0f){
					rotation += RotationSpeed;
				}
				else{
					rotation -= RotationSpeed;
				}
			}

			if (this.y < Player.playerY){
				this.y += Speed;
				if (rotation < 90.0f){
					rotation += RotationSpeed;
				}
				else{
					rotation -= RotationSpeed;
				}
			}
			Console.WriteLine("{0}", rotation);
*/