using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using META.GameWorld.Objects.Characters;

namespace META.Engine
{
	public static class Camera
	{
		public static SpriteBatch SpriteBatch;
		public static Vector2 Pan;
		public static float Scale;

		public static void Initialize(SpriteBatch spriteBatch, Vector2 pan = new Vector2())
		{
			SpriteBatch = spriteBatch;
			Pan = pan;
			Scale = spriteBatch.GraphicsDevice.Viewport.Height / 1080f;
		}

		public static void Update(GameTime gameTime)
		{
			if (WorldToScreenPoint(Player.Main.position).X >= SpriteBatch.GraphicsDevice.Viewport.Width * 0.75f)
			{
				Vector2 screenPos = WorldToScreenPoint(Player.Main.position);
				screenPos.X -= SpriteBatch.GraphicsDevice.Viewport.Width * 0.75f;
				Pan.X = ScreenToWorldPoint(screenPos).X;
			}
				//Pan.X = Player.Main.position.X - SpriteBatch.GraphicsDevice.Viewport.Width * 0.75f;
			else if (WorldToScreenPoint(Player.Main.position).X <= SpriteBatch.GraphicsDevice.Viewport.Width * 0.25f)
			{
				Vector2 screenPos = WorldToScreenPoint(Player.Main.position);
				screenPos.X -= SpriteBatch.GraphicsDevice.Viewport.Width * 0.25f;
				Pan.X = ScreenToWorldPoint(screenPos).X;
			}
				//Pan.X = Player.Main.position.X - SpriteBatch.GraphicsDevice.Viewport.Width * 0.25f;

            Vector2 screenPos2 = WorldToScreenPoint(new Vector2(10240, 0)) - new Vector2(SpriteBatch.GraphicsDevice.Viewport.Width, 0);
            Pan.X = MathHelper.Clamp(Pan.X, 0, (int)ScreenToWorldPoint(screenPos2).X);
		}

		public static void Draw(Texture2D texture, Rectangle destinationRectangle, Color color, bool flipHorizontally = false)
		{
			Vector2 screenPos = WorldToScreenPoint(new Vector2(destinationRectangle.X, destinationRectangle.Y));
			if (flipHorizontally)
				SpriteBatch.Draw(texture, new Rectangle((int)screenPos.X, (int)screenPos.Y, (int)(destinationRectangle.Width * Scale), (int)(destinationRectangle.Height * Scale)), null, color, 0, new Vector2(), SpriteEffects.FlipHorizontally, 0);
			else
				SpriteBatch.Draw(texture, new Rectangle((int)screenPos.X, (int)screenPos.Y, (int)(destinationRectangle.Width * Scale), (int)(destinationRectangle.Height * Scale)), color);
		}

		public static void DrawString(SpriteFont font, string text, Vector2 position, Color color)
		{
			SpriteBatch.DrawString(font, text, WorldToScreenPoint(position), color, 0, new Vector2(), Scale * new Vector2(1.2f, 1), SpriteEffects.None, 0);
		}

		public static Vector2 WorldToScreenPoint(Vector2 worldPoint)
		{
			return (worldPoint - Pan) * Scale;
		}

		public static Vector2 ScreenToWorldPoint(Vector2 screenPoint)
		{
			return screenPoint / Scale + Pan;
		}
	}
}
