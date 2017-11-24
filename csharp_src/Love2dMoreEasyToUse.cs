// Author : endlesstravel
// this part aim is that make love2d for cs more easy to be used

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Love2d.Module;
using Love2d.Type;

namespace Love2d.Module
{
    public partial class FileSystem
    {
        //// new plus
        public static FileData newFileData(string filename)
        {
            return newFileData(newFile(filename));
        }
        //// end new plus
    }


    public partial class Audio
    {
        //// new plus
        public static Source newSource(File file, Source.Type type = Source.Type.TYPE_STREAM)
        {
            var sounddata = Sound.newSoundData(file);
            return newSource(sounddata);
        }
        public static Source newSource(FileData fdata, Source.Type type = Source.Type.TYPE_STREAM)
        {
            return newSource(Sound.newDecoder(fdata));
        }
        public static Source newSource(string filename, Source.Type type = Source.Type.TYPE_STREAM)
        {
            //return newSource(FileSystem.newFile(filename));
            var file = FileSystem.newFile(filename);
            var dec = Sound.newDecoder(file);
            return newSource(dec);
        }
        //// end new plus
    }

    public partial class ImageModule
    {
        public static ImageData newImageData(string filename)
        {
            var filedata = FileSystem.newFileData(filename);
            return newImageData(filedata);
        }
        public static CompressedImageData newCompressedData(string filename)
        {
            var filedata = FileSystem.newFileData(filename);
            return newCompressedData(filedata);
        }
    }

    public partial class FontModule
    {
        public static Rasterizer newRasterizer(string filename)
        {
            var filedata = FileSystem.newFileData(filename);
            return newRasterizer(filedata);
        }
    }

    public partial class VideoModule
    {
        public static VideoStream newVideoStream(string filename)
        {
            var file = FileSystem.newFile(filename);
            return newVideoStream(file);
        }
    }

    public partial class Graphics
    {
        public static Image newImage(ImageData imageData, bool flagMipmaps = false, bool flagLinear = false)
        {
            return newImage(new ImageData[] { imageData }, flagMipmaps, flagLinear);
        }
        public static Image newImage(CompressedImageData compressedImageData, bool flagMipmaps = false, bool flagLinear = false)
        {
            return newImage(new CompressedImageData[] { compressedImageData }, flagMipmaps, flagLinear);
        }
        public static Image newImage(string filename)
        {
            var filedata = ImageModule.newImageData(filename);
            return newImage(filedata);
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
        public static Mesh newMesh(Vertex[] vertices, Mesh.Usage drawMode, Mesh.Usage usage)
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
            return newMesh(posArray, uvArray, colorArray, drawMode, usage);
        }
        public static Text newText(Font font, string text)
        {
            return newText(font, DllTool.ToColoredStrings(text));
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
            return points(coords, colors);
        }
    }

}

namespace Love2d.Type
{
    public partial class File
    {
        public void write(byte[] data)
        {
            write(data, data.Length);
        }
        public void write(Data data)
        {
            write(data, data.getSize());
        }
    }

    public partial class Text
    {
        public int add(string text, float x, float y, float angle = 0, float sx = 1, float sy = 1, float ox = 0, float oy = 0, float kx = 0, float ky = 0)
        {
            return add(DllTool.ToColoredStrings(text), x, y, angle, sx, sy, ox, oy, kx, ky);
        }
        public int add(string text, float wraplimit, Font.AlignMode align_type, float x, float y, float angle = 0, float sx = 1, float sy = 1, float ox = 0, float oy = 0, float kx = 0, float ky = 0)
        {
            return add(DllTool.ToColoredStrings(text), wraplimit, align_type, x, y, angle, sx, sy, ox, oy, kx, ky);
        }
    }

}
