﻿// Author : endlesstravel
// this part aim is that make love2d for cs more easy to be used

using System;
using System.Collections.Generic;
using System.Linq;

namespace Love
{

    #region Love Module

    public partial class Window
    {
        /// <summary>
        /// Enters or exits fullscreen. The display to use when entering fullscreen is chosen based on which display the window is currently in, if multiple monitors are connected.
        /// </summary>
        /// <param name="fullscreen">Whether to enter or exit fullscreen mode.</param>
        /// <returns>True if an attempt to enter fullscreen was successful, false otherwise.</returns>
        public static bool SetFullscreen(bool fullscreen)
        {
            bool out_fullscreen; FullscreenType out_fstype;
            GetFullscreen(out out_fullscreen, out out_fstype);
            return SetFullscreen(fullscreen, out_fstype);
        }

        /// <summary>
        /// Gets whether the window is fullscreen.
        /// </summary>
        /// <returns>True if the window is fullscreen, false otherwise.</returns>
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
        /// <summary>
        /// Creates a new Source from file data. 
        /// </summary>
        /// <param name="fdata">The FileData to create a Source from.</param>
        /// <param name="type">Streaming or static source.</param>
        /// <returns></returns>
        public static Source NewSource(FileData fdata, SourceType type)
        {
            return NewSource(Sound.NewDecoder(fdata), type);
        }

        /// <summary>
        /// Creates a new Source from file name. 
        /// </summary>
        /// <param name="decoder">The filepath to the audio file.</param>
        /// <param name="type">Streaming or static source.</param>
        /// <returns></returns>
        public static Source NewSource(string filename, SourceType type)
        {
            var file = FileSystem.NewFile(filename);
            var dec = Sound.NewDecoder(file);
            return NewSource(dec, type);
        }

        /// <summary>
        /// Returns the orientation of the listener.
        /// </summary>
        /// <returns>tuple (Forward vector of the listener orientation, Up vector of the listener orientation.)</returns>
        public static Tuple<Float3, Float3> GetOrientation()
        {
            Float3 forword, up;
            GetOrientation(out forword, out up);
            return Tuple.Create(forword, up);
        }
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

    public partial class Mouse
    {
        /// <summary>
        /// <para>Creates a new hardware Cursor object from an image file or ImageData.</para>
        /// <para>Hardware cursors are framerate-independent and work the same way as normal operating system cursors. Unlike drawing an image at the mouse's current coordinates, hardware cursors never have visible lag between when the mouse is moved and when the cursor position updates, even at low framerates.</para>
        /// <para>The hot spot is the point the operating system uses to determine what was clicked and at what position the mouse cursor is. For example, the normal arrow pointer normally has its hot spot at the top left of the image, but a crosshair cursor might have it in the middle.</para>
        /// </summary>
        /// <param name="filename">Path to the image to use for the new Cursor.</param>
        /// <param name="hotX">The x-coordinate in the image of the cursor's hot spot.</param>
        /// <param name="hotY">The y-coordinate in the image of the cursor's hot spot.</param>
        /// <returns></returns>
        public static Cursor NewCursor(string filename, int hotX, int hotY)
        {
            return NewCursor(Image.NewImageData(filename), hotX, hotY);
        }


        public static Float2 GetPosition()
        {
            double out_x, out_y;
            Love2dDll.wrap_love_dll_mouse_getPosition(out out_x, out out_y);
            return new Float2((float)out_x, (float)out_y);
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
        /// <summary>
        /// Creates a new VideoStream. Currently only Ogg Theora video files are supported. VideoStreams can't draw videos, see love.graphics.newVideo for that.
        /// </summary>
        /// <param name="filename">The file path to the Ogg Theora video file.</param>
        /// <returns>A new VideoStream.</returns>
        public static VideoStream NewVideoStream(string filename)
        {
            var file = FileSystem.NewFile(filename);
            return NewVideoStream(file);
        }

        /// <summary>
        /// Starts playing the Video.
        /// </summary>
        public void Play()
        {
            GetStream().Play();
        }

        /// <summary>
        /// Pauses the Video.
        /// </summary>
        public void Pause()
        {
            GetStream().Pause();
        }

        /// <summary>
        /// Sets the current playback position of the Video.
        /// </summary>
        /// <param name="offset"></param>
        public void Seek(double offset)
        {
            GetStream().Seek(offset);
        }

        /// <summary>
        /// Rewinds the Video to the beginning.
        /// </summary>
        public void Rewind()
        {
            GetStream().Rewind();
        }

        /// <summary>
        /// Gets the current playback position of the Video.
        /// </summary>
        /// <returns></returns>
        public double Tell()
        {
            return GetStream().Tell();
        }

        /// <summary>
        /// Gets whether the Video is currently playing.
        /// </summary>
        /// <returns></returns>
        public bool IsPlaying()
        {
            return GetStream().IsPlaying();
        }

    }

    public partial class Graphics
    {
        /// <summary>
        /// Creates a new drawable Text object.
        /// </summary>
        /// <param name="font">The font to use for the text.</param>
        /// <param name="coloredStr">The initial string of text that the new Text object will contain. May be nil.</param>
        /// <returns></returns>
        public static Text NewText(Font font, string coloredStr)
        {
            Check.ArgumentNull(font, "font");
            return NewText(font, ColoredStringArray.Create(coloredStr));
        }

        public static Image NewImage(ImageData imageData, bool flagMipmaps = false, bool flagLinear = false)
        {
            return NewImage(new ImageData[] { imageData }, flagMipmaps, flagLinear);
        }
        public static Image NewImage(CompressedImageData compressedImageData, bool flagMipmaps = false, bool flagLinear = false)
        {
            return NewImage(new CompressedImageData[] { compressedImageData }, flagMipmaps, flagLinear);
        }

        /// <summary>
        /// Creates a new Image from a filepath.
        /// </summary>
        /// <param name="filename">The filepath to the image file.</param>
        /// <returns></returns>
        public static Image NewImage(string filename)
        {
            var filedata = Image.NewImageData(filename);
            return NewImage(filedata);
        }

        /// <summary>
        /// Creates a new Image from a filepath.
        /// </summary>
        /// <param name="filename">The filepath to the image file .</param>
        /// <param name="flagMipmaps">If true, mipmaps for the image will be automatically generated (or taken from the images's file if possible, if the image originated from a CompressedImageData). If this value is a table, it should contain a list of other filenames of images of the same format that have progressively half-sized dimensions, all the way down to 1x1. Those images will be used as this Image's mipmap levels.</param>
        /// <param name="flagLinear">True if the image's pixels should be interpreted as being linear RGB rather than sRGB-encoded, if gamma-correct rendering is enabled. Has no effect otherwise.</param>
        /// <returns></returns>
        public static Image NewImage(string filename, bool flagMipmaps = false, bool flagLinear = false)
        {
            var filedata = Image.NewImageData(filename);
            return NewImage(filedata, flagMipmaps, flagLinear);
        }


        /// <summary>
        /// </summary>
        /// <param name="filename">The filepath to the BMFont file.</param>
        /// <param name="imageFileName">The filepath to the BMFont's image file.</param>
        /// <returns></returns>
        public static Font NewBMFont(string filename, params string[] imageFilename)
        {
            if (imageFilename == null)
            {
                throw new Exception("imageFilename array can't be null !");
            }

            var imageData = new ImageData[imageFilename.Length];
            for (int i = 0; i < imageFilename.Length; i++)
            {
                imageData[i] = Image.NewImageData(imageFilename[i]);
            }
            var filedata  = FileSystem.NewFileData(filename);
            var rasterizerImage = Font.NewBMFontRasterizer(filedata, imageData);
            return NewFont(rasterizerImage);
        }


        /// <summary>
        /// Create a new BMFont. The filepath to the BMFont's image file specified inside the BMFont file will be used.
        /// </summary>
        /// <param name="filename">The filepath to the BMFont file.</param>
        /// <returns></returns>
        public static Font NewBMFont(string filename)
        {
            return NewBMFont(filename, new string[0]);
        }

        /// <summary>
        /// Creates a new Font by loading a specifically formatted image.
        /// <para>In versions prior to 0.9.0, LÖVE expects ISO 8859-1 encoding for the glyphs string.</para>
        /// <para>	This function can be slow if it is called repeatedly, such as from Scene.Update. If you need to use a specific resource often, create it once and store it somewhere it can be reused!</para>
        /// </summary>
        /// <param name="filename">The filepath to the image file.</param>
        /// <param name="glyphs">A string of the characters in the image in order from left to right.</param>
        /// <param name="extraspacing">Additional spacing (positive or negative) to apply to each glyph in the Font.</param>
        /// <returns></returns>
        public static Font NewImageFont(string filename, string glyphs, int extraspacing = 0)
        {
            var imageData = Image.NewImageData(filename);
            var glyphsBytes = DllTool.ToUTF8Bytes(glyphs);
            var rasterizerImage = Font.NewImageRasterizer(imageData, glyphsBytes, extraspacing);
            return NewFont(rasterizerImage);
        }

        /// <summary>
        /// Create a new TrueType font.
        /// </summary>
        /// <param name="filename">The filepath to the TrueType font file.</param>
        /// <param name="size">The size of the font in pixels.</param>
        /// <returns>A Font object which can be used to draw text on screen.</returns>
        public static Font NewFont(string filename, int size = 12, TrueTypeRasterizer.Hinting hinting = TrueTypeRasterizer.Hinting.Normal)
        {
            var fileData = FileSystem.NewFileData(filename);
            var rasterizer = Font.NewTrueTypeRasterizer(fileData, size, hinting);
            return NewFont(rasterizer);
        }

        /// <summary>
        /// Creates a new drawable Video. Currently only Ogg Theora video files are supported.
        /// </summary>
        /// <param name="filename">The file path to the Ogg Theora video file.</param>
        /// <param name="audio">Whether to try to load the video's audio into an audio Source. If not explicitly set to true or false, it will try without causing an error if the video has no audio.</param>
        /// <param name="dipScale">The DPI scale factor of the video. if it was null, value will be Graphics.GetDPIScale()</param>
        /// <returns></returns>
        Video NewVideo(string filename, bool audio = true, float? dipScale = null)
        {
            return NewVideo(Video.NewVideoStream(filename), audio, dipScale);
        }


        /// <summary>
        /// Draws an arc using the "pie" ArcType.
        /// </summary>
        /// <param name="draw_mode"></param>
        /// <param name="x">The position of the center along x-axis.</param>
        /// <param name="y">The position of the center along y-axis.</param>
        /// <param name="radius">Radius of the arc.</param>
        /// <param name="angle1">The angle at which the arc begins.</param>
        /// <param name="angle2">The angle at which the arc terminates.</param>
        public static void Arc(DrawMode draw_mode, float x, float y, float radius, float angle1, float angle2)
        {
            Arc(draw_mode, ArcType.Pie, x, y, radius, angle1, angle2);
        }

        /// <summary>
        /// Discards (trashes) the contents of the screen or active Canvas. This is a performance optimization function with niche use cases.
        /// </summary>
        /// <param name="discardColor">Whether to discard the texture(s) of the active Canvas(es) (the contents of the screen if no Canvas is active.)</param>
        /// <param name="discardStencil">Whether to discard the contents of the stencil buffer of the screen / active Canvas.</param>
        public static void Discard(bool discardColor = true, bool discardStencil = true)
        {
            var dc = new bool[] { discardColor };
            Discard(dc, discardStencil);
        }

        /// <summary>
        /// Creates a new Mesh.
        /// <para>Use Mesh.SetTexture if the Mesh should be textured with an Image or Canvas when it's drawn.</para>
        /// <para>In versions prior to 11.0, color and byte component values were within the range of 0 to 255 instead of 0 to 1.</para>
        /// </summary>
        /// <param name="vertices">The array filled with vertex information tables for each vertex as follows:</param>
        /// <param name="drawMode">How the vertices are used when drawing. The default mode "fan" is sufficient for simple convex polygons.</param>
        /// <param name="usage">The expected usage of the Mesh. The specified usage mode affects the Mesh's memory usage and performance.</param>
        /// <returns>The new mesh.</returns>
        public static Mesh NewMesh(Vertex[] vertices, MeshDrawMode drawMode = MeshDrawMode.Fan, SpriteBatchUsage usage = SpriteBatchUsage.Dynamic)
        {
            var posArray = new Float2[vertices.Length];
            var uvArray = new Float2[vertices.Length];
            var colorArray = new Float4[vertices.Length];
            for (int i = 0; i < vertices.Length; i++)
            {
                posArray[i] = vertices[i].pos;
                uvArray[i] = vertices[i].uv;
                colorArray[i] = vertices[i].color;
            }
            return NewMesh(posArray, uvArray, colorArray, drawMode, usage);
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

        public static void Points(ColoredPoint[] coloredPoints)
        {
            Float2[] coords = new Float2[coloredPoints.Length];
            Int4[] colors = new Int4[coloredPoints.Length];
            Points(coords, colors);
        }

        public static void Print(ColoredString[] coloredStr, float x, float y, float angle = 0, float sx = 1, float sy = 1, float ox = 0, float oy = 0, float kx = 0, float ky = 0)
        {
            Print(new ColoredStringArray(coloredStr), x, y, angle, sx, sy, ox, oy, kx, ky);
        }


        /// <summary>
        /// <para> Draws formatted text, with word wrap and alignment.</para>
        /// <para> See additional notes in love.graphics.print. </para>
        /// <para>In version 0.9.2 and earlier, wrapping was implemented by breaking up words by spaces and putting them back together to make sure things fit nicely within the limit provided. However, due to the way this is done, extra spaces between words would end up missing when printed on the screen, and some lines could overflow past the provided wrap limit. In version 0.10.0 and newer this is no longer the case.</para>
        /// <para>Aligning does not work as one might expect! It doesn't align to the x/y coordinates given, but in a rectangle, where the limit is the width.</para>
        /// <para>Text may appear blurry if it's rendered at non-integer pixel locations.</para>
        /// </summary>
        /// <param name="coloredStr">A text string.</param>
        /// <param name="x">The position on the x-axis.</param>
        /// <param name="y">The position on the y-axis.</param>
        /// <param name="wrap"></param>
        /// <param name="align_type">Wrap the line after this many horizontal pixels.</param>
        /// <param name="angle">Orientation (radians).</param>
        /// <param name="sx">Scale factor (x-axis).</param>
        /// <param name="sy">Scale factor (y-axis).</param>
        /// <param name="ox">Origin offset (x-axis).</param>
        /// <param name="oy">Origin offset (y-axis).</param>
        /// <param name="kx">Shearing factor (x-axis).</param>
        /// <param name="ky">Shearing factor (y-axis).</param>
        public static void Printf(string text, float x, float y, float wrap, Font.AlignMode align_type = Font.AlignMode.Left, float angle = 0, float sx = 1, float sy = 1, float ox = 0, float oy = 0, float kx = 0, float ky = 0)
        {
            Printf(ColoredStringArray.Create(text), x, y, wrap, align_type, angle, sx, sy, ox, oy, kx, ky);
        }

        /// <summary>
        /// Disables stencil testing.
        /// </summary>
        public static void SetStencilTest()
        {
            SetStencilTest(CompareMode.Always, 0);
        }

        /// <summary>
        /// Gets the width and height of the window.
        /// </summary>
        /// <returns></returns>
        public Int2 GetDimensions()
        {
            return new Int2(GetWidth(), GetHeight());
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
            return Add(ColoredStringArray.Create(text), x, y, angle, sx, sy, ox, oy, kx, ky);
        }
        public int add(string text, float wraplimit, Font.AlignMode align_type, float x, float y, float angle = 0, float sx = 1, float sy = 1, float ox = 0, float oy = 0, float kx = 0, float ky = 0)
        {
            return Addf(ColoredStringArray.Create(text), wraplimit, align_type, x, y, angle, sx, sy, ox, oy, kx, ky);
        }
    }


    #endregion
}
