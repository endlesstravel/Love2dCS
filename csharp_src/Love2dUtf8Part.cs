// Author : endlesstravel
// this file define function that trans string to utf8 bytes automatic to make libray easy to use

using System;

namespace Love
{
    #region Love Module

    public partial class FileSystem
    {
        /// <summary>
        /// Creates a new File object. It needs to be opened before it can be accessed.
        /// </summary>
        /// <param name="filename">The filename of the file.</param>
        /// <param name="fmode_type">The mode to open the file in.</param>
        /// <returns></returns>
        public static File NewFile(string filename, FileMode fmode_type = FileMode.Read)
        {
            return NewFile(DllTool.GetNullTailUTF8Bytes(filename), fmode_type);
        }

        /// <summary>
        /// Creates a new FileData object.
        /// </summary>
        /// <param name="contents">The contents of the file.</param>
        /// <param name="filename">The name of the file.</param>
        /// <returns></returns>
        public static FileData NewFileData(string contents, string filename)
        {
            return NewFileData(DllTool.GetUTF8Bytes(contents), DllTool.GetNullTailUTF8Bytes(filename));
        }

        /// <summary>
        /// Creates a new FileData object.
        /// </summary>
        /// <param name="contents">The contents of the file.</param>
        /// <param name="filename">The name  of the file.</param>
        /// <returns></returns>
        public static FileData NewFileData(byte[] contents, string filename)
        {
            return NewFileData(contents, DllTool.GetNullTailUTF8Bytes(filename));
        }

        /// <summary>
        /// Initializes FileSystem, will be called internally, so should not be used explictly.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static bool Init(string args)
        {
            return Init(DllTool.GetNullTailUTF8Bytes(args));
        }

        /// <summary>
        /// Gets information about the specified file or directory.
        /// </summary>
        /// <param name="path">The file or directory path to check.</param>
        /// <returns></returns>
        public static Info GetInfo(string path)
        {
            return GetInfo(DllTool.GetNullTailUTF8Bytes(path));
        }

        /// <summary>
        /// Sets the write directory for your game. Note that you can only set the name of the folder to store your files in, not the location.
        /// </summary>
        /// <param name="path">The new identity that will be used as write directory.</param>
        /// <param name="append">Whether the identity directory will be searched when reading a filepath before or after the game's source directory and any currently.
        /// TRUE: results in searching source before searching save directory; FALSE: results in searching game save directory before searching source directorymounted archives.</param>
        public static void SetIdentity(string path, bool append = false)
        {
            SetIdentity(DllTool.GetNullTailUTF8Bytes(path), append);
        }

        /// <summary>
        /// Sets the source of the game, where the code is present. This function can only be called once, and is normally automatically done by LÖVE.
        /// </summary>
        /// <param name="path">Absolute path to the game's source folder.</param>
        public static void SetSource(string path)
        {
            SetSource(DllTool.GetNullTailUTF8Bytes(path));
        }

        /// <summary>
        /// Mounts a zip file or folder in the game's save directory for reading. It is also possible to mount love.filesystem.getSourceBaseDirectory if the game is in fused mode.
        /// </summary>
        /// <param name="archive">The folder or zip file in the game's save directory to mount.</param>
        /// <param name="mountpoint">The new path the archive will be mounted to.</param>
        /// <param name="appendToPath">Whether the archive will be searched when reading a filepath before or after already-mounted archives. This includes the game's source and save directories.</param>
        /// <returns></returns>
        public static bool Mount(string archive, string mountpoint, bool appendToPath = false)
        {
            return Mount(DllTool.GetNullTailUTF8Bytes(archive), DllTool.GetNullTailUTF8Bytes(mountpoint), appendToPath);
        }
        public static bool Unmount(string archive)
        {
            return Unmount(DllTool.GetNullTailUTF8Bytes(archive));
        }

        /// <summary>
        /// <para>Gets the platform-specific absolute path of the directory containing a filepath.</para>
        /// </summary>
        /// <param name="filename">The filepath to get the directory of.</param>
        /// <returns>The platform-specific full path of the directory containing the filepath.</returns>
        public static string GetRealDirectory(string filename)
        {
            return GetRealDirectory(DllTool.GetNullTailUTF8Bytes(filename));
        }

        /// <summary>
        /// <para>Recursively creates a directory.</para>
        /// <para>When called with "a/b" it creates both "a" and "a/b", if they don't exist already.</para>
        /// </summary>
        /// <param name="name">The directory to create.</param>
        /// <returns>True if the directory was created, false if not.</returns>
        public static bool CreateDirectory(string name)
        {
            return CreateDirectory(DllTool.GetNullTailUTF8Bytes(name));
        }

        /// <summary>
        /// Removes a file (or directory).
        /// </summary>
        /// <param name="path">The file or directory to remove.</param>
        public static bool Remove(string path)
        {
            return Remove(DllTool.GetNullTailUTF8Bytes(path));
        }

        /// <summary>
        /// Read the contents of a file.
        /// </summary>
        /// <param name="filename">The name (and path) of the file.</param>
        /// <param name="len">How many bytes to read. (-1 means all)</param>
        /// <returns></returns>
        public static byte[] Read(string filename, long len = -1)
        {
            return Read(DllTool.GetNullTailUTF8Bytes(filename), len);
        }

        /// <summary>
        /// Write data to a file in the save directory. If the file existed already, it will be completely replaced by the new contents.
        /// </summary>
        /// <param name="filename">The name (and path) of the file.</param>
        /// <param name="input">The data to write to the file.</param>
        public static void Write(string filename, byte[] input)
        {
            Write(DllTool.GetNullTailUTF8Bytes(filename), input);
        }


        /// <summary>
        /// Write data to a file in the save directory. If the file existed already, it will be completely replaced by the new contents.
        /// <para>encode with UTF-8</para>
        /// </summary>
        /// <param name="filename">The name (and path) of the file.</param>
        /// <param name="input">The string data to write to the file.</param>
        public static void Write(string filename, string input)
        {
            Write(DllTool.GetNullTailUTF8Bytes(filename), DllTool.GetUTF8Bytes(input));
        }

        /// <summary>
        /// Append data to an existing file.
        /// </summary>
        /// <param name="filename">The name (and path) of the file.</param>
        /// <param name="input">The data to append to the file.</param>
        public static void Append(string filename, byte[] input)
        {
            Append(DllTool.GetNullTailUTF8Bytes(filename), input);
        }

        /// <summary>
        /// Append text to an existing file (encode with UTF-8).
        /// </summary>
        /// <param name="filename">The name (and path) of the file.</param>
        /// <param name="txt">string to append</param>
        public static void Append(string filename, string txt)
        {
            Append(DllTool.GetNullTailUTF8Bytes(filename), DllTool.GetUTF8Bytes(txt));
        }

        /// <summary>
        /// <para>Returns a table with the names of files and subdirectories in the specified path. The array is not sorted in any way; the order is undefined.</para>
        /// <para>If the path passed to the function exists in the game and the save directory, it will list the files and directories from both places.</para>
        /// </summary>
        /// <param name="dir">The directory.</param>
        /// <returns></returns>
        public static string[] GetDirectoryItems(string dir)
        {
            return GetDirectoryItems(DllTool.GetNullTailUTF8Bytes(dir));
        }


        public static long _GetLastModified(string filename)
        {
            return _GetLastModified(DllTool.GetNullTailUTF8Bytes(filename));
        }

        public static void _SetRequirePath(string paths)
        {
            _SetRequirePath(DllTool.GetNullTailUTF8Bytes(paths));
        }
    }

    public partial class Window
    {
        /// <summary>
        /// Sets the window title.
        /// <para>Constantly updating the window title can lead to issues on some systems and therefore is discouraged.</para>
        /// </summary>
        /// <param name="titleStr">The new window title.</param>
        public static void SetTitle(string titleStr)
        {
            SetTitle(DllTool.GetNullTailUTF8Bytes(titleStr));
        }

        /// <summary>
        /// Displays a simple message box with a single 'OK' button.
        /// <para>	This function will pause all execution of the main thread until the user has clicked a button to exit the message box. Calling the function from a different thread may cause love to crash.</para>
        /// </summary>
        /// <param name="title">The title of the message box.</param>
        /// <param name="message">The text inside the message box.</param>
        /// <param name="msgbox_type">The type of the message box.</param>
        /// <param name="attachToWindow">Whether the message box should be attached to the love window or free-floating.</param>
        /// <returns>Whether the message box was successfully displayed.</returns>
        public static bool ShowMessageBox(string title, string message, MessageBoxType msgbox_type = MessageBoxType.Info, bool attachToWindow = true)
        {
            return ShowMessageBox(DllTool.GetNullTailUTF8Bytes(title), DllTool.GetNullTailUTF8Bytes(message), msgbox_type, attachToWindow);
        }
    }

    public partial class Joystick
    {
        //public static bool SetGamepadMapping(string guid, Joystick.InputType gp_inputType_type, Joystick.InputType j_inputType_type, int inputIndex, JoystickHat hat_type)
        //{
        //    return SetGamepadMapping(DllTool.GetNullTailUTF8Bytes(guid), gp_inputType_type, j_inputType_type, inputIndex, hat_type);
        //}
        //public static bool LoadGamepadMappings(string str)
        //{
        //    return LoadGamepadMappings(DllTool.GetNullTailUTF8Bytes(str));
        //}
    }

    public partial class Graphics
    {
        public static bool IsOpenGLES()
        {
            string name, unused;
            GetRendererInfo(out name, out unused, out unused, out unused);
            return name == "OpenGL ES";
        }
        public static Shader NewShader(string codeStr)
        {
            return NewShader(codeStr, null);
        }
        public static Shader NewShader(string vertexCodeStr, string pixelCodeStr)
        {
            bool gles = IsOpenGLES();

            IntPtr out_shader = IntPtr.Zero;
            string vertexCode, pixelCode;
            Love2dGraphicsShaderBoot.shaderCodeToGLSL(gles, vertexCodeStr, pixelCodeStr, out vertexCode, out pixelCode);
            Love2dDll.wrap_love_dll_graphics_newShader(DllTool.GetNullTailUTF8Bytes(vertexCode), DllTool.GetNullTailUTF8Bytes(pixelCode), out out_shader);
            return LoveObject.NewObject<Shader>(out_shader);
        }
    }

    public partial class Font
    {
        /// <summary>
        /// Creates a new GlyphData.
        /// </summary>
        /// <param name="rasterizer">The Rasterizer containing the font.</param>
        /// <param name="glyph">The character code of the glyph.</param>
        /// <returns></returns>
        public GlyphData NewGlyphData(Rasterizer rasterizer, string glyph)
        {
            return NewGlyphData(rasterizer, DllTool.GetNullTailUTF8Bytes(glyph));
        }
    }

    #endregion


    #region Love Type

    public partial class Rasterizer
    {
        public GlyphData GetGlyphData(string str)
        {
            return GetGlyphData(DllTool.GetNullTailUTF8Bytes(str));
        }
        public bool HasGlyphs(string str)
        {
            return HasGlyphs(DllTool.GetNullTailUTF8Bytes(str));
        }
    }

    public partial class Font
    {
        /// <summary>
        /// Creates a new Rasterizer.
        /// </summary>
        /// <param name="filename">The font file.</param>
        /// <returns>The rasterizer.</returns>
        public static Rasterizer NewRasterizer(string filename)
        {
            var filedata = FileSystem.NewFileData(filename);
            return NewRasterizer(filedata);
        }

        /// <summary>
        /// Determines the width of the given text.
        /// </summary>
        /// <param name="str">A string.</param>
        /// <returns>The width of the text.</returns>
        public int GetWidth(string str)
        {
            return GetWidth(DllTool.GetNullTailUTF8Bytes(str));
        }

        /// <summary>
        /// Gets whether the Font can render a character or string.
        /// </summary>
        /// <param name="str">A string.</param>
        /// <returns>Whether the font can render all characters in the string.</returns>
        public bool HasGlyphs(string str)
        {
            return HasGlyphs(DllTool.GetNullTailUTF8Bytes(str));
        }

        /// <summary>
        /// Gets formatting information for text, given a wrap limit.
        /// </summary>
        /// <param name="text">The text that will be wrapped.</param>
        /// <param name="wrap_limit">The maximum width in pixels of each line that text is allowed before wrapping.</param>
        /// <returns>(The maximum width of the wrapped text., A sequence containing each line of text that was wrapped.)</returns>
        public Tuple<int, string[]> GetWrap(string text, float wrap_limit)
        {
            var coloredStr = ColoredStringArray.Create(text);
            IntPtr out_pws = IntPtr.Zero;
            int out_maxWidth = 0;

            coloredStr.ExecResource( tmp => {
                Love2dDll.wrap_love_dll_type_Font_getWrap(p, tmp.Item1, tmp.Item2, coloredStr.Length, wrap_limit, out out_maxWidth, out out_pws);
            });
            var lines = DllTool.WSSToStringListAndRelease(out_pws);
            return Tuple.Create(out_maxWidth, lines);
        }
    }

    public partial class Mesh
    {
    }
    public partial class Shader
    {
        public void SendColors(string name, params Vector4[] valuearray)
        {
            SendColors(DllTool.GetNullTailUTF8Bytes(name), valuearray);
        }
        public void SendFloats(string name, params float[] valuearray)
        {
            SendFloats(DllTool.GetNullTailUTF8Bytes(name), valuearray);
        }
        public void SendInts(string name, params int[] valuearray)
        {
            SendInts(DllTool.GetNullTailUTF8Bytes(name), valuearray);
        }
        public void SendBooleans(string name, params bool[] valuearray)
        {
            SendBooleans(DllTool.GetNullTailUTF8Bytes(name), valuearray);
        }
        public void SendMatrix(string name, float[] valuearray, int columns, int rows, int count)
        {
            SendMatrix(DllTool.GetNullTailUTF8Bytes(name), valuearray, columns, rows, count);
        }
        public void SendMatrix(string name, params Matrix22[] valueArray)
        {
            float[] values = new float[2 * 2 * valueArray.Length];
            int offset = 0;
            foreach (var m in valueArray)
            {
                Array.Copy(m.data, 0, values, offset, m.data.Length);
                offset += m.data.Length;
            }
            SendMatrix(name, values, 2, 2, valueArray.Length);
        }
        public void SendMatrix(string name, params Matrix33[] valueArray)
        {
            float[] values = new float[3 * 3 * valueArray.Length];
            int offset = 0;
            foreach (var m in valueArray)
            {
                Array.Copy(m.data, 0, values, offset, m.data.Length);
                offset += m.data.Length;
            }
            SendMatrix(name, values, 3, 3, valueArray.Length);
        }
        public void SendMatrix(string name, params Matrix44[] valueArray)
        {
            float[] values = new float[4 * 4 * valueArray.Length];
            int offset = 0;
            foreach (var m in valueArray)
            {
                Array.Copy(m.data, 0, values, offset, m.data.Length);
                offset += m.data.Length;
            }
            SendMatrix(name, values, 4, 4, valueArray.Length);
        }
        public void SendTexture(string name, Texture texture)
        {
            SendTexture(DllTool.GetNullTailUTF8Bytes(name), texture);
        }
    }

    public partial class SpriteBatch
    {
        public void AttachAttribute(string name, Mesh mesh)
        {
            AttachAttribute(DllTool.GetNullTailUTF8Bytes(name), mesh);
        }

    }

    public partial class RandomGenerator
    {
        public void SetState(string state)
        {
            SetState(DllTool.GetNullTailUTF8Bytes(state));
        }
    }

    public partial class ImageData
    {
        /// <summary>
        /// Encodes the ImageData and optionally writes it to the save directory.
        /// </summary>
        /// <param name="format_type">The format to encode the image as.</param>
        /// <param name="filename">The filename to write the file to. If null, no file will be written but the FileData will still be returned.</param>
        /// <returns></returns>
        public FileData Encode(ImageFormat format_type, string filename = null)
        {
            return Encode(format_type, filename != null, DllTool.GetNullTailUTF8Bytes(filename));
        }
    }

    #endregion
}