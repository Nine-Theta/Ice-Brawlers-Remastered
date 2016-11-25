using System;
namespace GXPEngine
{
	public class StartMenu : GameObject
	{
		OptionsMenu options;

		AnimationSprite startButtons;
		AnimationSprite optionsButtons;
		AnimationSprite exitButtons;
		AnimationSprite music;
		Sprite credits;
		Sprite logo;
		Sprite background;

		public bool MenuLock = false;

		public StartMenu()
		{
			background = new Sprite("assets/sprites/mainscreen.png");
			AddChildAt(background, 0);
			background.SetXY(0, 0);

			startButtons = new AnimationSprite("assets/sprites/MenuButtons.png", 4, 3);
			startButtons.SetOrigin(startButtons.width / 2, startButtons.height / 2);
			AddChild(startButtons);
			startButtons.scale = 0.8f;
			startButtons.currentFrame = 5;
			startButtons.SetXY(game.width/2, game.height/2 + 17);

			optionsButtons = new AnimationSprite("assets/sprites/MenuButtons.png", 4, 3);
			optionsButtons.SetOrigin(optionsButtons.width / 2, optionsButtons.height / 2);
			AddChild(optionsButtons);
			optionsButtons.currentFrame = 3;
			optionsButtons.SetXY(game.width/5 - 32, game.height/2);

			exitButtons = new AnimationSprite("assets/sprites/MenuButtons.png", 4, 3);
			exitButtons.SetOrigin(exitButtons.width / 2, exitButtons.height / 2);
			AddChild(exitButtons);
			exitButtons.currentFrame = 8;
			exitButtons.SetXY(game.width/5 * 4 + 32, game.height/2.09f);

			music = new AnimationSprite("assets/sprites/MenuButtons.png", 4, 3);
			music.SetOrigin(music.width / 2, music.height / 2);
			AddChild(music);
			music.currentFrame = 10;
			music.scale = 0.7f;
			music.SetXY(game.width/5, game.height/3 * 2);

			credits = new Sprite("assets/sprites/credits.png");
			credits.SetOrigin(credits.width / 2, credits.height / 2);
			credits.scale = 0.5f;
			AddChild(credits);
			credits.SetXY(game.width / 2, game.height);
			       
			logo = new Sprite("assets/sprites/logo.png");
			logo.SetOrigin(logo.width / 2, logo.height / 2);
			//AddChild(logo);
			logo.scale = 0.3f;
			logo.SetXY(game.width/2 - 8, -200);

			//((MyGame)game).backgroundMusic.Play();
		}

		protected override Core.Collider createCollider()
		{
			return null;//base.createCollider();
		}

		void Update()
		{
			if (((MyGame)game).toggleMusicOn == true)
			{
				music.SetFrame(10);
			}
			else {
				music.SetFrame(11);
			}

			/*if (credits.y > game.height)
			{
				credits.y -= 10.0f;
			}

			if (exitButtons.x < game.width / 5 * 4)
				exitButtons.x += 35.0f;

			if (optionsButtons.x > game.width / 5)
				optionsButtons.x -= 35.0f;
			
			if (startButtons.y < game.height/2)
				startButtons.y += 25.0f;
				*/

			if (Input.GetKeyDown(Key.UP))
			{
				startButtons.currentFrame = 5;
				startButtons.x = game.width / 2;
				startButtons.y = game.height / 2 + 17;

				optionsButtons.currentFrame = 3;
				optionsButtons.x = game.width / 5 - 32;

				exitButtons.currentFrame = 8;
				exitButtons.x = (game.width / 5) * 4 + 32;
			}

			if (Input.GetKeyDown(Key.LEFT))
			{
				startButtons.currentFrame = 4;
				startButtons.x = game.width / 2 + 14;
				startButtons.y = game.height / 2 + 16;

				optionsButtons.currentFrame = 2;
				optionsButtons.x = game.width / 5 - 18;

				exitButtons.currentFrame = 8;
				exitButtons.x = (game.width / 5) * 4 + 32;
			}

			if (Input.GetKeyDown(Key.RIGHT))
			{
				startButtons.currentFrame = 4;
				startButtons.x = game.width / 2 + 14;
				startButtons.y = game.height / 2 + 16;

				optionsButtons.currentFrame = 3;
				optionsButtons.x = game.width / 5 - 32;

				exitButtons.currentFrame = 9;
				exitButtons.x = (game.width / 5) * 4 + 18;
			}

			if (Input.GetKey(Key.DOWN))
			{
				credits.y = game.height - (credits.height/2);
			}
			else
				credits.y = game.height;


			if (startButtons.currentFrame == 5 && (Input.GetKeyDown(Key.ENTER) || Input.GetKeyDown(Key.FIVE) || Input.GetKeyDown(Key.LEFT_SHIFT) || Input.GetKeyDown(Key.LEFT_CTRL)))
			{
				startButtons.x = game.width / 2 + 14;
				startButtons.y = game.height / 2 + 16;
				((MyGame)game).loader = new LevelLoader(((MyGame)game).mainScaleX, ((MyGame)game).mainScaleY);
				((MyGame)game).AddChild(((MyGame)game).loader);
				MenuLock = true;
				this.Destroy();
			}

			if (optionsButtons.currentFrame == 2 && (Input.GetKeyDown(Key.ENTER) || Input.GetKeyDown(Key.FIVE) || Input.GetKeyDown(Key.LEFT_SHIFT) || Input.GetKeyDown(Key.LEFT_CTRL)))
			{
				if (music.currentFrame == 10)
				{
					music.SetFrame(11);
					music.x = game.width / 5 - 9.75f;
					music.y = game.height / 3 * 2 - 0.25f;
					((MyGame)game).toggleMusicOn = false;
				}
				else {
					music.SetFrame(10);
					music.x = game.width / 5;
					music.y = game.height / 3 * 2;
					((MyGame)game).toggleMusicOn = true;
				}
			}

			if (exitButtons.currentFrame == 9 && (Input.GetKeyDown(Key.ENTER) || Input.GetKeyDown(Key.FIVE) || Input.GetKeyDown(Key.LEFT_SHIFT) || Input.GetKeyDown(Key.LEFT_CTRL)))
			{
				Environment.Exit(0);
			}
		}
	}
}
