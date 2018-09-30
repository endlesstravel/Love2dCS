// Author : endlesstravel
// this file define function that trans string to utf8 bytes automatic to make libray easy to use

using System;

namespace Love
{
    #region Love Module

    public partial class FileSystem
    {
        public static File NewFile(string filename, File.Mode fmode_type = File.Mode.Read)
        {
            return NewFile(DllTool.ToUTF8Bytes(filename), fmode_type);
        }
        public static FileData NewFileData(string contents, string filename)
        {
            return NewFileData(DllTool.ToUTF8Bytes(contents), DllTool.ToUTF8Bytes(filename));
        }
        public static FileData NewFileData(byte[] contents, string filename)
        {
            return NewFileData(contents, DllTool.ToUTF8Bytes(filename));
        }
        public static bool Init(string args)
        {
            return Init(DllTool.ToUTF8Bytes(args));
        }
        public static void SetIdentity(string arg, bool append = false)
        {
            SetIdentity(DllTool.ToUTF8Bytes(arg), append);
        }
        public static void SetSource(string arg)
        {
            SetSource(DllTool.ToUTF8Bytes(arg));
        }
        public static bool Mount(string archive, string mountpoint, bool appendToPath)
        {
            return Mount(DllTool.ToUTF8Bytes(archive), DllTool.ToUTF8Bytes(mountpoint), appendToPath);
        }
        public static bool Unmount(string archive)
        {
            return Unmount(DllTool.ToUTF8Bytes(archive));
        }
        public static String GetRealDirectory(string filename)
        {
            return GetRealDirectory(DllTool.ToUTF8Bytes(filename));
        }
        public static void CreateDirectory(string arg)
        {
            CreateDirectory(DllTool.ToUTF8Bytes(arg));
        }
        public static void Remove(string arg)
        {
            Remove(DllTool.ToUTF8Bytes(arg));
        }
        public static byte[] read(string filename, long len = -1)
        {
            return read(DllTool.ToUTF8Bytes(filename), len);
        }
        public static void Write(string filename, byte[] input)
        {
            Write(DllTool.ToUTF8Bytes(filename), input);
        }
        public static void Append(string filename, byte[] input)
        {
            Append(DllTool.ToUTF8Bytes(filename), input);
        }
        public static String[] getDirectoryItems(string dir)
        {
            return getDirectoryItems(DllTool.ToUTF8Bytes(dir));
        }
        public static long GetLastModified(string filename)
        {
            return GetLastModified(DllTool.ToUTF8Bytes(filename));
        }
        public static void SetRequirePath(string paths)
        {
            SetRequirePath(DllTool.ToUTF8Bytes(paths));
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
            SetTitle(DllTool.ToUTF8Bytes(titleStr));
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
            return ShowMessageBox(DllTool.ToUTF8Bytes(title), DllTool.ToUTF8Bytes(message), msgbox_type, attachToWindow);
        }
    }

    public partial class Joystick
    {
        public static bool SetGamepadMapping(string guid, Joystick.InputType gp_inputType_type, Joystick.InputType j_inputType_type, int inputIndex, Joystick.Hat hat_type)
        {
            return SetGamepadMapping(DllTool.ToUTF8Bytes(guid), gp_inputType_type, j_inputType_type, inputIndex, hat_type);
        }
        public static bool LoadGamepadMappings(string str)
        {
            return LoadGamepadMappings(DllTool.ToUTF8Bytes(str));
        }
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
            Love2dDll.wrap_love_dll_graphics_newShader(DllTool.ToUTF8Bytes(vertexCode), DllTool.ToUTF8Bytes(pixelCode), out out_shader);
            return LoveObject.NewObject<Shader>(out_shader);
        }
    }
    public partial class Font
    {
        Rasterizer newImageRasterizer(ImageData imageData, string glyphs, int extraspacing)
        {
            return NewImageRasterizer(imageData, DllTool.ToUTF8Bytes(glyphs), extraspacing);
        }
        GlyphData NewGlyphData(Rasterizer rasterizer, string glyph)
        {
            return NewGlyphData(rasterizer, DllTool.ToUTF8Bytes(glyph));
        }
    }

    #endregion


    #region Love Type

    public partial class Rasterizer
    {
        public GlyphData GetGlyphData(string str)
        {
            return GetGlyphData(DllTool.ToUTF8Bytes(str));
        }
        public bool HasGlyphs(string str)
        {
            return HasGlyphs(DllTool.ToUTF8Bytes(str));
        }
    }
    public partial class Font
    {
        public int GetWidth(string str)
        {
            return GetWidth(DllTool.ToUTF8Bytes(str));
        }
        public bool HasGlyphs(string str)
        {
            return HasGlyphs(DllTool.ToUTF8Bytes(str));
        }
        public Tuple<string[], int> GetWrap(string text, float wrap_limit)
        {
            var coloredStr = ColoredStringArray.Create(text);
            IntPtr out_pws = IntPtr.Zero;
            int out_maxWidth = 0;

            coloredStr.ExecResource( tmp => {
                Love2dDll.wrap_love_dll_type_Font_getWrap(p, tmp.Item1, tmp.Item2, coloredStr.Length, wrap_limit, out out_maxWidth, out out_pws);
            });
            var lines = DllTool.WSSToStringListAndRelease(out_pws);
            return new Tuple<string[], int>(lines, out_maxWidth);
        }
    }

    public partial class Mesh
    {
    }
    public partial class Shader
    {
        public void SendColors(string name, params Float4[] valuearray)
        {
            SendColors(DllTool.ToUTF8Bytes(name), valuearray);
        }
        public void SendFloats(string name, params float[] valuearray)
        {
            SendFloats(DllTool.ToUTF8Bytes(name), valuearray);
        }
        public void SendInts(string name, params int[] valuearray)
        {
            SendInts(DllTool.ToUTF8Bytes(name), valuearray);
        }
        public void SendBooleans(string name, params bool[] valuearray)
        {
            SendBooleans(DllTool.ToUTF8Bytes(name), valuearray);
        }
        public void SendMatrix(string name, float[] valuearray, int columns, int rows, int count)
        {
            SendMatrix(DllTool.ToUTF8Bytes(name), valuearray, columns, rows, count);
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
            SendTexture(DllTool.ToUTF8Bytes(name), texture);
        }
    }

    public partial class SpriteBatch
    {
        public void AttachAttribute(string name, Mesh mesh)
        {
            AttachAttribute(DllTool.ToUTF8Bytes(name), mesh);
        }

    }

    public partial class RandomGenerator
    {
        public void SetState(string state)
        {
            SetState(DllTool.ToUTF8Bytes(state));
        }
    }

    public partial class ImageData
    {
        public void Encode(FormatHandler.EncodedFormat format_type, string filename)
        {
            Encode(format_type, DllTool.ToUTF8Bytes(filename));
        }
    }

    #endregion
}