// Author : endlesstravel
// this part aim is that make love2d for cs more easy to be used

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Love
{


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

    public partial class Sound
    {
        /// <summary>
        /// Attempts to find a decoder for the encoded sound data in the specified file.
        /// </summary>
        /// <param name="filename">The filename of the file with encoded sound data.</param>
        /// <param name="bufferSize">The size of each decoded chunk, in bytes.</param>
        /// <returns></returns>
        public static Decoder NewDecoder(string filename, int bufferSize = Decoder.DEFAULT_BUFFER_SIZE)
        {
            var fdata = FileSystem.NewFileData(filename);
            return NewDecoder(fdata, bufferSize);
        }

        /// <summary>
        /// <para> Creates a new SoundData.</para>
        /// <para>It's also possible to create SoundData with a custom sample rate, channel and bit depth.</para>
        /// <para>The sound data will be decoded to the memory in a raw format. It is recommended to create only short sounds like effects, as a 3 minute song uses 30 MB of memory this way.</para>
        /// </summary>
        /// <param name="file">A File pointing to an audio file.</param>
        /// <returns>A new SoundData object.</returns>
        public static SoundData NewSoundData(File file)
        {
            var decoder = NewDecoder(file);
            return NewSoundData(decoder);
        }

        /// <summary>
        /// <para> Creates a new SoundData.</para>
        /// <para>It's also possible to create SoundData with a custom sample rate, channel and bit depth.</para>
        /// <para>The sound data will be decoded to the memory in a raw format. It is recommended to create only short sounds like effects, as a 3 minute song uses 30 MB of memory this way.</para>
        /// </summary>
        /// <param name="filename">The file name of the file to load.</param>
        /// <returns>A new SoundData object.</returns>
        public static SoundData NewSoundData(string filename)
        {
            var decoder = NewDecoder(filename);
            return NewSoundData(decoder);
        }

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
        /// <param name="filename">The filepath to the audio file.</param>
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
        public static Tuple<Vector3, Vector3> GetOrientation()
        {
            Vector3 forword, up;
            GetOrientation(out forword, out up);
            return Tuple.Create(forword, up);
        }
    }

    public partial class Image  // this is part of love module
    {
        /// <summary>
        /// Creates a new <see cref="ImageData"/> object.
        /// </summary>
        /// <param name="filename">The filename of the image file.</param>
        /// <returns>The new ImageData object.</returns>
        public static ImageData NewImageData(string filename)
        {
            var filedata = FileSystem.NewFileData(filename);
            return NewImageData(filedata);
        }

        /// <summary>
        /// Create a new <see cref="CompressedImageData"/> object from a compressed image file. LÖVE supports several compressed texture formats, enumerated in the <see cref="PixelFormat"/> page.
        /// </summary>
        /// <param name="filename">The filename of the compressed image file.</param>
        /// <returns>The new CompressedImageData object.</returns>
        public static CompressedImageData NewCompressedData(string filename)
        {
            var filedata = FileSystem.NewFileData(filename);
            return NewCompressedData(filedata);
        }

        /// <summary>
        /// Creates a new ImageData object. -----  Vector4[x, y]
        /// <para> Vector4[x, y] - new Vector4(0.1f, 0.2f, 0.3f, 0.4f) </para>
        /// </summary>
        /// <param name="rawData">color data to set</param>
        /// <param name="format">The pixel format of the ImageData.</param>
        /// <returns></returns>
        public static ImageData NewImageData(Vector4[,] rawData, ImageDataPixelFormat format)
        {
            Check.ArgumentNull(rawData, "rawData");
            int w = rawData.GetLength(0);
            int h = rawData.GetLength(1);
            Vector4[] data = new Vector4[w * h];

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    data[x + y * w] = rawData[x, y];
                }
            }
            var imageData = NewImageData(w, h, format);
            imageData.SetPixelsFloat(data);
            return imageData;
        }


        /// <summary>
        /// Creates a new ImageData object. -----  Vector4[x][y]
        /// </summary>
        /// <param name="rawData">Optional raw byte data to load into the ImageData, in the format specified by format.</param>
        /// <param name="format">The pixel format of the ImageData.</param>
        /// <returns></returns>
        public static ImageData NewImageData(Vector4[][] rawData, ImageDataPixelFormat format)
        {
            CheckTDA(rawData, out int W, out int H);

            // copy data
            Vector4[] data = new Vector4[W * H];
            for (int y = 0; y < H; y++)
            {
                for (int x = 0; x < W; x++)
                {
                    data[x + y * W] = rawData[x][y];
                }
            }

            var imageData = NewImageData(W, H, format);
            imageData.SetPixelsFloat(data);
            return imageData;
        }

        internal static void CheckTDA<T>(T[][] rawData, out int w, out int h)
        {
            if (rawData == null)
                throw new ArgumentNullException(nameof(rawData));

            w = 0;
            h = rawData.Length;
            if (h > 0)
            {
                if (rawData[0] == null)
                {
                    throw new Exception("col[0] was null !");
                }
                w = rawData[0].Length;

                for (int i = 1; i < rawData.Length; i++)
                {
                    var row = rawData[i];
                    if (row == null)
                    {
                        throw new Exception($"col[{i}] was null !");
                    }

                    if (row.Length != w)
                    {
                        throw new Exception($"w not equal with col[{i}]");
                    }
                }
            }
            else
                throw new Exception("size of data is [0][?] !");
        }

        /// <summary>
        /// Creates a new ImageData object. -----  Color[x][y]
        /// </summary>
        /// <param name="rawData">Optional raw byte data to load into the ImageData, in the format specified by format.</param>
        /// <param name="format">The pixel format of the ImageData.</param>
        /// <returns></returns>
        public static ImageData NewImageData(Color[][] rawData, ImageDataPixelFormat format)
        {
            CheckTDA(rawData, out int W, out int H);
            var imageData = NewImageData(W, H, format);
            imageData.SetPixels(rawData);
            return imageData;
        }

        /// <summary>
        /// Creates a new ImageData object. -----  Color[x, y]
        /// </summary>
        /// <param name="rawData">Optional raw byte data to load into the ImageData, in the format specified by format.</param>
        /// <param name="format">The pixel format of the ImageData.</param>
        /// <returns></returns>
        public static ImageData NewImageData(Color[, ] rawData, ImageDataPixelFormat format)
        {
            if (rawData == null)
                throw new ArgumentNullException(nameof(rawData));

            int W = rawData.GetLength(0);
            int H = rawData.GetLength(1);

            var imageData = NewImageData(W, H, format);
            imageData.SetPixels(rawData);
            return imageData;
        }

        /// <summary>
        /// Determines whether a file can be loaded as <see cref="CompressedImageData"/>.
        /// </summary>
        /// <param name="filename">The filename of the potentially compressed image file.</param>
        /// <returns>Whether the file can be loaded as <see cref="CompressedImageData"/> or not.</returns>
        public static bool IsCompressed(string filename)
        {
            return IsCompressed(FileSystem.NewFileData(filename));
        }
    }
    public partial class ImageData
    {
        public delegate Vector4 MapVectorDelegate(int x, int y, Vector4 p);
        public delegate Color MapColorDelegate(int x, int y, Color color);

        #region MapPixel


        /// <summary>
        /// <para> Transform an image by applying a function to every pixel. </para>
        /// <para> This function is a higher-order function(https://en.wikipedia.org/wiki/Higher-order_function). It takes another function as a parameter, and calls it once for each pixel in the ImageData. </para>
        /// <para>The passed function is called with six parameters for each pixel in turn. The parameters are numbers that represent the x and y coordinates of the pixel and its red, green, blue and alpha values. The function should return the new red, green, blue, and alpha values for that pixel.</para>
        /// </summary>
        /// <param name="func">Function to apply to every pixel.</param>
        public void MapPixel(MapColorDelegate func)
        {
            MapPixel(func, 0, 0, GetWidth(), GetHeight());
        }


        /// <summary>
        /// <para> Transform an image by applying a function to every pixel. </para>
        /// <para> This function is a higher-order function(https://en.wikipedia.org/wiki/Higher-order_function). It takes another function as a parameter, and calls it once for each pixel in the ImageData. </para>
        /// <para>The passed function is called with six parameters for each pixel in turn. The parameters are numbers that represent the x and y coordinates of the pixel and its red, green, blue and alpha values. The function should return the new red, green, blue, and alpha values for that pixel.</para>
        /// </summary>
        /// <param name="func">Function to apply to every pixel.</param>
        /// <param name="sx">The x-axis of the top-left corner of the area within the ImageData to apply the function to.</param>
        /// <param name="sy">The y-axis of the top-left corner of the area within the ImageData to apply the function to.</param>
        /// <param name="w">The width of the area within the ImageData to apply the function to.</param>
        /// <param name="h">The height of the area within the ImageData to apply the function to.</param>
        public void MapPixel(MapColorDelegate func, int sx, int sy, int w, int h)
        {
            int ex = sx + w - 1;
            int ey = sy + h - 1;
            if (!CheckInSide(sx, sy, ex, ey))
                throw new Exception("Invalid rectangle dimensions.");

            var fdata = GetPixels(sx, sy, w, h);
            int idx = 0;
            for (int y = sy; y <= ey; y++)
            {
                for (int x = sx; x <= ex; x++)
                {
                    fdata[idx] = func(x, y, fdata[idx]);
                    idx++;
                }
            }

            SetPixels(sx, sy, w, h, fdata);
        }


        /// <summary>
        /// <para> Transform an image by applying a function to every pixel. </para>
        /// <para> This function is a higher-order function(https://en.wikipedia.org/wiki/Higher-order_function). It takes another function as a parameter, and calls it once for each pixel in the ImageData. </para>
        /// <para>The passed function is called with six parameters for each pixel in turn. The parameters are numbers that represent the x and y coordinates of the pixel and its red, green, blue and alpha values. The function should return the new red, green, blue, and alpha values for that pixel.</para>
        /// </summary>
        /// <param name="func">Function to apply to every pixel.</param>
        public void MapPixel(MapVectorDelegate func)
        {
            MapPixel(func, 0, 0, GetWidth(), GetHeight());
        }

        /// <summary>
        /// <para> Transform an image by applying a function to every pixel. </para>
        /// <para> This function is a higher-order function(https://en.wikipedia.org/wiki/Higher-order_function). It takes another function as a parameter, and calls it once for each pixel in the ImageData. </para>
        /// <para>The passed function is called with six parameters for each pixel in turn. The parameters are numbers that represent the x and y coordinates of the pixel and its red, green, blue and alpha values. The function should return the new red, green, blue, and alpha values for that pixel.</para>
        /// </summary>
        /// <param name="func">Function to apply to every pixel.</param>
        /// <param name="sx">The x-axis of the top-left corner of the area within the ImageData to apply the function to.</param>
        /// <param name="sy">The y-axis of the top-left corner of the area within the ImageData to apply the function to.</param>
        /// <param name="w">The width of the area within the ImageData to apply the function to.</param>
        /// <param name="h">The height of the area within the ImageData to apply the function to.</param>
        public void MapPixel(MapVectorDelegate func, int sx, int sy, int w, int h)
        {
            int ex = sx + w - 1;
            int ey = sy + h - 1;
            if (!CheckInSide(sx, sy, ex, ey))
                throw new Exception("Invalid rectangle dimensions.");

            var fdata = GetPixelsFloat(sx, sy, w, h);
            int idx = 0;
            for (int y = sy; y <= ey; y++)
            {
                for (int x = sx; x <= ex; x++)
                {
                    fdata[idx] = func(x, y, fdata[idx]);
                    idx++;
                }
            }

            SetPixelsFloat(sx, sy, w, h, fdata);
        }

        #endregion

        public Color[] GetPixels()
        {
            return GetPixels(0, 0, GetWidth(), GetHeight());
        }

        /// <param name="area">The area within the ImageData</param>
        public Color[] GetPixels(Rectangle area)
        {
            return GetPixels(area.X, area.Y, area.Width, area.Height);
        }
        
        /// <param name="x">The x-axis of the top-left corner of the area within the ImageData</param>
        /// <param name="y">The y-axis of the top-left corner of the area within the ImageData</param>
        /// <param name="w">The width of the area within the ImageData</param>
        /// <param name="h">The height of the area within the ImageData</param>
        public Color[] GetPixels(int x, int y, int w, int h)
        {
            var floatData = GetPixelsFloat(x, y, w, h);
            var len = floatData.Length;
            var colorData = new Color[len];
            for (int i = 0; i < len; i++)
            {
                var v = floatData[i];
                colorData[i] = new Color(v.r, v.g, v.b, v.a);
            }
            return colorData;
        }

        public void SetPixels(Color[] data)
        {
            SetPixels(0, 0, GetWidth(), GetHeight(), data);
        }

        public void SetPixels(Rectangle area, Color[] data)
        {
            SetPixels(area.X, area.Y, area.Width, area.Height, data);
        }

        /// <param name="x">The x-axis of the top-left corner of the area within the ImageData</param>
        /// <param name="y">The y-axis of the top-left corner of the area within the ImageData</param>
        /// <param name="w">The width of the area within the ImageData</param>
        /// <param name="h">The height of the area within the ImageData</param>
        public void SetPixels(int x, int y, int w, int h, Color[] data)
        {
            var len = data.Length;
            if (len != w * h)
                throw new Exception("data no fill size of width * height");

            var floatData = new Vector4[len];
            for (int i = 0; i < len; i++)
            {
                var c = data[i];
                floatData[i] = new Vector4(c.Rf, c.Gf, c.Bf, c.Af);
            }
            SetPixelsFloat(0, 0, w, h, floatData);
        }

        public void SetPixels(Color[][] data)
        {
            SetPixels(0, 0, GetWidth(), GetHeight(), data);
        }

        /// <param name="area">The area within the ImageData</param>
        public void SetPixels(Rectangle area, Color[][] data)
        {
            SetPixels(area.X, area.Y, area.Width, area.Height, data);
        }

        /// <param name="x">The x-axis of the top-left corner of the area within the ImageData</param>
        /// <param name="y">The y-axis of the top-left corner of the area within the ImageData</param>
        /// <param name="w">The width of the area within the ImageData, can small then the width of rawData</param>
        /// <param name="h">The height of the area within the ImageData, can small then the height of rawData</param>
        public void SetPixels(int x, int y, int w, int h, Color[][] rawData)
        {
            Image.CheckTDA(rawData, out int TW, out int TH);

            if (TW < w)
                throw new Exception($"{nameof(rawData)} small then width you give ({nameof(w)}) !");
            if (TH < h)
                throw new Exception($"{nameof(rawData)} small then height you give ({nameof(h)}) !");

            var pixelData = new Vector4[w * h];
            for (int iy = 0; iy < h; iy++)
            {
                for (int ix = 0; ix < w; ix++)
                {
                    int i = ix + iy * w;
                    var data = rawData[ix][iy];
                    pixelData[i].r = data.Rf;
                    pixelData[i].g = data.Gf;
                    pixelData[i].b = data.Bf;
                    pixelData[i].a = data.Af;
                }
            }
            SetPixelsFloat(x, y, w, h, pixelData);
        }

        public void SetPixels(Color[,] data)
        {
            SetPixels(0, 0, GetWidth(), GetHeight(), data);
        }

        public void SetPixels(int x, int y, Color[,] data)
        {
            SetPixels(x, y, data.GetLength(0), data.GetLength(1), data);
        }

        /// <param name="area">The area within the ImageData</param>
        public void SetPixels(Rectangle area, Color[,] data)
        {
            SetPixels(area.X, area.Y, area.Width, area.Height, data);
        }

        /// <param name="x">The x-axis of the top-left corner of the area within the ImageData</param>
        /// <param name="y">The y-axis of the top-left corner of the area within the ImageData</param>
        /// <param name="w">The width of the area within the ImageData, can small then the width of rawData</param>
        /// <param name="h">The height of the area within the ImageData, can small then the height of rawData</param>
        public void SetPixels(int x, int y, int w, int h, Color[,] rawData)
        {
            if (rawData.GetLength(0) < w)
                throw new Exception($"{nameof(rawData)}.GetLength(0) small then width you give ({nameof(w)}) !");
            if (rawData.GetLength(1) < h)
                throw new Exception($"{nameof(rawData)}.GetLength(1) small then height you give ({nameof(h)}) !");

            if (!CheckInSide(x, y, w, h))
                throw new Exception("Invalid rectangle dimensions.");

            var pixelData = new Vector4[w * h];
            for (int iy = 0; iy < h; iy++)
            {
                for (int ix = 0; ix < w; ix++)
                {
                    int i = ix + iy * w;
                    pixelData[i].r = rawData[ix, iy].Rf;
                    pixelData[i].g = rawData[ix, iy].Gf;
                    pixelData[i].b = rawData[ix, iy].Bf;
                    pixelData[i].a = rawData[ix, iy].Af;
                }
            }
            SetPixelsFloat(x, y, w, h, pixelData);
        }

        bool CheckInSide(int x, int y)
        {
            return 0 <= x && x < GetWidth() && 0 <= y && y < GetHeight();
        }

        bool CheckInSide(int x, int y, int w, int h)
        {
            return CheckInSide(x, y) && CheckInSide(x + w - 1, y + h - 1);
        }

        /// <summary>
        /// get every pixel, as Float4 format
        /// </summary>
        /// <returns></returns>
        public Vector4[] GetPixelsFloat()
        {
            return GetPixelsFloat(0, 0, GetWidth(), GetHeight());
        }

        /// <summary>
        /// get every pixel, as Float4 format
        /// </summary>
        /// <param name="x">The x-axis of the top-left corner of the area within the ImageData</param>
        /// <param name="y">The y-axis of the top-left corner of the area within the ImageData</param>
        /// <param name="w">The width of the area within the ImageData</param>
        /// <param name="h">The height of the area within the ImageData</param>
        public Vector4[] GetPixelsFloat(int x, int y, int w, int h)
        {
            if (!CheckInSide(x, y, w, h))
                throw new Exception("Invalid rectangle dimensions.");

            //Vector4[] output = new Vector4[w * h];
            //DllTool.ExecuteAsIntprt(output, dest => Love2dDll.inner_wrap_love_dll_type_ImageData_getPixels_float4(p, x, y, w, h, dest));
            Love2dDll.inner_wrap_love_dll_type_ImageData_getPixels_float4(p, x, y, w, h, out var out_dest);
            return DllTool.ReadVector4sAndRelease(out_dest, w * h);
        }

        /// <summary>
        /// set every pixel with given data, function will convert Float4 to correct pixel format automatically
        /// </summary>
        /// <param name="data">color data to set</param>
        public void SetPixelsFloat(Vector4[] data)
        {
            SetPixelsFloat(0, 0, GetWidth(), GetHeight(), data);
        }

        /// <summary>
        /// set every pixel with given data, function will convert Float4 to correct pixel format automatically
        /// </summary>
        /// <param name="x">The x-axis of the top-left corner of the area within the ImageData</param>
        /// <param name="y">The y-axis of the top-left corner of the area within the ImageData</param>
        /// <param name="w">The width of the area within the ImageData</param>
        /// <param name="h">The height of the area within the ImageData</param>
        /// <param name="data">color data to set</param>
        public void SetPixelsFloat(int x, int y, int w, int h, Vector4[] data)
        {
            Check.ArgumentNull(data, "data");
            if (data.Length != w * h)
            {
                throw new Exception("Length of input data not equal with w * h ");
            }

            Love2dDll.inner_wrap_love_dll_type_ImageData_setPixels_float4(p, x, y, w, h, data);
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
        /// Creates a new Canvas.
        /// <para>This function can be slow if it is called repeatedly, such as from love.update or love.draw. If you need to use a specific resource often, create it once and store it somewhere it can be reused!</para>
        /// </summary>
        /// <returns>A new Canvas with dimensions equal to the window's size in pixels.</returns>
        public static Canvas NewCanvas()
        {
            Window.GetMode(out int w, out int h);
            return NewCanvas(w, h);
        }

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

        ///// <summary>
        ///// Creates a new Image from a filepath.
        ///// </summary>
        ///// <param name="filename">The filepath to the image file.</param>
        ///// <returns></returns>
        //public static Image NewImage(string filename)
        //{
        //    var imagedata = Image.NewImageData(filename);
        //    return NewImage(imagedata);
        //}

        /// <summary>
        /// Creates a new Image from a filepath.
        /// </summary>
        /// <param name="filename">The filepath to the image file .</param>
        /// <param name="flagMipmaps">If true, mipmaps for the image will be automatically generated (or taken from the images's file if possible, if the image originated from a CompressedImageData). If this value is a table, it should contain a list of other filenames of images of the same format that have progressively half-sized dimensions, all the way down to 1x1. Those images will be used as this Image's mipmap levels.</param>
        /// <param name="flagLinear">True if the image's pixels should be interpreted as being linear RGB rather than sRGB-encoded, if gamma-correct rendering is enabled. Has no effect otherwise.</param>
        /// <returns></returns>
        public static Image NewImage(string filename, bool flagMipmaps = false, bool flagLinear = false)
        {
            var imagedata = Image.NewImageData(filename);
            return NewImage(imagedata, flagMipmaps, flagLinear);
        }

        /// <summary>
        /// Create a new instance of the default font (Vera Sans) with a custom size.
        /// </summary>
        /// <param name="size">The size of the font in pixels.</param>
        /// <param name="hinting">True Type hinting mode.</param>
        /// <returns></returns>
        public static Font NewFont(int size, HintingMode hinting = HintingMode.Normal)
        {
            var rasterizer = Font.NewTrueTypeRasterizer(size, hinting);
            return NewFont(rasterizer);
        }

        /// <summary>
        /// </summary>
        /// <param name="filename">The filepath to the BMFont file.</param>
        /// <param name="imageFileName">The filepath to the BMFont's image file.</param>
        /// <returns></returns>
        public static Font NewBMFont(string filename, params string[] imageFileName)
        {
            if (imageFileName == null)
            {
                throw new Exception("imageFilename array can't be null !");
            }

            var imageData = new ImageData[imageFileName.Length];
            for (int i = 0; i < imageFileName.Length; i++)
            {
                imageData[i] = Image.NewImageData(imageFileName[i]);
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
            var glyphsBytes = DllTool.GetNullTailUTF8Bytes(glyphs);
            var rasterizerImage = Font.NewImageRasterizer(imageData, glyphsBytes, extraspacing);
            return NewFont(rasterizerImage);
        }

        /// <summary>
        /// Create a new TrueType font.
        /// </summary>
        /// <param name="filename">The filepath to the TrueType font file.</param>
        /// <param name="size">The size of the font in pixels.</param>
        /// <param name="hinting">True Type hinting mode.</param>
        /// <returns>A Font object which can be used to draw text on screen.</returns>
        public static Font NewFont(string filename, int size = 12, HintingMode hinting = HintingMode.Normal)
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
        /// Clears the screen to transparent black (0, 0, 0, 0).
        /// </summary>
        public static void Clear()
        {
            var color = GetBackgroundColor();
            Clear(0, 0, 0, 0);
        }

        /// <summary>
        /// Clears the screen to transparent black (0, 0, 0, 0).
        /// </summary>
        public static void Clear(Color c)
        {
            Clear(c.Rf, c.Gf, c.Bf, c.Af);
        }

        /// <summary>
        /// Sets the color used for drawing.
        /// </summary>
        public static void SetColor(Color color)
        {
            SetColor(color.Rf, color.Gf, color.Bf, color.Af);
        }

        /// <summary>
        /// Sets the color used for drawing.
        /// </summary>
        public static void SetColor(Vector4 color)
        {
            SetColor(color.X, color.Y, color.Z, color.W);
        }

        /// <summary>
        /// Draws a circle.
        /// </summary>
        /// <param name="mode_type">How to draw the circle.</param>
        /// <param name="center">The position of the center .</param>
        /// <param name="radius">The radius of the circle.</param>
        /// <param name="points">The number of segments used for drawing the circle. Note: The default variable for the segments parameter varies between different versions of LÖVE.</param>
        /// <returns></returns>
        public static void Circle(DrawMode mode_type, Vector2 center, float radius)
        {
            Circle(mode_type, center.X, center.Y, radius);
        }
        /// <summary>
        /// Draws a circle.
        /// </summary>
        /// <param name="mode_type">How to draw the circle.</param>
        /// <param name="center">The position of the center .</param>
        /// <param name="radius">The radius of the circle.</param>
        /// <param name="points">The number of segments used for drawing the circle. Note: The default variable for the segments parameter varies between different versions of LÖVE.</param>
        /// <returns></returns>
        public static void Circle(DrawMode mode_type, Vector2 center, float radius, int point)
        {
            Circle(mode_type, center.X, center.Y, radius, point);
        }


        /// <summary>
        /// Draws a rectangle.
        /// </summary>
        /// <param name="mode">How to draw the rectangle.</param>
        /// <param name="rect">the rectangle to draw.</param>
        public static void Rectangle(DrawMode mode, RectangleF rect)
        {
            Rectangle(mode, rect.X, rect.Y, rect.Width, rect.Height);
        }

        /// <summary>
        /// Draws a rectangle.
        /// </summary>
        /// <param name="mode">How to draw the rectangle.</param>
        /// <param name="rect">the rectangle to draw.</param>
        public static void Rectangle(DrawMode mode, Rectangle rect)
        {
            Rectangle(mode, rect.X, rect.Y, rect.Width, rect.Height);
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
        /// Each vertex attribute component is initialized to 0 if its data type is "float", or 1 if its data type is "byte". Mesh:setVertices or Mesh:setVertex and Mesh:setDrawRange can be used to specify vertex information once the Mesh is created.
        /// <para> If the data type of an attribute is "float", components can be in the range 1 to 4, if the data type is "byte" it must be 4. </para>
        /// <para> If a custom vertex attribute uses the name "VertexPosition", "VertexTexCoord", or "VertexColor", then the vertex data for that vertex attribute will be used for the standard vertex positions, texture coordinates, or vertex colors respectively, when drawing the Mesh.Otherwise a Vertex Shader is required in order to make use of the vertex attribute when the Mesh is drawn. </para>
        /// </summary>
        public static Mesh NewMesh(MeshFormatDescribe desc, byte[] data, 
            MeshDrawMode drawMode = MeshDrawMode.Fan, SpriteBatchUsage usage = SpriteBatchUsage.Dynamic)
        {
            IEnumerable<MeshFormatDescribe.Entry> formatList = desc.entry;
            string[] strList = formatList.Select(item => item.name).ToArray();
            int[] typeList = formatList.Select(item => (int)item.type).ToArray();
            int[] comCountList = formatList.Select(item => item.componentCount).ToArray();

            IntPtr meshPtr = IntPtr.Zero;
            DllTool.ExecuteNullTailStringArray(strList, (strListPtr) =>
            {
                Love2dDll.wrap_love_dll_graphics_newMesh_custom(strListPtr, typeList, comCountList, strListPtr.Length, 
                    true, data, data.Length, (int)drawMode, (int)usage, out meshPtr);
            });
            return LoveObject.NewObject<Mesh>(meshPtr);
        }

        /// <summary>
        /// Each vertex attribute component is initialized to 0 if its data type is "float", or 1 if its data type is "byte". Mesh:setVertices or Mesh:setVertex and Mesh:setDrawRange can be used to specify vertex information once the Mesh is created.
        /// <para> If the data type of an attribute is "float", components can be in the range 1 to 4, if the data type is "byte" it must be 4. </para>
        /// <para> If a custom vertex attribute uses the name "VertexPosition", "VertexTexCoord", or "VertexColor", then the vertex data for that vertex attribute will be used for the standard vertex positions, texture coordinates, or vertex colors respectively, when drawing the Mesh.Otherwise a Vertex Shader is required in order to make use of the vertex attribute when the Mesh is drawn. </para>
        /// </summary>
        public static Mesh NewMesh(MeshFormatDescribe desc, int count, MeshDrawMode drawMode = MeshDrawMode.Fan, SpriteBatchUsage usage = SpriteBatchUsage.Dynamic)
        {
            IEnumerable<MeshFormatDescribe.Entry> formatList = desc.entry;
            string[] strList = formatList.Select(item => item.name).ToArray();
            int[] typeList = formatList.Select(item => (int)item.type).ToArray();
            int[] comCountList = formatList.Select(item => item.componentCount).ToArray();

            IntPtr meshPtr = IntPtr.Zero;
            DllTool.ExecuteNullTailStringArray(strList, (strListPtr) =>
            {
                Love2dDll.wrap_love_dll_graphics_newMesh_custom(strListPtr, typeList, comCountList, strListPtr.Length,
                    false, null, count, (int)drawMode, (int)usage, out meshPtr);
            });
            return LoveObject.NewObject<Mesh>(meshPtr);
        }

        public static void Points(ColoredPoint[] coloredPoints)
        {
            Vector2[] coords = new Vector2[coloredPoints.Length];
            Vector4[] colors = new Vector4[coloredPoints.Length];
            for (int i = 0; i< coloredPoints.Length; i++)
            {
                coords[i] = coloredPoints[i].pos;
                colors[i] = coloredPoints[i].color;
            }
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
        /// <param name="text">A text string.</param>
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
        public static void Printf(string text, float x, float y, float wrap, AlignMode align_type = AlignMode.Left, float angle = 0, float sx = 1, float sy = 1, float ox = 0, float oy = 0, float kx = 0, float ky = 0)
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
        public static Size GetDimensions()
        {
            return new Size(GetWidth(), GetHeight());
        }

        /// <summary>
        /// Draws lines between points.
        /// </summary>
        /// <param name="points">must be an integer multiple of 2. [first(x, y), second(x, y) ....]</param>
        public static void Line(params float[] points)
        {
            Line(Vector2.Array(points));
        }

        /// <summary>
        /// Draws one or more points.
        /// </summary>
        /// <param name="points">must be an integer multiple of 2. [first(x, y), second(x, y) ....]</param>
        public static void Points(params float[] points)
        {
            Points(Vector2.Array(points));
        }

        /// <summary>
        /// Draw a polygon.
        /// </summary>
        /// <param name="mode">How to draw the polygon.</param>
        /// <param name="points">must be an integer multiple of 2. [first(x, y), second(x, y) ....]</param>
        public static void Polygon(DrawMode mode, params float[] points)
        {
            Polygon(mode, Vector2.Array(points));
        }

        /// <summary>
        /// <para>以二维方式缩放坐标系。</para>
        /// <para>默认情况下，LÖVE中的坐标系在水平和垂直方向上一对一显示像素，x轴向右增加，y轴向下增加。 缩放坐标系会改变这种关系。</para>
        /// <para>在通过sx和sy进行缩放之后，所有坐标都被视为与sx和sy相乘。 绘图操作的每个结果也相应地缩放，例如按（2,2）缩放将意味着在x和y方向上使所有内容都变为两倍。 按负值缩放会使坐标系在相应的方向上翻转，所有内容都会被翻转或颠倒（或两者兼而有之）。 按零缩放没有意义。</para>
        /// <para>缩放(Scale)操作和平移(Translate)操作不是可交换操作，以不同的顺序调用它们会产生不同的结果。</para>
        /// <para>效果持续到 Scene.Draw 调用结束（每一帧画面绘制结束都会自动重置为1倍。）</para>
        /// 
        /// <para>Scales the coordinate system in two dimensions.</para> 
        /// <para>By default the coordinate system in LÖVE corresponds to the display pixels in horizontal and vertical directions one-to-one, and the x-axis increases towards the right while the y-axis increases downwards. Scaling the coordinate system changes this relation.</para>
        /// <para>After scaling by sx and sy, all coordinates are treated as if they were multiplied by sx and sy. Every result of a drawing operation is also correspondingly scaled, so scaling by (2, 2) for example would mean making everything twice as large in both x- and y-directions. Scaling by a negative value flips the coordinate system in the corresponding direction, which also means everything will be drawn flipped or upside down, or both. Scaling by zero is not a useful operation.</para>
        /// <para>Scale and translate are not commutative operations, therefore, calling them in different orders will change the outcome.</para>
        /// <para>Scaling lasts until Scene.Draw exits.</para>
        /// </summary>
        /// <param name="scale">The scaling on each axis.</param>
        public static void Scale(Vector2 scale)
        {
            Scale(scale.X, scale.Y);
        }


        /// <summary>
        /// <para>以二维方式缩放坐标系。</para>
        /// <para>默认情况下，LÖVE中的坐标系在水平和垂直方向上一对一显示像素，x轴向右增加，y轴向下增加。 缩放坐标系会改变这种关系。</para>
        /// <para>在通过sx和sy进行缩放之后，所有坐标都被视为与sx和sy相乘。 绘图操作的每个结果也相应地缩放，例如按（2,2）缩放将意味着在x和y方向上使所有内容都变为两倍。 按负值缩放会使坐标系在相应的方向上翻转，所有内容都会被翻转或颠倒（或两者兼而有之）。 按零缩放没有意义。</para>
        /// <para>缩放(Scale)操作和平移(Translate)操作不是可交换操作，以不同的顺序调用它们会产生不同的结果。</para>
        /// <para>效果持续到 Scene.Draw 调用结束（每一帧画面绘制结束都会自动重置为1倍。）</para>
        /// 
        /// <para>Scales the coordinate system in two dimensions.</para> 
        /// <para>By default the coordinate system in LÖVE corresponds to the display pixels in horizontal and vertical directions one-to-one, and the x-axis increases towards the right while the y-axis increases downwards. Scaling the coordinate system changes this relation.</para>
        /// <para>After scaling by sx and sy, all coordinates are treated as if they were multiplied by sx and sy. Every result of a drawing operation is also correspondingly scaled, so scaling by (2, 2) for example would mean making everything twice as large in both x- and y-directions. Scaling by a negative value flips the coordinate system in the corresponding direction, which also means everything will be drawn flipped or upside down, or both. Scaling by zero is not a useful operation.</para>
        /// <para>Scale and translate are not commutative operations, therefore, calling them in different orders will change the outcome.</para>
        /// <para>Scaling lasts until Scene.Draw exits.</para>
        /// </summary>
        /// <param name="scale">The scaling on each axis.</param>
        public static void Scale(float scale)
        {
            Scale(scale, scale);
        }






        /// <summary>
        /// Captures drawing operations to a Canvas
        /// <para>Sets the render target to a specified Canvas. All drawing operations until the next love.graphics.setCanvas call will be redirected to the Canvas and not shown on the screen.</para>
        /// <para>if Length of canvas is zero, then resets the render target to the screen, i.e. re-enables drawing to the screen.</para>
        /// </summary>
        /// <param name="canvas"></param>
        public static void SetCanvas(RenderTargetInfo renderTargetInfo)
        {

            if (renderTargetInfo.RenderTargetList.Count == 0 && renderTargetInfo.DepthStencil == null)
            {
                SetCanvas();
                return;
            }

            int tcount = renderTargetInfo.RenderTargetList.Count + 1;
            IntPtr[] canvaList = new IntPtr[tcount];
            int[] sliceList = new int[tcount];
            int[] mipmapList = new int[tcount];

            if (renderTargetInfo.DepthStencil != null && renderTargetInfo.DepthStencil.canvas != null)
            {
                canvaList[0] = renderTargetInfo.DepthStencil.canvas.p;
                sliceList[0] = renderTargetInfo.DepthStencil.slice;
                mipmapList[0] = renderTargetInfo.DepthStencil.mipmap;
            }

            for (int i = 1; i < tcount; i++)
            {
                var rt = renderTargetInfo.RenderTargetList[i - 1];
                canvaList[i] = rt.canvas.p;
                sliceList[i] = rt.slice;
                mipmapList[i] = rt.mipmap;
            }
            Love2dDll.wrap_love_dll_graphics_setCanvasRenderTagets(canvaList, sliceList, mipmapList, canvaList.Length, renderTargetInfo.tempDepthFlag, renderTargetInfo.tempStencilFlag);
        }


        /// <summary>
        /// Returns the current target Canvas. Returns zero length array if drawing to the real screen.
        /// </summary>
        public static RenderTargetInfo GetCanvas()
        {
            Love2dDll.wrap_love_dll_graphics_getCanvasTagets(out var canvasPtr, out var sliceListPtr, out var mipmapListPtr, out int targetCount);
            var canvasList = DllTool.ReadIntPtrsWithConvertAndRelease<Canvas>(canvasPtr, targetCount);
            var sliceList = DllTool.ReadInt32sAndRelease(sliceListPtr, targetCount);
            var mipmapList = DllTool.ReadInt32sAndRelease(mipmapListPtr, targetCount);

            List<RenderTarget> list = new List<RenderTarget>(targetCount);
            RenderTarget depthStencil = null;
            for (int i = 0; i < targetCount; i++)
            {
                var rt = RenderTarget.FromCanvas(canvasList[i], sliceList[i], mipmapList[i]);
                if (i == 0)
                    depthStencil = rt;
                else
                    list.Add(rt);
            }

            return new RenderTargetInfo(list, depthStencil);
        }



        /// <summary>
        /// Captures drawing operations to a Canvas
        /// <para>Sets the render target to a specified Canvas. All drawing operations until the next love.graphics.setCanvas call will be redirected to the Canvas and not shown on the screen.</para>
        /// <para>if Length of canvas is zero, then resets the render target to the screen, i.e. re-enables drawing to the screen.</para>
        /// </summary>
        /// <param name="canvas"></param>
        public static void SetCanvas(params Canvas[] canvas)
        {
            if (canvas == null)
            {
                canvas = new Canvas[0];
            }

            if (canvas.Length == 0)
            {
                Love2dDll.wrap_love_dll_graphics_setCanvasEmpty();
                return;
            }

            SetCanvas(RenderTargetInfo.CreateWithDepthStencil(null, canvas));
        }

        //public static Canvas[] GetCanvas()
        //{
        //    IntPtr out_canvaList = IntPtr.Zero;
        //    int out_canvaList_length = 0;
        //    Love2dDll.wrap_love_dll_graphics_getCanvas(out out_canvaList, out out_canvaList_length);
        //    var buffer = DllTool.ReadIntPtrsAndRelease(out_canvaList, out_canvaList_length);

        //    Canvas[] canvas = new Canvas[buffer.Length];
        //    for (int i = 0; i < buffer.Length; i++)
        //    {
        //        canvas[i] = LoveObject.NewObject<Canvas>(buffer[i]);
        //    }

        //    return canvas;
        //}
    }
}
