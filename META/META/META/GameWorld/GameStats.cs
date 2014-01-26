using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace META.GameWorld
{
	public static class GameStats
	{
		public static int UnlockedAchievements = 0;
		public static float TotalGameTime = 0;
		public static float TotalLevelTime = 0;
		public static float FastestLevelCompletionTime = float.MaxValue;
		public static float SlowestLevelCompletionTime = 0;
		public static float DistanceWalked = 0; // TODO
		public static int TotalJumps = 0;		// TODO
		public static float totalIdleTime = 0;	// TODO
		public static float TotalAirTime = 0;	// TODO
		//public static bool Paused = false; // TODO
		public static float TotalPauseTime = 0; // TODO
		public static bool Muted = false;
		public static bool HasBeenMuted = false;
		public static int TotalDeaths = 0;
		public static int TotalPitFalls = 0;
		public static int TotalSpikes = 0; // TODO
		public static int TotalCompletes = 0; // TODO
		public static int TotalHeadHits = 0;
		public static float FistsAndElbowsTimer = 0;
		public static int FistsAndElbowsCount = 0;

		public static int CodeIndex = 0;
		public static string[] Code = new string[]
			{
				"CodeUp",
				"CodeUp",
				"CodeDown",
				"CodeDown",
				"CodeLeft",
				"CodeRight",
				"CodeLeft",
				"CodeRight",
				"CodeB",
				"CodeA"
			};
	}
}
