// Author : endlesstravel
// this part make basical ability to use love2d

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using size_t = System.UInt32;
using int64 = System.Int64;

namespace Love
{

    static partial class DllTool
    {
        const int intSize = 4;

        public static byte[] ToUTF8Bytes(this string str)
        {
            return utf8.GetBytes(str);
        }

        public static ColoredString ToColoredStrings(string str)
        {
            ColoredString buffer = new ColoredString(new string[] {str }, new Int4[] { new Int4(255, 255, 255, 255) });
            return buffer;
        }

        public static string GetLoveLastError()
        {
            // ???????????? WSToString(Love2dDll.wrap_love_dll_last_error());
            // ????????????????
            IntPtr out_errormsg = IntPtr.Zero;
            Love2dDll.wrap_love_dll_last_error(out out_errormsg);
            WrapString ws = (WrapString)Marshal.PtrToStructure(out_errormsg, typeof(WrapString));
            return PtrToStringUTF8(ws.data);
        }

        public static Float3 ToFloat3(IntPtr ip)
        {
            Float3 f3 = (Float3)Marshal.PtrToStructure(ip, typeof(Float3));
            return f3;
        }

        public static Float6 ToFloat6(IntPtr ip)
        {
            Float6 f = (Float6)Marshal.PtrToStructure(ip, typeof(Float6));
            return f;
        }

        static System.Text.Encoding utf8 = System.Text.Encoding.GetEncoding("utf-8");
        public static string PtrToStringUTF8(IntPtr ip)
        {
            List<byte> list = new List<byte>();
            int offset = 0;
            byte b;
            while ((b = Marshal.ReadByte(ip, offset)) != 0)
            {
                list.Add(b);
                offset++;
            }
            return utf8.GetString(list.ToArray());
        }
        //public static string WSToStringAndRelease(IntPtr ip)
        //{
        //    WrapString ws = (WrapString)Marshal.PtrToStructure(ip, typeof(WrapString));
        //    // return PtrToStringUTF8(ws.data);
        //    var str = Marshal.PtrToStringAnsi(ws.data);
        //    Love2dDll.wrap_love_dll_delete_WrapString(ip);
        //    return str;
        //}
        public static string WSToStringAndRelease(IntPtr ip)
        {
            if (ip == IntPtr.Zero)
            {
                return null;
            }

            WrapString ws = (WrapString)Marshal.PtrToStructure(ip, typeof(WrapString));
            var str = PtrToStringUTF8(ws.data);
            Love2dDll.wrap_love_dll_delete_WrapString(ip);
            return str;
        }

        public static string[] WSSToStringListAndRelease(IntPtr ip)
        {
            WrapSequenceString ws = (WrapSequenceString)Marshal.PtrToStructure(ip, typeof(WrapSequenceString));
            string[] buffer = new string[ws.len];

            for (int i = 0; i < ws.len; i++)
            {
                string item = PtrToStringUTF8(Marshal.ReadIntPtr(ws.data, 4 * i));
                buffer[i] = item;
            }

            Love2dDll.wrap_love_dll_delete_WrapSequenceString(ip);
            return buffer;
        }

        public static byte[] readBytesAndRelease(IntPtr p, long size)
        {
            byte[] buffer = new byte[size];
            for (int i = 0; i < size; i++)
            {
                buffer[i] = Marshal.ReadByte(p, i);
            }

            Love2dDll.wrap_love_dll_delete_array(p);
            return buffer;
        }

        public static IntPtr[] readIntPtrsAndRelease(IntPtr p, long size)
        {
            IntPtr[] buffer = new IntPtr[size];
            for (int i = 0; i < size; i++)
            {
                buffer[i] = Marshal.ReadIntPtr(p, i);
            }

            Love2dDll.wrap_love_dll_delete_array(p);
            return buffer;
        }

        public static T[] readIntPtrsWithConvertAndRelease<T>(IntPtr p, long size) where T : LoveObject, new()
        {
            IntPtr[] buffer = new IntPtr[size];
            for (int i = 0; i < size; i++)
            {
                buffer[i] = Marshal.ReadIntPtr(p, i);
            }

            Love2dDll.wrap_love_dll_delete_array(p);

            T[] tarray = new T[buffer.Length];
            for (int i = 0; i < size; i++)
            {
                tarray[i] = LoveObject.NewObject<T>(buffer[i]);
            }

            return tarray;
        }

        public static int[] readInt32sAndRelease(IntPtr p, long size)
        {
            int[] buffer = new int[size];
            for (int i = 0; i < size; i++)
            {
                buffer[i] = Marshal.ReadInt32(p, i);
            }

            Love2dDll.wrap_love_dll_delete_array(p);
            return buffer;
        }

        public static uint[] readUInt32sAndRelease(IntPtr p, int size)
        {
            uint[] buffer = new uint[size];
            for (int i = 0; i < size; i++)
            {
                IntPtr offset = IntPtr.Add(p, Marshal.SizeOf(typeof(uint)) * size);
                buffer[i] = (uint)Marshal.PtrToStructure(offset, typeof(uint));
            }

            Love2dDll.wrap_love_dll_delete_array(p);
            return buffer;
        }


        public static long[] readLongsAndRelease(IntPtr p, int size)
        {
            long[] buffer = new long[size];
            for (int i = 0; i < size; i++)
            {
                IntPtr offset = IntPtr.Add(p, Marshal.SizeOf(typeof(long)) * size);
                buffer[i] = (long)Marshal.PtrToStructure(offset, typeof(long));
            }

            Love2dDll.wrap_love_dll_delete_array(p);
            return buffer;
        }

        public static double[] readDoublesAndRelease(IntPtr p, int size)
        {
            double[] buffer = new double[size];
            for (int i = 0; i < size; i++)
            {
                IntPtr offset = IntPtr.Add(p, Marshal.SizeOf(typeof(double)) * size);
                buffer[i] = (double)Marshal.PtrToStructure(offset, typeof(double));
            }

            Love2dDll.wrap_love_dll_delete_array(p);
            return buffer;
        }


        public static Int4[] readInt4sAndRelease(IntPtr p, int size)
        {
            Int4[] buffer = new Int4[size];
            for (int i = 0; i < size; i++)
            {
                IntPtr offset = IntPtr.Add(p, Marshal.SizeOf(typeof(Int4)) * size);
                buffer[i] = (Int4)Marshal.PtrToStructure(offset, typeof(Int4));
            }

            Love2dDll.wrap_love_dll_delete_array(p);
            return buffer;
        }

        public static Float4[] readFloat4sAndRelease(IntPtr p, int size)
        {
            Float4[] buffer = new Float4[size];
            for (int i = 0; i < size; i++)
            {
                IntPtr offset = IntPtr.Add(p, Marshal.SizeOf(typeof(Float4)) * size);
                buffer[i] = (Float4)Marshal.PtrToStructure(offset, typeof(Float4));
            }

            Love2dDll.wrap_love_dll_delete_array(p);
            return buffer;
        }

        public static Int2[] readInt2sAndRelease(IntPtr p, int size)
        {
            Int2[] buffer = new Int2[size];
            for (int i = 0; i < size; i++)
            {
                IntPtr offset = IntPtr.Add(p, Marshal.SizeOf(typeof(Int2)) * size);
                buffer[i] = (Int2)Marshal.PtrToStructure(offset, typeof(Int2));
            }

            Love2dDll.wrap_love_dll_delete_array(p);
            return buffer;
        }


        public static Float2[] readFloat2sAndRelease(IntPtr p, int size)
        {
            Float2[] buffer = new Float2[size];
            for (int i = 0; i < size; i++)
            {
                IntPtr offset = IntPtr.Add(p, Marshal.SizeOf(typeof(Float2)) * size);
                buffer[i] = (Float2)Marshal.PtrToStructure(offset, typeof(Float2));
            }

            Love2dDll.wrap_love_dll_delete_array(p);
            return buffer;
        }


        public static float[] readFloatsAndRelease(IntPtr p, int size)
        {
            float[] buffer = new float[size];
            Marshal.Copy(p, buffer, 0, size);
            Love2dDll.wrap_love_dll_delete_array(p);
            return buffer;
        }
    }

    public partial class Common
    {
        public static string GetVersion()
        {
            IntPtr out_str = IntPtr.Zero;
            Love2dDll.wrap_love_dll_common_getVersion(out out_str);
            return DllTool.WSToStringAndRelease(out_str);
        }
        public static string GetVersionCodeName()
        {
            IntPtr out_str = IntPtr.Zero;
            Love2dDll.wrap_love_dll_common_getVersionCodeName(out out_str);
            return DllTool.WSToStringAndRelease(out_str);
        }

    }

    public partial class Timer
    {
        public static bool Init()
        {
            return Love2dDll.wrap_love_dll_timer_open_timer();
        }
        public static void Step()
        {
            Love2dDll.wrap_love_dll_timer_step();
        }
        public static float GetDelta()
        {
            float out_delta = 0;
            Love2dDll.wrap_love_dll_timer_getDelta(out out_delta);
            return out_delta;
        }
        public static float GetFPS()
        {
            float out_fps = 0;
            Love2dDll.wrap_love_dll_timer_getFPS(out out_fps);
            return out_fps;
        }
        public static float GetAverageDelta()
        {
            float out_averageDelta = 0;
            Love2dDll.wrap_love_dll_timer_getAverageDelta(out out_averageDelta);
            return out_averageDelta;
        }
        public static void Sleep(float t)
        {
            Love2dDll.wrap_love_dll_timer_sleep(t);
        }
        public static float GetTime()
        {
            float out_time = 0;
            Love2dDll.wrap_love_dll_timer_getTime(out out_time);
            return out_time;
        }
    }

    public partial class Window
    {
        public enum FullscreenType
        {
            Exclusive,
            DeskTop,
        };

        public enum MessageBoxType
        {
            Error,
            Warning,
            Info,
        };

        public static bool Init()
        {
            var res = Love2dDll.wrap_love_dll_windows_open_love_window();
            return res;
        }

        public static int GetDisplayCount()
        {
            int out_count = 0;
            Love2dDll.wrap_love_dll_windows_getDisplayCount(out out_count);
            return out_count;
        }
        public static string GetDisplayName(int displayindex)
        {
            IntPtr out_name = IntPtr.Zero;
            Love2dDll.wrap_love_dll_windows_getDisplayName(displayindex, out out_name);
            return DllTool.WSToStringAndRelease(out_name);
        }
        public static void SetMode(int width, int height)
        {
            Love2dDll.wrap_love_dll_windows_setMode_w_h(width, height);
        }
        public static void SetMode(int width, int height, bool fullscreen, FullscreenType fstype, bool vsync, int msaa, bool resizable, int minwidth, int minheight, bool borderless, bool centered, int display, bool highdpi, double refreshrate, bool useposition, int x, int y)
        {
            Love2dDll.wrap_love_dll_windows_setMode_w_h_setting(width, height, fullscreen, (int)fstype, vsync, msaa, resizable, minwidth, minheight, borderless, centered, display, highdpi, refreshrate, useposition, x, y);
        }
        public static void GetMode(out int out_width, out int out_height, out bool out_fullscreen, out FullscreenType out_fstype, out bool out_vsync, out int out_msaa, out bool out_resizable, out int out_minwidth, out int out_minheight, out bool out_borderless, out bool out_centered, out int out_display, out bool out_highdpi, out double out_refreshrate, out bool out_useposition, out int out_x, out int out_y)
        {
            int outfstype;
            Love2dDll.wrap_love_dll_windows_getMode(out out_width, out out_height, out out_fullscreen, out outfstype, out out_vsync, out out_msaa, out out_resizable, out out_minwidth, out out_minheight, out out_borderless, out out_centered, out out_display, out out_highdpi, out out_refreshrate, out out_useposition, out out_x, out out_y);
            out_fstype = (FullscreenType)outfstype;
        }
        public static Int2[] GetFullscreenModes(int displayindex)
        {
            IntPtr out_modes;
            int out_modes_length;
            Love2dDll.wrap_love_dll_windows_getFullscreenModes(displayindex, out out_modes, out out_modes_length);
            return DllTool.readInt2sAndRelease(out_modes, out_modes_length);
        }
        public static bool SetFullscreen(bool fullscreen, FullscreenType fstype)
        {
            bool out_success = false;
            Love2dDll.wrap_love_dll_windows_setFullscreen(fullscreen, (int)fstype, out out_success);
            return out_success;
        }
        public static void GetFullscreen(out bool out_fullscreen, out FullscreenType out_fstype)
        {
            int t_out_fstype;
            Love2dDll.wrap_love_dll_windows_getFullscreen(out out_fullscreen, out t_out_fstype);
            out_fstype = (FullscreenType)t_out_fstype;
        }
        public static bool IsOpen()
        {
            bool out_isopen = false;
            Love2dDll.wrap_love_dll_windows_isOpen(out out_isopen);
            return out_isopen;
        }
        public static void Close()
        {
            Love2dDll.wrap_love_dll_windows_close();
        }
        public static void GetDesktopDimensions(int displayindex, out int out_width, out int out_height)
        {
            Love2dDll.wrap_love_dll_windows_getDesktopDimensions(displayindex, out out_width, out out_height);
        }
        public static void SetPosition(int x, int y, int displayindex)
        {
            Love2dDll.wrap_love_dll_windows_setPosition(x, y, displayindex);
        }
        public static void GetPosition(out int out_x, out int out_y, out int out_displayindex)
        {
            Love2dDll.wrap_love_dll_windows_getPosition(out out_x, out out_y, out out_displayindex);
        }
        public static bool SetIcon(ImageData imagedata)
        {
            bool out_success = false;
            Love2dDll.wrap_love_dll_windows_setIcon(imagedata.p, out out_success);
            return out_success;
        }
        public static ImageData GetIcon()
        {
            IntPtr out_imagedata = IntPtr.Zero;
            Love2dDll.wrap_love_dll_windows_getIcon(out out_imagedata);
            return LoveObject.NewObject<ImageData>(out_imagedata);
        }
        public static void SetDisplaySleepEnabled(bool enable)
        {
            Love2dDll.wrap_love_dll_windows_setDisplaySleepEnabled(enable);
        }
        public static bool IsDisplaySleepEnabled()
        {
            bool out_enable = false;
            Love2dDll.wrap_love_dll_windows_isDisplaySleepEnabled(out out_enable);
            return out_enable;
        }
        public static void SetTitle(byte[] titleStr)
        {
            Love2dDll.wrap_love_dll_windows_setTitle(titleStr);
        }
        public static string GetTitle()
        {
            IntPtr out_title = IntPtr.Zero;
            Love2dDll.wrap_love_dll_windows_getTitle(out out_title);
            return DllTool.WSToStringAndRelease(out_title);
        }
        public static bool HasFocus()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_windows_hasFocus(out out_result);
            return out_result;
        }
        public static bool HasMouseFocus()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_windows_hasMouseFocus(out out_result);
            return out_result;
        }
        public static bool IsVisible()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_windows_isVisible(out out_result);
            return out_result;
        }
        public static double GetPixelScale()
        {
            double out_result = 0;
            Love2dDll.wrap_love_dll_windows_getPixelScale(out out_result);
            return out_result;
        }
        public static double ToPixels(double value)
        {
            double out_result = 0;
            Love2dDll.wrap_love_dll_windows_toPixels(value, out out_result);
            return out_result;
        }
        public static double FromPixels(double pixelvalue)
        {
            double out_result = 0;
            Love2dDll.wrap_love_dll_windows_fromPixels(pixelvalue, out out_result);
            return out_result;
        }
        public static void Minimize()
        {
            Love2dDll.wrap_love_dll_windows_minimize();
        }
        public static void Maximize()
        {
            Love2dDll.wrap_love_dll_windows_maximize();
        }
        public static bool IsMaximized()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_windows_isMaximized(out out_result);
            return out_result;
        }
        public static bool ShowMessageBox(byte[] title, byte[] message, MessageBoxType msgbox_type, bool attachToWindow = true)
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_windows_showMessageBox(title, message, (int)msgbox_type, attachToWindow, out out_result);
            return out_result;
        }
        public static void RequestAttention(bool continuous)
        {
            Love2dDll.wrap_love_dll_windows_requestAttention(continuous);
        }
        public static void WindowToPixelCoords(out double out_x, out double out_y)
        {
            Love2dDll.wrap_love_dll_windows_windowToPixelCoords(out out_x, out out_y);
        }
        public static void PixelToWindowCoords(out double x, out double y)
        {
            Love2dDll.wrap_love_dll_windows_pixelToWindowCoords(out x, out y);
        }
        public static void GetPixelDimensions(out int out_w, out int out_h)
        {
            Love2dDll.wrap_love_dll_windows_getPixelDimensions(out out_w, out out_h);
        }
    }

    public partial class Mouse
    {
        //// raw new
        public static Cursor NewCursor(ImageData imageData, int hotX, int hotY)
        {
            IntPtr out_cursor;
            Love2dDll.wrap_love_dll_mouse_newCursor(imageData.p, hotX, hotY, out out_cursor);
            return LoveObject.NewObject<Cursor>(out_cursor);
        }
        //// end raw new

        public static bool Init()
        {
            return Love2dDll.wrap_love_dll_open_love_mouse();
        }

        public static Cursor GetSystemCursor(Cursor.SystemCursor sysctype)
        {
            IntPtr out_cursor = IntPtr.Zero;
            Love2dDll.wrap_love_dll_mouse_getSystemCursor((int)sysctype, out out_cursor);
            return LoveObject.NewObject<Cursor>(out_cursor);
        }
        public static void SetCursor(Cursor cursor)
        {
            Love2dDll.wrap_love_dll_mouse_setCursor(cursor.p);
        }
        public static Cursor GetCursor()
        {
            IntPtr out_cursor = IntPtr.Zero;
            Love2dDll.wrap_love_dll_mouse_getCursor(out out_cursor);
            return LoveObject.NewObject<Cursor>(out_cursor);
        }
        public static bool HasCursor()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_mouse_hasCursor(out out_result);
            return out_result;
        }
        public static double GetX()
        {
            double out_x = 0;
            Love2dDll.wrap_love_dll_mouse_getX(out out_x);
            return out_x;
        }
        public static double GetY()
        {
            double out_y = 0;
            Love2dDll.wrap_love_dll_mouse_getY(out out_y);
            return out_y;
        }
        public static void GetPosition(out double out_x, out double out_y)
        {
            Love2dDll.wrap_love_dll_mouse_getPosition(out out_x, out out_y);
        }
        public static void SetX(double x)
        {
            Love2dDll.wrap_love_dll_mouse_setX(x);
        }
        public static void SetY(double y)
        {
            Love2dDll.wrap_love_dll_mouse_setY(y);
        }
        public static void SetPosition(double x, double y)
        {
            Love2dDll.wrap_love_dll_mouse_setPosition(x, y);
        }
        public static bool IsDown(int buttonIndex)
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_mouse_isDown(buttonIndex, out out_result);
            return out_result;
        }
        public static void SetVisible(bool visible)
        {
            Love2dDll.wrap_love_dll_mouse_setVisible(visible);
        }
        public static bool IsVisible()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_mouse_isVisible(out out_result);
            return out_result;
        }
        public static void SetGrabbed(bool grabbed)
        {
            Love2dDll.wrap_love_dll_mouse_setGrabbed(grabbed);
        }
        public static bool IsGrabbed()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_mouse_isGrabbed(out out_result);
            return out_result;
        }
        public static bool SetRelativeMode(bool enable)
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_mouse_setRelativeMode(enable, out out_result);
            return out_result;
        }
        public static bool GetRelativeMode()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_mouse_getRelativeMode(out out_result);
            return out_result;
        }
    }

    public partial class Keyboard
    {
        public enum Key : int
        {
            Unknown,
            Return,
            Escape,
            Backspace,
            Tab,
            Space,
            Exclaim,
            Quotedbl,
            Hash,
            Percent,
            Dollar,
            Ampersand,
            Quote,
            LeftParen,
            RightParen,
            Asterisk,
            Plus,
            Comma,
            Minus,
            Period,
            Slash,

            Number0,
            Number1,
            Number2,
            Number3,
            Number4,
            Number5,
            Number6,
            Number7,
            Number8,
            Number9,

            Colon,
            SemiColon,

            Less,
            Equals,
            Greater,
            Question,
            At,

            LeftBracket,
            Backslash,
            RightBracket,
            Caret,
            Underscore,
            Backquote,

            A,
            B,
            C,
            D,
            E,
            F,
            G,
            H,
            I,
            J,
            K,
            L,
            M,
            N,
            O,
            P,
            Q,
            R,
            S,
            T,
            U,
            V,
            W,
            X,
            Y,
            Z,

            CapsLock,

            F1,
            F2,
            F3,
            F4,
            F5,
            F6,
            F7,
            F8,
            F9,
            F10,
            F11,
            F12,

            PrintScreen,
            ScrollLock,

            Pause,
            Insert,
            Home,
            PageUp,
            Delete,
            End,
            PageDown,
            Right,
            Left,
            Down,
            Up,

            NumLockClear,
            KeypadDivide,
            KeypadMultiply,
            KeypadMinus,
            KeypadPlus,
            KeypadEnter,
            Keypad1,
            Keypad2,
            Keypad3,
            Keypad4,
            Keypad5,
            Keypad6,
            Keypad7,
            Keypad8,
            Keypad9,
            Keypad0,
            KeypadPeriod,
            KeypadComma,
            KeypadEquals,

            Application,
            Power,

            F13,
            F14,
            F15,
            F16,
            F17,
            F18,
            F19,
            F20,
            F21,
            F22,
            F23,
            F24,

            Execute,
            Help,
            Menu,
            Select,
            Stop,
            Again,
            Undo,
            Cut,
            Copy,
            Paste,
            Find,
            Mute,

            VolumeUp,
            VolumeDown,
            Alterase,

            Sysreq,
            Cancel,
            Clear,
            Prior,
            Return2,
            Separator,
            Out,
            Oper,

            ClearAgain,

            ThousandsSeparator,
            DecimalSeparator,
            CurrencyUnit,
            CurrencySubunit,

            LCtrl,
            LShift,
            LAlt,
            LGUI,
            RCtrl,
            RShift,
            RAlt,
            RGUI,

            Mode,

            AudioNext,
            AudioPrev,
            AudioStop,
            AudioPlay,
            AudioMute,
            MediaSelect,

            WWW,
            Mail,
            Calculator,
            Computer,
            AppSearch,
            AppHome,
            AppBack,
            AppForward,
            AppStop,
            AppRefresh,
            AppBookmarks,

            BrightnessDown,
            BrightnessUp,
            DisplaySwitch,
            KBDILLUMToggle,
            KBDILLUMDown,
            KBDILLUMUp,
            Eject,
            Sleep,

        };
        public enum Scancode : int
        {
            Unknow,

            A,
            B,
            C,
            D,
            E,
            F,
            G,
            H,
            I,
            J,
            K,
            L,
            M,
            N,
            O,
            P,
            Q,
            R,
            S,
            T,
            U,
            V,
            W,
            X,
            Y,
            Z,

            Number1,
            Number2,
            Number3,
            Number4,
            Number5,
            Number6,
            Number7,
            Number8,
            Number9,
            Number0,

            Return,
            Escape,
            Backspace,
            Tab,
            Space,
            Minus,
            Equals,

            LeftBracket,
            RightBracket,

            Backslash,
            Nonushash,
            Semicolon,
            Apostrophe,
            Grave,
            Comma,
            Period,
            Slash,
            Capslock,


            F1,
            F2,
            F3,
            F4,
            F5,
            F6,
            F7,
            F8,
            F9,
            F10,
            F11,
            F12,

            PrintScreen,
            ScrollLock,
            Pause,
            Insert,
            Home,
            PageUp,
            Delete,
            End,
            PageDown,
            Right,
            Left,
            Down,
            Up,

            NumLockClear,
            KeypadDivide,
            KeypadMultiply,
            KeypadMinus,
            KeypadPlus,
            KeypadEnter,
            Keypad1,
            Keypad2,
            Keypad3,
            Keypad4,
            Keypad5,
            Keypad6,
            Keypad7,
            Keypad8,
            Keypad9,
            Keypad0,
            KeypadPeriod,

            NonusBackslash,
            Application,
            Power,

            KeypadEquals,
            F13,
            F14,
            F15,
            F16,
            F17,
            F18,
            F19,
            F20,
            F21,
            F22,
            F23,
            F24,

            Execute,
            Help,
            Menu,
            Select,
            Stop,
            Again,
            Undo,
            Cut,
            Copy,
            Paste,
            Find,
            Mute,

            VolumeUp,
            VolumeDown,
            KeypadComma,
            KeypadEqualSAS400,

            International1,
            International2,
            International3,
            International4,
            International5,
            International6,
            International7,
            International8,
            International9,
            Lang1,
            Lang2,
            Lang3,
            Lang4,
            Lang5,
            Lang6,
            Lang7,
            Lang8,
            Lang9,

            Alterase,
            Sysreq,
            Cancel,
            Clear,
            Prior,
            Return2,
            Separator,
            Out,
            Oper,
            ClearaAain,
            Crsel,
            Exsel,


            Keypad00,
            Keypad000,

            ThousandsSeparator,
            DecimalSeparator,
            CurrencyUnit,
            CurrencySubunit,

            KeypadLeftParen,
            KeypadRightParen,
            KeypadLeftBrace,
            KeypadRightBrace,
            KeypadTab,
            KeypadBackspace,

            KeypadA,
            KeypadB,
            KeypadC,
            KeypadD,
            KeypadE,
            KeypadF,

            KeypadXOR,
            KeypadPower,
            KeypadPercent,
            KeypadLess,
            KeypadGreater,
            KeypadAmpersand,
            KeypadDblampersand,
            KeypadVerticalBar,
            KeypadDblverticalBar,
            KeypadColon,
            KeypadHash,
            KeypadSpace,
            KeypadAt,
            KeypadExclam,
            KeypadMemstore,
            KeypadMemrecall,
            KeypadMemclear,
            KeypadMemadd,
            KeypadMemsubtract,
            KeypadMemmultiply,
            KeypadMemdivide,
            KeypadPlusminus,
            KeypadClear,
            KeypadClearEntry,
            KeypadBinary,
            KeypadOctal,
            KeypadDecimal,
            KeypadHexaecimal,

            LCtrl,
            LShift,
            LAlt,
            LGUI,
            RCtrl,
            RShift,
            RAlt,
            RGUI,

            Mode,

            AudioNext,
            AudioPrev,
            AudioStop,
            AudioPlay,
            AudioMute,
            MediaSelect,
            WWW,
            Mail,
            Calculator,
            Computer,
            ACSearch,
            ACHome,
            ACBack,
            ACForward,
            ACStop,
            ACRefresh,
            ACBookmarks,

            BrightnessDown,
            BrightnessUp,
            DisplaySwitch,
            KBDILLUMToggle,
            KBDILLUMDown,
            KBDILLUMUp,
            Eject,
            Sleep,

            App1,
            App2,

        };

        public static bool Init()
        {
            return Love2dDll.wrap_love_dll_keyboard_open_love_keyboard();
        }
        public static void SetKeyRepeat(bool enable)
        {
            Love2dDll.wrap_love_dll_keyboard_setKeyRepeat(enable);
        }
        public static bool HasKeyRepeat()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_keyboard_hasKeyRepeat(out out_result);
            return out_result;
        }
        public static bool IsDown(Key key_type)
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_keyboard_isDown((int)key_type, out out_result);
            return out_result;
        }
        public static bool IsScancodeDown(Scancode scancode_type)
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_keyboard_isScancodeDown((int)scancode_type, out out_result);
            return out_result;
        }
        public static Scancode GetScancodeFromKey(Key key_type)
        {
            int out_scancode_type = 0;
            Love2dDll.wrap_love_dll_keyboard_getScancodeFromKey((int)key_type, out out_scancode_type);
            return (Scancode)out_scancode_type;
        }
        public static Key GetKeyFromScancode(Scancode scancode_type)
        {
            int out_key_type = 0;
            Love2dDll.wrap_love_dll_keyboard_getKeyFromScancode((int)scancode_type, out out_key_type);
            return (Key)out_key_type;
        }
        public static void SetTextInput(bool enable)
        {
            Love2dDll.wrap_love_dll_keyboard_setTextInput(enable);
        }
        public static void SetTextInput_xywh(bool enable, double x, double y, double w, double h)
        {
            Love2dDll.wrap_love_dll_keyboard_setTextInput_xywh(enable, x, y, w, h);
        }
        public static bool HasTextInput()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_keyboard_hasTextInput(out out_result);
            return out_result;
        }
        public static bool HasScreenKeyboard()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_keyboard_hasScreenKeyboard(out out_result);
            return out_result;
        }


    }

    public partial class Touch
    {
        public struct TouchInfo
        {
            public long index;
            public double x, y, dx, dy, pressure;
        }

        public static void Init()
        {
            Love2dDll.wrap_love_dll_touch_open_love_touch();
        }
        public static TouchInfo[] GetTouches()
        {
            IntPtr out_indexs;IntPtr out_x;IntPtr out_y;IntPtr out_dx;IntPtr out_dy;IntPtr out_pressure;int out_length;
            Love2dDll.wrap_love_dll_touch_open_love_getTouches(out out_indexs, out out_x, out out_y, out out_dx, out out_dy, out out_pressure, out out_length);

            var indexs = DllTool.readLongsAndRelease(out_indexs, out_length);
            var x = DllTool.readDoublesAndRelease(out_x, out_length);
            var y = DllTool.readDoublesAndRelease(out_y, out_length);
            var dx = DllTool.readDoublesAndRelease(out_dx, out_length);
            var dy = DllTool.readDoublesAndRelease(out_dy, out_length);
            var pressure = DllTool.readDoublesAndRelease(out_pressure, out_length);

            TouchInfo[] infos = new TouchInfo[out_length];
            for (int i = 0; i < out_length; i++)
            {
                infos[i].index = indexs[i];
                infos[i].x = x[i];
                infos[i].y = y[i];
                infos[i].dx = dx[i];
                infos[i].dy = dy[i];
                infos[i].pressure = pressure[i];
            }

            return infos;
        }
        public static void GetToucheInfo(long idx, out double out_x, out double out_y, out double out_dx, out double out_dy, out double out_pressure)
        {
            Love2dDll.wrap_love_dll_touch_open_love_getToucheInfo(idx, out out_x, out out_y, out out_dx, out out_dy, out out_pressure);
        }

    }

    public partial class Joystick
    {
        public static void Init()
        {
            Love2dDll.wrap_love_dll_joystick_open_love_joystick();
        }
        public Joystick[] GetJoysticks()
        {
            IntPtr out_sticks;
            int out_sticks_lenght;
            Love2dDll.wrap_love_dll_joystick_getJoysticks(out out_sticks, out out_sticks_lenght);

            return DllTool.readIntPtrsWithConvertAndRelease<Joystick>(out_sticks, out_sticks_lenght);
        }
        public static int GetIndex(Joystick j)
        {
            int out_index = 0;
            Love2dDll.wrap_love_dll_joystick_getIndex(j.p, out out_index);
            return out_index;
        }
        public static int GetJoystickCount()
        {
            int out_sticks_lenght = 0;
            Love2dDll.wrap_love_dll_joystick_getJoystickCount(out out_sticks_lenght);
            return out_sticks_lenght;
        }
        public static bool SetGamepadMapping(byte[] guid, Joystick.InputType gp_inputType_type, Joystick.InputType j_inputType_type, int inputIndex, Joystick.Hat hat_type)
        {
            bool out_success = false;
            Love2dDll.wrap_love_dll_joystick_setGamepadMapping(guid, (int)gp_inputType_type, (int)j_inputType_type, inputIndex, (int)hat_type, out out_success);
            return out_success;
        }
        public static bool LoadGamepadMappings(byte[] str)
        {
            bool out_success = false;
            Love2dDll.wrap_love_dll_joystick_loadGamepadMappings(str, out out_success);
            return out_success;
        }
        public static string SaveGamepadMappings()
        {
            IntPtr out_str = IntPtr.Zero;
            Love2dDll.wrap_love_dll_joystick_saveGamepadMappings(out out_str);
            return DllTool.WSToStringAndRelease(out_str);
        }

    }

    public partial class Event
    {
        private enum WrapEventType
        {
            WRAP_EVENT_TYPE_UNKNOW,

            WRAP_EVENT_TYPE_KEY,
            WRAP_EVENT_TYPE_MOUSE_BUTTON,
            WRAP_EVENT_TYPE_MOUSE_MOTION,
            WRAP_EVENT_TYPE_MOUSE_WHEEL,

            WRAP_EVENT_TYPE_TOUCH_MOVED,
            WRAP_EVENT_TYPE_TOUCH_PRESSED,
            WRAP_EVENT_TYPE_TOUCH_RELEASED,

            WRAP_EVENT_TYPE_JOYSTICK_BUTTON,
            WRAP_EVENT_TYPE_JOYSTICK_AXIS_MOTION,
            WRAP_EVENT_TYPE_JOYSTICK_HAT_MOTION,
            WRAP_EVENT_TYPE_JOYSTICK_CONTROLLER_BUTTON,
            WRAP_EVENT_TYPE_JOYSTICK_CONTROLLER_AXIS_MOTION,
            WRAP_EVENT_TYPE_JOYSTICK_DEVICE_ADDED_OR_REMOVED,

            WRAP_EVENT_TYPE_TEXTINPUT,
            WRAP_EVENT_TYPE_TEXTEDITING,

            WRAP_EVENT_TYPE_WINDOW_VISIBLE,
            WRAP_EVENT_TYPE_WINDOW_ENTER_OR_LEAVE,
            WRAP_EVENT_TYPE_WINDOW_SHOWN_OR_HIDDEN,
            WRAP_EVENT_TYPE_WINDOW_RESIZED,
            WRAP_EVENT_TYPE_DROPPED,

            WRAP_EVENT_TYPE_LOWMEMORY,
            WRAP_EVENT_TYPE_QUIT,
        };

        public static void Quit(int exitStatus = 0)
        {
            System.Environment.Exit(exitStatus);
        }

        public static void Init()
        {
            Love2dDll.wrap_love_dll_event_open_love_event();
        }

        public static void PollOrWait(bool isPoll, out bool out_hasEvent, out int out_event_type, out bool out_down_or_up, out bool out_bool, out int out_idx, out int out_enum1_type, out int out_enum2_type, out string out_string, out Int4 out_int4, out Float4 out_float4, out float out_float_value, out Joystick out_joystick)
        {
            IntPtr out_str;
            IntPtr out_joystick_ptr;


            if (isPoll)
                Love2dDll.wrap_love_dll_event_poll(out out_hasEvent, out out_event_type, out out_down_or_up, out out_bool, out out_idx, out out_enum1_type, out out_enum2_type, out out_str, out out_int4, out out_float4, out out_float_value, out out_joystick_ptr);
            else
                Love2dDll.wrap_love_dll_event_wait(out out_hasEvent, out out_event_type, out out_down_or_up, out out_bool, out out_idx, out out_enum1_type, out out_enum2_type, out out_str, out out_int4, out out_float4, out out_float_value, out out_joystick_ptr);


            out_string = DllTool.WSToStringAndRelease(out_str);
            out_joystick = LoveObject.NewObject<Joystick>(out_joystick_ptr);
        }

        private static void DoHandleEvent(Scene scene, int out_event_type, bool out_down_or_up, bool out_bool, int out_idx, int out_enum1_type, int out_enum2_type, string out_string, Int4 out_int4, Float4 out_float4, float out_float_value, Joystick out_joystick)
        {
            switch ((WrapEventType)out_event_type)
            {
                case WrapEventType.WRAP_EVENT_TYPE_UNKNOW: { } break;
                case WrapEventType.WRAP_EVENT_TYPE_KEY:
                    {
                        if (out_down_or_up) scene.KeyPressed((Keyboard.Key)out_enum1_type, (Keyboard.Scancode)out_enum2_type, out_bool);
                        else scene.KeyReleased((Keyboard.Key)out_enum1_type, (Keyboard.Scancode)out_enum2_type);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_MOUSE_BUTTON:
                    {
                        if (out_down_or_up) scene.MousePressed(out_float4.x, out_float4.y, out_idx, out_bool);
                        else scene.MouseReleased(out_float4.x, out_float4.y, out_idx, out_bool);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_MOUSE_MOTION:
                    {
                        scene.MouseMoved(out_float4.x, out_float4.y, out_float4.w, out_float4.z, out_bool);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_MOUSE_WHEEL:
                    {
                        scene.WheelMoved(out_int4.x, out_int4.y);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_TOUCH_MOVED:
                    {
                        scene.TouchMoved(out_idx, out_float4.x, out_float4.y, out_float4.w, out_float4.z, out_float_value);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_TOUCH_PRESSED:
                    {
                        scene.TouchPressed(out_idx, out_float4.x, out_float4.y, out_float4.w, out_float4.z, out_float_value);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_TOUCH_RELEASED:
                    {
                        scene.TouchReleased(out_idx, out_float4.x, out_float4.y, out_float4.w, out_float4.z, out_float_value);
                    }
                    break;

                case WrapEventType.WRAP_EVENT_TYPE_JOYSTICK_BUTTON:
                    {
                        if (out_down_or_up) scene.JoystickPressed(out_joystick, out_idx);
                        else scene.JoystickReleased(out_joystick, out_idx);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_JOYSTICK_AXIS_MOTION:
                    {
                        scene.JoystickAxis(out_joystick, out_idx, out_float_value);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_JOYSTICK_HAT_MOTION:
                    {
                        scene.JoystickHat(out_joystick, out_idx, (Joystick.Hat)out_enum1_type);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_JOYSTICK_CONTROLLER_BUTTON:
                    {
                        if (out_down_or_up) scene.JoystickGamepadPressed(out_joystick, (Joystick.GamepadButton)out_enum1_type);
                        else scene.JoystickGamepadReleased(out_joystick, (Joystick.GamepadButton)out_enum1_type);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_JOYSTICK_CONTROLLER_AXIS_MOTION:
                    {
                        scene.JoystickGamepadAxis(out_joystick, (Joystick.GamepadAxis)out_enum1_type, out_float_value);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_JOYSTICK_DEVICE_ADDED_OR_REMOVED:
                    {
                        if (out_bool) scene.JoystickAdded(out_joystick);
                        else scene.JoystickRemoved(out_joystick);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_TEXTINPUT: {
                        scene.TextInput(out_string);
                    } break;
                case WrapEventType.WRAP_EVENT_TYPE_TEXTEDITING: {
                        scene.TextEditing(out_string, out_int4.x, out_int4.y);
                    } break;
                case WrapEventType.WRAP_EVENT_TYPE_WINDOW_VISIBLE: {
                        scene.WindowFocus(out_bool);
                    } break;
                case WrapEventType.WRAP_EVENT_TYPE_WINDOW_ENTER_OR_LEAVE: {
                        scene.MouseFocus(out_bool);
                    } break;
                case WrapEventType.WRAP_EVENT_TYPE_WINDOW_SHOWN_OR_HIDDEN: {
                        scene.WindowVisible(out_bool);
                    } break;
                case WrapEventType.WRAP_EVENT_TYPE_WINDOW_RESIZED: {
                        scene.WindowResize(out_int4.x, out_int4.y);
                    } break;
                case WrapEventType.WRAP_EVENT_TYPE_DROPPED: {
                        if (FileSystem.IsDirectory(out_string)) scene.DirectoryDropped(out_string);
                        else scene.FileDropped(FileSystem.NewFile(out_string));
                    } break;
                case WrapEventType.WRAP_EVENT_TYPE_LOWMEMORY: {
                        scene.LowMemory();
                    } break;
                case WrapEventType.WRAP_EVENT_TYPE_QUIT: {
                        if (scene.Quit()) Quit();
                    } break;
                default: break;
            }
        }

        public static bool Poll(Scene scene)
        {
            bool out_hasEvent;int out_event_type;bool out_down_or_up;bool out_bool;int out_idx;int out_enum1_type;int out_enum2_type;string out_string;Int4 out_int4;Float4 out_float4;float out_float_value;Joystick out_joystick;
            PollOrWait(true, out out_hasEvent, out out_event_type, out out_down_or_up, out out_bool, out out_idx, out out_enum1_type, out out_enum2_type, out out_string, out out_int4, out out_float4, out out_float_value, out out_joystick);

            if (out_hasEvent == false)
                return false;

            DoHandleEvent(scene, out_event_type, out_down_or_up, out_bool, out_idx, out_enum1_type, out_enum2_type, out_string, out_int4, out_float4, out_float_value, out_joystick);
            return true;
        }
    }

    public partial class FileSystem
    {
        //// raw *new*
        public static File NewFile(byte[] filename, File.Mode fmode_type = File.Mode.Read)
        {
            IntPtr out_file;
            Love2dDll.wrap_love_dll_filesystem_newFile(filename, (int)fmode_type, out out_file);
            return LoveObject.NewObject<File>(out_file);
        }
        public static FileData NewFileData(byte[] contents, byte[] filename, FileData.Decoder decoder_type)
        {
            IntPtr out_file;
            Love2dDll.wrap_love_dll_filesystem_newFileData_content(contents, contents.Length, filename, (int)decoder_type, out out_file);
            return LoveObject.NewObject<FileData>(out_file);
        }
        public static FileData NewFileData(File file)
        {
            IntPtr out_file;
            Love2dDll.wrap_love_dll_filesystem_newFileData_file(file.p, out out_file);
            return LoveObject.NewObject<FileData>(out_file);
        }
        //// end *new*

        public static bool Init(byte[] args)
        {
            Love2dDll.wrap_love_dll_filesystem_open_love_filesystem();
            return Love2dDll.wrap_love_dll_filesystem_init(args);
        }

        public static void SetFused(bool flag)
        {
            Love2dDll.wrap_love_dll_filesystem_setFused(flag);
        }
        public static bool IsFused()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_filesystem_isFused(out out_result);
            return out_result;
        }
        public static void SetAndroidSaveExternal(bool useExternal)
        {
            Love2dDll.wrap_love_dll_filesystem_setAndroidSaveExternal(useExternal);
        }
        public static void SetIdentity(byte[] arg, bool append = false)
        {
            Love2dDll.wrap_love_dll_filesystem_setIdentity(arg, append);
        }
        public static string GetIdentity()
        {
            IntPtr out_str = IntPtr.Zero;
            Love2dDll.wrap_love_dll_filesystem_getIdentity(out out_str);
            return DllTool.WSToStringAndRelease(out_str);
        }
        public static void SetSource(byte[] arg)
        {
            Love2dDll.wrap_love_dll_filesystem_setSource(arg);
        }
        public static string GetSource()
        {
            IntPtr out_str = IntPtr.Zero;
            Love2dDll.wrap_love_dll_filesystem_getSource(out out_str);
            return DllTool.WSToStringAndRelease(out_str);
        }
        public static bool Mount(byte[] archive, byte[] mountpoint, bool appendToPath)
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_filesystem_mount(archive, mountpoint, appendToPath, out out_result);
            return out_result;
        }
        public static bool Unmount(byte[] archive)
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_filesystem_unmount(archive, out out_result);
            return out_result;
        }
        public static string GetWorkingDirectory()
        {
            IntPtr out_str = IntPtr.Zero;
            Love2dDll.wrap_love_dll_filesystem_getWorkingDirectory(out out_str);
            return DllTool.WSToStringAndRelease(out_str);
        }
        public static string GetUserDirectory()
        {
            IntPtr out_str = IntPtr.Zero;
            Love2dDll.wrap_love_dll_filesystem_getUserDirectory(out out_str);
            return DllTool.WSToStringAndRelease(out_str);
        }
        public static string GetAppdataDirectory()
        {
            IntPtr out_str = IntPtr.Zero;
            Love2dDll.wrap_love_dll_filesystem_getAppdataDirectory(out out_str);
            return DllTool.WSToStringAndRelease(out_str);
        }
        public static string GetSaveDirectory()
        {
            IntPtr out_str = IntPtr.Zero;
            Love2dDll.wrap_love_dll_filesystem_getSaveDirectory(out out_str);
            return DllTool.WSToStringAndRelease(out_str);
        }
        public static string GetSourceBaseDirectory()
        {
            IntPtr out_str = IntPtr.Zero;
            Love2dDll.wrap_love_dll_filesystem_getSourceBaseDirectory(out out_str);
            return DllTool.WSToStringAndRelease(out_str);
        }
        public static string GetRealDirectory(byte[] filename)
        {
            IntPtr out_str = IntPtr.Zero;
            Love2dDll.wrap_love_dll_filesystem_getRealDirectory(filename, out out_str);
            return DllTool.WSToStringAndRelease(out_str);
        }
        public static string GetExecutablePath()
        {
            IntPtr out_str = IntPtr.Zero;
            Love2dDll.wrap_love_dll_filesystem_getExecutablePath(out out_str);
            return DllTool.WSToStringAndRelease(out_str);
        }
        public static bool Exists(byte[] arg)
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_filesystem_exists(arg, out out_result);
            return out_result;
        }
        public static bool IsDirectory(byte[] arg)
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_filesystem_isDirectory(arg, out out_result);
            return out_result;
        }
        public static bool IsFile(byte[] arg)
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_filesystem_isFile(arg, out out_result);
            return out_result;
        }
        public static bool IsSymlink(byte[] filename)
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_filesystem_isSymlink(filename, out out_result);
            return out_result;
        }
        public static void CreateDirectory(byte[] arg)
        {
            Love2dDll.wrap_love_dll_filesystem_createDirectory(arg);
        }
        public static void Remove(byte[] arg)
        {
            Love2dDll.wrap_love_dll_filesystem_remove(arg);
        }
        public static byte[] read(byte[] filename, long len = -1)
        {
            IntPtr out_data;
            uint out_data_length;
            Love2dDll.wrap_love_dll_filesystem_read(filename, len, out out_data, out out_data_length);
            return DllTool.readBytesAndRelease(out_data, out_data_length);
        }
        public static void Write(byte[] filename, byte[] input)
        {
            Love2dDll.wrap_love_dll_filesystem_write(filename, input, (uint)input.Length);
        }
        public static void Append(byte[] filename, byte[] input)
        {
            Love2dDll.wrap_love_dll_filesystem_append(filename, input, (uint)input.Length);
        }
        public static string[] getDirectoryItems(byte[] dir)
        {
            IntPtr out_wss = IntPtr.Zero;
            Love2dDll.wrap_love_dll_filesystem_getDirectoryItems(dir, out out_wss);
            return DllTool.WSSToStringListAndRelease(out_wss);
        }
        public static long GetLastModified(byte[] filename)
        {
            long out_time;
            Love2dDll.wrap_love_dll_filesystem_getLastModified(filename, out out_time);
            return out_time;
        }
        public static long GetSize(byte[] filename)
        {
            long out_size = 0;
            Love2dDll.wrap_love_dll_filesystem_getSize(filename, out out_size);
            return out_size;
        }
        public static void SetSymlinksEnabled(bool enable)
        {
            Love2dDll.wrap_love_dll_filesystem_setSymlinksEnabled(enable);
        }
        public static bool AreSymlinksEnabled()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_filesystem_areSymlinksEnabled(out out_result);
            return out_result;
        }
        public static string GetRequirePath()
        {
            IntPtr out_str = IntPtr.Zero;
            Love2dDll.wrap_love_dll_filesystem_getRequirePath(out out_str);
            return DllTool.WSToStringAndRelease(out_str);
        }
        public static void SetRequirePath(byte[] paths)
        {
            Love2dDll.wrap_love_dll_filesystem_setRequirePath(paths);
        }

    }

    public partial class Sound
    {
        //// raw *new*
        // filename -> file -> filedata -> decoder -> sounddata
        public static Decoder NewDecoder(FileData fdata, int buffersize = Decoder.DEFAULT_BUFFER_SIZE)
        {
            IntPtr out_ptr;
            Love2dDll.wrap_love_dll_sound_newDecoder_filedata(fdata.p, buffersize, out out_ptr);
            return LoveObject.NewObject<Decoder>(out_ptr);
        }
        public static Decoder NewDecoder(File file, int buffersize = Decoder.DEFAULT_BUFFER_SIZE)
        {
            IntPtr out_ptr;
            Love2dDll.wrap_love_dll_sound_newDecoder_file(file.p, buffersize, out out_ptr);
            return LoveObject.NewObject<Decoder>(out_ptr);
        }
        public static SoundData NewSoundData(Decoder decoder)
        {
            IntPtr out_soundData_ptr;
            Love2dDll.wrap_love_dll_sound_newSoundData_decoder(decoder.p, out out_soundData_ptr);
            return LoveObject.NewObject<SoundData>(out_soundData_ptr);
        }
        public static SoundData NewSoundData(int samples, int sampleRate = Decoder.DEFAULT_SAMPLE_RATE, int bits = Decoder.DEFAULT_BIT_DEPTH, int channels = Decoder.DEFAULT_CHANNELS)
        {
            IntPtr out_soundData_ptr;
            Love2dDll.wrap_love_dll_sound_newSoundData(samples, sampleRate, bits, channels, out out_soundData_ptr);
            return LoveObject.NewObject<SoundData>(out_soundData_ptr);
        }
        //// end *new*

        public static bool Init()
        {
            return Love2dDll.wrap_love_dll_sound_luaopen_love_sound();
        }
    }

    public partial class Audio
    {
        public enum DistanceModel
        {
            None,
            Inverse,
            Clamped,
            Linear,
            LinearClamped,
            Exponent,
            ExponentClamped,
        };

        //// raw *new*
        // filename -> file -> filedata -> decoder -> source 
        //             file -> sounddata -> 
        public static Source NewSource(Decoder decoder, Source.Type type = Source.Type.Stream)
        {
            IntPtr out_decoder_ptr;
            Love2dDll.wrap_love_dll_audio_newSource_decoder(decoder.p, (int)type, out out_decoder_ptr);
            return LoveObject.NewObject<Source>(out_decoder_ptr);
        }
        public static Source NewSource(SoundData sd, Source.Type type = Source.Type.Stream)
        {
            IntPtr out_decoder_ptr;
            Love2dDll.wrap_love_dll_audio_newSource_sounddata(sd.p, (int)type, out out_decoder_ptr);
            return LoveObject.NewObject<Source>(out_decoder_ptr);
        }
        //// end *new*



        public static bool Init()
        {
            return Love2dDll.wrap_love_dll_audio_open_love_audio();
        }

        public static int GetSourceCount()
        {
            int out_reslut = 0;
            Love2dDll.wrap_love_dll_audio_getSourceCount(out out_reslut);
            return out_reslut;
        }
        public static void Stop()
        {
            Love2dDll.wrap_love_dll_audio_stop();
        }
        public static void Pause()
        {
            Love2dDll.wrap_love_dll_audio_pause();
        }
        public static void Resume()
        {
            Love2dDll.wrap_love_dll_audio_resume();
        }
        public static void Rewind()
        {
            Love2dDll.wrap_love_dll_audio_rewind();
        }
        public static void SetVolume(float v)
        {
            Love2dDll.wrap_love_dll_audio_setVolume(v);
        }
        public static float GetVolume()
        {
            float out_volume = 0;
            Love2dDll.wrap_love_dll_audio_getVolume(out out_volume);
            return out_volume;
        }
        public static void SetPosition(float x, float y, float z)
        {
            Love2dDll.wrap_love_dll_audio_setPosition(x, y, z);
        }
        public static Float3 GetPosition()
        {
            float out_x, out_y, out_z;
            Love2dDll.wrap_love_dll_audio_getPosition(out out_x, out out_y, out out_z);
            return new Float3(out_x, out_y, out_z);
        }
        public static void SetOrientation(float x, float y, float z, float dx, float dy, float dz)
        {
            Love2dDll.wrap_love_dll_audio_setOrientation(x, y, z, dx, dy, dz);
        }
        public static void GetOrientation(out Float3 pos, out Float3 dir)
        {
            float out_x,out_y,out_z,out_dx,out_dy,out_dz;
            Love2dDll.wrap_love_dll_audio_getOrientation(out out_x, out out_y, out out_z, out out_dx, out out_dy, out out_dz);
            pos = new Float3(out_x, out_y, out_z);
            dir = new Float3(out_dx, out_dy, out_dz);
        }
        public static void SetVelocity(float x, float y, float z)
        {
            Love2dDll.wrap_love_dll_audio_setVelocity(x, y, z);
        }
        public static Float3 GetVelocity()
        {
            float out_x, out_y, out_z;
            Love2dDll.wrap_love_dll_audio_getVelocity(out out_x, out out_y, out out_z);
            return new Float3(out_x, out_y, out_z);
        }
        public static void SetDopplerScale(float scale)
        {
            Love2dDll.wrap_love_dll_audio_setDopplerScale(scale);
        }
        public static float GetDopplerScale()
        {
            float out_scale = 0;
            Love2dDll.wrap_love_dll_audio_getDopplerScale(out out_scale);
            return out_scale;
        }
        public static bool CanRecord()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_audio_canRecord(out out_result);
            return out_result;
        }
        public static void SetDistanceModel(int distancemodel_type)
        {
            Love2dDll.wrap_love_dll_audio_setDistanceModel(distancemodel_type);
        }
        public static int GetDistanceModel()
        {
            int out_distancemodel_type = 0;
            Love2dDll.wrap_love_dll_audio_getDistanceModel(out out_distancemodel_type);
            return out_distancemodel_type;
        }

    }

    public partial class Image // this is part of love module
    {
        //// raw new
        public static ImageData NewImageData(int w, int h, byte[] data)
        {
            if (w <= 0 || h <= 0)
            {
                throw new Exception("Invalid image size.");
            }

            IntPtr out_imagedata;
            Love2dDll.wrap_love_dll_image_newImageData_wh_data(w, h, data, data.Length, out out_imagedata);
            return LoveObject.NewObject<ImageData>(out_imagedata);
        }
        public static ImageData NewImageData(FileData data)
        {
            IntPtr out_imagedata;
            Love2dDll.wrap_love_dll_image_newImageData_filedata(data.p, out out_imagedata);
            return LoveObject.NewObject<ImageData>(out_imagedata);
        }
        public static CompressedImageData NewCompressedData(FileData data)
        {
            IntPtr out_compressedimagedata;
            var res = Love2dDll.wrap_love_dll_image_newCompressedData(data.p, out out_compressedimagedata);
            return LoveObject.NewObject<CompressedImageData>(out_compressedimagedata);
        }
        //// end raw new

        public static bool Init()
        {
            return Love2dDll.wrap_love_dll_image_open_love_image();
        }

        public static bool IsCompressed(FileData data)
        {
            bool out_result;
            Love2dDll.wrap_love_dll_image_isCompressed(data.p, out out_result);
            return out_result;
        }

    }

    public partial class Font // this is part of love module
    {
        //// raw new
        public static Rasterizer NewRasterizer(FileData fileData)
        {
            IntPtr out_reasterizer;
            Love2dDll.wrap_love_dll_font_newRasterizer(fileData.p, out out_reasterizer);
            return LoveObject.NewObject<Rasterizer>(out_reasterizer);
        }
        public static Rasterizer NewTrueTypeRasterizer(int size, TrueTypeRasterizer.Hinting hinting = TrueTypeRasterizer.Hinting.Normal)
        {
            IntPtr out_reasterizer;
            Love2dDll.wrap_love_dll_font_newTrueTypeRasterizer(size, (int)hinting, out out_reasterizer);
            return LoveObject.NewObject<Rasterizer>(out_reasterizer);
        }
        public static Rasterizer NewBMFontRasterizer(FileData fileData, params ImageData[] imageDatas)
        {
            IntPtr out_reasterizer;
            var datas = new IntPtr[imageDatas.Length];
            for (int i = 0; i < imageDatas.Length; i++) datas[i] = imageDatas[i].p;
            Love2dDll.wrap_love_dll_font_newBMFontRasterizer(fileData.p, datas, datas.Length, out out_reasterizer);
            return LoveObject.NewObject<Rasterizer>(out_reasterizer);
        }
        public static Rasterizer NewImageRasterizer(ImageData imageData, byte[] glyphs, int extraspacing)
        {
            IntPtr out_reasterizer;
            Love2dDll.wrap_love_dll_font_newImageRasterizer(imageData.p, glyphs, extraspacing, out out_reasterizer);
            return LoveObject.NewObject<Rasterizer>(out_reasterizer);
        }
        public static GlyphData NewGlyphData(Rasterizer rasterizer, byte[] glyph)
        {
            IntPtr out_GlyphData;
            Love2dDll.wrap_love_dll_font_newGlyphData_rasterizer_str(rasterizer.p, glyph, out out_GlyphData);
            return LoveObject.NewObject<GlyphData>(out_GlyphData);
        }
        public static GlyphData NewGlyphData(Rasterizer rasterizer, int glyphCode)
        {
            IntPtr out_GlyphData;
            Love2dDll.wrap_love_dll_font_newGlyphData_rasterizer_num(rasterizer.p, glyphCode, out out_GlyphData);
            return LoveObject.NewObject<GlyphData>(out_GlyphData);
        }
        //// end raw new
        
        public static bool Init()
        {
            return Love2dDll.wrap_love_dll_font_open_love_font();
        }
    }

    public partial class Video
    {
        //// raw new
        public static VideoStream NewVideoStream(File file)
        {
            IntPtr out_videostream;
            Love2dDll.wrap_love_dll_newVideoStream(file.p, out out_videostream);
            return LoveObject.NewObject<VideoStream>(out_videostream);
        }
        //// end raw new


        public static bool Init()
        {
            return Love2dDll.wrap_love_dll_video_open_love_video();
        }

    }

    public partial class Mathf
    {
        static RandomGenerator rg = null;
        public static void Init()
        {
            Love2dDll.wrap_love_dll_open_love_math();
            rg = NewRandomGenerator();
        }
        public static float Random(int l, int u)
        {
            double random = rg.Random();
            return (float)(System.Math.Floor(random * (u - l + 1) + l));
        }
        public static RandomGenerator Ext_getRandomGenerator()
        {
            return rg;
        }
        public static RandomGenerator NewRandomGenerator()
        {
            IntPtr out_RandomGenerator = IntPtr.Zero;
            Love2dDll.wrap_love_dll_math_newRandomGenerator(out out_RandomGenerator);
            return LoveObject.NewObject<RandomGenerator>(out_RandomGenerator);
        }
        public static BezierCurve NewBezierCurve(Float2[] points)
        {
            IntPtr out_BezierCurve = IntPtr.Zero;
            Love2dDll.wrap_love_dll_math_newBezierCurve(points, points.Length, out out_BezierCurve);
            return LoveObject.NewObject<BezierCurve>(out_BezierCurve);
        }
        public static Triangle[] triangulate(Float2[] points)
        {
            IntPtr out_triArray;
            int out_triCount;
            Love2dDll.wrap_love_dll_math_triangulate(points, points.Length, out out_triArray, out out_triCount);

            float[] buffer = DllTool.readFloatsAndRelease(out_triArray, out_triCount * 6);
            Triangle[] tri = new Triangle[out_triCount];

            for (int i = 0; i < out_triCount; i++)
            {
                tri[i].a = new Float2(buffer[i * 6 + 0], buffer[i * 6 + 1]);
                tri[i].b = new Float2(buffer[i * 6 + 2], buffer[i * 6 + 3]);
                tri[i].c = new Float2(buffer[i * 6 + 4], buffer[i * 6 + 5]);
            }

            return tri;
        }
        public static bool IsConvex(Float2[] points)
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_math_isConvex(points, points.Length, out out_result);
            return out_result;
        }
        public static float GammaToLinear(float gama)
        {
            float out_liner = 0;
            Love2dDll.wrap_love_dll_math_gammaToLinear(gama, out out_liner);
            return out_liner;
        }
        public static float LinearToGamma(float liner)
        {
            float out_gama = 0;
            Love2dDll.wrap_love_dll_math_linearToGamma(liner, out out_gama);
            return out_gama;
        }
        public static float Noise(float x)
        {
            float out_result = 0;
            Love2dDll.wrap_love_dll_math_noise_1(x, out out_result);
            return out_result;
        }
        public static float Noise(float x, float y)
        {
            float out_result = 0;
            Love2dDll.wrap_love_dll_math_noise_2(x, y, out out_result);
            return out_result;
        }
        public static float Noise(float x, float y, float z)
        {
            float out_result = 0;
            Love2dDll.wrap_love_dll_math_noise_3(x, y, z, out out_result);
            return out_result;
        }
        public static float Noise(float x, float y, float z, float w)
        {
            float out_result = 0;
            Love2dDll.wrap_love_dll_math_noise_4(x, y, z, w, out out_result);
            return out_result;
        }
        public static CompressedImageData Compress(byte[] datas, Compressor.Format format_type = Compressor.Format.LZ4, int level = -1)
        {
            IntPtr out_compressedData = IntPtr.Zero;
            Love2dDll.wrap_love_dll_math_compress_str(datas, datas.Length, (int)format_type, level, out out_compressedData);
            return LoveObject.NewObject<CompressedImageData>(out_compressedData);
        }
        public static CompressedImageData Compress(Data data, Compressor.Format format_type = Compressor.Format.LZ4, int level = -1)
        {
            IntPtr out_compressedData = IntPtr.Zero;
            Love2dDll.wrap_love_dll_math_compress_data(data.p, (int)format_type, level, out out_compressedData);
            return LoveObject.NewObject<CompressedImageData>(out_compressedData);
        }
        public static byte[] decompress(Data data)
        {
            IntPtr out_datas;
            int out_datas_length;
            Love2dDll.wrap_love_dll_math_decompress_data(data.p, out out_datas, out out_datas_length);

            return DllTool.readBytesAndRelease(out_datas, out_datas_length);
        }
        public static byte[] decompress_bytes(byte[] data, Compressor.Format format_type = Compressor.Format.LZ4, int level = -1)
        {
            IntPtr out_datas;
            int out_datas_length;
            Love2dDll.wrap_love_dll_math_decompress_bytes((int)format_type, data,data.Length, out out_datas, out out_datas_length);

            return DllTool.readBytesAndRelease(out_datas, out_datas_length);
        }

    }

    public partial class Graphics
    {
        public enum DrawMode : int
        {
            Line,
            Fill,
        };
        public enum ArcMode : int
        {
            Open,
            Closed,
            Pie,
            ARC_MAX_ENUM
        };
        public enum BlendMode : int
        {
            Alpha,
            Add,
            Subtract,
            Multiply,
            Lighten,
            Darken,
            Screen,
            Replace,
        };
        public enum BlendAlpha : int
        {
            Multiply,
            PreMultiplied,
        };
        public enum LineStyle : int
        {
            Rough,
            Smooth,
        };
        public enum LineJoin : int
        {
            None,
            Miter,
            Bevel,
        };
        public enum StencilAction : int
        {
            Replace,
            Increment,
            Decrement,
            IncrementWrap,
            DecrementWrap,
            Invert,
        };
        public enum CompareMode : int
        {
            Less,
            LEqual,
            Equal,
            GEqual,
            Greater,
            NotEqual,
            Always,
        };
        public enum Feature : int
        {
            MultiCanvasFormats,
            ClampZero,
            Lighten,
        };
        public enum Renderer : int
        {
            OpenGL= 0,
            OpenGLES,
        };
        public enum SystemLimit : int
        {
            PointSize,
            TextureSize,
            MultiCanvas,
            CanvasMSAA,
        };
        public enum StackType : int
        {
            All,
            Transform,
        };

        #region graphics Object Creation

        public static bool Init()
        {
            Love2dDll.wrap_love_dll_graphics_open_love_graphics();
            Love.Love2dGraphicsShaderBoot.init();
            return true;
        }

        public static Image NewImage(ImageData[] imageData, bool flagMipmaps = false, bool flagLinear = false)
        {
            IntPtr[] imageDataList = new IntPtr[imageData.Length];
            for (int i = 0; i < imageData.Length; i++)
            {
                imageDataList[i] = imageData[i].p;
            }

            IntPtr out_image = IntPtr.Zero;
            Love2dDll.wrap_love_dll_graphics_newImage_file(imageDataList, imageDataList.Length, flagMipmaps, flagLinear, out out_image);
            return LoveObject.NewObject<Image>(out_image);
        }
        public static Image NewImage(CompressedImageData[] compressedImageData, bool flagMipmaps = false, bool flagLinear = false)
        {
            IntPtr[] compressedImageDataList = new IntPtr[compressedImageData.Length];
            for (int i = 0; i < compressedImageData.Length; i++)
            {
                compressedImageDataList[i] = compressedImageData[i].p;
            }

            IntPtr out_image = IntPtr.Zero;
            Love2dDll.wrap_love_dll_graphics_newImage(compressedImageDataList, compressedImageDataList.Length, flagMipmaps, flagLinear, out out_image);
            return LoveObject.NewObject<Image>(out_image);
        }
        public static Quad NewQuad(double x, double y, double w, double h, double sw, double sh)
        {
            IntPtr out_quad = IntPtr.Zero;
            Love2dDll.wrap_love_dll_graphics_newQuad(x, y, w, h, sw, sh, out out_quad);
            return LoveObject.NewObject<Quad>(out_quad);
        }
        public static Font NewFont(Rasterizer rasterizer)
        {
            IntPtr out_font = IntPtr.Zero;
            Love2dDll.wrap_love_dll_graphics_newFont(rasterizer.p, out out_font);
            return LoveObject.NewObject<Font>(out_font);
        }
        public static SpriteBatch NewSpriteBatch(Texture texture, int maxSprites, Mesh.Usage usage_type)
        {
            IntPtr out_spriteBatch = IntPtr.Zero;
            Love2dDll.wrap_love_dll_graphics_newSpriteBatch(texture.p, maxSprites, (int)usage_type, out out_spriteBatch);
            return LoveObject.NewObject<SpriteBatch>(out_spriteBatch);
        }
        public static ParticleSystem NewParticleSystem(Texture texture, int buffer)
        {
            IntPtr out_particleSystem = IntPtr.Zero;
            Love2dDll.wrap_love_dll_graphics_newParticleSystem(texture.p, buffer, out out_particleSystem);
            return LoveObject.NewObject<ParticleSystem>(out_particleSystem);
        }
        public static Canvas NewCanvas(int width, int height, Canvas.Format format_type, int msaa)
        {
            IntPtr out_canvas = IntPtr.Zero;
            Love2dDll.wrap_love_dll_graphics_newCanvas(width, height, (int)format_type, msaa, out out_canvas);
            return LoveObject.NewObject<Canvas>(out_canvas);
        }
        public static Mesh NewMesh(Float2[] pos, Float2[] uv, Int4[] color, Mesh.Usage drawMode, Mesh.Usage usage)
        {
            IntPtr out_mesh = IntPtr.Zero;
            Love2dDll.wrap_love_dll_graphics_newMesh_specifiedVertices(pos, uv, color, pos.Length, (int)drawMode, (int)usage, out out_mesh);
            return LoveObject.NewObject<Mesh>(out_mesh);
        }
        public static Mesh NewMesh(int count, Mesh.Usage drawMode, Mesh.Usage usage)
        {
            IntPtr out_mesh = IntPtr.Zero;
            Love2dDll.wrap_love_dll_graphics_newMesh_count(count, (int)drawMode, (int)usage, out out_mesh);
            return LoveObject.NewObject<Mesh>(out_mesh);
        }
        public static Text NewText(Font font, ColoredString coloredStr)
        {
            IntPtr out_text = IntPtr.Zero;
            coloredStr.ExecResource((Tuple<BytePtr[], Int4[]> tmp) => {
                Love2dDll.wrap_love_dll_graphics_newText(font.p, tmp.Item1, tmp.Item2, coloredStr.items.Length, out out_text);
            });
            return LoveObject.NewObject<Text>(out_text);
        }
        public static Video NewVideo(VideoStream videoStream)
        {
            IntPtr out_video = IntPtr.Zero;
            Love2dDll.wrap_love_dll_graphics_newVideo(videoStream.p, out out_video);
            return LoveObject.NewObject<Video>(out_video);
        }
        public static ImageData NewScreenshot(bool copyAlpha)
        {
            IntPtr out_imageData = IntPtr.Zero;
            Love2dDll.wrap_love_dll_graphics_newScreenshot(copyAlpha, out out_imageData);
            return LoveObject.NewObject<ImageData>(out_imageData);
        }

        #endregion

        #region graphics State

        public static bool IsActive()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_graphics_isActive(out out_result);
            return out_result;
        }
        public static bool IsGammaCorrect()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_graphics_isGammaCorrect(out out_result);
            return out_result;
        }
        public static void SetScissor()
        {
            Love2dDll.wrap_love_dll_graphics_setScissor();
        }
        public static void SetScissor(int x, int y, int w, int h)
        {
            Love2dDll.wrap_love_dll_graphics_setScissor_xywh(x, y, w, h);
        }
        public static void IntersectScissor(int x, int y, int w, int h)
        {
            Love2dDll.wrap_love_dll_graphics_intersectScissor(x, y, w, h);
        }
        public static Int4 GetScissor()
        {
            int out_x, out_y, out_w, out_h;
            Love2dDll.wrap_love_dll_graphics_getScissor(out out_x, out out_y, out out_w, out out_h);
            return new Int4(out_x, out_y, out_w, out_h);
        }
        public static void SetStencilTest(CompareMode compare_type, int compareValue)
        {
            Love2dDll.wrap_love_dll_graphics_setStencilTest((int)compare_type, compareValue);
        }
        public static void GetStencilTest(out CompareMode out_compare_type, out int out_compareValue)
        {
            int compare_type = 0;
            Love2dDll.wrap_love_dll_graphics_getStencilTest(out compare_type, out out_compareValue);
            out_compare_type = (CompareMode)compare_type;
        }
        public static void SetColor(int r, int g, int b, int a = 255)
        {
            Love2dDll.wrap_love_dll_graphics_setColor(r, g, b, a);
        }
        public static Int4 GetColor()
        {
            int out_r, out_g, out_b, out_a;
            Love2dDll.wrap_love_dll_graphics_getColor(out out_r, out out_g, out out_b, out out_a);
            return new Int4(out_r, out_g, out_b, out_a);
        }
        public static void SetBackgroundColor(int r, int g, int b, int a = 255)
        {
            Love2dDll.wrap_love_dll_graphics_setBackgroundColor(r, g, b, a);
        }
        public static Int4 GetBackgroundColor()
        {
            int out_r, out_g, out_b, out_a;
            Love2dDll.wrap_love_dll_graphics_getBackgroundColor(out out_r, out out_g, out out_b, out out_a);
            return new Int4(out_r, out_g, out_b, out_a);
        }
        public static void SetFont(Font font)
        {
            Love2dDll.wrap_love_dll_graphics_setFont(font.p);
        }
        public static Font GetFont()
        {
            IntPtr out_font = IntPtr.Zero;
            Love2dDll.wrap_love_dll_graphics_getFont(out out_font);
            return LoveObject.NewObject<Font>(out_font);
        }
        public static void SetColorMask(bool r, bool g, bool b, bool a)
        {
            Love2dDll.wrap_love_dll_graphics_setColorMask(r, g, b, a);
        }
        public static void GetColorMask(out bool out_r, out bool out_g, out bool out_b, out bool out_a)
        {
            Love2dDll.wrap_love_dll_graphics_getColorMask(out out_r, out out_g, out out_b, out out_a);
        }
        public static void SetBlendMode(BlendMode blendMode_type, BlendAlpha blendAlphaMode_type)
        {
            Love2dDll.wrap_love_dll_graphics_setBlendMode((int)blendMode_type, (int)blendAlphaMode_type);
        }
        public static void GetBlendMode(out BlendMode out_blendMode_type, out BlendAlpha out_blendAlphaMode_type)
        {
            int blendMode_type = 0;
            int blendAlphaMode_type = 0;
            Love2dDll.wrap_love_dll_graphics_getBlendMode(out blendMode_type, out blendAlphaMode_type);
            out_blendMode_type = (BlendMode)blendMode_type;
            out_blendAlphaMode_type = (BlendAlpha)blendAlphaMode_type;
        }
        public static void SetDefaultFilter(Texture.FilterMode filterModeMagMin_type, Texture.FilterMode filterModeMag_type, int anisotropy)
        {
            Love2dDll.wrap_love_dll_graphics_setDefaultFilter((int)filterModeMagMin_type, (int)filterModeMag_type, anisotropy);
        }
        public static void GetDefaultFilter(out Texture.FilterMode out_filterModeMagMin_type, out Texture.FilterMode out_filterModeMag_type, out int out_anisotropy)
        {
            int filterModeMagMin_type = 0;
            int filterModeMag_type = 0;
            Love2dDll.wrap_love_dll_graphics_getDefaultFilter(out filterModeMagMin_type, out filterModeMag_type, out out_anisotropy);
            out_filterModeMagMin_type = (Texture.FilterMode)filterModeMagMin_type;
            out_filterModeMag_type = (Texture.FilterMode)filterModeMag_type;
        }
        public static void SetLineWidth(float width)
        {
            Love2dDll.wrap_love_dll_graphics_setLineWidth(width);
        }
        public static void SetLineStyle(LineStyle style_type)
        {
            Love2dDll.wrap_love_dll_graphics_setLineStyle((int)style_type);
        }
        public static void SetLineJoin(LineJoin join_type)
        {
            Love2dDll.wrap_love_dll_graphics_setLineJoin((int)join_type);
        }
        public static float GetLineWidth()
        {
            float out_result = 0;
            Love2dDll.wrap_love_dll_graphics_getLineWidth(out out_result);
            return out_result;
        }
        public static LineStyle GetLineStyle()
        {
            int out_lineStyle_type = 0;
            Love2dDll.wrap_love_dll_graphics_getLineStyle(out out_lineStyle_type);
            return (LineStyle)out_lineStyle_type;
        }
        public static LineJoin GetLineJoin()
        {
            int out_lineJoinStyle_type = 0;
            Love2dDll.wrap_love_dll_graphics_getLineJoin(out out_lineJoinStyle_type);
            return (LineJoin)out_lineJoinStyle_type;
        }
        public static void SetPointSize(float size)
        {
            Love2dDll.wrap_love_dll_graphics_setPointSize(size);
        }
        public static float GetPointSize()
        {
            float out_size = 0;
            Love2dDll.wrap_love_dll_graphics_getPointSize(out out_size);
            return out_size;
        }
        public static void SetWireframe(bool enable)
        {
            Love2dDll.wrap_love_dll_graphics_setWireframe(enable);
        }
        public static bool IsWireframe()
        {
            bool out_isWireFrame = false;
            Love2dDll.wrap_love_dll_graphics_isWireframe(out out_isWireFrame);
            return out_isWireFrame;
        }
        public static void SetCanvas(Canvas[] canvas)
        {
            IntPtr[] canvaList = new IntPtr[canvas.Length];
            for (int i = 0; i < canvas.Length; i++)
            {
                canvaList[i] = canvas[i].p;
            }

            Love2dDll.wrap_love_dll_graphics_setCanvas(canvaList, canvaList.Length);
        }
        public Canvas[] getCanvas()
        {
            IntPtr out_canvaList = IntPtr.Zero;
            int out_canvaList_length = 0;
            Love2dDll.wrap_love_dll_graphics_getCanvas(out out_canvaList, out out_canvaList_length);
            var buffer = DllTool.readIntPtrsAndRelease(out_canvaList, out_canvaList_length);

            Canvas[] canvas = new Canvas[buffer.Length];
            for (int i = 0; i < buffer.Length; i++)
            {
                canvas[i] = LoveObject.NewObject<Canvas>(buffer[i]);
            }

            return canvas;
        }
        public static void SetShader(Shader shader)
        {
            Love2dDll.wrap_love_dll_graphics_setShader(shader.p);
        }
        public static Shader GetShader()
        {
            IntPtr out_shader = IntPtr.Zero;
            Love2dDll.wrap_love_dll_graphics_getShader(out out_shader);
            return LoveObject.NewObject<Shader>(out_shader);
        }

        #endregion

        #region graphics Coordinate System

        public static void Push(StackType stack = StackType.Transform)
        {
            Love2dDll.wrap_love_dll_graphics_push((int)stack);
        }
        public static void Pop()
        {
            Love2dDll.wrap_love_dll_graphics_pop();
        }
        public static void Rotate(float angle)
        {
            Love2dDll.wrap_love_dll_graphics_rotate(angle);
        }
        public static void Scale(float sx, float sy)
        {
            Love2dDll.wrap_love_dll_graphics_scale(sx, sy);
        }
        public static void Translate(float x, float y)
        {
            Love2dDll.wrap_love_dll_graphics_translate(x, y);
        }
        public static void Shear(float kx, float ky)
        {
            Love2dDll.wrap_love_dll_graphics_shear(kx, ky);
        }
        public static void Origin()
        {
            Love2dDll.wrap_love_dll_graphics_origin();
        }

        #endregion

        #region graphics Drwing

        public static void Stencil(Action actionFunc, StencilAction stencilAction, int value, bool keepValue)
        {
            if (!keepValue)
                Love2dDll.wrap_love_dll_graphics_ext_stencil_clearStencil();

            Love2dDll.wrap_love_dll_graphics_ext_stencil_drawToStencilBuffer((int)stencilAction, value);

            actionFunc?.Invoke();

            Love2dDll.wrap_love_dll_graphics_ext_stencil_stopDrawToStencilBuffer();
        }

        public static void Clear(float r, float g, float b, float a)
        {
            Love2dDll.wrap_love_dll_graphics_clear_rgba(r, g, b, a);
        }
        public static void Clear(Float4[] colorList, bool[] enableList)
        {
            if (colorList.Length != enableList.Length)
            {
                throw new Exception("length of colorList and enableList should same !");
            }
            Love2dDll.wrap_love_dll_graphics_clear_rgbalist(colorList, enableList, colorList.Length);
        }
        public static void Discard(bool[] discardColors, bool discardStencil)
        {
            Love2dDll.wrap_love_dll_graphics_discard(discardColors, discardColors.Length, discardStencil);
        }
        public static void Present()
        {
            Love2dDll.wrap_love_dll_graphics_present();
        }

        public static bool Draw(Drawable drawable, float x, float y, float angle = 0, float sx = 1, float sy = 1, float ox = 0, float oy = 0, float kx = 0, float ky = 0)
        {
            return Love2dDll.wrap_love_dll_graphics_draw_drawable(drawable.p, x, y, angle, sx, sy, ox, oy, kx, ky);
        }
        public static bool Draw(Quad quad, Texture texture, float x, float y, float angle = 0, float sx = 1, float sy = 1, float ox = 0, float oy = 0, float kx = 0, float ky = 0)
        {
            return Love2dDll.wrap_love_dll_graphics_draw_texture_quad(quad.p, texture.p, x, y, angle, sx, sy, ox, oy, kx, ky);
        }
        public static void Print(string text, float x, float y, float angle = 0, float sx = 1, float sy = 1, float ox = 0, float oy = 0, float kx = 0, float ky = 0)
        {
            ColoredString coloredStr = DllTool.ToColoredStrings(text);

            coloredStr.ExecResource((Tuple<BytePtr[], Int4[]> tmp) =>{
                Love2dDll.wrap_love_dll_graphics_print(tmp.Item1, tmp.Item2, coloredStr.Length, x, y, angle, sx, sy, ox, oy, kx, ky);
            });
        }
        public static void Print(ColoredString coloredStr, float x, float y, float angle = 0, float sx = 1, float sy = 1, float ox = 0, float oy = 0, float kx = 0, float ky = 0)
        {
            coloredStr.ExecResource((Tuple<BytePtr[], Int4[]> tmp) =>{
                Love2dDll.wrap_love_dll_graphics_print(tmp.Item1, tmp.Item2, coloredStr.Length, x, y, angle, sx, sy, ox, oy, kx, ky);
            });
        }
        public static void Printf(ColoredString coloredStr, float x, float y, float wrap, Font.AlignMode align_type, float angle = 0, float sx = 1, float sy = 1, float ox = 0, float oy = 0, float kx = 0, float ky = 0)
        {
            coloredStr.ExecResource((Tuple<BytePtr[], Int4[]> tmp) =>{
                Love2dDll.wrap_love_dll_graphics_printf(tmp.Item1, tmp.Item2, coloredStr.Length, x, y, wrap, (int)align_type, angle, sx, sy, ox, oy, kx, ky);
            });
        }
        public static bool Rectangle(DrawMode mode_type, float x, float y, float w, float h)
        {
            return Love2dDll.wrap_love_dll_graphics_rectangle((int)mode_type, x, y, w, h);
        }
        public static bool Rectangle(DrawMode mode_type, float x, float y, float w, float h, float rx, float ry, int points)
        {
            return Love2dDll.wrap_love_dll_graphics_rectangle_with_rounded_corners((int)mode_type, x, y, w, h, rx, ry, points);
        }
        public static bool Circle(DrawMode mode_type, float x, float y, float radius, int points)
        {
            return Love2dDll.wrap_love_dll_graphics_circle((int)mode_type, x, y, radius, points);
        }
        public static bool Ellipse(DrawMode mode_type, float x, float y, float a, float b, int points)
        {
            return Love2dDll.wrap_love_dll_graphics_ellipse((int)mode_type, x, y, a, b, points);
        }
        public static bool Arc(DrawMode mode_type, int arcmode_type, float x, float y, float radius, float angle1, float angle2)
        {
            return Love2dDll.wrap_love_dll_graphics_arc((int)mode_type, arcmode_type, x, y, radius, angle1, angle2);
        }
        public static bool Arc(DrawMode mode_type, ArcMode arcmode_type, float x, float y, float radius, float angle1, float angle2, int points)
        {
            return Love2dDll.wrap_love_dll_graphics_arc_points((int)mode_type, (int)arcmode_type, x, y, radius, angle1, angle2, points);
        }
        public static bool Points(Float2[] coords)
        {
            return Love2dDll.wrap_love_dll_graphics_points(coords, coords.Length);
        }
        public static bool Points(Float2[] coords, Int4[] colors)
        {
            if (coords.Length != colors.Length)
            {
                throw new Exception("length of coords and colors should same");
            }
            return Love2dDll.wrap_love_dll_graphics_points_colors(coords, colors, coords.Length);
        }
        public static bool Line(Float2[] coords)
        {
            return Love2dDll.wrap_love_dll_graphics_line(coords, coords.Length);
        }
        public static bool Polygon(DrawMode mode_type, Float2[] coords)
        {
            return Love2dDll.wrap_love_dll_graphics_polygon((int)mode_type, coords, coords.Length);
        }

        #endregion

        #region graphics Window

        public static bool IsCreated()
        {
            bool out_reslut = false;
            Love2dDll.wrap_love_dll_graphics_isCreated(out out_reslut);
            return out_reslut;
        }
        public static int GetWidth()
        {
            int out_width = 0;
            Love2dDll.wrap_love_dll_graphics_getWidth(out out_width);
            return out_width;
        }
        public static int GetHeight()
        {
            int out_height = 0;
            Love2dDll.wrap_love_dll_graphics_getHeight(out out_height);
            return out_height;
        }


        #endregion

        #region graphics System Information
        public static bool GetSupported(Feature feature_type)
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_graphics_getSupported((int)feature_type, out out_result);
            return out_result;
        }
        public static bool GetCanvasFormats(Canvas.Format format_type)
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_graphics_getCanvasFormats((int)format_type, out out_result);
            return out_result;
        }
        public static bool GetCompressedImageFormats(CompressedImageData.Format format_type)
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_graphics_getCompressedImageFormats((int)format_type, out out_result);
            return out_result;
        }
        public static void GetRendererInfo(out string name, out string version, out string vendor, out string device)
        {
            IntPtr out_wss;
            Love2dDll.wrap_love_dll_graphics_getRendererInfo(out out_wss);
            var infos = DllTool.WSSToStringListAndRelease(out_wss);

            name = infos[0];
            version = infos[1];
            vendor = infos[2];
            device = infos[3];
        }
        public static double GetSystemLimits(SystemLimit limit_type)
        {
            double out_result = 0;
            Love2dDll.wrap_love_dll_graphics_getSystemLimits((int)limit_type, out out_result);
            return out_result;
        }
        public static void GetStats(out int out_drawCalls, out int out_canvasSwitches, out int out_shaderSwitches, out int out_canvases, out int out_images, out int out_fonts, out int out_textureMemory)
        {
            Love2dDll.wrap_love_dll_graphics_getStats(out out_drawCalls, out out_canvasSwitches, out out_shaderSwitches, out out_canvases, out out_images, out out_fonts, out out_textureMemory);
        }

        #endregion
    }
}
