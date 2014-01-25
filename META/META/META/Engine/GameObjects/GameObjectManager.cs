using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using META.GameWorld.Objects;
using META.GameWorld.Objects.Characters;
using META.Engine.Sprites;

namespace META.Engine.GameObjects
{
	public static class GameObjectManager
	{
		public static List<GameObject> Objects;

		public static void Initialize()
		{
			Objects = new List<GameObject>();
			Objects.Add(new GenericTestObstacle(new Rectangle(0, 550, 300, 50)));
			Objects.Add(new GenericTestObstacle(new Rectangle(200, 400, 100, 25)));
			Objects.Add(new GenericTestObstacle(new Rectangle(100, 250, 100, 25)));
			Objects.Add(new GenericTestObstacle(new Rectangle(200, 100, 100, 25)));
			//Objects.Add(new GenericTestObstacle(new Rectangle(300, 50, 50, 550)));
			Objects.Add(new GenericTestObstacle(new Rectangle(450, 550, 350, 50)));
			Objects.Add(new GenericTestObstacle(new Rectangle(450, 250, 50, 350)));
			Objects.Add(new GenericTestObstacle(new Rectangle(500, 400, 50, 250)));
            Objects.Add(new Player(new Vector2(10, 10)));
		}

		public static void Update(GameTime gameTime)
		{
			foreach (var o in Objects)
				o.Update(gameTime);
		}

		public static void Draw(SpriteBatch spriteBatch)
		{
			foreach (var o in Objects)
				o.Draw(spriteBatch);
		}
	}
}
