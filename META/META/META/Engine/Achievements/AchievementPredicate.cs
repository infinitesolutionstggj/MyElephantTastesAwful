using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace META.Engine.Achievements
{
	public class AchievementPredicate : AchievementSet
	{
		AchievementID id;
		Func<bool> predicate;

		public static AchievementPredicate KeyPredicate(string key)
		{
			return new AchievementPredicate(("HoorayYouPressed" + key).ToEnum<AchievementID>(),
				() => InputManager.CurrentKeyState.IsKeyDown(key.ToEnum<Keys>()));
		}

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
