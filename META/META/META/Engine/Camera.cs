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

		public static void Initialize(SpriteBatch spriteBatch, Vector2 pan = new Vector2())
		{
			SpriteBatch = spriteBatch;
			Pan = pan;
		}

		public static void Update(GameTime gameTime)
		{
			if (WorldToScreenPoint(Player.Main.position).X >= SpriteBatch.GraphicsDevice.Viewport.Width * 0.75f)
				Pan.X = Player.Main.position.X - SpriteBatch.GraphicsDevice.Viewport.Width * 0.75f;
			else if (WorldToScreenPoint(Player.Main.position).X <= SpriteBatch.GraphicsDevice.Viewport.Width * 0.25f)
				Pan.X = Player.Main.position.X - SpriteBatch.GraphicsDevice.Viewport.Width * 0.25f;

			Pan.X = (int)MathHelper.Clamp(Pan.X, 0, 10000);
		}

		public static void Draw(Texture2D texture, Rectangle destinationRectangle, Color color)
		{
			Vector2 screenPos = WorldToScreenPoint(new Vector2(destinationRectangle.X, destinationRectangle.Y));
			SpriteBatch.Draw(texture, new Rectangle((int)screenPos.X, (int)screenPos.Y, destinationRectangle.Width, destinationRectangle.Height), color);
		}

		public static Vector2 WorldToScreenPoint(Vector2 worldPoint)
		{
			return worldPoint - Pan;
		}

		public static Vector2 ScreenToWorldPoint(Vector2 screenPoint)
		{
			return screenPoint + Pan;
		}
	}
}
