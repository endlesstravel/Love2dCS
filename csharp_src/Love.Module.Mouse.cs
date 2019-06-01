using System;
using System.Collections.Generic;

namespace Love
{
    public partial class Mouse
    {
        /// <summary>
        /// The mouse left button
        /// </summary>
        public const int LeftButton = 0;

        /// <summary>
        /// The mouse right button
        /// </summary>
        public const int RightButton = 1;

        /// <summary>
        /// The mouse middle button
        /// </summary>
        public const int MiddleButton = 2;

        /// <summary>
        /// The first extended button
        /// </summary>
        public const int ExtendedButton1 = 3;

        /// <summary>
        /// The second extended button
        /// </summary>
        public const int ExtendedButton2 = 4;

        /// <summary>
        /// The third extended button
        /// </summary>
        public const int ExtendedButton3 = 5;

        /// <summary>
        /// The Fourth extended button
        /// </summary>
        public const int ExtendedButton4 = 6;

        /// <summary>
        /// The Fifth extended button
        /// </summary>
        public const int ExtendedButton5 = 7;
    }

    /// <summary>
    /// Provides an interface to the user's mouse.
    /// </summary>
    public partial class Mouse
    {
        public static bool Init()
        {
            return Love2dDll.wrap_love_dll_open_love_mouse();
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
        {
            IntPtr out_cursor;
            Love2dDll.wrap_love_dll_mouse_newCursor(imageData.p, hotX, hotY, out out_cursor);
            return LoveObject.NewObject<Cursor>(out_cursor);
        }

        /// <summary>
        /// <para> Gets a Cursor object representing a system-native hardware cursor. </para>
        /// <para>Hardware cursors are framerate-independent and work the same way as normal operating system cursors. Unlike drawing an image at the mouse's current coordinates, hardware cursors never have visible lag between when the mouse is moved and when the cursor position updates, even at low framerates.</para>
        /// </summary>
        /// <param name="sysctype"></param>
        /// <returns></returns>
        public static Cursor GetSystemCursor(SystemCursor sysctype)
        {
            IntPtr out_cursor = IntPtr.Zero;
            Love2dDll.wrap_love_dll_mouse_getSystemCursor((int)sysctype, out out_cursor);
            return LoveObject.NewObject<Cursor>(out_cursor);
        }

        /// <summary>
        /// Sets the current mouse cursor. null to set as default cursor.
        /// </summary>
        /// <param name="cursor"></param>
        public static void SetCursor(Cursor cursor = null)
        {
            Love2dDll.wrap_love_dll_mouse_setCursor(cursor != null ? cursor.p : IntPtr.Zero);
        }

        /// <summary>
        /// Gets the current Cursor.
        /// </summary>
        /// <returns></returns>
        public static Cursor GetCursor()
        {
            IntPtr out_cursor = IntPtr.Zero;
            Love2dDll.wrap_love_dll_mouse_getCursor(out out_cursor);
            return LoveObject.NewObject<Cursor>(out_cursor);
        }

        /// <summary>
        /// Gets whether cursor functionality is supported.
        /// </summary>
        /// <returns></returns>
        public static bool IsCursorSupported()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_mouse_isCursorSupported(out out_result);
            return out_result;
        }

        /// <summary>
        /// Returns the current x-position of the mouse.
        /// </summary>
        /// <returns></returns>
        public static double GetXDouble()
        {
            double out_x = 0;
            Love2dDll.wrap_love_dll_mouse_getX(out out_x);
            return out_x;
        }

        /// <summary>
        /// Returns the current y-position of the mouse.
        /// </summary>
        /// <returns></returns>
        public static double GetYDouble()
        {
            double out_y = 0;
            Love2dDll.wrap_love_dll_mouse_getY(out out_y);
            return out_y;
        }

        /// <summary>
        /// Returns the current x-position of the mouse.
        /// </summary>
        /// <returns></returns>
        public static float GetX()
        {
            return (float)GetXDouble();
        }

        /// <summary>
        /// Returns the current y-position of the mouse.
        /// </summary>
        /// <returns></returns>
        public static float GetY()
        {
            return (float)GetYDouble();
        }


        /// <summary>
        /// Returns the current x/y-position of the mouse.
        /// </summary>
        /// <returns></returns>
        public static void GetPosition(out double out_x, out double out_y)
        {
            Love2dDll.wrap_love_dll_mouse_getPosition(out out_x, out out_y);
        }

        /// <summary>
        /// Sets the current X position of the mouse.
        /// </summary>
        /// <param name="x"></param>
        public static void SetX(double x)
        {
            Love2dDll.wrap_love_dll_mouse_setX(x);
        }

        /// <summary>
        /// Sets the current Y position of the mouse.
        /// </summary>
        /// <param name="y"></param>
        public static void SetY(double y)
        {
            Love2dDll.wrap_love_dll_mouse_setY(y);
        }


        /// <summary>
        /// Sets the current X/Y position of the mouse.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void SetPosition(double x, double y)
        {
            Love2dDll.wrap_love_dll_mouse_setPosition(x, y);
        }

        /// <summary>
        /// Checks whether a certain button is down.
        /// <para>This function does not detect mouse wheel scrolling; you must use the love.wheelmoved (or love.mousepressed in version 0.9.2 and older) callback for that.</para>
        /// </summary>
        /// <param name="buttonIndex">The index of a button to check. <see cref="Mouse.LeftButton"> is the primary mouse button, <see cref="Mouse.RightButton"> is the secondary mouse button and <see cref="Mouse.MiddleButton"> is the middle button. Further buttons are mouse dependant.</param>
        /// <returns>True if any specified button is down.</returns>
        public static bool IsDown(int buttonIndex)
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_mouse_isDown(buttonIndex + 1, out out_result);
            return out_result;
        }

        /// <summary>
        /// Sets the current visibility of the cursor.
        /// </summary>
        /// <param name="visible"></param>
        public static void SetVisible(bool visible)
        {
            Love2dDll.wrap_love_dll_mouse_setVisible(visible);
        }

        /// <summary>
        /// Checks if the cursor is visible.
        /// </summary>
        /// <returns></returns>
        public static bool IsVisible()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_mouse_isVisible(out out_result);
            return out_result;
        }

        /// <summary>
        /// Grabs the mouse and confines it to the window.
        /// </summary>
        /// <param name="grabbed"></param>
        public static void SetGrabbed(bool grabbed)
        {
            Love2dDll.wrap_love_dll_mouse_setGrabbed(grabbed);
        }

        /// <summary>
        /// Checks if the mouse is grabbed.
        /// </summary>
        /// <returns></returns>
        public static bool IsGrabbed()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_mouse_isGrabbed(out out_result);
            return out_result;
        }

        /// <summary>
        /// <para>WARNNING: THIS API NOT STABLE, IT WILL CRASHED! </para>
        /// <para>Sets whether relative mode is enabled for the mouse. </para>
        /// <para>When relative mode is enabled, the cursor is hidden and doesn't move when the mouse does, but relative mouse motion events are still generated via love.mousemoved. This lets the mouse move in any direction indefinitely without the cursor getting stuck at the edges of the screen.</para>
        /// <para>The reported position of the mouse may not be updated while relative mode is enabled, even when relative mouse motion events are generated.</para>
        /// </summary>
        /// <param name="enable">True to enable relative mode, false to disable it.</param>
        /// <returns></returns>
        public static bool SetRelativeMode(bool enable)
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_mouse_setRelativeMode(enable, out out_result);
            return out_result;
        }

        /// <summary>
        /// Gets whether relative mode is enabled for the mouse.
        /// </summary>
        /// <returns>True if relative mode is enabled, false if it's disabled.</returns>
        public static bool GetRelativeMode()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_mouse_getRelativeMode(out out_result);
            return out_result;
        }
    }

    public partial class Mouse
    {
        const int RememberButtonCount = 32;
        static bool[] lastBtnDown = new bool[RememberButtonCount];
        static bool[] currentBtnDown = new bool[RememberButtonCount];
        public static void Step()
        {
            for (int i = 0; i < RememberButtonCount; i++)
            {
                lastBtnDown[i] = currentBtnDown[i];
                currentBtnDown[i] = Mouse.IsDown(i);
            }
        }


        public static bool IsPressed(int buttonIndex)
        {
            if (0 <= buttonIndex && buttonIndex <= RememberButtonCount)
            {
                return lastBtnDown[buttonIndex] == false && Mouse.IsDown(buttonIndex) == true;
            }

            return false;
        }

        public static bool IsReleased(int buttonIndex)
        {
            if (0 <= buttonIndex && buttonIndex <= RememberButtonCount)
            {
                return lastBtnDown[buttonIndex] == true && Mouse.IsDown(buttonIndex) == false;
            }

            return false;
        }


        static readonly Dictionary<SystemCursor, Cursor> systemCursorDict = new Dictionary<SystemCursor, Cursor>();

        /// <summary>
        /// Sets the current mouse cursor to system cursor.
        /// </summary>
        /// <param name="systemCursor"></param>
        public static void SetCursor(SystemCursor systemCursor)
        {
            if (!systemCursorDict.TryGetValue(systemCursor, out var cursorToUse))
            {
                cursorToUse = GetSystemCursor(systemCursor);
                systemCursorDict.Add(systemCursor, cursorToUse);
            }

            SetCursor(cursorToUse);
        }

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

        /// <summary>
        /// Returns the current position of the mouse.
        /// </summary>
        /// <returns>The position of the mouse.</returns>
        public static Vector2 GetPosition()
        {
            double out_x, out_y;
            Love2dDll.wrap_love_dll_mouse_getPosition(out out_x, out out_y);
            return new Vector2((float)out_x, (float)out_y);
        }


        static int mouseScrollValueX = 0, mouseScrollValueY = 0;
        public static int GetScrollX()
        {
            return mouseScrollValueX;
        }
        public static int GetScrollY()
        {
            return mouseScrollValueY;
        }
        public static void SetScrollX(int value)
        {
            mouseScrollValueX = value;
        }
        public static void SetScrollY(int value)
        {
            mouseScrollValueY = value;
        }


        static float mousePreviousX = 0, mousePreviousY = 0;
        public static float GetPreviousX()
        {
            return mousePreviousX;
        }
        public static float GetPreviousY()
        {
            return mousePreviousY;
        }
        public static Vector2 GetPreviousPosition()
        {
            return new Vector2(mousePreviousX, mousePreviousY);
        }
        public static void SetPreviousPosition(float x, float y)
        {
            mousePreviousX = x;
            mousePreviousY = y;
        }
        public static void SetPreviousPosition(Vector2 pos)
        {
            mousePreviousX = pos.X;
            mousePreviousY = pos.Y;
        }
    }


}