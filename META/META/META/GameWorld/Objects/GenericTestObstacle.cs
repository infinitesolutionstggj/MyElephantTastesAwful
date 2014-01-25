using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using META.Engine.GameObjects;
using META.Engine.Sprites;

namespace META.GameWorld.Objects
{
	public class GenericTestObstacle : GameObject
	{
		public GenericTestObstacle(Rectangle area)
			: base(area, SpriteID.Black, null)
		{
			collisionBox = area;
			relativeSpriteArea = new Rectangle(0, 0, area.Width, area.Height);
			sprite = new SpriteInstance[1];
			sprite[0] = new SpriteInstance(SpriteManager.GetSprite(SpriteID.Black));
		}
	}
}
