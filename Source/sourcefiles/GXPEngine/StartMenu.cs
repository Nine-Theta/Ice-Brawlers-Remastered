using System;
namespace GXPEngine
{
	public class StartMenu : GameObject
	{
		public bool MenuLock = false;

		public StartMenu()
		{
			
		}

		void Update()
		{
			if (!MenuLock && (Input.GetKeyDown(Key.ENTER) || Input.GetKeyDown(Key.SPACE)))
			{
				((MyGame)game).loader = new LevelLoader(((MyGame)game).mainScaleX, ((MyGame)game).mainScaleY);
				((MyGame)game).AddChild(((MyGame)game).loader);
				MenuLock = true;
			}
		}
	}
}
