using System;
using System.Collections.Generic;
using System.Linq;

namespace Love.Misc
{
    public static class InputBoost
    {
        public enum MouseButton
        {
            LeftButton = 0,
            RightButton = 1,
            MiddleButton = 2,
            ExtendedButton1 = 3,
            ExtendedButton2 = 4,
            ExtendedButton3 = 5,
            ExtendedButton4 = 6,
            ExtendedButton5 = 7,
        }

        #region Mouse
        const int RememberButtonCount = 32;
        static bool[] lastBtnDown = new bool[RememberButtonCount];
        static bool[] currentBtnDown = new bool[RememberButtonCount];
        public static void MouseUpdate()
        {
            for (int i = 0; i < RememberButtonCount; i++)
            {
                lastBtnDown[i] = currentBtnDown[i];
                currentBtnDown[i] = Mouse.IsDown(i);
            }
        }
        public static MouseButton[] GetMouseDown()
        {
            List<MouseButton> list = new List<MouseButton>();
            MouseButton[] mouseButton = (MouseButton[])System.Enum.GetValues(typeof(MouseButton));
            for (int i = 0; i < mouseButton.Length; i++)
            {
                if (currentBtnDown[i])
                    list.Add((MouseButton)i);
            }
            return list.ToArray();
        }
        public static MouseButton[] GetMouseDownPrevious()
        {
            List<MouseButton> list = new List<MouseButton>();
            MouseButton[] mouseButton = (MouseButton[])System.Enum.GetValues(typeof(MouseButton));
            for (int i = 0; i < mouseButton.Length; i++)
            {
                if (lastBtnDown[i])
                    list.Add((MouseButton)i);
            }
            return list.ToArray();
        }
        public static bool IsMouseDown(MouseButton button)
        {
            return currentBtnDown[(int)button];
        }
        public static bool IsMouseDownPrevious(MouseButton button)
        {
            return lastBtnDown[(int)button];
        }
        #endregion


        #region Keyboard
        static HashSet<KeyConstant> lastKeyboard = new HashSet<KeyConstant>();
        static HashSet<KeyConstant> currentKeyboard = new HashSet<KeyConstant>();
        public static void KeyboardUpdate()
        {
            lastKeyboard = currentKeyboard;
            currentKeyboard = new HashSet<KeyConstant>();
            foreach (var key in (KeyConstant[])System.Enum.GetValues(typeof(KeyConstant)))
            {
                if (Keyboard.IsDown(key))
                {
                    currentKeyboard.Add(key);
                }
            }
        }
        public static KeyConstant[] GetKeyboardDown()
        {
            return currentKeyboard.ToArray();
        }
        public static KeyConstant[] GetKeyboardPrevious()
        {
            return lastKeyboard.ToArray();
        }
        public static bool IsKeyboardDown(KeyConstant key)
        {
            return currentKeyboard.Contains(key);
        }
        public static bool IsKeyboardPrevious(KeyConstant key)
        {
            return lastKeyboard.Contains(key);
        }
        #endregion


        #region Joystick button
        static HashSet<string> lastGamePadPressedMemory = new HashSet<string>();
        static HashSet<string> currentGamePadPressedMemory = new HashSet<string>();
        public static void JoystickButtonUpdate()
        {
            lastGamePadPressedMemory = currentGamePadPressedMemory;
            currentGamePadPressedMemory = new HashSet<string>();
            foreach (var joy in Joystick.GetJoysticks())
            {
                string guid = joy.GetGUID();
                for (int i = 0; i < joy.GetButtonCount(); i++)
                {
                    if (joy.IsDown(i))
                    {
                        currentGamePadPressedMemory.Add(guid + i);
                    }
                }
                foreach (var gbtn in (GamepadButton[])System.Enum.GetValues(typeof(GamepadButton)))
                {
                    if (joy.IsGamepadDown(gbtn))
                    {
                        currentGamePadPressedMemory.Add(guid + gbtn);
                    }
                }
            }
        }
        public static GamepadButton[] GetGamepadDown(this Joystick joystick)
        {
            List<GamepadButton> list = new List<GamepadButton>();
            foreach (var gbtn in (GamepadButton[])System.Enum.GetValues(typeof(GamepadButton)))
            {
                string name = joystick.GetGUID() + gbtn;
                if (currentGamePadPressedMemory.Contains(name))
                {
                    list.Add(gbtn);
                }
            }
            return list.ToArray();
        }
        public static GamepadButton[] GetGamepadDownPrevious(this Joystick joystick)
        {
            List<GamepadButton> list = new List<GamepadButton>();
            foreach (var gbtn in (GamepadButton[])System.Enum.GetValues(typeof(GamepadButton)))
            {
                string name = joystick.GetGUID() + gbtn;
                if (lastGamePadPressedMemory.Contains(name))
                {
                    list.Add(gbtn);
                }
            }
            return list.ToArray();
        }
        public static bool IsGamepadDown(this Joystick joystick, GamepadButton gbtn)
        {
            string name = joystick.GetGUID() + gbtn;
            return currentGamePadPressedMemory.Contains(name);
        }
        public static bool IsGamepadDownPrevious(this Joystick joystick, GamepadButton gbtn)
        {
            string name = joystick.GetGUID() + gbtn;
            return currentGamePadPressedMemory.Contains(name);
        }
        #endregion
        static Dictionary<Joystick, AnxisValue> lastAxisMemory = new Dictionary<Joystick, AnxisValue>();
        static Dictionary<Joystick, AnxisValue> currentAxisMemory = new Dictionary<Joystick, AnxisValue>();

        public struct AnxisValue
        {
            readonly public float LeftX;
            readonly public float LeftY;
            readonly public float RightX;
            readonly public float RightY;
            readonly public float TriggerLeft;
            readonly public float TriggerRight;

            public AnxisValue(float leftX, float leftY, float rightX, float rightY, float triggerLeft, float triggerRight)
            {
                LeftX = leftX;
                LeftY = leftY;
                RightX = rightX;
                RightY = rightY;
                TriggerLeft = triggerLeft;
                TriggerRight = triggerRight;
            }
        }

        public static void JoystickAxisUpdate()
        {
            lastAxisMemory = currentAxisMemory;
            currentAxisMemory = new Dictionary<Joystick, AnxisValue>();
            foreach (var joy in Joystick.GetJoysticks())
            {
                currentAxisMemory[joy] = new AnxisValue(
                    joy.GetGamepadAxis(GamepadAxis.LeftX),
                    joy.GetGamepadAxis(GamepadAxis.LeftY),
                    joy.GetGamepadAxis(GamepadAxis.RightX),
                    joy.GetGamepadAxis(GamepadAxis.RightY),
                    joy.GetGamepadAxis(GamepadAxis.TriggerLeft),
                    joy.GetGamepadAxis(GamepadAxis.TriggerRight)
                    );
            }
        }

        public static AnxisValue GetGamepadAxis(this Joystick joy)
        {
            return currentAxisMemory.TryGetValue(joy, out var value) ? value : new AnxisValue();
        }
        public static AnxisValue GetGamepadAxisPrevious(this Joystick joy)
        {
            return lastAxisMemory.TryGetValue(joy, out var value) ? value : new AnxisValue();
        }

        public static bool IsGamepadAxisChange(this Joystick joy, GamepadAxis axis)
        {
            if (currentAxisMemory.TryGetValue(joy, out var currValue) && lastAxisMemory.TryGetValue(joy, out var lastValue))
            {
                switch (axis)
                {
                    case GamepadAxis.Invalid: return false;
                    case GamepadAxis.LeftX: return currValue.LeftX != lastValue.LeftX;
                    case GamepadAxis.LeftY: return currValue.LeftY != lastValue.LeftY;
                    case GamepadAxis.RightX: return currValue.RightX != lastValue.RightX;
                    case GamepadAxis.RightY: return currValue.RightY != lastValue.RightY;
                    case GamepadAxis.TriggerLeft: return currValue.TriggerLeft != lastValue.TriggerLeft;
                    case GamepadAxis.TriggerRight: return currValue.TriggerRight != lastValue.TriggerRight;
                }
            }

            return false;
        }

        public static void Step()
        {
            MouseUpdate();
            KeyboardUpdate();
            JoystickButtonUpdate();
            JoystickAxisUpdate();
        }
    }
}
