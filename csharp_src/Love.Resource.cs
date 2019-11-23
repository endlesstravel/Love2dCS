using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using SFile = System.IO.File;
using SFileMode = System.IO.FileMode;
using SFileInfo = System.IO.FileInfo;
using SIO = System.IO;
using System.Collections.Generic;

namespace Love
{
    /// <summary>
    /// This module will create resource through starndard C# IO,
    /// this means you can read a png file from path like C:/love-logo.png
    /// </summary>
    public class Resource
    {
        #region Persistence
        /// <summary>
        /// Save data to file, object will serialize as binary file.
        /// </summary>
        /// <param name="path">path to save</param>
        /// <param name="obj">object to save</param>
        public static void SaveData(string path, object obj)
        {
            MemoryStream memorystream = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(memorystream, obj);
            Resource.Write(path, memorystream.ToArray());
        }

        /// <summary>
        /// Load data from a file as specify type.
        /// </summary>
        /// <typeparam name="T">the data type you want convert</typeparam>
        /// <param name="path">the path you want to load</param>
        /// <returns></returns>
        public static T LoadData<T>(string path)
        {
            MemoryStream memorystreamd = new MemoryStream(Resource.Read(path));
            BinaryFormatter bfd = new BinaryFormatter();
            return (T)bfd.Deserialize(memorystreamd);
        }
        #endregion


        #region Audio
        /// <summary>
        /// Creates a new Source from file name.
        /// </summary>
        /// <param name="fileName">The filepath to the audio file.</param>
        /// <param name="type">Streaming or static source.</param>
        /// <returns></returns>
        public static Source NewSource(string fileName, SourceType type)
        {
            var fileData = NewFileData(fileName);
            return Audio.NewSource(fileData, type);
        }

        /// <summary>
        /// Creates a new Source from file name.
        /// </summary>
        /// <param name="type">Streaming or static source.</param>
        /// <returns></returns>
        public static Source NewSource(FileData fdata, SourceType type)
            => Audio.NewSource(fdata, type);

        public static Source NewSource(SoundData sd)
            => Audio.NewSource(sd);

        /// <summary>
        /// Creates a new Source from file name.
        /// </summary>
        /// <param name="type">Streaming or static source.</param>
        /// <returns></returns>
        public static Source NewSource(Decoder decoder, SourceType type)
            => Audio.NewSource(decoder, type);
        #endregion

        #region FileSystem

        /// <summary>
        /// Creates a new FileData object. This function will read file from standard C# IO File system.
        /// </summary>
        /// <param name="fileName">file name, such as C:/love-logo.png </param>
        /// <param name="contents">The contents of the file.</param>
        /// <returns></returns>
        public static FileData NewFileData(byte[] contents, string fileName)
            => FileSystem.NewFileData(contents, fileName);

        /// <summary>
        /// Creates a new FileData object. This function will read file from standard C# IO File system.
        /// </summary>
        /// <param name="fileName">file name, such as C:/love-logo.png </param>
        /// <returns></returns>
        public static FileData NewFileData(string fileName)
        {
            return FileSystem.NewFileData(SFile.ReadAllBytes(fileName), $"NewFileData:path:{fileName}");
        }

        /// <summary>
        /// Append data to an existing file.
        /// </summary>
        /// <param name="path">The name (and path) of the file.</param>
        /// <param name="byteArray">The data to append to the file.</param>
        public static void Append(string path, byte[] byteArray)
        {
            using (var fs = new FileStream(path, SFileMode.Append, FileAccess.Write))
            {
                fs.Write(byteArray, 0, byteArray.Length);
            }
        }

        /// <summary>
        /// Append data to an existing file.
        /// </summary>
        /// <param name="path">The name (and path) of the file.</param>
        /// <param name="content">The string to append to the file.</param>
        public static void Append(string path, string content)
        {
            SFile.AppendAllText(path, content);
        }

        /// <summary>
        /// Append data to an existing file.
        /// </summary>
        /// <param name="path">The name (and path) of the file.</param>
        /// <param name="content">The string to append to the file.</param>
        /// <param name="encoding">string encoding.</param>
        public static void Append(string path, string content, Encoding encoding)
        {
            SFile.AppendAllText(path, content, encoding);
        }

        /// <summary>
        /// <para>Recursively creates a directory.</para>
        /// <para>When called with "a/b" it creates both "a" and "a/b", if they don't exist already.</para>
        /// </summary>
        /// <param name="pathString">The directory to create.</param>
        /// <returns>True if the directory was created, false if not.</returns>
        public static bool CreateDirectory(string pathString)
        {
            try
            {
                Directory.CreateDirectory(pathString);
                return true;
            }
            catch (Exception e)
            {
                Log.Error($"Create Directory failed {pathString} : {e.Message}");
            }
            return false;
        }

        /// <summary>
        /// <para>Returns a table with the names of files and subdirectories in the specified path. The array is not sorted in any way; the order is undefined.</para>
        /// <para>If the path passed to the function exists in the game and the save directory, it will list the files and directories from both places.</para>
        /// </summary>
        /// <param name="path">The directory.</param>
        /// <returns></returns>
        public static string[] GetDirectoryItems(string path)
        {
            return Directory.GetFileSystemEntries(path, "*", SearchOption.TopDirectoryOnly);
        }

        /// <summary>
        /// Gets information about the specified file or directory.
        /// </summary>
        /// <param name="path">The file or directory path to check.</param>
        /// <returns></returns>
        public static FileInfo GetInfo(string path)
        {
            var sFileInfo = new SFileInfo(path);
            if (sFileInfo.Exists == false)
                return null;

            // get the file attributes for file or directory
            FileAttributes attr = sFileInfo.Attributes;
            //detect whether its a directory or file
            bool isDirectory = ((attr & FileAttributes.Directory) == FileAttributes.Directory);
            var type = isDirectory ? FileType.Directory : FileType.File;
            return new FileInfo(sFileInfo.Length, (long)FileInfo.ConvertToUnixTimestamp(sFileInfo.LastAccessTime), type);
        }

        /// <summary>
        /// Iterate over the lines in a file.
        /// </summary>
        /// <param name="lineFunction"></param>
        /// <param name="path"></param>
        public static void Lines(Action<string> lineFunction, string path)
        {
            Check.ArgumentNull(lineFunction, "lineFunction");

            string line;
            StreamReader file = new StreamReader(path);
            while ((line = file.ReadLine()) != null)
            {
                lineFunction(line);
            }
        }

        /// <summary>
        /// Read the all contents of a file.
        /// </summary>
        /// <param name="filename">The name (and path) of the file.</param>
        /// <returns></returns>
        public static byte[] Read(string path)
        {
            return SFile.ReadAllBytes(path);
        }

        /// <summary>
        /// Read the all contents of a file.
        /// </summary>
        /// <param name="filename">The name (and path) of the file.</param>
        /// <param name="len">How many bytes to read.</param>
        /// <returns></returns>
        public static byte[] Read(string path, int len)
        {
            return new BinaryReader(new FileStream(path, SFileMode.Open, FileAccess.Read)).ReadBytes(len);
        }

        /// <summary>
        /// Removes a file (or directory).
        /// </summary>
        /// <param name="path">The file or directory to remove.</param>
        public static bool Remove(string path)
        {
            try
            {
                //detect whether its a directory or file
                if ((SFile.GetAttributes(path) & FileAttributes.Directory) == FileAttributes.Directory)
                    Directory.Delete(path);
                else
                    SFile.Delete(path);

                return true;
            }
            catch(Exception e)
            {
                Log.Error($"Remove path failed {path} : {e.Message}");
            }
            return false;
        }


        /// <summary>
        /// Write data to a file in the save directory. If the file existed already, it will be completely replaced by the new contents.
        /// </summary>
        /// <param name="path">The name (and path) of the file.</param>
        /// <param name="bytes">The data to write to the file.</param>
        public static void Write(string path, byte[] bytes)
        {
            SFile.WriteAllBytes(path, bytes);
        }

        /// <summary>
        /// Write data to a file in the save directory. If the file existed already, it will be completely replaced by the new contents.
        /// </summary>
        /// <param name="filename">The name (and path) of the file.</param>
        /// <param name="contents">The string data to write to the file.</param>
        public static void Write(string path, string contents)
        {
            SFile.WriteAllText(path, contents);
        }

        /// <summary>
        /// Write data to a file in the save directory. If the file existed already, it will be completely replaced by the new contents.
        /// </summary>
        /// <param name="filename">The name (and path) of the file.</param>
        /// <param name="contents">The string data to write to the file.</param>
        /// <param name="encoding">string encodeing</param>
        public static void Write(string path, string contents, Encoding encoding)
        {
            SFile.WriteAllText(path, contents, encoding);
        }
        #endregion

        #region Font
        /// <summary>
        /// Creates a new Rasterizer.
        /// </summary>
        /// <param name="fileName">The font file.</param>
        /// <returns>The rasterizer.</returns>
        public static Rasterizer NewRasterizer(string fileName)
        {
            return Font.NewRasterizer(Resource.NewFileData(fileName));
        }
        public static Rasterizer NewBMFontRasterizer(FileData fileData, params ImageData[] imageDatas)
            => Font.NewBMFontRasterizer(fileData, imageDatas);
        public static GlyphData NewGlyphData(Rasterizer rasterizer, byte[] glyph)
            => Font.NewGlyphData(rasterizer, glyph);
        public static GlyphData NewGlyphData(Rasterizer rasterizer, int glyphCode)
            => Font.NewGlyphData(rasterizer, glyphCode);
        public static Rasterizer NewImageRasterizer(ImageData imageData, byte[] glyphs, int extraspacing)
            => Font.NewImageRasterizer(imageData, glyphs, extraspacing);
        public static Rasterizer NewRasterizer(FileData fileData)
            => Font.NewRasterizer(fileData);
        public static Rasterizer NewTrueTypeRasterizer(Data data, int size, HintingMode hinting = HintingMode.Normal)
            => Font.NewTrueTypeRasterizer(data, size, hinting);
        public static Rasterizer NewTrueTypeRasterizer(int size, HintingMode hinting = HintingMode.Normal)
            => Font.NewTrueTypeRasterizer(size, hinting);
        #endregion

        #region Graphics
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
                imageData[i] = Resource.NewImageData(imageFileName[i]);
            }
            var filedata = Resource.NewFileData(filename);
            var rasterizerImage = Font.NewBMFontRasterizer(filedata, imageData);
            return Graphics.NewFont(rasterizerImage);
        }

        /// <summary>
        /// </summary>
        /// <param name="filename">The filepath to the BMFont file.</param>
        /// <returns></returns>
        public static Font NewBMFont(string filename)
            => Resource.NewBMFont(filename, new string[0]);


        /// <summary>
        /// Creates a new Canvas.
        /// <para>This function can be slow if it is called repeatedly, such as from love.update or love.draw. If you need to use a specific resource often, create it once and store it somewhere it can be reused!</para>
        /// </summary>
        /// <returns>A new Canvas with dimensions equal to the window's size in pixels.</returns>
        public static Canvas NewCanvas()
            => Graphics.NewCanvas();

        /// <summary>
        /// Creates a new Canvas.
        /// <para>This function can be slow if it is called repeatedly, such as from love.update or love.draw. If you need to use a specific resource often, create it once and store it somewhere it can be reused!</para>
        /// </summary>
        /// <param name="width">The desired width of the Canvas.</param>
        /// <param name="height">The desired height of the Canvas.</param>
        /// <returns>A new Canvas with specified width and height.</returns>
        public static Canvas NewCanvas(int width, int height, Graphics.Settings settings = null)
            => Graphics.NewCanvas(width, height, settings);

        /// <summary>
        /// Create a new TrueType font.
        /// </summary>
        /// <param name="filename">The filepath to the TrueType font file.</param>
        /// <param name="size">The size of the font in pixels.</param>
        /// <param name="hinting">True Type hinting mode.</param>
        /// <returns>A Font object which can be used to draw text on screen.</returns>
        public static Font NewFont(string filename, int size = 12, HintingMode hinting = HintingMode.Normal)
        {
            var fileData = Resource.NewFileData(filename);
            var rasterizer = Font.NewTrueTypeRasterizer(fileData, size, hinting);
            return Graphics.NewFont(rasterizer);
        }

        public static Font NewFont(Rasterizer rasterizer)
            => Graphics.NewFont(rasterizer);

        /// <summary>
        /// Create a new instance of the default font (Vera Sans) with a custom size.
        /// </summary>
        /// <param name="size">The size of the font in pixels.</param>
        /// <param name="hinting">True Type hinting mode.</param>
        /// <returns></returns>
        public static Font NewFont(int size, HintingMode hinting = HintingMode.Normal)
            => Graphics.NewFont(size, hinting);

        public static Image NewImage(ImageData imageData, bool flagMipmaps = false, bool flagLinear = false)
            => Graphics.NewImage(imageData, flagMipmaps, flagLinear);

        public static Image NewImage(ImageDataBase[] imageData, bool flagMipmaps = false, bool flagLinear = false)
            => Graphics.NewImage(imageData, flagMipmaps, flagLinear);

        /// <summary>
        /// Creates a new Image from a filepath.
        /// </summary>
        /// <param name="filename">The filepath to the image file .</param>
        /// <param name="flagMipmaps">If true, mipmaps for the image will be automatically generated (or taken from the images's file if possible, if the image originated from a CompressedImageData). If this value is a table, it should contain a list of other filenames of images of the same format that have progressively half-sized dimensions, all the way down to 1x1. Those images will be used as this Image's mipmap levels.</param>
        /// <param name="flagLinear">True if the image's pixels should be interpreted as being linear RGB rather than sRGB-encoded, if gamma-correct rendering is enabled. Has no effect otherwise.</param>
        /// <returns></returns>
        public static Image NewImage(string filename, bool flagMipmaps = false, bool flagLinear = false)
        {
            var imgData = Resource.NewImageData(filename);
            return Graphics.NewImage(imgData, flagMipmaps, flagLinear);
        }

        public static Image NewImage(CompressedImageData compressedImageData, bool flagMipmaps = false, bool flagLinear = false)
            => Graphics.NewImage(compressedImageData, flagMipmaps, flagLinear);

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
            var imageData = Resource.NewImageData(filename);
            var glyphsBytes = DllTool.GetNullTailUTF8Bytes(glyphs);
            var rasterizerImage = Font.NewImageRasterizer(imageData, glyphsBytes, extraspacing);
            return Graphics.NewFont(rasterizerImage);
        }

        public static Mesh NewMesh(int count, MeshDrawMode drawMode, SpriteBatchUsage usage) 
            => Graphics.NewMesh(count, drawMode, usage);
        public static Mesh NewMesh(IEnumerable<MeshAttribFormat> formatList, int count, MeshDrawMode drawMode = MeshDrawMode.Fan, SpriteBatchUsage usage = SpriteBatchUsage.Dynamic)
            => Graphics.NewMesh(formatList, count, drawMode, usage);
        public static Mesh NewMesh(IEnumerable<MeshAttribFormat> formatList, byte[] data, MeshDrawMode drawMode = MeshDrawMode.Fan, SpriteBatchUsage usage = SpriteBatchUsage.Dynamic)
            => Graphics.NewMesh(formatList, data, drawMode, usage);
        public static ParticleSystem NewParticleSystem(Texture texture, int buffer = 1000)
            => Graphics.NewParticleSystem(texture, buffer);
        public static Quad NewQuad(double x, double y, double w, double h, double sw, double sh)
            => Graphics.NewQuad(x, y, w, h, sw, sh);
        public static Shader NewShader(string codeStr)
            => Graphics.NewShader(codeStr);
        public static Shader NewShader(string vertexCodeStr, string pixelCodeStr)
            => Graphics.NewShader(vertexCodeStr, pixelCodeStr);
        public static SpriteBatch NewSpriteBatch(Texture texture, int maxSprites, SpriteBatchUsage usage_type)
            => Graphics.NewSpriteBatch(texture, maxSprites, usage_type);
        public static Text NewText(Font font, ColoredStringArray coloredStr)
            => Graphics.NewText(font, coloredStr);
        public static Text NewText(Font font, string coloredStr)
            => Graphics.NewText(font, coloredStr);

        ///// <summary>
        ///// Creates a new drawable Video. Currently only Ogg Theora video files are supported.
        ///// </summary>
        ///// <param name="filename">The file path to the Ogg Theora video file.</param>
        ///// <param name="audio">Whether to try to load the video's audio into an audio Source. If not explicitly set to true or false, it will try without causing an error if the video has no audio.</param>
        ///// <param name="dipScale">The DPI scale factor of the video. if it was null, value will be Graphics.GetDPIScale()</param>
        ///// <returns></returns>
        //Video NewVideo(string filename, bool audio = true, float? dipScale = null)
        //{
        //    return NewVideo(Video.NewVideoStream(filename), audio, dipScale);
        //}

        #endregion

        #region Image
        /// <summary>
        /// Creates a new <see cref="ImageData"/> object.
        /// </summary>
        /// <param name="filename">The filename of the image file.</param>
        /// <returns>The new ImageData object.</returns>
        public static ImageData NewImageData(string filename)
        {
            return Image.NewImageData(Resource.NewFileData(filename));
        }

        /// <summary>
        /// Create a new <see cref="CompressedImageData"/> object from a compressed image file. LÖVE supports several compressed texture formats, enumerated in the <see cref="PixelFormat"/> page.
        /// </summary>
        /// <param name="filename">The filename of the compressed image file.</param>
        /// <returns>The new CompressedImageData object.</returns>
        public static CompressedImageData NewCompressedData(string filename)
        {
            return Image.NewCompressedData(Resource.NewFileData(filename));
        }

        public static CompressedImageData NewCompressedData(FileData data)
            => Image.NewCompressedData(data);

        /// <summary>
        /// Encodes the ImageData and writes it to the path.
        /// </summary>
        /// <param name="path">The filename to write the file to.</param>
        /// <param name="imageData">The imageData to write the file to. </param>
        /// <param name="format">The format to encode the image as.</param>
        /// <returns></returns>
        public static void EncodeToFile(string path, ImageData imageData, ImageFormat format)
        {
            Check.ArgumentNull(path, "path");
            Check.ArgumentNull(imageData, "imageData");

            var fileData = imageData.Encode(format);
            SFile.WriteAllBytes(path, fileData.GetBytes());
        }

        /// <summary>
        /// Creates a new ImageData object.
        /// <para> Vector4[x, y] - new Vector4(0.1f, 0.2f, 0.3f, 0.4f) </para>
        /// </summary>
        /// <param name="rawData">color data to set</param>
        /// <param name="format">The pixel format of the ImageData.</param>
        /// <returns></returns>
        public static ImageData NewImageData(Vector4[,] rawData, ImageDataPixelFormat format)
            => Image.NewImageData(rawData, format);

        /// <summary>
        /// Creates a new ImageData object.
        /// </summary>
        /// <param name="rawData">Optional raw byte data to load into the ImageData, in the format specified by format.</param>
        /// <param name="format">The pixel format of the ImageData.</param>
        /// <returns></returns>
        public static ImageData NewImageData(Vector4[][] rawData, ImageDataPixelFormat format)
            => Image.NewImageData(rawData, format);

        /// <summary>
        /// Creates a new <see cref="ImageData"/> object.
        /// </summary>
        /// <param name="w">The width of the ImageData.</param>
        /// <param name="h">The height of the ImageData.</param>
        /// <param name="format">The pixel format of the ImageData.</param>
        /// <param name="data">Optional raw byte data to load into the ImageData, in the format specified by format.</param>
        /// <returns></returns>
        public static ImageData NewImageData(uint w, uint h, ImageDataPixelFormat format = ImageDataPixelFormat.RGBA8, byte[] data = null)
            => Image.NewImageData(w, h, format, data);

        /// <summary>
        /// Creates a new <see cref="ImageData"/> object.
        /// </summary>
        /// <param name="w">The width of the ImageData.</param>
        /// <param name="h">The height of the ImageData.</param>
        /// <param name="format">The pixel format of the ImageData.</param>
        /// <param name="data">Optional raw byte data to load into the ImageData, in the format specified by format.</param>
        /// <returns></returns>
        public static ImageData NewImageData(int w, int h, ImageDataPixelFormat format = ImageDataPixelFormat.RGBA8, byte[] data = null)
            => Image.NewImageData(w, h, format, data);

        /// <summary>
        /// Creates a new <see cref="ImageData"/> object.
        /// </summary>
        /// <param name="data">The encoded file data to decode into image data.</param>
        /// <returns></returns>
        public static ImageData NewImageData(FileData data)
            => Image.NewImageData(data);

        #endregion

        #region Mouse
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
            return Mouse.NewCursor(Resource.NewImageData(filename), hotX, hotY);
        }

        /// <summary>
        /// <para>Creates a new hardware Cursor object from an image file or ImageData.</para>
        /// <para>Hardware cursors are framerate-independent and work the same way as normal operating system cursors. Unlike drawing an image at the mouse's current coordinates, hardware cursors never have visible lag between when the mouse is moved and when the cursor position updates, even at low framerates.</para>
        /// <para>The hot spot is the point the operating system uses to determine what was clicked and at what position the mouse cursor is. For example, the normal arrow pointer normally has its hot spot at the top left of the image, but a crosshair cursor might have it in the middle.</para>
        /// </summary>
        /// <param name="imageData">The ImageData to use for the new Cursor.</param>
        /// <param name="hotX">The x-coordinate in the image of the cursor's hot spot.</param>
        /// <param name="hotY">The y-coordinate in the image of the cursor's hot spot.</param>
        /// <returns></returns>
        public static Cursor NewCursor(ImageData imageData, int hotX, int hotY)
            => Mouse.NewCursor(imageData, hotX, hotY);
        #endregion

        #region Sound
        /// <summary>
        /// Attempts to find a decoder for the encoded sound data in the specified file.
        /// </summary>
        /// <param name="filename">The filename of the file with encoded sound data.</param>
        /// <param name="bufferSize">The size of each decoded chunk, in bytes.</param>
        /// <returns></returns>
        public static Decoder NewDecoder(string filename, int bufferSize = Decoder.DEFAULT_BUFFER_SIZE)
        {
            return Sound.NewDecoder(Resource.NewFileData(filename), bufferSize);
        }

        /// <summary>
        /// Attempts to find a decoder for the encoded sound data in the specified file.
        /// </summary>
        /// <param name="fdata">The file data with encoded sound data.</param>
        /// <param name="buffersize">The size of each decoded chunk, in bytes.</param>
        /// <returns></returns>
        public static Decoder NewDecoder(FileData fdata, int buffersize = 16384)
            => Sound.NewDecoder(fdata, buffersize);

        /// <summary>
        /// <para> Creates a new SoundData.</para>
        /// <para>It's also possible to create SoundData with a custom sample rate, channel and bit depth.</para>
        /// <para>The sound data will be decoded to the memory in a raw format. It is recommended to create only short sounds like effects, as a 3 minute song uses 30 MB of memory this way.</para>
        /// </summary>
        /// <param name="filename">The file name of the file to load.</param>
        /// <returns>A new SoundData object.</returns>
        public static SoundData NewSoundData(string filename)
        {
            return Sound.NewSoundData(Resource.NewDecoder(filename));
        }

        /// <summary>
        /// <para>Creates a new SoundData.</para>
        /// <para>It's also possible to create SoundData with a custom sample rate, channel and bit depth.</para>
        /// <para>The sound data will be decoded to the memory in a raw format. It is recommended to create only short sounds like effects, as a 3 minute song uses 30 MB of memory this way.</para>
        /// </summary>
        /// <param name="decoder">Decode data from this Decoder until EOF.</param>
        /// <returns>A new SoundData object.</returns>
        public static SoundData NewSoundData(Decoder decoder)
            => Sound.NewSoundData(decoder);

        /// <summary>
        /// <para>Creates a new SoundData.</para>
        /// <para>It's also possible to create SoundData with a custom sample rate, channel and bit depth.</para>
        /// <para>The sound data will be decoded to the memory in a raw format. It is recommended to create only short sounds like effects, as a 3 minute song uses 30 MB of memory this way.</para>
        /// </summary>
        /// <param name="samples">Total number of samples.</param>
        /// <param name="sampleRate">Number of samples per second</param>
        /// <param name="bits">Bits per sample (8 or 16).</param>
        /// <param name="channels">Either 1 for mono or 2 for stereo.</param>
        /// <returns>A new SoundData object.</returns>
        public static SoundData NewSoundData(int samples, int sampleRate = 44100, int bits = 16, int channels = 2)
            => Sound.NewSoundData(sampleRate, sampleRate, bits, channels);

        #endregion

        #region Video
        ///// <summary>
        ///// Creates a new VideoStream. Currently only Ogg Theora video files are supported. VideoStreams can't draw videos, see love.graphics.newVideo for that.
        ///// </summary>
        ///// <param name="filename">The file path to the Ogg Theora video file.</param>
        ///// <returns>A new VideoStream.</returns>
        //public static VideoStream NewVideoStream(string filename)
        //{
        //    var file = Persistence.NewFile(filename);
        //    return Video.NewVideoStream(file);
        //}
        #endregion

    }
}
