// Author : endlesstravel
// this part make basical ability to use love2d

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using size_t = System.UInt32;
using int64 = System.Int64;

using Love2d.Type;

namespace Love2d
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


        public static void checkPointZero(IntPtr ip)
        {
            if (ip == IntPtr.Zero)
            {
                throw new Exception(getLoveLastError());
            }
        }
        //public static void checkSuccess(bool flag)
        //{
        //    if (flag == false)
        //    {
        //        throw new Exception(getLoveLastError());
        //    }
        //}
        public static string getLoveLastError()
        {
            // 这里不能直接调用 WSToString(Love2dDll.wrap_love_dll_last_error());
            // 否则可能导致无限递归
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
                tarray[i] = LoveObject.newObject<T>(buffer[i]);
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
}

namespace Love2d.Module
{
    public partial class Common
    {
        public static string getVersion()
        {
            IntPtr out_str = IntPtr.Zero;
            Love2dDll.wrap_love_dll_common_getVersion(out out_str);
            return DllTool.WSToStringAndRelease(out_str);
        }
        public static string getVersionCodeName()
        {
            IntPtr out_str = IntPtr.Zero;
            Love2dDll.wrap_love_dll_common_getVersionCodeName(out out_str);
            return DllTool.WSToStringAndRelease(out_str);
        }

    }

    public partial class Timer
    {
        public static bool init()
        {
            return Love2dDll.wrap_love_dll_timer_open_timer();
        }
        public static void step()
        {
            Love2dDll.wrap_love_dll_timer_step();
        }
        public static float getDelta()
        {
            float out_delta = 0;
            Love2dDll.wrap_love_dll_timer_getDelta(out out_delta);
            return out_delta;
        }
        public static float getFPS()
        {
            float out_fps = 0;
            Love2dDll.wrap_love_dll_timer_getFPS(out out_fps);
            return out_fps;
        }
        public static float getAverageDelta()
        {
            float out_averageDelta = 0;
            Love2dDll.wrap_love_dll_timer_getAverageDelta(out out_averageDelta);
            return out_averageDelta;
        }
        public static void sleep(float t)
        {
            Love2dDll.wrap_love_dll_timer_sleep(t);
        }
        public static float getTime()
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
            FULLSCREEN_EXCLUSIVE,
            FULLSCREEN_DESKTOP,
            FULLSCREEN_MAX_ENUM
        };

        public enum MessageBoxType
        {
            MESSAGEBOX_ERROR,
            MESSAGEBOX_WARNING,
            MESSAGEBOX_INFO,
            MESSAGEBOX_MAX_ENUM
        };

        public static bool init()
        {
            var res = Love2dDll.wrap_love_dll_windows_open_love_window();
            return res;
        }

        public static void open_love_window()
        {
            Love2dDll.wrap_love_dll_windows_open_love_window();
        }
        public static int getDisplayCount()
        {
            int out_count = 0;
            Love2dDll.wrap_love_dll_windows_getDisplayCount(out out_count);
            return out_count;
        }
        public static string getDisplayName(int displayindex)
        {
            IntPtr out_name = IntPtr.Zero;
            Love2dDll.wrap_love_dll_windows_getDisplayName(displayindex, out out_name);
            return DllTool.WSToStringAndRelease(out_name);
        }
        public static void setMode(int width, int height)
        {
            Love2dDll.wrap_love_dll_windows_setMode_w_h(width, height);
        }
        public static void setMode_w_h_setting(int width, int height, bool fullscreen, FullscreenType fstype, bool vsync, int msaa, bool resizable, int minwidth, int minheight, bool borderless, bool centered, int display, bool highdpi, double refreshrate, bool useposition, int x, int y)
        {
            Love2dDll.wrap_love_dll_windows_setMode_w_h_setting(width, height, fullscreen, (int)fstype, vsync, msaa, resizable, minwidth, minheight, borderless, centered, display, highdpi, refreshrate, useposition, x, y);
        }
        public static void getMode(out int out_width, out int out_height, out bool out_fullscreen, out FullscreenType out_fstype, out bool out_vsync, out int out_msaa, out bool out_resizable, out int out_minwidth, out int out_minheight, out bool out_borderless, out bool out_centered, out int out_display, out bool out_highdpi, out double out_refreshrate, out bool out_useposition, out int out_x, out int out_y)
        {
            int outfstype;
            Love2dDll.wrap_love_dll_windows_getMode(out out_width, out out_height, out out_fullscreen, out outfstype, out out_vsync, out out_msaa, out out_resizable, out out_minwidth, out out_minheight, out out_borderless, out out_centered, out out_display, out out_highdpi, out out_refreshrate, out out_useposition, out out_x, out out_y);
            out_fstype = (FullscreenType)outfstype;
        }
        public static Int2[] getFullscreenModes(int displayindex)
        {
            IntPtr out_modes;
            int out_modes_length;
            Love2dDll.wrap_love_dll_windows_getFullscreenModes(displayindex, out out_modes, out out_modes_length);
            return DllTool.readInt2sAndRelease(out_modes, out_modes_length);
        }
        public static bool setFullscreen(bool fullscreen, FullscreenType fstype)
        {
            bool out_success = false;
            Love2dDll.wrap_love_dll_windows_setFullscreen(fullscreen, (int)fstype, out out_success);
            return out_success;
        }
        public static bool setFullscreen(bool fullscreen)
        {
            bool out_fullscreen; FullscreenType out_fstype;
            getFullscreen(out out_fullscreen, out out_fstype);
            return setFullscreen(fullscreen, out_fstype);
        }
        public static bool getFullscreen()
        {
            bool out_fullscreen; FullscreenType out_fstype;
            getFullscreen(out out_fullscreen, out out_fstype);
            return out_fullscreen;
        }
        public static void getFullscreen(out bool out_fullscreen, out FullscreenType out_fstype)
        {
            int t_out_fstype;
            Love2dDll.wrap_love_dll_windows_getFullscreen(out out_fullscreen, out t_out_fstype);
            out_fstype = (FullscreenType)t_out_fstype;
        }
        public static bool isOpen()
        {
            bool out_isopen = false;
            Love2dDll.wrap_love_dll_windows_isOpen(out out_isopen);
            return out_isopen;
        }
        public static void close()
        {
            Love2dDll.wrap_love_dll_windows_close();
        }
        public static void getDesktopDimensions(int displayindex, out int out_width, out int out_height)
        {
            Love2dDll.wrap_love_dll_windows_getDesktopDimensions(displayindex, out out_width, out out_height);
        }
        public static void setPosition(int x, int y, int displayindex)
        {
            Love2dDll.wrap_love_dll_windows_setPosition(x, y, displayindex);
        }
        public static void getPosition(out int out_x, out int out_y, out int out_displayindex)
        {
            Love2dDll.wrap_love_dll_windows_getPosition(out out_x, out out_y, out out_displayindex);
        }
        public static bool setIcon(ImageData imagedata)
        {
            bool out_success = false;
            Love2dDll.wrap_love_dll_windows_setIcon(imagedata.p, out out_success);
            return out_success;
        }
        public static ImageData getIcon()
        {
            IntPtr out_imagedata = IntPtr.Zero;
            Love2dDll.wrap_love_dll_windows_getIcon(out out_imagedata);
            return LoveObject.newObject<ImageData>(out_imagedata);
        }
        public static void setDisplaySleepEnabled(bool enable)
        {
            Love2dDll.wrap_love_dll_windows_setDisplaySleepEnabled(enable);
        }
        public static bool isDisplaySleepEnabled()
        {
            bool out_enable = false;
            Love2dDll.wrap_love_dll_windows_isDisplaySleepEnabled(out out_enable);
            return out_enable;
        }
        public static void setTitle(byte[] titleStr)
        {
            Love2dDll.wrap_love_dll_windows_setTitle(titleStr);
        }
        public static string getTitle()
        {
            IntPtr out_title = IntPtr.Zero;
            Love2dDll.wrap_love_dll_windows_getTitle(out out_title);
            return DllTool.WSToStringAndRelease(out_title);
        }
        public static bool hasFocus()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_windows_hasFocus(out out_result);
            return out_result;
        }
        public static bool hasMouseFocus()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_windows_hasMouseFocus(out out_result);
            return out_result;
        }
        public static bool isVisible()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_windows_isVisible(out out_result);
            return out_result;
        }
        public static double getPixelScale()
        {
            double out_result = 0;
            Love2dDll.wrap_love_dll_windows_getPixelScale(out out_result);
            return out_result;
        }
        public static double toPixels(double value)
        {
            double out_result = 0;
            Love2dDll.wrap_love_dll_windows_toPixels(value, out out_result);
            return out_result;
        }
        public static double fromPixels(double pixelvalue)
        {
            double out_result = 0;
            Love2dDll.wrap_love_dll_windows_fromPixels(pixelvalue, out out_result);
            return out_result;
        }
        public static void minimize()
        {
            Love2dDll.wrap_love_dll_windows_minimize();
        }
        public static void maximize()
        {
            Love2dDll.wrap_love_dll_windows_maximize();
        }
        public static bool isMaximized()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_windows_isMaximized(out out_result);
            return out_result;
        }
        public static bool showMessageBox(byte[] title, byte[] message, MessageBoxType msgbox_type, bool attachToWindow = true)
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_windows_showMessageBox(title, message, (int)msgbox_type, attachToWindow, out out_result);
            return out_result;
        }
        public static void requestAttention(bool continuous)
        {
            Love2dDll.wrap_love_dll_windows_requestAttention(continuous);
        }
        public static void windowToPixelCoords(out double out_x, out double out_y)
        {
            Love2dDll.wrap_love_dll_windows_windowToPixelCoords(out out_x, out out_y);
        }
        public static void pixelToWindowCoords(out double x, out double y)
        {
            Love2dDll.wrap_love_dll_windows_pixelToWindowCoords(out x, out y);
        }
        public static void getPixelDimensions(out int out_w, out int out_h)
        {
            Love2dDll.wrap_love_dll_windows_getPixelDimensions(out out_w, out out_h);
        }
    }

    public partial class Mouse
    {
        //// raw new
        public static Cursor wrap_love_dll_mouse_newCursor(ImageData imageData, int hotX, int hotY)
        {
            IntPtr out_cursor;
            Love2dDll.wrap_love_dll_mouse_newCursor(imageData.p, hotX, hotY, out out_cursor);
            return LoveObject.newObject<Cursor>(out_cursor);
        }
        //// end raw new

        public static bool init()
        {
            return Love2dDll.wrap_love_dll_open_love_mouse();
        }

        public static Cursor getSystemCursor(Cursor.SystemCursor sysctype)
        {
            IntPtr out_cursor = IntPtr.Zero;
            Love2dDll.wrap_love_dll_mouse_getSystemCursor((int)sysctype, out out_cursor);
            return LoveObject.newObject<Cursor>(out_cursor);
        }
        public static void setCursor(Cursor cursor)
        {
            Love2dDll.wrap_love_dll_mouse_setCursor(cursor.p);
        }
        public static Cursor getCursor()
        {
            IntPtr out_cursor = IntPtr.Zero;
            Love2dDll.wrap_love_dll_mouse_getCursor(out out_cursor);
            return LoveObject.newObject<Cursor>(out_cursor);
        }
        public static bool hasCursor()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_mouse_hasCursor(out out_result);
            return out_result;
        }
        public static double getX()
        {
            double out_x = 0;
            Love2dDll.wrap_love_dll_mouse_getX(out out_x);
            return out_x;
        }
        public static double getY()
        {
            double out_y = 0;
            Love2dDll.wrap_love_dll_mouse_getY(out out_y);
            return out_y;
        }
        public static void getPosition(out double out_x, out double out_y)
        {
            Love2dDll.wrap_love_dll_mouse_getPosition(out out_x, out out_y);
        }
        public static void setX(double x)
        {
            Love2dDll.wrap_love_dll_mouse_setX(x);
        }
        public static void setY(double y)
        {
            Love2dDll.wrap_love_dll_mouse_setY(y);
        }
        public static void setPosition(double x, double y)
        {
            Love2dDll.wrap_love_dll_mouse_setPosition(x, y);
        }
        public static bool isDown(int buttonIndex)
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_mouse_isDown(buttonIndex, out out_result);
            return out_result;
        }
        public static void setVisible(bool visible)
        {
            Love2dDll.wrap_love_dll_mouse_setVisible(visible);
        }
        public static bool isVisible()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_mouse_isVisible(out out_result);
            return out_result;
        }
        public static void setGrabbed(bool grabbed)
        {
            Love2dDll.wrap_love_dll_mouse_setGrabbed(grabbed);
        }
        public static bool isGrabbed()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_mouse_isGrabbed(out out_result);
            return out_result;
        }
        public static bool setRelativeMode(bool enable)
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_mouse_setRelativeMode(enable, out out_result);
            return out_result;
        }
        public static bool getRelativeMode()
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
            KEY_UNKNOWN,

            KEY_RETURN,
            KEY_ESCAPE,
            KEY_BACKSPACE,
            KEY_TAB,
            KEY_SPACE,
            KEY_EXCLAIM,
            KEY_QUOTEDBL,
            KEY_HASH,
            KEY_PERCENT,
            KEY_DOLLAR,
            KEY_AMPERSAND,
            KEY_QUOTE,
            KEY_LEFTPAREN,
            KEY_RIGHTPAREN,
            KEY_ASTERISK,
            KEY_PLUS,
            KEY_COMMA,
            KEY_MINUS,
            KEY_PERIOD,
            KEY_SLASH,
            KEY_0,
            KEY_1,
            KEY_2,
            KEY_3,
            KEY_4,
            KEY_5,
            KEY_6,
            KEY_7,
            KEY_8,
            KEY_9,
            KEY_COLON,
            KEY_SEMICOLON,
            KEY_LESS,
            KEY_EQUALS,
            KEY_GREATER,
            KEY_QUESTION,
            KEY_AT,

            KEY_LEFTBRACKET,
            KEY_BACKSLASH,
            KEY_RIGHTBRACKET,
            KEY_CARET,
            KEY_UNDERSCORE,
            KEY_BACKQUOTE,
            KEY_A,
            KEY_B,
            KEY_C,
            KEY_D,
            KEY_E,
            KEY_F,
            KEY_G,
            KEY_H,
            KEY_I,
            KEY_J,
            KEY_K,
            KEY_L,
            KEY_M,
            KEY_N,
            KEY_O,
            KEY_P,
            KEY_Q,
            KEY_R,
            KEY_S,
            KEY_T,
            KEY_U,
            KEY_V,
            KEY_W,
            KEY_X,
            KEY_Y,
            KEY_Z,

            KEY_CAPSLOCK,

            KEY_F1,
            KEY_F2,
            KEY_F3,
            KEY_F4,
            KEY_F5,
            KEY_F6,
            KEY_F7,
            KEY_F8,
            KEY_F9,
            KEY_F10,
            KEY_F11,
            KEY_F12,

            KEY_PRINTSCREEN,
            KEY_SCROLLLOCK,
            KEY_PAUSE,
            KEY_INSERT,
            KEY_HOME,
            KEY_PAGEUP,
            KEY_DELETE,
            KEY_END,
            KEY_PAGEDOWN,
            KEY_RIGHT,
            KEY_LEFT,
            KEY_DOWN,
            KEY_UP,

            KEY_NUMLOCKCLEAR,
            KEY_KP_DIVIDE,
            KEY_KP_MULTIPLY,
            KEY_KP_MINUS,
            KEY_KP_PLUS,
            KEY_KP_ENTER,
            KEY_KP_1,
            KEY_KP_2,
            KEY_KP_3,
            KEY_KP_4,
            KEY_KP_5,
            KEY_KP_6,
            KEY_KP_7,
            KEY_KP_8,
            KEY_KP_9,
            KEY_KP_0,
            KEY_KP_PERIOD,
            KEY_KP_COMMA,
            KEY_KP_EQUALS,

            KEY_APPLICATION,
            KEY_POWER,
            KEY_F13,
            KEY_F14,
            KEY_F15,
            KEY_F16,
            KEY_F17,
            KEY_F18,
            KEY_F19,
            KEY_F20,
            KEY_F21,
            KEY_F22,
            KEY_F23,
            KEY_F24,
            KEY_EXECUTE,
            KEY_HELP,
            KEY_MENU,
            KEY_SELECT,
            KEY_STOP,
            KEY_AGAIN,
            KEY_UNDO,
            KEY_CUT,
            KEY_COPY,
            KEY_PASTE,
            KEY_FIND,
            KEY_MUTE,
            KEY_VOLUMEUP,
            KEY_VOLUMEDOWN,

            KEY_ALTERASE,
            KEY_SYSREQ,
            KEY_CANCEL,
            KEY_CLEAR,
            KEY_PRIOR,
            KEY_RETURN2,
            KEY_SEPARATOR,
            KEY_OUT,
            KEY_OPER,
            KEY_CLEARAGAIN,

            KEY_THOUSANDSSEPARATOR,
            KEY_DECIMALSEPARATOR,
            KEY_CURRENCYUNIT,
            KEY_CURRENCYSUBUNIT,

            KEY_LCTRL,
            KEY_LSHIFT,
            KEY_LALT,
            KEY_LGUI,
            KEY_RCTRL,
            KEY_RSHIFT,
            KEY_RALT,
            KEY_RGUI,

            KEY_MODE,

            KEY_AUDIONEXT,
            KEY_AUDIOPREV,
            KEY_AUDIOSTOP,
            KEY_AUDIOPLAY,
            KEY_AUDIOMUTE,
            KEY_MEDIASELECT,
            KEY_WWW,
            KEY_MAIL,
            KEY_CALCULATOR,
            KEY_COMPUTER,
            KEY_APP_SEARCH,
            KEY_APP_HOME,
            KEY_APP_BACK,
            KEY_APP_FORWARD,
            KEY_APP_STOP,
            KEY_APP_REFRESH,
            KEY_APP_BOOKMARKS,

            KEY_BRIGHTNESSDOWN,
            KEY_BRIGHTNESSUP,
            KEY_DISPLAYSWITCH,
            KEY_KBDILLUMTOGGLE,
            KEY_KBDILLUMDOWN,
            KEY_KBDILLUMUP,
            KEY_EJECT,
            KEY_SLEEP,

            KEY_MAX_ENUM
        };
        public enum Scancode : int
        {
            SCANCODE_UNKNOWN,

            SCANCODE_A,
            SCANCODE_B,
            SCANCODE_C,
            SCANCODE_D,
            SCANCODE_E,
            SCANCODE_F,
            SCANCODE_G,
            SCANCODE_H,
            SCANCODE_I,
            SCANCODE_J,
            SCANCODE_K,
            SCANCODE_L,
            SCANCODE_M,
            SCANCODE_N,
            SCANCODE_O,
            SCANCODE_P,
            SCANCODE_Q,
            SCANCODE_R,
            SCANCODE_S,
            SCANCODE_T,
            SCANCODE_U,
            SCANCODE_V,
            SCANCODE_W,
            SCANCODE_X,
            SCANCODE_Y,
            SCANCODE_Z,

            SCANCODE_1,
            SCANCODE_2,
            SCANCODE_3,
            SCANCODE_4,
            SCANCODE_5,
            SCANCODE_6,
            SCANCODE_7,
            SCANCODE_8,
            SCANCODE_9,
            SCANCODE_0,

            SCANCODE_RETURN,
            SCANCODE_ESCAPE,
            SCANCODE_BACKSPACE,
            SCANCODE_TAB,
            SCANCODE_SPACE,

            SCANCODE_MINUS,
            SCANCODE_EQUALS,
            SCANCODE_LEFTBRACKET,
            SCANCODE_RIGHTBRACKET,
            SCANCODE_BACKSLASH,
            SCANCODE_NONUSHASH,
            SCANCODE_SEMICOLON,
            SCANCODE_APOSTROPHE,
            SCANCODE_GRAVE,
            SCANCODE_COMMA,
            SCANCODE_PERIOD,
            SCANCODE_SLASH,

            SCANCODE_CAPSLOCK,

            SCANCODE_F1,
            SCANCODE_F2,
            SCANCODE_F3,
            SCANCODE_F4,
            SCANCODE_F5,
            SCANCODE_F6,
            SCANCODE_F7,
            SCANCODE_F8,
            SCANCODE_F9,
            SCANCODE_F10,
            SCANCODE_F11,
            SCANCODE_F12,

            SCANCODE_PRINTSCREEN,
            SCANCODE_SCROLLLOCK,
            SCANCODE_PAUSE,
            SCANCODE_INSERT,
            SCANCODE_HOME,
            SCANCODE_PAGEUP,
            SCANCODE_DELETE,
            SCANCODE_END,
            SCANCODE_PAGEDOWN,
            SCANCODE_RIGHT,
            SCANCODE_LEFT,
            SCANCODE_DOWN,
            SCANCODE_UP,

            SCANCODE_NUMLOCKCLEAR,
            SCANCODE_KP_DIVIDE,
            SCANCODE_KP_MULTIPLY,
            SCANCODE_KP_MINUS,
            SCANCODE_KP_PLUS,
            SCANCODE_KP_ENTER,
            SCANCODE_KP_1,
            SCANCODE_KP_2,
            SCANCODE_KP_3,
            SCANCODE_KP_4,
            SCANCODE_KP_5,
            SCANCODE_KP_6,
            SCANCODE_KP_7,
            SCANCODE_KP_8,
            SCANCODE_KP_9,
            SCANCODE_KP_0,
            SCANCODE_KP_PERIOD,

            SCANCODE_NONUSBACKSLASH,
            SCANCODE_APPLICATION,
            SCANCODE_POWER,
            SCANCODE_KP_EQUALS,
            SCANCODE_F13,
            SCANCODE_F14,
            SCANCODE_F15,
            SCANCODE_F16,
            SCANCODE_F17,
            SCANCODE_F18,
            SCANCODE_F19,
            SCANCODE_F20,
            SCANCODE_F21,
            SCANCODE_F22,
            SCANCODE_F23,
            SCANCODE_F24,
            SCANCODE_EXECUTE,
            SCANCODE_HELP,
            SCANCODE_MENU,
            SCANCODE_SELECT,
            SCANCODE_STOP,
            SCANCODE_AGAIN,
            SCANCODE_UNDO,
            SCANCODE_CUT,
            SCANCODE_COPY,
            SCANCODE_PASTE,
            SCANCODE_FIND,
            SCANCODE_MUTE,
            SCANCODE_VOLUMEUP,
            SCANCODE_VOLUMEDOWN,
            SCANCODE_KP_COMMA,
            SCANCODE_KP_EQUALSAS400,

            SCANCODE_INTERNATIONAL1,
            SCANCODE_INTERNATIONAL2,
            SCANCODE_INTERNATIONAL3,
            SCANCODE_INTERNATIONAL4,
            SCANCODE_INTERNATIONAL5,
            SCANCODE_INTERNATIONAL6,
            SCANCODE_INTERNATIONAL7,
            SCANCODE_INTERNATIONAL8,
            SCANCODE_INTERNATIONAL9,
            SCANCODE_LANG1,
            SCANCODE_LANG2,
            SCANCODE_LANG3,
            SCANCODE_LANG4,
            SCANCODE_LANG5,
            SCANCODE_LANG6,
            SCANCODE_LANG7,
            SCANCODE_LANG8,
            SCANCODE_LANG9,

            SCANCODE_ALTERASE,
            SCANCODE_SYSREQ,
            SCANCODE_CANCEL,
            SCANCODE_CLEAR,
            SCANCODE_PRIOR,
            SCANCODE_RETURN2,
            SCANCODE_SEPARATOR,
            SCANCODE_OUT,
            SCANCODE_OPER,
            SCANCODE_CLEARAGAIN,
            SCANCODE_CRSEL,
            SCANCODE_EXSEL,

            SCANCODE_KP_00,
            SCANCODE_KP_000,
            SCANCODE_THOUSANDSSEPARATOR,
            SCANCODE_DECIMALSEPARATOR,
            SCANCODE_CURRENCYUNIT,
            SCANCODE_CURRENCYSUBUNIT,
            SCANCODE_KP_LEFTPAREN,
            SCANCODE_KP_RIGHTPAREN,
            SCANCODE_KP_LEFTBRACE,
            SCANCODE_KP_RIGHTBRACE,
            SCANCODE_KP_TAB,
            SCANCODE_KP_BACKSPACE,
            SCANCODE_KP_A,
            SCANCODE_KP_B,
            SCANCODE_KP_C,
            SCANCODE_KP_D,
            SCANCODE_KP_E,
            SCANCODE_KP_F,
            SCANCODE_KP_XOR,
            SCANCODE_KP_POWER,
            SCANCODE_KP_PERCENT,
            SCANCODE_KP_LESS,
            SCANCODE_KP_GREATER,
            SCANCODE_KP_AMPERSAND,
            SCANCODE_KP_DBLAMPERSAND,
            SCANCODE_KP_VERTICALBAR,
            SCANCODE_KP_DBLVERTICALBAR,
            SCANCODE_KP_COLON,
            SCANCODE_KP_HASH,
            SCANCODE_KP_SPACE,
            SCANCODE_KP_AT,
            SCANCODE_KP_EXCLAM,
            SCANCODE_KP_MEMSTORE,
            SCANCODE_KP_MEMRECALL,
            SCANCODE_KP_MEMCLEAR,
            SCANCODE_KP_MEMADD,
            SCANCODE_KP_MEMSUBTRACT,
            SCANCODE_KP_MEMMULTIPLY,
            SCANCODE_KP_MEMDIVIDE,
            SCANCODE_KP_PLUSMINUS,
            SCANCODE_KP_CLEAR,
            SCANCODE_KP_CLEARENTRY,
            SCANCODE_KP_BINARY,
            SCANCODE_KP_OCTAL,
            SCANCODE_KP_DECIMAL,
            SCANCODE_KP_HEXADECIMAL,

            SCANCODE_LCTRL,
            SCANCODE_LSHIFT,
            SCANCODE_LALT,
            SCANCODE_LGUI,
            SCANCODE_RCTRL,
            SCANCODE_RSHIFT,
            SCANCODE_RALT,
            SCANCODE_RGUI,

            SCANCODE_MODE,

            SCANCODE_AUDIONEXT,
            SCANCODE_AUDIOPREV,
            SCANCODE_AUDIOSTOP,
            SCANCODE_AUDIOPLAY,
            SCANCODE_AUDIOMUTE,
            SCANCODE_MEDIASELECT,
            SCANCODE_WWW,
            SCANCODE_MAIL,
            SCANCODE_CALCULATOR,
            SCANCODE_COMPUTER,
            SCANCODE_AC_SEARCH,
            SCANCODE_AC_HOME,
            SCANCODE_AC_BACK,
            SCANCODE_AC_FORWARD,
            SCANCODE_AC_STOP,
            SCANCODE_AC_REFRESH,
            SCANCODE_AC_BOOKMARKS,

            SCANCODE_BRIGHTNESSDOWN,
            SCANCODE_BRIGHTNESSUP,
            SCANCODE_DISPLAYSWITCH,
            SCANCODE_KBDILLUMTOGGLE,
            SCANCODE_KBDILLUMDOWN,
            SCANCODE_KBDILLUMUP,
            SCANCODE_EJECT,
            SCANCODE_SLEEP,

            SCANCODE_APP1,
            SCANCODE_APP2,

            SCANCODE_MAX_ENUM
        };

        public static bool init()
        {
            return Love2dDll.wrap_love_dll_keyboard_open_love_keyboard();
        }
        public static void setKeyRepeat(bool enable)
        {
            Love2dDll.wrap_love_dll_keyboard_setKeyRepeat(enable);
        }
        public static bool hasKeyRepeat()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_keyboard_hasKeyRepeat(out out_result);
            return out_result;
        }
        public static bool isDown(Key key_type)
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_keyboard_isDown((int)key_type, out out_result);
            return out_result;
        }
        public static bool isScancodeDown(Scancode scancode_type)
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_keyboard_isScancodeDown((int)scancode_type, out out_result);
            return out_result;
        }
        public static Scancode getScancodeFromKey(Key key_type)
        {
            int out_scancode_type = 0;
            Love2dDll.wrap_love_dll_keyboard_getScancodeFromKey((int)key_type, out out_scancode_type);
            return (Scancode)out_scancode_type;
        }
        public static Key getKeyFromScancode(Scancode scancode_type)
        {
            int out_key_type = 0;
            Love2dDll.wrap_love_dll_keyboard_getKeyFromScancode((int)scancode_type, out out_key_type);
            return (Key)out_key_type;
        }
        public static void setTextInput(bool enable)
        {
            Love2dDll.wrap_love_dll_keyboard_setTextInput(enable);
        }
        public static void setTextInput_xywh(bool enable, double x, double y, double w, double h)
        {
            Love2dDll.wrap_love_dll_keyboard_setTextInput_xywh(enable, x, y, w, h);
        }
        public static bool hasTextInput()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_keyboard_hasTextInput(out out_result);
            return out_result;
        }
        public static bool hasScreenKeyboard()
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

        public static void init()
        {
            Love2dDll.wrap_love_dll_touch_open_love_touch();
        }
        public static TouchInfo[] getTouches()
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
        public static void getToucheInfo(long idx, out double out_x, out double out_y, out double out_dx, out double out_dy, out double out_pressure)
        {
            Love2dDll.wrap_love_dll_touch_open_love_getToucheInfo(idx, out out_x, out out_y, out out_dx, out out_dy, out out_pressure);
        }

    }

    public partial class JoystickModule
    {
        public static void init()
        {
            Love2dDll.wrap_love_dll_joystick_open_love_joystick();
        }
        public Joystick[] getJoysticks()
        {
            IntPtr out_sticks;
            int out_sticks_lenght;
            Love2dDll.wrap_love_dll_joystick_getJoysticks(out out_sticks, out out_sticks_lenght);

            return DllTool.readIntPtrsWithConvertAndRelease<Joystick>(out_sticks, out_sticks_lenght);
        }
        public static int getIndex(Joystick j)
        {
            int out_index = 0;
            Love2dDll.wrap_love_dll_joystick_getIndex(j.p, out out_index);
            return out_index;
        }
        public static int getJoystickCount()
        {
            int out_sticks_lenght = 0;
            Love2dDll.wrap_love_dll_joystick_getJoystickCount(out out_sticks_lenght);
            return out_sticks_lenght;
        }
        public static bool setGamepadMapping(byte[] guid, Joystick.InputType gp_inputType_type, Joystick.InputType j_inputType_type, int inputIndex, Joystick.Hat hat_type)
        {
            bool out_success = false;
            Love2dDll.wrap_love_dll_joystick_setGamepadMapping(guid, (int)gp_inputType_type, (int)j_inputType_type, inputIndex, (int)hat_type, out out_success);
            return out_success;
        }
        public static bool loadGamepadMappings(byte[] str)
        {
            bool out_success = false;
            Love2dDll.wrap_love_dll_joystick_loadGamepadMappings(str, out out_success);
            return out_success;
        }
        public static string saveGamepadMappings()
        {
            IntPtr out_str = IntPtr.Zero;
            Love2dDll.wrap_love_dll_joystick_saveGamepadMappings(out out_str);
            return DllTool.WSToStringAndRelease(out_str);
        }

    }

    public partial class Event
    {
        public enum WrapEventType
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

        public static void quit(int exitStatus = 0)
        {
            System.Environment.Exit(exitStatus);
        }

        public static void init()
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
            out_joystick = LoveObject.newObject<Joystick>(out_joystick_ptr);
        }

        private static void DoHandleEvent(EventHandler handler, int out_event_type, bool out_down_or_up, bool out_bool, int out_idx, int out_enum1_type, int out_enum2_type, string out_string, Int4 out_int4, Float4 out_float4, float out_float_value, Joystick out_joystick)
        {
            switch ((WrapEventType)out_event_type)
            {
                case WrapEventType.WRAP_EVENT_TYPE_UNKNOW: { } break;
                case WrapEventType.WRAP_EVENT_TYPE_KEY:
                    {
                        if (out_down_or_up) handler.KeyPressed((Keyboard.Key)out_enum1_type, (Keyboard.Scancode)out_enum2_type, out_bool);
                        else handler.KeyReleased((Keyboard.Key)out_enum1_type, (Keyboard.Scancode)out_enum2_type);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_MOUSE_BUTTON:
                    {
                        if (out_down_or_up) handler.MousePressed(out_float4.x, out_float4.y, out_idx, out_bool);
                        else handler.MouseReleased(out_float4.x, out_float4.y, out_idx, out_bool);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_MOUSE_MOTION:
                    {
                        handler.MouseMoved(out_float4.x, out_float4.y, out_float4.w, out_float4.z, out_bool);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_MOUSE_WHEEL:
                    {
                        handler.WheelMoved(out_int4.x, out_int4.y);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_TOUCH_MOVED:
                    {
                        handler.TouchMoved(out_idx, out_float4.x, out_float4.y, out_float4.w, out_float4.z, out_float_value);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_TOUCH_PRESSED:
                    {
                        handler.TouchPressed(out_idx, out_float4.x, out_float4.y, out_float4.w, out_float4.z, out_float_value);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_TOUCH_RELEASED:
                    {
                        handler.TouchReleased(out_idx, out_float4.x, out_float4.y, out_float4.w, out_float4.z, out_float_value);
                    }
                    break;

                case WrapEventType.WRAP_EVENT_TYPE_JOYSTICK_BUTTON:
                    {
                        if (out_down_or_up) handler.JoystickPressed(out_joystick, out_idx);
                        else handler.JoystickReleased(out_joystick, out_idx);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_JOYSTICK_AXIS_MOTION:
                    {
                        handler.JoystickAxis(out_joystick, out_idx, out_float_value);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_JOYSTICK_HAT_MOTION:
                    {
                        handler.JoystickHat(out_joystick, out_idx, (Joystick.Hat)out_enum1_type);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_JOYSTICK_CONTROLLER_BUTTON:
                    {
                        if (out_down_or_up) handler.JoystickGamepadPressed(out_joystick, (Joystick.GamepadButton)out_enum1_type);
                        else handler.JoystickGamepadReleased(out_joystick, (Joystick.GamepadButton)out_enum1_type);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_JOYSTICK_CONTROLLER_AXIS_MOTION:
                    {
                        handler.JoystickGamepadAxis(out_joystick, (Joystick.GamepadAxis)out_enum1_type, out_float_value);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_JOYSTICK_DEVICE_ADDED_OR_REMOVED:
                    {
                        if (out_bool) handler.JoystickAdded(out_joystick);
                        else handler.JoystickRemoved(out_joystick);
                    }
                    break;
                case WrapEventType.WRAP_EVENT_TYPE_TEXTINPUT: {
                        handler.TextInput(out_string);
                    } break;
                case WrapEventType.WRAP_EVENT_TYPE_TEXTEDITING: {
                        handler.TextEditing(out_string, out_int4.x, out_int4.y);
                    } break;
                case WrapEventType.WRAP_EVENT_TYPE_WINDOW_VISIBLE: {
                        handler.WindowFocus(out_bool);
                    } break;
                case WrapEventType.WRAP_EVENT_TYPE_WINDOW_ENTER_OR_LEAVE: {
                        handler.MouseFocus(out_bool);
                    } break;
                case WrapEventType.WRAP_EVENT_TYPE_WINDOW_SHOWN_OR_HIDDEN: {
                        handler.WindowVisible(out_bool);
                    } break;
                case WrapEventType.WRAP_EVENT_TYPE_WINDOW_RESIZED: {
                        handler.WindowResize(out_int4.x, out_int4.y);
                    } break;
                case WrapEventType.WRAP_EVENT_TYPE_DROPPED: {
                        if (FileSystem.isDirectory(out_string)) handler.DirectoryDropped(out_string);
                        else handler.FileDropped(FileSystem.newFile(out_string));
                    } break;
                case WrapEventType.WRAP_EVENT_TYPE_LOWMEMORY: {
                        handler.LowMemory();
                    } break;
                case WrapEventType.WRAP_EVENT_TYPE_QUIT: {
                        if (handler.Quit()) quit();
                    } break;
                default: break;
            }
        }

        public static bool poll(EventHandler handler)
        {
            bool out_hasEvent;int out_event_type;bool out_down_or_up;bool out_bool;int out_idx;int out_enum1_type;int out_enum2_type;string out_string;Int4 out_int4;Float4 out_float4;float out_float_value;Joystick out_joystick;
            PollOrWait(true, out out_hasEvent, out out_event_type, out out_down_or_up, out out_bool, out out_idx, out out_enum1_type, out out_enum2_type, out out_string, out out_int4, out out_float4, out out_float_value, out out_joystick);

            if (out_hasEvent == false)
                return false;

            if (handler == null)
            {
                handler = new EventHandler();
            }
            DoHandleEvent(handler, out_event_type, out_down_or_up, out_bool, out_idx, out_enum1_type, out_enum2_type, out_string, out_int4, out_float4, out_float_value, out_joystick);
            return true;
        }
    }

    public partial class FileSystem
    {
        //// raw *new*
        public static File newFile(byte[] filename, File.Mode fmode_type = File.Mode.MODE_READ)
        {
            IntPtr out_file;
            Love2dDll.wrap_love_dll_filesystem_newFile(filename, (int)fmode_type, out out_file);
            return LoveObject.newObject<File>(out_file);
        }
        public static FileData newFileData(byte[] contents, byte[] filename, FileData.Decoder decoder_type)
        {
            IntPtr out_file;
            Love2dDll.wrap_love_dll_filesystem_newFileData_content(contents, contents.Length, filename, (int)decoder_type, out out_file);
            return LoveObject.newObject<FileData>(out_file);
        }
        public static FileData newFileData(File file)
        {
            IntPtr out_file;
            Love2dDll.wrap_love_dll_filesystem_newFileData_file(file.p, out out_file);
            return LoveObject.newObject<FileData>(out_file);
        }
        //// end *new*

        public static bool init(byte[] args)
        {
            Love2dDll.wrap_love_dll_filesystem_open_love_filesystem();
            return Love2dDll.wrap_love_dll_filesystem_init(args);
        }

        public static void setFused(bool flag)
        {
            Love2dDll.wrap_love_dll_filesystem_setFused(flag);
        }
        public static bool isFused()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_filesystem_isFused(out out_result);
            return out_result;
        }
        public static void setAndroidSaveExternal(bool useExternal)
        {
            Love2dDll.wrap_love_dll_filesystem_setAndroidSaveExternal(useExternal);
        }
        public static void setIdentity(byte[] arg, bool append = false)
        {
            Love2dDll.wrap_love_dll_filesystem_setIdentity(arg, append);
        }
        public static string getIdentity()
        {
            IntPtr out_str = IntPtr.Zero;
            Love2dDll.wrap_love_dll_filesystem_getIdentity(out out_str);
            return DllTool.WSToStringAndRelease(out_str);
        }
        public static void setSource(byte[] arg)
        {
            Love2dDll.wrap_love_dll_filesystem_setSource(arg);
        }
        public static string getSource()
        {
            IntPtr out_str = IntPtr.Zero;
            Love2dDll.wrap_love_dll_filesystem_getSource(out out_str);
            return DllTool.WSToStringAndRelease(out_str);
        }
        public static bool mount(byte[] archive, byte[] mountpoint, bool appendToPath)
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_filesystem_mount(archive, mountpoint, appendToPath, out out_result);
            return out_result;
        }
        public static bool unmount(byte[] archive)
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_filesystem_unmount(archive, out out_result);
            return out_result;
        }
        public static string getWorkingDirectory()
        {
            IntPtr out_str = IntPtr.Zero;
            Love2dDll.wrap_love_dll_filesystem_getWorkingDirectory(out out_str);
            return DllTool.WSToStringAndRelease(out_str);
        }
        public static string getUserDirectory()
        {
            IntPtr out_str = IntPtr.Zero;
            Love2dDll.wrap_love_dll_filesystem_getUserDirectory(out out_str);
            return DllTool.WSToStringAndRelease(out_str);
        }
        public static string getAppdataDirectory()
        {
            IntPtr out_str = IntPtr.Zero;
            Love2dDll.wrap_love_dll_filesystem_getAppdataDirectory(out out_str);
            return DllTool.WSToStringAndRelease(out_str);
        }
        public static string getSaveDirectory()
        {
            IntPtr out_str = IntPtr.Zero;
            Love2dDll.wrap_love_dll_filesystem_getSaveDirectory(out out_str);
            return DllTool.WSToStringAndRelease(out_str);
        }
        public static string getSourceBaseDirectory()
        {
            IntPtr out_str = IntPtr.Zero;
            Love2dDll.wrap_love_dll_filesystem_getSourceBaseDirectory(out out_str);
            return DllTool.WSToStringAndRelease(out_str);
        }
        public static string getRealDirectory(byte[] filename)
        {
            IntPtr out_str = IntPtr.Zero;
            Love2dDll.wrap_love_dll_filesystem_getRealDirectory(filename, out out_str);
            return DllTool.WSToStringAndRelease(out_str);
        }
        public static string getExecutablePath()
        {
            IntPtr out_str = IntPtr.Zero;
            Love2dDll.wrap_love_dll_filesystem_getExecutablePath(out out_str);
            return DllTool.WSToStringAndRelease(out_str);
        }
        public static bool exists(byte[] arg)
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_filesystem_exists(arg, out out_result);
            return out_result;
        }
        public static bool isDirectory(byte[] arg)
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_filesystem_isDirectory(arg, out out_result);
            return out_result;
        }
        public static bool isFile(byte[] arg)
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_filesystem_isFile(arg, out out_result);
            return out_result;
        }
        public static bool isSymlink(byte[] filename)
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_filesystem_isSymlink(filename, out out_result);
            return out_result;
        }
        public static void createDirectory(byte[] arg)
        {
            Love2dDll.wrap_love_dll_filesystem_createDirectory(arg);
        }
        public static void remove(byte[] arg)
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
        public static void write(byte[] filename, byte[] input)
        {
            Love2dDll.wrap_love_dll_filesystem_write(filename, input, (uint)input.Length);
        }
        public static void append(byte[] filename, byte[] input)
        {
            Love2dDll.wrap_love_dll_filesystem_append(filename, input, (uint)input.Length);
        }
        public static string[] getDirectoryItems(byte[] dir)
        {
            IntPtr out_wss = IntPtr.Zero;
            Love2dDll.wrap_love_dll_filesystem_getDirectoryItems(dir, out out_wss);
            return DllTool.WSSToStringListAndRelease(out_wss);
        }
        public static long getLastModified(byte[] filename)
        {
            long out_time;
            Love2dDll.wrap_love_dll_filesystem_getLastModified(filename, out out_time);
            return out_time;
        }
        public static long getSize(byte[] filename)
        {
            long out_size = 0;
            Love2dDll.wrap_love_dll_filesystem_getSize(filename, out out_size);
            return out_size;
        }
        public static void setSymlinksEnabled(bool enable)
        {
            Love2dDll.wrap_love_dll_filesystem_setSymlinksEnabled(enable);
        }
        public static bool areSymlinksEnabled()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_filesystem_areSymlinksEnabled(out out_result);
            return out_result;
        }
        public static string getRequirePath()
        {
            IntPtr out_str = IntPtr.Zero;
            Love2dDll.wrap_love_dll_filesystem_getRequirePath(out out_str);
            return DllTool.WSToStringAndRelease(out_str);
        }
        public static void setRequirePath(byte[] paths)
        {
            Love2dDll.wrap_love_dll_filesystem_setRequirePath(paths);
        }

    }

    public partial class Sound
    {
        //// raw *new*
        // filename -> file -> filedata -> decoder -> sounddata
        public static Decoder newDecoder(FileData fdata, int buffersize = Decoder.DEFAULT_BUFFER_SIZE)
        {
            IntPtr out_ptr;
            Love2dDll.wrap_love_dll_sound_newDecoder_filedata(fdata.p, buffersize, out out_ptr);
            return LoveObject.newObject<Decoder>(out_ptr);
        }
        public static Decoder newDecoder(File file, int buffersize = Decoder.DEFAULT_BUFFER_SIZE)
        {
            IntPtr out_ptr;
            Love2dDll.wrap_love_dll_sound_newDecoder_file(file.p, buffersize, out out_ptr);
            return LoveObject.newObject<Decoder>(out_ptr);
        }
        public static SoundData newSoundData(Decoder decoder)
        {
            IntPtr out_soundData_ptr;
            Love2dDll.wrap_love_dll_sound_newSoundData_decoder(decoder.p, out out_soundData_ptr);
            return LoveObject.newObject<SoundData>(out_soundData_ptr);
        }
        public static SoundData newSoundData(int samples, int sampleRate = Decoder.DEFAULT_SAMPLE_RATE, int bits = Decoder.DEFAULT_BIT_DEPTH, int channels = Decoder.DEFAULT_CHANNELS)
        {
            IntPtr out_soundData_ptr;
            Love2dDll.wrap_love_dll_sound_newSoundData(samples, sampleRate, bits, channels, out out_soundData_ptr);
            return LoveObject.newObject<SoundData>(out_soundData_ptr);
        }
        //// end *new*


        //// new plus
        public static SoundData newSoundData(File file)
        {
            var decoder = newDecoder(file);
            return newSoundData(decoder);
        }
        public static Decoder newDecoder(string filename, int bufferSize = Decoder.DEFAULT_BUFFER_SIZE)
        {
            var fdata = FileSystem.newFileData(filename);
            return newDecoder(fdata, bufferSize);
        }
        public static SoundData newSoundData(string filename)
        {
            var decoder = newDecoder(filename);
            return newSoundData(decoder);
        }
        //// end new plus


        public static bool init()
        {
            return Love2dDll.wrap_love_dll_sound_luaopen_love_sound();
        }
    }

    public partial class Audio
    {
        public enum DistanceModel
        {
            DISTANCE_NONE,
            DISTANCE_INVERSE,
            DISTANCE_INVERSE_CLAMPED,
            DISTANCE_LINEAR,
            DISTANCE_LINEAR_CLAMPED,
            DISTANCE_EXPONENT,
            DISTANCE_EXPONENT_CLAMPED,
            DISTANCE_MAX_ENUM
        };

        //// raw *new*
        // filename -> file -> filedata -> decoder -> source 
        //             file -> sounddata -> 
        public static Source newSource(Decoder decoder, Source.Type type = Source.Type.TYPE_STREAM)
        {
            IntPtr out_decoder_ptr;
            Love2dDll.wrap_love_dll_audio_newSource_decoder(decoder.p, (int)type, out out_decoder_ptr);
            return LoveObject.newObject<Source>(out_decoder_ptr);
        }
        public static Source newSource(SoundData sd, Source.Type type = Source.Type.TYPE_STREAM)
        {
            IntPtr out_decoder_ptr;
            Love2dDll.wrap_love_dll_audio_newSource_sounddata(sd.p, (int)type, out out_decoder_ptr);
            return LoveObject.newObject<Source>(out_decoder_ptr);
        }
        //// end *new*



        public static bool init()
        {
            return Love2dDll.wrap_love_dll_audio_open_love_audio();
        }

        public static int getSourceCount()
        {
            int out_reslut = 0;
            Love2dDll.wrap_love_dll_audio_getSourceCount(out out_reslut);
            return out_reslut;
        }
        public static void stop()
        {
            Love2dDll.wrap_love_dll_audio_stop();
        }
        public static void pause()
        {
            Love2dDll.wrap_love_dll_audio_pause();
        }
        public static void resume()
        {
            Love2dDll.wrap_love_dll_audio_resume();
        }
        public static void rewind()
        {
            Love2dDll.wrap_love_dll_audio_rewind();
        }
        public static void setVolume(float v)
        {
            Love2dDll.wrap_love_dll_audio_setVolume(v);
        }
        public static float getVolume()
        {
            float out_volume = 0;
            Love2dDll.wrap_love_dll_audio_getVolume(out out_volume);
            return out_volume;
        }
        public static void setPosition(float x, float y, float z)
        {
            Love2dDll.wrap_love_dll_audio_setPosition(x, y, z);
        }
        public static Float3 getPosition()
        {
            float out_x, out_y, out_z;
            Love2dDll.wrap_love_dll_audio_getPosition(out out_x, out out_y, out out_z);
            return new Float3(out_x, out_y, out_z);
        }
        public static void setOrientation(float x, float y, float z, float dx, float dy, float dz)
        {
            Love2dDll.wrap_love_dll_audio_setOrientation(x, y, z, dx, dy, dz);
        }
        public static void getOrientation(out Float3 pos, out Float3 dir)
        {
            float out_x,out_y,out_z,out_dx,out_dy,out_dz;
            Love2dDll.wrap_love_dll_audio_getOrientation(out out_x, out out_y, out out_z, out out_dx, out out_dy, out out_dz);
            pos = new Float3(out_x, out_y, out_z);
            dir = new Float3(out_dx, out_dy, out_dz);
        }
        public static void setVelocity(float x, float y, float z)
        {
            Love2dDll.wrap_love_dll_audio_setVelocity(x, y, z);
        }
        public static Float3 getVelocity()
        {
            float out_x, out_y, out_z;
            Love2dDll.wrap_love_dll_audio_getVelocity(out out_x, out out_y, out out_z);
            return new Float3(out_x, out_y, out_z);
        }
        public static void setDopplerScale(float scale)
        {
            Love2dDll.wrap_love_dll_audio_setDopplerScale(scale);
        }
        public static float getDopplerScale()
        {
            float out_scale = 0;
            Love2dDll.wrap_love_dll_audio_getDopplerScale(out out_scale);
            return out_scale;
        }
        public static bool canRecord()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_audio_canRecord(out out_result);
            return out_result;
        }
        public static void setDistanceModel(int distancemodel_type)
        {
            Love2dDll.wrap_love_dll_audio_setDistanceModel(distancemodel_type);
        }
        public static int getDistanceModel()
        {
            int out_distancemodel_type = 0;
            Love2dDll.wrap_love_dll_audio_getDistanceModel(out out_distancemodel_type);
            return out_distancemodel_type;
        }

    }

    public partial class ImageModule
    {
        //// raw new
        public static ImageData newImageData(int w, int h, byte[] data)
        {
            if (w <= 0 || h <= 0)
            {
                throw new Exception("Invalid image size.");
            }

            IntPtr out_imagedata;
            Love2dDll.wrap_love_dll_image_newImageData_wh_data(w, h, data, data.Length, out out_imagedata);
            return LoveObject.newObject<ImageData>(out_imagedata);
        }
        public static ImageData newImageData(FileData data)
        {
            IntPtr out_imagedata;
            Love2dDll.wrap_love_dll_image_newImageData_filedata(data.p, out out_imagedata);
            return LoveObject.newObject<ImageData>(out_imagedata);
        }
        public static CompressedImageData newCompressedData(FileData data)
        {
            IntPtr out_compressedimagedata;
            var res = Love2dDll.wrap_love_dll_image_newCompressedData(data.p, out out_compressedimagedata);
            return LoveObject.newObject<CompressedImageData>(out_compressedimagedata);
        }
        //// end raw new

        public static bool init()
        {
            return Love2dDll.wrap_love_dll_image_open_love_image();
        }

        public static bool isCompressed(FileData data)
        {
            bool out_result;
            Love2dDll.wrap_love_dll_image_isCompressed(data.p, out out_result);
            return out_result;
        }

    }

    public partial class FontModule
    {
        //// raw new
        public static Rasterizer newRasterizer(FileData fileData)
        {
            IntPtr out_reasterizer;
            Love2dDll.wrap_love_dll_font_newRasterizer(fileData.p, out out_reasterizer);
            return LoveObject.newObject<Rasterizer>(out_reasterizer);
        }
        public static Rasterizer newTrueTypeRasterizer(int size, TrueTypeRasterizer.Hinting hinting = TrueTypeRasterizer.Hinting.HINTING_NORMAL)
        {
            IntPtr out_reasterizer;
            Love2dDll.wrap_love_dll_font_newTrueTypeRasterizer(size, (int)hinting, out out_reasterizer);
            return LoveObject.newObject<Rasterizer>(out_reasterizer);
        }
        public static Rasterizer newBMFontRasterizer(FileData fileData, params ImageData[] imageDatas)
        {
            IntPtr out_reasterizer;
            var datas = new IntPtr[imageDatas.Length];
            for (int i = 0; i < imageDatas.Length; i++) datas[i] = imageDatas[i].p;
            Love2dDll.wrap_love_dll_font_newBMFontRasterizer(fileData.p, datas, datas.Length, out out_reasterizer);
            return LoveObject.newObject<Rasterizer>(out_reasterizer);
        }
        public static Rasterizer newImageRasterizer(ImageData imageData, byte[] glyphs, int extraspacing)
        {
            IntPtr out_reasterizer;
            Love2dDll.wrap_love_dll_font_newImageRasterizer(imageData.p, glyphs, extraspacing, out out_reasterizer);
            return LoveObject.newObject<Rasterizer>(out_reasterizer);
        }
        public static GlyphData newGlyphData(Rasterizer rasterizer, byte[] glyph)
        {
            IntPtr out_GlyphData;
            Love2dDll.wrap_love_dll_font_newGlyphData_rasterizer_str(rasterizer.p, glyph, out out_GlyphData);
            return LoveObject.newObject<GlyphData>(out_GlyphData);
        }
        public static GlyphData newGlyphData(Rasterizer rasterizer, int glyphCode)
        {
            IntPtr out_GlyphData;
            Love2dDll.wrap_love_dll_font_newGlyphData_rasterizer_num(rasterizer.p, glyphCode, out out_GlyphData);
            return LoveObject.newObject<GlyphData>(out_GlyphData);
        }
        //// end raw new
        
        public static bool init()
        {
            return Love2dDll.wrap_love_dll_font_open_love_font();
        }
    }

    public partial class VideoModule
    {
        //// raw new
        public static VideoStream newVideoStream(File file)
        {
            IntPtr out_videostream;
            Love2dDll.wrap_love_dll_newVideoStream(file.p, out out_videostream);
            return LoveObject.newObject<VideoStream>(out_videostream);
        }
        //// end raw new


        public static bool init()
        {
            return Love2dDll.wrap_love_dll_video_open_love_video();
        }

    }

    public partial class MathModule
    {
        static RandomGenerator rg = null;
        public static void init()
        {
            Love2dDll.wrap_love_dll_open_love_math();
            rg = newRandomGenerator();
        }
        public static float random(int l, int u)
        {
            double random = rg.random();
            return (float)(System.Math.Floor(random * (u - l + 1) + l));
        }
        public static RandomGenerator ext_getRandomGenerator()
        {
            return rg;
        }
        public static RandomGenerator newRandomGenerator()
        {
            IntPtr out_RandomGenerator = IntPtr.Zero;
            Love2dDll.wrap_love_dll_math_newRandomGenerator(out out_RandomGenerator);
            return LoveObject.newObject<RandomGenerator>(out_RandomGenerator);
        }
        public static BezierCurve newBezierCurve(Float2[] points)
        {
            IntPtr out_BezierCurve = IntPtr.Zero;
            Love2dDll.wrap_love_dll_math_newBezierCurve(points, points.Length, out out_BezierCurve);
            return LoveObject.newObject<BezierCurve>(out_BezierCurve);
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
        public static bool isConvex(Float2[] points)
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_math_isConvex(points, points.Length, out out_result);
            return out_result;
        }
        public static float gammaToLinear(float gama)
        {
            float out_liner = 0;
            Love2dDll.wrap_love_dll_math_gammaToLinear(gama, out out_liner);
            return out_liner;
        }
        public static float linearToGamma(float liner)
        {
            float out_gama = 0;
            Love2dDll.wrap_love_dll_math_linearToGamma(liner, out out_gama);
            return out_gama;
        }
        public static float noise(float x)
        {
            float out_result = 0;
            Love2dDll.wrap_love_dll_math_noise_1(x, out out_result);
            return out_result;
        }
        public static float noise(float x, float y)
        {
            float out_result = 0;
            Love2dDll.wrap_love_dll_math_noise_2(x, y, out out_result);
            return out_result;
        }
        public static float noise(float x, float y, float z)
        {
            float out_result = 0;
            Love2dDll.wrap_love_dll_math_noise_3(x, y, z, out out_result);
            return out_result;
        }
        public static float noise(float x, float y, float z, float w)
        {
            float out_result = 0;
            Love2dDll.wrap_love_dll_math_noise_4(x, y, z, w, out out_result);
            return out_result;
        }
        public static CompressedImageData compress(byte[] datas, Compressor.Format format_type = Compressor.Format.FORMAT_LZ4, int level = -1)
        {
            IntPtr out_compressedData = IntPtr.Zero;
            Love2dDll.wrap_love_dll_math_compress_str(datas, datas.Length, (int)format_type, level, out out_compressedData);
            return LoveObject.newObject<CompressedImageData>(out_compressedData);
        }
        public static CompressedImageData compress(Data data, Compressor.Format format_type = Compressor.Format.FORMAT_LZ4, int level = -1)
        {
            IntPtr out_compressedData = IntPtr.Zero;
            Love2dDll.wrap_love_dll_math_compress_data(data.p, (int)format_type, level, out out_compressedData);
            return LoveObject.newObject<CompressedImageData>(out_compressedData);
        }
        public static byte[] decompress(Data data)
        {
            IntPtr out_datas;
            int out_datas_length;
            Love2dDll.wrap_love_dll_math_decompress_data(data.p, out out_datas, out out_datas_length);

            return DllTool.readBytesAndRelease(out_datas, out_datas_length);
        }
        public static byte[] decompress_bytes(byte[] data, Compressor.Format format_type = Compressor.Format.FORMAT_LZ4, int level = -1)
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
            DRAW_LINE,
            DRAW_FILL,
            DRAW_MAX_ENUM
        };
        public enum ArcMode : int
        {
            ARC_OPEN,
            ARC_CLOSED,
            ARC_PIE,
            ARC_MAX_ENUM
        };
        public enum BlendMode : int
        {
            BLEND_ALPHA,
            BLEND_ADD,
            BLEND_SUBTRACT,
            BLEND_MULTIPLY,
            BLEND_LIGHTEN,
            BLEND_DARKEN,
            BLEND_SCREEN,
            BLEND_REPLACE,
            BLEND_MAX_ENUM
        };
        public enum BlendAlpha : int
        {
            BLENDALPHA_MULTIPLY,
            BLENDALPHA_PREMULTIPLIED,
            BLENDALPHA_MAX_ENUM
        };
        public enum LineStyle : int
        {
            LINE_ROUGH,
            LINE_SMOOTH,
            LINE_MAX_ENUM
        };
        public enum LineJoin : int
        {
            LINE_JOIN_NONE,
            LINE_JOIN_MITER,
            LINE_JOIN_BEVEL,
            LINE_JOIN_MAX_ENUM
        };
        public enum StencilAction : int
        {
            STENCIL_REPLACE,
            STENCIL_INCREMENT,
            STENCIL_DECREMENT,
            STENCIL_INCREMENT_WRAP,
            STENCIL_DECREMENT_WRAP,
            STENCIL_INVERT,
            STENCIL_MAX_ENUM
        };
        public enum CompareMode : int
        {
            COMPARE_LESS,
            COMPARE_LEQUAL,
            COMPARE_EQUAL,
            COMPARE_GEQUAL,
            COMPARE_GREATER,
            COMPARE_NOTEQUAL,
            COMPARE_ALWAYS,
            COMPARE_MAX_ENUM
        };
        public enum Feature : int
        {
            FEATURE_MULTI_CANVAS_FORMATS,
            FEATURE_CLAMP_ZERO,
            FEATURE_LIGHTEN,
            FEATURE_MAX_ENUM
        };
        public enum Renderer : int
        {
            RENDERER_OPENGL = 0,
            RENDERER_OPENGLES,
            RENDERER_MAX_ENUM
        };
        public enum SystemLimit : int
        {
            LIMIT_POINT_SIZE,
            LIMIT_TEXTURE_SIZE,
            LIMIT_MULTI_CANVAS,
            LIMIT_CANVAS_MSAA,
            LIMIT_MAX_ENUM
        };
        public enum StackType : int
        {
            STACK_ALL,
            STACK_TRANSFORM,
            STACK_MAX_ENUM
        };

        #region graphics Object Creation

        public static bool init()
        {
            Love2dDll.wrap_love_dll_graphics_open_love_graphics();
            Love2d.Love2dGraphicsShaderBoot.init();
            return true;
        }

        public static Image newImage(ImageData[] imageData, bool flagMipmaps = false, bool flagLinear = false)
        {
            IntPtr[] imageDataList = new IntPtr[imageData.Length];
            for (int i = 0; i < imageData.Length; i++)
            {
                imageDataList[i] = imageData[i].p;
            }

            IntPtr out_image = IntPtr.Zero;
            Love2dDll.wrap_love_dll_graphics_newImage_file(imageDataList, imageDataList.Length, flagMipmaps, flagLinear, out out_image);
            return LoveObject.newObject<Image>(out_image);
        }
        public static Image newImage(CompressedImageData[] compressedImageData, bool flagMipmaps = false, bool flagLinear = false)
        {
            IntPtr[] compressedImageDataList = new IntPtr[compressedImageData.Length];
            for (int i = 0; i < compressedImageData.Length; i++)
            {
                compressedImageDataList[i] = compressedImageData[i].p;
            }

            IntPtr out_image = IntPtr.Zero;
            Love2dDll.wrap_love_dll_graphics_newImage(compressedImageDataList, compressedImageDataList.Length, flagMipmaps, flagLinear, out out_image);
            return LoveObject.newObject<Image>(out_image);
        }
        public static Quad newQuad(double x, double y, double w, double h, double sw, double sh)
        {
            IntPtr out_quad = IntPtr.Zero;
            Love2dDll.wrap_love_dll_graphics_newQuad(x, y, w, h, sw, sh, out out_quad);
            return LoveObject.newObject<Quad>(out_quad);
        }
        public static Font newFont(Rasterizer rasterizer)
        {
            IntPtr out_font = IntPtr.Zero;
            Love2dDll.wrap_love_dll_graphics_newFont(rasterizer.p, out out_font);
            return LoveObject.newObject<Font>(out_font);
        }
        public static SpriteBatch newSpriteBatch(Texture texture, int maxSprites, Mesh.Usage usage_type)
        {
            IntPtr out_spriteBatch = IntPtr.Zero;
            Love2dDll.wrap_love_dll_graphics_newSpriteBatch(texture.p, maxSprites, (int)usage_type, out out_spriteBatch);
            return LoveObject.newObject<SpriteBatch>(out_spriteBatch);
        }
        public static ParticleSystem newParticleSystem(Texture texture, int buffer)
        {
            IntPtr out_particleSystem = IntPtr.Zero;
            Love2dDll.wrap_love_dll_graphics_newParticleSystem(texture.p, buffer, out out_particleSystem);
            return LoveObject.newObject<ParticleSystem>(out_particleSystem);
        }
        public static Canvas newCanvas(int width, int height, Canvas.Format format_type, int msaa)
        {
            IntPtr out_canvas = IntPtr.Zero;
            Love2dDll.wrap_love_dll_graphics_newCanvas(width, height, (int)format_type, msaa, out out_canvas);
            return LoveObject.newObject<Canvas>(out_canvas);
        }
        public static Mesh newMesh(Float2[] pos, Float2[] uv, Int4[] color, Mesh.Usage drawMode, Mesh.Usage usage)
        {
            IntPtr out_mesh = IntPtr.Zero;
            Love2dDll.wrap_love_dll_graphics_newMesh_specifiedVertices(pos, uv, color, pos.Length, (int)drawMode, (int)usage, out out_mesh);
            return LoveObject.newObject<Mesh>(out_mesh);
        }
        public static Mesh newMesh(int count, Mesh.Usage drawMode, Mesh.Usage usage)
        {
            IntPtr out_mesh = IntPtr.Zero;
            Love2dDll.wrap_love_dll_graphics_newMesh_count(count, (int)drawMode, (int)usage, out out_mesh);
            return LoveObject.newObject<Mesh>(out_mesh);
        }
        public static Text newText(Font font, ColoredString coloredStr)
        {
            IntPtr out_text = IntPtr.Zero;
            coloredStr.ExecResource((Tuple<BytePtr[], Int4[]> tmp) => {
                Love2dDll.wrap_love_dll_graphics_newText(font.p, tmp.Item1, tmp.Item2, coloredStr.items.Length, out out_text);
            });
            return LoveObject.newObject<Text>(out_text);
        }
        public static Video newVideo(VideoStream videoStream)
        {
            IntPtr out_video = IntPtr.Zero;
            Love2dDll.wrap_love_dll_graphics_newVideo(videoStream.p, out out_video);
            return LoveObject.newObject<Video>(out_video);
        }
        public static ImageData newScreenshot(bool copyAlpha)
        {
            IntPtr out_imageData = IntPtr.Zero;
            Love2dDll.wrap_love_dll_graphics_newScreenshot(copyAlpha, out out_imageData);
            return LoveObject.newObject<ImageData>(out_imageData);
        }

        #endregion

        #region graphics State

        public static bool isActive()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_graphics_isActive(out out_result);
            return out_result;
        }
        public static bool isGammaCorrect()
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_graphics_isGammaCorrect(out out_result);
            return out_result;
        }
        public static void setScissor()
        {
            Love2dDll.wrap_love_dll_graphics_setScissor();
        }
        public static void setScissor(int x, int y, int w, int h)
        {
            Love2dDll.wrap_love_dll_graphics_setScissor_xywh(x, y, w, h);
        }
        public static void intersectScissor(int x, int y, int w, int h)
        {
            Love2dDll.wrap_love_dll_graphics_intersectScissor(x, y, w, h);
        }
        public static Int4 getScissor()
        {
            int out_x, out_y, out_w, out_h;
            Love2dDll.wrap_love_dll_graphics_getScissor(out out_x, out out_y, out out_w, out out_h);
            return new Int4(out_x, out_y, out_w, out_h);
        }
        public static void setStencilTest(CompareMode compare_type, int compareValue)
        {
            Love2dDll.wrap_love_dll_graphics_setStencilTest((int)compare_type, compareValue);
        }
        public static void getStencilTest(out CompareMode out_compare_type, out int out_compareValue)
        {
            int compare_type = 0;
            Love2dDll.wrap_love_dll_graphics_getStencilTest(out compare_type, out out_compareValue);
            out_compare_type = (CompareMode)compare_type;
        }
        public static void setColor(int r, int g, int b, int a = 255)
        {
            Love2dDll.wrap_love_dll_graphics_setColor(r, g, b, a);
        }
        public static Int4 getColor()
        {
            int out_r, out_g, out_b, out_a;
            Love2dDll.wrap_love_dll_graphics_getColor(out out_r, out out_g, out out_b, out out_a);
            return new Int4(out_r, out_g, out_b, out_a);
        }
        public static void setBackgroundColor(int r, int g, int b, int a = 255)
        {
            Love2dDll.wrap_love_dll_graphics_setBackgroundColor(r, g, b, a);
        }
        public static Int4 getBackgroundColor()
        {
            int out_r, out_g, out_b, out_a;
            Love2dDll.wrap_love_dll_graphics_getBackgroundColor(out out_r, out out_g, out out_b, out out_a);
            return new Int4(out_r, out_g, out_b, out_a);
        }
        public static void setFont(Font font)
        {
            Love2dDll.wrap_love_dll_graphics_setFont(font.p);
        }
        public static Font getFont()
        {
            IntPtr out_font = IntPtr.Zero;
            Love2dDll.wrap_love_dll_graphics_getFont(out out_font);
            return LoveObject.newObject<Font>(out_font);
        }
        public static void setColorMask(bool r, bool g, bool b, bool a)
        {
            Love2dDll.wrap_love_dll_graphics_setColorMask(r, g, b, a);
        }
        public static void getColorMask(out bool out_r, out bool out_g, out bool out_b, out bool out_a)
        {
            Love2dDll.wrap_love_dll_graphics_getColorMask(out out_r, out out_g, out out_b, out out_a);
        }
        public static void setBlendMode(BlendMode blendMode_type, BlendAlpha blendAlphaMode_type)
        {
            Love2dDll.wrap_love_dll_graphics_setBlendMode((int)blendMode_type, (int)blendAlphaMode_type);
        }
        public static void getBlendMode(out BlendMode out_blendMode_type, out BlendAlpha out_blendAlphaMode_type)
        {
            int blendMode_type = 0;
            int blendAlphaMode_type = 0;
            Love2dDll.wrap_love_dll_graphics_getBlendMode(out blendMode_type, out blendAlphaMode_type);
            out_blendMode_type = (BlendMode)blendMode_type;
            out_blendAlphaMode_type = (BlendAlpha)blendAlphaMode_type;
        }
        public static void setDefaultFilter(Texture.FilterMode filterModeMagMin_type, Texture.FilterMode filterModeMag_type, int anisotropy)
        {
            Love2dDll.wrap_love_dll_graphics_setDefaultFilter((int)filterModeMagMin_type, (int)filterModeMag_type, anisotropy);
        }
        public static void getDefaultFilter(out Texture.FilterMode out_filterModeMagMin_type, out Texture.FilterMode out_filterModeMag_type, out int out_anisotropy)
        {
            int filterModeMagMin_type = 0;
            int filterModeMag_type = 0;
            Love2dDll.wrap_love_dll_graphics_getDefaultFilter(out filterModeMagMin_type, out filterModeMag_type, out out_anisotropy);
            out_filterModeMagMin_type = (Texture.FilterMode)filterModeMagMin_type;
            out_filterModeMag_type = (Texture.FilterMode)filterModeMag_type;
        }
        public static void setLineWidth(float width)
        {
            Love2dDll.wrap_love_dll_graphics_setLineWidth(width);
        }
        public static void setLineStyle(LineStyle style_type)
        {
            Love2dDll.wrap_love_dll_graphics_setLineStyle((int)style_type);
        }
        public static void setLineJoin(LineJoin join_type)
        {
            Love2dDll.wrap_love_dll_graphics_setLineJoin((int)join_type);
        }
        public static float getLineWidth()
        {
            float out_result = 0;
            Love2dDll.wrap_love_dll_graphics_getLineWidth(out out_result);
            return out_result;
        }
        public static LineStyle getLineStyle()
        {
            int out_lineStyle_type = 0;
            Love2dDll.wrap_love_dll_graphics_getLineStyle(out out_lineStyle_type);
            return (LineStyle)out_lineStyle_type;
        }
        public static LineJoin getLineJoin()
        {
            int out_lineJoinStyle_type = 0;
            Love2dDll.wrap_love_dll_graphics_getLineJoin(out out_lineJoinStyle_type);
            return (LineJoin)out_lineJoinStyle_type;
        }
        public static void setPointSize(float size)
        {
            Love2dDll.wrap_love_dll_graphics_setPointSize(size);
        }
        public static float getPointSize()
        {
            float out_size = 0;
            Love2dDll.wrap_love_dll_graphics_getPointSize(out out_size);
            return out_size;
        }
        public static void setWireframe(bool enable)
        {
            Love2dDll.wrap_love_dll_graphics_setWireframe(enable);
        }
        public static bool isWireframe()
        {
            bool out_isWireFrame = false;
            Love2dDll.wrap_love_dll_graphics_isWireframe(out out_isWireFrame);
            return out_isWireFrame;
        }
        public static void setCanvas(Canvas[] canvas)
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
                canvas[i] = LoveObject.newObject<Canvas>(buffer[i]);
            }

            return canvas;
        }
        public static void setShader(Shader shader)
        {
            Love2dDll.wrap_love_dll_graphics_setShader(shader.p);
        }
        public static Shader getShader()
        {
            IntPtr out_shader = IntPtr.Zero;
            Love2dDll.wrap_love_dll_graphics_getShader(out out_shader);
            return LoveObject.newObject<Shader>(out_shader);
        }

        #endregion

        #region graphics Coordinate System

        public static void push(StackType stack = StackType.STACK_TRANSFORM)
        {
            Love2dDll.wrap_love_dll_graphics_push((int)stack);
        }
        public static void pop()
        {
            Love2dDll.wrap_love_dll_graphics_pop();
        }
        public static void rotate(float angle)
        {
            Love2dDll.wrap_love_dll_graphics_rotate(angle);
        }
        public static void scale(float sx, float sy)
        {
            Love2dDll.wrap_love_dll_graphics_scale(sx, sy);
        }
        public static void translate(float x, float y)
        {
            Love2dDll.wrap_love_dll_graphics_translate(x, y);
        }
        public static void shear(float kx, float ky)
        {
            Love2dDll.wrap_love_dll_graphics_shear(kx, ky);
        }
        public static void origin()
        {
            Love2dDll.wrap_love_dll_graphics_origin();
        }

        #endregion

        #region graphics Drwing

        public static void stencil(Action actionFunc, StencilAction stencilAction, int value, bool keepValue)
        {
            if (!keepValue)
                Love2dDll.wrap_love_dll_graphics_ext_stencil_clearStencil();

            Love2dDll.wrap_love_dll_graphics_ext_stencil_drawToStencilBuffer((int)stencilAction, value);

            actionFunc?.Invoke();

            Love2dDll.wrap_love_dll_graphics_ext_stencil_stopDrawToStencilBuffer();
        }

        public static void clear(float r, float g, float b, float a)
        {
            Love2dDll.wrap_love_dll_graphics_clear_rgba(r, g, b, a);
        }
        public static void clear(Float4[] colorList, bool[] enableList)
        {
            if (colorList.Length != enableList.Length)
            {
                throw new Exception("length of colorList and enableList should same !");
            }
            Love2dDll.wrap_love_dll_graphics_clear_rgbalist(colorList, enableList, colorList.Length);
        }
        public static void discard(bool[] discardColors, bool discardStencil)
        {
            Love2dDll.wrap_love_dll_graphics_discard(discardColors, discardColors.Length, discardStencil);
        }
        public static void present()
        {
            Love2dDll.wrap_love_dll_graphics_present();
        }

        public static bool draw(Drawable drawable, float x, float y, float angle = 0, float sx = 1, float sy = 1, float ox = 0, float oy = 0, float kx = 0, float ky = 0)
        {
            return Love2dDll.wrap_love_dll_graphics_draw_drawable(drawable.p, x, y, angle, sx, sy, ox, oy, kx, ky);
        }
        public static bool draw(Quad quad, Texture texture, float x, float y, float angle = 0, float sx = 1, float sy = 1, float ox = 0, float oy = 0, float kx = 0, float ky = 0)
        {
            return Love2dDll.wrap_love_dll_graphics_draw_texture_quad(quad.p, texture.p, x, y, angle, sx, sy, ox, oy, kx, ky);
        }
        public static void print(string text, float x, float y, float angle = 0, float sx = 1, float sy = 1, float ox = 0, float oy = 0, float kx = 0, float ky = 0)
        {
            ColoredString coloredStr = DllTool.ToColoredStrings(text);

            coloredStr.ExecResource((Tuple<BytePtr[], Int4[]> tmp) =>{
                Love2dDll.wrap_love_dll_graphics_print(tmp.Item1, tmp.Item2, coloredStr.Length, x, y, angle, sx, sy, ox, oy, kx, ky);
            });
        }
        public static void print(ColoredString coloredStr, float x, float y, float angle = 0, float sx = 1, float sy = 1, float ox = 0, float oy = 0, float kx = 0, float ky = 0)
        {
            coloredStr.ExecResource((Tuple<BytePtr[], Int4[]> tmp) =>{
                Love2dDll.wrap_love_dll_graphics_print(tmp.Item1, tmp.Item2, coloredStr.Length, x, y, angle, sx, sy, ox, oy, kx, ky);
            });
        }
        public static void printf(ColoredString coloredStr, float x, float y, float wrap, Font.AlignMode align_type, float angle = 0, float sx = 1, float sy = 1, float ox = 0, float oy = 0, float kx = 0, float ky = 0)
        {
            coloredStr.ExecResource((Tuple<BytePtr[], Int4[]> tmp) =>{
                Love2dDll.wrap_love_dll_graphics_printf(tmp.Item1, tmp.Item2, coloredStr.Length, x, y, wrap, (int)align_type, angle, sx, sy, ox, oy, kx, ky);
            });
        }
        public static bool rectangle(DrawMode mode_type, float x, float y, float w, float h)
        {
            return Love2dDll.wrap_love_dll_graphics_rectangle((int)mode_type, x, y, w, h);
        }
        public static bool rectangle(DrawMode mode_type, float x, float y, float w, float h, float rx, float ry, int points)
        {
            return Love2dDll.wrap_love_dll_graphics_rectangle_with_rounded_corners((int)mode_type, x, y, w, h, rx, ry, points);
        }
        public static bool circle(DrawMode mode_type, float x, float y, float radius, int points)
        {
            return Love2dDll.wrap_love_dll_graphics_circle((int)mode_type, x, y, radius, points);
        }
        public static bool ellipse(DrawMode mode_type, float x, float y, float a, float b, int points)
        {
            return Love2dDll.wrap_love_dll_graphics_ellipse((int)mode_type, x, y, a, b, points);
        }
        public static bool arc(DrawMode mode_type, int arcmode_type, float x, float y, float radius, float angle1, float angle2)
        {
            return Love2dDll.wrap_love_dll_graphics_arc((int)mode_type, arcmode_type, x, y, radius, angle1, angle2);
        }
        public static bool arc(DrawMode mode_type, ArcMode arcmode_type, float x, float y, float radius, float angle1, float angle2, int points)
        {
            return Love2dDll.wrap_love_dll_graphics_arc_points((int)mode_type, (int)arcmode_type, x, y, radius, angle1, angle2, points);
        }
        public static bool points(Float2[] coords)
        {
            return Love2dDll.wrap_love_dll_graphics_points(coords, coords.Length);
        }
        public static bool points(Float2[] coords, Int4[] colors)
        {
            if (coords.Length != colors.Length)
            {
                throw new Exception("length of coords and colors should same");
            }
            return Love2dDll.wrap_love_dll_graphics_points_colors(coords, colors, coords.Length);
        }
        public static bool line(Float2[] coords)
        {
            return Love2dDll.wrap_love_dll_graphics_line(coords, coords.Length);
        }
        public static bool polygon(DrawMode mode_type, Float2[] coords)
        {
            return Love2dDll.wrap_love_dll_graphics_polygon((int)mode_type, coords, coords.Length);
        }

        #endregion

        #region graphics Window

        public static bool isCreated()
        {
            bool out_reslut = false;
            Love2dDll.wrap_love_dll_graphics_isCreated(out out_reslut);
            return out_reslut;
        }
        public static int getWidth()
        {
            int out_width = 0;
            Love2dDll.wrap_love_dll_graphics_getWidth(out out_width);
            return out_width;
        }
        public static int getHeight()
        {
            int out_height = 0;
            Love2dDll.wrap_love_dll_graphics_getHeight(out out_height);
            return out_height;
        }


        #endregion

        #region graphics System Information
        public static bool getSupported(Feature feature_type)
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_graphics_getSupported((int)feature_type, out out_result);
            return out_result;
        }
        public static bool getCanvasFormats(Canvas.Format format_type)
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_graphics_getCanvasFormats((int)format_type, out out_result);
            return out_result;
        }
        public static bool getCompressedImageFormats(CompressedImageData.Format format_type)
        {
            bool out_result = false;
            Love2dDll.wrap_love_dll_graphics_getCompressedImageFormats((int)format_type, out out_result);
            return out_result;
        }
        public static void getRendererInfo(out string name, out string version, out string vendor, out string device)
        {
            IntPtr out_wss;
            Love2dDll.wrap_love_dll_graphics_getRendererInfo(out out_wss);
            var infos = DllTool.WSSToStringListAndRelease(out_wss);

            name = infos[0];
            version = infos[1];
            vendor = infos[2];
            device = infos[3];
        }
        public static double getSystemLimits(SystemLimit limit_type)
        {
            double out_result = 0;
            Love2dDll.wrap_love_dll_graphics_getSystemLimits((int)limit_type, out out_result);
            return out_result;
        }
        public static void getStats(out int out_drawCalls, out int out_canvasSwitches, out int out_shaderSwitches, out int out_canvases, out int out_images, out int out_fonts, out int out_textureMemory)
        {
            Love2dDll.wrap_love_dll_graphics_getStats(out out_drawCalls, out out_canvasSwitches, out out_shaderSwitches, out out_canvases, out out_images, out out_fonts, out out_textureMemory);
        }

        #endregion
    }
}
