using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using META.Engine.Sprites;
using META.Engine.GameObjects;
using META.Engine.Achievements;

namespace META.GameWorld.Objects.Characters
{
    class Player : Character
    {
		public static Player Main;

		public float jumpPower;

        public Player(Vector2 _position)
            : base(_position, SpriteID.Player, 350)
        {
			if (Main != null)
				GameObjectManager.Objects.Remove(Main);
			Main = this;

			jumpPower = 550;
        }

		public override void Update(GameTime gameTime)
		{
			if (isGrounded && InputManager.GetCommandDown("Jump"))
				yVelocity -= jumpPower;

			if (position.Y > 600)
			{
				GameStats.TotalPitFalls++;
				Kill();
			}

			base.Update(gameTime);
		}

		public override int GetDirection()
		{
			int output = 0;
			if (InputManager.GetCommand("Left"))
				output--;
			if (InputManager.GetCommand("Right"))
				output++;
			return output;
		}

		public void LevelComplete()
		{
			GameStats.TotalCompletes++;
			if (GameStats.TotalLevelTime > GameStats.SlowestLevelCompletionTime)
				GameStats.SlowestLevelCompletionTime = GameStats.TotalLevelTime;
			if (GameStats.TotalLevelTime < GameStats.FastestLevelCompletionTime)
				GameStats.FastestLevelCompletionTime = GameStats.TotalLevelTime;
		}

		public void Reset()
		{
			position = new Vector2(10, 10);
			yVelocity = 0;
			GameStats.TotalLevelTime = 0;
		}

		public void Kill()
		{
			if (position.X < 0)
				AchievementManager.Unlock(AchievementID.WrongWayDumbass);
			Reset();
			GameStats.TotalDeaths++;
			AchievementManager.Unlock(AchievementID.JustLikeNew);
		}
    }
}
