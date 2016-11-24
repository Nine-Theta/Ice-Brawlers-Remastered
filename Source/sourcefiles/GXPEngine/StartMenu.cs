using System;
namespace GXPEngine
{
	public class StartMenu : GameObject
	{
		AnimationSprite menuButtons;
		Sprite logo;
		public Sprite background;

		public bool MenuLock = false;

		public StartMenu()
		{
			//AddChildAt(background, 0);
			//background.SetXY(0, 0);

			menuButtons = new AnimationSprite("MenuButtons.png", 4, 3);
			menuButtons.SetOrigin(menuButtons.width / 2, menuButtons.height / 2);
			AddChild(menuButtons);
			menuButtons.currentFrame = 5;
			menuButtons.SetXY(game.width + 200, game.height / 2 - 1);

			logo = new Sprite("logo.png");
			logo.SetOrigin(logo.width / 2, logo.height / 2);
			AddChild(logo);
			logo.scale = 0.3f;
			logo.SetXY(game.width/2 - 8, -200);
		}

		protected override Core.Collider createCollider()
		{
			return null;//base.createCollider();
		}

		void Update()
		{
			if (logo.y < 160)
			{
				logo.y += 10.0f;
			}

			if (menuButtons.x > game.width/2 + 17)
			{
				menuButtons.x -= 25.0f;
			}

			if (Input.GetKeyDown(Key.ENTER) || Input.GetKeyDown(Key.SPACE))
			{
				menuButtons.currentFrame = 4;
				menuButtons.x += 17.0f;
				menuButtons.y -= 1.0f;
			}

			if (!MenuLock && (Input.GetKeyUp(Key.ENTER) || Input.GetKeyUp(Key.SPACE)))
			{
				menuButtons.currentFrame = 5;
				menuButtons.x -= 17.0f;
				menuButtons.y += 1.0f;
				((MyGame)game).loader = new LevelLoader(((MyGame)game).mainScaleX, ((MyGame)game).mainScaleY);
				((MyGame)game).AddChild(((MyGame)game).loader);
				MenuLock = true;
			}
		}
	}
}
