using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace META.Engine.Achievements
{
	public class AchievementManager
	{
		public static Achievement[] Achievements;
		public static List<AchievementFamily> Families;

		public static void Initialize()
		{
			Achievements = new Achievement[(int)AchievementID.Count];
			for (int i = 0; i < (int)AchievementID.Count; i++)
			{
				Achievements[i] = new Achievement();
				Achievements[i].id = (AchievementID)i;
				Achievements[i].isUnlocked = false;
				Achievements[i].name = Achievements[i].id.ToString();
				Achievements[i].description = Achievements[i].name + " Description";
				Achievements[i].icon = null;
			}

			Families = new List<AchievementFamily>();
			Families.Add(new AchievementFamily(new List<AchievementFamilyRegistration>()
				{
					new AchievementFamilyRegistration(AchievementID.Played1Second, 1),
					new AchievementFamilyRegistration(AchievementID.Played1Minute, 60),
					new AchievementFamilyRegistration(AchievementID.Played1Hour, 3600),
					new AchievementFamilyRegistration(AchievementID.Played1Day, 86400)
				}, () => (int)Game1.TotalGameTime));
		}

		public static void Update(GameTime gameTime)
		{
			foreach (var f in Families)
			{
				f.Check();
			}
			Families.RemoveAll(x => x.members.Count == 0);
		}

		public static Achievement GetAchievement(AchievementID id)
		{
			foreach (var a in Achievements)
				if (a.id == id)
					return a;

			return null;
		}

		public static void Unlock(AchievementID id)
		{
			foreach (var a in Achievements)
			{
				if (a.id == id)
				{
					if (!a.isUnlocked)
					{
						a.isUnlocked = true;
						DisplayUnlock(a);
					}

					break;
				}
			}
		}

		public static void DisplayUnlock(Achievement a)
		{
			Game1.MostRecentAchievement = a.name;
		}
	}
}
