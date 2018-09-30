
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
}
