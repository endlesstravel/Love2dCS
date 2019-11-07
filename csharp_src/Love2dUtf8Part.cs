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
        /// <param name="filename">The name of the file.</param>
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
        public static FileInfo GetInfo(string path)
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


        //public static long _GetLastModified(string filename)
        //{
        //    return _GetLastModified(DllTool.GetNullTailUTF8Bytes(filename));
        //}

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
        /// <summary>
        /// Sends one or more colors to a special (extern / uniform) vec3 or vec4 variable inside the shader. The color components must be in the range of [0, 1]. The colors are gamma-corrected if global gamma-correction is enabled.
        /// </summary>
        /// <param name="name">The name of the color extern variable to send to in the shader.</param>
        /// <param name="valueArray">A array with red, green, blue, and alpha color components in the range of [0, 1] to send to the extern as a vector.</param>
        public void SendColors(string name, params Vector4[] valueArray)
        {
            SendColors(DllTool.GetNullTailUTF8Bytes(name), valueArray);
        }

        /// <summary>
        /// Sends one or more float values to a special (uniform) variable inside the shader.
        /// </summary>
        /// <param name="name">Name of the float to send to the shader.</param>
        /// <param name="valueArray">Float to send to store in the uniform variable.</param>
        public void SendFloats(string name, params float[] valueArray)
        {
            SendFloats(DllTool.GetNullTailUTF8Bytes(name), valueArray);
        }

        /// <summary>
        /// Sends one or more uint values to a special (uniform) variable inside the shader.
        /// </summary>
        /// <param name="name">Name of the uint to send to the shader.</param>
        /// <param name="valueArray">Uint to send to store in the uniform variable.</param>
        public void SendUints(string name, params uint[] valueArray)
        {
            SendUints(DllTool.GetNullTailUTF8Bytes(name), valueArray);
        }

        /// <summary>
        /// Sends one or more int values to a special (uniform) variable inside the shader.
        /// </summary>
        /// <param name="name">Name of the int to send to the shader.</param>
        /// <param name="valueArray">Int to send to store in the uniform variable.</param>
        public void SendInts(string name, params int[] valueArray)
        {
            SendInts(DllTool.GetNullTailUTF8Bytes(name), valueArray);
        }

        /// <summary>
        /// Sends one or more boolean values to a special (uniform) variable inside the shader.
        /// </summary>
        /// <param name="name">Name of the boolean to send to the shader.</param>
        /// <param name="valueArray">Boolean to send to store in the uniform variable.</param>
        public void SendBooleans(string name, params bool[] valueArray)
        {
            SendBooleans(DllTool.GetNullTailUTF8Bytes(name), valueArray);
        }

        /// <summary>
        /// WARNNING: incorrect use of this function can carsh program.
        /// <para> params of valueArray.Length should equals columns * rows * count </para>
        /// </summary>
        /// <param name="name">uniform variable name</param>
        /// <param name="valueArray">each float consistute matrix array</param>
        /// <param name="columns">matrix columns</param>
        /// <param name="rows">matrix rows</param>
        /// <param name="count">matrix count</param>
        public void SendMatrix(string name, float[] valueArray, int columns, int rows, int count)
        {
            SendMatrix(DllTool.GetNullTailUTF8Bytes(name), valueArray, columns, rows, count);
        }

        /// <summary>
        /// Sends one or more Matrix22 values to a special (uniform) variable inside the shader.
        /// </summary>
        /// <param name="name">Name of the Matrix22 to send to the shader.</param>
        /// <param name="valueArray">Matrix22 to send to store in the uniform variable.</param>
        public void SendMatrix(string name, params Matrix22[] valueArray)
        {
            float[] values = new float[2 * 2 * valueArray.Length];
            int offset = 0;
            foreach (var m in valueArray)
            {
                values[offset + 00] = m.M11;
                values[offset + 01] = m.M12;

                values[offset + 02] = m.M21;
                values[offset + 03] = m.M22;

                offset += (2 * 2);
            }
            SendMatrix(name, values, 2, 2, valueArray.Length);
        }

        /// <summary>
        /// Sends one or more Matrix33 values to a special (uniform) variable inside the shader.
        /// </summary>
        /// <param name="name">Name of the Matrix33 to send to the shader.</param>
        /// <param name="valueArray">Matrix33 to send to store in the uniform variable.</param>
        public void SendMatrix(string name, params Matrix33[] valueArray)
        {
            float[] values = new float[3 * 3 * valueArray.Length];
            int offset = 0;
            foreach (var m in valueArray)
            {
                values[offset + 00] = m.M11;
                values[offset + 01] = m.M12;
                values[offset + 02] = m.M13;

                values[offset + 03] = m.M21;
                values[offset + 04] = m.M22;
                values[offset + 05] = m.M23;

                values[offset + 06] = m.M31;
                values[offset + 07] = m.M32;
                values[offset + 08] = m.M33;

                offset += (3 * 3);
            }
            SendMatrix(name, values, 3, 3, valueArray.Length);
        }

        /// <summary>
        /// Sends one or more SendMatrix values to a special (uniform) variable inside the shader.
        /// </summary>
        /// <param name="name">Name of the SendMatrix to send to the shader.</param>
        /// <param name="valueArray">SendMatrix to send to store in the uniform variable.</param>
        public void SendMatrix(string name, params Matrix44[] valueArray)
        {
            float[] values = new float[4 * 4 * valueArray.Length];
            int offset = 0;
            foreach (var m in valueArray)
            {
                values[offset + 00] = m.M11;
                values[offset + 01] = m.M12;
                values[offset + 02] = m.M13;
                values[offset + 03] = m.M14;

                values[offset + 04] = m.M21;
                values[offset + 05] = m.M22;
                values[offset + 06] = m.M23;
                values[offset + 07] = m.M24;

                values[offset + 08] = m.M31;
                values[offset + 09] = m.M32;
                values[offset + 10] = m.M33;
                values[offset + 11] = m.M34;

                values[offset + 12] = m.M41;
                values[offset + 13] = m.M42;
                values[offset + 14] = m.M43;
                values[offset + 15] = m.M44;

                offset += (4 * 4);
            }
            SendMatrix(name, values, 4, 4, valueArray.Length);
        }

        /// <summary>
        /// Sends one or more texture to a special (uniform) variable inside the shader.
        /// </summary>
        /// <param name="name">Name of the Texture to send to the shader.(UTF8 byte array)</param>
        /// <param name="texture">Texture (Image or Canvas) to send to the uniform variable.</param>
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
        public FileData Encode(ImageFormat format, string filename = null)
        {
            return Encode(format, filename != null, DllTool.GetNullTailUTF8Bytes(filename));
        }
    }

    #endregion
}