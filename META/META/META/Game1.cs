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
			AchievementManager.Initialize(Content);
			GameObjectManager.Initialize();
			Camera.Initialize(spriteBatch);

			InputManager.AddCommand("Confirm", Keys.Enter, Buttons.Start);
			InputManager.AddCommand("Exit", Keys.Escape, Buttons.Back);
			InputManager.AddCommand("Left", Keys.Left, Buttons.LeftThumbstickLeft);
			InputManager.AddCommand("Right", Keys.Right, Buttons.LeftThumbstickRight);
			InputManager.AddCommand("Jump", Keys.Up, Buttons.A);
			InputManager.AddCommand("Pause", Keys.Space, Buttons.Start);

			InputManager.AddCommand("CheckCode", Keys.Up, Buttons.DPadUp);
			InputManager.AddCommand("CheckCode", Keys.Down, Buttons.DPadDown);
			InputManager.AddCommand("CheckCode", Keys.Left, Buttons.DPadLeft);
			InputManager.AddCommand("CheckCode", Keys.Right, Buttons.DPadRight);
			InputManager.AddCommand("CheckCode", Keys.B, Buttons.B);
			InputManager.AddCommand("CheckCode", Keys.A, Buttons.A);
			InputManager.AddCommand("CodeUp", Keys.Up, Buttons.DPadUp);
			InputManager.AddCommand("CodeDown", Keys.Down, Buttons.DPadDown);
			InputManager.AddCommand("CodeLeft", Keys.Left, Buttons.DPadLeft);
			InputManager.AddCommand("CodeRight", Keys.Right, Buttons.DPadRight);
			InputManager.AddCommand("CodeB", Keys.B, Buttons.B);
			InputManager.AddCommand("CodeA", Keys.A, Buttons.A);
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

			if (InputManager.GetCommandDown("Confirm"))
				AchievementManager.Unlock(AchievementID.ParticipationRibbon);

			if (InputManager.GetCommand("Pause"))
			{
				GameStats.Paused = !GameStats.Paused;

				if (GameStats.Paused)
					AchievementManager.Unlock(AchievementID.PleaseComeBack);
				else
					AchievementManager.Unlock(AchievementID.IMissedYou);
			}

			GameStats.TotalGameTime = (float)gameTime.TotalGameTime.TotalSeconds;
			if(!GameStats.Paused)
				GameStats.TotalLevelTime = (float)gameTime.TotalGameTime.TotalSeconds;

			GameStats.FistsAndElbowsTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
			if (GameStats.FistsAndElbowsTimer >= 1)
			{
				GameStats.FistsAndElbowsTimer -= 1;
				GameStats.FistsAndElbowsCount = 0;
			}
			if (InputManager.GetCommandDown("Left") || InputManager.GetCommandDown("Right"))
			{
				if (++GameStats.FistsAndElbowsCount >= 10)
					AchievementManager.Unlock(AchievementID.FistsAndElbows);
			}

			if (InputManager.GetCommandDown("CheckCode"))
			{
				if (InputManager.GetCommandDown(GameStats.Code[GameStats.CodeIndex]))
				{
					GameStats.CodeIndex++;
					if (GameStats.CodeIndex >= GameStats.Code.Length)
					{
						AchievementManager.Unlock(AchievementID.SuperNiceTry);
						GameStats.CodeIndex = 0;
					}
				}
				else
					GameStats.CodeIndex = 0;
			}

			if (InputManager.GetCommand("Exit"))
			{
				AchievementManager.Unlock(AchievementID.GoodbyeCruelWorld);
				this.Exit();
			}
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
