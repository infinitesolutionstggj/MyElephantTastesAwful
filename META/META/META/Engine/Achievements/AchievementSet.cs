using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace META.Engine.Achievements
{
	public abstract class AchievementSet
	{
		public abstract void Check();

		public abstract bool ShouldDie();
	}
}
