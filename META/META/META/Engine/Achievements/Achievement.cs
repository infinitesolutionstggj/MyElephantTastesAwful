using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace META.Engine.Achievements
{
	public class Achievement
	{
		public AchievementID id;
		public string name;
		public string description;
		public bool isUnlocked;
		public Texture2D icon;
	}
}
