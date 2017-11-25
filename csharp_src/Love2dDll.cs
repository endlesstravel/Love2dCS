// Author : endlesstravel
// this part provide C# interactive with C API

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;


using size_t = System.UInt32;
using Int64 = System.Int64;
using UInt8 = System.Byte;

using Love2d.Type;
using System.IO;

namespace Love2d
{
    class Love2dDll
    {
        const string DllPath = @"love.dll";

        #region common

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_common_getVersion(out IntPtr out_str);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_common_getVersionCodeName(out IntPtr out_str);


        #endregion

        #region release delete region
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_last_error(out IntPtr out_errormsg);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_release_obj(IntPtr p);
        //[DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        //internal extern static void wrap_love_dll_retain_obj(IntPtr p);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_delete(IntPtr p);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_delete_array(IntPtr p);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_delete_WrapString(IntPtr ws);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_delete_WrapSequenceString(IntPtr wss);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_delete_WrapSequenceInt2(IntPtr wsi2);

        #endregion

        #region *new*

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_filesystem_newFile(byte[] filename, int fmode, out IntPtr out_file);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_filesystem_newFileData_content(byte[] contents, int contents_length, byte[] filename, int decoder, out IntPtr out_filedata);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_filesystem_newFileData_file(IntPtr file, out IntPtr out_filedata);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_sound_newDecoder_filedata(IntPtr filedata, int bufferSize, out IntPtr out_Decoder);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_sound_newDecoder_file(IntPtr filedata, int bufferSize, out IntPtr out_Decoder);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_sound_newSoundData_decoder(IntPtr decoder, out IntPtr out_SoundData);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_sound_newSoundData(int samples, int rate, int bits, int channels, out IntPtr out_SoundData);



        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_audio_newSource_decoder(IntPtr decoder, int type, out IntPtr out_source);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_audio_newSource_sounddata(IntPtr sd, int type, out IntPtr out_source);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_mouse_newCursor(IntPtr imageData, int hotx, int hoty, out IntPtr out_cursor);


        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_image_newImageData_wh_data(int w, int h, byte[] data, int dataLength, out IntPtr out_imagedata);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_image_newImageData_filedata(IntPtr fileData, out IntPtr out_imagedata);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_image_newCompressedData(IntPtr fileData, out IntPtr out_imagedata);


        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_font_newRasterizer(IntPtr fileData, out IntPtr out_reasterizer);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_font_newTrueTypeRasterizer(int size, int hinting_type, out IntPtr out_reasterizer);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_font_newBMFontRasterizer(IntPtr fileData, IntPtr[] datas, int dataLength, out IntPtr out_reasterizer);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_font_newImageRasterizer(IntPtr imageData, byte[] glyphsStr, int extraspacing, out IntPtr out_reasterizer);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_font_newGlyphData_rasterizer_str(IntPtr r, byte[] glyphStr, out IntPtr out_GlyphData);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_font_newGlyphData_rasterizer_num(IntPtr r, int glyphCode, out IntPtr out_GlyphData);


        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_newVideoStream(IntPtr file, out IntPtr out_videostream);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_math_newRandomGenerator(out IntPtr out_RandomGenerator);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_math_newBezierCurve(Float2[] pointsList, int pointsList_lenght, out IntPtr out_BezierCurve);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Canvas_newImageData(IntPtr canvas, out IntPtr out_imageData);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Canvas_newImageData_xywh(IntPtr canvas, int x, int y, int w, int h, out IntPtr out_imageData);

        #endregion

        #region timer

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_timer_open_timer();
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_timer_step();
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_timer_getDelta(out float out_delta);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_timer_getFPS(out float out_fps);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_timer_getAverageDelta(out float out_averageDelta);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_timer_sleep(float t);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_timer_getTime(out float out_time);

        #endregion

        #region window

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_windows_open_love_window();
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_windows_getDisplayCount(out int out_count);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_windows_getDisplayName(int displayindex, out IntPtr out_name);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_windows_setMode_w_h(int width, int height);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_windows_setMode_w_h_setting(int width, int height, bool fullscreen, int fstype, bool vsync, int msaa, bool resizable, int minwidth, int minheight, bool borderless, bool centered, int display, bool highdpi, double refreshrate, bool useposition, int x, int y);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_windows_getMode(out int out_width, out int out_height, out bool out_fullscreen, out int out_fstype, out bool out_vsync, out int out_msaa, out bool out_resizable, out int out_minwidth, out int out_minheight, out bool out_borderless, out bool out_centered, out int out_display, out bool out_highdpi, out double out_refreshrate, out bool out_useposition, out int out_x, out int out_y);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_windows_getFullscreenModes(int displayindex, out IntPtr out_modes, out int out_modes_length);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_windows_setFullscreen(bool fullscreen, int fstype, out bool out_success);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_windows_getFullscreen(out bool out_fullscreen, out int out_fstype);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_windows_isOpen(out bool out_isopen);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_windows_close();
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_windows_getDesktopDimensions(int displayindex, out int out_width, out int out_height);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_windows_setPosition(int x, int y, int displayindex);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_windows_getPosition(out int out_x, out int out_y, out int out_displayindex);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_windows_setIcon(IntPtr imagedata, out bool out_success);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_windows_getIcon(out IntPtr out_imagedata);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_windows_setDisplaySleepEnabled(bool enable);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_windows_isDisplaySleepEnabled(out bool out_enable);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_windows_setTitle(byte[] titleStr);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_windows_getTitle(out IntPtr out_title);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_windows_hasFocus(out bool out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_windows_hasMouseFocus(out bool out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_windows_isVisible(out bool out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_windows_getPixelScale(out double out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_windows_toPixels(double value, out double out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_windows_fromPixels(double pixelvalue, out double out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_windows_minimize();
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_windows_maximize();
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_windows_isMaximized(out bool out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_windows_showMessageBox(byte[] title, byte[] message, int type, bool attachToWindow, out bool out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_windows_requestAttention(bool continuous);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_windows_windowToPixelCoords(out double out_x, out double out_y);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_windows_pixelToWindowCoords(out double x, out double y);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_windows_getPixelDimensions(out int out_w, out int out_h);

        #endregion

        #region mouse

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_open_love_mouse();
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_mouse_getSystemCursor(int sysctype, out IntPtr out_cursor);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_mouse_setCursor(IntPtr cursor);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_mouse_getCursor(out IntPtr out_cursor);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_mouse_hasCursor(out bool out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_mouse_getX(out double out_x);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_mouse_getY(out double out_y);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_mouse_getPosition(out double out_x, out double out_y);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_mouse_setX(double x);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_mouse_setY(double y);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_mouse_setPosition(double x, double y);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_mouse_isDown(int buttonIndex, out bool out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_mouse_setVisible(bool visible);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_mouse_isVisible(out bool out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_mouse_setGrabbed(bool grabbed);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_mouse_isGrabbed(out bool out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_mouse_setRelativeMode(bool enable, out bool out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_mouse_getRelativeMode(out bool out_result);

        #endregion

        #region keyboard

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_keyboard_open_love_keyboard();
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_keyboard_setKeyRepeat(bool enable);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_keyboard_hasKeyRepeat(out bool out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_keyboard_isDown(int key_type, out bool out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_keyboard_isScancodeDown(int scancode_type, out bool out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_keyboard_getScancodeFromKey(int key_type, out int out_scancode_type);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_keyboard_getKeyFromScancode(int scancode_type, out int out_key_type);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_keyboard_setTextInput(bool enable);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_keyboard_setTextInput_xywh(bool enable, double x, double y, double w, double h);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_keyboard_hasTextInput(out bool out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_keyboard_hasScreenKeyboard(out bool out_result);


        #endregion

        #region touch
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_touch_open_love_touch();
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_touch_open_love_getTouches(out IntPtr out_indexs, out IntPtr out_x, out IntPtr out_y, out IntPtr out_dx, out IntPtr out_dy, out IntPtr out_pressure, out int out_length);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_touch_open_love_getToucheInfo(long idx, out double out_x, out double out_y, out double out_dx, out double out_dy, out double out_pressure);

        #endregion

        #region joystick
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_joystick_open_love_joystick();
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_joystick_getJoysticks(out IntPtr out_sticks, out int out_sticks_lenght);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_joystick_getIndex(IntPtr j, out int out_index);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_joystick_getJoystickCount(out int out_sticks_lenght);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_joystick_setGamepadMapping(byte[] guid, int gp_inputType_type, int j_inputType_type, int inputIndex, int hat_type, out bool out_success);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_joystick_loadGamepadMappings(byte[] str, out bool out_success);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_joystick_saveGamepadMappings(out IntPtr out_str);

        #endregion

        #region Event

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_event_open_love_event();
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_event_push_nil();
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_event_poll(out bool out_hasEvent, out int out_event_type, out bool out_down_or_up, out bool out_bool, out int out_idx, out int out_enum1_type, out int out_enum2_type, out IntPtr out_str, out Int4 out_int4, out Float4 out_float4, out float out_float_value, out IntPtr out_joystick);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_event_wait(out bool out_hasEvent, out int out_event_type, out bool out_down_or_up, out bool out_bool, out int out_idx, out int out_enum1_type, out int out_enum2_type, out IntPtr out_str, out Int4 out_int4, out Float4 out_float4, out float out_float_value, out IntPtr out_joystick);

        #endregion

        #region filesystem

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_filesystem_open_love_filesystem();
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_filesystem_init(byte[] arg0);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_filesystem_setFused(bool flag);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_filesystem_isFused(out bool out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_filesystem_setAndroidSaveExternal(bool useExternal);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_filesystem_setIdentity(byte[] arg, bool append);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_filesystem_getIdentity(out IntPtr out_str);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_filesystem_setSource(byte[] arg);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_filesystem_getSource(out IntPtr out_str);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_filesystem_mount(byte[] archive, byte[] mountpoint, bool appendToPath, out bool out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_filesystem_unmount(byte[] archive, out bool out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_filesystem_getWorkingDirectory(out IntPtr out_str);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_filesystem_getUserDirectory(out IntPtr out_str);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_filesystem_getAppdataDirectory(out IntPtr out_str);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_filesystem_getSaveDirectory(out IntPtr out_str);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_filesystem_getSourceBaseDirectory(out IntPtr out_str);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_filesystem_getRealDirectory(byte[] filename, out IntPtr out_str);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_filesystem_getExecutablePath(out IntPtr out_str);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_filesystem_exists(byte[] arg, out bool out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_filesystem_isDirectory(byte[] arg, out bool out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_filesystem_isFile(byte[] arg, out bool out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_filesystem_isSymlink(byte[] filename, out bool out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_filesystem_createDirectory(byte[] arg);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_filesystem_remove(byte[] arg);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_filesystem_read(byte[] filename, long len, out IntPtr out_data, out uint out_data_length);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_filesystem_write(byte[] filename, byte[] input, size_t len);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_filesystem_append(byte[] filename, byte[] input, size_t len);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_filesystem_getDirectoryItems(byte[] dir, out IntPtr out_wss);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_filesystem_getLastModified(byte[] filename, out long out_time);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_filesystem_getSize(byte[] filename, out long out_size);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_filesystem_setSymlinksEnabled(bool enable);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_filesystem_areSymlinksEnabled(out bool out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_filesystem_getRequirePath(out IntPtr out_str);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_filesystem_setRequirePath(byte[] paths);


        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_filesystem_ext_allowMountingForPath(IntPtr pathStr);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_filesystem_ext_isRealDirectory(IntPtr pathStr, out bool out_result);

        #endregion

        #region sound

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_sound_luaopen_love_sound();
        #endregion

        #region audio
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_audio_open_love_audio();


        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_audio_getSourceCount(out int out_reslut);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_audio_stop();
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_audio_pause();
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_audio_resume();
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_audio_rewind();
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_audio_setVolume(float v);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_audio_getVolume(out float out_volume);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_audio_setPosition(float x, float y, float z);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_audio_getPosition(out float out_x, out float out_y, out float out_z);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_audio_setOrientation(float x, float y, float z, float dx, float dy, float dz);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_audio_getOrientation(out float out_x, out float out_y, out float out_z, out float out_dx, out float out_dy, out float out_dz);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_audio_setVelocity(float x, float y, float z);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_audio_getVelocity(out float out_x, out float out_y, out float out_z);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_audio_setDopplerScale(float scale);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_audio_getDopplerScale(out float out_scale);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_audio_canRecord(out bool out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_audio_setDistanceModel(int distancemodel_type);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_audio_getDistanceModel(out int out_distancemodel_type);


        #endregion

        #region image

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_image_open_love_image();
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_image_isCompressed(IntPtr fileData, out bool out_result);

        #endregion

        #region font

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_font_open_love_font();

        #endregion

        #region video

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_video_open_love_video();

        #endregion

        #region math

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_open_love_math();
       [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_math_triangulate(Float2[] pointsList, int pointsList_lenght, out IntPtr out_triArray, out int out_triCount);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_math_isConvex(Float2[] pointsList, int pointsList_lenght, out bool out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_math_gammaToLinear(float gama, out float out_liner);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_math_linearToGamma(float liner, out float out_gama);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_math_noise_1(float x, out float out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_math_noise_2(float x, float y, out float out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_math_noise_3(float x, float y, float z, out float out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_math_noise_4(float x, float y, float z, float w, out float out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_math_compress_str(byte[] str, int str_size, int format_type, int level, out IntPtr out_compressedData);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_math_compress_data(IntPtr data, int format_type, int level, out IntPtr out_compressedData);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_math_decompress_data(IntPtr data, out IntPtr out_datas, out int out_datas_length);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_math_decompress_bytes(int format_type, byte[] cbytes, int cbytes_length, out IntPtr out_datas, out int out_datas_length);


        #endregion



        #region graphics Object Creation

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_graphics_open_love_graphics();

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_graphics_newImage_file(IntPtr[] imageDataList, int imageDataListLength, bool flagMipmaps, bool flagLinear, out IntPtr out_image);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_graphics_newImage(IntPtr[] compressedImageDataList, int compressedImageDataListLength, bool flagMipmaps, bool flagLinear, out IntPtr out_image);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_graphics_newQuad(double x, double y, double w, double h, double sw, double sh, out IntPtr out_quad);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_graphics_newFont(IntPtr rasterizer, out IntPtr out_font);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_graphics_newSpriteBatch(IntPtr texture, int maxSprites, int usage_type, out IntPtr out_spriteBatch);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_graphics_newParticleSystem(IntPtr texture, int buffer, out IntPtr out_particleSystem);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_graphics_newCanvas(int width, int height, int format_type, int msaa, out IntPtr out_canvas);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_graphics_newShader(byte[] vertexCodeStr,byte[] pixelCodeStr, out IntPtr out_shader);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_graphics_newMesh_specifiedVertices(Float2[] pos, Float2[] uv, Int4[] color, int vertexCount, int drawMode_type, int usage_type, out IntPtr out_mesh);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_graphics_newMesh_count(int count, int drawMode_type, int usage_type, out IntPtr out_mesh);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_graphics_newText(IntPtr font, BytePtr[] coloredStringText, Int4[] coloredStringColor,  int coloredStringLength, out IntPtr out_text);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_graphics_newVideo(IntPtr videoStream, out IntPtr out_video);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_graphics_newScreenshot(bool copyAlpha, out IntPtr out_imageData);


        #endregion

        #region graphics State

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_graphics_reset();
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_graphics_isActive(out bool out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_graphics_isGammaCorrect(out bool out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_graphics_setScissor();
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_graphics_setScissor_xywh(int x, int y, int w, int h);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_graphics_intersectScissor(int x, int y, int w, int h);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_graphics_getScissor(out int out_x, out int out_y, out int out_w, out int out_h);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_graphics_setStencilTest(int compare_type, int compareValue);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_graphics_getStencilTest(out int out_compare_type, out int out_compareValue);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_graphics_setColor(int r, int g, int b, int a);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_graphics_getColor(out int out_r, out int out_g, out int out_b, out int out_a);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_graphics_setBackgroundColor(int r, int g, int b, int a);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_graphics_getBackgroundColor(out int out_r, out int out_g, out int out_b, out int out_a);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_graphics_setFont(IntPtr font);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_graphics_getFont(out IntPtr out_font);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_graphics_setColorMask(bool r, bool g, bool b, bool a);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_graphics_getColorMask(out bool out_r, out bool out_g, out bool out_b, out bool out_a);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_graphics_setBlendMode(int blendMode_type, int blendAlphaMode_type);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_graphics_getBlendMode(out int out_blendMode_type, out int out_blendAlphaMode_type);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_graphics_setDefaultFilter(int filterModeMagMin_type, int filterModeMag_type, int anisotropy);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_graphics_getDefaultFilter(out int out_filterModeMagMin_type, out int out_filterModeMag_type, out int out_anisotropy);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_graphics_setLineWidth(float width);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_graphics_setLineStyle(int style_type);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_graphics_setLineJoin(int join_type);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_graphics_getLineWidth(out float out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_graphics_getLineStyle(out int out_lineStyle_type);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_graphics_getLineJoin(out int out_lineJoinStyle_type);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_graphics_setPointSize(float size);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_graphics_getPointSize(out float out_size);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_graphics_setWireframe(bool enable);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_graphics_isWireframe(out bool out_isWireFrame);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_graphics_setCanvas(IntPtr[] canvaList, int canvaListLength);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_graphics_getCanvas(out IntPtr out_canvas, out int out_canvas_lenght);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_graphics_setShader(IntPtr shader);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_graphics_getShader(out IntPtr out_shader);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_graphics_setDefaultShaderCode(
            byte[] glsl_codeVertexStr, byte[] glsl_codePixelxStr, byte[] glsl_videoPixelCodeStr,
            byte[] glsl_codeVertexStr_gammacorrect, byte[] glsl_codePixelxStr_gammacorrect, byte[] glsl_videoPixelCodeStr_gammacorrect,
            byte[] glsles_codeVertexStr, byte[] glsles_codePixelxStr, byte[] glsles_videoPixelCodeStr,
            byte[] glsles_codeVertexStr_gammacorrect, byte[] glsles_codePixelxStr_gammacorrect, byte[] glsles_videoPixelCodeStr_gammacorrect
        );

        #endregion

        #region graphics Coordinate System
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_graphics_push(int stack_type);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_graphics_pop();
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_graphics_rotate(float angle);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_graphics_scale(float sx, float sy);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_graphics_translate(float x, float y);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_graphics_shear(float kx, float ky);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_graphics_origin();

        #endregion

        #region graphics drawing
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_graphics_ext_stencil_clearStencil();
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_graphics_ext_stencil_drawToStencilBuffer(int action_type, int stencilValue);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_graphics_ext_stencil_stopDrawToStencilBuffer();

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_graphics_clear_rgba(float r, float g, float b, float a);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_graphics_clear_rgbalist(Float4[] colorList, bool[] enableList, int listLength);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_graphics_discard(bool[] discardColors, int discardColorsLength, bool discardStencil);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_graphics_present();

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_graphics_draw_drawable(IntPtr drawable, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_graphics_draw_texture_quad(IntPtr quad, IntPtr texture, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_graphics_print(BytePtr[] coloredStringListStr, Int4[] coloredStringListColor, int coloredStringListLength, float x, float y, float angle, float sx, float sy, float ox, float oy, float kx, float ky);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_graphics_printf(BytePtr[] coloredStringListStr, Int4[] coloredStringListColor, int coloredStringListLength, float x, float y, float wrap, int align_type, float angle, float sx, float sy, float ox, float oy, float kx, float ky);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_graphics_rectangle(int mode_type, float x, float y, float w, float h);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_graphics_rectangle_with_rounded_corners(int mode_type, float x, float y, float w, float h, float rx, float ry, int points);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_graphics_circle(int mode_type, float x, float y, float radius, int points);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_graphics_ellipse(int mode_type, float x, float y, float a, float b, int points);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_graphics_arc(int mode_type, int arcmode_type, float x, float y, float radius, float angle1, float angle2);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_graphics_arc_points(int mode_type, int arcmode_type, float x, float y, float radius, float angle1, float angle2, int points);


        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_graphics_points(Float2[] coords, int coordsLength);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_graphics_points_colors(Float2[] coords, Int4[] colors, int coordsLength);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_graphics_line(Float2[] coords, int coordsLength);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_graphics_polygon(int mode_type, Float2[] coords, int coordsLength);
        #endregion

        #region graphics Window
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_graphics_isCreated(out bool out_reslut);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_graphics_getWidth(out int out_width);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_graphics_getHeight(out int out_height);

        #endregion

        #region graphics System Information
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_graphics_getSupported(int feature_type, out bool out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_graphics_getCanvasFormats(int format_type, out bool out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_graphics_getCompressedImageFormats(int format_type, out bool out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_graphics_getRendererInfo(out IntPtr out_wss);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_graphics_getSystemLimits(int limit_type, out double out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_graphics_getStats(out int out_drawCalls, out int out_canvasSwitches, out int out_shaderSwitches, out int out_canvases, out int out_images, out int out_fonts, out int out_textureMemory);



        #endregion


        #region type - Source
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Source_clone(IntPtr t, out IntPtr out_clone);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Source_play(IntPtr t, out bool out_success);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Source_stop(IntPtr t);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Source_pause(IntPtr t);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Source_resume(IntPtr t);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Source_rewind(IntPtr t);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Source_setPitch(IntPtr t, float pitch);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Source_getPitch(IntPtr t, out float out_pitch);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Source_setVolume(IntPtr t, float p);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Source_getVolume(IntPtr t, out float out_volume);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Source_seek(IntPtr t, float offset, int unit_type);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Source_tell(IntPtr t, int unit_type, out float out_position);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Source_getDuration(IntPtr t, int unit_type, out float out_duration);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Source_setPosition(IntPtr t, float x, float y, float z);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Source_getPosition(IntPtr t, out float out_x, out float out_y, out float out_z);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Source_setVelocity(IntPtr t, float x, float y, float z);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Source_getVelocity(IntPtr t, out float out_x, out float out_y, out float out_z);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Source_setDirection(IntPtr t, float x, float y, float z);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Source_getDirection(IntPtr t, out float out_x, out float out_y, out float out_z);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Source_setCone(IntPtr t, float innerAngle, float outerAngle, float outerVolume);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Source_getCone(IntPtr t, out float out_innerAngle, out float out_outerAngle, out float out_outerVolume);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Source_setRelative(IntPtr t, bool relative);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Source_isRelative(IntPtr t, out bool out_relative);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Source_setLooping(IntPtr t, bool looping);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Source_isLooping(IntPtr t, out bool out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Source_isStopped(IntPtr t, out bool out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Source_isPaused(IntPtr t, out bool out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Source_isPlaying(IntPtr t, out bool out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Source_setVolumeLimits(IntPtr t, float vmin, float vmax);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Source_getVolumeLimits(IntPtr t, out float out_vmin, out float out_vmax);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Source_setAttenuationDistances(IntPtr t, float dref, float dmax);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Source_getAttenuationDistances(IntPtr t, out float out_dref, out float out_dmax);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Source_setRolloff(IntPtr t, float rolloff);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Source_getRolloff(IntPtr t, out float out_rolloff);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Source_getChannels(IntPtr t, out int out_chanels);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Source_getType(IntPtr t, out int out_type);

        #endregion

        #region type - File

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_File_getSize(IntPtr file, out double out_size);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_File_open(IntPtr file, int mode_type);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_File_close(IntPtr file, out bool out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_File_isOpen(IntPtr file, out bool out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_File_read(IntPtr file, Int64 size, out IntPtr out_data, out Int64 out_readedSize);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_File_write_String(IntPtr file, byte[] data, Int64 datasize);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_File_write_Data_datasize(IntPtr file, IntPtr data, Int64 datasize);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_File_flush(IntPtr file);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_File_isEOF(IntPtr file, out bool out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_File_tell(IntPtr file, out Int64 out_pos);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_File_seek(IntPtr file, Int64 pos);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_File_setBuffer(IntPtr file, int bufmode_type, Int64 size);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_File_getBuffer(IntPtr file, out int out_bufmode_type, out Int64 out_size);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_File_getMode(IntPtr file, out int out_mode_type);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_File_getFilename(IntPtr file, out IntPtr out_filename);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_File_getExtension(IntPtr file, out IntPtr out_extension);


        #endregion

        #region type - FileData

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_FileData_getFilename(IntPtr t, out IntPtr out_filename);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_FileData_getExtension(IntPtr t, out IntPtr out_extension);

        #endregion

        #region type - GlyphData

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_GlyphData_getWidth(IntPtr t, out int out_width);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_GlyphData_getHeight(IntPtr t, out int out_height);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_GlyphData_getGlyph(IntPtr t, out uint out_glyph);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_GlyphData_getGlyphString(IntPtr t, out IntPtr out_str);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_GlyphData_getAdvance(IntPtr t, out int out_advance);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_GlyphData_getBearing(IntPtr t, out int out_bearingX, out int out_bearingY);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_GlyphData_getBoundingBox(IntPtr t, out int out_minX, out int out_minY, out int out_width, out int out_height);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_GlyphData_getFormat(IntPtr t, out int out_format_type);


        #endregion

        #region type - Rasterizer

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Rasterizer_getHeight(IntPtr t, out int out_heigth);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Rasterizer_getAdvance(IntPtr t, out int out_advance);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Rasterizer_getAscent(IntPtr t, out int out_ascent);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Rasterizer_getDescent(IntPtr t, out int out_descent);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Rasterizer_getLineHeight(IntPtr t, out int out_lineHeight);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Rasterizer_getGlyphData_uint32(IntPtr t, uint glyph, out IntPtr out_glyphData);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Rasterizer_getGlyphData_string(IntPtr t, byte[] str, out IntPtr out_glyphData);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Rasterizer_getGlyphCount(IntPtr t, out int out_count);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Rasterizer_hasGlyphs_uint32(IntPtr t, uint glyph, out bool out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Rasterizer_hasGlyphs_string(IntPtr t, byte[] str, out bool out_result);


        #endregion

        #region type - Canvas

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Canvas_getFormat(IntPtr canvas, out int out_format_type);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Canvas_getMSAA(IntPtr canvas, out int out_msaa);

        #endregion

        #region type - Font

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Font_getHeight(IntPtr t, out int out_height);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Font_getWidth(IntPtr t, byte[] str, out int out_width);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Font_getWrap(IntPtr t, BytePtr[] coloredStringText, Int4[] coloredStringColor, int coloredStringLength, float wrap, out int out_maxWidth, out IntPtr out_pws);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Font_setLineHeight(IntPtr t, float h);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Font_getLineHeight(IntPtr t, out float out_h);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Font_setFilter(IntPtr t, int min_type, int mag_type, float anisotropy);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Font_getFilter(IntPtr t, out int out_min_type, out int out_mag_type, out float out_anisotropy);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Font_getAscent(IntPtr t, out int out_ascent);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Font_getDescent(IntPtr t, out int out_descent);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Font_getBaseline(IntPtr t, out float out_baseline);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Font_hasGlyphs_uint32(IntPtr t, uint chr, out bool out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Font_hasGlyphs_string(IntPtr t, byte[] str, out bool out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Font_setFallbacks(IntPtr t, IntPtr[] fallback, int length);


        #endregion

        #region type - Image

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Image_setMipmapFilter(IntPtr image, int mipmap_type, float sharpness);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Image_getMipmapFilter(IntPtr image, out int out_mipmap_type, out float out_sharpness);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Image_isCompressed(IntPtr image, out bool out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Image_refresh(IntPtr image, int xoffset, int yoffset, int w, int h);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Image_getData(IntPtr image, out IntPtr out_datas, out int out_datas_lenght);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Image_getFlags(IntPtr image, out bool out_mipmaps, out bool out_linear);


        #endregion

        #region type - Mesh

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Mesh_setVertices_data(IntPtr p, IntPtr data, uint vertoffset);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Mesh_setVertices(IntPtr p, uint vertoffset, byte[] srcData, uint nvertices);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Mesh_setVertex(IntPtr p, uint index, byte[] data, int data_length);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Mesh_getVertex(IntPtr p, uint index, out IntPtr out_data, out int out_data_length, out int out_count);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Mesh_setVertexAttribute(IntPtr p, uint vertindex, int attribindex, uint data0, uint data1, uint data2, uint data3);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Mesh_getVertexAttribute(IntPtr p, uint vertindex, int attribindex, out int out_type, out int out_components, out uint out_data0, out uint out_data1, out uint out_data2, out uint out_data3);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Mesh_getVertexCount(IntPtr p, out int out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Mesh_getVertexFormat(IntPtr p, out IntPtr out_names, out IntPtr out_datatype, out IntPtr out_components, out int out_length);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Mesh_setAttributeEnabled(IntPtr p, byte[] name, bool enable);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Mesh_isAttributeEnabled(IntPtr p, byte[] name, out bool out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Mesh_attachAttribute(IntPtr p, byte[] name, IntPtr otherMesh);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Mesh_flush(IntPtr p);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Mesh_setVertexMap_nil(IntPtr p);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Mesh_setVertexMap(IntPtr p, uint[] vertexmaps, int vertexmaps_length);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Mesh_getVertexMap(IntPtr p, out bool out_has_vertex_map, out IntPtr out_vertexmaps, out int out_vertexmaps_length);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Mesh_setTexture_nil(IntPtr p);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Mesh_setTexture_Texture(IntPtr p, IntPtr tex);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Mesh_getTexture(IntPtr p, out IntPtr out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Mesh_setDrawMode(IntPtr p, int mode_type);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Mesh_getDrawMode(IntPtr p, out int out_mode_type);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Mesh_setDrawRange(IntPtr p);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Mesh_setDrawRange_minmax(IntPtr p, int rangemin, int rangemax);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Mesh_getDrawRange(IntPtr p, out int out_rangemin, out int out_rangemax);



        #endregion

        #region type - ParticleSystem

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_ParticleSystem_clone(IntPtr p, out IntPtr out_clone);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ParticleSystem_setTexture(IntPtr p, IntPtr tex);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_ParticleSystem_getTexture(IntPtr p, out IntPtr out_texture);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_ParticleSystem_setBufferSize(IntPtr p, uint buffersize);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ParticleSystem_getBufferSize(IntPtr p, out uint out_buffersize);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ParticleSystem_setInsertMode(IntPtr p, int mode_type);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ParticleSystem_getInsertMode(IntPtr p, out int out_mode_type);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_ParticleSystem_setEmissionRate(IntPtr p, float rate);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ParticleSystem_getEmissionRate(IntPtr p, out float out_rate);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ParticleSystem_setEmitterLifetime(IntPtr p, float lifetime);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ParticleSystem_getEmitterLifetime(IntPtr p, out float out_lifetime);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ParticleSystem_setParticleLifetime(IntPtr p, float min, float max);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ParticleSystem_getParticleLifetime(IntPtr p, out int out_min, out int out_max);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ParticleSystem_setPosition(IntPtr p, float x, float y);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ParticleSystem_getPosition(IntPtr p, out float out_x, out float out_y);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ParticleSystem_moveTo(IntPtr p, float x, float y);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_ParticleSystem_setAreaSpread(IntPtr p, int distribution_type, float x, float y);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ParticleSystem_getAreaSpread(IntPtr p, out int out_distribution_type, out float out_x, out float out_y);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ParticleSystem_setDirection(IntPtr p, float direction);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ParticleSystem_getDirection(IntPtr p, out float out_direction);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ParticleSystem_setSpread(IntPtr p, float spread);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ParticleSystem_getSpread(IntPtr p, out float out_spread);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ParticleSystem_setSpeed(IntPtr p, float min, float max);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ParticleSystem_getSpeed(IntPtr p, out float out_min, out float out_max);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ParticleSystem_setLinearAcceleration(IntPtr p, float xmin, float ymin, float xmax, float ymax);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ParticleSystem_getLinearAcceleration(IntPtr p, out float out_xmin, out float out_ymin, out float out_xmax, out float out_ymax);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ParticleSystem_setRadialAcceleration(IntPtr p, float min, float max);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ParticleSystem_getRadialAcceleration(IntPtr p, out int out_min, out int out_max);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ParticleSystem_setTangentialAcceleration(IntPtr p, float min, float max);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ParticleSystem_getTangentialAcceleration(IntPtr p, out int out_min, out int out_max);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ParticleSystem_setLinearDamping(IntPtr p, float min, float max);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ParticleSystem_getLinearDamping(IntPtr p, out int out_min, out int out_max);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_ParticleSystem_setSizes(IntPtr p, float[] sizearray, int sizearray_length);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ParticleSystem_getSizes(IntPtr p, out IntPtr out_sizearray, out int out_sizearray_length);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_ParticleSystem_setSizeVariation(IntPtr p, float variation);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ParticleSystem_getSizeVariation(IntPtr p, out float out_variation);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ParticleSystem_setRotation(IntPtr p, float min, float max);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ParticleSystem_getRotation(IntPtr p, out int out_min, out int out_max);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ParticleSystem_setSpin(IntPtr p, float start, float end);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ParticleSystem_getSpin(IntPtr p, out float out_start, out float out_end);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ParticleSystem_setSpinVariation(IntPtr p, float variation);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ParticleSystem_getSpinVariation(IntPtr p, out float out_variation);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ParticleSystem_setOffset(IntPtr p, float x, float y);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ParticleSystem_getOffset(IntPtr p, out float out_x, out float out_y);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_ParticleSystem_setColors(IntPtr p, Int4[] colorarray, int colorarray_length);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ParticleSystem_getColors(IntPtr p, out IntPtr out_colorarray, out int out_colorarray_length);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ParticleSystem_setQuads(IntPtr p, IntPtr[] quadsarray, int quadsarray_length);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ParticleSystem_getQuads(IntPtr p, out IntPtr out_quadsarray, out int out_quadsarray_length);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ParticleSystem_setRelativeRotation(IntPtr p, bool enable);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ParticleSystem_hasRelativeRotation(IntPtr p, out bool out_enable);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ParticleSystem_getCount(IntPtr p, out uint out_count);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ParticleSystem_start(IntPtr p);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ParticleSystem_stop(IntPtr p);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ParticleSystem_pause(IntPtr p);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ParticleSystem_reset(IntPtr p);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ParticleSystem_emit(IntPtr p, int num);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ParticleSystem_isActive(IntPtr p, out bool out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ParticleSystem_isPaused(IntPtr p, out bool out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ParticleSystem_isStopped(IntPtr p, out bool out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ParticleSystem_update(IntPtr p, float dt);


        #endregion

        #region type - Quad

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Quad_setViewport(IntPtr p, float x, float y, float w, float h);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Quad_getViewport(IntPtr p, out float out_x, out float out_y, out float out_w, out float out_h);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Quad_getTextureDimensions(IntPtr p, out double out_sw, out double out_sh);

        #endregion

        #region type - Shader

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Shader_getWarnings(IntPtr p, out IntPtr out_str);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Shader_sendColors(IntPtr p, byte[] name, Int4[] valuearray, int value_lenght);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Shader_sendFloats(IntPtr p, byte[] name, float[] valuearray, int valuearray_lenght);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Shader_sendInts(IntPtr p, byte[] name, int[] valuearray, int valuearray_lenght);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Shader_sendBooleans(IntPtr p, byte[] name, bool[] valuearray, int valuearray_lenght);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Shader_sendMatrices(IntPtr p, byte[] name, float[] valuearray, int valuearray_lenght);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Shader_sendTexture(IntPtr p, byte[] name, IntPtr texture);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Shader_getExternVariable(IntPtr p, byte[] name, out bool out_variableExists, out int out_uniform_type, out int out_components, out int out_arrayelements);

        #endregion

        #region type - SpriteBatch

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_SpriteBatch_add(IntPtr p, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky, out int out_index);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_SpriteBatch_add_Quad(IntPtr p, IntPtr quad, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky, out int out_index);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_SpriteBatch_set(IntPtr p, int index, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_SpriteBatch_set_Quad(IntPtr p, int index, IntPtr quad, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_SpriteBatch_clear(IntPtr p);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_SpriteBatch_flush(IntPtr p);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_SpriteBatch_setTexture(IntPtr p, IntPtr tex);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_SpriteBatch_getTexture(IntPtr p, out IntPtr out_texture);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_SpriteBatch_setColor_nil(IntPtr p);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_SpriteBatch_setColor(IntPtr p, int r, int g, int b, int a);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_SpriteBatch_getColor(IntPtr p, out bool out_exist, out int out_r, out int out_g, out int out_b, out int out_a);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_SpriteBatch_getCount(IntPtr p, out int out_count);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_SpriteBatch_setBufferSize(IntPtr p, int size);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_SpriteBatch_getBufferSize(IntPtr p, out int out_buffersize);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_SpriteBatch_attachAttribute(IntPtr p, byte[] name, IntPtr mesh);


        #endregion

        #region type - Text
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Text_set_nil(IntPtr p);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Text_set_coloredstring(IntPtr p, BytePtr[] coloredStringText, Int4[] coloredStringColor, int coloredStringLength);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Text_setf(IntPtr p, BytePtr[] coloredStringText, Int4[] coloredStringColor, int coloredStringLength, float wraplimit, int align_type);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Text_add(IntPtr p, BytePtr[] coloredStringText, Int4[] coloredStringColor, int coloredStringLength, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky, out int out_index);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Text_addf(IntPtr p, BytePtr[] coloredStringText, Int4[] coloredStringColor, int coloredStringLength, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky, float wraplimit, int align_type, out int out_index);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Text_clear(IntPtr p);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Text_setFont(IntPtr p, IntPtr f);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Text_getFont(IntPtr p, out IntPtr font);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Text_getWidth(IntPtr p, int index, out int out_w);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Text_getHeight(IntPtr p, int index, out int out_h);

        #endregion

        #region type - Texture
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Texture_getWidth(IntPtr p, out int out_w);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Texture_getHeight(IntPtr p, out int out_h);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Texture_setFilter(IntPtr p, int filtermin_type, int filtermag_type, float anisotropy);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Texture_getFilter(IntPtr p, out int out_filtermin_type, out int out_filtermag_type, out float out_anisotropy);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Texture_setWrap(IntPtr p, int wraphoriz_type, int wrapvert_type);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Texture_getWrap(IntPtr p, out int out_wraphoriz_type, out int out_wrapvert_type);

        #endregion

        #region type - Video

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Video_getStream(IntPtr p, out IntPtr out_videsStream);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Video_getSource(IntPtr p, out IntPtr out_source);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Video_setSource_nil(IntPtr p);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Video_setSource(IntPtr p, IntPtr source);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Video_getWidth(IntPtr p, out int out_w);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Video_getHeight(IntPtr p, out int out_h);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_Video_setFilter(IntPtr p, int filtermin_type, int filtermag_type, float anisotropy);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Video_getFilter(IntPtr p, out int out_filtermin_type, out int out_filtermag_type, out float out_anisotropy);

        #endregion

        #region type - CompressedImageData

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_CompressedImageData_getWidth(IntPtr p, int miplevel, out int out_w);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_CompressedImageData_getHeight(IntPtr p, int miplevel, out int out_h);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_CompressedImageData_getMipmapCount(IntPtr p, out int out_count);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_CompressedImageData_getFormat(IntPtr p, out int out_format_type);


        #endregion

        #region type - ImageData

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ImageData_getWidth(IntPtr p, out int out_w);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ImageData_getHeight(IntPtr p, out int out_h);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_ImageData_getPixel(IntPtr p, int x, int y, out byte out_r, out byte out_g, out byte out_b, out byte out_a);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_ImageData_setPixel(IntPtr p, int x, int y, byte r, byte g, byte b, byte a);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ImageData_ext_getPixelUnsafe(IntPtr p, int x, int y, out byte out_r, out byte out_g, out byte out_b, out byte out_a);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ImageData_ext_setPixelUnsafe(IntPtr p, int x, int y, byte r, byte g, byte b, byte a);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ImageData_paste(IntPtr p, IntPtr src_imageData, int dx, int dy, int sx, int sy, int sw, int sh);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ImageData_encode(IntPtr p, int format_type, byte[] filename);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ImageData_ext_mutexLock(IntPtr p, out IntPtr out_lock);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_ImageData_ext_mutexUnlock(IntPtr p, IntPtr ptrLock);


        #endregion

        #region type - Cursor

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Cursor_getType(IntPtr p, out int out_cursortype_type);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Cursor_getSystemType(IntPtr p, out int out_systype_type);

        #endregion

        #region type - Decoder

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Decoder_getChannels(IntPtr p, out int out_channels);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Decoder_getBitDepth(IntPtr p, out int out_bitDepth);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Decoder_getSampleRate(IntPtr p, out int out_sampleRate);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Decoder_getDuration(IntPtr p, out double out_duration);


        #endregion

        #region type - SoundData

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_SoundData_getChannels(IntPtr p, out int out_channels);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_SoundData_getBitDepth(IntPtr p, out int out_bitDepth);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_SoundData_getSampleRate(IntPtr p, out int out_sampleRate);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_SoundData_getSampleCount(IntPtr p, out int out_sampleCount);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_SoundData_getDuration(IntPtr p, out double out_duration);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_SoundData_setSample(IntPtr p, int i, float sample);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_SoundData_getSample(IntPtr p, int i, out float out_sample);


        #endregion

        #region type - VideoStream

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_VideoStream_getFilename(IntPtr p, out IntPtr out_filename);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_VideoStream_play(IntPtr p);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_VideoStream_pause(IntPtr p);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_VideoStream_seek(IntPtr p, double offset);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_VideoStream_rewind(IntPtr p);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_VideoStream_tell(IntPtr p, out double out_position);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_VideoStream_isPlaying(IntPtr p, out bool out_isplaying);


        #endregion

        #region type - BezierCurve

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_BezierCurve_getDegree(IntPtr p, out int out_degree);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_BezierCurve_getDerivative(IntPtr p, out IntPtr out_deriv);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_BezierCurve_getControlPoint(IntPtr p, int idx, out float out_x, out float out_y);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_BezierCurve_setControlPoint(IntPtr p, int idx, float x, float y);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_BezierCurve_insertControlPoint(IntPtr p, int idx, float x, float y);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_BezierCurve_removeControlPoint(IntPtr p, int idx);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_BezierCurve_getControlPointCount(IntPtr p, out int out_count);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_BezierCurve_translate(IntPtr p, float dx, float dy);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_BezierCurve_rotate(IntPtr p, double phi, float ox, float oy);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_BezierCurve_scale(IntPtr p, double s, float ox, float oy);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_BezierCurve_evaluate(IntPtr p, double t, out float out_x, out float out_y);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_BezierCurve_getSegment(IntPtr p, double t1, double t2, out IntPtr out_segment);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_BezierCurve_render(IntPtr p, int accuracy, out IntPtr out_points, out int out_points_lenght);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_BezierCurve_renderSegment(IntPtr p, double start, double end, int accuracy, out IntPtr out_points, out int out_points_lenght);
        #endregion

        #region type - RandomGenerator

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_RandomGenerator_random(IntPtr p, out double out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_RandomGenerator_randomNormal(IntPtr p, double stddev, double mean, out double out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_RandomGenerator_setSeed(IntPtr p, uint low, uint high);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_RandomGenerator_getSeed(IntPtr p, out uint out_low, out uint out_high);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_type_RandomGenerator_setState(IntPtr p, byte[] state);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_RandomGenerator_getState(IntPtr p, out IntPtr out_str);


        #endregion

        #region type - Joystick

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Joystick_isConnected(IntPtr p, out bool out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Joystick_getName(IntPtr p, out IntPtr out_str);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Joystick_getID(IntPtr p, out int out_id);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Joystick_getInstanceID(IntPtr p, out int out_instanceid);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Joystick_getGUID(IntPtr p, out IntPtr out_str);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Joystick_getAxisCount(IntPtr p, out int out_count);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Joystick_getButtonCount(IntPtr p, out int out_count);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Joystick_getHatCount(IntPtr p, out int out_count);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Joystick_getAxis(IntPtr p, int axisindex, out float out_axis);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Joystick_getAxes(IntPtr p, out IntPtr out_axes, out int out_axes_length);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Joystick_getHat(IntPtr p, int hatindex, out int out_hat_type);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Joystick_isDown(IntPtr p, int button, out bool out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Joystick_isGamepad(IntPtr p, out bool out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Joystick_getGamepadAxis(IntPtr p, int axis_type, out float out_gamepadaxis);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Joystick_isGamepadDown(IntPtr p, int gamepadButton_type, out bool out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Joystick_isVibrationSupported(IntPtr p, out bool out_result);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Joystick_setVibration_nil(IntPtr p, out bool out_success);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Joystick_setVibration(IntPtr p, float left, float right, float duration, out bool out_success);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Joystick_getVibration(IntPtr p, out float out_left, out float out_right);

        #endregion

        #region type - Other

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_type_Data_getSize(IntPtr data, out uint out_datasize);

        #endregion
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct WrapString
    {
        //[MarshalAs(UnmanagedType.LPStr)]
        internal IntPtr data;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct WrapSequenceString
    {
        internal int len;
        internal IntPtr data;
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct Int2
    {
        public int x, y;
    };
    [StructLayout(LayoutKind.Sequential)]
    public struct Int4
    {
        public int x, y, z, w;
        public int r { get { return x; } }
        public int g { get { return y; } }
        public int b { get { return z; } }
        public int a { get { return w; } }
        public Int4(int x, int y, int z, int w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct Float2
    {
        public Float2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        public float x, y;
    };
    [StructLayout(LayoutKind.Sequential)]
    public struct Float3
    {
        internal float x, y, z;
        public Float3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Float4
    {
        public float x, y, z, w;
        public Float4(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Float6
    {
        public float x, y, z;
        public float dx, dy, dz;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct BytePtr
    {
        IntPtr bytes;
        public BytePtr(IntPtr bytes) { this.bytes = bytes; }
    }
}
