using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using META.Engine.Achievements;
using META.Engine.GameObjects;
using META.Engine.Sprites;
using META.Engine;
using META.GameWorld;

namespace META
{
	public class Game1 : Microsoft.Xna.Framework.Game
	{
		public static string MostRecentAchievement = "";
		public SpriteFont font;

		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			graphics.PreferredBackBufferWidth = 800;
			graphics.PreferredBackBufferHeight = 600;
		}

		protected override void Initialize()
		{
			base.Initialize();

			InputManager.Initialize();
			AchievementManager.Initialize();
			GameObjectManager.Initialize();
			Camera.Initialize(spriteBatch);

			InputManager.AddCommand("Exit", Keys.Escape, Buttons.Back);
			InputManager.AddCommand("Left", Keys.Left, Buttons.LeftThumbstickLeft);
			InputManager.AddCommand("Right", Keys.Right, Buttons.LeftThumbstickRight);
			InputManager.AddCommand("Jump", Keys.Space, Buttons.A);
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);
			font = Content.Load<SpriteFont>("SpriteFont1");
			SpriteManager.LoadContent(Content);
		}

		protected override void UnloadContent()
		{
		}

		protected override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			InputManager.Update();
			AchievementManager.Update(gameTime);
			GameObjectManager.Update(gameTime);
			Camera.Update(gameTime);

			GameStats.TotalGameTime = (float)gameTime.TotalGameTime.TotalSeconds;
			if(!GameStats.Paused)
				GameStats.TotalLevelTime = (float)gameTime.TotalGameTime.TotalSeconds;

			if (InputManager.GetCommand("Exit"))
				this.Exit();
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();
			GameObjectManager.Draw(spriteBatch);
			spriteBatch.DrawString(font, MostRecentAchievement, new Vector2(10, 10), Color.Purple);
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
