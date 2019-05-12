
using size_t = System.UInt32;
using int64 = System.Int64;
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;

namespace Love
{
    [StructLayout(LayoutKind.Explicit)]
    internal struct FloatIntMarshal
    {
        [FieldOffset(0)] public float floatValue;
        [FieldOffset(0)] public int intValue;

        public FloatIntMarshal(float v)
        {
            this.intValue = 0;
            this.floatValue = v;
        }

        public static int Convert(float v)
        {
            return new FloatIntMarshal(v).intValue;
        }
    }

    internal class HashHelpers
    {
        public static int Combine(float a, float b)
        {
            unchecked
            {
                return a.GetHashCode() ^ b.GetHashCode();
            }
        }

        public static int Combine(float a, float b, float c)
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = (int)2166136261;
                // Suitable nullity checks etc, of course :)
                hash = (hash * 16777619) ^ a.GetHashCode();
                hash = (hash * 16777619) ^ b.GetHashCode();
                hash = (hash * 16777619) ^ c.GetHashCode();
                return hash;
            }
        }

        public static int Combine(float a, float b, float c, float d)
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = (int)2166136261;
                // Suitable nullity checks etc, of course :)
                hash = (hash * 16777619) ^ a.GetHashCode();
                hash = (hash * 16777619) ^ b.GetHashCode();
                hash = (hash * 16777619) ^ c.GetHashCode();
                hash = (hash * 16777619) ^ d.GetHashCode();
                return hash;
            }
        }


        public static int Combine(int a, int b, int c, int d)
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = (int)2166136261;
                // Suitable nullity checks etc, of course :)
                hash = (hash * 16777619) ^ a;
                hash = (hash * 16777619) ^ b;
                hash = (hash * 16777619) ^ c;
                hash = (hash * 16777619) ^ d;
                return hash;
            }
        }
    }

    public class FileInfo
    {
        /// <summary>
        /// Numbers will be -1 if they cannot be determined.
        /// </summary>
        public readonly int64 Size;

        /// <summary>
        /// The file's last modification time in seconds since the unix epoch, or nil if it can't be determined.
        /// </summary>
        public readonly int64 ModifyTime;

        /// <summary>
        /// The type of the object at the path (file, directory, symlink, etc.)
        /// </summary>
        public readonly FileType Type;

        public FileInfo(int64 size, int64 modifyTime, FileType type)
        {
            Size = size;
            ModifyTime = modifyTime;
            Type = type;
        }

        public static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return origin.AddSeconds(timestamp);
        }

        public static double ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalSeconds);
        }

        public override string ToString()
        {
            var time = ConvertFromUnixTimestamp(ModifyTime);
            return $"size: {string.Format("{0:0,00}", Size)}, modify-time: {time}, type: {Type}";
        }
    };

    /// <summary>
    /// 窗口属性
    /// </summary>
    public class WindowSettings
    {

        /// <summary>
        /// Fullscreen (true), or windowed (false).
        /// </summary>
        public bool Fullscreen
        {
            get { return fullscreen.GetValueOrDefault(false); }
            set { fullscreen = value; }
        }
        internal bool? fullscreen;

        /// <summary>
        /// Choose between "DeskTop" fullscreen or "Exclusive" fullscreen mode 
        /// </summary>
        public FullscreenType FullscreenType
        {
            get { return fullscreenType.GetValueOrDefault(FullscreenType.DeskTop); }
            set { fullscreenType = value; }
        }
        public FullscreenType? fullscreenType;

        /// <summary>
        /// True if LÖVE should wait for vsync, false otherwise.
        /// </summary>
        public bool Vsync
        {
            get { return vsync.GetValueOrDefault(true); }
            set { vsync = value; }
        }
        public bool? vsync;

        /// <summary>
        /// The number of antialiasing samples.
        /// </summary>
        public int MSAA
        {
            get { return msaa.GetValueOrDefault(0); }
            set { msaa = value; }
        }
        public int? msaa;

        /// <summary>
        /// Whether a stencil buffer should be allocated. If true, the stencil buffer will have 8 bits.
        /// </summary>
        public bool Stencil
        {
            get { return stencil.GetValueOrDefault(true); }
            set { stencil = value; }
        }
        public bool? stencil;

        /// <summary>
        /// The number of bits in the depth buffer.
        /// </summary>
        public int Depth
        {
            get { return depth.GetValueOrDefault(0); }
            set { depth = value; }
        }
        public int? depth;

        /// <summary>
        /// Let the window be user-resizable
        /// </summary>
        public bool Resizable
        {
            get { return resizable.GetValueOrDefault(false); }
            set { resizable = value; }
        }
        public bool? resizable;

        /// <summary>
        /// Remove all border visuals from the window
        /// </summary>
        public bool Borderless
        {
            get { return borderless.GetValueOrDefault(false); }
            set { borderless = value; }
        }
        public bool? borderless;

        /// <summary>
        /// True if the window should be centered.
        /// </summary>
        public bool Centered
        {
            get { return centered.GetValueOrDefault(true); }
            set { centered = value; usePosition = false; }
        }
        public bool? centered;

        /// <summary>
        /// The index of the display to show the window in, if multiple monitors are available.
        /// </summary>
        public int Display
        {
            get { return depth.GetValueOrDefault(0); }
            set { depth = value; }
        }
        public int? display;

        /// <summary>
        /// True if high-dpi mode should be used on Retina displays in macOS and iOS. Does nothing on non-Retina displays. Added in 0.9.1.
        /// </summary>
        public bool HighDpi
        {
            get { return highDpi.GetValueOrDefault(false); }
            set { highDpi = value; }
        }
        public bool? highDpi;

        /// <summary>
        /// The minimum width of the window, if it's resizable. Cannot be less than 1.
        /// </summary>
        public int MinWidth
        {
            get { return minWidth.GetValueOrDefault(1); }
            set { minWidth = value; }
        }

        /// <summary>
        /// The minimum height of the window, if it's resizable. Cannot be less than 1.
        /// </summary>
        public int MinHeight
        {
            get { return minHeight.GetValueOrDefault(1); }
            set { minHeight = value; }
        }
        public int? minWidth, minHeight;

        /// <summary>
        /// True if use the position params, false otherwise.
        /// </summary>
        public bool UsePosition
        {
            get { return usePosition && x.HasValue && y.HasValue; }
        }
        public bool usePosition = false;

        /// <summary>
        /// The x-coordinate of the window's position in the specified display. Added in 0.9.2.
        /// </summary>
        public int X
        {
            get { return x.GetValueOrDefault(0); }
            set { x = value; usePosition = true; }
        }
        public int Y
        {
            get { return y.GetValueOrDefault(0); }
            set { y = value; usePosition = true; }
        }
        public int? x, y;

        /// <summary>
        /// We don't explicitly set the refresh rate, it's "read-only".
        /// <para>The refresh rate of the screen's current display mode, in Hz. May be 0 if the value can't be determined. Added in 0.9.2.</para>
        /// </summary>
        public double Refreshrate
        {
            get { return refreshrate.GetValueOrDefault(0); }
            set { refreshrate = value; }
        }
        public double? refreshrate;
    }

    public class MeshAttribFormat
    {
        public readonly string name;
        public readonly VertexDataType type;
        public readonly int componentCount;

        public MeshAttribFormat(string name, VertexDataType type, int componentCount)
        {
            if (!(0 <= componentCount && componentCount <= 4))
            {
                throw new ArgumentOutOfRangeException("componentCount should <= 4");
            }

            this.name = name;
            this.type = type;
            this.componentCount = componentCount;

        }
    }

    public class RenderTarget
    {
        readonly public Canvas canvas;

        readonly public int slice;
        readonly public int mipmap;

        public RenderTarget(Canvas canvas, int slice = 0, int mipmap = 0)
        {
            this.canvas = canvas;
            this.slice = slice;
            this.mipmap = mipmap;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="slice">For cubemaps this is the cube face index to render to (between 1 and 6). For Array textures this is the array layer. For volume textures this is the depth slice. 2D canvases should use a value of 1.</param>
        /// <param name="mipmap">The mipmap level to render to, for Canvases with mipmaps.</param>
        /// <returns></returns>
        public static RenderTarget FromCanvas(Canvas canvas, int slice = 0, int mipmap = 0)
        {
            return new RenderTarget(canvas, slice, mipmap);
        }
    }

    public class RenderTargetInfo
    {
        public List<Canvas> CanvasList => renderTargetList.Select(item => item.canvas).ToList();
        public List<RenderTarget> RenderTargetList => renderTargetList;
        public RenderTarget DepthStencil => depthStencil;

        readonly List<RenderTarget> renderTargetList = new List<RenderTarget>();
        readonly RenderTarget depthStencil;

        /// <summary>
        /// for enabling internal depth and stencil buffers if 'depthstencil' isn't used.
        /// </summary>
        public bool tempDepthFlag, tempStencilFlag;

        public RenderTargetInfo(List<RenderTarget> list, RenderTarget depthStencil, bool tempDepthFlag = false, bool tempStencilFlag = false)
        {
            this.renderTargetList = list;
            this.depthStencil = depthStencil;
            this.tempDepthFlag = tempDepthFlag;
            this.tempStencilFlag = tempStencilFlag;
        }

        public void SetRenderTagets(IEnumerable<RenderTarget> rts)
        {
            renderTargetList.Clear();
            renderTargetList.AddRange(rts);
        }

        public static RenderTargetInfo CreateWithDepthStencil(Canvas depthStencil, params Canvas[] renderTagets)
        {
            return CreateWithDepthStencil(new RenderTarget(depthStencil), renderTagets.Select(item => new RenderTarget(item)).ToArray());
        }

        public static RenderTargetInfo CreateWithDepthStencil(RenderTarget depthStencil, params RenderTarget[] renderTagets)
        {
            List<RenderTarget> list = new List<RenderTarget>();
            list.AddRange(renderTagets);
            return new RenderTargetInfo(list, depthStencil);
        }

        public static RenderTargetInfo Create(params RenderTarget[] renderTagets)
        {
            return CreateWithDepthStencil(null, renderTagets);
        }
    }

    public struct ColoredPoint
    {
        public readonly Vector2 pos;
        public readonly Vector4 color;
        public ColoredPoint(Vector2 pos, Vector4 color)
        {
            this.pos = pos;
            this.color = color;
        }
    }

    
    public struct Viewport
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">The top-left corner along the x-axis.</param>
        /// <param name="y">The top-right corner along the y-axis.</param>
        /// <param name="w">The width of the viewport.</param>
        /// <param name="h">The height of the viewport.</param>
        public Viewport(float x, float y, float w, float h)
        {
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
        }

        /// <summary>
        /// The top-left corner along the x-axis.
        /// </summary>
        public float x;

        /// <summary>
        /// The top-right corner along the y-axis.
        /// </summary>
        public float y;

        /// <summary>
        /// The width of the viewport.
        /// </summary>
        public float w;

        /// <summary>
        /// The height of the viewport.
        /// </summary>
        public float h;

        /// <summary>
        /// The top-left corner along the x-axis.
        /// </summary>
        public float X
        {
            get { return x; }
            set { x = value; }
        }

        /// <summary>
        /// The top-right corner along the y-axis.
        /// </summary>
        public float Y
        {
            get { return y; }
            set { y = value; }
        }

        /// <summary>
        /// The width of the viewport.
        /// </summary>
        public float Width
        {
            get { return w; }
            set { w = value; }
        }

        /// <summary>
        /// The height of the viewport.
        /// </summary>
        public float Height
        {
            get { return h; }
            set { h = value; }
        }
    }

}
