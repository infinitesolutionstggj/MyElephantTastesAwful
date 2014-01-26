using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using META.Engine;
using META.Engine.Sprites;
using META.Engine.GameObjects;
using META.Engine.Achievements;
using META.Engine.InterfaceSystem;

namespace META.GameWorld.Objects.Characters
{
	public class Player : Character
	{
		public enum Animations
		{
			Idle,
			Walk,
			Jump,
			Count
		}

		public static Player Main;

		public float jumpPower;

		public Player(Vector2 _position)
			: base(new Rectangle((int)_position.X, (int)_position.Y, 100, 220), SpriteID.PlayerIdle, 500, DEFAULT_GRAVITY, new Rectangle(-50, -80, 210, 300))
		{
			if (Main != null)
				GameObjectManager.Objects.Remove(Main);
			Main = this;

			sprite = new SpriteInstance[(int)Animations.Count];
			sprite[(int)Animations.Idle] = new SpriteInstance(SpriteManager.GetSprite(SpriteID.PlayerIdle));
			sprite[(int)Animations.Walk] = new SpriteInstance(SpriteManager.GetSprite(SpriteID.PlayerWalk));
			sprite[(int)Animations.Jump] = new SpriteInstance(SpriteManager.GetSprite(SpriteID.PlayerJump));
			_currentAnimation = (int)Animations.Idle;

			jumpPower = 900;
		}

		public override void Update(GameTime gameTime)
		{
			if (isGrounded || GetCurrentAnimation() != Animations.Jump)
			{
				if (GetDirection() != 0)
				{
					SetAnimation(Animations.Walk);
				}
				else
				{
					SetAnimation(Animations.Idle);
				}
			}

			if (isGrounded && InputManager.GetCommandDown("Jump"))
			{
				GameStats.TotalJumps++;
				yVelocity -= jumpPower;
				SetAnimation(Animations.Jump);
				Sound.PlayerJump.PlayIfNotMuted();
			}
            if (!isGrounded)
                GameStats.TotalAirTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

			if (position.Y > 1080)
			{
				GameStats.TotalPitFalls++;
				Kill();
				Sound.PlayerFall.PlayIfNotMuted();
			}
            if (GetCurrentAnimation() == Animations.Idle && StateMachineManager.CurrentState == State.Playing)
                GameStats.TotalIdleTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

			if (Collision.PointToRectCollision(GameStats.LevelGoal, this.collisionBox))
			{
				LevelComplete();
			}

			base.Update(gameTime);
		}

		public override int GetDirection()
		{
			int output = 0;
			if (InputManager.GetCommand("Left"))
				output--;
			if (InputManager.GetCommand("Right"))
				output++;
			return output;
		}

		public void LevelComplete()
		{
			GameStats.TotalCompletes++;
			if (GameStats.TotalLevelTime > GameStats.SlowestLevelCompletionTime)
				GameStats.SlowestLevelCompletionTime = GameStats.TotalLevelTime;
			if (GameStats.TotalLevelTime < GameStats.FastestLevelCompletionTime)
				GameStats.FastestLevelCompletionTime = GameStats.TotalLevelTime;
			Sound.LevelComplete.PlayIfNotMuted();
			GameEnvironment.Reset();
		}

		public override void Reset()
		{
            base.Reset();
			SetAnimation(Animations.Idle);
		}

		public void Kill()
		{
			if (position.X < 0)
				AchievementManager.Unlock(AchievementID.WrongWayDumbass);
			GameEnvironment.Reset();
			GameStats.TotalDeaths++;
			AchievementManager.Unlock(AchievementID.JustLikeNew);
		}

		public void Kill(Enemy assailant)
		{
			Kill();

			assailant.killSound.PlayIfNotMuted();
		}

		public void SetAnimation(Animations animation)
		{
			if (_currentAnimation == (int)animation)
				return;

			switch (animation)
			{
				case Animations.Idle:
				case Animations.Walk:
				case Animations.Jump:
					_currentAnimation = (int)animation;
					sprite[_currentAnimation].Reset();
					break;
				default:
					break;
			}
		}

		public Animations GetCurrentAnimation()
		{
			return (Animations)_currentAnimation;
		}

        public void ResolveEnemyCollision(Enemy enemy)
        {
            if (!enemy.active)
                return;

            Rectangle? collision = Collision.Rect(collisionBox, enemy.collisionBox);

            if (collision == null)
                return;

            Rectangle rect = (Rectangle)collision;
            if (rect.Width > rect.Height)	// Vertical Collision
            {
                if (rect.Y > Y)				// Character on top
                {
                    Bottom = enemy.Top;
                    if (yVelocity > 0)
                    {
                        yVelocity = -jumpPower;
                        enemy.active = false;
						enemy.deathSound.PlayIfNotMuted();
                    }
                    else
                    {
                        Kill(enemy);
                    }
                    isGrounded = true;

                    if ((this is Player) && (Right - enemy.Left <= 1 || enemy.Right - Left <= 1))
                        AchievementManager.Unlock(AchievementID.LifeOnTheEdge);
                }
                else if (rect.Height > 0)	// Character on bottom
                {
                    Kill(enemy);
                }
            }
            else							// Horizontal Collision
            {
                Kill(enemy);
            }
        }
        public override void ResolveAllCollisions()
        {
            base.ResolveAllCollisions();

            foreach (Enemy e in GameObjectManager.Objects.OfType<Enemy>())
                ResolveEnemyCollision(e);
        }
	}
}
