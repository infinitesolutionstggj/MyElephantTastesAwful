using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using META.Engine;
using META.Engine.GameObjects;
using META.GameWorld.Objects.Characters;

namespace META.GameWorld
{
	class GameEnvironment
	{
		private static List<Vector2> SpikeList;

		public static void Initialize()
		{
			SpikeList = new List<Vector2>();
			SpikeList.Add(new Vector2(300, 607));
			SpikeList.Add(new Vector2(384, 526));
			SpikeList.Add(new Vector2(544, 660));
			SpikeList.Add(new Vector2(622, 920));
			SpikeList.Add(new Vector2(2144, 713));
			SpikeList.Add(new Vector2(3824, 947));
			SpikeList.Add(new Vector2(4448, 941));
			SpikeList.Add(new Vector2(7003, 383));
			SpikeList.Add(new Vector2(9600, 1080));
			SpikeList.Add(new Vector2(7509, 767));
			SpikeList.Add(new Vector2(9125, 871));
		}

		public static void Update(GameTime gameTime)
		{
			CheckSpikeCollisions();
		}

		private static void CheckSpikeCollisions()
		{
			foreach (var s in SpikeList)
			{
				if (Collision.PointToRectCollision(s, Player.Main.collisionBox))
				{
					Player.Main.Kill();
					GameStats.TotalSpikes++;
					Sound.SpikeKill.PlayIfNotMuted();
					return;
				}
			}
		}

		public static void Reset()
		{
			GameStats.TotalLevelTime = 0;
			foreach (Character c in GameObjectManager.Objects.OfType<Character>())
				c.Reset();
		}
	}
}
