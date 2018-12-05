
namespace Love
{
    /// <summary>
    /// 窗口属性
    /// </summary>
    public class WindowSettings
    {
        /// <summary>
        /// Fullscreen (true), or windowed (false).
        /// </summary>
        public bool fullscreen = false;

        /// <summary>
        /// Choose between "DeskTop" fullscreen or "Exclusive" fullscreen mode 
        /// </summary>
        public FullscreenType fullscreenType;

        /// <summary>
        /// True if LÖVE should wait for vsync, false otherwise.
        /// </summary>
        public bool vsync = true;

        /// <summary>
        /// The number of antialiasing samples.
        /// </summary>
        public int msaa = 0;

        /// <summary>
        /// Whether a stencil buffer should be allocated. If true, the stencil buffer will have 8 bits.
        /// </summary>
        public bool stencil = true;

        /// <summary>
        /// The number of bits in the depth buffer.
        /// </summary>
        public int depth = 0;

        /// <summary>
        /// Let the window be user-resizable
        /// </summary>
        public bool resizable = false;

        /// <summary>
        /// Remove all border visuals from the window
        /// </summary>
        public bool borderless = false;

        /// <summary>
        /// True if the window should be borderless in windowed mode, false otherwise.
        /// </summary>
        public bool centered = true;

        /// <summary>
        /// The index of the display to show the window in, if multiple monitors are available.
        /// </summary>
        public int display = 0;

        /// <summary>
        /// True if high-dpi mode should be used on Retina displays in macOS and iOS. Does nothing on non-Retina displays. Added in 0.9.1.
        /// </summary>
        public bool highDpi = false;

        /// <summary>
        /// The minimum width(height) of the window, if it's resizable. Cannot be less than 1.
        /// </summary>
        public int minWidth = 1, minHeight = 1;

        /// <summary>
        /// True if use the position params, false otherwise.
        /// </summary>
        public bool useposition = true;

        /// <summary>
        /// The x-coordinate of the window's position in the specified display. Added in 0.9.2.
        /// </summary>
        public int x = 0, y = 0;

        /// <summary>
        /// We don't explicitly set the refresh rate, it's "read-only".
        /// <para>The refresh rate of the screen's current display mode, in Hz. May be 0 if the value can't be determined. Added in 0.9.2.</para>
        /// </summary>
        public double refreshrate;
    }

    /// <summary>
    /// for Mesh function
    /// </summary>
    public struct Vertex
    {
        /// <summary>
        /// The position of the vertex .
        /// </summary>
        public readonly Vector2 pos;

        /// <summary>
        /// The u and v texture coordinate of the vertex. Texture coordinates are normally in the range of [0, 1], but can be greater or less (see WrapMode.)
        /// </summary>
        public readonly Vector2 uv;

        /// <summary>
        /// The vertex color.
        /// </summary>
        public readonly Vector4 color;


        /// <summary>
        /// Mesh vertex.
        /// </summary>
        /// <param name="pos">The position of the vertex.</param>
        /// <param name="uv">The u and vtexture coordinate of the vertex. Texture coordinates are normally in the range of [0, 1], but can be greater or less (see <see cref="Texture.WrapMode"/>)  <para>https://love2d.org/wiki/WrapMode</para></param>
        /// <param name="color">The vertex color.</param>
        public Vertex(Vector2 pos)
        {
            this.pos = pos;
            uv = new Vector2(0, 0);
            color = new Vector4(1, 1, 1, 1);
        }

        /// <summary>
        /// Mesh vertex.
        /// </summary>
        /// <param name="pos">The position of the vertex.</param>
        /// <param name="uv">The u and vtexture coordinate of the vertex. Texture coordinates are normally in the range of [0, 1], but can be greater or less (see <see cref="Texture.WrapMode"/>)  <para>https://love2d.org/wiki/WrapMode</para></param>
        /// <param name="color">The vertex color.</param>
        public Vertex(Vector2 pos, Vector2 uv, Vector4 color)
        {
            this.pos = pos;
            this.uv = uv;
            this.color = color;
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
