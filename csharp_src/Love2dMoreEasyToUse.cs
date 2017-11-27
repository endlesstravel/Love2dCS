// Author : endlesstravel
// this part aim is that make love2d for cs more easy to be used

using System;
using System.Collections.Generic;
using System.Linq;

namespace Love
{

    #region Love Module

    public partial class Window
    {
        public static bool SetFullscreen(bool fullscreen)
        {
            bool out_fullscreen; FullscreenType out_fstype;
            GetFullscreen(out out_fullscreen, out out_fstype);
            return SetFullscreen(fullscreen, out_fstype);
        }
        public static bool GetFullscreen()
        {
            bool out_fullscreen; FullscreenType out_fstype;
            GetFullscreen(out out_fullscreen, out out_fstype);
            return out_fullscreen;
        }
    }

    public partial class FileSystem
    {
        //// new plus
        public static FileData NewFileData(string filename)
        {
            return NewFileData(NewFile(filename));
        }
        //// end new plus
    }

    public partial class Sound
    {

        //// new plus
        public static SoundData NewSoundData(File file)
        {
            var decoder = NewDecoder(file);
            return NewSoundData(decoder);
        }
        public static Decoder NewDecoder(string filename, int bufferSize = Decoder.DEFAULT_BUFFER_SIZE)
        {
            var fdata = FileSystem.NewFileData(filename);
            return NewDecoder(fdata, bufferSize);
        }
        public static SoundData NewSoundData(string filename)
        {
            var decoder = NewDecoder(filename);
            return NewSoundData(decoder);
        }
        //// end new plus

    }

    public partial class Audio
    {
        //// new plus
        public static Source NewSource(File file, Source.Type type = Source.Type.Stream)
        {
            var sounddata = Sound.NewSoundData(file);
            return NewSource(sounddata);
        }
        public static Source NewSource(FileData fdata, Source.Type type = Source.Type.Stream)
        {
            return NewSource(Sound.NewDecoder(fdata));
        }
        public static Source NewSource(string filename, Source.Type type = Source.Type.Stream)
        {
            //return NewSource(FileSystem.NewFile(filename));
            var file = FileSystem.NewFile(filename);
            var dec = Sound.NewDecoder(file);
            return NewSource(dec);
        }
        //// end new plus
    }

    public partial class Image  // this is part of love module
    {
        public static ImageData NewImageData(string filename)
        {
            var filedata = FileSystem.NewFileData(filename);
            return NewImageData(filedata);
        }
        public static CompressedImageData NewCompressedData(string filename)
        {
            var filedata = FileSystem.NewFileData(filename);
            return NewCompressedData(filedata);
        }
    }

    public partial class Font
    {
        public static Rasterizer NewRasterizer(string filename)
        {
            var filedata = FileSystem.NewFileData(filename);
            return NewRasterizer(filedata);
        }
    }

    public partial class Video
    {
        public static VideoStream NewVideoStream(string filename)
        {
            var file = FileSystem.NewFile(filename);
            return NewVideoStream(file);
        }
    }

    public partial class Graphics
    {
        public static Image NewImage(ImageData imageData, bool flagMipmaps = false, bool flagLinear = false)
        {
            return NewImage(new ImageData[] { imageData }, flagMipmaps, flagLinear);
        }
        public static Image NewImage(CompressedImageData compressedImageData, bool flagMipmaps = false, bool flagLinear = false)
        {
            return NewImage(new CompressedImageData[] { compressedImageData }, flagMipmaps, flagLinear);
        }
        public static Image NewImage(string filename)
        {
            var filedata = Image.NewImageData(filename);
            return NewImage(filedata);
        }

        public struct Vertex
        {
            public readonly Float2 pos;
            public readonly Float2 uv;
            public readonly Int4 color;
            public Vertex(Float2 pos, Float2 uv, Int4 color)
            {
                this.pos = pos;
                this.uv = uv;
                this.color = color;
            }
        }
        public static Mesh NewMesh(Vertex[] vertices, Mesh.Usage drawMode, Mesh.Usage usage)
        {
            var posArray = new Float2[vertices.Length];
            var uvArray = new Float2[vertices.Length];
            var colorArray = new Int4[vertices.Length];
            for (int i = 0; i < vertices.Length; i++)
            {
                posArray[i] = vertices[i].pos;
                uvArray[i] = vertices[i].uv;
                colorArray[i] = vertices[i].color;
            }
            return NewMesh(posArray, uvArray, colorArray, drawMode, usage);
        }
        public static Text NewText(Font font, string text)
        {
            return NewText(font, DllTool.ToColoredStrings(text));
        }


        public struct ColoredPoint
        {
            public readonly Float2 pos;
            public readonly Int4 color;
            public ColoredPoint(Float2 pos, Int4 color)
            {
                this.pos = pos;
                this.color = color;
            }
        }

        public static bool points(ColoredPoint[] coloredPoints)
        {
            Float2[] coords = new Float2[coloredPoints.Length];
            Int4[] colors = new Int4[coloredPoints.Length];
            return Points(coords, colors);
        }
    }


    #endregion


    #region Love Type

    public partial class File
    {
        public void Write(byte[] data)
        {
            Write(data, data.Length);
        }
        public void Write(Data data)
        {
            Write(data, data.GetSize());
        }
    }

    public partial class Text
    {
        public int Add(string text, float x, float y, float angle = 0, float sx = 1, float sy = 1, float ox = 0, float oy = 0, float kx = 0, float ky = 0)
        {
            return Add(DllTool.ToColoredStrings(text), x, y, angle, sx, sy, ox, oy, kx, ky);
        }
        public int add(string text, float wraplimit, Font.AlignMode align_type, float x, float y, float angle = 0, float sx = 1, float sy = 1, float ox = 0, float oy = 0, float kx = 0, float ky = 0)
        {
            return Add(DllTool.ToColoredStrings(text), wraplimit, align_type, x, y, angle, sx, sy, ox, oy, kx, ky);
        }
    }


    #endregion
}
