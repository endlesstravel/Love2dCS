using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Love
{
    static public partial class Boot
    {

        public const int RTLD_NOW = 0x002;
        [System.Runtime.InteropServices.DllImport("libdl")]
        public static extern IntPtr dlopen(string fileName, int flags);

        [System.Runtime.InteropServices.DllImport("libdl")]
        public static extern string dlerror();

        static void LoadUnixLibrary()
        {
            if (dlopen($"{Environment.CurrentDirectory}/liblove.so", RTLD_NOW) == IntPtr.Zero)
            {
                Console.WriteLine($"load library error: {dlerror()}");
            }
        }

        static void LoadWinLibrary()
        {

        }

        public static void InitNativeLibrary()
        {
            OperatingSystem os = Environment.OSVersion;
            PlatformID pid = os.Platform;
            switch (pid)
            {
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                    LoadWinLibrary();
                    break;
                case PlatformID.Unix:
                    LoadUnixLibrary();
                    break;
                default:
                    break;
            }
        }
    }
}
