using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Love
{
    /// <summary>
    /// Provides an interface to the user's keyboard.
    /// </summary>
    public partial class Keyboard
    {
        /// <summary>
        /// Initialization module
        /// </summary>
        /// <returns></returns>
        public static bool Init()
        {
            return Love2dDll.wrap_love_dll_keyboard_open_love_keyboard();
        }

        /// <summary>
        /// Enables or disables key repeat for love.keypressed. It is disabled by default.
        /// <para>The interval between repeats depends on the user's system settings. This function doesn't affect whether <see cref="Scene.TextInput"/> is called multiple times while a key is held down</para>
        /// </summary>
        /// <param name="enable">Whether repeat keypress events should be enabled when a key is held down.</param>
        public static void SetKeyRepeat(bool enable)
        {
            Love2dDll.wrap_love_dll_keyboard_setKeyRepeat(enable);
        }

        /// <summary>
        /// Gets whether key repeat is enabled.
        /// </summary>
        /// <returns></returns>
        public static bool HasKeyRepeat()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_keyboard_hasKeyRepeat(out out_result);
            return out_result;
        }

        /// <summary>
        /// Checks whether a certain <see cref="KeyConstant"/> is down. Not to be confused with <see cref="Scene.KeyPressed(KeyConstant, Scancode, bool)"/> or <see cref="Scene.KeyReleased(KeyConstant, Scancode)"/>.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>True if the key is down, false if not.</returns>
        public static bool IsDown(KeyConstant key)
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_keyboard_isDown((int)key, out out_result);
            return out_result;
        }

        /// <summary>
        /// Checks whether a certain <see cref="Scancode"/> is down. Not to be confused with <see cref="Scene.KeyPressed(KeyConstant, Scancode, bool)"/> or <see cref="Scene.KeyReleased(KeyConstant, Scancode)"/>.
        /// <para>Unlike regular KeyConstants, Scancodes are keyboard layout-independent. The scancode "w" is used if the key in the same place as the "w" key on an American keyboard is pressed, no matter what the key is labelled or what the user's operating system settings are.</para>
        /// </summary>
        /// <param name="scancode"></param>
        /// <returns></returns>
        public static bool IsScancodeDown(Scancode scancode)
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_keyboard_isScancodeDown((int)scancode, out out_result);
            return out_result;
        }

        /// <summary>
        /// Gets the hardware scancode corresponding to the given key.
        /// <para>Unlike <see cref="KeyConstant"/>, <see cref="Scancode"/> are keyboard layout-independent. For example the scancode "w" will be generated if the key in the same place as the "w" key on an American keyboard is pressed, no matter what the key is labelled or what the user's operating system settings are.</para>
        /// <para><see cref="Scancode"/> are useful for creating default controls that have the same physical locations on on all systems.</para>
        /// </summary>
        /// <param name="key">The key to get the scancode from.</param>
        /// <returns>The scancode corresponding to the given key, or "unknown" if the given key has no known physical representation on the current system.</returns>
        public static Scancode GetScancodeFromKey(KeyConstant key)
        {
            int out_scancode_type = 0;
            Love2dDll.wrap_love_dll_keyboard_getScancodeFromKey((int)key, out out_scancode_type);
            return (Scancode)out_scancode_type;
        }

        /// <summary>
        /// <para>Gets the key corresponding to the given hardware scancode.</para>
        /// <para>Unlike <see cref="KeyConstant"/>, <see cref="Scancode"/> are keyboard layout-independent. For example the scancode "w" will be generated if the key in the same place as the "w" key on an American keyboard is pressed, no matter what the key is labelled or what the user's operating system settings are.</para>
        /// <para><see cref="Scancode"/> are useful for creating default controls that have the same physical locations on on all systems.</para>
        /// </summary>
        /// <param name="scancode">The scancode to get the key from.</param>
        /// <returns>The key corresponding to the given <see cref="Scancode"/> , or "unknown" if the <see cref="Scancode"/> doesn't map to a KeyConstant on the current system.</returns>
        public static KeyConstant GetKeyFromScancode(Scancode scancode)
        {
            int out_key_type = 0;
            Love2dDll.wrap_love_dll_keyboard_getKeyFromScancode((int)scancode, out out_key_type);
            return (KeyConstant)out_key_type;
        }

        /// <summary>
        /// <para>Enables or disables text input events. It is enabled by default on Windows, Mac, and Linux, and disabled by default on iOS and Android.</para>
        /// <para>On touch devices, this shows the system's native on-screen keyboard when it's enabled.</para>
        /// </summary>
        /// <param name="enable">Whether text input events should be enabled.</param>
        public static void SetTextInput(bool enable)
        {
            Love2dDll.wrap_love_dll_keyboard_setTextInput(enable);
        }

        /// <summary>
        /// <para>Enables or disables text input events. It is enabled by default on Windows, Mac, and Linux, and disabled by default on iOS and Android.</para>
        /// <para>On iOS and Android this variant tells the OS that the specified rectangle is where text will show up in the game, which prevents the system on-screen keyboard from covering the text.</para>
        /// <para>On touch devices, this shows the system's native on-screen keyboard when it's enabled.</para>
        /// </summary>
        /// <param name="enable">Whether text input events should be enabled.</param>
        /// <param name="x">Text rectangle x position.</param>
        /// <param name="y">Text rectangle y position.</param>
        /// <param name="w">Text rectangle width.</param>
        /// <param name="h">Text rectangle height.</param>
        public static void SetTextInput(bool enable, double x, double y, double w, double h)
        {
            Love2dDll.wrap_love_dll_keyboard_setTextInput_xywh(enable, x, y, w, h);
        }

        /// <summary>
        /// Gets whether key repeat is enabled.
        /// </summary>
        /// <returns>Whether key repeat is enabled.</returns>
        public static bool HasTextInput()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_keyboard_hasTextInput(out out_result);
            return out_result;
        }


        public static bool HasScreenKeyboard()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_keyboard_hasScreenKeyboard(out out_result);
            return out_result;
        }
    }

    class KeyboardHelper
    {
        readonly KeyConstant[] keyConstants = (KeyConstant[])Enum.GetValues(typeof(KeyConstant));
        readonly bool[] keyStates;
        readonly bool[] lastKeyStates;

        public KeyboardHelper()
        {
            int max = 0;
            foreach(var key in keyConstants)
            {
                int value = (int)key;
                if (value > max)
                    max = value;
            }

            keyStates = new bool[max + 1];
            lastKeyStates = new bool[max + 1];


            for (int i = 0; i < keyStates.Length; i++)
            {
                keyStates[i] = false;
                lastKeyStates[i] = false;
            }
        }

        public void Step()
        {
            foreach (var key in keyConstants)
            {
                int index = (int)key;
                lastKeyStates[index] = keyStates[index];
                keyStates[index] = Keyboard.IsDown(key);
            }
        }

        /// <summary>
        /// Checks whether a certain key is pressed.
        /// </summary>
        /// <param name="key">The key to check.</param>
        public bool IsPressed(KeyConstant key)
        {
            int index = (int)key;
            return lastKeyStates[index] == false && keyStates[index] == true;
        }

        /// <summary>
        /// Checks whether a certain key is released.
        /// </summary>
        /// <param name="key">The key to check.</param>
        public bool IsReleased(KeyConstant key)
        {
            int index = (int)key;
            return lastKeyStates[index] == true && keyStates[index] == false;
        }
    }


    public partial class Keyboard
    {
        static readonly KeyboardHelper keyboardHelper = new KeyboardHelper();

        /// <summary>
        /// Internal clled when call Boot.Run() for <see cref="IsPressed"/> and <see cref="IsReleased"/> .......
        /// </summary>
        internal static void Step()
        {
            keyboardHelper.Step();
        }

        /// <summary>
        /// Checks whether a certain key is pressed.
        /// </summary>
        /// <param name="key">The key to check.</param>
        public static bool IsPressed(KeyConstant key)
        {
            return keyboardHelper.IsPressed(key);
        }

        /// <summary>
        /// Checks whether a certain key is released.
        /// </summary>
        /// <param name="key">The key to check.</param>
        public static bool IsReleased(KeyConstant key)
        {
            return keyboardHelper.IsReleased(key);
        }
    }
}
