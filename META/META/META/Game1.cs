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

namespace META
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
		public static float TotalGameTime;

		public static string MostRecentAchievement = "";
		public SpriteFont font;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
		{
			base.Initialize();

			InputManager.Initialize();
			AchievementManager.Initialize();

			InputManager.AddCommand("Exit", Keys.Escape, Buttons.Back);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
			font = Content.Load<SpriteFont>("SpriteFont1");
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			InputManager.Update();
			AchievementManager.Update(gameTime);

			TotalGameTime = (float)gameTime.TotalGameTime.TotalSeconds;

			if (InputManager.GetCommand("Exit"))
                this.Exit();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();
			spriteBatch.DrawString(font, MostRecentAchievement, new Vector2(10, 10), Color.Purple);
			spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
