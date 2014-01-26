using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using META.Engine;
using META.Engine.GameObjects;
using META.Engine.Sprites;
using META.Engine.Achievements;

namespace META.GameWorld.Objects.Characters
{
	public abstract class Character : GameObject
	{
		public const float DEFAULT_GRAVITY = 1000;

		public float moveSpeed;
		public float gravity;
		public float yVelocity;
		public bool isGrounded;
		protected int facingDir = 1;

		public Character(Vector2 _position, SpriteID _sprite, float _moveSpeed, float _gravity = DEFAULT_GRAVITY)
			: base(_position, _sprite)
		{
			moveSpeed = _moveSpeed;
			gravity = _gravity;
			yVelocity = 0;
			isGrounded = false;
		}

		public Character(Rectangle _collisionBox, SpriteID _sprite, float _moveSpeed, float _gravity = DEFAULT_GRAVITY, Rectangle? _relativeSpriteArea = null)
			: base(_collisionBox, _sprite, _relativeSpriteArea)
		{
			moveSpeed = _moveSpeed;
			gravity = _gravity;
			yVelocity = 0;
			isGrounded = false;
		}

		public override void Update(GameTime gameTime)
		{
			float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
			int direction = GetDirection();
			if (direction != 0)
				facingDir = direction;

			position += new Vector2(moveSpeed * direction, yVelocity) * deltaTime;

			if (yVelocity != 0)
				isGrounded = false;

			yVelocity += gravity * deltaTime;
			SyncCollisionBox();
			ResolveAllCollisions();
			base.Update(gameTime);
		}

		public void ResolveCollision(GenericTestObstacle obstacle)
		{
			Rectangle? collision = Collision.Rect(collisionBox, obstacle.collisionBox);

			if (collision == null)
				return;

			Rectangle rect = (Rectangle)collision;
			if (rect.Width > rect.Height)	// Vertical Collision
			{
				if (rect.Y > Y)				// Character on top
				{
					Bottom = obstacle.Top;
					if (yVelocity > 0)
						yVelocity = 0;
					isGrounded = true;

					if ((this is Player) && (Right - obstacle.Left <= 1 || obstacle.Right - Left <= 1))
						AchievementManager.Unlock(AchievementID.LifeOnTheEdge);
				}
				else if (rect.Height > 0)	// Character on bottom
				{
					Top = obstacle.Bottom;
					if (yVelocity < 0)
					{
						if (this is Player)
							GameStats.TotalHeadHits++;

						yVelocity = 0;
					}
				}
			}
			else							// Horizontal Collision
			{
				if (rect.X > X)				// Character on left
				{
					Right = obstacle.Left;
				}
				else						// Character on right
				{
					Left = obstacle.Right;
				}
			}
		}

		public void ResolveAllCollisions()
		{
			foreach (var o in GameObjectManager.Objects.OfType<GenericTestObstacle>())
				ResolveCollision(o);
		}

		public abstract int GetDirection();

		public override void Draw()
		{
			if (!active)
				return;
			bool flipDirection = false;
			if (facingDir == -1)
				flipDirection = true;
			Camera.Draw(sprite[_currentAnimation].CurrentFrame, Canvas, Color.White, flipDirection);
		}
	}
}
