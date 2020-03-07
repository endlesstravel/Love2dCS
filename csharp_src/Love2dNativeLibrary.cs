using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using File = System.IO.File;
using FileInfo = System.IO.FileInfo;
using System.Text;
using System.Diagnostics;

namespace Love
{
    static public partial class Boot
    {
        static void LoadUnixLibrary()
        {
            UnixNativeLibraryLoader.Load();
        }
        static void LoadMacLibrary()
        {
            MacNativeLibraryLoader.Load();
        }
        static void LoadWinLibrary()
        {
#if DEBUG
            WindowsNativeLibraryLoader.DebugLoad();
            Log.Error("Debug Load Mode Error !");
            return;
#endif

            WindowsNativeLibraryLoader.Load();
        }
        static IntPtr GetUnixLibraryFunc(string name)
        {
            return UnixNativeLibraryLoader.DynamciLoadFunction(name);
        }
        static IntPtr GetMacLibraryFunc(string name)
        {
            return MacNativeLibraryLoader.DynamciLoadFunction(name);
        }
        static IntPtr GetWinLibraryFunc(string name)
        {
            return WindowsNativeLibraryLoader.DynamciLoadFunction(name);
        }


        static bool IsMacOSFlag = false;
        public static bool TestIsMacOS()
        {
            bool isMacFlag = false;
            var thg = new Thread(() =>
            {
                var proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "uname",
                        Arguments = "-a",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };
                proc.Start();
                List<char> list = new List<char>();
                while (!proc.StandardOutput.EndOfStream && (list.Count < 20))
                {
                    unchecked
                    {
                        list.Add((char)proc.StandardOutput.Read());
                    }
                }

                var info = new string(list.ToArray());
                isMacFlag = info.ToLower().Contains("darwin");
            });

            thg.Start();
            thg.Join(10 * 1000); // time out failed !
            return isMacFlag;
        }

        public static IntPtr GetLibraryFunc(string name)
        {
            OperatingSystem os = Environment.OSVersion;
            PlatformID pid = os.Platform;
            switch (pid)
            {
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                    return GetWinLibraryFunc(name);
                case PlatformID.Unix:
                    if (IsMacOSFlag)
                    {
                        return GetMacLibraryFunc(name);
                    }
                    else
                    {
                        return GetUnixLibraryFunc(name);
                    }
                default:
                    throw new Exception("unsupport platform : " + pid);
            }
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
                    IsMacOSFlag = TestIsMacOS();
                    if (IsMacOSFlag)
                    {
                        LoadMacLibrary();
                    }
                    else
                    {
                        LoadUnixLibrary();
                    }
                    break;
                default:
                    throw new Exception("unsupport platform : " + pid);
            }
        }
    }

    static class WindowsNativeLibraryLoader
    {
        #region Process Info
        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEM_INFO
        {
            public ushort wProcessorArchitecture;   //public uint dwOemId;
            public ushort wReserved;   //public uint dwOemId;
            public uint dwPageSize;
            public uint lpMinimumApplicationAddress;
            public uint lpMaximumApplicationAddress;
            public uint dwActiveProcessorMask;
            public uint dwNumberOfProcessors;
            public uint dwProcessorType;
            public uint dwAllocationGranularity;
            public uint dwProcessorLevel;
            public uint dwProcessorRevision;
        }

        const ushort PROCESSOR_ARCHITECTURE_AMD64 = 9,
            PROCESSOR_ARCHITECTURE_ARM = 5,
            PROCESSOR_ARCHITECTURE_ARM64 = 12,
            PROCESSOR_ARCHITECTURE_INTEL = 0
            ;

        [DllImport("kernel32")]
        static extern void GetSystemInfo(ref SYSTEM_INFO pSI);
        public static Architecture ProcessorArchitecture
        {
            get
            {
                SYSTEM_INFO pSI = new SYSTEM_INFO();
                GetSystemInfo(ref pSI);

                switch (pSI.wProcessorArchitecture)
                {
                    case PROCESSOR_ARCHITECTURE_INTEL: return Architecture.x86;
                    case PROCESSOR_ARCHITECTURE_AMD64: return Architecture.x64;
                    case PROCESSOR_ARCHITECTURE_ARM: return Architecture.ARM;
                    case PROCESSOR_ARCHITECTURE_ARM64: return Architecture.ARM64;
                    default: return Architecture.Unknow;
                }
            }
        }
        public enum Architecture
        {
            x86,
            /// <summary>
            /// x64 (AMD or Intel)
            /// </summary>
            x64,
            /// <summary>
            ///
            /// </summary>
            ARM,
            /// <summary>
            ///
            /// </summary>
            ARM64,

            Unknow,
        }
        #endregion

        const string native_lib_win_x64 = "native_lib_win_x64";
        const string native_lib_win_x86 = "native_lib_win_x86";

        public static void DebugLoad()
        {
            var libPtr = LoadLibrary("love.dll");
            if (libPtr == IntPtr.Zero)
            {
                var errorInfo = $"error when load love.dll  " + Environment.CurrentDirectory;
                Log.Error(errorInfo);
                throw new Exception(errorInfo);
            }

            LoveLibraryPtr = libPtr;
        }

        public static void Load()
        {
            var parcitecture = ProcessorArchitecture;
            string dllStringTip = "";
            if (parcitecture == Architecture.x86)
            {
                dllStringTip = native_lib_win_x86;
            }
            else if (parcitecture == Architecture.x64)
            {
                dllStringTip = native_lib_win_x64;
            }
            else
            {
                throw new Exception("Love2DCS unsupport Processor Architecture " + parcitecture);
            }
            Log.Info(dllStringTip);


            var assem = Assembly.GetExecutingAssembly();
            var an = assem.GetName();
            var asseName = an.Name + ".";
            var dirName = new DirectoryInfo(Path.Combine(Path.GetTempPath(), $"{NativlibTool.GetHashAssembly()}.{an.ProcessorArchitecture}.{an.Version}"));
            // 1. set env
            {
                if (!dirName.Exists)
                {
                    dirName.Create();
                }

                // no need to set env now...
                //var path = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Process) ?? String.Empty;
                //if (path.Split(';').All(pathPiece => pathPiece != dirName.FullName))
                //{
                //    Environment.SetEnvironmentVariable("PATH", String.Join(";", dirName, path), EnvironmentVariableTarget.Process);
                //}
            }

            // 2. uzip
            {
                var allNames = assem.GetManifestResourceNames();
                var name = allNames.FirstOrDefault(item => item.Contains(dllStringTip));
                if (name != null)
                {
                    //Log.Info("lib --------------------------------------- begin ");
                    //Log.Info(dirName);
                    //Log.Info("lib --------------------------------------- end ");
                }
                else
                {
                    var errorInfo = dllStringTip + " not founded in Embedded Resource ! May be incorrect platform package Love2dCS-win|ubuntu|mac";
                    Log.Error(errorInfo);
                    throw new Exception(errorInfo);
                }

                NativlibTool.UnzipEmbeddedResourceToDir(assem, name, dirName.FullName);
            }

            // 3. load library
            {
                var LOVE_LIB_NAME = "love.dll";
                var winLibTable = new string[]
                {
                    "SDL2.dll",
                    "OpenAL32.dll",
                    "mpg123.dll",
                    "lua51.dll",
                    LOVE_LIB_NAME,
                };
                foreach (var libname in winLibTable)
                {
                    var fileToLoadPath = dirName.FullName + "/" + libname;
                    var libPtr = LoadLibrary(fileToLoadPath);
                    if (libPtr == IntPtr.Zero)
                    {
                        var errorInfo = $"load lib error: {fileToLoadPath} {Marshal.GetLastWin32Error()}";
                        Log.Error(errorInfo);
                        throw new Exception(errorInfo);
                    }

                    if (libname == LOVE_LIB_NAME)
                    {
                        LoveLibraryPtr = libPtr;
                    }
                }
            }


        }

        static IntPtr LoveLibraryPtr = IntPtr.Zero;
        public static IntPtr DynamciLoadFunction(string functionName)
        {
            return GetProcAddress(LoveLibraryPtr, functionName);
        }

        //[DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "LoadLibraryW", ExactSpelling = true, SetLastError = true, CharSet = CharSet.Unicode)]
        //private static extern IntPtr LoadLibrary(string lpFileName);


        [DllImport("kernel32")]
        public static extern IntPtr LoadLibrary(string fileName);
        [DllImport("kernel32")]
        public static extern IntPtr GetProcAddress(IntPtr module, string procName);
        [DllImport("kernel32")]
        public static extern int FreeLibrary(IntPtr module);
    }

    static class UnixNativeLibraryLoader
    {
        // public const int RTLD_LAZY = 0x001;
        public const int RTLD_NOW = 0x002;
        [DllImport("libdl")]
        public static extern IntPtr dlopen(string fileName, int flags);
        [DllImport("libdl")]
        public static extern string dlerror();
        [DllImport("libdl")]
        public static extern IntPtr dlsym(IntPtr handle, string name);

        const string ZIP_FILE_NAME = "native_lib_linux_x64.zip";
        
        /// linux下递归列出目录下的所有文件名（不包括目录），并且去掉空行
        /// ls -lR |grep -v ^d|awk '{print $9}' |tr -s '\n'
        public static void Load()
        {

            // 1. Extract all DLL (embedded resources) into the a temporary directory:
            var assem = Assembly.GetExecutingAssembly();
            var an = assem.GetName();
            var asseName = an.Name + ".";
            var dirName = new DirectoryInfo(Path.Combine(Path.GetTempPath(), $"{NativlibTool.GetHashAssembly()}{an.ProcessorArchitecture}.{an.Version}"));
            {
                if (!dirName.Exists)
                {
                    dirName.Create();
                }

                // Log To Delete:
                Log.Info(dirName);

                var names = assem.GetManifestResourceNames();
                var name = names.FirstOrDefault(item => item.EndsWith(ZIP_FILE_NAME));
                if (name != null)
                {
                    //Log.Info("lib --------------------------------------- begin ");
                    //Log.Info("lib " + names.Length);
                    //Log.Info(string.Join("\n", names));
                    //Log.Info("lib --------------------------------------- end ");
                }
                else
                {
                    var errorInfo = ZIP_FILE_NAME + " not founded in Embedded Resource ! May be incorrect platform package, Try Love2dCS-win";
                    Log.Error(errorInfo);
                    throw new Exception(errorInfo);
                }

                // extract unzip file to temp
                NativlibTool.UnzipEmbeddedResourceToDir(assem, name, dirName.FullName);
            }

            // 2. load the native library
            {
                var libLove2d_so_filename = $"usr/lib/liblove-11.3.so";
                var linuxLibTableArray = new string[]
                {
                    "libstdc++/libstdc++.so.6",
                    "lib/x86_64-linux-gnu/libgcc_s.so.1",
                    "lib/x86_64-linux-gnu/libz.so.1",
                    "lib/x86_64-linux-gnu/libpng12.so.0",

                    "usr/lib/x86_64-linux-gnu/libatomic.so.1",
                    "usr/lib/x86_64-linux-gnu/libtheoradec.so.1",
                    "usr/lib/x86_64-linux-gnu/libvorbis.so.0",
                    "usr/lib/x86_64-linux-gnu/libvorbisfile.so.3",
                    "usr/lib/x86_64-linux-gnu/libogg.so.0",
                    "usr/lib/x86_64-linux-gnu/libfreetype.so.6",

                    "usr/lib/libSDL2-2.0.so.0",
                    "usr/lib/libluajit-5.1.so.2",
                    "usr/lib/libmodplug.so.1",
                    "usr/lib/x86_64-linux-gnu/libopenal.so.1",
                    "usr/lib/x86_64-linux-gnu/libmpg123.so.0",

                    libLove2d_so_filename,

                };
                foreach (var libname in linuxLibTableArray)
                {
                    var path = dirName.FullName + "/" + libname;
                    var dlPtr = dlopen(path, RTLD_NOW);
                    if (dlPtr == IntPtr.Zero)
                    {
                        var errorInfo = $"load library '{path}' error: [{dlerror()}]";
                        Log.Error(errorInfo);
                        throw new Exception(errorInfo);
                    }

                    if (libname == libLove2d_so_filename)
                    {
                        LOVE_LIB_PTR = dlPtr;
                    }
                }
            }
        }

        static IntPtr LOVE_LIB_PTR = IntPtr.Zero;

        public static IntPtr DynamciLoadFunction(string name)
        {
            return dlsym(LOVE_LIB_PTR, name);
        }
    }

    static class MacNativeLibraryLoader
    {

        // public const int RTLD_LAZY = 0x001;
        public const int RTLD_NOW = 0x002;
        [DllImport("libdl")]
        public static extern IntPtr dlopen(string fileName, int flags);
        [DllImport("libdl")]
        public static extern string dlerror();
        [DllImport("libdl")]
        public static extern IntPtr dlsym(IntPtr handle, string name);

        const string ZIP_FILE_NAME = "native_lib_mac_x64.zip";

        /// linux下递归列出目录下的所有文件名（不包括目录），并且去掉空行
        /// ls -lR |grep -v ^d|awk '{print $9}' |tr -s '\n'
        public static void Load()
        {

            // 1. Extract all DLL (embedded resources) into the a temporary directory:
            var assem = Assembly.GetExecutingAssembly();
            var an = assem.GetName();
            var asseName = an.Name + ".";
            var dirName = new DirectoryInfo(Path.Combine(Path.GetTempPath(), $"{NativlibTool.GetHashAssembly()}{an.ProcessorArchitecture}.{an.Version}"));
            {
                if (!dirName.Exists)
                {
                    dirName.Create();
                }

                // Log To Delete:
                Log.Info(dirName);

                var names = assem.GetManifestResourceNames();
                var name = names.FirstOrDefault(item => item.EndsWith(ZIP_FILE_NAME));
                if (name != null)
                {
                    //Log.Info("lib --------------------------------------- begin ");
                    //Log.Info("lib " + names.Length);
                    //Log.Info(string.Join("\n", names));
                    //Log.Info("lib --------------------------------------- end ");
                }
                else
                {
                    var errorInfo = ZIP_FILE_NAME + " not founded in Embedded Resource ! May be incorrect platform package Love2dCS-win|ubuntu|mac";
                    Log.Error(errorInfo);
                    throw new Exception(errorInfo);
                }

                // extract unzip file to temp
                NativlibTool.UnzipEmbeddedResourceToDir(assem, name, dirName.FullName);
            }

            // 2. load the native library
            {
                //var Love2dDll_DllPath_NAME = "love.framework/Versions/A/love";
                var Love2dDll_DllPath_NAME = "love";
                var linuxLibTable = new string[]
                {
                    "Ogg",
                    "Theora",
                    "Vorbis",
                    "FreeType",
                    "SDL2",
                    "libmodplug",
                    "Lua",
                    "OpenAL-Soft",
                    "mpg123",

                    //"ogg.framework/Versions/A/Ogg", // dep [/usr/lib/libSystem.B.dylib]
                    //"theora.framework/Versions/A/Theora", // dep [ogg] [/usr/lib/libSystem.B.dylib]
                    //"vorbis.framework/Versions/A/Vorbis", // dep [ogg]
                    //"freetype.framework/Versions/A/FreeType", // dep [/usr/lib/libz.1.dylib ]
                    //"libmodplug.framework/Versions/A/libmodplug", // [/usr/lib/libstdc++.6.dylib] [/usr/lib/libSystem.B.dylib] [/usr/lib/libgcc_s.1.dylib]
                    //"Lua.framework/Versions/A/Lua", // dep [/usr/lib/libSystem.B.dylib]
                    //"mpg123.framework/Versions/A/mpg123", // dep [/usr/lib/libSystem.B.dylib]
                    //"OpenAL-Soft.framework/Versions/A/OpenAL-Soft", 
                    //        // dep [/System/Library/Frameworks/AudioToolbox.framework/Versions/A/AudioToolbox]
                    //        // [/System/Library/Frameworks/ApplicationServices.framework/Versions/A/ApplicationServices]
                    //        // [/System/Library/Frameworks/AudioUnit.framework/Versions/A/AudioUnit]
                    //        // [/System/Library/Frameworks/CoreAudio.framework/Versions/A/CoreAudio]
                    //        // [/usr/lib/libSystem.B.dylib]
                    //"SDL2.framework/Versions/A/SDL2", // dep [/usr/lib/libiconv.2.dylib] ........

                    Love2dDll_DllPath_NAME,

                };
                foreach (var libname in linuxLibTable)
                {
                    var dllFilePath = dirName.FullName + "/" + libname;
                    var dlPtr = dlopen(dllFilePath, RTLD_NOW);
                    if (dlPtr == IntPtr.Zero)
                    {
                        var errorInfo = $"load library '{dllFilePath}' error: [{dlerror()}]";
                        errorInfo += $" ... file exists ? : {new System.IO.FileInfo(dllFilePath).Exists}";
                        Log.Error(errorInfo);
                        throw new Exception(errorInfo);
                    }

                    if (libname == Love2dDll_DllPath_NAME)
                    {
                        LOVE_LIB_PTR = dlPtr;
                    }
                }
            }
        }

        static IntPtr LOVE_LIB_PTR = IntPtr.Zero;

        public static IntPtr DynamciLoadFunction(string name)
        {
            return dlsym(LOVE_LIB_PTR, name);
        }
    }

    static class NativlibTool
    {
        public static void UnzipEmbeddedResourceToDir_Old(Assembly assem, string name, string tergetPath)
        {
            using (var stream = assem.GetManifestResourceStream(name))
            {
                using (ZipArchive archive = new ZipArchive(stream, ZipArchiveMode.Read))
                {
                    archive.ExtractToDirectory(tergetPath);
                }
            }
        }

        public static void UnzipEmbeddedResourceToDir(Assembly assem, string name, string extractPath) // CheckRewrite
        {
            if (!extractPath.EndsWith(Path.DirectorySeparatorChar.ToString(), StringComparison.Ordinal))
                extractPath += Path.DirectorySeparatorChar;

            using (var zipStream = assem.GetManifestResourceStream(name))
            {
                using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Read))
                {
                    foreach (var entry in archive.Entries)
                    {
                        if (entry.Name != "") // skip dictionary
                        {
                            CreateFolderCheck(extractPath, entry.FullName);
                            ExtractStreamResourceToDir(entry.Open(), Path.Combine(extractPath, entry.FullName));
                        }
                    }
                }
            }
        }

        public static byte[] ReadFully(System.IO.Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (input)
            {
                if (input == null)
                {
                    throw new ArgumentNullException(nameof(input));
                }
                using (MemoryStream ms = new MemoryStream())
                {
                    int read;
                    while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, read);
                    }
                    return ms.ToArray();
                }
            }
        }

        public static void CreateFolderCheck(string targetFolder, string zipEntryName)
        {
            var path = zipEntryName.Split('/');
            if (path.Length > 1)
            {
                var releativeFolder = string.Join(Path.DirectorySeparatorChar.ToString(), path.Take(path.Length - 1).ToArray());
                var dirName = new System.IO.DirectoryInfo(Path.Combine(targetFolder, releativeFolder));
                if (!dirName.Exists)
                {
                    dirName.Create();
                }
            }
        }

        public static void ExtractStreamResourceToDir(System.IO.Stream stream, string targetFilePath)
        {
            try // no catch
            {
                byte[] bytes = ReadFully(stream);
                var dllPath = new System.IO.FileInfo(targetFilePath);
                var rewrite = true;
                if (dllPath.Exists)
                {
                    var existing = System.IO.File.ReadAllBytes(dllPath.FullName);
                    if (bytes.SequenceEqual(existing))
                    {
                        rewrite = false;
                    }
                }
                if (rewrite)
                {
                    System.IO.File.WriteAllBytes(dllPath.FullName, bytes);
                }
            }
             catch (Exception ex)
             {
                Log.Error(ex.Message);
            }
        }


        public static void ExtractEmbeddedResourceToDir(Assembly assem, string name, string tergetPath)
        {
            byte[] bytes;
            using (var stream = assem.GetManifestResourceStream(name))
            {
                if (stream == null)
                {
                    throw new Exception($"GetManifestResourceStream(\"{name}\") is NULL");
                }
                bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
            }
            // try // no catch
            // {
            var dllPath = new System.IO.FileInfo(tergetPath);
            var rewrite = true;
            if (dllPath.Exists)
            {
                var existing = System.IO.File.ReadAllBytes(dllPath.FullName);
                if (bytes.SequenceEqual(existing))
                {
                    rewrite = false;
                }
            }
            if (rewrite)
            {
                System.IO.File.WriteAllBytes(dllPath.FullName, bytes);
            }
            // }
            // catch (Exception ex)
            // {
            //     // Log.Error(ex.Message);
            //     // Console.Error.WriteLine(ex.Message);
            // }
        }

        //public static void DeleteFolder(string path)
        //{
        //    foreach (string d in Directory.GetFileSystemEntries(path))
        //    {
        //        if (System.IO.File.Exists(d)) // it's a file
        //        {
        //            var fi = new System.IO.FileInfo(d);
        //            if (fi.Attributes.ToString().IndexOf("ReadOnly") != -1)
        //                fi.Attributes = FileAttributes.Normal;
        //            System.IO.File.Delete(d);//直接删除其中的文件  
        //        }
        //        else // it's a directory
        //        {
        //            var d1 = new System.IO.DirectoryInfo(d);
        //            DeleteFolder(d1.FullName);////递归删除子文件夹
        //            Directory.Delete(d); // delete it self
        //        }
        //    }
        //}

        public static string GetHashAssembly()
        {
            var assem = Assembly.GetEntryAssembly();
            return assem.GetName().Name + "." + GetHashString(assem.Location);
        }

        public static byte[] GetHash(string inputString)
        {
            var algorithm = System.Security.Cryptography.MD5.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
    }
}
