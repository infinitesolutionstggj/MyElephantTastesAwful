using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace META.Engine.Achievements
{
	public class AchievementFamily
	{
		public LinkedList<AchievementFamilyRegistration> members;
		public Func<int> currentValue;

		public AchievementFamily(List<AchievementFamilyRegistration> _members, Func<int> valueFunc)
		{
			members = new LinkedList<AchievementFamilyRegistration>();
			for (int i = 0; i < _members.Count; i++)
			{
				members.AddLast(_members[i]);
			}
			currentValue = valueFunc;
		}

		public void Check()
		{
			if (currentValue() >= members.First.Value.requirement)
			{
				AchievementManager.Unlock(members.First.Value.id);
				members.RemoveFirst();
			}
		}
	}
}
