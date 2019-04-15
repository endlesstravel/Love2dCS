using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Love
{
    /// <summary>
    /// The basic state of the system's power supply.
    /// </summary>
    public enum PowerState: int 
    {
        /// <summary>
        /// Cannot determine power status.
        /// </summary>
        Unknow = 0,
        /// <summary>
        /// Not plugged in, running on a battery.
        /// </summary>
        Battery,

        /// <summary>
        /// Plugged in, no battery available.
        /// </summary>
        NoBattery,

        /// <summary>
        /// Plugged in, charging battery.
        /// </summary>
        Charging,

        /// <summary>
        /// Plugged in, battery is fully charged.
        /// </summary>
        Charged,

        /// <summary>
        /// invalid enum
        /// </summary>
        MaxEnum
    };

    /// <summary>
    /// Provides access to information about the user's system.
    /// </summary>
    public partial class Special
    {
        /// <summary>
        /// Initialization module
        /// </summary>
        /// <returns></returns>
        public static bool Init()
        {
            return Love2dDll.wrap_love_dll_system_open_love_system_module();
        }

        /// <summary>
        /// Gets the current operating system. In general, LÖVE abstracts away the need to know the current operating system, but there are a few cases where it can be useful (especially in combination with os.execute.)
        /// </summary>
        public static string GetOS()
        {
            Love2dDll.wrap_love_dll_system_getOS(out var out_str);
            return DllTool.WSToStringAndRelease(out_str);
        }
        /// <summary>
        /// Gets the amount of logical processor in the system.
        /// </summary>
        public static int GetProcessorCount()
        {
            Love2dDll.wrap_love_dll_system_getProcessorCount(out int count);
            return count;
        }

        /// <summary>
        /// Puts text in the clipboard.
        /// </summary>
        /// <returns></returns>
        public static void SetClipboardText(string text)
        {
            Love2dDll.wrap_love_dll_system_setClipboardText(DllTool.GetNullTailUTF8Bytes(text));
        }

        /// <summary>
        /// Gets text from the clipboard.
        /// </summary>
        public static string GetClipboardText()
        {
            Love2dDll.wrap_love_dll_system_getClipboardText(out var out_str);
            return DllTool.WSToStringAndRelease(out_str);
        }

        /// <summary>
        /// Gets information about the system's power supply.
        /// </summary>
        /// <param name="state">The basic state of the power supply.</param>
        /// <param name="percent">Percentage of battery life left, between 0 and 100. nil if the value can't be determined or there's no battery.</param>
        /// <param name="seconds">Seconds of battery life left. nil if the value can't be determined or there's no battery.</param>
        public static void GetPowerInfo(out PowerState state, out int percent, out int seconds)
        {
            percent = -1;
            seconds = -1;
            Love2dDll.wrap_love_dll_system_getPowerInfo(out int stateType, out percent, out seconds);
            try{ state = (PowerState)stateType; } catch { state = PowerState.Unknow; }
        }

        /// <summary>
        /// Opens a URL with the user's web or file browser.
        /// </summary>
        /// <param name="url">The URL to open. Must be formatted as a proper URL.</param>
        /// <returns>Whether the URL was opened successfully.</returns>
        public static bool OpenURL(string url)
        {
            Love2dDll.wrap_love_dll_system_openURL(DllTool.GetNullTailUTF8Bytes(url), out var result);
            return result;
        }
    }

    public partial class Special
    {
        /// <summary>
        /// Get win32 HANDLE on windows platform
        /// </summary>
        /// <returns></returns>
        static public IntPtr GetWin32Handle()
        {
            if ("Windows".Equals(GetOS()) == false)
            {
                throw new Exception("can only execute this function on Windows platform");
            }

            IntPtr p = IntPtr.Zero;
            Love2dDll.inner_wrap_love_dll_get_win32_handle(out p);
            return p;
        }
    }
}
