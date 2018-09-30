// Author : endlesstravel
// this part same as love2d's boot.lua

using System;

namespace Love
{
    /// <summary>
    /// <para>继承本类，作为 Boot.Run 的启动参数。</para>
    /// <para>Inherit this class as the startup parameter for Boot.Run()</para>
    /// </summary>
    abstract public class Scene
    {
        /// <summary>
        /// Triggered when a key is pressed.
        /// </summary>
        /// <param name="key">Character of the pressed key.</param>
        /// <param name="scancode">The scancode representing the pressed key.</param>
        /// <param name="isRepeat">Whether this keypress event is a repeat. The delay between key repeats depends on the user's system settings.</param>
        public virtual void KeyPressed(Keyboard.Key key, Keyboard.Scancode scancode, bool isRepeat) { }

        /// <summary>
        /// Triggered when a keyboard key is released.
        /// </summary>
        /// <param name="key">Character of the pressed key.</param>
        /// <param name="scancode">The scancode representing the pressed key.</param>
        public virtual void KeyReleased(Keyboard.Key key, Keyboard.Scancode scancode) { }

        /// <summary>
        /// Callback function triggered when the mouse is moved.
        /// </summary>
        /// <param name="x">The mouse position on the x-axis.</param>
        /// <param name="y">The mouse position on the y-axis.</param>
        /// <param name="dx">The amount moved along the x-axis since the last time love.mousemoved was called.</param>
        /// <param name="dy">The amount moved along the y-axis since the last time love.mousemoved was called.</param>
        /// <param name="isTouch">True if the mouse button press originated from a touchscreen touch-press.</param>
        public virtual void MouseMoved(float x, float y, float dx, float dy, bool isTouch) { }

        /// <summary>
        /// Callback function triggered when a mouse button is pressed.
        /// </summary>
        /// <param name="x">Mouse x position, in pixels.</param>
        /// <param name="y">Mouse y position, in pixels.</param>
        /// <param name="button">The button index that was pressed. 1 is the primary mouse button, 2 is the secondary mouse button and 3 is the middle button. Further buttons are mouse dependent.</param>
        /// <param name="isTouch">True if the mouse button press originated from a touchscreen touch-press.</param>
        public virtual void MousePressed(float x, float y, int button, bool isTouch) { }

        /// <summary>
        /// Callback function triggered when a mouse button is released.
        /// </summary>
        /// <param name="x">Mouse x position, in pixels.</param>
        /// <param name="y">Mouse y position, in pixels.</param>
        /// <param name="button">The button index that was pressed. 1 is the primary mouse button, 2 is the secondary mouse button and 3 is the middle button. Further buttons are mouse dependent.</param>
        /// <param name="isTouch">True if the mouse button press originated from a touchscreen touch-press.</param>
        public virtual void MouseReleased(float x, float y, int button, bool isTouch) { }

        /// <summary>
        /// Callback function triggered when window receives or loses mouse focus.
        /// </summary>
        /// <param name="focus">Whether the window has mouse focus or not.</param>
        public virtual void MouseFocus(bool focus) { }

        /// <summary>
        /// Callback function triggered when the mouse wheel is moved.
        /// </summary>
        /// <param name="x">Amount of horizontal mouse wheel movement. Positive values indicate movement to the right.</param>
        /// <param name="y">Amount of vertical mouse wheel movement. Positive values indicate upward movement.</param>
        public virtual void WheelMoved(int x, int y) { }

        /// <summary>
        /// Called when a joystick button is pressed.
        /// </summary>
        /// <param name="joystick">The joystick object.</param>
        /// <param name="button">The button number.</param>
        public virtual void JoystickPressed(Joystick joystick, int button) { }

        /// <summary>
        /// Called when a joystick button is released.
        /// </summary>
        /// <param name="joystick">The joystick object.</param>
        /// <param name="button">The button number.</param>
        public virtual void JoystickReleased(Joystick joystick, int button) { }

        /// <summary>
        /// Called when a joystick axis moves.
        /// </summary>
        /// <param name="joystick">The joystick object.</param>
        /// <param name="axis">The axis number.</param>
        /// <param name="value">The new axis value.</param>
        public virtual void JoystickAxis(Joystick joystick, float axis, float value) { }

        /// <summary>
        /// Called when a joystick hat direction changes.
        /// </summary>
        /// <param name="joystick">The joystick object.</param>
        /// <param name="hat">The hat number.</param>
        /// <param name="direction">The new hat direction.</param>
        public virtual void JoystickHat(Joystick joystick, int hat, Joystick.Hat direction) { }

        /// <summary>
        /// Called when a Joystick's virtual gamepad button is pressed.
        /// </summary>
        /// <param name="joystick">The joystick object.</param>
        /// <param name="button">The virtual gamepad button.</param>
        public virtual void JoystickGamepadPressed(Joystick joystick, Joystick.GamepadButton button) { }

        /// <summary>
        /// Called when a Joystick's virtual gamepad button is released.
        /// </summary>
        /// <param name="joystick">The joystick object.</param>
        /// <param name="button">The virtual gamepad button.</param>
        public virtual void JoystickGamepadReleased(Joystick joystick, Joystick.GamepadButton button) { }

        /// <summary>
        /// Called when a Joystick's virtual gamepad axis is moved.
        /// </summary>
        /// <param name="joystick">The joystick object.</param>
        /// <param name="axis">The virtual gamepad axis.</param>
        /// <param name="value">The new axis value.</param>
        public virtual void JoystickGamepadAxis(Joystick joystick, Joystick.GamepadAxis axis, float value) { }

        /// <summary>
        /// Called when a Joystick is connected.
        /// </summary>
        /// <param name="joystick">The newly connected Joystick object.</param>
        public virtual void JoystickAdded(Joystick joystick) { }

        /// <summary>
        /// Called when a Joystick is disconnected.
        /// </summary>
        /// <param name="joystick">The now-disconnected Joystick object.</param>
        public virtual void JoystickRemoved(Joystick joystick) { }

        /// <summary>
        /// Callback function triggered when a touch press moves inside the touch screen.
        /// </summary>
        /// <param name="id">The identifier for the touch press.</param>
        /// <param name="x">The x-axis position of the touch inside the window, in pixels.</param>
        /// <param name="y">The y-axis position of the touch inside the window, in pixels.</param>
        /// <param name="dx">The x-axis movement of the touch inside the window, in pixels.</param>
        /// <param name="dy">The y-axis movement of the touch inside the window, in pixels.</param>
        /// <param name="pressure">The amount of pressure being applied. Most touch screens aren't pressure sensitive, in which case the pressure will be 1.</param>
        public virtual void TouchMoved(long id, float x, float y, float dx, float dy, float pressure) { }

        /// <summary>
        /// Callback function triggered when the touch screen is touched.
        /// </summary>
        /// <param name="id">The identifier for the touch press.</param>
        /// <param name="x">The x-axis position of the touch inside the window, in pixels.</param>
        /// <param name="y">The y-axis position of the touch inside the window, in pixels.</param>
        /// <param name="dx">The x-axis movement of the touch inside the window, in pixels.</param>
        /// <param name="dy">The y-axis movement of the touch inside the window, in pixels.</param>
        /// <param name="pressure">The amount of pressure being applied. Most touch screens aren't pressure sensitive, in which case the pressure will be 1.</param>
        public virtual void TouchPressed(long id, float x, float y, float dx, float dy, float pressure) { }

        /// <summary>
        /// Callback function triggered when the touch screen stops being touched.
        /// </summary>
        /// <param name="id">The identifier for the touch press.</param>
        /// <param name="x">The x-axis position of the touch inside the window, in pixels.</param>
        /// <param name="y">The y-axis position of the touch inside the window, in pixels.</param>
        /// <param name="dx">The x-axis movement of the touch inside the window, in pixels.</param>
        /// <param name="dy">The y-axis movement of the touch inside the window, in pixels.</param>
        /// <param name="pressure">The amount of pressure being applied. Most touch screens aren't pressure sensitive, in which case the pressure will be 1.</param>
        public virtual void TouchReleased(long id, float x, float y, float dx, float dy, float pressure) { }

        /// <summary>
        /// Called when the candidate text for an IME has changed.
        /// </summary>
        /// <param name="text">The UTF-8 encoded unicode candidate text.</param>
        /// <param name="start">The start cursor of the selected candidate text.</param>
        /// <param name="end">The length of the selected candidate text. May be 0.</param>
        public virtual void TextEditing(string text, int start, int end) { }

        /// <summary>
        /// Called when text has been entered by the user.
        /// </summary>
        /// <param name="text">The UTF-8 encoded unicode text.</param>
        public virtual void TextInput(string text) { }

        /// <summary>
        /// Callback function triggered when window receives or loses focus.
        /// </summary>
        /// <param name="focus">True if the window gains focus, false if it loses focus.</param>
        public virtual void WindowFocus(bool focus) { }

        /// <summary>
        /// Callback function triggered when window is shown or hidden.
        /// </summary>
        /// <param name="visible">True if the window is visible, false if it isn't.</param>
        public virtual void WindowVisible(bool visible) { }

        /// <summary>
        /// Called when the window is resized, for example if the user resizes the window, or if Love.Window.SetMode is called with an unsupported width or height in fullscreen and the window chooses the closest appropriate size.
        /// </summary>
        /// <param name="w">The new width, in pixels.</param>
        /// <param name="h">The new height, in pixels.</param>
        public virtual void WindowResize(int w, int h) { }

        /// <summary>
        /// Callback function triggered when a directory is dragged and dropped onto the window.
        /// </summary>
        /// <param name="path">The full platform-dependent path to the directory. It can be used as an argument to love.filesystem.mount, in order to gain read access to the directory with love.filesystem.</param>
        public virtual void DirectoryDropped(string path) { }

        /// <summary>
        /// Callback function triggered when a file is dragged and dropped onto the window.
        /// </summary>
        /// <param name="file">The unopened File object representing the file that was dropped.</param>
        public virtual void FileDropped(File file) { }

        /// <summary>
        /// Callback function triggered when the game is closed.
        /// </summary>
        /// <returns>Abort quitting. If true, do not close the game.</returns>
        public virtual bool Quit() { return true; }

        /// <summary>
        /// Callback function triggered when the system is running out of memory on mobile devices.
        /// </summary>
        public virtual void LowMemory() { }

        /// <summary>
        /// This function is called exactly once at the beginning of the game.
        /// </summary>
        public virtual void Load() {}

        /// <summary>
        /// Callback function used to update the state of the game every frame.
        /// </summary>
        /// <param name="dt">Time since the last update in seconds.</param>
        public virtual void Update(float dt) {}

        /// <summary>
        /// Callback function used to draw on the screen every frame.
        /// </summary>
        public virtual void Draw() {}
    }

    /// <summary>
    /// Boot class start params
    /// </summary>
    public class BootConfig
    {
        /// <summary>
        /// The window width(height)
        /// </summary>
        public int WindowWidth = 800, WindowHeight = 600;

        /// <summary>
        /// The window title
        /// </summary>
        public string WindowTitle = "No Title";

        /// <summary>
        /// Remove all border visuals from the window
        /// </summary>
        public bool WindowBorderless = false;

        /// <summary>
        /// Let the window be user-resizable
        /// </summary>
        public bool WindowResizable = false;

        /// <summary>
        /// Minimum window width if the window is resizable
        /// </summary>
        public int WindowMinWidth = 1;

        /// <summary>
        /// Minimum window height if the window is resizable
        /// </summary>
        public int WindowMinHeight = 1;

        /// <summary>
        /// Choose between "DeskTop" fullscreen or "Exclusive" fullscreen mode 
        /// </summary>
        public FullscreenType WindowFullscreen = FullscreenType.DeskTop;

        /// <summary>
        /// Vertical sync mode
        /// </summary>
        public bool WindowVsync = true;

        /// <summary>
        /// The number of samples to use with multi-sampled antialiasing
        /// </summary>
        public int WindowMSAA = 0;

        /// <summary>
        /// Index of the monitor to show the window in
        /// </summary>
        public int WindowDisplay = 1;


        public bool WindowCentered = true;

        /// <summary>
        /// Enable high-dpi mode for the window on a Retina display
        /// </summary>
        public bool WindowHighdpi = false;

        /// <summary>
        /// The x-coordinate(y-coordinate) of the window's position in the specified display
        /// </summary>
        public int? WindowX = null, WindowY = null;
    }

    /// <summary>
    /// LÖVE engine entrance class
    /// <para>LÖVE 引擎入口类</para> 
    /// </summary>
    static public class Boot
    {
        static void Init(BootConfig bootConfig)
        {
            Mathf.Init();
            FileSystem.Init("");

            Timer.Init();
            Event.Init();
            Keyboard.Init();
            Joystick.Init();
            Mouse.Init();
            Touch.Init();
            Sound.Init();

            Audio.Init();
            Font.Init();
            Image.Init();
            Window.Init();
            Graphics.Init();

            // config
            if (bootConfig == null)
            {
                bootConfig = new BootConfig();
            }

            bool usePosition = bootConfig.WindowX != null && bootConfig.WindowY != null;

            WindowSettings settings = new WindowSettings();

            settings.fullscreenType = bootConfig.WindowFullscreen;
            settings.vsync = bootConfig.WindowVsync;
            settings.msaa = bootConfig.WindowMSAA;
            settings.resizable = bootConfig.WindowResizable;
            settings.minWidth = bootConfig.WindowMinWidth;
            settings.minHeight = bootConfig.WindowMinHeight;
            settings.borderless = bootConfig.WindowBorderless;
            settings.centered = bootConfig.WindowCentered;
            settings.display = bootConfig.WindowDisplay;
            settings.highDpi = bootConfig.WindowHighdpi;
            settings.useposition = usePosition;
            settings.x = bootConfig.WindowX != null ? (int)bootConfig.WindowX : 0;
            settings.y = bootConfig.WindowY != null ? (int)bootConfig.WindowY : 0;
            Window.SetMode(bootConfig.WindowWidth, bootConfig.WindowHeight, settings);

            string str = FileSystem.GetExecutablePath();
            int index = str.LastIndexOf(@"\");
            if (index == -1)
                index = str.LastIndexOf(@"/");
            string path = str.Substring(0, index);
            Console.WriteLine($"FileSystem set source with path : {path}");
            FileSystem.SetSource(path);
        }

        static void Loop(Scene scene)
        {
            scene.Load();
            while (true)
            {
                Event.Poll(scene);
                Timer.Step();

                scene.Update(Timer.GetDelta());

                var c = Graphics.GetBackgroundColor();
                Graphics.Clear(c.r, c.g, c.b, c.a);
                Graphics.Origin();
                scene.Draw();
                Graphics.Present();
                Timer.Sleep(0.001f);
            }
        }

        /// <summary>
        /// LÖVE engine entrance function
        /// </summary>
        /// <param name="scene">The way to run LÖVE engine</param>
        /// <param name="bootConfig">LÖVE engine boot config</param>
        static public void Run(Scene scene = null, BootConfig bootConfig = null)
        {
            try
            {
                Init(bootConfig);
                Loop(scene != null ? scene : new Love2dNoGame());
            }
            catch (Exception e)
            {
                Window.ShowMessageBox("error", e.ToString(), MessageBoxType.Error);
            }
        }
    }
}
