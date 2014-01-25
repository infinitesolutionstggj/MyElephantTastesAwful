using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace META
{
	public static class GameObjectManager
	{
		public static List<GameObject> Objects;

		public static void Initialize()
		{
			Objects = new List<GameObject>();
		}

		public static void Update(GameTime gameTime)
		{
			foreach (var o in Objects)
				o.Update(gameTime);
		}

		public static void Draw(SpriteBatch spriteBatch)
		{
			foreach (var o in Objects)
				o.Draw(spriteBatch);
		}
	}
}
