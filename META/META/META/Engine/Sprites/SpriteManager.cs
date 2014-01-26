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
		PlayerIdle,
		PlayerWalk,
		PlayerJump,
		Crab,
        Butterfly,
        Balloon
	}

	public static class SpriteManager
	{
		public static List<Sprite> Sprites;

		public static void LoadContent(ContentManager content)
		{
			Sprites = new List<Sprite>();
			Sprites.Add(new Sprite(SpriteID.Black, GetTextures(content, "Clear", 1), 60));
			Sprites.Add(new Sprite(SpriteID.PlayerIdle, GetTextures(content, "Player/Idle/MainCharacter_Idle", 15), 15));
			Sprites.Add(new Sprite(SpriteID.PlayerWalk, GetTextures(content, "Player/Run/MainCharacter_Run", 8), 15));
			Sprites.Add(new Sprite(SpriteID.PlayerJump, GetTextures(content, "Player/Jump/MainCharacter_Jump", 6), 10));
            Sprites.Add(new Sprite(SpriteID.PlayerJump, GetTextures(content, "AchievementBG", 1), 60));
            Sprites.Add(new Sprite(SpriteID.Crab, GetTextures(content, "Crab/Crab", 8), 15));
            Sprites.Add(new Sprite(SpriteID.Butterfly, GetTextures(content, "Butterfly/Butterfly", 3), 15));
            Sprites.Add(new Sprite(SpriteID.Balloon, GetTextures(content, "Balloon/Balloon", 8), 15));
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
