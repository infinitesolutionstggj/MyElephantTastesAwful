using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace META.Engine.Achievements
{
	public struct AchievementFamilyRegistration
	{
		public AchievementID id;
		public int requirement;

		public AchievementFamilyRegistration(AchievementID _id, int _requirement)
		{
			id = _id;
			requirement = _requirement;
		}
	}
}
