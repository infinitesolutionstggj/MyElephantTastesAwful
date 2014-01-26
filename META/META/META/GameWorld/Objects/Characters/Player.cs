using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
			: base(new Rectangle(10, -300, 100, 220), SpriteID.PlayerIdle, 350, DEFAULT_GRAVITY, new Rectangle(-50, -80, 210, 300))
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
				yVelocity -= jumpPower;
				SetAnimation(Animations.Jump);
			}
            if (!isGrounded)
                GameStats.TotalAirTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

			if (position.Y > 1080)
			{
				GameStats.TotalPitFalls++;
				Kill();
			}
            if (GetCurrentAnimation() == Animations.Idle && StateMachineManager.CurrentState == State.Playing)
                GameStats.totalIdleTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

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
		}

		public void Reset()
		{
			position = new Vector2(10, 10);
			yVelocity = 0;
			SetAnimation(Animations.Idle);
			GameStats.TotalLevelTime = 0;
		}

		public void Kill()
		{
			if (position.X < 0)
				AchievementManager.Unlock(AchievementID.WrongWayDumbass);
			Reset();
			GameStats.TotalDeaths++;
			AchievementManager.Unlock(AchievementID.JustLikeNew);
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
	}
}
