using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using META.Engine;
using META.Engine.Sprites;
using META.GameWorld;

namespace META.Engine.Achievements
{
	public class AchievementNotification
	{
		public const float DISPLAY_TIME = 0.5f;
		public const int DISPLAY_COUNT = 8;

		public static Texture2D Background;
		public static int Height;
		public static SpriteFont Font;

		public Achievement achievement;
		public float? timeStamp;

		public static void Initialize(ContentManager content)
		{
			Background = content.Load<Texture2D>("AchievementBG00");
			Height = Background.Height;
			Font = content.Load<SpriteFont>("SpriteFont1");
		}

		public AchievementNotification(Achievement _achievement)
		{
			achievement = _achievement;
			timeStamp = null;
		}

		public void Draw(int index)
		{
			if (timeStamp == null)
				timeStamp = GameStats.TotalGameTime;

			float scaleRatio = (float)Height / Background.Height;

			Camera.Draw(Background, new Rectangle((int)Camera.Pan.X, 1080 - Height * index, (int)(Background.Width * scaleRatio), Height), Color.White);
			Camera.Draw(achievement.icon, new Rectangle((int)(Camera.Pan.X + 10 * scaleRatio), (int)(1080 - Height * index + 3 * scaleRatio), (int)(scaleRatio * achievement.icon.Width), Height), Color.White);
			Camera.DrawString(Font, achievement.name, new Vector2((int)(Camera.Pan.X + scaleRatio * (achievement.icon.Width + 50)), (int)(1080 - Height * index + (scaleRatio * 60))), Color.White);
		}
	}
}
