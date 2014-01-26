using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace META.Engine.InterfaceSystem
{
    public enum State
    {
        MainMenu,
        Paused,
        Playing,
        AchievementList
    }

    public class StateMachineManager
    {
		public static State CurrentState;

        public static void Initialize()
        {
            CurrentState = State.Playing;
        }
    }
}
