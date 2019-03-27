// Author : endlesstravel
// this part make interface with love object
// REMEMBER that :
// ** part of C resonse for retain **
// ** part of C# response for release **

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Love
{
    /// <summary>
    /// LÖVE 引擎对象
    /// </summary>
    public abstract partial class LoveObject : IDisposable
    {
        // use factory design pattern
        internal static T NewObject<T>(IntPtr ip) where T : LoveObject
        {
            if (IntPtr.Zero == ip)
            {
                return null;
            }

            var obj = (T)Activator.CreateInstance(typeof(T), BindingFlags.Instance | BindingFlags.NonPublic, null, null, null);
            obj.p = ip;

            // part of C resonse for retain
            // part of C# response for release
            // Love2dDll.wrap_love_dll_retain_obj(ip);

            return obj;
        }

        /// <summary>
        /// danger !!!!!
        /// </summary>
        /// <param name="p"></param>
        internal static void RetainLoveObject(IntPtr p)
        {
            Love2dDll.wrap_love_dll_retain_obj(p);
        }

        // real pointer
        internal IntPtr p;

        public static IntPtr AcquirePointer(LoveObject loveObject)
        {
            return loveObject == null ? IntPtr.Zero : loveObject.p;
        }

        // disable no-param construct
        internal LoveObject() { }

        // part of C resonse for retain
        // part of C# response for release
        public void Dispose()
        {
            Console.WriteLine("release : " + GetType());
            Love2dDll.wrap_love_dll_release_obj(p);
            p = IntPtr.Zero;
        }

        // if we release resource in ~LoveObject() we may encounter crash
        // when we exit program. I think the reason is that C# disorder the
        // time of release chance between openGL and opengGL resource (for example Texture)

        /// <summary>
        /// 如果两个 LoveObject 指向的非托管对象一样，那么则返回相等
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            // 检查 null 值并且比较运行时类型。
            if (obj == null || GetType() != obj.GetType())
                return false;

            return this.p == (obj as LoveObject).p;
        }

        //public static bool operator ==(LoveObject lhs, LoveObject rhs)
        //{
        //    if (lhs == null && rhs == null)
        //        return true;

        //    if (lhs == null || rhs == null)
        //        return false;

        //    return lhs.p == rhs.p;
        //}

        //public static bool operator !=(LoveObject lhs, LoveObject rhs)
        //{
        //    if (lhs == null && rhs == null)
        //        return false;
        //    if (lhs == null || rhs == null)
        //        return true;
        //    return lhs.p != rhs.p;
        //}


        /// <summary>
        /// 返回此实例的 IntPtr p 的哈希代码。
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return p.GetHashCode();
        }
    }

    /// <summary>
    /// <para>A Source represents audio you can play back. You can do interesting things with Sources, like set the volume, pitch, and its position relative to the listener. Please note that positional audio only works for mono (i.e. non-stereo) sources.</para>
    /// </summary>
    public partial class Source : LoveObject
    {
        /// <summary>
        /// disable no-param construct
        /// </summary>
        protected Source() {}

        /// <summary>
        /// Creates an identical copy of the Source in the stopped state.
        /// <para>Static Sources will use significantly less memory and take much less time to be created if <see cref="Clone"/> is used to create them instead of Audio.NewSource, so this method should be preferred when making multiple Sources which play the same sound.</para>
        /// </summary>
        /// <returns></returns>
        public Source Clone()
        {
            IntPtr out_clone = IntPtr.Zero;
            Love2dDll.wrap_love_dll_type_Source_clone(p, out out_clone);
            return LoveObject.NewObject<Source>(out_clone);
        }
        /// <summary>
        /// Plays a source.
        /// </summary>
        /// <returns>Whether the Source was able to successfully start playing.</returns>
        public bool Play()
        {
            bool out_success = false;
            Love2dDll.wrap_love_dll_type_Source_play(p, out out_success);
            return out_success;
        }

        /// <summary>
        /// Stops a source.
        /// </summary>
        public void Stop()
        {
            Love2dDll.wrap_love_dll_type_Source_stop(p);
        }

        /// <summary>
        /// Pauses the Source.
        /// </summary>
        public void Pause()
        {
            Love2dDll.wrap_love_dll_type_Source_pause(p);
        }

        /// <summary>
        /// Sets the pitch of the Source.
        /// </summary>
        /// <param name="pitch">Calculated with regard to 1 being the base pitch. Each reduction by 50 percent equals a pitch shift of -12 semitones (one octave reduction). Each doubling equals a pitch shift of 12 semitones (one octave increase). Zero is not a legal value.</param>
        public void SetPitch(float pitch)
        {
            Love2dDll.wrap_love_dll_type_Source_setPitch(p, pitch);
        }

        /// <summary>
        /// Gets the current pitch of the Source.
        /// </summary>
        /// <returns>The pitch, where 1.0 is normal.</returns>
        public float GetPitch()
        {
            float out_pitch = 0;
            Love2dDll.wrap_love_dll_type_Source_getPitch(p, out out_pitch);
            return out_pitch;
        }

        /// <summary>
        /// Sets the current volume of the Source.
        /// </summary>
        /// <param name="volume">The volume for a Source, where 1.0 is normal volume. Volume cannot be raised above 1.0.</param>
        public void SetVolume(float volume)
        {
            Love2dDll.wrap_love_dll_type_Source_setVolume(p, volume);
        }

        /// <summary>
        /// Gets the current volume of the Source.
        /// </summary>
        /// <returns>The volume of the Source, where 1.0 is normal volume.</returns>
        public float GetVolume()
        {
            float out_volume = 0;
            Love2dDll.wrap_love_dll_type_Source_getVolume(p, out out_volume);
            return out_volume;
        }

        /// <summary>
        /// Sets the currently playing position of the Source.
        /// </summary>
        /// <param name="offset">The position to seek to.</param>
        /// <param name="unit_type">The unit of the position value.</param>
        public void Seek(float offset, TimeUnit unit_type = TimeUnit.Seconds)
        {
            Love2dDll.wrap_love_dll_type_Source_seek(p, offset, (int)unit_type);
        }

        /// <summary>
        /// Gets the currently playing position of the Source.
        /// </summary>
        /// <param name="unit_type">The type of unit for the return value.</param>
        /// <returns>The currently playing position of the Source.</returns>
        public float Tell(TimeUnit unit_type = TimeUnit.Seconds)
        {
            float out_position = 0;
            Love2dDll.wrap_love_dll_type_Source_tell(p, (int)unit_type, out out_position);
            return out_position;
        }

        /// <summary>
        /// Gets the duration of the Source.
        /// </summary>
        /// <param name="unit_type">The time unit for the return value.</param>
        /// <returns>The duration of the Source, or -1 if it cannot be determined.</returns>
        public float GetDuration(TimeUnit unit_type = TimeUnit.Seconds)
        {
            float out_duration = 0;
            Love2dDll.wrap_love_dll_type_Source_getDuration(p, (int)unit_type, out out_duration);
            return out_duration;
        }

        /// <summary>
        /// Sets the position of the Source.
        /// </summary>
        /// <param name="x">The X position of the Source.</param>
        /// <param name="y">The Y position of the Source.</param>
        /// <param name="z">The Z position of the Source.</param>
        public void SetPosition(float x, float y, float z)
        {
            Love2dDll.wrap_love_dll_type_Source_setPosition(p, x, y, z);
        }

        /// <summary>
        /// Gets the position of the Source.
        /// </summary>
        /// <returns></returns>
        public Vector3 GetPosition()
        {
            float out_x = 0, out_y = 0, out_z = 0;
            Love2dDll.wrap_love_dll_type_Source_getPosition(p, out out_x, out out_y, out out_z);
            return new Vector3(out_x, out_y, out_z);
        }

        /// <summary>
        /// Sets the velocity of the Source.
        /// </summary>
        /// <param name="x">The X part of the velocity vector.</param>
        /// <param name="y">The Y part of the velocity vector.</param>
        /// <param name="z">The Z part of the velocity vector.</param>
        public void SetVelocity(float x, float y, float z)
        {
            Love2dDll.wrap_love_dll_type_Source_setVelocity(p, x, y, z);
        }

        /// <summary>
        /// Gets the velocity of the Source.
        /// </summary>
        /// <returns></returns>
        public Vector3 GetVelocity()
        {
            float out_x = 0, out_y = 0, out_z = 0;
            Love2dDll.wrap_love_dll_type_Source_getVelocity(p, out out_x, out out_y, out out_z);
            return new Vector3(out_x, out_y, out_z);
        }

        /// <summary>
        /// Sets the direction of the Source.
        /// </summary>
        /// <param name="x">The X part of the direction vector.</param>
        /// <param name="y">The Y part of the direction vector.</param>
        /// <param name="z">The Z part of the direction vector.</param>
        public void SetDirection(float x, float y, float z)
        {
            Love2dDll.wrap_love_dll_type_Source_setDirection(p, x, y, z);
        }

        /// <summary>
        /// Gets the direction of the Source.
        /// </summary>
        /// <returns></returns>
        public Vector3 GetDirection()
        {
            float out_x = 0, out_y = 0, out_z = 0;
            Love2dDll.wrap_love_dll_type_Source_getDirection(p, out out_x, out out_y, out out_z);
            return new Vector3(out_x, out_y, out_z);
        }

        /// <summary>
        /// Sets the Source's directional volume cones.
        /// </summary>
        /// <param name="innerAngle">The inner angle from the Source's direction, in radians. The Source will play at normal volume if the listener is inside the cone defined by this angle.</param>
        /// <param name="outerAngle">The outer angle from the Source's direction, in radians. The Source will play at a volume between the normal and outer volumes, if the listener is in between the cones defined by the inner and outer angles.</param>
        /// <param name="outerVolume">The Source's volume when the listener is outside both the inner and outer cone angles.</param>
        public void SetCone(float innerAngle, float outerAngle, float outerVolume = 0)
        {
            Love2dDll.wrap_love_dll_type_Source_setCone(p, innerAngle, outerAngle, outerVolume);
        }

        /// <summary>
        /// Gets the Source's directional volume cones.
        /// </summary>
        /// <param name="out_innerAngle"></param>
        /// <param name="out_outerAngle"></param>
        /// <param name="out_outerVolume"></param>
        public void GetCone(out float out_innerAngle, out float out_outerAngle, out float out_outerVolume)
        {
            Love2dDll.wrap_love_dll_type_Source_getCone(p, out out_innerAngle, out out_outerAngle, out out_outerVolume);
        }

        /// <summary>
        /// Sets whether the Source's position and direction are relative to the listener.
        /// <para>By default, all sources are absolute and therefore relative to the origin of love's coordinate system [0, 0, 0]. Only absolute sources are affected by the position of the listener. Please note that positional audio only works for mono (i.e. non-stereo) sources.</para>
        /// </summary>
        /// <param name="relative">True to make the position, velocity, direction and cone angles relative to the listener, false to make them absolute.</param>
        public void SetRelative(bool relative)
        {
            Love2dDll.wrap_love_dll_type_Source_setRelative(p, relative);
        }

        /// <summary>
        /// Gets whether the Source's position and direction are relative to the listener.
        /// </summary>
        /// <returns>True if the position, velocity, direction and cone angles are relative to the listener, false if they're absolute.</returns>
        public bool IsRelative()
        {
            bool out_relative = false;
            Love2dDll.wrap_love_dll_type_Source_isRelative(p, out out_relative);
            return out_relative;
        }

        /// <summary>
        /// Sets whether the Source should loop.
        /// </summary>
        /// <param name="looping">True if the source should loop, false otherwise.</param>
        public void SetLooping(bool looping)
        {
            Love2dDll.wrap_love_dll_type_Source_setLooping(p, looping);
        }

        /// <summary>
        /// Returns whether the Source will loop.
        /// </summary>
        /// <returns>True if the Source will loop, false otherwise.</returns>
        public bool IsLooping()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_type_Source_isLooping(p, out out_result);
            return out_result;
        }

        /// <summary>
        /// Returns whether the Source is playing.
        /// </summary>
        /// <returns>True if the Source is playing, false otherwise.</returns>
        public bool IsPlaying()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_type_Source_isPlaying(p, out out_result);
            return out_result;
        }

        /// <summary>
        /// Sets the volume limits of the source.
        /// </summary>
        /// <param name="vmin">The minimum volume.</param>
        /// <param name="vmax">The maximum volume.</param>
        public void SetVolumeLimits(float vmin, float vmax)
        {
            Love2dDll.wrap_love_dll_type_Source_setVolumeLimits(p, vmin, vmax);
        }

        /// <summary>
        /// Returns the volume limits of the source.
        /// </summary>
        /// <param name="out_vmin">The minimum volume.</param>
        /// <param name="out_vmax">The minimum volume.</param>
        public void GetVolumeLimits(out float out_vmin, out float out_vmax)
        {
            Love2dDll.wrap_love_dll_type_Source_getVolumeLimits(p, out out_vmin, out out_vmax);
        }

        /// <summary>
        /// Sets the reference and maximum attenuation distances of the Source.
        /// <para>The parameters, combined with the current DistanceModel, affect how the Source's volume attenuates based on distance. Distance attenuation is only applicable to Sources based on mono (rather than stereo) audio.</para>
        /// </summary>
        /// <param name="dref">The new reference attenuation distance. If the current <see cref="DistanceModel"/> is clamped, this is the minimum attenuation distance.</param>
        /// <param name="dmax"></param>
        public void SetAttenuationDistances(float dref, float dmax)
        {
            Love2dDll.wrap_love_dll_type_Source_setAttenuationDistances(p, dref, dmax);
        }

        /// <summary>
        /// Gets the reference and maximum attenuation distances of the Source.
        /// <para>The values, combined with the current DistanceModel, affect how the Source's volume attenuates based on distance from the listener.</para>
        /// </summary>
        /// <param name="out_dref">The current reference attenuation distance. If the current <see cref="DistanceModel"/> is clamped, this is the minimum distance before the Source is no longer attenuated.</param>
        /// <param name="out_dmax">The current maximum attenuation distance.</param>
        public void GetAttenuationDistances(out float out_dref, out float out_dmax)
        {
            Love2dDll.wrap_love_dll_type_Source_getAttenuationDistances(p, out out_dref, out out_dmax);
        }

        /// <summary>
        /// Sets the rolloff factor which affects the strength of the used distance attenuation. see <see cref="DistanceModel"/>
        /// <para>Extended information and detailed formulas can be found in the chapter "3.4. Attenuation By Distance" of OpenAL 1.1 specification(https://www.openal.org/documentation/openal-1.1-specification.pdf).</para>
        /// </summary>
        /// <param name="rolloff">The new rolloff factor.</param>
        public void SetRolloff(float rolloff)
        {
            Love2dDll.wrap_love_dll_type_Source_setRolloff(p, rolloff);
        }

        /// <summary>
        /// Returns the rolloff factor of the source.
        /// </summary>
        /// <returns>The rolloff factor.</returns>
        public float GetRolloff()
        {
            float out_rolloff = 0;
            Love2dDll.wrap_love_dll_type_Source_getRolloff(p, out out_rolloff);
            return out_rolloff;
        }

        /// <summary>
        /// Gets the number of channels in the Source. Only 1-channel (mono) Sources can use directional and positional effects.
        /// </summary>
        /// <returns>1 for mono, 2 for stereo.</returns>
        public int GetChannelCount()
        {
            int out_chanels = 0;
            Love2dDll.wrap_love_dll_type_Source_getChannelCount(p, out out_chanels);
            return out_chanels;
        }

        /// <summary>
        /// Gets the type of the Source.
        /// </summary>
        /// <returns>The type of the source.</returns>
        public SourceType GetSourceType()
        {
            int out_type = 0;
            Love2dDll.wrap_love_dll_type_Source_getType(p, out out_type);
            return (SourceType)out_type;
        }
    }

    /// <summary>
    /// <para>Represents a file on the filesystem. A function that takes a file path can also take a <see cref="File"/>.</para>
    /// </summary>
    public partial class File : LoveObject
    {
        /// <summary>
        /// disable no-param construct
        /// </summary>
        protected File() {}

        /// <summary>
        /// Returns the file size(in byte).
        /// </summary>
        /// <returns></returns>
        public double GetSize()
        {
            double out_size = 0;
            Love2dDll.wrap_love_dll_type_File_getSize(p, out out_size);
            return out_size;
        }

        /// <summary>
        /// Open the file for write, read or append.
        /// </summary>
        /// <param name="mode_type">The mode to open the file in.</param>
        public bool Open(FileMode mode_type)
        {
            return Love2dDll.wrap_love_dll_type_File_open(p, (int)mode_type);
        }

        /// <summary>
        /// Closes a File.
        /// </summary>
        /// <returns>Whether closing was successful.</returns>
        public bool Close()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_type_File_close(p, out out_result);
            return out_result;
        }

        /// <summary>
        /// Gets whether the file is open.
        /// </summary>
        /// <returns>True if the file is currently open, false otherwise.</returns>
        public bool IsOpen()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_type_File_isOpen(p, out out_result);
            return out_result;
        }

        /// <summary>
        /// Read a number of bytes from a file
        /// </summary>
        /// <param name="size">The number of bytes to read.</param>
        /// <returns></returns>
        public byte[] Read(long size)
        {
            IntPtr out_data = IntPtr.Zero;
            long out_readedSize = 0;
            Love2dDll.wrap_love_dll_type_File_read(p, size, out out_data, out out_readedSize);

            return DllTool.ReadBytesAndRelease(out_data, out_readedSize);
        }

        /// <summary>
        /// Write data to a file.
        /// </summary>
        /// <param name="data">The byte data to write.</param>
        /// <param name="datasize">How many bytes to write.</param>
        /// <returns>Whether the operation was successful.</returns>
        public bool Write(byte[] data, long datasize)
        {
            return Love2dDll.wrap_love_dll_type_File_write_String(p, data, datasize);
        }

        /// <summary>
        /// Write data to a file.
        /// </summary>
        /// <param name="data">The Data object to write.</param>
        /// <param name="datasize">How many bytes to write.</param>
        /// <returns>Whether the operation was successful.</returns>
        public bool Write(Data data, long datasize)
        {
            return Love2dDll.wrap_love_dll_type_File_write_Data_datasize(p, data.p, datasize);
        }

        /// <summary>
        /// Flushes any buffered written data in the file to disk.
        /// </summary>
        public void Flush()
        {
            Love2dDll.wrap_love_dll_type_File_flush(p);
        }

        /// <summary>
        /// Gets whether end-of-file has been reached.
        /// </summary>
        /// <returns>Whether EOF has been reached.</returns>
        public bool IsEOF()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_type_File_isEOF(p, out out_result);
            return out_result;
        }

        /// <summary>
        /// Returns the position in the file.
        /// </summary>
        /// <returns>The current position.</returns>
        public long Tell()
        {
            long out_pos = 0;
            Love2dDll.wrap_love_dll_type_File_tell(p, out out_pos);
            return out_pos;
        }

        /// <summary>
        /// Seek to a position in a file
        /// </summary>
        /// <param name="pos">The position to seek to</param>
        /// <returns>Whether the operation was successful</returns>
        public bool Seek(long pos)
        {
            return Love2dDll.wrap_love_dll_type_File_seek(p, pos);
        }

        /// <summary>
        /// <para>Sets the buffer mode for a file opened for writing or appending. Files with buffering enabled will not write data to the disk until the buffer size limit is reached, depending on the buffer mode.</para>
        /// <para><see cref="Flush"/> will force any buffered data to be written to the disk.</para>
        /// </summary>
        /// <param name="bufmode_type">The buffer mode to use.</param>
        /// <param name="size">The maximum size in bytes of the file's buffer.</param>
        /// <returns>Whether the buffer mode was successfully set.</returns>
        public bool SetBuffer(BufferMode bufmode_type, long size = 0)
        {
            return Love2dDll.wrap_love_dll_type_File_setBuffer(p, (int)bufmode_type, size);
        }

        /// <summary>
        /// Gets the buffer mode of a file.
        /// </summary>
        /// <param name="out_bufmode_type">The current buffer mode of the file.</param>
        /// <param name="out_size">The maximum size in bytes of the file's buffer.</param>
        public void GetBuffer(out BufferMode out_bufmode_type, out long out_size)
        {
            int bufmode_type = 0;
            Love2dDll.wrap_love_dll_type_File_getBuffer(p, out bufmode_type, out out_size);
            out_bufmode_type = (BufferMode)bufmode_type;
        }

        /// <summary>
        /// Gets the <see cref="FileMode"/> the file has been opened with.
        /// </summary>
        /// <returns>The mode this file has been opened with.</returns>
        public FileMode GetMode()
        {
            int mode_type = 0;
            Love2dDll.wrap_love_dll_type_File_getMode(p, out mode_type);
            return (FileMode)mode_type;
        }

        /// <summary>
        /// Gets the filename that the File object was created with. If the file object originated from the <see cref="Scene.FileDropped(File)"/> callback, the filename will be the full platform-dependent file path.
        /// </summary>
        /// <returns></returns>
        public string GetFilename()
        {
            IntPtr out_filename = IntPtr.Zero;
            Love2dDll.wrap_love_dll_type_File_getFilename(p, out out_filename);
            return DllTool.WSToStringAndRelease(out_filename);
        }

    }

    /// <summary>
    /// <see cref="Data"/> representing the contents of a file.
    /// </summary>
    public partial class FileData : Data
    {
        /// <summary>
        /// disable no-param construct
        /// </summary>
        protected FileData() {}

        /// <summary>
        /// Gets the filename of the FileData.
        /// </summary>
        /// <returns>The name of the file the FileData represents.</returns>
        public string GetFilename()
        {
            IntPtr out_filename = IntPtr.Zero;
            Love2dDll.wrap_love_dll_type_FileData_getFilename(p, out out_filename);
            return DllTool.WSToStringAndRelease(out_filename);
        }

        /// <summary>
        /// Gets the extension of the FileData.
        /// </summary>
        /// <returns>The extension of the file the FileData represents.</returns>
        public string GetExtension()
        {
            IntPtr out_extension = IntPtr.Zero;
            Love2dDll.wrap_love_dll_type_FileData_getExtension(p, out out_extension);
            return DllTool.WSToStringAndRelease(out_extension);
        }
    }

    /// <summary>
    /// A GlyphData represents a drawable symbol of a font Rasterizer.
    /// </summary>
    public partial class GlyphData : Data
    {
        /// <summary>
        /// disable no-param construct
        /// </summary>
        protected GlyphData() {}

        public enum Format : int
        {
            LuminanceAlpha,
            RGBA,
        };

        public int GetWidth()
        {
            int out_width = 0;
            Love2dDll.wrap_love_dll_type_GlyphData_getWidth(p, out out_width);
            return out_width;
        }
        public int GetHeight()
        {
            int out_height = 0;
            Love2dDll.wrap_love_dll_type_GlyphData_getHeight(p, out out_height);
            return out_height;
        }
        public uint GetGlyph()
        {
            uint out_glyph = 0;
            Love2dDll.wrap_love_dll_type_GlyphData_getGlyph(p, out out_glyph);
            return out_glyph;
        }
        public string GetGlyphString()
        {
            IntPtr out_str = IntPtr.Zero;
            Love2dDll.wrap_love_dll_type_GlyphData_getGlyphString(p, out out_str);
            return DllTool.WSToStringAndRelease(out_str);
        }
        public int GetAdvance()
        {
            int out_advance = 0;
            Love2dDll.wrap_love_dll_type_GlyphData_getAdvance(p, out out_advance);
            return out_advance;
        }
        public void GetBearing(out int out_bearingX, out int out_bearingY)
        {
            Love2dDll.wrap_love_dll_type_GlyphData_getBearing(p, out out_bearingX, out out_bearingY);
        }
        public void GetBoundingBox(out int out_minX, out int out_minY, out int out_width, out int out_height)
        {
            Love2dDll.wrap_love_dll_type_GlyphData_getBoundingBox(p, out out_minX, out out_minY, out out_width, out out_height);
        }
        public Format GetFormat()
        {
            int out_format_type = 0;
            Love2dDll.wrap_love_dll_type_GlyphData_getFormat(p, out out_format_type);
            return (Format)out_format_type;
        }
    }

    /// <summary>
    /// A Rasterizer handles font rendering, containing the font data (image or TrueType font) and drawable glyphs.
    /// </summary>
    public partial class Rasterizer : LoveObject
    {
        /// <summary>
        /// disable no-param construct
        /// </summary>
        protected Rasterizer() {}

        /// <summary>
        /// Get font height
        /// </summary>
        /// <returns></returns>
        public int GetHeight()
        {
            int out_heigth = 0;
            Love2dDll.wrap_love_dll_type_Rasterizer_getHeight(p, out out_heigth);
            return out_heigth;
        }
        public int GetAdvance()
        {
            int out_advance = 0;
            Love2dDll.wrap_love_dll_type_Rasterizer_getAdvance(p, out out_advance);
            return out_advance;
        }
        public int GetAscent()
        {
            int out_ascent = 0;
            Love2dDll.wrap_love_dll_type_Rasterizer_getAscent(p, out out_ascent);
            return out_ascent;
        }
        public int GetDescent()
        {
            int out_descent = 0;
            Love2dDll.wrap_love_dll_type_Rasterizer_getDescent(p, out out_descent);
            return out_descent;
        }
        public int GetLineHeight()
        {
            int out_lineHeight = 0;
            Love2dDll.wrap_love_dll_type_Rasterizer_getLineHeight(p, out out_lineHeight);
            return out_lineHeight;
        }
        public GlyphData GetGlyphData(uint glyph)
        {
            IntPtr out_glyphData = IntPtr.Zero;
            Love2dDll.wrap_love_dll_type_Rasterizer_getGlyphData_uint32(p, glyph, out out_glyphData);
            return LoveObject.NewObject<GlyphData>(out_glyphData);
        }
        public GlyphData GetGlyphData(byte[] str)
        {
            IntPtr out_glyphData = IntPtr.Zero;
            Love2dDll.wrap_love_dll_type_Rasterizer_getGlyphData_string(p, str, out out_glyphData);
            return LoveObject.NewObject<GlyphData>(out_glyphData);
        }
        public int GetGlyphCount()
        {
            int out_count = 0;
            Love2dDll.wrap_love_dll_type_Rasterizer_getGlyphCount(p, out out_count);
            return out_count;
        }
        public bool HasGlyphs(uint glyph)
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_type_Rasterizer_hasGlyphs_uint32(p, glyph, out out_result);
            return out_result;
        }
        public bool HasGlyphs(byte[] str)
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_type_Rasterizer_hasGlyphs_string(p, str, out out_result);
            return out_result;
        }
    }

    /// <summary>
    /// <para>A Canvas is used for off-screen rendering. Think of it as an invisible screen that you can draw to, but that will not be visible until you draw it to the actual visible screen. It is also known as "render to texture".</para>
    /// <para>By drawing things that do not change position often (such as background items) to the Canvas, and then drawing the entire Canvas instead of each item, you can reduce the number of draw operations performed each frame.</para>
    /// <para>When drawing content to a Canvas using regular alpha blending, the alpha values of the content get multiplied with its RGB values. Therefore the Canvas' pixel colors will have premultiplied alpha once it has been drawn to, so when drawing the Canvas to the screen or to another Canvas you must use premultiplied alpha blending – Graphics.SetBlendMode(BlendMode.Alpha, BlendAlphaMode.PreMultiplied).</para>
    /// </summary>
    public partial class Canvas : Texture
    {
        /// <summary>
        /// disable no-param construct
        /// </summary>
        protected Canvas() {}


        /// <summary>
        /// Generates ImageData from the contents of the Canvas.
        /// </summary>
        /// <returns></returns>
        public ImageData NewImageData()
        {
            return NewImageData(0, 0, 0, 0, GetWidth(), GetHeight());
        }


        /// <summary>
        /// Generates ImageData from the contents of the Canvas.
        /// </summary>
        /// <param name="slice">The cubemap face index, array index, or depth layer for cubemap, array, or volume type Canvases, respectively. This argument is ignored for regular 2D canvases.</param>
        /// <param name="mipmap">he mipmap index to use, for Canvases with mipmaps. https://love2d.org/wiki/CanvasMipmapMode </param>
        /// <returns></returns>
        public ImageData NewImageData(int slice, int mipmap = 0)
        {
            IntPtr out_imageData = IntPtr.Zero;
            Love2dDll.wrap_love_dll_type_Canvas_newImageData_xywh(p, slice, mipmap, 0, 0, GetWidth(), GetHeight(), out out_imageData);
            return NewObject<ImageData>(out_imageData);
        }

        /// <summary>
        /// Generates ImageData from the contents of the Canvas.
        /// </summary>
        /// <param name="slice">The cubemap face index, array index, or depth layer for cubemap, array, or volume type Canvases, respectively. This argument is ignored for regular 2D canvases.</param>
        /// <param name="mipmap">he mipmap index to use, for Canvases with mipmaps. (default is 0) https://love2d.org/wiki/CanvasMipmapMode </param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        /// <returns></returns>
        public ImageData NewImageData(int slice, int mipmap, int x, int y, int w, int h)
        {
            IntPtr out_imageData = IntPtr.Zero;
            Love2dDll.wrap_love_dll_type_Canvas_newImageData_xywh(p, slice, mipmap, x, y, w, h, out out_imageData);
            return NewObject<ImageData>(out_imageData);
        }

        /// <summary>
        /// Gets the texture format of the Canvas.
        /// </summary>
        /// <returns></returns>
        public PixelFormat GetFormat()
        {
            int out_format_type = 0;
            Love2dDll.wrap_love_dll_type_Canvas_getFormat(p, out out_format_type);
            return (PixelFormat)out_format_type;
        }

        /// <summary>
        /// Gets the number of MSAA samples used when drawing to the Canvas.
        /// </summary>
        /// <returns></returns>
        public int GetMSAA()
        {
            int out_msaa = 0;
            Love2dDll.wrap_love_dll_type_Canvas_getMSAA(p, out out_msaa);
            return out_msaa;
        }
    }

    public partial class Font : LoveObject
    {
        /// <summary>
        /// disable construct
        /// </summary>
        protected Font() {}

        /// <summary>
        /// Gets the height of the Font in pixels.
        /// </summary>
        /// <returns>The height of the Font in pixels.</returns>
        public int GetHeight()
        {
            int out_height = 0;
            Love2dDll.wrap_love_dll_type_Font_getHeight(p, out out_height);
            return out_height;
        }

        /// <summary>
        /// Determines the width of the given text. (UTF-8 byte array version)
        /// </summary>
        /// <param name="str">A string. (UTF-8 byte array needed)</param>
        /// <returns>The width of the text.</returns>
        public int GetWidth(byte[] str)
        {
            int out_width = 0;
            Love2dDll.wrap_love_dll_type_Font_getWidth(p, str, out out_width);
            return out_width;
        }

        /// <summary>
        /// Sets the line height. When rendering the font in lines the actual height will be determined by the line height multiplied by the height of the font. The default is 1.0.
        /// </summary>
        /// <param name="h">The new line height.</param>
        public void SetLineHeight(float h)
        {
            Love2dDll.wrap_love_dll_type_Font_setLineHeight(p, h);
        }

        /// <summary>
        /// Gets the line height. This will be the value previously set by <see cref="SetLineHeight(float)"/>, or 1.0 by default.
        /// </summary>
        /// <returns>The current line height.</returns>
        public float GetLineHeight()
        {
            float out_h = 0;
            Love2dDll.wrap_love_dll_type_Font_getLineHeight(p, out out_h);
            return out_h;
        }

        /// <summary>
        /// Sets the filter mode for a font.
        /// </summary>
        /// <param name="min_type">How to scale a font down.</param>
        /// <param name="mag_type">How to scale a font up.</param>
        /// <param name="anisotropy">Maximum amount of anisotropic filtering used.</param>
        public void SetFilter(FilterMode min_type, FilterMode mag_type, float anisotropy = 1)
        {
            Love2dDll.wrap_love_dll_type_Font_setFilter(p, (int)min_type, (int)mag_type, anisotropy);
        }

        /// <summary>
        /// Gets the filter mode for a font.
        /// </summary>
        /// <returns></returns>
        /// <param name="min_type">Filter mode used when minifying the font.</param>
        /// <param name="mag_type">Filter mode used when magnifying the font.</param>
        /// <param name="out_anisotropy">Maximum amount of anisotropic filtering used.</param>
        public void GetFilter(out FilterMode min_type, out FilterMode mag_type, out float out_anisotropy)
        {
            int out_min_type, out_mag_type;
            Love2dDll.wrap_love_dll_type_Font_getFilter(p, out out_min_type, out out_mag_type, out out_anisotropy);
            min_type = (FilterMode)out_min_type;
            mag_type = (FilterMode)out_mag_type;
        }

        /// <summary>
        /// Gets the ascent of the Font. The ascent spans the distance between the baseline and the top of the glyph that reaches farthest from the baseline.
        /// </summary>
        /// <returns></returns>
        public int GetAscent()
        {
            int out_ascent = 0;
            Love2dDll.wrap_love_dll_type_Font_getAscent(p, out out_ascent);
            return out_ascent;
        }

        /// <summary>
        /// Gets the descent of the Font. The descent spans the distance between the baseline and the lowest descending glyph in a typeface.
        /// </summary>
        /// <returns></returns>
        public int GetDescent()
        {
            int out_descent = 0;
            Love2dDll.wrap_love_dll_type_Font_getDescent(p, out out_descent);
            return out_descent;
        }

        /// <summary>
        /// Gets the baseline of the Font. Most scripts share the notion of a baseline: an imaginary horizontal line on which characters rest. In some scripts, parts of glyphs lie below the baseline.
        /// </summary>
        /// <returns></returns>
        public float GetBaseline()
        {
            float out_baseline = 0;
            Love2dDll.wrap_love_dll_type_Font_getBaseline(p, out out_baseline);
            return out_baseline;
        }
        public bool HasGlyphs(uint chr)
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_type_Font_hasGlyphs_uint32(p, chr, out out_result);
            return out_result;
        }

        /// <summary>
        /// Gets whether the Font can render a character or string. (UTF-8 byte array version)
        /// </summary>
        /// <param name="str">A string. (UTF-8 byte array needed)</param>
        /// <returns>Whether the font can render all characters in the string.</returns>
        public bool HasGlyphs(byte[] str)
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_type_Font_hasGlyphs_string(p, str, out out_result);
            return out_result;
        }

        /// <summary>
        /// Sets the fallback fonts. When the Font doesn't contain a glyph, it will substitute the glyph from the next subsequent fallback Fonts. This is akin to setting a "font stack" in Cascading Style Sheets (CSS).
        /// </summary>
        /// <param name="fallback">[The first fallback Font to use, ...Additional fallback Fonts.]</param>
        public void SetFallbacks(params Font[] fallback)
        {
            IntPtr[] fallbackarray = new IntPtr[fallback.Length];
            for (int i = 0; i < fallbackarray.Length; i++)
            {
                fallbackarray[i] = fallback[i].p;
            }

            Love2dDll.wrap_love_dll_type_Font_setFallbacks(p, fallbackarray, fallbackarray.Length);
        }
    }

    public partial class Image : Texture
    {
        /// <summary>
        /// disable construct
        /// </summary>
        protected Image() {}

        /// <summary>
        /// Gets whether the Image was created from <see cref="CompressedImageData"/>.
        /// </summary>
        /// <returns></returns>
        public bool IsCompressed()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_type_Image_isCompressed(p, out out_result);
            return out_result;
        }

        /// <summary>
        /// Replace the contents of an Image.
        /// </summary>
        /// <param name="imageData">The new ImageData to replace the contents with.</param>
        /// <param name="slice">Which cubemap face, array index, or volume layer to replace, if applicable.</param>
        /// <param name="mipmap">The mimap level to replace, if the Image has mipmaps.</param>
        /// <param name="x">The x-offset in pixels from the top-left of the image to replace. The given ImageData's width plus this value must not be greater than the pixel width of the Image's specified mipmap level.</param>
        /// <param name="y">The y-offset in pixels from the top-left of the image to replace. The given ImageData's height plus this value must not be greater than the pixel height of the Image's specified mipmap level.</param>
        /// <param name="reloadmipmaps">Whether to generate new mipmaps after replacing the Image's pixels. True by default if the Image was created with automatically generated mipmaps, false by default otherwise.</param>
        public void ReplacePixels(ImageData imageData, int slice, int mipmap, int x, int y, bool reloadmipmaps)
        {
            Love2dDll.wrap_love_dll_type_Image_replacePixels(p, imageData.p, slice, mipmap, x, y, reloadmipmaps);
        }
    }

    /// <summary>
    /// Mesh in Love2d CS is different from  love2d lua.
    /// </summary>
    public partial class Mesh : Drawable
    {
        /// <summary>
        /// disable construct
        /// </summary>
        protected Mesh() {}

        /// <summary>
        /// Replaces a range of vertices in the Mesh with new ones. 
        /// </summary>
        /// <param name="vertices">each vertex</param>
        /// <param name="startVertex">The index of the first vertex to replace.</param>
        public void SetVertices(Vertex[] vertices, int startVertex = 0)
        {
            var posArray = new Vector2[vertices.Length];
            var uvArray = new Vector2[vertices.Length];
            var colorArray = new Vector4[vertices.Length];
            for (int i = 0; i < vertices.Length; i++)
            {
                posArray[i] = vertices[i].pos;
                uvArray[i] = vertices[i].uv;
                colorArray[i] = vertices[i].color;
            }
            Love2dDll.wrap_love_dll_type_Mesh_setVertices(p, startVertex, posArray, uvArray, colorArray, posArray.Length);
        }
        public void SetVertex(int index, Vertex vertex)
        {
            Love2dDll.wrap_love_dll_type_Mesh_setVertex(p, index, vertex.pos, vertex.uv, vertex.color);
        }

        /// TODO
        public Vertex GetVertex(int index)
        {
            Vector2 pos, uv;
            Vector4 color;
            Love2dDll.wrap_love_dll_type_Mesh_getVertex(p, index, out pos, out uv, out color);
            return new Vertex(pos, uv, color);
        }

        public int GetVertexCount()
        {
            int out_result = 0;
            Love2dDll.wrap_love_dll_type_Mesh_getVertexCount(p, out out_result);
            return out_result;
        }
        public void Flush()
        {
            Love2dDll.wrap_love_dll_type_Mesh_flush(p);
        }
        public void SetVertexMap()
        {
            Love2dDll.wrap_love_dll_type_Mesh_setVertexMap_nil(p);
        }
        public void SetVertexMap(uint[] vertexmaps)
        {
            Love2dDll.wrap_love_dll_type_Mesh_setVertexMap(p, vertexmaps, vertexmaps.Length);
        }
        public uint[] GetVertexMap()
        {
            bool out_has_vertex_map = false;
            IntPtr out_vertexmaps = IntPtr.Zero;
            int out_vertexmaps_length = 0;
            Love2dDll.wrap_love_dll_type_Mesh_getVertexMap(p, out out_has_vertex_map, out out_vertexmaps, out out_vertexmaps_length);

            if (out_has_vertex_map == false)
                return null;

            return DllTool.ReadUInt32sAndRelease(out_vertexmaps, out_vertexmaps_length);
        }
        public void SetTexture()
        {
            Love2dDll.wrap_love_dll_type_Mesh_setTexture_nil(p);
        }
        public void SetTexture(Texture tex)
        {
            if (tex == null)
                SetTexture();
            else
                Love2dDll.wrap_love_dll_type_Mesh_setTexture_Texture(p, tex.p);
        }
        public Texture GetTexture()
        {
            IntPtr out_result = IntPtr.Zero;
            Love2dDll.wrap_love_dll_type_Mesh_getTexture(p, out out_result);
            return NewObject<Texture>(out_result);
        }
        public void SetDrawMode(MeshDrawMode mode_type)
        {
            Love2dDll.wrap_love_dll_type_Mesh_setDrawMode(p, (int)mode_type);
        }
        public MeshDrawMode GetDrawMode()
        {
            int out_mode_type = 0;
            Love2dDll.wrap_love_dll_type_Mesh_getDrawMode(p, out out_mode_type);
            return (MeshDrawMode)out_mode_type;
        }
        public void SetDrawRange()
        {
            Love2dDll.wrap_love_dll_type_Mesh_setDrawRange(p);
        }
        public void SetDrawRange_minmax(int rangemin, int rangemax)
        {
            Love2dDll.wrap_love_dll_type_Mesh_setDrawRange_minmax(p, rangemin, rangemax);
        }
        public void GetDrawRange(out int out_rangemin, out int out_rangemax)
        {
            Love2dDll.wrap_love_dll_type_Mesh_getDrawRange(p, out out_rangemin, out out_rangemax);
        }
    }

    /// <summary>
    /// A ParticleSystem can be used to create particle effects like fire or smoke.
    /// <para>The particle system has to be created using <see cref="Graphics.NewParticleSystem"/>. Just like any other <see cref="Drawable"/> it can be drawn to the screen using love.graphics.draw. You also have to call <see cref="ParticleSystem.Update(float)"/> in the update callback to see any changes in the particles emitted.</para>
    /// <para>The particle system won't create any particles unless you call <see cref="SetParticleLifetime"/> and <see cref="SetEmissionRate"/>.</para>
    /// </summary>
    public partial class ParticleSystem : Drawable
    {
        /// <summary>
        /// disable construct
        /// </summary>
        protected ParticleSystem() {}

        /// <summary>
        /// Creates an identical copy of the ParticleSystem in the stopped state.
        /// </summary>
        /// <returns></returns>
        public ParticleSystem Clone()
        {
            IntPtr out_clone = IntPtr.Zero;
            Love2dDll.wrap_love_dll_type_ParticleSystem_clone(p, out out_clone);
            return NewObject<ParticleSystem>(out_clone);
        }

        /// <summary>
        /// Sets the texture (Image or Canvas) to be used for the particles.
        /// </summary>
        /// <param name="texture">An Image or Canvas to use for the particles.</param>
        public void SetTexture(Texture texture)
        {
            Check.ArgumentNull(texture, "texture");
            Love2dDll.wrap_love_dll_type_ParticleSystem_setTexture(p, texture.p);
        }

        /// <summary>
        /// Gets the texture (Image or Canvas) used for the particles.
        /// </summary>
        /// <returns>The Image or Canvas used for the particles.</returns>
        public Texture GetTexture()
        {
            IntPtr out_texture = IntPtr.Zero;
            Love2dDll.wrap_love_dll_type_ParticleSystem_getTexture(p, out out_texture);
            return NewObject<Texture>(out_texture);
        }

        /// <summary>
        /// Sets the size of the buffer (the max allowed amount of particles in the system).
        /// </summary>
        /// <param name="buffersize">The buffer size.</param>
        public void SetBufferSize(uint buffersize)
        {
            Love2dDll.wrap_love_dll_type_ParticleSystem_setBufferSize(p, buffersize);
        }

        /// <summary>
        /// Gets the maximum number of particles the ParticleSystem can have at once.
        /// </summary>
        /// <returns>The maximum number of particles.</returns>
        public uint GetBufferSize()
        {
            uint out_buffersize = 0;
            Love2dDll.wrap_love_dll_type_ParticleSystem_getBufferSize(p, out out_buffersize);
            return out_buffersize;
        }

        /// <summary>
        /// Sets the mode to use when the ParticleSystem adds new particles.
        /// </summary>
        /// <param name="mode_type">The mode to use when the ParticleSystem adds new particles.</param>
        public void SetInsertMode(ParticleInsertMode mode_type)
        {
            Love2dDll.wrap_love_dll_type_ParticleSystem_setInsertMode(p, (int)mode_type);
        }

        /// <summary>
        /// Gets the mode used when the ParticleSystem adds new particles.
        /// </summary>
        /// <returns>The mode used when the ParticleSystem adds new particles.</returns>
        public ParticleInsertMode GetInsertMode()
        {
            int out_mode_type = 0;
            Love2dDll.wrap_love_dll_type_ParticleSystem_getInsertMode(p, out out_mode_type);
            return (ParticleInsertMode)out_mode_type;
        }

        /// <summary>
        /// Sets the amount of particles emitted per second.
        /// </summary>
        /// <param name="rate">The amount of particles per second.</param>
        public void SetEmissionRate(float rate)
        {
            Love2dDll.wrap_love_dll_type_ParticleSystem_setEmissionRate(p, rate);
        }

        /// <summary>
        /// Gets the amount of particles emitted per second.
        /// </summary>
        /// <returns>The amount of particles per second.</returns>
        public float GetEmissionRate()
        {
            float out_rate = 0;
            Love2dDll.wrap_love_dll_type_ParticleSystem_getEmissionRate(p, out out_rate);
            return out_rate;
        }

        /// <summary>
        /// Sets how long the particle system should emit particles (if -1 then it emits particles forever).
        /// </summary>
        /// <param name="lifetime">The lifetime of the emitter (in seconds).</param>
        public void SetEmitterLifetime(float lifetime)
        {
            Love2dDll.wrap_love_dll_type_ParticleSystem_setEmitterLifetime(p, lifetime);
        }

        /// <summary>
        /// Gets how long the particle system will emit particles (if -1 then it emits particles forever).
        /// </summary>
        /// <returns>The lifetime of the emitter (in seconds).</returns>
        public float GetEmitterLifetime()
        {
            float out_lifetime = 0;
            Love2dDll.wrap_love_dll_type_ParticleSystem_getEmitterLifetime(p, out out_lifetime);
            return out_lifetime;
        }

        /// <summary>
        /// Sets the lifetime of the particles.
        /// </summary>
        /// <param name="min">The minimum life of the particles (in seconds).</param>
        /// <param name="max">The maximum life of the particles (in seconds).</param>
        public void SetParticleLifetime(float min, float max)
        {
            Love2dDll.wrap_love_dll_type_ParticleSystem_setParticleLifetime(p, min, max);
        }

        /// <summary>
        /// Gets the lifetime of the particles.
        /// </summary>
        /// <param name="out_min">The minimum life of the particles (in seconds).</param>
        /// <param name="out_max">The maximum life of the particles (in seconds).</param>
        public void GetParticleLifetime(out int out_min, out int out_max)
        {
            Love2dDll.wrap_love_dll_type_ParticleSystem_getParticleLifetime(p, out out_min, out out_max);
        }

        /// <summary>
        /// Sets the position of the emitter.
        /// </summary>
        /// <param name="x">Position along x-axis.</param>
        /// <param name="y">Position along y-axis.</param>
        public void SetPosition(float x, float y)
        {
            Love2dDll.wrap_love_dll_type_ParticleSystem_setPosition(p, x, y);
        }

        /// <summary>
        /// Gets the position of the emitter.
        /// </summary>
        /// <returns>Position of the emitter.</returns>
        public Vector2 GetPosition()
        {
            float out_x = 0, out_y = 0;
            Love2dDll.wrap_love_dll_type_ParticleSystem_getPosition(p, out out_x, out out_y);
            return new Vector2(out_x, out_y);
        }

        /// <summary>
        /// Moves the position of the emitter.
        /// This results in smoother particle spawning behaviour than if ParticleSystem.SetPosition is used every frame.
        /// </summary>
        /// <param name="x">Position along x-axis.</param>
        /// <param name="y">Position along y-axis.</param>
        public void MoveTo(float x, float y)
        {
            Love2dDll.wrap_love_dll_type_ParticleSystem_moveTo(p, x, y);
        }

        /// <summary>
        /// Gets the area-based spawn parameters for the particles.
        /// </summary>
        /// <param name="out_distribution_type">The type of distribution for new particles.</param>
        /// <param name="out_x"></param>
        /// <param name="out_y"></param>
        public void GetAreaSpread(out AreaSpreadDistribution out_distribution_type, out float out_x, out float out_y)
        {
            int out_distribution = 0;
            Love2dDll.wrap_love_dll_type_ParticleSystem_getAreaSpread(p, out out_distribution, out out_x, out out_y);
            out_distribution_type = (AreaSpreadDistribution)out_distribution;
        }

        /// <summary>
        /// Sets area-based spawn parameters for the particles. Newly created particles will spawn in an area around the emitter based on the parameters to this function.
        /// </summary>
        /// <param name="distribution">The type of distribution for new particles.</param>
        /// <param name="dx">The maximum spawn distance from the emitter along the x-axis for uniform distribution, or the standard deviation along the x-axis for normal distribution.</param>
        /// <param name="dy">The maximum spawn distance from the emitter along the y-axis for uniform distribution, or the standard deviation along the y-axis for normal distribution.</param>
        /// <param name="angle">The angle in radians of the emission area.</param>
        /// <param name="directionRelativeToCenter">True if newly spawned particles will be oriented relative to the center of the emission area, false otherwise.</param>
        public void SetEmissionArea(AreaSpreadDistribution distribution, float dx, float dy, float angle = 0, bool directionRelativeToCenter = false)
        {
            Love2dDll.wrap_love_dll_type_ParticleSystem_setEmissionArea(p, (int)distribution, dx, dy, angle, directionRelativeToCenter);
        }

        /// <summary>
        /// Sets the direction the particles will be emitted in.
        /// </summary>
        /// <param name="direction">The direction of the particles(in radians).</param>
        public void SetDirection(float direction)
        {
            Love2dDll.wrap_love_dll_type_ParticleSystem_setDirection(p, direction);
        }

        /// <summary>
        /// Gets the direction of the particle emitter (in radians).
        /// </summary>
        /// <returns>The direction of the emitter (radians).</returns>
        public float GetDirection()
        {
            float out_direction = 0;
            Love2dDll.wrap_love_dll_type_ParticleSystem_getDirection(p, out out_direction);
            return out_direction;
        }

        /// <summary>
        /// Sets the amount of spread for the system.
        /// </summary>
        /// <param name="spread">The amount of spread (radians).</param>
        public void SetSpread(float spread)
        {
            Love2dDll.wrap_love_dll_type_ParticleSystem_setSpread(p, spread);
        }

        /// <summary>
        /// Gets the amount of directional spread of the particle emitter (in radians).
        /// </summary>
        /// <returns>The spread of the emitter (radians).</returns>
        public float GetSpread()
        {
            float out_spread = 0;
            Love2dDll.wrap_love_dll_type_ParticleSystem_getSpread(p, out out_spread);
            return out_spread;
        }

        /// <summary>
        /// Sets the speed of the particles.
        /// </summary>
        /// <param name="min">The minimum linear speed of the particles.</param>
        /// <param name="max">The maximun linear speed of the particles.</param>
        public void SetSpeed(float min, float max)
        {
            Love2dDll.wrap_love_dll_type_ParticleSystem_setSpeed(p, min, max);
        }

        /// <summary>
        /// Gets the speed of the particles.
        /// </summary>
        /// <param name="out_min">The minimum linear speed of the particles.</param>
        /// <param name="out_max">The maximum linear speed of the particles.</param>
        public void GetSpeed(out float out_min, out float out_max)
        {
            Love2dDll.wrap_love_dll_type_ParticleSystem_getSpeed(p, out out_min, out out_max);
        }

        /// <summary>
        /// Sets the linear acceleration (acceleration along the x and y axes) for particles.
        /// <para>Every particle created will accelerate along the x and y axes between xmin,ymin and xmax,ymax.</para>
        /// </summary>
        /// <param name="xmin">The minimum acceleration along the x axis.</param>
        /// <param name="ymin">The minimum acceleration along the y axis.</param>
        /// <param name="xmax">The maximum acceleration along the x axis.</param>
        /// <param name="ymax">The maximum acceleration along the y axis.</param>
        public void SetLinearAcceleration(float xmin, float ymin, float xmax, float ymax)
        {
            Love2dDll.wrap_love_dll_type_ParticleSystem_setLinearAcceleration(p, xmin, ymin, xmax, ymax);
        }

        /// <summary>
        /// Gets the linear acceleration (acceleration along the x and y axes) for particles.
        /// <para>Every particle created will accelerate along the x and y axes between xmin,ymin and xmax,ymax.</para>
        /// </summary>
        /// <param name="out_xmin">The minimum acceleration along the x axis.</param>
        /// <param name="out_ymin">The minimum acceleration along the y axis.</param>
        /// <param name="out_xmax">The maximum acceleration along the x axis.</param>
        /// <param name="out_ymax">The maximum acceleration along the y axis.</param>
        public void GetLinearAcceleration(out float out_xmin, out float out_ymin, out float out_xmax, out float out_ymax)
        {
            Love2dDll.wrap_love_dll_type_ParticleSystem_getLinearAcceleration(p, out out_xmin, out out_ymin, out out_xmax, out out_ymax);
        }

        /// <summary>
        /// Set the radial acceleration (away from the emitter).
        /// </summary>
        /// <param name="min">The minimum acceleration.</param>
        /// <param name="max">The maximum acceleration.</param>
        public void SetRadialAcceleration(float min, float max)
        {
            Love2dDll.wrap_love_dll_type_ParticleSystem_setRadialAcceleration(p, min, max);
        }

        /// <summary>
        /// Gets the radial acceleration (away from the emitter).
        /// </summary>
        /// <param name="out_min">The minimum acceleration.</param>
        /// <param name="out_max">The maximum acceleration.</param>
        public void GetRadialAcceleration(out int out_min, out int out_max)
        {
            Love2dDll.wrap_love_dll_type_ParticleSystem_getRadialAcceleration(p, out out_min, out out_max);
        }

        /// <summary>
        /// Sets the tangential acceleration (acceleration perpendicular to the particle's direction).
        /// </summary>
        /// <param name="min">The minimum acceleration.</param>
        /// <param name="max">The maximum acceleration.</param>
        public void SetTangentialAcceleration(float min, float max)
        {
            Love2dDll.wrap_love_dll_type_ParticleSystem_setTangentialAcceleration(p, min, max);
        }

        /// <summary>
        /// Gets the tangential acceleration (acceleration perpendicular to the particle's direction).
        /// </summary>
        /// <param name="out_min">The minimum acceleration.</param>
        /// <param name="out_max">The maximum acceleration.</param>
        public void GetTangentialAcceleration(out int out_min, out int out_max)
        {
            Love2dDll.wrap_love_dll_type_ParticleSystem_getTangentialAcceleration(p, out out_min, out out_max);
        }

        /// <summary>
        /// Sets the amount of linear damping (constant deceleration) for particles.
        /// </summary>
        /// <param name="min">The minimum amount of linear damping applied to particles.</param>
        /// <param name="max">The maximum amount of linear damping applied to particles.</param>
        public void SetLinearDamping(float min, float max)
        {
            Love2dDll.wrap_love_dll_type_ParticleSystem_setLinearDamping(p, min, max);
        }

        /// <summary>
        /// Gets the amount of linear damping (constant deceleration) for particles.
        /// </summary>
        /// <param name="out_min">The minimum amount of linear damping applied to particles.</param>
        /// <param name="out_max">The maximum amount of linear damping applied to particles.</param>
        public void GetLinearDamping(out int out_min, out int out_max)
        {
            Love2dDll.wrap_love_dll_type_ParticleSystem_getLinearDamping(p, out out_min, out out_max);
        }

        /// <summary>
        /// Sets a series of sizes by which to scale a particle sprite. 1.0 is normal size. The particle system will interpolate between each size evenly over the particle's lifetime.
        /// <para>At least one size must be specified.A maximum of eight may be used.</para>
        /// </summary>
        /// <param name="sizeArray">The size array.</param>
        public void SetSizes(params float[] sizeArray)
        {
            Love2dDll.wrap_love_dll_type_ParticleSystem_setSizes(p, sizeArray, sizeArray.Length);
        }

        /// <summary>
        /// Gets the series of sizes by which the sprite is scaled. 1.0 is normal size. The particle system will interpolate between each size evenly over the particle's lifetime.
        /// </summary>
        /// <returns>The size array.</returns>
        public float[] GetSizes()
        {
            IntPtr out_sizearray = IntPtr.Zero;
            int out_sizearray_length = 0;
            Love2dDll.wrap_love_dll_type_ParticleSystem_getSizes(p, out out_sizearray, out out_sizearray_length);
            return DllTool.ReadFloatsAndRelease(out_sizearray, out_sizearray_length);
        }

        /// <summary>
        /// Sets the amount of size variation (0 meaning no variation and 1 meaning full variation between start and end).
        /// </summary>
        /// <param name="variation">The amount of variation (0 meaning no variation and 1 meaning full variation between start and end).</param>
        public void SetSizeVariation(float variation)
        {
            Love2dDll.wrap_love_dll_type_ParticleSystem_setSizeVariation(p, variation);
        }

        /// <summary>
        /// Gets the amount of size variation (0 meaning no variation and 1 meaning full variation between start and end).
        /// </summary>
        /// <returns>The amount of variation (0 meaning no variation and 1 meaning full variation between start and end).</returns>
        public float GetSizeVariation()
        {
            float out_variation = 0;
            Love2dDll.wrap_love_dll_type_ParticleSystem_getSizeVariation(p, out out_variation);
            return out_variation;
        }

        /// <summary>
        /// Sets the rotation of the image upon particle creation (in radians).
        /// </summary>
        /// <param name="min">The minimum initial angle (radians).</param>
        /// <param name="max">The maximum initial angle (radians).</param>
        public void SetRotation(float min, float max)
        {
            Love2dDll.wrap_love_dll_type_ParticleSystem_setRotation(p, min, max);
        }

        /// <summary>
        /// Gets the rotation of the image upon particle creation (in radians).
        /// </summary>
        /// <param name="out_min">The minimum initial angle (radians).</param>
        /// <param name="out_max">The maximum initial angle (radians).</param>
        public void GetRotation(out int out_min, out int out_max)
        {
            Love2dDll.wrap_love_dll_type_ParticleSystem_getRotation(p, out out_min, out out_max);
        }

        /// <summary>
        /// Sets the spin of the sprite.
        /// </summary>
        /// <param name="start">The minimum spin (radians per second).</param>
        /// <param name="end">The maximum spin (radians per second).</param>
        public void SetSpin(float start, float end)
        {
            Love2dDll.wrap_love_dll_type_ParticleSystem_setSpin(p, start, end);
        }

        /// <summary>
        /// Gets the spin of the sprite.
        /// </summary>
        /// <param name="out_start">The minimum spin (radians per second).</param>
        /// <param name="out_end">The maximum spin (radians per second).</param>
        public void GetSpin(out float out_start, out float out_end)
        {
            Love2dDll.wrap_love_dll_type_ParticleSystem_getSpin(p, out out_start, out out_end);
        }

        /// <summary>
        /// Sets the amount of spin variation (0 meaning no variation and 1 meaning full variation between start and end).
        /// </summary>
        /// <param name="variation">The amount of variation (0 meaning no variation and 1 meaning full variation between start and end).</param>
        public void SetSpinVariation(float variation)
        {
            Love2dDll.wrap_love_dll_type_ParticleSystem_setSpinVariation(p, variation);
        }

        /// <summary>
        /// Gets the amount of spin variation (0 meaning no variation and 1 meaning full variation between start and end).
        /// </summary>
        /// <returns>The amount of variation (0 meaning no variation and 1 meaning full variation between start and end).</returns>
        public float GetSpinVariation()
        {
            float out_variation = 0;
            Love2dDll.wrap_love_dll_type_ParticleSystem_getSpinVariation(p, out out_variation);
            return out_variation;
        }

        /// <summary>
        /// Set the offset position which the particle sprite is rotated around. If this function is not used, the particles rotate around their center.
        /// </summary>
        /// <param name="x">The x coordinate of the rotation offset.</param>
        /// <param name="y">The y coordinate of the rotation offset.</param>
        public void SetOffset(float x, float y)
        {
            Love2dDll.wrap_love_dll_type_ParticleSystem_setOffset(p, x, y);
        }

        /// <summary>
        /// Get the offset position which the particle sprite is rotated around. If this function is not used, the particles rotate around their center.
        /// </summary>
        /// <param name="out_x">The x coordinate of the rotation offset.</param>
        /// <param name="out_y">The y coordinate of the rotation offset.</param>
        public void GetOffset(out float out_x, out float out_y)
        {
            Love2dDll.wrap_love_dll_type_ParticleSystem_getOffset(p, out out_x, out out_y);
        }

        /// <summary>
        /// <para>Sets a series of colors to apply to the particle sprite. The particle system will interpolate between each color evenly over the particle's lifetime.</para>
        /// <para>Arguments can be passed in groups of four, representing the components of the desired RGBA value, or as tables of RGBA component values, with a default alpha value of 1 if only three values are given. At least one color must be specified. A maximum of eight may be used.</para>
        /// </summary>
        /// <param name="colorArray"></param>
        public void SetColors(params Vector4[] colorArray)
        {
            Love2dDll.wrap_love_dll_type_ParticleSystem_setColors(p, colorArray, colorArray.Length);
        }

        /// <summary>
        /// Gets the series of colors applied to the particle sprite.
        /// </summary>
        /// <returns></returns>
        public Vector4[] GetColors()
        {
            IntPtr out_colorarray = IntPtr.Zero;
            int out_colorarray_length = 0;
            Love2dDll.wrap_love_dll_type_ParticleSystem_getColors(p, out out_colorarray, out out_colorarray_length);
            return DllTool.ReadVector4sAndRelease(out_colorarray, out_colorarray_length);
        }

        /// <summary>
        /// Sets a series of Quads to use for the particle sprites. Particles will choose a Quad from the list based on the particle's current lifetime, allowing for the use of animated sprite sheets with ParticleSystems.
        /// </summary>
        /// <param name="quads">The Quads to use.</param>
        public void SetQuads(params Quad[] quads)
        {
            IntPtr[] quadsarray = new IntPtr[quads.Length];
            for (int i = 0; i < quads.Length; i++)
            {
                quadsarray[i] = quads[i].p;
            }

            Love2dDll.wrap_love_dll_type_ParticleSystem_setQuads(p, quadsarray, quadsarray.Length);
        }

        /// <summary>
        /// Gets the series of Quads used for the particle sprites.
        /// </summary>
        /// <returns></returns>
        public Quad[] GetQuads()
        {
            IntPtr out_quadsarray = IntPtr.Zero;
            int out_quadsarray_length = 0;
            Love2dDll.wrap_love_dll_type_ParticleSystem_getQuads(p, out out_quadsarray, out out_quadsarray_length);
            return DllTool.ReadIntPtrsWithConvertAndRelease<Quad>(out_quadsarray, out_quadsarray_length);
        }

        /// <summary>
        /// Sets whether particle angles and rotations are relative to their velocities. If enabled, particles are aligned to the angle of their velocities and rotate relative to that angle.
        /// </summary>
        /// <param name="enable">True to enable relative particle rotation, false to disable it.</param>
        public void SetRelativeRotation(bool enable)
        {
            Love2dDll.wrap_love_dll_type_ParticleSystem_setRelativeRotation(p, enable);
        }

        /// <summary>
        /// Gets whether particle angles and rotations are relative to their velocities. If enabled, particles are aligned to the angle of their velocities and rotate relative to that angle.
        /// </summary>
        /// <returns>True if relative particle rotation is enabled, false if it's disabled.</returns>
        public bool HasRelativeRotation()
        {
            bool out_enable = false;
            Love2dDll.wrap_love_dll_type_ParticleSystem_hasRelativeRotation(p, out out_enable);
            return out_enable;
        }

        /// <summary>
        /// Gets the number of particles that are currently in the system.
        /// </summary>
        /// <returns>The current number of live particles.</returns>
        public uint GetCount()
        {
            uint out_count = 0;
            Love2dDll.wrap_love_dll_type_ParticleSystem_getCount(p, out out_count);
            return out_count;
        }

        /// <summary>
        /// Starts the particle emitter.
        /// </summary>
        public void Start()
        {
            Love2dDll.wrap_love_dll_type_ParticleSystem_start(p);
        }

        /// <summary>
        /// Stops the particle emitter, resetting the lifetime counter.
        /// </summary>
        public void Stop()
        {
            Love2dDll.wrap_love_dll_type_ParticleSystem_stop(p);
        }

        /// <summary>
        /// Pauses the particle emitter.
        /// </summary>
        public void Pause()
        {
            Love2dDll.wrap_love_dll_type_ParticleSystem_pause(p);
        }

        /// <summary>
        /// Resets the particle emitter, removing any existing particles and resetting the lifetime counter.
        /// </summary>
        public void Reset()
        {
            Love2dDll.wrap_love_dll_type_ParticleSystem_reset(p);
        }

        /// <summary>
        /// Emits a burst of particles from the particle emitter.
        /// </summary>
        /// <param name="num">The amount of particles to emit. The number of emitted particles will be truncated if the particle system's max buffer size is reached.</param>
        public void Emit(int num)
        {
            Love2dDll.wrap_love_dll_type_ParticleSystem_emit(p, num);
        }

        /// <summary>
        /// Checks whether the particle system is actively emitting particles.
        /// </summary>
        /// <returns>True if system is active, false otherwise.</returns>
        public bool IsActive()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_type_ParticleSystem_isActive(p, out out_result);
            return out_result;
        }

        /// <summary>
        /// Checks whether the particle system is paused.
        /// </summary>
        /// <returns>True if system is paused, false otherwise.</returns>
        public bool IsPaused()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_type_ParticleSystem_isPaused(p, out out_result);
            return out_result;
        }

        /// <summary>
        /// Checks whether the particle system is stopped.
        /// </summary>
        /// <returns>True if system is stopped, false otherwise.</returns>
        public bool IsStopped()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_type_ParticleSystem_isStopped(p, out out_result);
            return out_result;
        }

        /// <summary>
        /// Updates the particle system; moving, creating and killing particles.
        /// </summary>
        /// <param name="dt">The time (seconds) since last frame.</param>
        public void Update(float dt)
        {
            Love2dDll.wrap_love_dll_type_ParticleSystem_update(p, dt);
        }
    }

    /// <summary>
    /// A quadrilateral (a polygon with four sides and four corners) with texture coordinate information.
    /// <para>Quads can be used to select part of a texture to draw. In this way, one large texture atlas can be loaded, and then split up into sub-images.</para>
    /// <para>Quads 'bleed' when scaled, rotated or drawn at non-integer coordinates, even within SpriteBatches, to compensate for this, use 1px borders around the textures inside the texture atlas (preferably with the same colors as the actual border)</para>
    /// </summary>
    public partial class Quad : LoveObject
    {
        /// <summary>
        /// disable construct
        /// </summary>
        protected Quad() {}

        /// <summary>
        /// Sets the texture coordinates according to a viewport.
        /// </summary>
        /// <param name="x">The top-left corner along the x-axis.</param>
        /// <param name="y">The top-right corner along the y-axis.</param>
        /// <param name="w">The width of the viewport.</param>
        /// <param name="h">The height of the viewport.</param>
        public void SetViewport(float x, float y, float w, float h)
        {
            Love2dDll.wrap_love_dll_type_Quad_setViewport(p, x, y, w, h);
        }

        /// <summary>
        /// Gets the texture coordinates according to a viewport.
        /// </summary>
        /// <param name="out_x">The top-left corner along the x-axis.</param>
        /// <param name="out_y">The top-right corner along the y-axis.</param>
        /// <param name="out_w">The width of the viewport.</param>
        /// <param name="out_h">The height of the viewport.</param>
        public void GetViewport(out float out_x, out float out_y, out float out_w, out float out_h)
        {
            Love2dDll.wrap_love_dll_type_Quad_getViewport(p, out out_x, out out_y, out out_w, out out_h);
        }

        /// <summary>
        /// Gets reference texture dimensions initially specified in love.graphics.newQuad.
        /// </summary>
        /// <returns>The Texture size used by the Quad.</returns>
        public void GetTextureDimensions(out float out_sw, out float out_sh)
        {
            Love2dDll.wrap_love_dll_type_Quad_getTextureDimensions(p, out out_sw, out out_sh);
        }
    }

    /// <summary>
    /// A Shader is used for advanced hardware-accelerated pixel or vertex manipulation. These effects are written in a language based on GLSL (OpenGL Shading Language) with a few things simplified for easier coding.
    /// <para>Potential uses for shaders include HDR/bloom, motion blur, grayscale/invert/sepia/any kind of color effect, reflection/refraction, distortions, bump mapping, and much more! Here is a collection of basic shaders and good starting point to learn: https://github.com/vrld/moonshine </para>
    /// </summary>
    public partial class Shader : LoveObject
    {
        /// <summary>
        /// Use Graphics.NewShader(...) to create Shader !
        /// </summary>
        protected Shader() {}

        // Types of potential uniform (extern) variables used in love's shaders.
        public enum UniformType
        {
            Float,
            Matrix,
            Int,
            Bool,
            Sampler,
            Unknown,
        };

        /// <summary>
        /// Gets warning and error messages (if any).
        /// </summary>
        /// <returns></returns>
        public string GetWarnings()
        {
            IntPtr out_str = IntPtr.Zero;
            Love2dDll.wrap_love_dll_type_Shader_getWarnings(p, out out_str);
            return DllTool.WSToStringAndRelease(out_str);
        }

        /// <summary>
        /// Sends one or more colors to a special (extern / uniform) vec3 or vec4 variable inside the shader. The color components must be in the range of [0, 1]. The colors are gamma-corrected if global gamma-correction is enabled.
        /// </summary>
        /// <param name="name">The name of the color extern variable to send to in the shader. (UTF8 byte array)</param>
        /// <param name="valueArray">A array with red, green, blue, and alpha color components in the range of [0, 1] to send to the extern as a vector.</param>
        public void SendColors(byte[] name, params Vector4[] valueArray)
        {
            Love2dDll.wrap_love_dll_type_Shader_sendColors(p, name, valueArray, valueArray.Length);
        }

        /// <summary>
        /// Sends one or more float values to a special (uniform) variable inside the shader.
        /// </summary>
        /// <param name="name">Name of the float to send to the shader. (UTF8 byte array)</param>
        /// <param name="valueArray">Float to send to store in the uniform variable.</param>
        public void SendFloats(byte[] name, params float[] valueArray)
        {
            Love2dDll.wrap_love_dll_type_Shader_sendFloats(p, name, valueArray, valueArray.Length);
        }

        /// <summary>
        /// Sends one or more uint values to a special (uniform) variable inside the shader.
        /// </summary>
        /// <param name="name">Name of the uint to send to the shader. (UTF8 byte array)</param>
        /// <param name="valueArray">Uint to send to store in the uniform variable.</param>
        public void SendUints(byte[] name, params uint[] valueArray)
        {
            Love2dDll.wrap_love_dll_type_Shader_sendUints(p, name, valueArray, valueArray.Length);
        }

        /// <summary>
        /// Sends one or more int values to a special (uniform) variable inside the shader.
        /// </summary>
        /// <param name="name">Name of the int to send to the shader. (UTF8 byte array)</param>
        /// <param name="valueArray">Int to send to store in the uniform variable.</param>
        public void SendInts(byte[] name, params int[] valueArray)
        {
            Love2dDll.wrap_love_dll_type_Shader_sendInts(p, name, valueArray, valueArray.Length);
        }


        /// <summary>
        /// Sends one or more boolean values to a special (uniform) variable inside the shader.
        /// </summary>
        /// <param name="name">Name of the boolean to send to the shader. (UTF8 byte array)</param>
        /// <param name="valueArray">Boolean to send to store in the uniform variable.</param>
        public void SendBooleans(byte[] name, params bool[] valueArray)
        {
            Love2dDll.wrap_love_dll_type_Shader_sendBooleans(p, name, valueArray, valueArray.Length);
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
        public void SendMatrix(byte[] name, float[] valueArray, int columns, int rows, int count)
        {
            if (valueArray.Length != columns * rows * count)
            {
                throw new Exception("passed params error, columns * rows not equal valueArray.Length");
            }

            Love2dDll.wrap_love_dll_type_Shader_sendMatrices(p, name, valueArray, columns, rows, count);
        }

        /// <summary>
        /// Sends one or more texture to a special (uniform) variable inside the shader.
        /// </summary>
        /// <param name="name">Name of the Texture to send to the shader.(UTF8 byte array)</param>
        /// <param name="texture">Texture (Image or Canvas) to send to the uniform variable.</param>
        public void SendTexture(byte[] name, params Texture[] texture)
        {
            IntPtr[] txts = DllTool.GenIntPtrArray(texture);
            Love2dDll.wrap_love_dll_type_Shader_sendTexture(p, name, txts, txts.Length);
        }
    }

    /// <summary>
    /// Using a single image, draw any number of identical copies of the image using a single call to Love.Graphics.Draw(). This can be used, for example, to draw repeating copies of a single background image with high performance.
    /// <para>A SpriteBatch can be even more useful when the underlying image is a texture atlas (a single image file containing many independent images); by adding Quads to the batch, different sub-images from within the atlas can be drawn.</para>
    /// </summary>
    public partial class SpriteBatch : Drawable
    {
        /// <summary>
        /// disable construct
        /// </summary>
        protected SpriteBatch() {}

        public int Add(float x, float y, float angle = 0, float sx = 1, float sy = 1, float ox = 0, float oy = 0, float kx = 0, float ky = 0)
        {
            int out_index = 0;
            Love2dDll.wrap_love_dll_type_SpriteBatch_add(p, x, y, angle, sx, sy, ox, oy, kx, ky, out out_index);
            return out_index;
        }
        public int Add(Quad quad, float x, float y, float angle = 0, float sx = 1, float sy = 1, float ox = 0, float oy = 0, float kx = 0, float ky = 0)
        {
            int out_index = 0;
            Love2dDll.wrap_love_dll_type_SpriteBatch_add_Quad(p, quad.p, x, y, angle, sx, sy, ox, oy, kx, ky, out out_index);
            return out_index;
        }
        public void Set(int index, float x, float y, float angle = 0, float sx = 1, float sy = 1, float ox = 0, float oy = 0, float kx = 0, float ky = 0)
        {
            Love2dDll.wrap_love_dll_type_SpriteBatch_set(p, index, x, y, angle, sx, sy, ox, oy, kx, ky);
        }
        public void Set(int index, Quad quad, float x, float y, float angle = 0, float sx = 1, float sy = 1, float ox = 0, float oy = 0, float kx = 0, float ky = 0)
        {
            Love2dDll.wrap_love_dll_type_SpriteBatch_set_Quad(p, index, quad.p, x, y, angle, sx, sy, ox, oy, kx, ky);
        }
        public void Clear()
        {
            Love2dDll.wrap_love_dll_type_SpriteBatch_clear(p);
        }
        public void Flush()
        {
            Love2dDll.wrap_love_dll_type_SpriteBatch_flush(p);
        }
        public void SetTexture(Text texture)
        {
            Love2dDll.wrap_love_dll_type_SpriteBatch_setTexture(p, texture.p);
        }
        public Texture GetTexture()
        {
            IntPtr out_texture = IntPtr.Zero;
            Love2dDll.wrap_love_dll_type_SpriteBatch_getTexture(p, out out_texture);
            return NewObject<Texture>(out_texture);
        }
        public void SetColor()
        {
            Love2dDll.wrap_love_dll_type_SpriteBatch_setColor_nil(p);
        }
        public void SetColor(float r, float g, float b, float a = 1)
        {
            Love2dDll.wrap_love_dll_type_SpriteBatch_setColor(p, r, g, b, a);
        }

        // If no color has been set with SpriteBatch:setColor or the current SpriteBatch color has been cleared, this method will return false.
        public Tuple<bool, Vector4> GetColor()
        {
            bool out_exist = false;
            float out_r, out_g, out_b, out_a;
            Love2dDll.wrap_love_dll_type_SpriteBatch_getColor(p, out out_exist, out out_r, out out_g, out out_b, out out_a);
            return new Tuple<bool, Vector4>(out_exist, new Vector4(out_r, out_g, out_b, out_a));
        }
        public int GetCount()
        {
            int out_count = 0;
            Love2dDll.wrap_love_dll_type_SpriteBatch_getCount(p, out out_count);
            return out_count;
        }
        public int GetBufferSize()
        {
            int out_buffersize = 0;
            Love2dDll.wrap_love_dll_type_SpriteBatch_getBufferSize(p, out out_buffersize);
            return out_buffersize;
        }
        public void AttachAttribute(byte[] name, Mesh mesh)
        {
            Love2dDll.wrap_love_dll_type_SpriteBatch_attachAttribute(p, name, mesh.p);
        }
    }

    public partial class Text : Drawable
    {
        /// <summary>
        /// disable construct
        /// </summary>
        protected Text() {}

        public void Set(ColoredStringArray coloredStr)
        {
            coloredStr.ExecResource((tmp) =>{
                Love2dDll.wrap_love_dll_type_Text_set_coloredstring(p, tmp.Item1, tmp.Item2, coloredStr.Length);
            });
        }
        public void Setf(ColoredStringArray coloredStr, float wraplimit, AlignMode align_type)
        {
            coloredStr.ExecResource((tmp) =>{
                Love2dDll.wrap_love_dll_type_Text_setf(p, tmp.Item1, tmp.Item2, coloredStr.Length, wraplimit, (int)align_type);
            });
        }
        public int Add(ColoredStringArray coloredStr, float x, float y, float angle = 0, float sx = 1, float sy = 1, float ox = 0, float oy = 0, float kx = 0, float ky = 0)
        {
            int out_index = 0;
            coloredStr.ExecResource((tmp) =>{
                Love2dDll.wrap_love_dll_type_Text_add(p, tmp.Item1, tmp.Item2, coloredStr.Length, x, y, angle, sx, sy, ox, oy, kx, ky, out out_index);
            });
            return out_index;
        }
        public int Addf(ColoredStringArray coloredStr, float wraplimit, AlignMode align_type, float x, float y, float angle = 0, float sx = 1, float sy = 1, float ox = 0, float oy = 0, float kx = 0, float ky = 0)
        {
            int out_index = 0;
            coloredStr.ExecResource((tmp) => {
                Love2dDll.wrap_love_dll_type_Text_addf(p, tmp.Item1, tmp.Item2, coloredStr.Length, x, y, angle, sx, sy, ox, oy, kx, ky, wraplimit, (int)align_type, out out_index);
            });
            return out_index;
        }
        public void Clear()
        {
            Love2dDll.wrap_love_dll_type_Text_clear(p);
        }
        public void SetFont(Font f)
        {
            Love2dDll.wrap_love_dll_type_Text_setFont(p, f.p);
        }
        public Font GetFont()
        {
            IntPtr font = IntPtr.Zero;
            Love2dDll.wrap_love_dll_type_Text_getFont(p, out font);
            return NewObject<Font>(font);
        }

        /// <summary>
        /// Gets the width of the text in pixels.
        /// </summary>
        /// <param name="index">An index number returned by Text:add or Text:addf.</param>
        /// <returns>The width of the sub-string (before scaling and other transformations).</returns>
        public int GetWidth(int index = 0)
        {
            int out_w = 0;
            Love2dDll.wrap_love_dll_type_Text_getWidth(p, index, out out_w);
            return out_w;
        }

        /// <summary>
        /// Gets the height of the text in pixels.
        /// </summary>
        /// <param name="index">An index number returned by Text:add or Text:addf.</param>
        /// <returns>The height of the sub-string (before scaling and other transformations).</returns>
        public int GetHeight(int index = 0)
        {
            int out_h = 0;
            Love2dDll.wrap_love_dll_type_Text_getHeight(p, index, out out_h);
            return out_h;
        }
    }

    /// <summary>
    /// Superclass for drawable objects which represent a texture. All Textures can be drawn with Quads. This is an abstract type that can't be created directly.
    /// </summary>
    public partial class Texture : Drawable
    {
        /// <summary>
        /// disable construct
        /// </summary>
        protected Texture() {}

        /// <summary>
        /// Sets the mipmap filter mode for a Texture.
        /// <para>Mipmapping(https://en.wikipedia.org/wiki/Mipmap) is useful when drawing a texture at a reduced scale. It can improve performance and reduce aliasing issues.</para>
        /// <para>The texture must be created with the mipmaps flag enabled for the mipmap filter to have any effect. </para>
        /// <remarks>On mobile devices (Android and iOS), the sharpness parameter is not supported and will do nothing. You can use a custom Shader instead, and specify the mipmap sharpness in the optional third parameter to the Texel function in the shader (a negative value makes the texture use a more detailed mipmap level.)</remarks>
        /// </summary>
        /// <param name="mipmap_type">The filter mode to use in between mipmap levels. "nearest" will often give better performance.</param>
        /// <param name="sharpness">A positive sharpness value makes the texture use a more detailed mipmap level when drawing, at the expense of performance. A negative value does the reverse.</param>
        public void SetMipmapFilter(FilterMode mipmap_type, float sharpness)
        {
            Love2dDll.wrap_love_dll_type_Texture_setMipmapFilter(p, (int)mipmap_type, sharpness);
        }

        /// <summary>
        /// Gets the mipmap filter mode for a Texture.
        /// </summary>
        /// <param name="out_mipmap_type">The filter mode used in between mipmap levels. nil if mipmap filtering is not enabled.</param>
        /// <param name="out_sharpness">Value used to determine whether the image should use more or less detailed mipmap levels than normal when drawing.</param>
        public void GetMipmapFilter(out FilterMode out_mipmap_type, out float out_sharpness)
        {
            int out_mipmap_type_int;
            Love2dDll.wrap_love_dll_type_Texture_getMipmapFilter(p, out out_mipmap_type_int, out out_sharpness);
            out_mipmap_type = (FilterMode)out_mipmap_type_int;
        }

        /// <summary>
        /// Gets the width of the Texture.
        /// </summary>
        /// <returns></returns>
        public int GetWidth()
        {
            int out_w;
            Love2dDll.wrap_love_dll_type_Texture_getWidth(p, out out_w);
            return out_w;
        }

        /// <summary>
        /// Gets the height of the Texture.
        /// </summary>
        /// <returns></returns>
        public int GetHeight()
        {
            int out_h;
            Love2dDll.wrap_love_dll_type_Texture_getHeight(p, out out_h);
            return out_h;
        }

        /// <summary>
        /// Sets the filter mode of the Texture.
        /// </summary>
        /// <param name="filtermin_type">Filter mode to use when minifying the texture (rendering it at a smaller size on-screen than its size in pixels).</param>
        /// <param name="filtermag_type">Filter mode to use when magnifying the texture (rendering it at a larger size on-screen than its size in pixels).</param>
        /// <param name="anisotropy">Maximum amount of anisotropic filtering to use.</param>
        public void SetFilter(FilterMode filtermin_type, FilterMode filtermag_type, float anisotropy = 1)
        {
            Love2dDll.wrap_love_dll_type_Texture_setFilter(p, (int)filtermin_type, (int)filtermag_type, anisotropy);
        }

        /// <summary>
        /// Gets the filter mode of the Texture.
        /// </summary>
        /// <param name="out_filtermin_type">Filter mode to use when minifying the texture (rendering it at a smaller size on-screen than its size in pixels).</param>
        /// <param name="out_filtermag_type">Filter mode to use when magnifying the texture (rendering it at a smaller size on-screen than its size in pixels).</param>
        /// <param name="out_anisotropy">Maximum amount of anisotropic filtering used.</param>
        public void GetFilter(out FilterMode out_filtermin_type, out FilterMode out_filtermag_type, out float out_anisotropy)
        {
            int out_filtermin = 0, out_filtermag = 0;
            Love2dDll.wrap_love_dll_type_Texture_getFilter(p, out out_filtermin, out out_filtermag, out out_anisotropy);

            out_filtermag_type = (FilterMode)out_filtermag;
            out_filtermin_type = (FilterMode)out_filtermin;
        }

        /// <summary>
        /// Sets the wrapping properties of a Texture.
        /// <para>This function sets the way a Texture is repeated when it is drawn with a Quad that is larger than the texture's extent, or when a custom Shader is used which uses texture coordinates outside of [0, 1]. A texture may be clamped or set to repeat in both horizontal and vertical directions.</para>
        /// <para>Clamped textures appear only once (with the edges of the texture stretching to fill the extent of the Quad), whereas repeated ones repeat as many times as there is room in the Quad.</para>
        /// </summary>
        /// <param name="wraphoriz_type">Horizontal wrapping mode of the texture.</param>
        /// <param name="wrapvert_type">Vertical wrapping mode of the texture.</param>
        public void SetWrap(WrapMode wraphoriz_type, WrapMode wrapvert_type)
        {
            Love2dDll.wrap_love_dll_type_Texture_setWrap(p, (int)wraphoriz_type, (int)wrapvert_type);
        }

        /// <summary>
        /// Gets the wrapping properties of a Texture.
        /// <para>This function returns the currently set horizontal and vertical wrapping modes for the texture.</para>
        /// </summary>
        /// <param name="out_wraphoriz_type">Horizontal wrapping mode of the texture.</param>
        /// <param name="out_wrapvert_type">Vertical wrapping mode of the texture.</param>
        public void GetWrap(out WrapMode out_wraphoriz_type, out WrapMode out_wrapvert_type)
        {
            int out_wraphoriz = 0, out_wrapvert = 0;
            Love2dDll.wrap_love_dll_type_Texture_getWrap(p, out out_wraphoriz, out out_wrapvert);
            out_wraphoriz_type = (WrapMode)out_wraphoriz;
            out_wrapvert_type = (WrapMode)out_wrapvert;
        }
    }

    public partial class Video : Drawable
    {
        /// <summary>
        /// disable construct
        /// </summary>
        protected Video() {}

        public VideoStream GetStream()
        {
            IntPtr out_videsStream = IntPtr.Zero;
            Love2dDll.wrap_love_dll_type_Video_getStream(p, out out_videsStream);
            return NewObject<VideoStream>(out_videsStream);
        }
        public Source GetSource()
        {
            IntPtr out_source = IntPtr.Zero;
            Love2dDll.wrap_love_dll_type_Video_getSource(p, out out_source);
            return NewObject<Source>(out_source);
        }

        public void SetSource(Source source = null)
        {
            if (source == null)
            {
                Love2dDll.wrap_love_dll_type_Video_setSource_nil(p);
                Love2dDll.wrap_love_dll_type_VideoStream_setSync(GetStream().p, IntPtr.Zero);
            }
            else
            {
                Love2dDll.wrap_love_dll_type_Video_setSource(p, source.p);
                Love2dDll.wrap_love_dll_type_VideoStream_setSync(GetStream().p, source.p);
            }
        }
        public int GetWidth()
        {
            int out_w = 0;
            Love2dDll.wrap_love_dll_type_Video_getWidth(p, out out_w);
            return out_w;
        }
        public int GetHeight()
        {
            int out_h = 0;
            Love2dDll.wrap_love_dll_type_Video_getHeight(p, out out_h);
            return out_h;
        }
        public void SetFilter(FilterMode filtermin_type, FilterMode filtermag_type, float anisotropy)
        {
            Love2dDll.wrap_love_dll_type_Video_setFilter(p, (int)filtermin_type, (int)filtermag_type, anisotropy);
        }
        public void GetFilter(out FilterMode out_filtermin_type, out FilterMode out_filtermag_type, out float out_anisotropy)
        {
            int out_filtermin = 0, out_filtermag = 0;
            Love2dDll.wrap_love_dll_type_Video_getFilter(p, out out_filtermin, out out_filtermag, out out_anisotropy);
            out_filtermin_type = (FilterMode)out_filtermin;
            out_filtermag_type = (FilterMode)out_filtermag;
        }
    }

    public partial class ImageDataBase : Data
    {
        /// <summary>
        /// disable construct
        /// </summary>
        protected ImageDataBase() {}
    }

    public partial class CompressedImageData : ImageDataBase
    {
        /// <summary>
        /// disable construct
        /// </summary>
        protected CompressedImageData() { }

        public int GetWidth(int miplevel)
        {
            int out_w = 0;
            Love2dDll.wrap_love_dll_type_CompressedImageData_getWidth(p, miplevel, out out_w);
            return out_w;
        }
        public int GetHeight(int miplevel)
        {
            int out_h = 0;
            Love2dDll.wrap_love_dll_type_CompressedImageData_getHeight(p, miplevel, out out_h);
            return out_h;
        }
        public int GetMipmapCount()
        {
            int out_count = 0;
            Love2dDll.wrap_love_dll_type_CompressedImageData_getMipmapCount(p, out out_count);
            return out_count;
        }
        public PixelFormat GetFormat()
        {
            int out_format_type = 0;
            Love2dDll.wrap_love_dll_type_CompressedImageData_getFormat(p, out out_format_type);
            return (PixelFormat)out_format_type;
        }
    }

    public partial class ImageData : ImageDataBase
    {
        /// <summary>
        /// disable construct
        /// </summary>
        protected ImageData() { }

        /// <summary>
        /// Gets the width of the ImageData in pixels.	
        /// </summary>
        /// <returns></returns>
        public int GetWidth()
        {
            int out_w = 0;
            Love2dDll.wrap_love_dll_type_ImageData_getWidth(p, out out_w);
            return out_w;
        }

        /// <summary>
        /// Gets the height of the ImageData in pixels.	
        /// </summary>
        /// <returns></returns>
        public int GetHeight()
        {
            int out_h = 0;
            Love2dDll.wrap_love_dll_type_ImageData_getHeight(p, out out_h);
            return out_h;
        }

        /// <summary>
        /// <para>Gets the color of a pixel at a specific position in the image.</para>
        /// </summary>
        /// <param name="x">The position of the pixel on the x-axis.</param>
        /// <param name="y">The position of the pixel on the y-axis.</param>
        /// <returns></returns>
        public Pixel GetPixel(int x, int y)
        {
            Pixel out_pixel;
            Love2dDll.wrap_love_dll_type_ImageData_getPixel(p, x, y, out out_pixel);
            return out_pixel;
        }

        /// <summary>
        /// <para>Sets the color of a pixel at a specific position in the image. </para>
        /// </summary>
        /// <param name="x">The position of the pixel on the x-axis.</param>
        /// <param name="y">The position of the pixel on the y-axis.</param>
        /// <param name="pixel"></param>
        public void SetPixel(int x, int y, Pixel pixel)
        {
            Love2dDll.wrap_love_dll_type_ImageData_setPixel(p, x, y, pixel);
        }

        /// <summary>
        /// Gets the pixel format of the ImageData.
        /// </summary>
        /// <returns></returns>
        public PixelFormat GetFormat()
        {
            int format;
            Love2dDll.wrap_love_dll_type_ImageData_GetFormat(p, out format);
            return (PixelFormat)format;
        }

        /// <summary>
        /// Paste into ImageData from another source ImageData.
        /// </summary>
        /// <param name="src_imageData">Source ImageData from which to copy.</param>
        /// <param name="dx">Destination top-left position on x-axis.</param>
        /// <param name="dy">Destination top-left position on y-axis.</param>
        /// <param name="sx">Source top-left position on x-axis.</param>
        /// <param name="sy">Source top-left position on y-axis.</param>
        /// <param name="sw">Source width.</param>
        /// <param name="sh">Source height.</param>
        public void Paste(ImageData src_imageData, int dx, int dy, int sx, int sy, int sw, int sh)
        {
            Love2dDll.wrap_love_dll_type_ImageData_paste(p, src_imageData.p, dx, dy, sx, sy, sw, sh);
        }

        /// <summary>
        /// Encodes the ImageData and optionally writes it to the save directory.
        /// </summary>
        /// <param name="format_type">The format to encode the image as.</param>
        /// <param name="writeToFile">Whether to write to the specified file</param>
        /// <param name="filename">The filename to write the file to. If null, no file will be written but the FileData will still be returned.</param>
        /// <returns></returns>
        public FileData Encode(ImageFormat format_type, bool writeToFile, byte[] filename)
        {
            IntPtr out_fileData = IntPtr.Zero;
            Love2dDll.wrap_love_dll_type_ImageData_encode(p, (int)format_type, writeToFile, filename, out out_fileData);
            return NewObject<FileData>(out_fileData);
        }
    }

    public partial class Cursor : LoveObject
    {
        /// <summary>
        /// disable construct
        /// </summary>
        protected Cursor() { }

        /// <summary>
        /// Gets the type of the Cursor.
        /// </summary>
        /// <returns>The type of the Cursor.</returns>
        new public Tuple<CursorType, SystemCursor> GetType()
        {
            int out_cursortype_type = 0;
            bool out_custom = false;
            Love2dDll.wrap_love_dll_type_Cursor_getType(p, out out_cursortype_type, out out_custom);
            if (out_custom)
            {
                return Tuple.Create(CursorType.Custom, SystemCursor.Arrow);
            }

            return Tuple.Create(CursorType.System, (SystemCursor)out_cursortype_type);
        }
    }

    public partial class Decoder : LoveObject
    {
        /// <summary>
        /// disable construct
        /// </summary>
        protected Decoder() { }

        /// <summary>
        /// Indicates how many bytes of raw data should be generated at each call to Decode.
        /// </summary>
        public const int DEFAULT_BUFFER_SIZE = 16384;

        /// <summary>
        /// Indicates the quality of the sound.
        /// </summary>
        public const int DEFAULT_SAMPLE_RATE = 44100;

        /// <summary>
        /// Default is stereo.
        /// </summary>
        public const int DEFAULT_CHANNELS = 2;

        /// <summary>
        /// 16 bit audio is the default.
        /// </summary>
        public const int DEFAULT_BIT_DEPTH = 16;


        public int GetChannelCount()
        {
            int out_channels = 0;
            Love2dDll.wrap_love_dll_type_Decoder_getChannelCount(p, out out_channels);
            return out_channels;
        }
        public int GetBitDepth()
        {
            int out_bitDepth = 0;
            Love2dDll.wrap_love_dll_type_Decoder_getBitDepth(p, out out_bitDepth);
            return out_bitDepth;
        }
        public int GetSampleRate()
        {
            int out_sampleRate = 0;
            Love2dDll.wrap_love_dll_type_Decoder_getSampleRate(p, out out_sampleRate);
            return out_sampleRate;
        }
        public double GetDuration()
        {
            double out_duration = 0;
            Love2dDll.wrap_love_dll_type_Decoder_getDuration(p, out out_duration);
            return out_duration;
        }
    }

    public partial class SoundData : Data
    {
        /// <summary>
        /// disable construct
        /// </summary>
        protected SoundData() { }

        public int GetChannelCount()
        {
            int out_channels = 0;
            Love2dDll.wrap_love_dll_SoundData_getChannelCount(p, out out_channels);
            return out_channels;
        }
        public int GetBitDepth()
        {
            int out_bitDepth = 0;
            Love2dDll.wrap_love_dll_SoundData_getBitDepth(p, out out_bitDepth);
            return out_bitDepth;
        }
        public int GetSampleRate()
        {
            int out_sampleRate = 0;
            Love2dDll.wrap_love_dll_SoundData_getSampleRate(p, out out_sampleRate);
            return out_sampleRate;
        }
        public int GetSampleCount()
        {
            int out_sampleCount = 0;
            Love2dDll.wrap_love_dll_SoundData_getSampleCount(p, out out_sampleCount);
            return out_sampleCount;
        }
        public double GetDuration()
        {
            double out_duration = 0;
            Love2dDll.wrap_love_dll_SoundData_getDuration(p, out out_duration);
            return out_duration;
        }
        public void SetSample(int i, float sample)
        {
            Love2dDll.wrap_love_dll_SoundData_setSample(p, i, sample);
        }
        public float GetSample(int i)
        {
            float out_sample = 0;
            Love2dDll.wrap_love_dll_SoundData_getSample(p, i, out out_sample);
            return out_sample;
        }
    }

    public partial class VideoStream : Stream
    {
        /// <summary>
        /// disable construct
        /// </summary>
        protected VideoStream() { }

        public string GetFilename()
        {
            IntPtr out_filename = IntPtr.Zero;
            Love2dDll.wrap_love_dll_type_VideoStream_getFilename(p, out out_filename);
            return DllTool.WSToStringAndRelease(out_filename);
        }
        public void Play()
        {
            Love2dDll.wrap_love_dll_type_VideoStream_play(p);
        }
        public void Pause()
        {
            Love2dDll.wrap_love_dll_type_VideoStream_pause(p);
        }
        public void Seek(double offset)
        {
            Love2dDll.wrap_love_dll_type_VideoStream_seek(p, offset);
        }
        public void Rewind()
        {
            Love2dDll.wrap_love_dll_type_VideoStream_rewind(p);
        }
        public double Tell()
        {
            double out_position = 0;
            Love2dDll.wrap_love_dll_type_VideoStream_tell(p, out out_position);
            return out_position;
        }
        public bool IsPlaying()
        {
            bool out_isplaying = false;
            Love2dDll.wrap_love_dll_type_VideoStream_isPlaying(p, out out_isplaying);
            return out_isplaying;
        }
    }

    public partial class BezierCurve : LoveObject
    {
        /// <summary>
        /// disable construct
        /// </summary>
        protected BezierCurve() { }

        public int GetDegree()
        {
            int out_degree = 0;
            Love2dDll.wrap_love_dll_type_BezierCurve_getDegree(p, out out_degree);
            return out_degree;
        }
        public BezierCurve GetDerivative()
        {
            IntPtr out_deriv = IntPtr.Zero;
            Love2dDll.wrap_love_dll_type_BezierCurve_getDerivative(p, out out_deriv);
            return NewObject<BezierCurve>(out_deriv);
        }
        public Vector2 GetControlPoint(int idx)
        {
            float out_x, out_y;
            Love2dDll.wrap_love_dll_type_BezierCurve_getControlPoint(p, idx, out out_x, out out_y);
            return new Vector2(out_x, out_y);
        }
        public void SetControlPoint(int idx, float x, float y)
        {
            Love2dDll.wrap_love_dll_type_BezierCurve_setControlPoint(p, idx, x, y);
        }
        public void InsertControlPoint(int idx, float x, float y)
        {
            Love2dDll.wrap_love_dll_type_BezierCurve_insertControlPoint(p, idx, x, y);
        }
        public void RemoveControlPoint(int idx)
        {
            Love2dDll.wrap_love_dll_type_BezierCurve_removeControlPoint(p, idx);
        }
        public int GetControlPointCount()
        {
            int out_count = 0;
            Love2dDll.wrap_love_dll_type_BezierCurve_getControlPointCount(p, out out_count);
            return out_count;
        }
        public void Translate(float dx, float dy)
        {
            Love2dDll.wrap_love_dll_type_BezierCurve_translate(p, dx, dy);
        }
        public void Rotate(double phi, float ox, float oy)
        {
            Love2dDll.wrap_love_dll_type_BezierCurve_rotate(p, phi, ox, oy);
        }
        public void Scale(double s, float ox, float oy)
        {
            Love2dDll.wrap_love_dll_type_BezierCurve_scale(p, s, ox, oy);
        }
        public Vector2 Evaluate(double t)
        {
            float out_x, out_y;
            Love2dDll.wrap_love_dll_type_BezierCurve_evaluate(p, t, out out_x, out out_y);
            return new Vector2(out_x, out_y);
        }
        public BezierCurve GetSegment(double t1, double t2)
        {
            IntPtr out_segment = IntPtr.Zero;
            Love2dDll.wrap_love_dll_type_BezierCurve_getSegment(p, t1, t2, out out_segment);
            return NewObject<BezierCurve>(out_segment);
        }
        public Vector2[] Render(int accuracy)
        {
            IntPtr out_points;
            int out_points_lenght;
            Love2dDll.wrap_love_dll_type_BezierCurve_render(p, accuracy, out out_points, out out_points_lenght);
            return DllTool.ReadVector2sAndRelease(out_points, out_points_lenght);
        }
        public Vector2[] RenderSegment(double start, double end, int accuracy)
        {
            IntPtr out_points;
            int out_points_lenght;
            Love2dDll.wrap_love_dll_type_BezierCurve_renderSegment(p, start, end, accuracy, out out_points, out out_points_lenght);
            return DllTool.ReadVector2sAndRelease(out_points, out_points_lenght);
        }
    }

    public partial class RandomGenerator : LoveObject
    {
        /// <summary>
        /// disable construct
        /// </summary>
        protected RandomGenerator() { }

        public float Random()
        {
            double out_result = 0;
            Love2dDll.wrap_love_dll_type_RandomGenerator_random(p, out out_result);
            return (float)out_result;
        }

        /// <summary>
        /// Get uniformly distributed pseudo-random integer within [min, max].
        /// </summary>
        /// <param name="min">The minimum possible value it should return.</param>
        /// <param name="max">The maximum possible value it should return.</param>
        /// <returns></returns>
        public int Random(int min, int max)
        {
            float random = Random();
            return Mathf.RoundToInt(random * (max - min) + min);
        }

        /// <summary>
        /// Get uniformly distributed pseudo-random integer within [min, max].
        /// </summary>
        /// <param name="min">The minimum possible value it should return.</param>
        /// <param name="max">The maximum possible value it should return.</param>
        /// <returns></returns>
        public float Random(float min, float max)
        {
            float random = Random();
            return random * (max - min) + min;
        }

        public float RandomNormal(double stddev, double mean)
        {
            double out_result = 0;
            Love2dDll.wrap_love_dll_type_RandomGenerator_randomNormal(p, stddev, mean, out out_result);
            return (float)out_result;
        }
        public void SetSeed(uint low, uint high)
        {
            Love2dDll.wrap_love_dll_type_RandomGenerator_setSeed(p, low, high);
        }
        public void GetSeed(out uint out_low, out uint out_high)
        {
            Love2dDll.wrap_love_dll_type_RandomGenerator_getSeed(p, out out_low, out out_high);
        }
        public void SetState(byte[] state)
        {
            Love2dDll.wrap_love_dll_type_RandomGenerator_setState(p, state);
        }
        public string GetState()
        {
            IntPtr out_str;
            Love2dDll.wrap_love_dll_type_RandomGenerator_getState(p, out out_str);
            return DllTool.WSToStringAndRelease(out_str);
        }
    }

    public partial class Joystick : LoveObject
    {
        /// <summary>
        /// disable construct
        /// </summary>
        protected Joystick() { }

        //// Different types of inputs for a joystick.
        //public enum InputType
        //{
        //    Axis,
        //    Button,
        //    Hat,
        //};

        public bool IsConnected()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_type_Joystick_isConnected(p, out out_result);
            return out_result;
        }
        public string GetName()
        {
            IntPtr out_str = IntPtr.Zero;
            Love2dDll.wrap_love_dll_type_Joystick_getName(p, out out_str);
            return DllTool.WSToStringAndRelease(out_str);
        }
        public int GetID()
        {
            int out_id = 0;
            Love2dDll.wrap_love_dll_type_Joystick_getID(p, out out_id);
            return out_id;
        }
        public int GetInstanceID()
        {
            int out_instanceid = 0;
            Love2dDll.wrap_love_dll_type_Joystick_getInstanceID(p, out out_instanceid);
            return out_instanceid;
        }
        public string GetGUID()
        {
            IntPtr out_str = IntPtr.Zero;
            Love2dDll.wrap_love_dll_type_Joystick_getGUID(p, out out_str);
            return DllTool.WSToStringAndRelease(out_str);
        }
        public int GetAxisCount()
        {
            int out_count = 0;
            Love2dDll.wrap_love_dll_type_Joystick_getAxisCount(p, out out_count);
            return out_count;
        }
        public int GetButtonCount()
        {
            int out_count = 0;
            Love2dDll.wrap_love_dll_type_Joystick_getButtonCount(p, out out_count);
            return out_count;
        }
        public int GetHatCount()
        {
            int out_count = 0;
            Love2dDll.wrap_love_dll_type_Joystick_getHatCount(p, out out_count);
            return out_count;
        }
        public float GetAxis(int axisindex)
        {
            float out_axis = 0;
            Love2dDll.wrap_love_dll_type_Joystick_getAxis(p, axisindex, out out_axis);
            return out_axis;
        }
        public float[] GetAxes()
        {
            IntPtr out_axes;
            int out_axes_length;
            Love2dDll.wrap_love_dll_type_Joystick_getAxes(p, out out_axes, out out_axes_length);
            return DllTool.ReadFloatsAndRelease(out_axes, out_axes_length);
        }
        public JoystickHat GetHat(int hatindex)
        {
            int out_hat_type = 0;
            Love2dDll.wrap_love_dll_type_Joystick_getHat(p, hatindex, out out_hat_type);
            return (JoystickHat)out_hat_type;
        }
        public bool IsDown(int button)
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_type_Joystick_isDown(p, button, out out_result);
            return out_result;
        }
        public bool IsGamepad()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_type_Joystick_isGamepad(p, out out_result);
            return out_result;
        }
        public float GetGamepadAxis(GamepadAxis axis_type)
        {
            float out_gamepadaxis = 0;
            Love2dDll.wrap_love_dll_type_Joystick_getGamepadAxis(p, (int)axis_type, out out_gamepadaxis);
            return out_gamepadaxis;
        }
        public bool IsGamepadDown(GamepadButton gamepadButton_type)
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_type_Joystick_isGamepadDown(p, (int)gamepadButton_type, out out_result);
            return out_result;
        }
        public bool IsVibrationSupported()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_type_Joystick_isVibrationSupported(p, out out_result);
            return out_result;
        }
        public bool SetVibration()
        {
            bool out_success = false;
            Love2dDll.wrap_love_dll_type_Joystick_setVibration_nil(p, out out_success);
            return out_success;
        }
        public bool SetVibration(float left, float right, float duration)
        {
            bool out_success = false;
            Love2dDll.wrap_love_dll_type_Joystick_setVibration(p, left, right, duration, out out_success);
            return out_success;
        }
        public void GetVibration(out float out_left, out float out_right)
        {
            Love2dDll.wrap_love_dll_type_Joystick_getVibration(p, out out_left, out out_right);
        }
    }

    public partial class Data : LoveObject
    {
        /// <summary>
        /// disable construct
        /// </summary>
        protected Data() { }

        /// <summary>
        /// Gets the Data's size in bytes.
        /// </summary>
        /// <returns></returns>
        public uint GetSize()
        {
            uint datasize;
            Love2dDll.wrap_love_dll_type_Data_getSize(p, out datasize);
            return datasize;
        }

        /// <summary>
        /// Gets a pointer to the Data.
        /// <para>Use at your own risk. Directly reading from and writing to the raw memory owned by the Data will bypass any safety checks and thread-safety the Data might normally have.</para>
        /// </summary>
        /// <returns></returns>
        public IntPtr GetPointer()
        {
            IntPtr out_pointer;
            Love2dDll.wrap_love_dll_type_Data_getPointer(p, out out_pointer);
            return out_pointer;
        }


        /// <summary>
        /// Get file data full byte[]
        /// <para>This function can be slow if it is called repeatedly, such as from <see cref="Scene.Update(float)"/> or <see cref="Scene.Draw"/> . If you need to use a specific resource often, create it once and store it somewhere it can be reused!</para>
        /// </summary>
        /// <returns></returns>
        public byte[] GetBytes()
        {
            var byteSize = (int)GetSize();
            byte[] data = new byte[byteSize];
            IntPtr pointer = GetPointer();
            Marshal.Copy(pointer, data, 0, byteSize);
            return data;
        }
    }

    public partial class TrueTypeRasterizer : LoveObject
    {
        /// <summary>
        /// disable construct
        /// </summary>
        protected TrueTypeRasterizer() { }
    }
    public partial class Drawable : LoveObject
    {
        /// <summary>
        /// disable construct
        /// </summary>
        protected Drawable() { }

    }
    public partial class DroppedFile : File
    {
        /// <summary>
        /// disable construct
        /// </summary>
        protected DroppedFile() { }
    }
    public partial class Stream : LoveObject
    {
        /// <summary>
        /// disable construct
        /// </summary>
        protected Stream() { }
    }
}


namespace Love
{

    public class ColoredString
    {
        public readonly string text;
        public readonly Vector4 color;
        public ColoredString(string text, Vector4 color)
        {
            this.text = text;
            this.color = color;
        }

        /// <summary>
        /// Create ColoredString form text and color
        /// </summary>
        /// <param name="text"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static ColoredString Create(string text, Vector4 color)
        {
            return new ColoredString(text, color);
        }

        /// <summary>
        /// Create ColoredString form text and color
        /// </summary>
        /// <param name="text"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static ColoredString Create(string text, float r, float g, float b, float a = 1)
        {
            return Create(text, new Vector4(r, g, b, a));
        }
    }

    public struct ColoredStringArray
    {
        /// <summary>
        /// Create white Color text
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static ColoredStringArray Create(string text)
        {
            return new ColoredStringArray(ColoredString.Create(text, new Vector4(1, 1, 1, 1)));
        }

        public readonly string[] texts;
        public readonly Vector4[] colors;

        public int Length
        {
            get { return texts.Length; }
        }

        public ColoredStringArray(params ColoredString[] inputItems)
        {
            texts = new string[inputItems.Length];
            colors = new Vector4[inputItems.Length];
            for (int i = 0; i < inputItems.Length; i++)
            {
                texts[i] = inputItems[i].text;
                colors[i] = inputItems[i].color;
            }
        }

        public ColoredStringArray(string[] texts, Vector4[] colors)
        {
            if (texts.Length != colors.Length)
            {
                throw new Exception("lenght of texts and colors must be same");
            }

            this.texts = texts;
            this.colors = colors;
        }

        public void ExecResource(Action<Tuple<IntPtr[], Vector4[]>> func)
        {
            if (func != null)
            {
                var that = this;
                DllTool.ExecuteNullTailStringArray(texts, (pointers) =>
                {
                    func(Tuple.Create(pointers, that.colors));
                });
            }
        }
    }


}