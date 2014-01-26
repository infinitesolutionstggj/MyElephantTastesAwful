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
		public const float DEFAULT_GRAVITY = 2000;

        public Vector2 spawnPosition;
		public float moveSpeed;
		public float gravity;
		public float yVelocity;
		public bool isGrounded;
		protected int facingDir = 1;

		public Character(Vector2 _position, SpriteID _sprite, float _moveSpeed, float _gravity = DEFAULT_GRAVITY)
			: base(_position, _sprite)
		{
            spawnPosition = _position;
			moveSpeed = _moveSpeed;
			gravity = _gravity;
			yVelocity = 0;
			isGrounded = false;
		}

		public Character(Rectangle _collisionBox, SpriteID _sprite, float _moveSpeed, float _gravity = DEFAULT_GRAVITY, Rectangle? _relativeSpriteArea = null)
			: base(_collisionBox, _sprite, _relativeSpriteArea)
		{
            spawnPosition = new Vector2(_collisionBox.X, _collisionBox.Y);
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
            Vector2 displacement = new Vector2(moveSpeed * direction, yVelocity) * deltaTime;
            if (this is Player)
            {
                GameStats.DistanceWalked += displacement.Length();
            }
            position += displacement;

			if (yVelocity != 0)
                isGrounded = false;

            yVelocity += gravity * deltaTime;
			SyncCollisionBox();
			ResolveAllCollisions();
			base.Update(gameTime);
		}

		public void ResolveTerrainCollision(GenericTestObstacle obstacle)
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

					if (!isGrounded)
					{
						Sound.PlayerLand.PlayIfNotMuted();
						isGrounded = true;
					}

					if ((this is Player) && (Right - obstacle.Left <= 1 || obstacle.Right - Left <= 1))
						AchievementManager.Unlock(AchievementID.LifeOnTheEdge);
				}
				else if (rect.Height > 0 && this is Player)	// Character on bottom
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

		public virtual void ResolveAllCollisions()
		{
			foreach (var o in GameObjectManager.Objects.OfType<GenericTestObstacle>())
				ResolveTerrainCollision(o);
		}

		public abstract int GetDirection();

        public virtual void Reset()
        {
            position = spawnPosition;
            yVelocity = 0;
			active = true;
        }

		public override void Draw()
		{
			if (!active)
				return;
			bool flipDirection = false;
			if (facingDir == -1)
				flipDirection = true;
			Camera.Draw(sprite[_currentAnimation].CurrentFrame, Canvas, Color.White, flipDirection);
			//Camera.Draw(debugTexture, collisionBox, new Color(1, 1, 1, 0.25f));
		}
	}
}
