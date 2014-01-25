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
		Black,
		Player,
		Crab
	}

	public static class SpriteManager
	{
		public static List<Sprite> Sprites;

		public static void LoadContent(ContentManager content)
		{
			Sprites = new List<Sprite>();
			Sprites.Add(new Sprite(SpriteID.Black, GetTextures(content, "Blackness", 1), 60));
            Sprites.Add(new Sprite(SpriteID.Player, GetTextures(content, "Player", 1), 60));
		}

		public static Sprite GetSprite(SpriteID id)
		{
			foreach (var s in Sprites)
				if (s.id == id)
					return s;
			return null;
		}

		public static Texture2D[] GetTextures(ContentManager content, string baseName, int frames, int numDigits = 2)
		{
			string format = "";
			for (int i = 0; i < numDigits; i++)
				format += "0";

			Texture2D[] output = new Texture2D[frames];
			for (int i = 0; i < frames; i++)
				output[i] = content.Load<Texture2D>(baseName + i.ToString(format));
			return output;
		}
	}
}
