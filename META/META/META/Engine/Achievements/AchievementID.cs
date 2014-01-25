using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace META.Engine.Achievements
{
	public enum AchievementID
	{
		HelloWorld,				// Started the game
		GoodbyeCruelWorld,		// Closed the game
		AbortMission,			// Crashed the game
		Tautology,				// Collect an achievement
		AchievementAchieved,	// Collect 10 achievements
		Yo,						// 20...
		Dawg,
		We,
		Herd,
		U,
		Like,
		Achievements,
		So,
		Here,
		Are,
		Some,					// ... 30
		Almost,					// 40
		LifeTheUniverseAndEverything,// 42
		OneForEveryShade,		// 50
		ZeroToSixty,			// 60
		HeHeHe,					// 69
		SixtyNinePlusOne,		// 70
		FourTwenties,			// 80
		PenultimateVictory,		// All but one (this being the last)
		ParticipationRibbon,	// Pressed start to begin
		LockedNLoaded,			// Level loaded
        TheLongHaul,			// Played 1 second
		ThisIsGettingExhausting,// Played 30 seconds
        DoYouHaveNothingBetterToDo,// Played 1 minute
		H4rdc0r3G4m3r,		// Played 10 minutes
		YouShouldGoOutside,		// Played 30 minutes
		SeriouslyThisCantBeHealthy,// Played 1 hour
		ItsaMe,					// Jumped once
		LetsBounce,				// Jumped 10 times
		ThisIsNotAFlightSimulator,// Jumped 100 times
		PayAttention,			// Idle 1 second
		OnePlayerGame,			// Idle 30 seconds
		OneIsTheLoneliestNumber,// Idle 1 minute
		AreYouStillThere,		// Idle 5 minutes
		ItsABird,				// Air time 1 second
		ItsAPlane,				// Air time 30 seconds
		LikeAGazelle,			// Air time 1 minute
		PilotingABlimp,			// Air time 5 minutes
		PCMasterRace,			// Used keyboard
		ConsoleFanboy,			// Used gamepad
		TheButtonsTheyDoNothing,// Used mouse
		PleaseComeBack,			// Paused
		IMissedYou,				// Unpaused
		ICanChange,				// Paused 1 second
		YouCanBlameItAllOnMe,	// Paused 30 seconds
		RecklessAbandon,		// Paused 1 minute
		ThinkerNotADoer,		// Pause 5 minutes
		LetMeFocus,				// Mute
		ItsAMiracle,			// Unmute
		Backtracking,			// Walk left
		Progress,				// Walk right
		FistsAndElbows,			// Change directions a bunch
		ARegularRichardSimmons,	// Walk 100 units
		ToFallDownAtYourDoor,	// Walk 500 units
		SimplyWalkedIntoMordor,	// Walk 1000 units
		FiveThundred,			// Walk 5000 units
		WhatDoesTheScouterSay,	// Walk 9001 units
		WrongWayDumbass,		// Die off the left side
		FatalError,				// Died
		Tenacious,				// Died 10 times
		DoesntKnowWhenToQuit,	// Died 100 times
		Genocide,				// Died 1000 times
		JustLikeNew,			// Respawn
		GoingDown,				// Fall in a pit
		HighwayToHell,			// Fall in 10 pits
		OhGravityThouArtAHeartlessBitch,		// Fall in 100 pits
		ThisKillsThePlayer,		// Spiked
		EveryTime,				// Spiked 10 times
		NoExceptions,			// Spiked 100 times
		SuperNiceTry,			// Konami code
		NotCompletelyUseless,	// Level complete
		PracticeMakesPerfect,	// Level complete 10 times
		OneTrickPony,			// Level complete 100 times
		Retrograde,				// Complete backwards
		YouGetAStar,			// Complete in under 5 minutes
		Brisk,					// Complete under 2 minutes
		GottaGoFast,			// Under 1 minute completion time
		ArribaArribaAndale,		// Under 30 second completion time
		SlowAsMolasses,						// Over 1 minute completion time
		SlowAsMolassesInJanuary,			// Over 2 minute completion time
		SlowAsMolassesInJanuaryGoingUphill,	// Over 5 minute completion time
		LifeOnTheEdge,				// Stand on the very edge of a platform
		BoomHeadshot,				// Hit your head
		DumbItalianPlumberSyndrome,	// Hit your head 10 times
		Concussed,					// Hit your head 100 times
		ItsAFeature,				// Find a bug
		Count
	}
}
