// Author : endlesstravel
// this file define function that trans string to utf8 bytes automatic to make libray easy to use

using System;
using System.Linq;

namespace Love
{
    #region Love Module

    public partial class FileSystem
    {
        /// <summary>
        /// Write data to a file in the save directory. If the file existed already, it will be completely replaced by the new contents.
        /// <para>encode with UTF-8</para>
        /// </summary>
        /// <param name="filename">The name (and path) of the file.</param>
        /// <param name="input">The string data to write to the file.</param>
        public static void Write(string filename, string input)
        {
            Write(filename, DllTool.GetUTF8Bytes(input));
        }

        /// <summary>
        /// Append text to an existing file (encode with UTF-8).
        /// </summary>
        /// <param name="filename">The name (and path) of the file.</param>
        /// <param name="txt">string to append</param>
        public static void Append(string filename, string txt)
        {
            Append(filename, DllTool.GetUTF8Bytes(txt));
        }

        //public static long _GetLastModified(string filename)
        //{
        //    return _GetLastModified(DllTool.GetNullTailUTF8Bytes(filename));
        //}
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
        public void SendColors(string name, params Color[] valueArray)
        {
            SendColors(name, valueArray.Select(
                item => new Color(item.Rf, item.Gf, item.Bf, item.Af))
                .ToArray());
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
    }

    public partial class ImageData
    {
        /// <summary>
        /// Encodes the ImageData and optionally writes it to the save directory.
        /// </summary>
        /// <param name="format_type">The format to encode the image as.</param>
        public FileData Encode(ImageFormat format)
        {
            return Encode(format, false, "");
        }

        /// <summary>
        /// Encodes the ImageData and optionally writes it to the save directory.
        /// </summary>
        /// <param name="format_type">The format to encode the image as.</param>
        /// <param name="filename">The filename to write the file to. If null, no file will be written but the FileData will still be returned.</param>
        /// <returns></returns>
        public FileData Encode(ImageFormat format, string filename)
        {
            return Encode(format, true, filename ?? throw new ArgumentNullException(nameof(filename)));
        }
    }

    #endregion
}