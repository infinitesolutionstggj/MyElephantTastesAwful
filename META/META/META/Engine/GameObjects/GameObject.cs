using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using META.Engine;
using META.Engine.Sprites;

namespace META.Engine.GameObjects
{
	public class GameObject
	{
		public Vector2 position;
		public Rectangle collisionBox;
		public SpriteInstance[] sprite;
		public Rectangle relativeSpriteArea;
		public bool active;
		protected int _currentAnimation;

		public float X { get { return position.X; } set { position.X = value; } }
		public float Y { get { return position.Y; } set { position.Y = value; } }
		public float Left { get { return position.X; } set { position.X = value; } }
		public float Top { get { return position.Y; } set { position.Y = value; } }
		public float Right { get { return position.X + Width; } set { position.X = value - Width; } }
		public float Bottom { get { return position.Y + Height; } set { position.Y = value - Height; } }
		public int Width { get { return collisionBox.Width; } set { collisionBox.Width = value; } }
		public int Height { get { return collisionBox.Height; } set { collisionBox.Height = value; } }

		public Rectangle Canvas
		{
			get
			{
				return new Rectangle(collisionBox.X + relativeSpriteArea.X, collisionBox.Y + relativeSpriteArea.Y, relativeSpriteArea.Width, relativeSpriteArea.Height);
			}
		}

		public GameObject(Vector2 _position, SpriteID _sprite)
		{
			position = _position;
			_currentAnimation = 0;
			sprite = new SpriteInstance[1];
			sprite[_currentAnimation] = new SpriteInstance(SpriteManager.GetSprite(_sprite));
			collisionBox = new Rectangle((int)position.X, (int)position.Y, sprite[0].CurrentFrame.Width, sprite[0].CurrentFrame.Height);
			relativeSpriteArea = new Rectangle(0, 0, collisionBox.Width, collisionBox.Height);
			active = true;
		}

		public GameObject(Rectangle _collisionBox, SpriteID _sprite, Rectangle? _relativeSpriteArea = null)
		{
			collisionBox = _collisionBox;
			position = new Vector2(collisionBox.X, collisionBox.Y);
			_currentAnimation = 0;
			sprite = new SpriteInstance[1];
			sprite[_currentAnimation] = new SpriteInstance(SpriteManager.GetSprite(_sprite));
			relativeSpriteArea = (_relativeSpriteArea == null ? new Rectangle(0, 0, collisionBox.Width, collisionBox.Height) : (Rectangle)_relativeSpriteArea);
			active = true;
		}

		public virtual void Update(GameTime gameTime)
		{
			if (!active)
				return;

			SyncCollisionBox();

			sprite[_currentAnimation].Update(gameTime);
		}

		public void SyncCollisionBox()
		{
			if (!active)
				return;

			collisionBox.X = (int)position.X;
			collisionBox.Y = (int)position.Y;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			if (!active)
				return;

			Camera.Draw(sprite[_currentAnimation].CurrentFrame, Canvas, Color.White);
		}
	}
}
