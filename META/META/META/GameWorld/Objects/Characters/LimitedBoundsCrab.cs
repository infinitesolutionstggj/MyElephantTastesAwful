using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using META.Engine.Sprites;
using META.Engine.GameObjects;

namespace META.GameWorld.Objects.Characters
{
	class LimitedBoundsCrab : Enemy
	{
		float minXPosition, maxXPosition;
		int direction;

		public LimitedBoundsCrab(Vector2 _position, float _minXPosition, float _maxXPosition)
			: base(_position, Engine.Sprites.SpriteID.Crab, Sound.CrabKill, Sound.CrabDeath, 350)
		{
			this.maxXPosition = _maxXPosition;
			this.minXPosition = _minXPosition;
			direction = -1;
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			if (position.X <= minXPosition)
				direction = 1;
			if (position.X >= maxXPosition - Width)
				direction = -1;
		}

		public override void Reset()
		{
			base.Reset();
			direction = -1;
		}

		public override int GetDirection()
		{
			return direction;
		}
	}
}
