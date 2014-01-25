using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace META
{
    public struct PlayerButton
    {
        public Buttons button;
        public uint player;

        public static bool operator ==(PlayerButton lhs, PlayerButton rhs)
        {
            return lhs.player == rhs.player && lhs.button == rhs.button;
        }
        public static bool operator !=(PlayerButton lhs, PlayerButton rhs)
        {
            return !(lhs == rhs);
        }
		public override bool Equals(object obj)
		{
			if (!(obj is PlayerButton))
				return false;

			return this == (PlayerButton)obj;
		}
		public override int GetHashCode()
		{
			int hash = 17;
			hash = hash * 23 + player.GetHashCode();
			hash = hash * 23 + button.GetHashCode();
			return hash;
		}

        public PlayerButton(uint player, Buttons button)
        {
            this.player = player;
            this.button = button;
        }
    }

    public class Command
    {
        public string name;
        public List<Keys> keys;
        public List<PlayerButton> buttons;

        public Command(string _name)
        {
            name = _name;
            keys = new List<Keys>();
            buttons = new List<PlayerButton>();
        }

        public bool HasKey(Keys key)
        {
            foreach (Keys k in keys)
                if (k == key)
                    return true;
            return false;
        }

        public bool HasButton(PlayerButton playerButton)
        {
            foreach (PlayerButton pb in buttons)
                if (pb == playerButton)
                    return true;
            return false;
        }

        public bool HasButton(uint player, Buttons button)
        {
            foreach (PlayerButton pb in buttons)
                if (pb.player == player && pb.button == button)
                    return true;
            return false;
        }
    }

    public static class InputManager
    {
        public enum Device
        {
            None,
            Keyboard,
            GamePad
        }

        private static List<Command> commands;

		private static KeyboardState lastKeyState;
		public static KeyboardState CurrentKeyState { get; private set; }

		private static GamePadState[] lastPadState;
		public static GamePadState[] CurrentPadState { get; private set; }

        public static Device LastInputDevice { get; private set; }

        public static void Initialize()
        {
            commands = new List<Command>();
            lastKeyState = Keyboard.GetState();
            CurrentKeyState = lastKeyState;

            lastPadState = new GamePadState[4];
            CurrentPadState = new GamePadState[4];
            lastPadState[0] = GamePad.GetState(PlayerIndex.One);
            lastPadState[1] = GamePad.GetState(PlayerIndex.Two);
            lastPadState[2] = GamePad.GetState(PlayerIndex.Three);
            lastPadState[3] = GamePad.GetState(PlayerIndex.Four);
            for (int i = 0; i < 4; i++)
                CurrentPadState[i] = lastPadState[i];

            #if XBOX
                LastInputDevice = Device.GamePad;
            #else
                LastInputDevice = Device.Keyboard;
            #endif
            
        }

        public static void Update()
        {
            lastKeyState = CurrentKeyState;
            CurrentKeyState = Keyboard.GetState();

            if (CurrentKeyState.GetPressedKeys().Length > 0)
                LastInputDevice = Device.Keyboard;

            for (int i = 0; i < 4; i++)
                lastPadState[i] = CurrentPadState[i];
            CurrentPadState[0] = GamePad.GetState(PlayerIndex.One);
            CurrentPadState[1] = GamePad.GetState(PlayerIndex.Two);
            CurrentPadState[2] = GamePad.GetState(PlayerIndex.Three);
            CurrentPadState[3] = GamePad.GetState(PlayerIndex.Four);

            for (int i = 0; i < 4; i++)
                if (CurrentPadState[i].Buttons != new GamePadButtons())
                    LastInputDevice = Device.GamePad;
        }

		public static List<Keys> GetKeysDown()
		{
			List<Keys> output = new List<Keys>();
			Keys[] down = CurrentKeyState.GetPressedKeys();

			foreach (Keys key in down)
				if (GetKeyDown(key))
					output.Add(key);

			return output;
		}

		public static List<Keys> GetKeys()
		{
			return CurrentKeyState.GetPressedKeys().ToList();
		}

		public static List<Keys> GetKeysUp()
		{
			List<Keys> output = new List<Keys>();
			Keys[] down = lastKeyState.GetPressedKeys();

			foreach (Keys key in down)
				if (GetKeyUp(key))
					output.Add(key);

			return output;
		}

        public static bool GetKeyDown(Keys key)
        {
            return lastKeyState.IsKeyUp(key) && CurrentKeyState.IsKeyDown(key);
        }
        public static bool GetKeyUp(Keys key)
        {
            return lastKeyState.IsKeyDown(key) && CurrentKeyState.IsKeyUp(key);
        }
        public static bool GetKey(Keys key)
        {
            return CurrentKeyState.IsKeyDown(key);
        }

        public static bool GetButtonDown(uint player, Buttons button)
        {
            return lastPadState[player - 1].IsButtonUp(button) && CurrentPadState[player - 1].IsButtonDown(button);
        }
        public static bool GetButtonUp(uint player, Buttons button)
        {
            return lastPadState[player - 1].IsButtonDown(button) && CurrentPadState[player - 1].IsButtonUp(button);
        }
        public static bool GetButton(uint player, Buttons button)
        {
            return CurrentPadState[player - 1].IsButtonDown(button);
        }

        public static bool GetButtonDown(PlayerButton playerButton)
        {
            return GetButtonDown(playerButton.player, playerButton.button);
        }
        public static bool GetButtonUp(PlayerButton playerButton)
        {
            return GetButtonUp(playerButton.player, playerButton.button);
        }
        public static bool GetButton(PlayerButton playerButton)
        {
            return GetButton(playerButton.player, playerButton.button);
        }

        public static bool GetCommandDown(string name)
        {
            Command c = GetCommandByName(name);
			if (c == null)
				return false;

            foreach (Keys key in c.keys)
                if (GetKeyDown(key))
                    return true;
            foreach (PlayerButton pb in c.buttons)
                if (GetButtonDown(pb))
                    return true;
            return false;
        }
        public static bool GetCommandUp(string name)
        {
            Command c = GetCommandByName(name);
            foreach (Keys key in c.keys)
                if (GetKeyUp(key))
                    return true;
            foreach (PlayerButton pb in c.buttons)
                if (GetButtonUp(pb))
                    return true;
            return false;
        }
        public static bool GetCommand(string name)
        {
            Command c = GetCommandByName(name);
            foreach (Keys key in c.keys)
                if (GetKey(key))
                    return true;
            foreach (PlayerButton pb in c.buttons)
                if (GetButton(pb))
                    return true;
            return false;
        }
        
        private static Command GetCommandByName(string name)
        {
            foreach (Command c in commands)
                if (c.name == name)
                    return c;
            return null;
        }

        public static string CommandNameForPlayer(string name, uint player)
        {
            if (player < 1 || player > 4)
                return "";

            return name + "-P" + player;
        }

        public static bool GetCommandForPlayerDown(string name, uint player)
        {
            return GetCommandDown(CommandNameForPlayer(name, player));
        }
        public static bool GetCommandForPlayerUp(string name, uint player)
        {
            return GetCommandUp(CommandNameForPlayer(name, player));
        }
        public static bool GetCommandForPlayer(string name, uint player)
        {
            return GetCommand(CommandNameForPlayer(name, player));
        }

        public static void AddCommand(string name, Keys key)
        {
            Command c = GetCommandByName(name);
            if (c != null && !c.HasKey(key))
                c.keys.Add(key);
            else if (GetCommandByName(name) == null)
            {
                commands.Add(new Command(name));
                commands.Last().keys.Add(key);
            }
        }
        public static void RemoveCommand(string name, Keys key)
        {
            Command c = GetCommandByName(name);
            if (c != null && c.HasKey(key))
            {
                c.keys.Remove(key);
                if (c.keys.Count <= 0 && c.buttons.Count <= 0)
                    commands.Remove(c);
            }
        }

        public static void AddCommand(string name, Buttons button, uint player = 1)
        {
            Command c = GetCommandByName(name);
            if (c != null && !c.HasButton(player, button))
                c.buttons.Add(new PlayerButton(player, button));
            else if (c == null)
            {
                commands.Add(new Command(name));
                commands.Last().buttons.Add(new PlayerButton(player, button));
            }
        }
        public static void RemoveCommand(string name, Buttons button, uint player = 1)
        {
            Command c = GetCommandByName(name);
            if (c != null && c.HasButton(player, button))
                c.buttons.Remove(new PlayerButton(player, button));
        }

        public static void AddCommand(string name, Keys key, Buttons button, uint player = 1)
        {
            AddCommand(name, key);
            AddCommand(name, button);
        }

        public static void RemoveCommand(string name)
        {
            Command c = GetCommandByName(name);
            if (c != null)
                commands.Remove(c);
        }

        public static void AddCommandForPlayer(string name, Buttons button, uint player)
        {
            AddCommand(CommandNameForPlayer(name, player), button, player);
        }
        public static void RemoveCommandForPlayer(string name, Buttons button, uint player)
        {
            RemoveCommand(CommandNameForPlayer(name, player), button, player);
        }
        public static void RemoveCommandForPlayer(string name, uint player)
        {
            RemoveCommand(CommandNameForPlayer(name, player));
        }

        public static void AddCommandForAllPlayers(string name, Buttons button)
        {
            for (uint i = 1; i <= 4; i++)
            {
                AddCommandForPlayer(name, button, i);
            }
        }
        public static void RemoveCommandForAllPlayers(string name, Buttons button)
        {
            for (uint i = 1; i <= 4; i++)
            {
                RemoveCommandForPlayer(name, button, i);
            }
        }
        public static void RemoveCommandForAllPlayers(string name)
        {
            for (uint i = 1; i <= 4; i++)
            {
                RemoveCommandForPlayer(name, i);
            }
        }
    }
}
