using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace META.Engine.Sprites
{
	public class Sprite
	{
		public SpriteID id;
		public Texture2D[] frames;
		public float frameRate;
	}
}
