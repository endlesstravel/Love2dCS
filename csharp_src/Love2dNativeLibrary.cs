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
using System.IO.Compression;
using File = System.IO.File;
using FileInfo = System.IO.FileInfo;
using System.Text;

namespace Love
{
    static public partial class Boot
    {
        static void LoadUnixLibrary()
        {
            UnixNativeLibraryLoader.Load();
        }
        static void LoadWinLibrary()
        {
            WindowsNativeLibraryLoader.Load();
            // System.Runtime.InteropServices.RuntimeInformation.ProcessArchitecture // from 4.7.1
            //Log.Warnning(ProcessorArchitecture);

            //var assem = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
            //assem.ManifestModule.GetPEKind(out var peKind, out var imageFileMachine);
            //Console.WriteLine(assem.ToString() + "  " + peKind + " " + imageFileMachine);


            //ExtractWinLibrary();
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
            Console.WriteLine(dllStringTip);


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

                var path = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Process) ?? String.Empty;
                if (path.Split(';').All(pathPiece => pathPiece != dirName.FullName))
                {
                    Environment.SetEnvironmentVariable("PATH", String.Join(";", dirName, path), EnvironmentVariableTarget.Process);
                }
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
                    var errorInfo = dllStringTip + " ont founded in Embedded Resource !";
                    Log.Error(errorInfo);
                    throw new Exception(errorInfo);
                }

                NativlibTool.UnzipEmbeddedResourceToDir(assem, name, dirName.FullName);
            }

            // 3. load library // not need now
            {
                //var fileToLoad = dirName.FullName + "\\" + Love2dDll.DllPath;
                //if (LoadLibrary(fileToLoad) == IntPtr.Zero)
                //{
                //    throw new Exception($"load {fileToLoad}");
                //}
            }

        }

        //[DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "LoadLibraryW", ExactSpelling = true, SetLastError = true, CharSet = CharSet.Unicode)]
        //private static extern IntPtr LoadLibrary(string lpFileName);
    }

    static class UnixNativeLibraryLoader
    {

        // public const int RTLD_LAZY = 0x001;
        public const int RTLD_NOW = 0x002;
        [System.Runtime.InteropServices.DllImport("libdl")]
        public static extern IntPtr dlopen(string fileName, int flags);

        [System.Runtime.InteropServices.DllImport("libdl")]
        public static extern string dlerror();

        const string LD_LIBRARY_PATH = "LD_LIBRARY_PATH";
        const string ZIP_FILE_NAME = "native_lib_linux_x64.zip";
        const char ENV_PATH_SPITER = ':';

        public static bool LoadLibrary(string path, out string errorInfo)
        {
            // Console.WriteLine($"{Environment.CurrentDirectory}/{name}");
            if (dlopen(path, RTLD_NOW) == IntPtr.Zero)
            {
                errorInfo = dlerror();
                Log.Error($"load library '{path}' error: [{errorInfo}]");
                return false;
            }

            errorInfo = "";
            return true;
        }

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
                    var errorInfo = ZIP_FILE_NAME + " ont founded in Embedded Resource !";
                    Log.Error(errorInfo);
                    throw new Exception(errorInfo);
                }

                // extract unzip file to temp
                NativlibTool.UnzipEmbeddedResourceToDir(assem, name, dirName.FullName);
            }

            // 2. Only load the native "usr/lib/liblove-11.2.so"
            // (it will automatically load others; lua51/mpg123/OpenAL32/SDL2, dependency DLLs):
            {
                Set_LD_LIBRARY_PATH_Env(dirName.FullName);
                // if (LoadLibrary(dirName.FullName + "/usr/lib/liblove-11.2.so", out var errorInfo) == false)
                // {
                //     throw new Exception(errorInfo);
                // }
                // if (LoadLibrary("liblove-11.2.so", out var errorInfo) == false)
                // {
                //     throw new Exception(errorInfo);
                // }
                // var linuxLibHashSet = new HashSet<string>()
                // {
                //     "lib/x86_64-linux-gnu/libgcc_s.so.1",
                //     "lib/x86_64-linux-gnu/libz.so.1",
                //     "lib/x86_64-linux-gnu/libm.so.6",
                //     "libstdc++/libstdc++.so.6",
                //     "usr/lib/liblove-11.2.so",
                //     "usr/lib/libmodplug.so.1",
                //     "usr/lib/libluajit-5.1.so.2",
                //     "usr/lib/x86_64-linux-gnu/libtheoradec.so.1",
                //     "usr/lib/x86_64-linux-gnu/libmpg123.so.0",
                //     "usr/lib/x86_64-linux-gnu/libopenal.so.1",
                //     "usr/lib/x86_64-linux-gnu/libvorbis.so.0",
                //     "usr/lib/x86_64-linux-gnu/libvorbisfile.so.3",
                //     "usr/lib/x86_64-linux-gnu/libogg.so.0",
                //     "usr/lib/x86_64-linux-gnu/libfreetype.so.6",
                //     "usr/lib/libSDL2-2.0.so.0",
                // };



                var linuxLibHashSet = new string[]
                {
                    "libstdc++/libstdc++.so.6",
                    "lib/x86_64-linux-gnu/libgcc_s.so.1",
                    "lib/x86_64-linux-gnu/libz.so.1",

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

                    $"usr/lib/{Love2dDll.DllPath}.so",

                };
                foreach (var libname in linuxLibHashSet)
                {
                    // var nn = libname.Split("/").Last();
                    // if (LoadLibrary(nn, out var errorInfo) == false)
                    // {
                    //     throw new Exception(errorInfo);
                    // }
                    if (LoadLibrary(dirName.FullName + "/" + libname, out var errorInfo) == false)
                    {
                        throw new Exception(errorInfo);
                    }
                }
            }
        }

        static void Set_LD_LIBRARY_PATH_Env(string folderPath)
        {
            var path = Environment.GetEnvironmentVariable(LD_LIBRARY_PATH, EnvironmentVariableTarget.Process) ?? String.Empty;
            Environment.SetEnvironmentVariable(LD_LIBRARY_PATH,
                string.Join(ENV_PATH_SPITER.ToString(),
                $"{folderPath}/lib/x86_64-linux-gnu",
                $"{folderPath}/usr/bin",
                $"{folderPath}/usr/lib",
                $"{folderPath}/usr/lib/x86_64-linux-gnu",
                path), EnvironmentVariableTarget.Process);
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

        public static void DeleteFolder(string path)
        {
            foreach (string d in Directory.GetFileSystemEntries(path))
            {
                if (System.IO.File.Exists(d)) // it's a file
                {
                    var fi = new System.IO.FileInfo(d);
                    if (fi.Attributes.ToString().IndexOf("ReadOnly") != -1)
                        fi.Attributes = FileAttributes.Normal;
                    System.IO.File.Delete(d);//直接删除其中的文件  
                }
                else // it's a directory
                {
                    var d1 = new System.IO.DirectoryInfo(d);
                    DeleteFolder(d1.FullName);////递归删除子文件夹
                    Directory.Delete(d); // delete it self
                }
            }
        }

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
