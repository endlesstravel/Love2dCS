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
        public static FileData NewFileData(string contents, string filename, FileData.Decoder decoder_type)
        {
            return NewFileData(DllTool.ToUTF8Bytes(contents), DllTool.ToUTF8Bytes(filename), decoder_type);
        }
        public static FileData NewFileData(byte[] contents, string filename, FileData.Decoder decoder_type)
        {
            return NewFileData(contents, DllTool.ToUTF8Bytes(filename), decoder_type);
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
        public static bool Exists(string arg)
        {
            return Exists(DllTool.ToUTF8Bytes(arg));
        }
        public static bool IsDirectory(string arg)
        {
            return IsDirectory(DllTool.ToUTF8Bytes(arg));
        }
        public static bool IsFile(string arg)
        {
            return IsFile(DllTool.ToUTF8Bytes(arg));
        }
        public static bool IsSymlink(string filename)
        {
            return IsSymlink(DllTool.ToUTF8Bytes(filename));
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
        public static long GetSize(string filename)
        {
            return GetSize(DllTool.ToUTF8Bytes(filename));
        }
        public static void SetRequirePath(string paths)
        {
            SetRequirePath(DllTool.ToUTF8Bytes(paths));
        }
    }

    public partial class Window
    {
        public static void SetTitle(string titleStr)
        {
            SetTitle(DllTool.ToUTF8Bytes(titleStr));
        }
        public static bool ShowMessageBox(string title, string message, MessageBoxType msgbox_type, bool attachToWindow = true)
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
        public static Shader NewShader(string vertexCodeStr, string pixelCodeStr)
        {
            IntPtr out_shader = IntPtr.Zero;
            string vertexCode, pixelCode;
            Love2dGraphicsShaderBoot.shaderCodeToGLSL(vertexCodeStr, pixelCodeStr, out vertexCode, out pixelCode);
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
        public Tuple<string[], int> getWrap(string text, float wrap_limit)
        {
            ColoredString coloredStr = DllTool.ToColoredStrings(text);
            IntPtr out_pws = IntPtr.Zero;
            int out_maxWidth = 0;

            coloredStr.ExecResource((Tuple<BytePtr[], Int4[]> tmp) =>{
                Love2dDll.wrap_love_dll_type_Font_getWrap(p, tmp.Item1, tmp.Item2, coloredStr.Length, wrap_limit, out out_maxWidth, out out_pws);
            });
            var lines = DllTool.WSSToStringListAndRelease(out_pws);
            return new Tuple<string[], int>(lines, out_maxWidth);
        }
    }

    public partial class Mesh
    {
        public void SetAttributeEnabled(string name, bool enable)
        {
            SetAttributeEnabled(DllTool.ToUTF8Bytes(name), enable);
        }
        public bool IsAttributeEnabled(string name)
        {
            return IsAttributeEnabled(DllTool.ToUTF8Bytes(name));
        }
        public void AttachAttribute(string name, Mesh otherMesh)
        {
            AttachAttribute(DllTool.ToUTF8Bytes(name), otherMesh);
        }
    }
    public partial class Shader
    {
        public void SendColors(string name, params Int4[] valuearray)
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
        public void SendMatrix(string name, float[] valuearray)
        {
            SendMatrix(DllTool.ToUTF8Bytes(name), valuearray);
        }
        public void SendMatrix(string name, Matrix22 valuearray)
        {
            SendMatrix(DllTool.ToUTF8Bytes(name), valuearray);
        }
        public void SendMatrix(string name, Matrix33 valuearray)
        {
            SendMatrix(DllTool.ToUTF8Bytes(name), valuearray);
        }
        public void SendMatrix(string name, Matrix44 valuearray)
        {
            SendMatrix(DllTool.ToUTF8Bytes(name), valuearray);
        }
        public void SendTexture(string name, Texture texture)
        {
            SendTexture(DllTool.ToUTF8Bytes(name), texture);
        }

        public Tuple<UniformType, int, int> getExternVariable(string name)
        {
            return getExternVariable(DllTool.ToUTF8Bytes(name));
        }

    }

    public partial class SpriteBatch
    {
        public void attachAttribute(string name, Mesh mesh)
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
        public void encode(EncodedFormat format_type, string filename)
        {
            Encode(format_type, DllTool.ToUTF8Bytes(filename));
        }
    }

    #endregion
}