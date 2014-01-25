using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace META.Engine.Achievements
{
	public class AchievementPredicate : AchievementSet
	{
		AchievementID id;
		Func<bool> predicate;

		public AchievementPredicate(AchievementID _id, Func<bool> _predicate)
		{
			id = _id;
			predicate = _predicate;
		}

		public override void Check()
		{
			if (predicate())
				AchievementManager.Unlock(id);
		}

		public override bool ShouldDie()
		{
			return AchievementManager.GetAchievement(id).isUnlocked;
		}
	}
}
