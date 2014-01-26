using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using META.Engine;
using META.Engine.InterfaceSystem;
using META.Engine.Sprites;
using META.GameWorld;

namespace META.Engine.Achievements
{
	public class AchievementManager
	{
		public static Achievement[] Achievements;
		public static LinkedList<AchievementNotification> CurrentDisplayQueue;
		public static List<AchievementSet> Sets;
		public static Texture2D[] Icons;

		public static void Initialize(ContentManager content)
		{
			Icons = SpriteManager.GetTextures(content, "Achievements/TJ14_Achievements", (int)AchievementID.Count, 3);
			AchievementNotification.Initialize(content);
			CurrentDisplayQueue = new LinkedList<AchievementNotification>();
			Achievements = new Achievement[(int)AchievementID.Count];
			for (int i = 0; i < (int)AchievementID.Count; i++)
			{
				Achievements[i] = new Achievement();
				Achievements[i].id = (AchievementID)i;
				Achievements[i].isUnlocked = false;
				Achievements[i].name = StringExt.SplitCamelCase(Achievements[i].id.ToString()).ToLower();

				// Special cases for oddly Named Achievements
				if (Achievements[i].id == AchievementID.H4rdc0r3G4m3r)
					Achievements[i].name = "H4rdc0r3 G4m3r";

				Achievements[i].description = Achievements[i].name + " Description";
				Achievements[i].icon = Icons[i];
			}

			Sets = new List<AchievementSet>()
				{
					new AchievementPredicate(AchievementID.HelloWorld, () => true),
					new AchievementFamily(new List<AchievementFamilyRegistration>()
						{
							new AchievementFamilyRegistration(AchievementID.Tautology, 1),
							new AchievementFamilyRegistration(AchievementID.AchievementAchieved, 10),
							new AchievementFamilyRegistration(AchievementID.Yo, 20),
							new AchievementFamilyRegistration(AchievementID.Dawg, 21),
							new AchievementFamilyRegistration(AchievementID.We, 22),
							new AchievementFamilyRegistration(AchievementID.Herd, 23),
							new AchievementFamilyRegistration(AchievementID.U, 24),
							new AchievementFamilyRegistration(AchievementID.Like, 25),
							new AchievementFamilyRegistration(AchievementID.Achievements, 26),
							new AchievementFamilyRegistration(AchievementID.So, 27),
							new AchievementFamilyRegistration(AchievementID.Here, 28),
							new AchievementFamilyRegistration(AchievementID.Are, 29),
							new AchievementFamilyRegistration(AchievementID.Some, 30),
							new AchievementFamilyRegistration(AchievementID.Almost, 40),
							new AchievementFamilyRegistration(AchievementID.LifeTheUniverseAndEverything, 42),
							new AchievementFamilyRegistration(AchievementID.OneForEveryShade, 50),
							new AchievementFamilyRegistration(AchievementID.ZeroToSixty, 60),
							new AchievementFamilyRegistration(AchievementID.HeHeHe, 69),
							new AchievementFamilyRegistration(AchievementID.SixtyNinePlusOne, 70),
							new AchievementFamilyRegistration(AchievementID.FourTwenties, 80),
							new AchievementFamilyRegistration(AchievementID.PenultimateVictory, (int)AchievementID.Count - 1),
						}, () => GameStats.UnlockedAchievements),
					new AchievementPredicate(AchievementID.LockedNLoaded, () => false),
					new AchievementFamily(new List<AchievementFamilyRegistration>()
						{
							new AchievementFamilyRegistration(AchievementID.TheLongHaul, 1),
							new AchievementFamilyRegistration(AchievementID.ThisIsGettingExhausting, 30),
							new AchievementFamilyRegistration(AchievementID.DoYouHaveNothingBetterToDo, 60),
							new AchievementFamilyRegistration(AchievementID.H4rdc0r3G4m3r, 600),
							new AchievementFamilyRegistration(AchievementID.YouShouldGoOutside, 1800),
							new AchievementFamilyRegistration(AchievementID.SeriouslyThisCantBeHealthy, 3600)
						}, () => (int)GameStats.TotalGameTime),
					new AchievementFamily(new List<AchievementFamilyRegistration>()
						{
							new AchievementFamilyRegistration(AchievementID.ItsaMe, 1),
							new AchievementFamilyRegistration(AchievementID.LetsBounce, 10),
							new AchievementFamilyRegistration(AchievementID.ThisIsNotAFlightSimulator, 100)
						}, () => GameStats.TotalJumps),
					new AchievementFamily(new List<AchievementFamilyRegistration>()
						{
							new AchievementFamilyRegistration(AchievementID.PayAttention, 1),
							new AchievementFamilyRegistration(AchievementID.OnePlayerGame, 30),
							new AchievementFamilyRegistration(AchievementID.OneIsTheLoneliestNumber, 60),
							new AchievementFamilyRegistration(AchievementID.AreYouStillThere, 300)
						}, () => (int)(GameStats.TotalIdleTime)),
					new AchievementFamily(new List<AchievementFamilyRegistration>()
						{
							new AchievementFamilyRegistration(AchievementID.ItsABird, 1),
							new AchievementFamilyRegistration(AchievementID.ItsAPlane, 30),
							new AchievementFamilyRegistration(AchievementID.LikeAGazelle, 60),
							new AchievementFamilyRegistration(AchievementID.PilotingABlimp, 300)
						}, () => (int)(GameStats.TotalAirTime)),
					new AchievementPredicate(AchievementID.PCMasterRace, () => InputManager.CurrentKeyState.GetPressedKeys().Count() > 0),
					new AchievementPredicate(AchievementID.ConsoleFanboy, () => InputManager.CurrentPadState[0] != InputManager.CurrentPadState[3]),
					new AchievementPredicate(AchievementID.TheButtonsTheyDoNothing, () => Mouse.GetState().LeftButton == ButtonState.Pressed || Mouse.GetState().MiddleButton == ButtonState.Pressed || Mouse.GetState().RightButton == ButtonState.Pressed),
					new AchievementFamily(new List<AchievementFamilyRegistration>()
						{
							new AchievementFamilyRegistration(AchievementID.ICanChange, 1),
							new AchievementFamilyRegistration(AchievementID.YouCanBlameItAllOnMe, 30),
							new AchievementFamilyRegistration(AchievementID.RecklessAbandon, 60),
							new AchievementFamilyRegistration(AchievementID.ThinkerNotADoer, 300)
						}, () => (int)(GameStats.TotalPauseTime)),
					new AchievementPredicate(AchievementID.Backtracking, () => StateMachineManager.CurrentState == State.Playing && InputManager.GetCommand("Left")),
					new AchievementPredicate(AchievementID.Progress, () => StateMachineManager.CurrentState == State.Playing && InputManager.GetCommand("Right")),
					new AchievementFamily(new List<AchievementFamilyRegistration>()
						{
							new AchievementFamilyRegistration(AchievementID.ARegularRichardSimmons, 1000),
							new AchievementFamilyRegistration(AchievementID.ToFallDownAtYourDoor, 5000),
							new AchievementFamilyRegistration(AchievementID.SimplyWalkedIntoMordor, 10000),
							new AchievementFamilyRegistration(AchievementID.FiveThundred, 50000),
							new AchievementFamilyRegistration(AchievementID.WhatDoesTheScouterSay, 90001)
						}, () => (int)(GameStats.DistanceWalked)),
					new AchievementFamily(new List<AchievementFamilyRegistration>()
						{
							new AchievementFamilyRegistration(AchievementID.FatalError, 1),
							new AchievementFamilyRegistration(AchievementID.Tenacious, 10),
							new AchievementFamilyRegistration(AchievementID.DoesntKnowWhenToQuit, 100),
							new AchievementFamilyRegistration(AchievementID.Genocide, 1000)
						}, () => GameStats.TotalDeaths),
					new AchievementFamily(new List<AchievementFamilyRegistration>()
						{
							new AchievementFamilyRegistration(AchievementID.GoingDown, 1),
							new AchievementFamilyRegistration(AchievementID.HighwayToHell, 10),
							new AchievementFamilyRegistration(AchievementID.OhGravityThouArtAHeartlessBitch, 100)
						}, () => GameStats.TotalPitFalls),
					new AchievementFamily(new List<AchievementFamilyRegistration>()
						{
							new AchievementFamilyRegistration(AchievementID.ThisKillsThePlayer, 1),
							new AchievementFamilyRegistration(AchievementID.EveryTime, 10),
							new AchievementFamilyRegistration(AchievementID.NoExceptions, 100)
						}, () => GameStats.TotalSpikes),
					new AchievementFamily(new List<AchievementFamilyRegistration>()
						{
							new AchievementFamilyRegistration(AchievementID.NotCompletelyUseless, 1),
							new AchievementFamilyRegistration(AchievementID.PracticeMakesPerfect, 10),
							new AchievementFamilyRegistration(AchievementID.OneTrickPony, 100)
						}, () => GameStats.TotalCompletes),
					new AchievementFamily(new List<AchievementFamilyRegistration>()
						{
							new AchievementFamilyRegistration(AchievementID.YouGetAStar, -300),
							new AchievementFamilyRegistration(AchievementID.Brisk, -120),
							new AchievementFamilyRegistration(AchievementID.GottaGoFast, -60),
							new AchievementFamilyRegistration(AchievementID.ArribaArribaAndale, -30)
						}, () => (int)(GameStats.FastestLevelCompletionTime)),
					new AchievementFamily(new List<AchievementFamilyRegistration>()
						{
							new AchievementFamilyRegistration(AchievementID.SlowAsMolasses, 60),
							new AchievementFamilyRegistration(AchievementID.SlowAsMolassesInJanuary, 120),
							new AchievementFamilyRegistration(AchievementID.SlowAsMolassesInJanuaryGoingUphill, 300)
						}, () => (int)(GameStats.SlowestLevelCompletionTime)),
					new AchievementFamily(new List<AchievementFamilyRegistration>()
						{
							new AchievementFamilyRegistration(AchievementID.BoomHeadshot, 1),
							new AchievementFamilyRegistration(AchievementID.DumbItalianPlumberSyndrome, 10),
							new AchievementFamilyRegistration(AchievementID.Concussed, 100)
						}, () => GameStats.TotalHeadHits),
					new AchievementFamily(new List<AchievementFamilyRegistration>()
						{
							new AchievementFamilyRegistration(AchievementID.Squish, 1),
							new AchievementFamilyRegistration(AchievementID.BugCollector, 10),
							new AchievementFamilyRegistration(AchievementID.CatchEmAll, 100)
						}, () => GameStats.ButterfliesKilled),
					new AchievementFamily(new List<AchievementFamilyRegistration>()
						{
							new AchievementFamilyRegistration(AchievementID.CrabCracker, 1),
							new AchievementFamilyRegistration(AchievementID.DinnerTime, 10),
							new AchievementFamilyRegistration(AchievementID.JustWriteButterDip, 100)
						}, () => GameStats.CrabsKilled),
					new AchievementFamily(new List<AchievementFamilyRegistration>()
						{
							new AchievementFamilyRegistration(AchievementID.HeliumDown, 1),
							new AchievementFamilyRegistration(AchievementID.NoMoreParties, 10),
							new AchievementFamilyRegistration(AchievementID.PopGoesYourLife, 100)
						}, () => GameStats.BalloonsKilled),
					new AchievementFamily(new List<AchievementFamilyRegistration>()
						{
							new AchievementFamilyRegistration(AchievementID.NobodySuspectsTheButterfly, 1),
							new AchievementFamilyRegistration(AchievementID.WingedDeath, 10),
							new AchievementFamilyRegistration(AchievementID.FlutteringMassacre, 100)
						}, () => GameStats.ButterflyDeaths),
					new AchievementFamily(new List<AchievementFamilyRegistration>()
						{
							new AchievementFamilyRegistration(AchievementID.Globophobia, 1),
							new AchievementFamilyRegistration(AchievementID.Asphixiation, 10),
							new AchievementFamilyRegistration(AchievementID.TheFaceOfMercy, 100)
						}, () => GameStats.BalloonDeaths),
					new AchievementFamily(new List<AchievementFamilyRegistration>()
						{
							new AchievementFamilyRegistration(AchievementID.YouGotCrabs, 1),
							new AchievementFamilyRegistration(AchievementID.PinchyDoom, 10),
							new AchievementFamilyRegistration(AchievementID.DeathOfAThousandPinches, 100)
						}, () => GameStats.CrabDeaths),
					new AchievementFamily(new List<AchievementFamilyRegistration>()
						{
							new AchievementFamilyRegistration(AchievementID.Squish, 1),
							new AchievementFamilyRegistration(AchievementID.BugCollector, 10),
							new AchievementFamilyRegistration(AchievementID.CatchEmAll, 100)
						}, () => GameStats.ButterfliesKilled)
				};
			for (char c = '0'; c <= '9'; c++)
			{
				Sets.Add(AchievementPredicate.KeyPredicate("NumPad" + c.ToString()));
				Sets.Add(AchievementPredicate.KeyPredicate("D" + c.ToString()));
			}
			for (char c = 'A'; c <= 'Z'; c++)
				Sets.Add(AchievementPredicate.KeyPredicate(c.ToString()));
		}

		public static void Update(GameTime gameTime)
		{
			foreach (var s in Sets)
			{
				s.Check();
			}
			Sets.RemoveAll(x => x.ShouldDie());

			while (CurrentDisplayQueue.Count != 0 && CurrentDisplayQueue.First().timeStamp != null && CurrentDisplayQueue.First().timeStamp + AchievementNotification.DISPLAY_TIME <= GameStats.TotalGameTime)
				CurrentDisplayQueue.RemoveFirst();
		}

		public static void Draw()
		{
			int i = 0;
			foreach (AchievementNotification a in CurrentDisplayQueue)
			{
				if (i++ >= AchievementNotification.DISPLAY_COUNT)
					break;

				a.Draw(i);
			}
		}

		public static Achievement GetAchievement(AchievementID id)
		{
			return Achievements[(int)id];
		}

		public static void Unlock(AchievementID id)
		{
			Achievement a = GetAchievement(id);
			if (!a.isUnlocked)
			{
				a.isUnlocked = true;
				GameStats.UnlockedAchievements++;
				DisplayUnlock(a);
			}
		}

		public static void DisplayUnlock(Achievement a)
		{
			//Game1.MostRecentAchievement = a.name;
			CurrentDisplayQueue.AddLast(new AchievementNotification(a));
		}
	}
}
