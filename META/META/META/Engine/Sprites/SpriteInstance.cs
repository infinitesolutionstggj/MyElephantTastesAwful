using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace META.Engine.Sprites
{
	public class SpriteInstance
	{
		public Sprite sprite;
		private int currentFrameIndex;
		private float elapsedFrameTime;

		public Texture2D CurrentFrame { get { return sprite.frames[currentFrameIndex]; } }

		public void Update(GameTime gameTime)
		{
			elapsedFrameTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

			while (elapsedFrameTime >= 1 / sprite.frameRate)
				NextFrame();
		}

		public void Reset()
		{
			currentFrameIndex = 0;
			elapsedFrameTime = 0;
		}

		public void NextFrame()
		{
			currentFrameIndex = (currentFrameIndex + 1) % sprite.frames.Length;
			elapsedFrameTime -= 1 / sprite.frameRate;
		}
	}
}
