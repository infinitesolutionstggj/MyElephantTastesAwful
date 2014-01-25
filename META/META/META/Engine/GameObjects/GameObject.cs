using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using META.Engine.Sprites;

namespace META
{
	public class GameObject
	{
		public Rectangle collisionBox;
		public SpriteInstance sprite;
		public bool active;

		public int X { get { return collisionBox.X; } set { collisionBox.X = value; } }
		public int Y { get { return collisionBox.Y; } set { collisionBox.Y = value; } }
		public int Left { get { return collisionBox.Left; } }
		public int Top { get { return collisionBox.Top; } }
		public int Right { get { return collisionBox.Right; } }
		public int Bottom { get { return collisionBox.Bottom; } }
		public int Width { get { return collisionBox.Width; } }
		public int Height { get { return collisionBox.Height; } }

		public GameObject(Rectangle _collisionBox, SpriteInstance _sprite)
		{
			collisionBox = _collisionBox;
			sprite = _sprite;
			active = true;
		}

		public virtual void Update(GameTime gameTime)
		{
			if (!active)
				return;

			sprite.Update(gameTime);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			if (!active)
				return;

			spriteBatch.Draw(sprite.CurrentFrame, collisionBox, Color.White);
		}
	}
}
