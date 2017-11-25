// Author : endlesstravel
// this file define function that trans string to utf8 bytes automatic to make libray easy to use

using System;
using Love2d.Type;
using Love2d.Module;

namespace Love2d.Module
{
    public partial class FileSystem
    {
        public static File newFile(string filename, File.Mode fmode_type = File.Mode.MODE_READ)
        {
            return newFile(DllTool.ToUTF8Bytes(filename), fmode_type);
        }
        public static FileData newFileData(string contents, string filename, FileData.Decoder decoder_type)
        {
            return newFileData(DllTool.ToUTF8Bytes(contents), DllTool.ToUTF8Bytes(filename), decoder_type);
        }
        public static FileData newFileData(byte[] contents, string filename, FileData.Decoder decoder_type)
        {
            return newFileData(contents, DllTool.ToUTF8Bytes(filename), decoder_type);
        }
        public static bool init(string args)
        {
            return init(DllTool.ToUTF8Bytes(args));
        }
        public static void setIdentity(string arg, bool append = false)
        {
            setIdentity(DllTool.ToUTF8Bytes(arg), append);
        }
        public static void setSource(string arg)
        {
            setSource(DllTool.ToUTF8Bytes(arg));
        }
        public static bool mount(string archive, string mountpoint, bool appendToPath)
        {
            return mount(DllTool.ToUTF8Bytes(archive), DllTool.ToUTF8Bytes(mountpoint), appendToPath);
        }
        public static bool unmount(string archive)
        {
            return unmount(DllTool.ToUTF8Bytes(archive));
        }
        public static string getRealDirectory(string filename)
        {
            return getRealDirectory(DllTool.ToUTF8Bytes(filename));
        }
        public static bool exists(string arg)
        {
            return exists(DllTool.ToUTF8Bytes(arg));
        }
        public static bool isDirectory(string arg)
        {
            return isDirectory(DllTool.ToUTF8Bytes(arg));
        }
        public static bool isFile(string arg)
        {
            return isFile(DllTool.ToUTF8Bytes(arg));
        }
        public static bool isSymlink(string filename)
        {
            return isSymlink(DllTool.ToUTF8Bytes(filename));
        }
        public static void createDirectory(string arg)
        {
            createDirectory(DllTool.ToUTF8Bytes(arg));
        }
        public static void remove(string arg)
        {
            remove(DllTool.ToUTF8Bytes(arg));
        }
        public static byte[] read(string filename, long len = -1)
        {
            return read(DllTool.ToUTF8Bytes(filename), len);
        }
        public static void write(string filename, byte[] input)
        {
            write(DllTool.ToUTF8Bytes(filename), input);
        }
        public static void append(string filename, byte[] input)
        {
            append(DllTool.ToUTF8Bytes(filename), input);
        }
        public static string[] getDirectoryItems(string dir)
        {
            return getDirectoryItems(DllTool.ToUTF8Bytes(dir));
        }
        public static long getLastModified(string filename)
        {
            return getLastModified(DllTool.ToUTF8Bytes(filename));
        }
        public static long getSize(string filename)
        {
            return getSize(DllTool.ToUTF8Bytes(filename));
        }
        public static void setRequirePath(string paths)
        {
            setRequirePath(DllTool.ToUTF8Bytes(paths));
        }
    }

    public partial class Window
    {
        public static void setTitle(string titleStr)
        {
            setTitle(DllTool.ToUTF8Bytes(titleStr));
        }
        public static bool showMessageBox(string title, string message, MessageBoxType msgbox_type, bool attachToWindow = true)
        {
            return showMessageBox(DllTool.ToUTF8Bytes(title), DllTool.ToUTF8Bytes(message), msgbox_type, attachToWindow);
        }
    }

    public partial class JoystickModule
    {
        public static bool setGamepadMapping(string guid, Joystick.InputType gp_inputType_type, Joystick.InputType j_inputType_type, int inputIndex, Joystick.Hat hat_type)
        {
            return setGamepadMapping(DllTool.ToUTF8Bytes(guid), gp_inputType_type, j_inputType_type, inputIndex, hat_type);
        }
        public static bool loadGamepadMappings(string str)
        {
            return loadGamepadMappings(DllTool.ToUTF8Bytes(str));
        }
    }

    public partial class Graphics
    {
        public static Shader newShader(string vertexCodeStr, string pixelCodeStr)
        {
            IntPtr out_shader = IntPtr.Zero;
            string vertexCode, pixelCode;
            Love2dGraphicsShaderBoot.shaderCodeToGLSL(vertexCodeStr, pixelCodeStr, out vertexCode, out pixelCode);
            Love2dDll.wrap_love_dll_graphics_newShader(DllTool.ToUTF8Bytes(vertexCode), DllTool.ToUTF8Bytes(pixelCode), out out_shader);
            return LoveObject.newObject<Shader>(out_shader);
        }
    }
    public partial class FontModule
    {
        Rasterizer newImageRasterizer(ImageData imageData, string glyphs, int extraspacing)
        {
            return newImageRasterizer(imageData, DllTool.ToUTF8Bytes(glyphs), extraspacing);
        }
        GlyphData newGlyphData(Rasterizer rasterizer, string glyph)
        {
            return newGlyphData(rasterizer, DllTool.ToUTF8Bytes(glyph));
        }
    }


}

namespace Love2d.Type
{
    public partial class Rasterizer
    {
        public GlyphData getGlyphData(string str)
        {
            return getGlyphData(DllTool.ToUTF8Bytes(str));
        }
        public bool hasGlyphs(string str)
        {
            return hasGlyphs(DllTool.ToUTF8Bytes(str));
        }
    }
    public partial class Font
    {
        public int getWidth(string str)
        {
            return getWidth(DllTool.ToUTF8Bytes(str));
        }
        public bool hasGlyphs(string str)
        {
            return hasGlyphs(DllTool.ToUTF8Bytes(str));
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
        public void setAttributeEnabled(string name, bool enable)
        {
            setAttributeEnabled(DllTool.ToUTF8Bytes(name), enable);
        }
        public bool isAttributeEnabled(string name)
        {
            return isAttributeEnabled(DllTool.ToUTF8Bytes(name));
        }
        public void attachAttribute(string name, Mesh otherMesh)
        {
            attachAttribute(DllTool.ToUTF8Bytes(name), otherMesh);
        }
    }
    public partial class Shader
    {
        public void sendColors(string name, params Int4[] valuearray)
        {
            sendColors(DllTool.ToUTF8Bytes(name), valuearray);
        }
        public void sendFloats(string name, params float[] valuearray)
        {
            sendFloats(DllTool.ToUTF8Bytes(name), valuearray);
        }
        public void sendInts(string name, params int[] valuearray)
        {
            sendInts(DllTool.ToUTF8Bytes(name), valuearray);
        }
        public void sendBooleans(string name, params bool[] valuearray)
        {
            sendBooleans(DllTool.ToUTF8Bytes(name), valuearray);
        }
        public void sendMatrix(string name, float[] valuearray)
        {
            sendMatrix(DllTool.ToUTF8Bytes(name), valuearray);
        }
        public void sendMatrix(string name, Matrix22 valuearray)
        {
            sendMatrix(DllTool.ToUTF8Bytes(name), valuearray);
        }
        public void sendMatrix(string name, Matrix33 valuearray)
        {
            sendMatrix(DllTool.ToUTF8Bytes(name), valuearray);
        }
        public void sendMatrix(string name, Matrix44 valuearray)
        {
            sendMatrix(DllTool.ToUTF8Bytes(name), valuearray);
        }
        public void sendTexture(string name, Texture texture)
        {
            sendTexture(DllTool.ToUTF8Bytes(name), texture);
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
            attachAttribute(DllTool.ToUTF8Bytes(name), mesh);
        }

    }

    public partial class RandomGenerator
    {
        public void setState(string state)
        {
            setState(DllTool.ToUTF8Bytes(state));
        }
    }

    public partial class ImageData
    {
        public void encode(EncodedFormat format_type, string filename)
        {
            encode(format_type, DllTool.ToUTF8Bytes(filename));
        }
    }

}