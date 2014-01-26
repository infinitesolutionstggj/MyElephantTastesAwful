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
using META.Engine.InterfaceSystem;
using META.GameWorld;
using META.GameWorld.Objects.Characters;

namespace META
{
	public class Game1 : Microsoft.Xna.Framework.Game
	{
		public static string MostRecentAchievement = "";
		public SpriteFont font;
		public Texture2D[] backgrounds;
		public Texture2D menuBackground;

		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			//graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
			//graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
			//graphics.IsFullScreen = true;
		}

		protected override void Initialize()
		{
			base.Initialize();

			InputManager.Initialize();
			StateMachineManager.Initialize();
			AchievementManager.Initialize(Content);
			GameObjectManager.Initialize();
			Camera.Initialize(spriteBatch);
			GameEnvironment.Initialize();

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
			backgrounds = SpriteManager.GetTextures(Content, "BG/Background_", 5);
			menuBackground = Content.Load<Texture2D>("menu");
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
			GameEnvironment.Update(gameTime);

			switch (StateMachineManager.CurrentState)
			{ //State Machine
				case State.MainMenu:
					{
						if (InputManager.GetCommandDown("Confirm"))//Start Game Achievement
						{
							AchievementManager.Unlock(AchievementID.ParticipationRibbon);
							StateMachineManager.CurrentState = State.Playing;
						}
					}
					break;
				case State.Playing:
					{
						GameObjectManager.Update(gameTime);
						Camera.Update(gameTime);

						if (InputManager.GetCommandDown("Pause"))
						{//Pause Game Toggle
							AchievementManager.Unlock(AchievementID.PleaseComeBack);
							StateMachineManager.CurrentState = State.Paused;
						}

						GameStats.FistsAndElbowsTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
						if (GameStats.FistsAndElbowsTimer >= 1)
						{//Fists and Elbows Achievement
							GameStats.FistsAndElbowsTimer -= 1;
							GameStats.FistsAndElbowsCount = 0;
						}
						if (InputManager.GetCommandDown("Left") || InputManager.GetCommandDown("Right"))
						{
							if (++GameStats.FistsAndElbowsCount >= 10)
								AchievementManager.Unlock(AchievementID.FistsAndElbows);
						}
					}
					break;
				case State.Paused:
					{
						GameStats.TotalPauseTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
						if (InputManager.GetCommandDown("Pause"))
						{//Pause Game Toggle
							StateMachineManager.CurrentState = State.Playing;
							AchievementManager.Unlock(AchievementID.IMissedYou);
						}
					}
					break;
				case State.AchievementList:
					{

					}
					break;
			}

			GameStats.TotalGameTime = (float)gameTime.TotalGameTime.TotalSeconds;
			if (StateMachineManager.CurrentState != State.Paused)
				GameStats.TotalLevelTime = (float)gameTime.TotalGameTime.TotalSeconds;

			if (InputManager.GetCommandDown("CheckCode"))
			{//Super Nice Try Achievement
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
			base.Draw(gameTime);

			GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();
			switch (StateMachineManager.CurrentState)
			{//State Machine 
				case State.MainMenu:
					{
						spriteBatch.Draw(menuBackground, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
					}
					break;
				case State.Playing:
					{
						for (int i = 0; i < backgrounds.Length; i++)
							Camera.Draw(backgrounds[i], new Rectangle(i * 2048, 0, 2048, 1080), Color.White);
						GameObjectManager.Draw();
						//spriteBatch.DrawString(font, Player.Main.position.X.ToString() , new Vector2(10, 10), Color.Purple);
					}
					break;
				case State.Paused:
					{
						for (int i = 0; i < backgrounds.Length; i++)
							Camera.Draw(backgrounds[i], new Rectangle(i * 2048, 0, 2048, 1080), Color.White);

						spriteBatch.DrawStringCentered(font, "Game Paused", new Vector2(GraphicsDevice.Viewport.Width * 0.5f, GraphicsDevice.Viewport.Height * 0.5f), Color.Red);
						spriteBatch.DrawStringCentered(font, "Press Space to Continue", new Vector2(GraphicsDevice.Viewport.Width * 0.5f, GraphicsDevice.Viewport.Height * 0.85f), Color.Red);

						GameObjectManager.Draw();
						spriteBatch.DrawString(font, MostRecentAchievement, new Vector2(10, 10), Color.Purple);
					}
					break;
				case State.AchievementList:
					{

					}
					break;
			}
			AchievementManager.Draw();
			spriteBatch.End();
		}
	}
}
