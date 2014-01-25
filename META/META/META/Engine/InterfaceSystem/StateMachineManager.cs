using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace META.Engine.InterfaceSystem
{
    public enum State
    { 
        UnpauseState,
        MainMenu,
        Paused,
        Playing,
        AchievementList
    }

    public class StateMachineManager
    {
        private static State _currentState;
        private static State _priorState;

        public static void Initialize()
        {
            _currentState = State.Playing;
            _priorState = State.UnpauseState;
        }

        public static State currentState
        { 
            get { return _currentState; }
            set 
            {
                if (value == State.Paused && _currentState != State.Paused)
                {
                    _priorState = _currentState;
                    _currentState = value; 
                }
                else if (value == State.UnpauseState)
                {
                    _currentState = _priorState;
                }   
            }
        }
        public static State priorState
        { get { return _priorState; } }
    }
}
