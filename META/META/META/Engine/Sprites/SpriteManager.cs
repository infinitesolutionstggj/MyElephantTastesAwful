using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace META.Engine.Sprites
{
	public enum SpriteID
	{
		Player,
		Crab
	}

	public static class SpriteManager
	{
		public static List<Sprite> Sprites;

		public static void LoadContent()
		{
			// LOAD THE GODDAMN SPRITES GODDAMMIT
		}

		public static Sprite GetSprite(SpriteID id)
		{
			foreach (var s in Sprites)
				if (s.id == id)
					return s;
			return null;
		}
	}
}
