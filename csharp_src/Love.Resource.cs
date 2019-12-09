using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using SFile = System.IO.File;
using SFileMode = System.IO.FileMode;
using SFileInfo = System.IO.FileInfo;
using SIO = System.IO;
using System.Collections.Generic;

namespace Love
{
    public partial class FileSystem
    {
        /// <summary>
        ///  File which is created when a user drags and drops an actual file onto the
        ///  LOVE game. Uses C's stdio. Filenames are system-dependent full paths.
        /// </summary>
        /// <param name="filename">The filename of the file.</param>
        /// <param name="fmode_type">The mode to open the file in.</param>
        public static File NewFile(string filename, FileMode fmode_type = FileMode.Read)
            => NewDroppedFile(filename, fmode_type);

        /// <summary>
        ///  File which is created when a user drags and drops an actual file onto the
        ///  LOVE game. Uses C's stdio. Filenames are system-dependent full paths.
        /// </summary>
        /// <param name="filename">The filename of the file.</param>
        /// <param name="fmode_type">The mode to open the file in.</param>
        public static File NewDroppedFile(string filename, FileMode fmode_type = FileMode.Read)
        {
            Love2dDll.wrap_love_dll_filesystem_newDroppedFile(DllTool.GetNullTailUTF8Bytes(filename), (int)fmode_type, out IntPtr out_file);
            return LoveObject.NewObject<File>(out_file);
        }

        /// <summary>
        /// Creates a new FileData object.
        /// </summary>
        /// <param name="contents">The contents of the file.</param>
        /// <param name="filename">The name of the file.</param>
        /// <returns></returns>
        public static FileData NewFileData(byte[] contents, string filename)
        {
            IntPtr out_file;
            Love2dDll.wrap_love_dll_filesystem_newFileData_content(contents, contents.Length, DllTool.GetNullTailUTF8Bytes(filename), out out_file);
            return LoveObject.NewObject<FileData>(out_file);
        }

        /// <summary>
        /// Creates a new UTF8 text FileData object. 
        /// </summary>
        /// <param name="contents">The contents of the file.</param>
        /// <param name="filename">The name of the file.</param>
        /// <returns></returns>
        public static FileData NewFileData(string contents, string filename)
        {
            return NewFileData(DllTool.GetUTF8Bytes(contents), filename);
        }

        /// <summary>
        /// Creates a new FileData object.
        /// </summary>
        /// <param name="filename">The name of the file.</param>
        /// <returns></returns>
        public static FileData NewFileData(string filename)
        {
            return NewFileData(NewFile(filename));
        }

        /// <summary>
        /// Creates a new FileData object.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        public static FileData NewFileData(File file)
        {
            IntPtr out_file;
            Love2dDll.wrap_love_dll_filesystem_newFileData_file(file.p, out out_file);
            return LoveObject.NewObject<FileData>(out_file);
        }





        #region query opt
        public static string GetWorkingDirectory()
        {
            return Directory.GetCurrentDirectory();
        }


        #endregion





        #region other opt

        /// <summary>
        /// Append data to an existing file.
        /// </summary>
        /// <param name="path">The name (and path) of the file.</param>
        /// <param name="byteArray">The data to append to the file.</param>
        public static void Append(string path, byte[] byteArray)
        {
            using (var fs = new FileStream(path, SFileMode.Append, FileAccess.Write))
            {
                fs.Write(byteArray, 0, byteArray.Length);
            }
        }

        /// <summary>
        /// Append data to an existing file.
        /// </summary>
        /// <param name="path">The name (and path) of the file.</param>
        /// <param name="content">The string to append to the file.</param>
        public static void Append(string path, string content)
        {
            SFile.AppendAllText(path, content);
        }

        /// <summary>
        /// Append data to an existing file.
        /// </summary>
        /// <param name="path">The name (and path) of the file.</param>
        /// <param name="content">The string to append to the file.</param>
        /// <param name="encoding">string encoding.</param>
        public static void Append(string path, string content, Encoding encoding)
        {
            SFile.AppendAllText(path, content, encoding);
        }

        /// <summary>
        /// <para>Recursively creates a directory.</para>
        /// <para>When called with "a/b" it creates both "a" and "a/b", if they don't exist already.</para>
        /// </summary>
        /// <param name="pathString">The directory to create.</param>
        /// <returns>True if the directory was created, false if not.</returns>
        public static bool CreateDirectory(string pathString)
        {
            try
            {
                Directory.CreateDirectory(pathString);
                return true;
            }
            catch (Exception e)
            {
                Log.Error($"Create Directory failed {pathString} : {e.Message}");
            }
            return false;
        }

        /// <summary>
        /// <para>Returns a table with the names of files and subdirectories in the specified path. The array is not sorted in any way; the order is undefined.</para>
        /// <para>If the path passed to the function exists in the game and the save directory, it will list the files and directories from both places.</para>
        /// </summary>
        /// <param name="path">The directory.</param>
        /// <returns></returns>
        public static List<string> GetDirectoryItems(string path)
        {
            List<string> list = new List<string>(100);
            list.AddRange(Directory.GetDirectories(path));
            list.AddRange(Directory.GetFiles(path));
            return list;
            // return Directory.GetFileSystemEntries(path, "*", SearchOption.TopDirectoryOnly);
        }

        /// <summary>
        /// Gets information about the specified file or directory.
        /// </summary>
        /// <param name="path">The file or directory path to check.</param>
        /// <returns></returns>
        public static FileInfo GetInfo(string path)
        {
            var sFileInfo = new SFileInfo(path);
            if (sFileInfo.Exists)
            {
                // get the file attributes for file or directory
                FileAttributes attr = sFileInfo.Attributes;
                //detect whether its a directory or file
                bool isDirectory = ((attr & FileAttributes.Directory) == FileAttributes.Directory);
                var type = isDirectory ? FileType.Directory : FileType.File;
                return new FileInfo(sFileInfo.Length, (long)FileInfo.ConvertToUnixTimestamp(sFileInfo.LastAccessTime), type);
            }

            var sDirInfo = new DirectoryInfo(path);

            if (sDirInfo.Exists)
            {
                return new FileInfo(0, (long)FileInfo.ConvertToUnixTimestamp(sFileInfo.LastAccessTime), FileType.Directory);
            }

            return null;
        }

        /// <summary>
        /// Iterate over the lines in a file.
        /// </summary>
        /// <param name="lineFunction"></param>
        /// <param name="path"></param>
        public static void Lines(Action<string> lineFunction, string path)
        {
            Check.ArgumentNull(lineFunction, "lineFunction");

            string line;
            StreamReader file = new StreamReader(path);
            while ((line = file.ReadLine()) != null)
            {
                lineFunction(line);
            }
        }

        /// <summary>
        /// Read the all contents of a file.
        /// </summary>
        /// <param name="filename">The name (and path) of the file.</param>
        /// <returns></returns>
        public static byte[] Read(string path)
        {
            return SFile.ReadAllBytes(path);
        }

        /// <summary>
        /// Read the all contents of a file.
        /// </summary>
        /// <param name="filename">The name (and path) of the file.</param>
        /// <param name="len">How many bytes to read.</param>
        /// <returns></returns>
        public static byte[] Read(string path, int len)
        {
            return new BinaryReader(new FileStream(path, SFileMode.Open, FileAccess.Read)).ReadBytes(len);
        }

        /// <summary>
        /// Removes a file (or directory).
        /// </summary>
        /// <param name="path">The file or directory to remove.</param>
        public static bool Remove(string path)
        {
            try
            {
                //detect whether its a directory or file
                if ((SFile.GetAttributes(path) & FileAttributes.Directory) == FileAttributes.Directory)
                    Directory.Delete(path);
                else
                    SFile.Delete(path);

                return true;
            }
            catch (Exception e)
            {
                Log.Error($"Remove path failed {path} : {e.Message}");
            }
            return false;
        }


        /// <summary>
        /// Write data to a file in the save directory. If the file existed already, it will be completely replaced by the new contents.
        /// </summary>
        /// <param name="path">The name (and path) of the file.</param>
        /// <param name="bytes">The data to write to the file.</param>
        public static void Write(string path, byte[] bytes)
        {
            SFile.WriteAllBytes(path, bytes);
        }

        /// <summary>
        /// Write data to a file in the save directory. If the file existed already, it will be completely replaced by the new contents.
        /// </summary>
        /// <param name="filename">The name (and path) of the file.</param>
        /// <param name="contents">The string data to write to the file.</param>
        public static void Write(string path, string contents)
        {
            SFile.WriteAllText(path, contents);
        }

        /// <summary>
        /// Write data to a file in the save directory. If the file existed already, it will be completely replaced by the new contents.
        /// </summary>
        /// <param name="filename">The name (and path) of the file.</param>
        /// <param name="contents">The string data to write to the file.</param>
        /// <param name="encoding">string encodeing</param>
        public static void Write(string path, string contents, Encoding encoding)
        {
            SFile.WriteAllText(path, contents, encoding);
        }
        #endregion
    }

    /// <summary>
    /// This module will create resource through starndard C# IO,
    /// this means you can read a png file from path like C:/love-logo.png
    /// </summary>
    public class Resource
    {
        #region Persistence
        /// <summary>
        /// Save data to file, object will serialize as binary file.
        /// </summary>
        /// <param name="path">path to save</param>
        /// <param name="obj">object to save</param>
        public static void SaveData(string path, object obj)
        {
            MemoryStream memorystream = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(memorystream, obj);
            FileSystem.Write(path, memorystream.ToArray());
        }

        /// <summary>
        /// Load data from a file as specify type.
        /// </summary>
        /// <typeparam name="T">the data type you want convert</typeparam>
        /// <param name="path">the path you want to load</param>
        /// <returns></returns>
        public static T LoadData<T>(string path)
        {
            MemoryStream memorystreamd = new MemoryStream(FileSystem.Read(path));
            BinaryFormatter bfd = new BinaryFormatter();
            return (T)bfd.Deserialize(memorystreamd);
        }
        #endregion

    }
}
