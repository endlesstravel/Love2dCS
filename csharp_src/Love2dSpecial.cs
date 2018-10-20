using System;

namespace Love
{
    public class Special
    {
        /// <summary>
        /// get win32 HANDLE on windows platform
        /// </summary>
        /// <returns></returns>
        static public IntPtr GetWin32Handle()
        {
            IntPtr p = IntPtr.Zero;
            Love2dDll.inner_wrap_love_dll_get_win32_handle(out p);
            return p;
        }
    }
}
