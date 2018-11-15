// Author : endlesstravel
// this part provide C# interactive with C API

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;


using size_t = System.UInt32;
using Int64 = System.Int64;
using UInt8 = System.Byte;
using BytePtr = System.IntPtr;

namespace Love
{
    class Love2dDll
    {
        static bool CheckCAPIException(bool hasNoException)
        {
            if (hasNoException == false)
            {
                string errInfo = DllTool.GetLoveLastError();
                throw new Exception(errInfo);
            }

            return hasNoException;
        }

        const string DllPath = @"love";


        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void inner_wrap_love_dll_type_c_size_info();

        #region platform
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void inner_wrap_love_dll_get_win32_handle(out IntPtr out_str);
        #endregion

        #region common
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_common_getVersion")]
        internal extern static void _wrap_love_dll_common_getVersion(out IntPtr out_str);
        internal static void wrap_love_dll_common_getVersion(out IntPtr out_str)
        {
            _wrap_love_dll_common_getVersion(out out_str);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_common_getVersionCodeName")]
        internal extern static void _wrap_love_dll_common_getVersionCodeName(out IntPtr out_str);
        internal static void wrap_love_dll_common_getVersionCodeName(out IntPtr out_str)
        {
            _wrap_love_dll_common_getVersionCodeName(out out_str);
        }

        #endregion

        #region release delete region
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_last_error")]
        internal extern static void _wrap_love_dll_last_error(out IntPtr out_errormsg);
        internal static void wrap_love_dll_last_error(out IntPtr out_errormsg)
        {
            _wrap_love_dll_last_error(out out_errormsg);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_release_obj")]
        internal extern static void _wrap_love_dll_release_obj(IntPtr p);
        internal static void wrap_love_dll_release_obj(IntPtr p)
        {
            _wrap_love_dll_release_obj(p);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_delete")]
        internal extern static void _wrap_love_dll_delete(IntPtr p);
        internal static void wrap_love_dll_delete(IntPtr p)
        {
            _wrap_love_dll_delete(p);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_delete_array")]
        internal extern static void _wrap_love_dll_delete_array(IntPtr p);
        internal static void wrap_love_dll_delete_array(IntPtr p)
        {
            _wrap_love_dll_delete_array(p);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_delete_WrapString")]
        internal extern static void _wrap_love_dll_delete_WrapString(IntPtr ws);
        internal static void wrap_love_dll_delete_WrapString(IntPtr ws)
        {
            _wrap_love_dll_delete_WrapString(ws);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_delete_WrapSequenceString")]
        internal extern static void _wrap_love_dll_delete_WrapSequenceString(IntPtr wss);
        internal static void wrap_love_dll_delete_WrapSequenceString(IntPtr wss)
        {
            _wrap_love_dll_delete_WrapSequenceString(wss);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_delete_WrapSequenceInt2")]
        internal extern static void _wrap_love_dll_delete_WrapSequenceInt2(IntPtr wsi2);
        internal static void wrap_love_dll_delete_WrapSequenceInt2(IntPtr wsi2)
        {
            _wrap_love_dll_delete_WrapSequenceInt2(wsi2);
        }

        #endregion

        #region *new*
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_newFile")]
        internal extern static bool _wrap_love_dll_filesystem_newFile(byte[] filename, int fmode, out IntPtr out_file);
        internal static bool wrap_love_dll_filesystem_newFile(byte[] filename, int fmode, out IntPtr out_file)
        {
            return CheckCAPIException(_wrap_love_dll_filesystem_newFile(filename, fmode, out out_file));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_newFileData_content")]
        internal extern static bool _wrap_love_dll_filesystem_newFileData_content(byte[] contents, int contents_length, byte[] filename, out IntPtr out_filedata);
        internal static bool wrap_love_dll_filesystem_newFileData_content(byte[] contents, int contents_length, byte[] filename, out IntPtr out_filedata)
        {
            return CheckCAPIException(_wrap_love_dll_filesystem_newFileData_content(contents, contents_length, filename, out out_filedata));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_newFileData_file")]
        internal extern static bool _wrap_love_dll_filesystem_newFileData_file(IntPtr file, out IntPtr out_filedata);
        internal static bool wrap_love_dll_filesystem_newFileData_file(IntPtr file, out IntPtr out_filedata)
        {
            return CheckCAPIException(_wrap_love_dll_filesystem_newFileData_file(file, out out_filedata));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_sound_newDecoder_filedata")]
        internal extern static bool _wrap_love_dll_sound_newDecoder_filedata(IntPtr filedata, int bufferSize, out IntPtr out_Decoder);
        internal static bool wrap_love_dll_sound_newDecoder_filedata(IntPtr filedata, int bufferSize, out IntPtr out_Decoder)
        {
            return CheckCAPIException(_wrap_love_dll_sound_newDecoder_filedata(filedata, bufferSize, out out_Decoder));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_sound_newDecoder_file")]
        internal extern static bool _wrap_love_dll_sound_newDecoder_file(IntPtr filedata, int bufferSize, out IntPtr out_Decoder);
        internal static bool wrap_love_dll_sound_newDecoder_file(IntPtr filedata, int bufferSize, out IntPtr out_Decoder)
        {
            return CheckCAPIException(_wrap_love_dll_sound_newDecoder_file(filedata, bufferSize, out out_Decoder));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_sound_newSoundData_decoder")]
        internal extern static bool _wrap_love_dll_sound_newSoundData_decoder(IntPtr decoder, out IntPtr out_SoundData);
        internal static bool wrap_love_dll_sound_newSoundData_decoder(IntPtr decoder, out IntPtr out_SoundData)
        {
            return CheckCAPIException(_wrap_love_dll_sound_newSoundData_decoder(decoder, out out_SoundData));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_sound_newSoundData")]
        internal extern static bool _wrap_love_dll_sound_newSoundData(int samples, int rate, int bits, int channels, out IntPtr out_SoundData);
        internal static bool wrap_love_dll_sound_newSoundData(int samples, int rate, int bits, int channels, out IntPtr out_SoundData)
        {
            return CheckCAPIException(_wrap_love_dll_sound_newSoundData(samples, rate, bits, channels, out out_SoundData));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_audio_newSource_decoder")]
        internal extern static bool _wrap_love_dll_audio_newSource_decoder(IntPtr decoder, int type, out IntPtr out_source);
        internal static bool wrap_love_dll_audio_newSource_decoder(IntPtr decoder, int type, out IntPtr out_source)
        {
            return CheckCAPIException(_wrap_love_dll_audio_newSource_decoder(decoder, type, out out_source));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_audio_newSource_sounddata")]
        internal extern static bool _wrap_love_dll_audio_newSource_sounddata(IntPtr sd, out IntPtr out_source);
        internal static bool wrap_love_dll_audio_newSource_sounddata(IntPtr sd, out IntPtr out_source)
        {
            return CheckCAPIException(_wrap_love_dll_audio_newSource_sounddata(sd, out out_source));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_mouse_newCursor")]
        internal extern static bool _wrap_love_dll_mouse_newCursor(IntPtr imageData, int hotx, int hoty, out IntPtr out_cursor);
        internal static bool wrap_love_dll_mouse_newCursor(IntPtr imageData, int hotx, int hoty, out IntPtr out_cursor)
        {
            return CheckCAPIException(_wrap_love_dll_mouse_newCursor(imageData, hotx, hoty, out out_cursor));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_image_newImageData_wh_format_data")]
        internal extern static bool _wrap_love_dll_image_newImageData_wh_format_data(int w, int h, byte[] data, int dataLength, int format_type, out IntPtr out_imagedata);
        internal static bool wrap_love_dll_image_newImageData_wh_format_data(int w, int h, byte[] data, int dataLength, int format_type, out IntPtr out_imagedata)
        {
            return CheckCAPIException(_wrap_love_dll_image_newImageData_wh_format_data(w, h, data, dataLength, format_type, out out_imagedata));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_image_newImageData_filedata")]
        internal extern static bool _wrap_love_dll_image_newImageData_filedata(IntPtr fileData, out IntPtr out_imagedata);
        internal static bool wrap_love_dll_image_newImageData_filedata(IntPtr fileData, out IntPtr out_imagedata)
        {
            return CheckCAPIException(_wrap_love_dll_image_newImageData_filedata(fileData, out out_imagedata));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_image_newCompressedData")]
        internal extern static bool _wrap_love_dll_image_newCompressedData(IntPtr fileData, out IntPtr out_imagedata);
        internal static bool wrap_love_dll_image_newCompressedData(IntPtr fileData, out IntPtr out_imagedata)
        {
            return CheckCAPIException(_wrap_love_dll_image_newCompressedData(fileData, out out_imagedata));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_font_newRasterizer")]
        internal extern static bool _wrap_love_dll_font_newRasterizer(IntPtr fileData, out IntPtr out_reasterizer);
        internal static bool wrap_love_dll_font_newRasterizer(IntPtr fileData, out IntPtr out_reasterizer)
        {
            return CheckCAPIException(_wrap_love_dll_font_newRasterizer(fileData, out out_reasterizer));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_font_newTrueTypeRasterizer")]
        internal extern static bool _wrap_love_dll_font_newTrueTypeRasterizer(int size, int hinting_type, out IntPtr out_reasterizer);
        internal static bool wrap_love_dll_font_newTrueTypeRasterizer(int size, int hinting_type, out IntPtr out_reasterizer)
        {
            return CheckCAPIException(_wrap_love_dll_font_newTrueTypeRasterizer(size, hinting_type, out out_reasterizer));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_font_newTrueTypeRasterizer_data")]
        internal extern static bool _wrap_love_dll_font_newTrueTypeRasterizer_data(IntPtr data, int size, int hinting_type, out IntPtr out_reasterizer);
        internal static bool wrap_love_dll_font_newTrueTypeRasterizer_data(IntPtr data, int size, int hinting_type, out IntPtr out_reasterizer)
        {
            return CheckCAPIException(_wrap_love_dll_font_newTrueTypeRasterizer_data(data, size, hinting_type, out out_reasterizer));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_font_newBMFontRasterizer")]
        internal extern static bool _wrap_love_dll_font_newBMFontRasterizer(IntPtr fileData, IntPtr[] datas, int dataLength, out IntPtr out_reasterizer);
        internal static bool wrap_love_dll_font_newBMFontRasterizer(IntPtr fileData, IntPtr[] datas, int dataLength, out IntPtr out_reasterizer)
        {
            return CheckCAPIException(_wrap_love_dll_font_newBMFontRasterizer(fileData, datas, dataLength, out out_reasterizer));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_font_newImageRasterizer")]
        internal extern static bool _wrap_love_dll_font_newImageRasterizer(IntPtr imageData, byte[] glyphsStr, int extraspacing, out IntPtr out_reasterizer);
        internal static bool wrap_love_dll_font_newImageRasterizer(IntPtr imageData, byte[] glyphsStr, int extraspacing, out IntPtr out_reasterizer)
        {
            return CheckCAPIException(_wrap_love_dll_font_newImageRasterizer(imageData, glyphsStr, extraspacing, out out_reasterizer));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_font_newGlyphData_rasterizer_str")]
        internal extern static bool _wrap_love_dll_font_newGlyphData_rasterizer_str(IntPtr r, byte[] glyphStr, out IntPtr out_GlyphData);
        internal static bool wrap_love_dll_font_newGlyphData_rasterizer_str(IntPtr r, byte[] glyphStr, out IntPtr out_GlyphData)
        {
            return CheckCAPIException(_wrap_love_dll_font_newGlyphData_rasterizer_str(r, glyphStr, out out_GlyphData));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_font_newGlyphData_rasterizer_num")]
        internal extern static bool _wrap_love_dll_font_newGlyphData_rasterizer_num(IntPtr r, int glyphCode, out IntPtr out_GlyphData);
        internal static bool wrap_love_dll_font_newGlyphData_rasterizer_num(IntPtr r, int glyphCode, out IntPtr out_GlyphData)
        {
            return CheckCAPIException(_wrap_love_dll_font_newGlyphData_rasterizer_num(r, glyphCode, out out_GlyphData));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_newVideoStream")]
        internal extern static bool _wrap_love_dll_newVideoStream(IntPtr file, out IntPtr out_videostream);
        internal static bool wrap_love_dll_newVideoStream(IntPtr file, out IntPtr out_videostream)
        {
            return CheckCAPIException(_wrap_love_dll_newVideoStream(file, out out_videostream));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_math_newRandomGenerator")]
        internal extern static bool _wrap_love_dll_math_newRandomGenerator(out IntPtr out_RandomGenerator);
        internal static bool wrap_love_dll_math_newRandomGenerator(out IntPtr out_RandomGenerator)
        {
            return CheckCAPIException(_wrap_love_dll_math_newRandomGenerator(out out_RandomGenerator));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_math_newBezierCurve")]
        internal extern static void _wrap_love_dll_math_newBezierCurve(Float2[] pointsList, int pointsList_lenght, out IntPtr out_BezierCurve);
        internal static void wrap_love_dll_math_newBezierCurve(Float2[] pointsList, int pointsList_lenght, out IntPtr out_BezierCurve)
        {
            _wrap_love_dll_math_newBezierCurve(pointsList, pointsList_lenght, out out_BezierCurve);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Canvas_newImageData_xywh")]
        internal extern static bool _wrap_love_dll_type_Canvas_newImageData_xywh(IntPtr canvas, int slice, int mipmap, int x, int y, int w, int h, out IntPtr out_imageData);
        internal static bool wrap_love_dll_type_Canvas_newImageData_xywh(IntPtr canvas, int slice, int mipmap, int x, int y, int w, int h, out IntPtr out_imageData)
        {
            return CheckCAPIException(_wrap_love_dll_type_Canvas_newImageData_xywh(canvas, slice, mipmap, x, y, w, h, out out_imageData));
        }

        #endregion

        #region timer
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_timer_open_timer")]
        internal extern static bool _wrap_love_dll_timer_open_timer();
        internal static bool wrap_love_dll_timer_open_timer()
        {
            return CheckCAPIException(_wrap_love_dll_timer_open_timer());
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_timer_step")]
        internal extern static void _wrap_love_dll_timer_step();
        internal static void wrap_love_dll_timer_step()
        {
            _wrap_love_dll_timer_step();
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_timer_getDelta")]
        internal extern static void _wrap_love_dll_timer_getDelta(out float out_delta);
        internal static void wrap_love_dll_timer_getDelta(out float out_delta)
        {
            _wrap_love_dll_timer_getDelta(out out_delta);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_timer_getFPS")]
        internal extern static void _wrap_love_dll_timer_getFPS(out float out_fps);
        internal static void wrap_love_dll_timer_getFPS(out float out_fps)
        {
            _wrap_love_dll_timer_getFPS(out out_fps);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_timer_getAverageDelta")]
        internal extern static void _wrap_love_dll_timer_getAverageDelta(out float out_averageDelta);
        internal static void wrap_love_dll_timer_getAverageDelta(out float out_averageDelta)
        {
            _wrap_love_dll_timer_getAverageDelta(out out_averageDelta);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_timer_sleep")]
        internal extern static void _wrap_love_dll_timer_sleep(float t);
        internal static void wrap_love_dll_timer_sleep(float t)
        {
            _wrap_love_dll_timer_sleep(t);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_timer_getTime")]
        internal extern static void _wrap_love_dll_timer_getTime(out float out_time);
        internal static void wrap_love_dll_timer_getTime(out float out_time)
        {
            _wrap_love_dll_timer_getTime(out out_time);
        }

        #endregion

        #region window

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_open_love_window")]
        internal extern static bool _wrap_love_dll_windows_open_love_window();
        internal static bool wrap_love_dll_windows_open_love_window()
        {
            return CheckCAPIException(_wrap_love_dll_windows_open_love_window());
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_getDisplayCount")]
        internal extern static void _wrap_love_dll_windows_getDisplayCount(out int out_count);
        internal static void wrap_love_dll_windows_getDisplayCount(out int out_count)
        {
            _wrap_love_dll_windows_getDisplayCount(out out_count);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_getDisplayName")]
        internal extern static bool _wrap_love_dll_windows_getDisplayName(int displayindex, out IntPtr out_name);
        internal static bool wrap_love_dll_windows_getDisplayName(int displayindex, out IntPtr out_name)
        {
            return CheckCAPIException(_wrap_love_dll_windows_getDisplayName(displayindex, out out_name));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_setMode_w_h")]
        internal extern static bool _wrap_love_dll_windows_setMode_w_h(int width, int height);
        internal static bool wrap_love_dll_windows_setMode_w_h(int width, int height)
        {
            return CheckCAPIException(_wrap_love_dll_windows_setMode_w_h(width, height));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_setMode_w_h_setting")]
        internal extern static bool _wrap_love_dll_windows_setMode_w_h_setting(int width, int height, bool fullscreen, int fstype, bool vsync, int msaa, int depth, bool stencil, bool resizable, int minwidth, int minheight, bool borderless, bool centered, int display, bool highdpi, double refreshrate, bool useposition, int x, int y);
        internal static bool wrap_love_dll_windows_setMode_w_h_setting(int width, int height, bool fullscreen, int fstype, bool vsync, int msaa, int depth, bool stencil, bool resizable, int minwidth, int minheight, bool borderless, bool centered, int display, bool highdpi, double refreshrate, bool useposition, int x, int y)
        {
            return CheckCAPIException(_wrap_love_dll_windows_setMode_w_h_setting(width, height, fullscreen, fstype, vsync, msaa, depth, stencil, resizable, minwidth, minheight, borderless, centered, display, highdpi, refreshrate, useposition, x, y));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_getMode")]
        internal extern static void _wrap_love_dll_windows_getMode(out int out_width, out int out_height, out bool out_fullscreen, out int out_fstype, out bool out_vsync, out int out_msaa, out int out_depth, out bool out_stencil, out bool out_resizable, out int out_minwidth, out int out_minheight, out bool out_borderless, out bool out_centered, out int out_display, out bool out_highdpi, out double out_refreshrate, out bool out_useposition, out int out_x, out int out_y);
        internal static void wrap_love_dll_windows_getMode(out int out_width, out int out_height, out bool out_fullscreen, out int out_fstype, out bool out_vsync, out int out_msaa, out int out_depth, out bool out_stencil, out bool out_resizable, out int out_minwidth, out int out_minheight, out bool out_borderless, out bool out_centered, out int out_display, out bool out_highdpi, out double out_refreshrate, out bool out_useposition, out int out_x, out int out_y)
        {
            _wrap_love_dll_windows_getMode(out out_width, out out_height, out out_fullscreen, out out_fstype, out out_vsync, out out_msaa, out out_depth, out out_stencil, out out_resizable, out out_minwidth, out out_minheight, out out_borderless, out out_centered, out out_display, out out_highdpi, out out_refreshrate, out out_useposition, out out_x, out out_y);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_getFullscreenModes")]
        internal extern static void _wrap_love_dll_windows_getFullscreenModes(int displayindex, out IntPtr out_modes, out int out_modes_length);
        internal static void wrap_love_dll_windows_getFullscreenModes(int displayindex, out IntPtr out_modes, out int out_modes_length)
        {
            _wrap_love_dll_windows_getFullscreenModes(displayindex, out out_modes, out out_modes_length);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_setFullscreen")]
        internal extern static void _wrap_love_dll_windows_setFullscreen(bool fullscreen, int fstype, out bool out_success);
        internal static void wrap_love_dll_windows_setFullscreen(bool fullscreen, int fstype, out bool out_success)
        {
            _wrap_love_dll_windows_setFullscreen(fullscreen, fstype, out out_success);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_getFullscreen")]
        internal extern static void _wrap_love_dll_windows_getFullscreen(out bool out_fullscreen, out int out_fstype);
        internal static void wrap_love_dll_windows_getFullscreen(out bool out_fullscreen, out int out_fstype)
        {
            _wrap_love_dll_windows_getFullscreen(out out_fullscreen, out out_fstype);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_isOpen")]
        internal extern static void _wrap_love_dll_windows_isOpen(out bool out_isopen);
        internal static void wrap_love_dll_windows_isOpen(out bool out_isopen)
        {
            _wrap_love_dll_windows_isOpen(out out_isopen);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_close")]
        internal extern static void _wrap_love_dll_windows_close();
        internal static void wrap_love_dll_windows_close()
        {
            _wrap_love_dll_windows_close();
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_getDesktopDimensions")]
        internal extern static void _wrap_love_dll_windows_getDesktopDimensions(int displayindex, out int out_width, out int out_height);
        internal static void wrap_love_dll_windows_getDesktopDimensions(int displayindex, out int out_width, out int out_height)
        {
            _wrap_love_dll_windows_getDesktopDimensions(displayindex, out out_width, out out_height);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_setPosition")]
        internal extern static void _wrap_love_dll_windows_setPosition(int x, int y, int displayindex);
        internal static void wrap_love_dll_windows_setPosition(int x, int y, int displayindex)
        {
            _wrap_love_dll_windows_setPosition(x, y, displayindex);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_getPosition")]
        internal extern static void _wrap_love_dll_windows_getPosition(out int out_x, out int out_y, out int out_displayindex);
        internal static void wrap_love_dll_windows_getPosition(out int out_x, out int out_y, out int out_displayindex)
        {
            _wrap_love_dll_windows_getPosition(out out_x, out out_y, out out_displayindex);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_setIcon")]
        internal extern static void _wrap_love_dll_windows_setIcon(IntPtr imagedata, out bool out_success);
        internal static void wrap_love_dll_windows_setIcon(IntPtr imagedata, out bool out_success)
        {
            _wrap_love_dll_windows_setIcon(imagedata, out out_success);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_getIcon")]
        internal extern static void _wrap_love_dll_windows_getIcon(out IntPtr out_imagedata);
        internal static void wrap_love_dll_windows_getIcon(out IntPtr out_imagedata)
        {
            _wrap_love_dll_windows_getIcon(out out_imagedata);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_setDisplaySleepEnabled")]
        internal extern static void _wrap_love_dll_windows_setDisplaySleepEnabled(bool enable);
        internal static void wrap_love_dll_windows_setDisplaySleepEnabled(bool enable)
        {
            _wrap_love_dll_windows_setDisplaySleepEnabled(enable);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_isDisplaySleepEnabled")]
        internal extern static void _wrap_love_dll_windows_isDisplaySleepEnabled(out bool out_enable);
        internal static void wrap_love_dll_windows_isDisplaySleepEnabled(out bool out_enable)
        {
            _wrap_love_dll_windows_isDisplaySleepEnabled(out out_enable);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_setTitle")]
        internal extern static void _wrap_love_dll_windows_setTitle(byte[] titleStr);
        internal static void wrap_love_dll_windows_setTitle(byte[] titleStr)
        {
            _wrap_love_dll_windows_setTitle(titleStr);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_getTitle")]
        internal extern static void _wrap_love_dll_windows_getTitle(out IntPtr out_title);
        internal static void wrap_love_dll_windows_getTitle(out IntPtr out_title)
        {
            _wrap_love_dll_windows_getTitle(out out_title);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_hasFocus")]
        internal extern static void _wrap_love_dll_windows_hasFocus(out bool out_result);
        internal static void wrap_love_dll_windows_hasFocus(out bool out_result)
        {
            _wrap_love_dll_windows_hasFocus(out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_hasMouseFocus")]
        internal extern static void _wrap_love_dll_windows_hasMouseFocus(out bool out_result);
        internal static void wrap_love_dll_windows_hasMouseFocus(out bool out_result)
        {
            _wrap_love_dll_windows_hasMouseFocus(out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_isVisible")]
        internal extern static void _wrap_love_dll_windows_isVisible(out bool out_result);
        internal static void wrap_love_dll_windows_isVisible(out bool out_result)
        {
            _wrap_love_dll_windows_isVisible(out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_getDPIScale")]
        internal extern static void _wrap_love_dll_windows_getDPIScale(out double out_result);
        internal static void wrap_love_dll_windows_getDPIScale(out double out_result)
        {
            _wrap_love_dll_windows_getDPIScale(out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_toPixels")]
        internal extern static void _wrap_love_dll_windows_toPixels(double value, out double out_result);
        internal static void wrap_love_dll_windows_toPixels(double value, out double out_result)
        {
            _wrap_love_dll_windows_toPixels(value, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_fromPixels")]
        internal extern static void _wrap_love_dll_windows_fromPixels(double pixelvalue, out double out_result);
        internal static void wrap_love_dll_windows_fromPixels(double pixelvalue, out double out_result)
        {
            _wrap_love_dll_windows_fromPixels(pixelvalue, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_minimize")]
        internal extern static void _wrap_love_dll_windows_minimize();
        internal static void wrap_love_dll_windows_minimize()
        {
            _wrap_love_dll_windows_minimize();
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_maximize")]
        internal extern static void _wrap_love_dll_windows_maximize();
        internal static void wrap_love_dll_windows_maximize()
        {
            _wrap_love_dll_windows_maximize();
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_isMaximized")]
        internal extern static void _wrap_love_dll_windows_isMaximized(out bool out_result);
        internal static void wrap_love_dll_windows_isMaximized(out bool out_result)
        {
            _wrap_love_dll_windows_isMaximized(out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_showMessageBox")]
        internal extern static void _wrap_love_dll_windows_showMessageBox(byte[] title, byte[] message, int type, bool attachToWindow, out bool out_result);
        internal static void wrap_love_dll_windows_showMessageBox(byte[] title, byte[] message, int type, bool attachToWindow, out bool out_result)
        {
            _wrap_love_dll_windows_showMessageBox(title, message, type, attachToWindow, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_showMessageBox_list")]
        internal extern static void _wrap_love_dll_windows_showMessageBox_list(byte[] title, byte[] message, IntPtr[] buttonName, int buttonsLength, int enterButtonIndex, int escapebuttonIndex, int type, bool attachToWindow, out int out_index_returned);
        internal static void wrap_love_dll_windows_showMessageBox_list(byte[] title, byte[] message, IntPtr[] buttonName, int buttonsLength, int enterButtonIndex, int escapebuttonIndex, int type, bool attachToWindow, out int out_index_returned)
        {
            _wrap_love_dll_windows_showMessageBox_list(title, message, buttonName, buttonsLength, enterButtonIndex, escapebuttonIndex, type, attachToWindow, out out_index_returned);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_requestAttention")]
        internal extern static void _wrap_love_dll_windows_requestAttention(bool continuous);
        internal static void wrap_love_dll_windows_requestAttention(bool continuous)
        {
            _wrap_love_dll_windows_requestAttention(continuous);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_windowToPixelCoords")]
        internal extern static void _wrap_love_dll_windows_windowToPixelCoords(out double out_x, out double out_y);
        internal static void wrap_love_dll_windows_windowToPixelCoords(out double out_x, out double out_y)
        {
            _wrap_love_dll_windows_windowToPixelCoords(out out_x, out out_y);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_windows_pixelToWindowCoords")]
        internal extern static void _wrap_love_dll_windows_pixelToWindowCoords(out double x, out double y);
        internal static void wrap_love_dll_windows_pixelToWindowCoords(out double x, out double y)
        {
            _wrap_love_dll_windows_pixelToWindowCoords(out x, out y);
        }

        #endregion

        #region mouse
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_open_love_mouse")]
        internal extern static bool _wrap_love_dll_open_love_mouse();
        internal static bool wrap_love_dll_open_love_mouse()
        {
            return CheckCAPIException(_wrap_love_dll_open_love_mouse());
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_mouse_getSystemCursor")]
        internal extern static bool _wrap_love_dll_mouse_getSystemCursor(int sysctype, out IntPtr out_cursor);
        internal static bool wrap_love_dll_mouse_getSystemCursor(int sysctype, out IntPtr out_cursor)
        {
            return CheckCAPIException(_wrap_love_dll_mouse_getSystemCursor(sysctype, out out_cursor));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_mouse_setCursor")]
        internal extern static void _wrap_love_dll_mouse_setCursor(IntPtr cursor);
        internal static void wrap_love_dll_mouse_setCursor(IntPtr cursor)
        {
            _wrap_love_dll_mouse_setCursor(cursor);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_mouse_getCursor")]
        internal extern static void _wrap_love_dll_mouse_getCursor(out IntPtr out_cursor);
        internal static void wrap_love_dll_mouse_getCursor(out IntPtr out_cursor)
        {
            _wrap_love_dll_mouse_getCursor(out out_cursor);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_mouse_isCursorSupported")]
        internal extern static void _wrap_love_dll_mouse_isCursorSupported(out bool out_result);
        internal static void wrap_love_dll_mouse_isCursorSupported(out bool out_result)
        {
            _wrap_love_dll_mouse_isCursorSupported(out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_mouse_getX")]
        internal extern static void _wrap_love_dll_mouse_getX(out double out_x);
        internal static void wrap_love_dll_mouse_getX(out double out_x)
        {
            _wrap_love_dll_mouse_getX(out out_x);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_mouse_getY")]
        internal extern static void _wrap_love_dll_mouse_getY(out double out_y);
        internal static void wrap_love_dll_mouse_getY(out double out_y)
        {
            _wrap_love_dll_mouse_getY(out out_y);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_mouse_getPosition")]
        internal extern static void _wrap_love_dll_mouse_getPosition(out double out_x, out double out_y);
        internal static void wrap_love_dll_mouse_getPosition(out double out_x, out double out_y)
        {
            _wrap_love_dll_mouse_getPosition(out out_x, out out_y);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_mouse_setX")]
        internal extern static void _wrap_love_dll_mouse_setX(double x);
        internal static void wrap_love_dll_mouse_setX(double x)
        {
            _wrap_love_dll_mouse_setX(x);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_mouse_setY")]
        internal extern static void _wrap_love_dll_mouse_setY(double y);
        internal static void wrap_love_dll_mouse_setY(double y)
        {
            _wrap_love_dll_mouse_setY(y);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_mouse_setPosition")]
        internal extern static void _wrap_love_dll_mouse_setPosition(double x, double y);
        internal static void wrap_love_dll_mouse_setPosition(double x, double y)
        {
            _wrap_love_dll_mouse_setPosition(x, y);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_mouse_isDown")]
        internal extern static void _wrap_love_dll_mouse_isDown(int buttonIndex, out bool out_result);
        internal static void wrap_love_dll_mouse_isDown(int buttonIndex, out bool out_result)
        {
            _wrap_love_dll_mouse_isDown(buttonIndex, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_mouse_setVisible")]
        internal extern static void _wrap_love_dll_mouse_setVisible(bool visible);
        internal static void wrap_love_dll_mouse_setVisible(bool visible)
        {
            _wrap_love_dll_mouse_setVisible(visible);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_mouse_isVisible")]
        internal extern static void _wrap_love_dll_mouse_isVisible(out bool out_result);
        internal static void wrap_love_dll_mouse_isVisible(out bool out_result)
        {
            _wrap_love_dll_mouse_isVisible(out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_mouse_setGrabbed")]
        internal extern static void _wrap_love_dll_mouse_setGrabbed(bool grabbed);
        internal static void wrap_love_dll_mouse_setGrabbed(bool grabbed)
        {
            _wrap_love_dll_mouse_setGrabbed(grabbed);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_mouse_isGrabbed")]
        internal extern static void _wrap_love_dll_mouse_isGrabbed(out bool out_result);
        internal static void wrap_love_dll_mouse_isGrabbed(out bool out_result)
        {
            _wrap_love_dll_mouse_isGrabbed(out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_mouse_setRelativeMode")]
        internal extern static void _wrap_love_dll_mouse_setRelativeMode(bool enable, out bool out_result);
        internal static void wrap_love_dll_mouse_setRelativeMode(bool enable, out bool out_result)
        {
            _wrap_love_dll_mouse_setRelativeMode(enable, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_mouse_getRelativeMode")]
        internal extern static void _wrap_love_dll_mouse_getRelativeMode(out bool out_result);
        internal static void wrap_love_dll_mouse_getRelativeMode(out bool out_result)
        {
            _wrap_love_dll_mouse_getRelativeMode(out out_result);
        }

        #endregion

        #region keyboard
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_keyboard_open_love_keyboard")]
        internal extern static bool _wrap_love_dll_keyboard_open_love_keyboard();
        internal static bool wrap_love_dll_keyboard_open_love_keyboard()
        {
            return CheckCAPIException(_wrap_love_dll_keyboard_open_love_keyboard());
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_keyboard_setKeyRepeat")]
        internal extern static void _wrap_love_dll_keyboard_setKeyRepeat(bool enable);
        internal static void wrap_love_dll_keyboard_setKeyRepeat(bool enable)
        {
            _wrap_love_dll_keyboard_setKeyRepeat(enable);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_keyboard_hasKeyRepeat")]
        internal extern static void _wrap_love_dll_keyboard_hasKeyRepeat(out bool out_result);
        internal static void wrap_love_dll_keyboard_hasKeyRepeat(out bool out_result)
        {
            _wrap_love_dll_keyboard_hasKeyRepeat(out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_keyboard_isDown")]
        internal extern static void _wrap_love_dll_keyboard_isDown(int key_type, out bool out_result);
        internal static void wrap_love_dll_keyboard_isDown(int key_type, out bool out_result)
        {
            _wrap_love_dll_keyboard_isDown(key_type, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_keyboard_isScancodeDown")]
        internal extern static void _wrap_love_dll_keyboard_isScancodeDown(int scancode_type, out bool out_result);
        internal static void wrap_love_dll_keyboard_isScancodeDown(int scancode_type, out bool out_result)
        {
            _wrap_love_dll_keyboard_isScancodeDown(scancode_type, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_keyboard_getScancodeFromKey")]
        internal extern static void _wrap_love_dll_keyboard_getScancodeFromKey(int key_type, out int out_scancode_type);
        internal static void wrap_love_dll_keyboard_getScancodeFromKey(int key_type, out int out_scancode_type)
        {
            _wrap_love_dll_keyboard_getScancodeFromKey(key_type, out out_scancode_type);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_keyboard_getKeyFromScancode")]
        internal extern static void _wrap_love_dll_keyboard_getKeyFromScancode(int scancode_type, out int out_key_type);
        internal static void wrap_love_dll_keyboard_getKeyFromScancode(int scancode_type, out int out_key_type)
        {
            _wrap_love_dll_keyboard_getKeyFromScancode(scancode_type, out out_key_type);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_keyboard_setTextInput")]
        internal extern static void _wrap_love_dll_keyboard_setTextInput(bool enable);
        internal static void wrap_love_dll_keyboard_setTextInput(bool enable)
        {
            _wrap_love_dll_keyboard_setTextInput(enable);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_keyboard_setTextInput_xywh")]
        internal extern static void _wrap_love_dll_keyboard_setTextInput_xywh(bool enable, double x, double y, double w, double h);
        internal static void wrap_love_dll_keyboard_setTextInput_xywh(bool enable, double x, double y, double w, double h)
        {
            _wrap_love_dll_keyboard_setTextInput_xywh(enable, x, y, w, h);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_keyboard_hasTextInput")]
        internal extern static void _wrap_love_dll_keyboard_hasTextInput(out bool out_result);
        internal static void wrap_love_dll_keyboard_hasTextInput(out bool out_result)
        {
            _wrap_love_dll_keyboard_hasTextInput(out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_keyboard_hasScreenKeyboard")]
        internal extern static void _wrap_love_dll_keyboard_hasScreenKeyboard(out bool out_result);
        internal static void wrap_love_dll_keyboard_hasScreenKeyboard(out bool out_result)
        {
            _wrap_love_dll_keyboard_hasScreenKeyboard(out out_result);
        }

        #endregion

        #region touch
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_touch_open_love_touch")]
        internal extern static bool _wrap_love_dll_touch_open_love_touch();
        internal static bool wrap_love_dll_touch_open_love_touch()
        {
            return CheckCAPIException(_wrap_love_dll_touch_open_love_touch());
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_touch_open_love_getTouches")]
        internal extern static void _wrap_love_dll_touch_open_love_getTouches(out IntPtr out_indexs, out IntPtr out_x, out IntPtr out_y, out IntPtr out_dx, out IntPtr out_dy, out IntPtr out_pressure, out int out_length);
        internal static void wrap_love_dll_touch_open_love_getTouches(out IntPtr out_indexs, out IntPtr out_x, out IntPtr out_y, out IntPtr out_dx, out IntPtr out_dy, out IntPtr out_pressure, out int out_length)
        {
            _wrap_love_dll_touch_open_love_getTouches(out out_indexs, out out_x, out out_y, out out_dx, out out_dy, out out_pressure, out out_length);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_touch_open_love_getToucheInfo")]
        internal extern static bool _wrap_love_dll_touch_open_love_getToucheInfo(long idx, out double out_x, out double out_y, out double out_dx, out double out_dy, out double out_pressure);
        internal static bool wrap_love_dll_touch_open_love_getToucheInfo(long idx, out double out_x, out double out_y, out double out_dx, out double out_dy, out double out_pressure)
        {
            return CheckCAPIException(_wrap_love_dll_touch_open_love_getToucheInfo(idx, out out_x, out out_y, out out_dx, out out_dy, out out_pressure));
        }


        #endregion

        #region joystick
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_joystick_open_love_joystick")]
        internal extern static bool _wrap_love_dll_joystick_open_love_joystick();
        internal static bool wrap_love_dll_joystick_open_love_joystick()
        {
            return CheckCAPIException(_wrap_love_dll_joystick_open_love_joystick());
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_joystick_getJoysticks")]
        internal extern static void _wrap_love_dll_joystick_getJoysticks(out IntPtr out_sticks, out int out_sticks_lenght);
        internal static void wrap_love_dll_joystick_getJoysticks(out IntPtr out_sticks, out int out_sticks_lenght)
        {
            _wrap_love_dll_joystick_getJoysticks(out out_sticks, out out_sticks_lenght);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_joystick_getIndex")]
        internal extern static void _wrap_love_dll_joystick_getIndex(IntPtr j, out int out_index);
        internal static void wrap_love_dll_joystick_getIndex(IntPtr j, out int out_index)
        {
            _wrap_love_dll_joystick_getIndex(j, out out_index);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_joystick_getJoystickCount")]
        internal extern static void _wrap_love_dll_joystick_getJoystickCount(out int out_sticks_lenght);
        internal static void wrap_love_dll_joystick_getJoystickCount(out int out_sticks_lenght)
        {
            _wrap_love_dll_joystick_getJoystickCount(out out_sticks_lenght);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_joystick_setGamepadMapping")]
        internal extern static void _wrap_love_dll_joystick_setGamepadMapping(byte[] guid, int gp_inputType_type, int j_inputType_type, int inputIndex, int hat_type, out bool out_success);
        internal static void wrap_love_dll_joystick_setGamepadMapping(byte[] guid, int gp_inputType_type, int j_inputType_type, int inputIndex, int hat_type, out bool out_success)
        {
            _wrap_love_dll_joystick_setGamepadMapping(guid, gp_inputType_type, j_inputType_type, inputIndex, hat_type, out out_success);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_joystick_loadGamepadMappings")]
        internal extern static void _wrap_love_dll_joystick_loadGamepadMappings(byte[] str, out bool out_success);
        internal static void wrap_love_dll_joystick_loadGamepadMappings(byte[] str, out bool out_success)
        {
            _wrap_love_dll_joystick_loadGamepadMappings(str, out out_success);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_joystick_saveGamepadMappings")]
        internal extern static void _wrap_love_dll_joystick_saveGamepadMappings(out IntPtr out_str);
        internal static void wrap_love_dll_joystick_saveGamepadMappings(out IntPtr out_str)
        {
            _wrap_love_dll_joystick_saveGamepadMappings(out out_str);
        }

        #endregion

        #region Event
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_event_open_love_event")]
        internal extern static bool _wrap_love_dll_event_open_love_event();
        internal static bool wrap_love_dll_event_open_love_event()
        {
            return CheckCAPIException(_wrap_love_dll_event_open_love_event());
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_event_push_nil")]
        internal extern static void _wrap_love_dll_event_push_nil();
        internal static void wrap_love_dll_event_push_nil()
        {
            _wrap_love_dll_event_push_nil();
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_event_poll")]
        internal extern static void _wrap_love_dll_event_poll(out bool out_hasEvent, out int out_event_type, out bool out_down_or_up, out bool out_bool, out int out_idx, out int out_enum1_type, out int out_enum2_type, out IntPtr out_str, out Int4 out_int4, out Float4 out_float4, out float out_float_value, out IntPtr out_joystick);
        internal static void wrap_love_dll_event_poll(out bool out_hasEvent, out int out_event_type, out bool out_down_or_up, out bool out_bool, out int out_idx, out int out_enum1_type, out int out_enum2_type, out IntPtr out_str, out Int4 out_int4, out Float4 out_float4, out float out_float_value, out IntPtr out_joystick)
        {
            _wrap_love_dll_event_poll(out out_hasEvent, out out_event_type, out out_down_or_up, out out_bool, out out_idx, out out_enum1_type, out out_enum2_type, out out_str, out out_int4, out out_float4, out out_float_value, out out_joystick);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_event_wait")]
        internal extern static void _wrap_love_dll_event_wait(out bool out_hasEvent, out int out_event_type, out bool out_down_or_up, out bool out_bool, out int out_idx, out int out_enum1_type, out int out_enum2_type, out IntPtr out_str, out Int4 out_int4, out Float4 out_float4, out float out_float_value, out IntPtr out_joystick);
        internal static void wrap_love_dll_event_wait(out bool out_hasEvent, out int out_event_type, out bool out_down_or_up, out bool out_bool, out int out_idx, out int out_enum1_type, out int out_enum2_type, out IntPtr out_str, out Int4 out_int4, out Float4 out_float4, out float out_float_value, out IntPtr out_joystick)
        {
            _wrap_love_dll_event_wait(out out_hasEvent, out out_event_type, out out_down_or_up, out out_bool, out out_idx, out out_enum1_type, out out_enum2_type, out out_str, out out_int4, out out_float4, out out_float_value, out out_joystick);
        }

        #endregion

        #region filesystem
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_open_love_filesystem")]
        internal extern static bool _wrap_love_dll_filesystem_open_love_filesystem();
        internal static bool wrap_love_dll_filesystem_open_love_filesystem()
        {
            return CheckCAPIException(_wrap_love_dll_filesystem_open_love_filesystem());
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_init")]
        internal extern static bool _wrap_love_dll_filesystem_init(byte[] arg0);
        internal static bool wrap_love_dll_filesystem_init(byte[] arg0)
        {
            return CheckCAPIException(_wrap_love_dll_filesystem_init(arg0));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_setFused")]
        internal extern static void _wrap_love_dll_filesystem_setFused(bool flag);
        internal static void wrap_love_dll_filesystem_setFused(bool flag)
        {
            _wrap_love_dll_filesystem_setFused(flag);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_isFused")]
        internal extern static void _wrap_love_dll_filesystem_isFused(out bool out_result);
        internal static void wrap_love_dll_filesystem_isFused(out bool out_result)
        {
            _wrap_love_dll_filesystem_isFused(out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_setAndroidSaveExternal")]
        internal extern static void _wrap_love_dll_filesystem_setAndroidSaveExternal(bool useExternal);
        internal static void wrap_love_dll_filesystem_setAndroidSaveExternal(bool useExternal)
        {
            _wrap_love_dll_filesystem_setAndroidSaveExternal(useExternal);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_setIdentity")]
        internal extern static bool _wrap_love_dll_filesystem_setIdentity(byte[] arg, bool append);
        internal static bool wrap_love_dll_filesystem_setIdentity(byte[] arg, bool append)
        {
            return CheckCAPIException(_wrap_love_dll_filesystem_setIdentity(arg, append));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_getIdentity")]
        internal extern static void _wrap_love_dll_filesystem_getIdentity(out IntPtr out_str);
        internal static void wrap_love_dll_filesystem_getIdentity(out IntPtr out_str)
        {
            _wrap_love_dll_filesystem_getIdentity(out out_str);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_setSource")]
        internal extern static bool _wrap_love_dll_filesystem_setSource(byte[] arg);
        internal static bool wrap_love_dll_filesystem_setSource(byte[] arg)
        {
            return CheckCAPIException(_wrap_love_dll_filesystem_setSource(arg));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_getSource")]
        internal extern static void _wrap_love_dll_filesystem_getSource(out IntPtr out_str);
        internal static void wrap_love_dll_filesystem_getSource(out IntPtr out_str)
        {
            _wrap_love_dll_filesystem_getSource(out out_str);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_mount")]
        internal extern static void _wrap_love_dll_filesystem_mount(byte[] archive, byte[] mountpoint, bool appendToPath, out bool out_result);
        internal static void wrap_love_dll_filesystem_mount(byte[] archive, byte[] mountpoint, bool appendToPath, out bool out_result)
        {
            _wrap_love_dll_filesystem_mount(archive, mountpoint, appendToPath, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_unmount")]
        internal extern static void _wrap_love_dll_filesystem_unmount(byte[] archive, out bool out_result);
        internal static void wrap_love_dll_filesystem_unmount(byte[] archive, out bool out_result)
        {
            _wrap_love_dll_filesystem_unmount(archive, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_getWorkingDirectory")]
        internal extern static void _wrap_love_dll_filesystem_getWorkingDirectory(out IntPtr out_str);
        internal static void wrap_love_dll_filesystem_getWorkingDirectory(out IntPtr out_str)
        {
            _wrap_love_dll_filesystem_getWorkingDirectory(out out_str);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_getUserDirectory")]
        internal extern static void _wrap_love_dll_filesystem_getUserDirectory(out IntPtr out_str);
        internal static void wrap_love_dll_filesystem_getUserDirectory(out IntPtr out_str)
        {
            _wrap_love_dll_filesystem_getUserDirectory(out out_str);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_getAppdataDirectory")]
        internal extern static void _wrap_love_dll_filesystem_getAppdataDirectory(out IntPtr out_str);
        internal static void wrap_love_dll_filesystem_getAppdataDirectory(out IntPtr out_str)
        {
            _wrap_love_dll_filesystem_getAppdataDirectory(out out_str);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_getSaveDirectory")]
        internal extern static void _wrap_love_dll_filesystem_getSaveDirectory(out IntPtr out_str);
        internal static void wrap_love_dll_filesystem_getSaveDirectory(out IntPtr out_str)
        {
            _wrap_love_dll_filesystem_getSaveDirectory(out out_str);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_getSourceBaseDirectory")]
        internal extern static void _wrap_love_dll_filesystem_getSourceBaseDirectory(out IntPtr out_str);
        internal static void wrap_love_dll_filesystem_getSourceBaseDirectory(out IntPtr out_str)
        {
            _wrap_love_dll_filesystem_getSourceBaseDirectory(out out_str);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_getRealDirectory")]
        internal extern static bool _wrap_love_dll_filesystem_getRealDirectory(byte[] filename, out IntPtr out_str);
        internal static bool wrap_love_dll_filesystem_getRealDirectory(byte[] filename, out IntPtr out_str)
        {
            return CheckCAPIException(_wrap_love_dll_filesystem_getRealDirectory(filename, out out_str));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_getExecutablePath")]
        internal extern static void _wrap_love_dll_filesystem_getExecutablePath(out IntPtr out_str);
        internal static void wrap_love_dll_filesystem_getExecutablePath(out IntPtr out_str)
        {
            _wrap_love_dll_filesystem_getExecutablePath(out out_str);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_getInfo")]
        internal extern static void _wrap_love_dll_filesystem_getInfo(byte[] arg, out int out_filetype, out long out_size, out long out_modtime, out bool out_result);
        internal static void wrap_love_dll_filesystem_getInfo(byte[] arg, out int out_filetype, out long out_size, out long out_modtime, out bool out_result)
        {
            _wrap_love_dll_filesystem_getInfo(arg, out out_filetype, out out_size, out out_modtime,  out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_createDirectory")]
        internal extern static void _wrap_love_dll_filesystem_createDirectory(byte[] arg, out bool out_result);
        internal static void wrap_love_dll_filesystem_createDirectory(byte[] arg, out bool out_result)
        {
            _wrap_love_dll_filesystem_createDirectory(arg, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_remove")]
        internal extern static void _wrap_love_dll_filesystem_remove(byte[] arg, out bool out_result);
        internal static void wrap_love_dll_filesystem_remove(byte[] arg, out bool out_result)
        {
            _wrap_love_dll_filesystem_remove(arg, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_read")]
        internal extern static bool _wrap_love_dll_filesystem_read(byte[] filename, long len, out IntPtr out_data, out uint out_data_length);
        internal static bool wrap_love_dll_filesystem_read(byte[] filename, long len, out IntPtr out_data, out uint out_data_length)
        {
            return CheckCAPIException(_wrap_love_dll_filesystem_read(filename, len, out out_data, out out_data_length));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_write")]
        internal extern static bool _wrap_love_dll_filesystem_write(byte[] filename, byte[] input, size_t len);
        internal static bool wrap_love_dll_filesystem_write(byte[] filename, byte[] input, size_t len)
        {
            return CheckCAPIException(_wrap_love_dll_filesystem_write(filename, input, len));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_append")]
        internal extern static bool _wrap_love_dll_filesystem_append(byte[] filename, byte[] input, size_t len);
        internal static bool wrap_love_dll_filesystem_append(byte[] filename, byte[] input, size_t len)
        {
            return CheckCAPIException(_wrap_love_dll_filesystem_append(filename, input, len));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_getDirectoryItems")]
        internal extern static void _wrap_love_dll_filesystem_getDirectoryItems(byte[] dir, out IntPtr out_wss);
        internal static void wrap_love_dll_filesystem_getDirectoryItems(byte[] dir, out IntPtr out_wss)
        {
            _wrap_love_dll_filesystem_getDirectoryItems(dir, out out_wss);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_getLastModified")]
        internal extern static bool _wrap_love_dll_filesystem_getLastModified(byte[] filename, out long out_time);
        internal static bool wrap_love_dll_filesystem_getLastModified(byte[] filename, out long out_time)
        {
            return CheckCAPIException(_wrap_love_dll_filesystem_getLastModified(filename, out out_time));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_areSymlinksEnabled")]
        internal extern static void _wrap_love_dll_filesystem_areSymlinksEnabled(out bool out_result);
        internal static void wrap_love_dll_filesystem_areSymlinksEnabled(out bool out_result)
        {
            _wrap_love_dll_filesystem_areSymlinksEnabled(out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_getRequirePath")]
        internal extern static void _wrap_love_dll_filesystem_getRequirePath(out IntPtr out_str);
        internal static void wrap_love_dll_filesystem_getRequirePath(out IntPtr out_str)
        {
            _wrap_love_dll_filesystem_getRequirePath(out out_str);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_filesystem_setRequirePath")]
        internal extern static void _wrap_love_dll_filesystem_setRequirePath(byte[] paths);
        internal static void wrap_love_dll_filesystem_setRequirePath(byte[] paths)
        {
            _wrap_love_dll_filesystem_setRequirePath(paths);
        }


        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void wrap_love_dll_filesystem_ext_allowMountingForPath(IntPtr pathStr);
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static bool wrap_love_dll_filesystem_ext_isRealDirectory(IntPtr pathStr, out bool out_result);

        #endregion

        #region sound
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_sound_luaopen_love_sound")]
        internal extern static bool _wrap_love_dll_sound_luaopen_love_sound();
        internal static bool wrap_love_dll_sound_luaopen_love_sound()
        {
            return CheckCAPIException(_wrap_love_dll_sound_luaopen_love_sound());
        }



        #endregion
        #region  audio

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_audio_open_love_audio")]
        internal extern static bool _wrap_love_dll_audio_open_love_audio();
        internal static bool wrap_love_dll_audio_open_love_audio()
        {
            return CheckCAPIException(_wrap_love_dll_audio_open_love_audio());
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_audio_getActiveSourceCount")]
        internal extern static void _wrap_love_dll_audio_getActiveSourceCount(out int out_reslut);
        internal static void wrap_love_dll_audio_getActiveSourceCount(out int out_reslut)
        {
            _wrap_love_dll_audio_getActiveSourceCount(out out_reslut);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_audio_stop")]
        internal extern static void _wrap_love_dll_audio_stop();
        internal static void wrap_love_dll_audio_stop()
        {
            _wrap_love_dll_audio_stop();
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_audio_pause")]
        internal extern static void _wrap_love_dll_audio_pause();
        internal static void wrap_love_dll_audio_pause()
        {
            _wrap_love_dll_audio_pause();
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_audio_play")]
        internal extern static void _wrap_love_dll_audio_play(IntPtr[] sourceArray, int source_array_length);
        internal static void wrap_love_dll_audio_play(IntPtr[] sourceArray, int source_array_length)
        {
            _wrap_love_dll_audio_play(sourceArray, source_array_length);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_audio_setVolume")]
        internal extern static void _wrap_love_dll_audio_setVolume(float v);
        internal static void wrap_love_dll_audio_setVolume(float v)
        {
            _wrap_love_dll_audio_setVolume(v);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_audio_getVolume")]
        internal extern static void _wrap_love_dll_audio_getVolume(out float out_volume);
        internal static void wrap_love_dll_audio_getVolume(out float out_volume)
        {
            _wrap_love_dll_audio_getVolume(out out_volume);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_audio_setPosition")]
        internal extern static void _wrap_love_dll_audio_setPosition(float x, float y, float z);
        internal static void wrap_love_dll_audio_setPosition(float x, float y, float z)
        {
            _wrap_love_dll_audio_setPosition(x, y, z);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_audio_getPosition")]
        internal extern static void _wrap_love_dll_audio_getPosition(out float out_x, out float out_y, out float out_z);
        internal static void wrap_love_dll_audio_getPosition(out float out_x, out float out_y, out float out_z)
        {
            _wrap_love_dll_audio_getPosition(out out_x, out out_y, out out_z);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_audio_setOrientation")]
        internal extern static void _wrap_love_dll_audio_setOrientation(float x, float y, float z, float dx, float dy, float dz);
        internal static void wrap_love_dll_audio_setOrientation(float x, float y, float z, float dx, float dy, float dz)
        {
            _wrap_love_dll_audio_setOrientation(x, y, z, dx, dy, dz);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_audio_getOrientation")]
        internal extern static void _wrap_love_dll_audio_getOrientation(out float out_x, out float out_y, out float out_z, out float out_dx, out float out_dy, out float out_dz);
        internal static void wrap_love_dll_audio_getOrientation(out float out_x, out float out_y, out float out_z, out float out_dx, out float out_dy, out float out_dz)
        {
            _wrap_love_dll_audio_getOrientation(out out_x, out out_y, out out_z, out out_dx, out out_dy, out out_dz);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_audio_setVelocity")]
        internal extern static void _wrap_love_dll_audio_setVelocity(float x, float y, float z);
        internal static void wrap_love_dll_audio_setVelocity(float x, float y, float z)
        {
            _wrap_love_dll_audio_setVelocity(x, y, z);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_audio_getVelocity")]
        internal extern static void _wrap_love_dll_audio_getVelocity(out float out_x, out float out_y, out float out_z);
        internal static void wrap_love_dll_audio_getVelocity(out float out_x, out float out_y, out float out_z)
        {
            _wrap_love_dll_audio_getVelocity(out out_x, out out_y, out out_z);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_audio_setDopplerScale")]
        internal extern static void _wrap_love_dll_audio_setDopplerScale(float scale);
        internal static void wrap_love_dll_audio_setDopplerScale(float scale)
        {
            _wrap_love_dll_audio_setDopplerScale(scale);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_audio_getDopplerScale")]
        internal extern static void _wrap_love_dll_audio_getDopplerScale(out float out_scale);
        internal static void wrap_love_dll_audio_getDopplerScale(out float out_scale)
        {
            _wrap_love_dll_audio_getDopplerScale(out out_scale);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_audio_setDistanceModel")]
        internal extern static void _wrap_love_dll_audio_setDistanceModel(int distancemodel_type);
        internal static void wrap_love_dll_audio_setDistanceModel(int distancemodel_type)
        {
            _wrap_love_dll_audio_setDistanceModel(distancemodel_type);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_audio_getDistanceModel")]
        internal extern static void _wrap_love_dll_audio_getDistanceModel(out int out_distancemodel_type);
        internal static void wrap_love_dll_audio_getDistanceModel(out int out_distancemodel_type)
        {
            _wrap_love_dll_audio_getDistanceModel(out out_distancemodel_type);
        }



        #endregion
        #region  image

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_image_open_love_image")]
        internal extern static bool _wrap_love_dll_image_open_love_image();
        internal static bool wrap_love_dll_image_open_love_image()
        {
            return CheckCAPIException(_wrap_love_dll_image_open_love_image());
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_image_isCompressed")]
        internal extern static void _wrap_love_dll_image_isCompressed(IntPtr fileData, out bool out_result);
        internal static void wrap_love_dll_image_isCompressed(IntPtr fileData, out bool out_result)
        {
            _wrap_love_dll_image_isCompressed(fileData, out out_result);
        }



        #endregion
        #region  font

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_font_open_love_font")]
        internal extern static bool _wrap_love_dll_font_open_love_font();
        internal static bool wrap_love_dll_font_open_love_font()
        {
            return CheckCAPIException(_wrap_love_dll_font_open_love_font());
        }



        #endregion
        #region  video

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_video_open_love_video")]
        internal extern static bool _wrap_love_dll_video_open_love_video();
        internal static bool wrap_love_dll_video_open_love_video()
        {
            return CheckCAPIException(_wrap_love_dll_video_open_love_video());
        }



        #endregion
        #region  math

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_open_love_math")]
        internal extern static void _wrap_love_dll_open_love_math();
        internal static void wrap_love_dll_open_love_math()
        {
            _wrap_love_dll_open_love_math();
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_math_triangulate")]
        internal extern static bool _wrap_love_dll_math_triangulate(Float2[] pointsList, int pointsList_lenght, out IntPtr out_triArray, out int out_triCount);
        internal static bool wrap_love_dll_math_triangulate(Float2[] pointsList, int pointsList_lenght, out IntPtr out_triArray, out int out_triCount)
        {
            return CheckCAPIException(_wrap_love_dll_math_triangulate(pointsList, pointsList_lenght, out out_triArray, out out_triCount));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_math_isConvex")]
        internal extern static void _wrap_love_dll_math_isConvex(Float2[] pointsList, int pointsList_lenght, out bool out_result);
        internal static void wrap_love_dll_math_isConvex(Float2[] pointsList, int pointsList_lenght, out bool out_result)
        {
            _wrap_love_dll_math_isConvex(pointsList, pointsList_lenght, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_math_gammaToLinear")]
        internal extern static void _wrap_love_dll_math_gammaToLinear(float gama, out float out_liner);
        internal static void wrap_love_dll_math_gammaToLinear(float gama, out float out_liner)
        {
            _wrap_love_dll_math_gammaToLinear(gama, out out_liner);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_math_linearToGamma")]
        internal extern static void _wrap_love_dll_math_linearToGamma(float liner, out float out_gama);
        internal static void wrap_love_dll_math_linearToGamma(float liner, out float out_gama)
        {
            _wrap_love_dll_math_linearToGamma(liner, out out_gama);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_math_noise_1")]
        internal extern static void _wrap_love_dll_math_noise_1(float x, out float out_result);
        internal static void wrap_love_dll_math_noise_1(float x, out float out_result)
        {
            _wrap_love_dll_math_noise_1(x, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_math_noise_2")]
        internal extern static void _wrap_love_dll_math_noise_2(float x, float y, out float out_result);
        internal static void wrap_love_dll_math_noise_2(float x, float y, out float out_result)
        {
            _wrap_love_dll_math_noise_2(x, y, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_math_noise_3")]
        internal extern static void _wrap_love_dll_math_noise_3(float x, float y, float z, out float out_result);
        internal static void wrap_love_dll_math_noise_3(float x, float y, float z, out float out_result)
        {
            _wrap_love_dll_math_noise_3(x, y, z, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_math_noise_4")]
        internal extern static void _wrap_love_dll_math_noise_4(float x, float y, float z, float w, out float out_result);
        internal static void wrap_love_dll_math_noise_4(float x, float y, float z, float w, out float out_result)
        {
            _wrap_love_dll_math_noise_4(x, y, z, w, out out_result);
        }



        #endregion
        #region  graphics Object Creation

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_open_love_graphics")]
        internal extern static bool _wrap_love_dll_graphics_open_love_graphics();
        internal static bool wrap_love_dll_graphics_open_love_graphics()
        {
            return CheckCAPIException(_wrap_love_dll_graphics_open_love_graphics());
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_newImage_data")]
        internal extern static bool _wrap_love_dll_graphics_newImage_data(IntPtr[] imageDataList, bool[] compressedTypeList, int imageDataListLength, bool flagMipmaps, bool flagLinear, out IntPtr out_image);
        internal static bool wrap_love_dll_graphics_newImage_data(IntPtr[] imageDataList, bool[] compressedTypeList, int imageDataListLength, bool flagMipmaps, bool flagLinear, out IntPtr out_image)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_newImage_data(imageDataList, compressedTypeList, imageDataListLength, flagMipmaps, flagLinear, out out_image));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_newQuad")]
        internal extern static void _wrap_love_dll_graphics_newQuad(double x, double y, double w, double h, double sw, double sh, out IntPtr out_quad);
        internal static void wrap_love_dll_graphics_newQuad(double x, double y, double w, double h, double sw, double sh, out IntPtr out_quad)
        {
            _wrap_love_dll_graphics_newQuad(x, y, w, h, sw, sh, out out_quad);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_newFont")]
        internal extern static bool _wrap_love_dll_graphics_newFont(IntPtr rasterizer, out IntPtr out_font);
        internal static bool wrap_love_dll_graphics_newFont(IntPtr rasterizer, out IntPtr out_font)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_newFont(rasterizer, out out_font));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_newSpriteBatch")]
        internal extern static bool _wrap_love_dll_graphics_newSpriteBatch(IntPtr texture, int maxSprites, int usage_type, out IntPtr out_spriteBatch);
        internal static bool wrap_love_dll_graphics_newSpriteBatch(IntPtr texture, int maxSprites, int usage_type, out IntPtr out_spriteBatch)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_newSpriteBatch(texture, maxSprites, usage_type, out out_spriteBatch));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_newParticleSystem")]
        internal extern static bool _wrap_love_dll_graphics_newParticleSystem(IntPtr texture, int buffer, out IntPtr out_particleSystem);
        internal static bool wrap_love_dll_graphics_newParticleSystem(IntPtr texture, int buffer, out IntPtr out_particleSystem)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_newParticleSystem(texture, buffer, out out_particleSystem));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_newCanvas")]
        internal extern static bool _wrap_love_dll_graphics_newCanvas(int width, int height, int texture_type, int format_type, bool readable, int msaa, float dpiscale, int mipmaps, out IntPtr out_canvas);
        internal static bool wrap_love_dll_graphics_newCanvas(int width, int height, int texture_type, int format_type, bool readable, int msaa, float dpiscale, int mipmaps, out IntPtr out_canvas)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_newCanvas(width, height, texture_type, format_type, readable, msaa, dpiscale, mipmaps, out out_canvas));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_newShader")]
        internal extern static bool _wrap_love_dll_graphics_newShader(byte[] vertexCodeStr, byte[] pixelCodeStr, out IntPtr out_shader);
        internal static bool wrap_love_dll_graphics_newShader(byte[] vertexCodeStr, byte[] pixelCodeStr, out IntPtr out_shader)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_newShader(vertexCodeStr, pixelCodeStr, out out_shader));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_newMesh_specifiedVertices")]
        internal extern static bool _wrap_love_dll_graphics_newMesh_specifiedVertices(Float2[] pos, Float2[] uv, Float4[] color, int vertexCount, int drawMode_type, int usage_type, out IntPtr out_mesh);
        internal static bool wrap_love_dll_graphics_newMesh_specifiedVertices(Float2[] pos, Float2[] uv, Float4[] color, int vertexCount, int drawMode_type, int usage_type, out IntPtr out_mesh)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_newMesh_specifiedVertices(pos, uv, color, vertexCount, drawMode_type, usage_type, out out_mesh));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_newMesh_count")]
        internal extern static bool _wrap_love_dll_graphics_newMesh_count(int count, int drawMode_type, int usage_type, out IntPtr out_mesh);
        internal static bool wrap_love_dll_graphics_newMesh_count(int count, int drawMode_type, int usage_type, out IntPtr out_mesh)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_newMesh_count(count, drawMode_type, usage_type, out out_mesh));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_newText")]
        internal extern static bool _wrap_love_dll_graphics_newText(IntPtr font, BytePtr[] coloredStringText, Float4[] coloredStringColor, int coloredStringLength, out IntPtr out_text);
        internal static bool wrap_love_dll_graphics_newText(IntPtr font, BytePtr[] coloredStringText, Float4[] coloredStringColor, int coloredStringLength, out IntPtr out_text)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_newText(font, coloredStringText, coloredStringColor, coloredStringLength, out out_text));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_newVideo")]
        internal extern static bool _wrap_love_dll_graphics_newVideo(IntPtr videoStream, float dpiScale, out IntPtr out_video);
        internal static bool wrap_love_dll_graphics_newVideo(IntPtr videoStream, float dpiScale, out IntPtr out_video)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_newVideo(videoStream, dpiScale, out out_video));
        }



        #endregion
        #region  graphics State

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_reset")]
        internal extern static void _wrap_love_dll_graphics_reset();
        internal static void wrap_love_dll_graphics_reset()
        {
            _wrap_love_dll_graphics_reset();
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_isActive")]
        internal extern static void _wrap_love_dll_graphics_isActive(out bool out_result);
        internal static void wrap_love_dll_graphics_isActive(out bool out_result)
        {
            _wrap_love_dll_graphics_isActive(out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_isGammaCorrect")]
        internal extern static void _wrap_love_dll_graphics_isGammaCorrect(out bool out_result);
        internal static void wrap_love_dll_graphics_isGammaCorrect(out bool out_result)
        {
            _wrap_love_dll_graphics_isGammaCorrect(out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_setScissor")]
        internal extern static void _wrap_love_dll_graphics_setScissor();
        internal static void wrap_love_dll_graphics_setScissor()
        {
            _wrap_love_dll_graphics_setScissor();
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_setScissor_xywh")]
        internal extern static bool _wrap_love_dll_graphics_setScissor_xywh(int x, int y, int w, int h);
        internal static bool wrap_love_dll_graphics_setScissor_xywh(int x, int y, int w, int h)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_setScissor_xywh(x, y, w, h));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_intersectScissor")]
        internal extern static bool _wrap_love_dll_graphics_intersectScissor(int x, int y, int w, int h);
        internal static bool wrap_love_dll_graphics_intersectScissor(int x, int y, int w, int h)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_intersectScissor(x, y, w, h));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_getScissor")]
        internal extern static void _wrap_love_dll_graphics_getScissor(out int out_x, out int out_y, out int out_w, out int out_h);
        internal static void wrap_love_dll_graphics_getScissor(out int out_x, out int out_y, out int out_w, out int out_h)
        {
            _wrap_love_dll_graphics_getScissor(out out_x, out out_y, out out_w, out out_h);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_setStencilTest")]
        internal extern static void _wrap_love_dll_graphics_setStencilTest(int compare_type, int compareValue);
        internal static void wrap_love_dll_graphics_setStencilTest(int compare_type, int compareValue)
        {
            _wrap_love_dll_graphics_setStencilTest(compare_type, compareValue);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_getStencilTest")]
        internal extern static void _wrap_love_dll_graphics_getStencilTest(out int out_compare_type, out int out_compareValue);
        internal static void wrap_love_dll_graphics_getStencilTest(out int out_compare_type, out int out_compareValue)
        {
            _wrap_love_dll_graphics_getStencilTest(out out_compare_type, out out_compareValue);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_setColor")]
        internal extern static void _wrap_love_dll_graphics_setColor(float r, float g, float b, float a);
        internal static void wrap_love_dll_graphics_setColor(float r, float g, float b, float a)
        {
            _wrap_love_dll_graphics_setColor(r, g, b, a);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_getColor")]
        internal extern static void _wrap_love_dll_graphics_getColor(out float out_r, out float out_g, out float out_b, out float out_a);
        internal static void wrap_love_dll_graphics_getColor(out float out_r, out float out_g, out float out_b, out float out_a)
        {
            _wrap_love_dll_graphics_getColor(out out_r, out out_g, out out_b, out out_a);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_setBackgroundColor")]
        internal extern static void _wrap_love_dll_graphics_setBackgroundColor(float r, float g, float b, float a);
        internal static void wrap_love_dll_graphics_setBackgroundColor(float r, float g, float b, float a)
        {
            _wrap_love_dll_graphics_setBackgroundColor(r, g, b, a);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_getBackgroundColor")]
        internal extern static void _wrap_love_dll_graphics_getBackgroundColor(out float out_r, out float out_g, out float out_b, out float out_a);
        internal static void wrap_love_dll_graphics_getBackgroundColor(out float out_r, out float out_g, out float out_b, out float out_a)
        {
            _wrap_love_dll_graphics_getBackgroundColor(out out_r, out out_g, out out_b, out out_a);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_setFont")]
        internal extern static void _wrap_love_dll_graphics_setFont(IntPtr font);
        internal static void wrap_love_dll_graphics_setFont(IntPtr font)
        {
            _wrap_love_dll_graphics_setFont(font);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_getFont")]
        internal extern static bool _wrap_love_dll_graphics_getFont(out IntPtr out_font);
        internal static bool wrap_love_dll_graphics_getFont(out IntPtr out_font)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_getFont(out out_font));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_setColorMask")]
        internal extern static void _wrap_love_dll_graphics_setColorMask(bool r, bool g, bool b, bool a);
        internal static void wrap_love_dll_graphics_setColorMask(bool r, bool g, bool b, bool a)
        {
            _wrap_love_dll_graphics_setColorMask(r, g, b, a);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_getColorMask")]
        internal extern static void _wrap_love_dll_graphics_getColorMask(out bool out_r, out bool out_g, out bool out_b, out bool out_a);
        internal static void wrap_love_dll_graphics_getColorMask(out bool out_r, out bool out_g, out bool out_b, out bool out_a)
        {
            _wrap_love_dll_graphics_getColorMask(out out_r, out out_g, out out_b, out out_a);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_setBlendMode")]
        internal extern static bool _wrap_love_dll_graphics_setBlendMode(int blendMode_type, int blendAlphaMode_type);
        internal static bool wrap_love_dll_graphics_setBlendMode(int blendMode_type, int blendAlphaMode_type)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_setBlendMode(blendMode_type, blendAlphaMode_type));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_getBlendMode")]
        internal extern static void _wrap_love_dll_graphics_getBlendMode(out int out_blendMode_type, out int out_blendAlphaMode_type);
        internal static void wrap_love_dll_graphics_getBlendMode(out int out_blendMode_type, out int out_blendAlphaMode_type)
        {
            _wrap_love_dll_graphics_getBlendMode(out out_blendMode_type, out out_blendAlphaMode_type);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_setDefaultFilter")]
        internal extern static void _wrap_love_dll_graphics_setDefaultFilter(int filterModeMagMin_type, int filterModeMag_type, int anisotropy);
        internal static void wrap_love_dll_graphics_setDefaultFilter(int filterModeMagMin_type, int filterModeMag_type, int anisotropy)
        {
            _wrap_love_dll_graphics_setDefaultFilter(filterModeMagMin_type, filterModeMag_type, anisotropy);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_getDefaultFilter")]
        internal extern static void _wrap_love_dll_graphics_getDefaultFilter(out int out_filterModeMagMin_type, out int out_filterModeMag_type, out int out_anisotropy);
        internal static void wrap_love_dll_graphics_getDefaultFilter(out int out_filterModeMagMin_type, out int out_filterModeMag_type, out int out_anisotropy)
        {
            _wrap_love_dll_graphics_getDefaultFilter(out out_filterModeMagMin_type, out out_filterModeMag_type, out out_anisotropy);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_setLineWidth")]
        internal extern static void _wrap_love_dll_graphics_setLineWidth(float width);
        internal static void wrap_love_dll_graphics_setLineWidth(float width)
        {
            _wrap_love_dll_graphics_setLineWidth(width);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_setLineStyle")]
        internal extern static void _wrap_love_dll_graphics_setLineStyle(int style_type);
        internal static void wrap_love_dll_graphics_setLineStyle(int style_type)
        {
            _wrap_love_dll_graphics_setLineStyle(style_type);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_setLineJoin")]
        internal extern static void _wrap_love_dll_graphics_setLineJoin(int join_type);
        internal static void wrap_love_dll_graphics_setLineJoin(int join_type)
        {
            _wrap_love_dll_graphics_setLineJoin(join_type);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_getLineWidth")]
        internal extern static void _wrap_love_dll_graphics_getLineWidth(out float out_result);
        internal static void wrap_love_dll_graphics_getLineWidth(out float out_result)
        {
            _wrap_love_dll_graphics_getLineWidth(out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_getLineStyle")]
        internal extern static void _wrap_love_dll_graphics_getLineStyle(out int out_lineStyle_type);
        internal static void wrap_love_dll_graphics_getLineStyle(out int out_lineStyle_type)
        {
            _wrap_love_dll_graphics_getLineStyle(out out_lineStyle_type);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_getLineJoin")]
        internal extern static void _wrap_love_dll_graphics_getLineJoin(out int out_lineJoinStyle_type);
        internal static void wrap_love_dll_graphics_getLineJoin(out int out_lineJoinStyle_type)
        {
            _wrap_love_dll_graphics_getLineJoin(out out_lineJoinStyle_type);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_setPointSize")]
        internal extern static void _wrap_love_dll_graphics_setPointSize(float size);
        internal static void wrap_love_dll_graphics_setPointSize(float size)
        {
            _wrap_love_dll_graphics_setPointSize(size);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_getPointSize")]
        internal extern static void _wrap_love_dll_graphics_getPointSize(out float out_size);
        internal static void wrap_love_dll_graphics_getPointSize(out float out_size)
        {
            _wrap_love_dll_graphics_getPointSize(out out_size);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_setWireframe")]
        internal extern static void _wrap_love_dll_graphics_setWireframe(bool enable);
        internal static void wrap_love_dll_graphics_setWireframe(bool enable)
        {
            _wrap_love_dll_graphics_setWireframe(enable);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_isWireframe")]
        internal extern static void _wrap_love_dll_graphics_isWireframe(out bool out_isWireFrame);
        internal static void wrap_love_dll_graphics_isWireframe(out bool out_isWireFrame)
        {
            _wrap_love_dll_graphics_isWireframe(out out_isWireFrame);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_setCanvas")]
        internal extern static bool _wrap_love_dll_graphics_setCanvas(IntPtr[] canvaList, int canvaListLength);
        internal static bool wrap_love_dll_graphics_setCanvas(IntPtr[] canvaList, int canvaListLength)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_setCanvas(canvaList, canvaListLength));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_getCanvas")]
        internal extern static void _wrap_love_dll_graphics_getCanvas(out IntPtr out_canvas, out int out_canvas_lenght);
        internal static void wrap_love_dll_graphics_getCanvas(out IntPtr out_canvas, out int out_canvas_lenght)
        {
            _wrap_love_dll_graphics_getCanvas(out out_canvas, out out_canvas_lenght);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_setShader")]
        internal extern static void _wrap_love_dll_graphics_setShader(IntPtr shader);
        internal static void wrap_love_dll_graphics_setShader(IntPtr shader)
        {
            _wrap_love_dll_graphics_setShader(shader);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_getShader")]
        internal extern static void _wrap_love_dll_graphics_getShader(out IntPtr out_shader);
        internal static void wrap_love_dll_graphics_getShader(out IntPtr out_shader)
        {
            _wrap_love_dll_graphics_getShader(out out_shader);
        }


        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_setDefaultShaderCode")]
        internal extern static void _wrap_love_dll_graphics_setDefaultShaderCode(IntPtr[] strPtr);
        internal static void wrap_love_dll_graphics_setDefaultShaderCode(IntPtr[] strPtr)
        {
            _wrap_love_dll_graphics_setDefaultShaderCode(strPtr);
        }



        #endregion
        #region  graphics Coordinate System

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_push")]
        internal extern static void _wrap_love_dll_graphics_push(int stack_type);
        internal static void wrap_love_dll_graphics_push(int stack_type)
        {
            _wrap_love_dll_graphics_push(stack_type);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_pop")]
        internal extern static void _wrap_love_dll_graphics_pop();
        internal static void wrap_love_dll_graphics_pop()
        {
            _wrap_love_dll_graphics_pop();
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_rotate")]
        internal extern static void _wrap_love_dll_graphics_rotate(float angle);
        internal static void wrap_love_dll_graphics_rotate(float angle)
        {
            _wrap_love_dll_graphics_rotate(angle);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_scale")]
        internal extern static void _wrap_love_dll_graphics_scale(float sx, float sy);
        internal static void wrap_love_dll_graphics_scale(float sx, float sy)
        {
            _wrap_love_dll_graphics_scale(sx, sy);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_translate")]
        internal extern static void _wrap_love_dll_graphics_translate(float x, float y);
        internal static void wrap_love_dll_graphics_translate(float x, float y)
        {
            _wrap_love_dll_graphics_translate(x, y);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_shear")]
        internal extern static void _wrap_love_dll_graphics_shear(float kx, float ky);
        internal static void wrap_love_dll_graphics_shear(float kx, float ky)
        {
            _wrap_love_dll_graphics_shear(kx, ky);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_origin")]
        internal extern static void _wrap_love_dll_graphics_origin();
        internal static void wrap_love_dll_graphics_origin()
        {
            _wrap_love_dll_graphics_origin();
        }



        #endregion
        #region  graphics drawing

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_ext_stencil_drawToStencilBuffer")]
        internal extern static void _wrap_love_dll_graphics_ext_stencil_drawToStencilBuffer(int action_type, int stencilValue);
        internal static void wrap_love_dll_graphics_ext_stencil_drawToStencilBuffer(int action_type, int stencilValue)
        {
            _wrap_love_dll_graphics_ext_stencil_drawToStencilBuffer(action_type, stencilValue);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_ext_stencil_stopDrawToStencilBuffer")]
        internal extern static void _wrap_love_dll_graphics_ext_stencil_stopDrawToStencilBuffer();
        internal static void wrap_love_dll_graphics_ext_stencil_stopDrawToStencilBuffer()
        {
            _wrap_love_dll_graphics_ext_stencil_stopDrawToStencilBuffer();
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_clear_rgba")]
        internal extern static bool _wrap_love_dll_graphics_clear_rgba(float r, float g, float b, float a, float stencil, bool enable_stencil, float depth, bool enable_depth);
        internal static bool wrap_love_dll_graphics_clear_rgba(float r, float g, float b, float a, float stencil, bool enable_stencil, float depth, bool enable_depth)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_clear_rgba(r, g, b, a, stencil, enable_stencil, depth, enable_depth));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_clear_rgbalist")]
        internal extern static bool _wrap_love_dll_graphics_clear_rgbalist(Float4[] colorList, bool[] enableList, int listLength, float stencil, bool enable_stencil, float depth, bool enable_depth);
        internal static bool wrap_love_dll_graphics_clear_rgbalist(Float4[] colorList, bool[] enableList, int listLength, float stencil, bool enable_stencil, float depth, bool enable_depth)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_clear_rgbalist(colorList, enableList, listLength, stencil, enable_stencil, depth, enable_depth));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_discard")]
        internal extern static void _wrap_love_dll_graphics_discard(bool[] discardColors, int discardColorsLength, bool discardStencil);
        internal static void wrap_love_dll_graphics_discard(bool[] discardColors, int discardColorsLength, bool discardStencil)
        {
            _wrap_love_dll_graphics_discard(discardColors, discardColorsLength, discardStencil);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_present")]
        internal extern static void _wrap_love_dll_graphics_present();
        internal static void wrap_love_dll_graphics_present()
        {
            _wrap_love_dll_graphics_present();
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_draw_drawable")]
        internal extern static bool _wrap_love_dll_graphics_draw_drawable(IntPtr drawable, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky);
        internal static bool wrap_love_dll_graphics_draw_drawable(IntPtr drawable, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_draw_drawable(drawable, x, y, a, sx, sy, ox, oy, kx, ky));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_draw_texture_quad")]
        internal extern static bool _wrap_love_dll_graphics_draw_texture_quad(IntPtr quad, IntPtr texture, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky);
        internal static bool wrap_love_dll_graphics_draw_texture_quad(IntPtr quad, IntPtr texture, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_draw_texture_quad(quad, texture, x, y, a, sx, sy, ox, oy, kx, ky));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_print")]
        internal extern static bool _wrap_love_dll_graphics_print(IntPtr[] coloredStringListStr, Float4[] coloredStringListColor, int coloredStringListLength, float x, float y, float angle, float sx, float sy, float ox, float oy, float kx, float ky);
        internal static bool wrap_love_dll_graphics_print(IntPtr[] coloredStringListStr, Float4[] coloredStringListColor, int coloredStringListLength, float x, float y, float angle, float sx, float sy, float ox, float oy, float kx, float ky)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_print(coloredStringListStr, coloredStringListColor, coloredStringListLength, x, y, angle, sx, sy, ox, oy, kx, ky));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_printf")]
        internal extern static bool _wrap_love_dll_graphics_printf(BytePtr[] coloredStringListStr, Float4[] coloredStringListColor, int coloredStringListLength, float x, float y, float wrap, int align_type, float angle, float sx, float sy, float ox, float oy, float kx, float ky);
        internal static bool wrap_love_dll_graphics_printf(BytePtr[] coloredStringListStr, Float4[] coloredStringListColor, int coloredStringListLength, float x, float y, float wrap, int align_type, float angle, float sx, float sy, float ox, float oy, float kx, float ky)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_printf(coloredStringListStr, coloredStringListColor, coloredStringListLength, x, y, wrap, align_type, angle, sx, sy, ox, oy, kx, ky));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_rectangle")]
        internal extern static bool _wrap_love_dll_graphics_rectangle(int mode_type, float x, float y, float w, float h);
        internal static bool wrap_love_dll_graphics_rectangle(int mode_type, float x, float y, float w, float h)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_rectangle(mode_type, x, y, w, h));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_rectangle_with_rounded_corners")]
        internal extern static bool _wrap_love_dll_graphics_rectangle_with_rounded_corners(int mode_type, float x, float y, float w, float h, float rx, float ry, int points);
        internal static bool wrap_love_dll_graphics_rectangle_with_rounded_corners(int mode_type, float x, float y, float w, float h, float rx, float ry, int points)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_rectangle_with_rounded_corners(mode_type, x, y, w, h, rx, ry, points));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_circle")]
        internal extern static bool _wrap_love_dll_graphics_circle(int mode_type, float x, float y, float radius, int points);
        internal static bool wrap_love_dll_graphics_circle(int mode_type, float x, float y, float radius, int points)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_circle(mode_type, x, y, radius, points));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_ellipse")]
        internal extern static bool _wrap_love_dll_graphics_ellipse(int mode_type, float x, float y, float a, float b, int points);
        internal static bool wrap_love_dll_graphics_ellipse(int mode_type, float x, float y, float a, float b, int points)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_ellipse(mode_type, x, y, a, b, points));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_arc")]
        internal extern static bool _wrap_love_dll_graphics_arc(int mode_type, int arcmode_type, float x, float y, float radius, float angle1, float angle2);
        internal static bool wrap_love_dll_graphics_arc(int mode_type, int arcmode_type, float x, float y, float radius, float angle1, float angle2)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_arc(mode_type, arcmode_type, x, y, radius, angle1, angle2));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_arc_points")]
        internal extern static bool _wrap_love_dll_graphics_arc_points(int mode_type, int arcmode_type, float x, float y, float radius, float angle1, float angle2, int points);
        internal static bool wrap_love_dll_graphics_arc_points(int mode_type, int arcmode_type, float x, float y, float radius, float angle1, float angle2, int points)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_arc_points(mode_type, arcmode_type, x, y, radius, angle1, angle2, points));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_points")]
        internal extern static bool _wrap_love_dll_graphics_points(Float2[] coords, int coordsLength);
        internal static bool wrap_love_dll_graphics_points(Float2[] coords, int coordsLength)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_points(coords, coordsLength));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_points_colors")]
        internal extern static bool _wrap_love_dll_graphics_points_colors(Float2[] coords, Int4[] colors, int coordsLength);
        internal static bool wrap_love_dll_graphics_points_colors(Float2[] coords, Int4[] colors, int coordsLength)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_points_colors(coords, colors, coordsLength));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_line")]
        internal extern static bool _wrap_love_dll_graphics_line(Float2[] coords, int coordsLength);
        internal static bool wrap_love_dll_graphics_line(Float2[] coords, int coordsLength)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_line(coords, coordsLength));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_polygon")]
        internal extern static bool _wrap_love_dll_graphics_polygon(int mode_type, Float2[] coords, int coordsLength);
        internal static bool wrap_love_dll_graphics_polygon(int mode_type, Float2[] coords, int coordsLength)
        {
            return CheckCAPIException(_wrap_love_dll_graphics_polygon(mode_type, coords, coordsLength));
        }



        #endregion
        #region  graphics Window

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_isCreated")]
        internal extern static void _wrap_love_dll_graphics_isCreated(out bool out_reslut);
        internal static void wrap_love_dll_graphics_isCreated(out bool out_reslut)
        {
            _wrap_love_dll_graphics_isCreated(out out_reslut);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_getDPIScale")]
        internal extern static void _wrap_love_dll_graphics_getDPIScale(out double out_dpiscale);
        internal static void wrap_love_dll_graphics_getDPIScale(out double out_dpiscale)
        {
            _wrap_love_dll_graphics_getDPIScale(out out_dpiscale);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_getWidth")]
        internal extern static void _wrap_love_dll_graphics_getWidth(out int out_width);
        internal static void wrap_love_dll_graphics_getWidth(out int out_width)
        {
            _wrap_love_dll_graphics_getWidth(out out_width);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_getHeight")]
        internal extern static void _wrap_love_dll_graphics_getHeight(out int out_height);
        internal static void wrap_love_dll_graphics_getHeight(out int out_height)
        {
            _wrap_love_dll_graphics_getHeight(out out_height);
        }



        #endregion
        #region  graphics System Information

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_getSupported")]
        internal extern static void _wrap_love_dll_graphics_getSupported(int feature_type, out bool out_result);
        internal static void wrap_love_dll_graphics_getSupported(int feature_type, out bool out_result)
        {
            _wrap_love_dll_graphics_getSupported(feature_type, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_getCanvasFormats")]
        internal extern static void _wrap_love_dll_graphics_getCanvasFormats(int format_type, out bool out_result);
        internal static void wrap_love_dll_graphics_getCanvasFormats(int format_type, out bool out_result)
        {
            _wrap_love_dll_graphics_getCanvasFormats(format_type, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_getImageFormats")]
        internal extern static void _wrap_love_dll_graphics_getImageFormats(int format_type, out bool out_result);
        internal static void wrap_love_dll_graphics_getImageFormats(int format_type, out bool out_result)
        {
            _wrap_love_dll_graphics_getImageFormats(format_type, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_getRendererInfo")]
        internal extern static void _wrap_love_dll_graphics_getRendererInfo(out IntPtr out_wss);
        internal static void wrap_love_dll_graphics_getRendererInfo(out IntPtr out_wss)
        {
            _wrap_love_dll_graphics_getRendererInfo(out out_wss);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_getSystemLimits")]
        internal extern static void _wrap_love_dll_graphics_getSystemLimits(int limit_type, out double out_result);
        internal static void wrap_love_dll_graphics_getSystemLimits(int limit_type, out double out_result)
        {
            _wrap_love_dll_graphics_getSystemLimits(limit_type, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_graphics_getStats")]
        internal extern static void _wrap_love_dll_graphics_getStats(out int out_drawCalls, out int out_canvasSwitches, out int out_shaderSwitches, out int out_canvases, out int out_images, out int out_fonts, out int out_textureMemory);
        internal static void wrap_love_dll_graphics_getStats(out int out_drawCalls, out int out_canvasSwitches, out int out_shaderSwitches, out int out_canvases, out int out_images, out int out_fonts, out int out_textureMemory)
        {
            _wrap_love_dll_graphics_getStats(out out_drawCalls, out out_canvasSwitches, out out_shaderSwitches, out out_canvases, out out_images, out out_fonts, out out_textureMemory);
        }



        #endregion
        #region  type - Source

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_clone")]
        internal extern static bool _wrap_love_dll_type_Source_clone(IntPtr t, out IntPtr out_clone);
        internal static bool wrap_love_dll_type_Source_clone(IntPtr t, out IntPtr out_clone)
        {
            return CheckCAPIException(_wrap_love_dll_type_Source_clone(t, out out_clone));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_play")]
        internal extern static void _wrap_love_dll_type_Source_play(IntPtr t, out bool out_success);
        internal static void wrap_love_dll_type_Source_play(IntPtr t, out bool out_success)
        {
            _wrap_love_dll_type_Source_play(t, out out_success);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_stop")]
        internal extern static void _wrap_love_dll_type_Source_stop(IntPtr t);
        internal static void wrap_love_dll_type_Source_stop(IntPtr t)
        {
            _wrap_love_dll_type_Source_stop(t);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_pause")]
        internal extern static void _wrap_love_dll_type_Source_pause(IntPtr t);
        internal static void wrap_love_dll_type_Source_pause(IntPtr t)
        {
            _wrap_love_dll_type_Source_pause(t);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_setPitch")]
        internal extern static bool _wrap_love_dll_type_Source_setPitch(IntPtr t, float pitch);
        internal static bool wrap_love_dll_type_Source_setPitch(IntPtr t, float pitch)
        {
            return CheckCAPIException(_wrap_love_dll_type_Source_setPitch(t, pitch));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_getPitch")]
        internal extern static void _wrap_love_dll_type_Source_getPitch(IntPtr t, out float out_pitch);
        internal static void wrap_love_dll_type_Source_getPitch(IntPtr t, out float out_pitch)
        {
            _wrap_love_dll_type_Source_getPitch(t, out out_pitch);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_setVolume")]
        internal extern static void _wrap_love_dll_type_Source_setVolume(IntPtr t, float p);
        internal static void wrap_love_dll_type_Source_setVolume(IntPtr t, float p)
        {
            _wrap_love_dll_type_Source_setVolume(t, p);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_getVolume")]
        internal extern static void _wrap_love_dll_type_Source_getVolume(IntPtr t, out float out_volume);
        internal static void wrap_love_dll_type_Source_getVolume(IntPtr t, out float out_volume)
        {
            _wrap_love_dll_type_Source_getVolume(t, out out_volume);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_seek")]
        internal extern static void _wrap_love_dll_type_Source_seek(IntPtr t, float offset, int unit_type);
        internal static void wrap_love_dll_type_Source_seek(IntPtr t, float offset, int unit_type)
        {
            _wrap_love_dll_type_Source_seek(t, offset, unit_type);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_tell")]
        internal extern static void _wrap_love_dll_type_Source_tell(IntPtr t, int unit_type, out float out_position);
        internal static void wrap_love_dll_type_Source_tell(IntPtr t, int unit_type, out float out_position)
        {
            _wrap_love_dll_type_Source_tell(t, unit_type, out out_position);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_getDuration")]
        internal extern static void _wrap_love_dll_type_Source_getDuration(IntPtr t, int unit_type, out float out_duration);
        internal static void wrap_love_dll_type_Source_getDuration(IntPtr t, int unit_type, out float out_duration)
        {
            _wrap_love_dll_type_Source_getDuration(t, unit_type, out out_duration);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_setPosition")]
        internal extern static bool _wrap_love_dll_type_Source_setPosition(IntPtr t, float x, float y, float z);
        internal static bool wrap_love_dll_type_Source_setPosition(IntPtr t, float x, float y, float z)
        {
            return CheckCAPIException(_wrap_love_dll_type_Source_setPosition(t, x, y, z));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_getPosition")]
        internal extern static bool _wrap_love_dll_type_Source_getPosition(IntPtr t, out float out_x, out float out_y, out float out_z);
        internal static bool wrap_love_dll_type_Source_getPosition(IntPtr t, out float out_x, out float out_y, out float out_z)
        {
            return CheckCAPIException(_wrap_love_dll_type_Source_getPosition(t, out out_x, out out_y, out out_z));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_setVelocity")]
        internal extern static bool _wrap_love_dll_type_Source_setVelocity(IntPtr t, float x, float y, float z);
        internal static bool wrap_love_dll_type_Source_setVelocity(IntPtr t, float x, float y, float z)
        {
            return CheckCAPIException(_wrap_love_dll_type_Source_setVelocity(t, x, y, z));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_getVelocity")]
        internal extern static bool _wrap_love_dll_type_Source_getVelocity(IntPtr t, out float out_x, out float out_y, out float out_z);
        internal static bool wrap_love_dll_type_Source_getVelocity(IntPtr t, out float out_x, out float out_y, out float out_z)
        {
            return CheckCAPIException(_wrap_love_dll_type_Source_getVelocity(t, out out_x, out out_y, out out_z));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_setDirection")]
        internal extern static bool _wrap_love_dll_type_Source_setDirection(IntPtr t, float x, float y, float z);
        internal static bool wrap_love_dll_type_Source_setDirection(IntPtr t, float x, float y, float z)
        {
            return CheckCAPIException(_wrap_love_dll_type_Source_setDirection(t, x, y, z));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_getDirection")]
        internal extern static bool _wrap_love_dll_type_Source_getDirection(IntPtr t, out float out_x, out float out_y, out float out_z);
        internal static bool wrap_love_dll_type_Source_getDirection(IntPtr t, out float out_x, out float out_y, out float out_z)
        {
            return CheckCAPIException(_wrap_love_dll_type_Source_getDirection(t, out out_x, out out_y, out out_z));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_setCone")]
        internal extern static bool _wrap_love_dll_type_Source_setCone(IntPtr t, float innerAngle, float outerAngle, float outerVolume);
        internal static bool wrap_love_dll_type_Source_setCone(IntPtr t, float innerAngle, float outerAngle, float outerVolume)
        {
            return CheckCAPIException(_wrap_love_dll_type_Source_setCone(t, innerAngle, outerAngle, outerVolume));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_getCone")]
        internal extern static bool _wrap_love_dll_type_Source_getCone(IntPtr t, out float out_innerAngle, out float out_outerAngle, out float out_outerVolume);
        internal static bool wrap_love_dll_type_Source_getCone(IntPtr t, out float out_innerAngle, out float out_outerAngle, out float out_outerVolume)
        {
            return CheckCAPIException(_wrap_love_dll_type_Source_getCone(t, out out_innerAngle, out out_outerAngle, out out_outerVolume));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_setRelative")]
        internal extern static bool _wrap_love_dll_type_Source_setRelative(IntPtr t, bool relative);
        internal static bool wrap_love_dll_type_Source_setRelative(IntPtr t, bool relative)
        {
            return CheckCAPIException(_wrap_love_dll_type_Source_setRelative(t, relative));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_isRelative")]
        internal extern static bool _wrap_love_dll_type_Source_isRelative(IntPtr t, out bool out_relative);
        internal static bool wrap_love_dll_type_Source_isRelative(IntPtr t, out bool out_relative)
        {
            return CheckCAPIException(_wrap_love_dll_type_Source_isRelative(t, out out_relative));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_setLooping")]
        internal extern static void _wrap_love_dll_type_Source_setLooping(IntPtr t, bool looping);
        internal static void wrap_love_dll_type_Source_setLooping(IntPtr t, bool looping)
        {
            _wrap_love_dll_type_Source_setLooping(t, looping);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_isLooping")]
        internal extern static void _wrap_love_dll_type_Source_isLooping(IntPtr t, out bool out_result);
        internal static void wrap_love_dll_type_Source_isLooping(IntPtr t, out bool out_result)
        {
            _wrap_love_dll_type_Source_isLooping(t, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_isPlaying")]
        internal extern static void _wrap_love_dll_type_Source_isPlaying(IntPtr t, out bool out_result);
        internal static void wrap_love_dll_type_Source_isPlaying(IntPtr t, out bool out_result)
        {
            _wrap_love_dll_type_Source_isPlaying(t, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_setVolumeLimits")]
        internal extern static bool _wrap_love_dll_type_Source_setVolumeLimits(IntPtr t, float vmin, float vmax);
        internal static bool wrap_love_dll_type_Source_setVolumeLimits(IntPtr t, float vmin, float vmax)
        {
            return CheckCAPIException(_wrap_love_dll_type_Source_setVolumeLimits(t, vmin, vmax));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_getVolumeLimits")]
        internal extern static void _wrap_love_dll_type_Source_getVolumeLimits(IntPtr t, out float out_vmin, out float out_vmax);
        internal static void wrap_love_dll_type_Source_getVolumeLimits(IntPtr t, out float out_vmin, out float out_vmax)
        {
            _wrap_love_dll_type_Source_getVolumeLimits(t, out out_vmin, out out_vmax);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_setAttenuationDistances")]
        internal extern static bool _wrap_love_dll_type_Source_setAttenuationDistances(IntPtr t, float dref, float dmax);
        internal static bool wrap_love_dll_type_Source_setAttenuationDistances(IntPtr t, float dref, float dmax)
        {
            return CheckCAPIException(_wrap_love_dll_type_Source_setAttenuationDistances(t, dref, dmax));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_getAttenuationDistances")]
        internal extern static bool _wrap_love_dll_type_Source_getAttenuationDistances(IntPtr t, out float out_dref, out float out_dmax);
        internal static bool wrap_love_dll_type_Source_getAttenuationDistances(IntPtr t, out float out_dref, out float out_dmax)
        {
            return CheckCAPIException(_wrap_love_dll_type_Source_getAttenuationDistances(t, out out_dref, out out_dmax));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_setRolloff")]
        internal extern static bool _wrap_love_dll_type_Source_setRolloff(IntPtr t, float rolloff);
        internal static bool wrap_love_dll_type_Source_setRolloff(IntPtr t, float rolloff)
        {
            return CheckCAPIException(_wrap_love_dll_type_Source_setRolloff(t, rolloff));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_getRolloff")]
        internal extern static bool _wrap_love_dll_type_Source_getRolloff(IntPtr t, out float out_rolloff);
        internal static bool wrap_love_dll_type_Source_getRolloff(IntPtr t, out float out_rolloff)
        {
            return CheckCAPIException(_wrap_love_dll_type_Source_getRolloff(t, out out_rolloff));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_getChannelCount")]
        internal extern static void _wrap_love_dll_type_Source_getChannelCount(IntPtr t, out int out_chanels);
        internal static void wrap_love_dll_type_Source_getChannelCount(IntPtr t, out int out_chanels)
        {
            _wrap_love_dll_type_Source_getChannelCount(t, out out_chanels);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Source_getType")]
        internal extern static void _wrap_love_dll_type_Source_getType(IntPtr t, out int out_type);
        internal static void wrap_love_dll_type_Source_getType(IntPtr t, out int out_type)
        {
            _wrap_love_dll_type_Source_getType(t, out out_type);
        }



        #endregion
        #region  type - File

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_File_getSize")]
        internal extern static bool _wrap_love_dll_type_File_getSize(IntPtr file, out double out_size);
        internal static bool wrap_love_dll_type_File_getSize(IntPtr file, out double out_size)
        {
            return CheckCAPIException(_wrap_love_dll_type_File_getSize(file, out out_size));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_File_open")]
        internal extern static bool _wrap_love_dll_type_File_open(IntPtr file, int mode_type);
        internal static bool wrap_love_dll_type_File_open(IntPtr file, int mode_type)
        {
            return CheckCAPIException(_wrap_love_dll_type_File_open(file, mode_type));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_File_close")]
        internal extern static void _wrap_love_dll_type_File_close(IntPtr file, out bool out_result);
        internal static void wrap_love_dll_type_File_close(IntPtr file, out bool out_result)
        {
            _wrap_love_dll_type_File_close(file, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_File_isOpen")]
        internal extern static void _wrap_love_dll_type_File_isOpen(IntPtr file, out bool out_result);
        internal static void wrap_love_dll_type_File_isOpen(IntPtr file, out bool out_result)
        {
            _wrap_love_dll_type_File_isOpen(file, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_File_read")]
        internal extern static bool _wrap_love_dll_type_File_read(IntPtr file, Int64 size, out IntPtr out_data, out Int64 out_readedSize);
        internal static bool wrap_love_dll_type_File_read(IntPtr file, Int64 size, out IntPtr out_data, out Int64 out_readedSize)
        {
            return CheckCAPIException(_wrap_love_dll_type_File_read(file, size, out out_data, out out_readedSize));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_File_write_String")]
        internal extern static bool _wrap_love_dll_type_File_write_String(IntPtr file, byte[] data, Int64 datasize);
        internal static bool wrap_love_dll_type_File_write_String(IntPtr file, byte[] data, Int64 datasize)
        {
            return CheckCAPIException(_wrap_love_dll_type_File_write_String(file, data, datasize));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_File_write_Data_datasize")]
        internal extern static bool _wrap_love_dll_type_File_write_Data_datasize(IntPtr file, IntPtr data, Int64 datasize);
        internal static bool wrap_love_dll_type_File_write_Data_datasize(IntPtr file, IntPtr data, Int64 datasize)
        {
            return CheckCAPIException(_wrap_love_dll_type_File_write_Data_datasize(file, data, datasize));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_File_flush")]
        internal extern static bool _wrap_love_dll_type_File_flush(IntPtr file);
        internal static bool wrap_love_dll_type_File_flush(IntPtr file)
        {
            return CheckCAPIException(_wrap_love_dll_type_File_flush(file));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_File_isEOF")]
        internal extern static void _wrap_love_dll_type_File_isEOF(IntPtr file, out bool out_result);
        internal static void wrap_love_dll_type_File_isEOF(IntPtr file, out bool out_result)
        {
            _wrap_love_dll_type_File_isEOF(file, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_File_tell")]
        internal extern static bool _wrap_love_dll_type_File_tell(IntPtr file, out Int64 out_pos);
        internal static bool wrap_love_dll_type_File_tell(IntPtr file, out Int64 out_pos)
        {
            return CheckCAPIException(_wrap_love_dll_type_File_tell(file, out out_pos));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_File_seek")]
        internal extern static bool _wrap_love_dll_type_File_seek(IntPtr file, Int64 pos);
        internal static bool wrap_love_dll_type_File_seek(IntPtr file, Int64 pos)
        {
            return CheckCAPIException(_wrap_love_dll_type_File_seek(file, pos));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_File_setBuffer")]
        internal extern static bool _wrap_love_dll_type_File_setBuffer(IntPtr file, int bufmode_type, Int64 size);
        internal static bool wrap_love_dll_type_File_setBuffer(IntPtr file, int bufmode_type, Int64 size)
        {
            return CheckCAPIException(_wrap_love_dll_type_File_setBuffer(file, bufmode_type, size));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_File_getBuffer")]
        internal extern static void _wrap_love_dll_type_File_getBuffer(IntPtr file, out int out_bufmode_type, out Int64 out_size);
        internal static void wrap_love_dll_type_File_getBuffer(IntPtr file, out int out_bufmode_type, out Int64 out_size)
        {
            _wrap_love_dll_type_File_getBuffer(file, out out_bufmode_type, out out_size);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_File_getMode")]
        internal extern static void _wrap_love_dll_type_File_getMode(IntPtr file, out int out_mode_type);
        internal static void wrap_love_dll_type_File_getMode(IntPtr file, out int out_mode_type)
        {
            _wrap_love_dll_type_File_getMode(file, out out_mode_type);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_File_getFilename")]
        internal extern static void _wrap_love_dll_type_File_getFilename(IntPtr file, out IntPtr out_filename);
        internal static void wrap_love_dll_type_File_getFilename(IntPtr file, out IntPtr out_filename)
        {
            _wrap_love_dll_type_File_getFilename(file, out out_filename);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_File_getExtension")]
        internal extern static void _wrap_love_dll_type_File_getExtension(IntPtr file, out IntPtr out_extension);
        internal static void wrap_love_dll_type_File_getExtension(IntPtr file, out IntPtr out_extension)
        {
            _wrap_love_dll_type_File_getExtension(file, out out_extension);
        }



        #endregion
        #region  type - FileData

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_FileData_getFilename")]
        internal extern static void _wrap_love_dll_type_FileData_getFilename(IntPtr t, out IntPtr out_filename);
        internal static void wrap_love_dll_type_FileData_getFilename(IntPtr t, out IntPtr out_filename)
        {
            _wrap_love_dll_type_FileData_getFilename(t, out out_filename);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_FileData_getExtension")]
        internal extern static void _wrap_love_dll_type_FileData_getExtension(IntPtr t, out IntPtr out_extension);
        internal static void wrap_love_dll_type_FileData_getExtension(IntPtr t, out IntPtr out_extension)
        {
            _wrap_love_dll_type_FileData_getExtension(t, out out_extension);
        }



        #endregion
        #region  type - GlyphData

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_GlyphData_getWidth")]
        internal extern static void _wrap_love_dll_type_GlyphData_getWidth(IntPtr t, out int out_width);
        internal static void wrap_love_dll_type_GlyphData_getWidth(IntPtr t, out int out_width)
        {
            _wrap_love_dll_type_GlyphData_getWidth(t, out out_width);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_GlyphData_getHeight")]
        internal extern static void _wrap_love_dll_type_GlyphData_getHeight(IntPtr t, out int out_height);
        internal static void wrap_love_dll_type_GlyphData_getHeight(IntPtr t, out int out_height)
        {
            _wrap_love_dll_type_GlyphData_getHeight(t, out out_height);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_GlyphData_getGlyph")]
        internal extern static void _wrap_love_dll_type_GlyphData_getGlyph(IntPtr t, out uint out_glyph);
        internal static void wrap_love_dll_type_GlyphData_getGlyph(IntPtr t, out uint out_glyph)
        {
            _wrap_love_dll_type_GlyphData_getGlyph(t, out out_glyph);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_GlyphData_getGlyphString")]
        internal extern static bool _wrap_love_dll_type_GlyphData_getGlyphString(IntPtr t, out IntPtr out_str);
        internal static bool wrap_love_dll_type_GlyphData_getGlyphString(IntPtr t, out IntPtr out_str)
        {
            return CheckCAPIException(_wrap_love_dll_type_GlyphData_getGlyphString(t, out out_str));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_GlyphData_getAdvance")]
        internal extern static void _wrap_love_dll_type_GlyphData_getAdvance(IntPtr t, out int out_advance);
        internal static void wrap_love_dll_type_GlyphData_getAdvance(IntPtr t, out int out_advance)
        {
            _wrap_love_dll_type_GlyphData_getAdvance(t, out out_advance);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_GlyphData_getBearing")]
        internal extern static void _wrap_love_dll_type_GlyphData_getBearing(IntPtr t, out int out_bearingX, out int out_bearingY);
        internal static void wrap_love_dll_type_GlyphData_getBearing(IntPtr t, out int out_bearingX, out int out_bearingY)
        {
            _wrap_love_dll_type_GlyphData_getBearing(t, out out_bearingX, out out_bearingY);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_GlyphData_getBoundingBox")]
        internal extern static void _wrap_love_dll_type_GlyphData_getBoundingBox(IntPtr t, out int out_minX, out int out_minY, out int out_width, out int out_height);
        internal static void wrap_love_dll_type_GlyphData_getBoundingBox(IntPtr t, out int out_minX, out int out_minY, out int out_width, out int out_height)
        {
            _wrap_love_dll_type_GlyphData_getBoundingBox(t, out out_minX, out out_minY, out out_width, out out_height);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_GlyphData_getFormat")]
        internal extern static void _wrap_love_dll_type_GlyphData_getFormat(IntPtr t, out int out_format_type);
        internal static void wrap_love_dll_type_GlyphData_getFormat(IntPtr t, out int out_format_type)
        {
            _wrap_love_dll_type_GlyphData_getFormat(t, out out_format_type);
        }



        #endregion
        #region  type - Rasterizer

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Rasterizer_getHeight")]
        internal extern static void _wrap_love_dll_type_Rasterizer_getHeight(IntPtr t, out int out_heigth);
        internal static void wrap_love_dll_type_Rasterizer_getHeight(IntPtr t, out int out_heigth)
        {
            _wrap_love_dll_type_Rasterizer_getHeight(t, out out_heigth);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Rasterizer_getAdvance")]
        internal extern static void _wrap_love_dll_type_Rasterizer_getAdvance(IntPtr t, out int out_advance);
        internal static void wrap_love_dll_type_Rasterizer_getAdvance(IntPtr t, out int out_advance)
        {
            _wrap_love_dll_type_Rasterizer_getAdvance(t, out out_advance);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Rasterizer_getAscent")]
        internal extern static void _wrap_love_dll_type_Rasterizer_getAscent(IntPtr t, out int out_ascent);
        internal static void wrap_love_dll_type_Rasterizer_getAscent(IntPtr t, out int out_ascent)
        {
            _wrap_love_dll_type_Rasterizer_getAscent(t, out out_ascent);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Rasterizer_getDescent")]
        internal extern static void _wrap_love_dll_type_Rasterizer_getDescent(IntPtr t, out int out_descent);
        internal static void wrap_love_dll_type_Rasterizer_getDescent(IntPtr t, out int out_descent)
        {
            _wrap_love_dll_type_Rasterizer_getDescent(t, out out_descent);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Rasterizer_getLineHeight")]
        internal extern static void _wrap_love_dll_type_Rasterizer_getLineHeight(IntPtr t, out int out_lineHeight);
        internal static void wrap_love_dll_type_Rasterizer_getLineHeight(IntPtr t, out int out_lineHeight)
        {
            _wrap_love_dll_type_Rasterizer_getLineHeight(t, out out_lineHeight);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Rasterizer_getGlyphData_uint32")]
        internal extern static bool _wrap_love_dll_type_Rasterizer_getGlyphData_uint32(IntPtr t, uint glyph, out IntPtr out_glyphData);
        internal static bool wrap_love_dll_type_Rasterizer_getGlyphData_uint32(IntPtr t, uint glyph, out IntPtr out_glyphData)
        {
            return CheckCAPIException(_wrap_love_dll_type_Rasterizer_getGlyphData_uint32(t, glyph, out out_glyphData));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Rasterizer_getGlyphData_string")]
        internal extern static bool _wrap_love_dll_type_Rasterizer_getGlyphData_string(IntPtr t, byte[] str, out IntPtr out_glyphData);
        internal static bool wrap_love_dll_type_Rasterizer_getGlyphData_string(IntPtr t, byte[] str, out IntPtr out_glyphData)
        {
            return CheckCAPIException(_wrap_love_dll_type_Rasterizer_getGlyphData_string(t, str, out out_glyphData));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Rasterizer_getGlyphCount")]
        internal extern static void _wrap_love_dll_type_Rasterizer_getGlyphCount(IntPtr t, out int out_count);
        internal static void wrap_love_dll_type_Rasterizer_getGlyphCount(IntPtr t, out int out_count)
        {
            _wrap_love_dll_type_Rasterizer_getGlyphCount(t, out out_count);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Rasterizer_hasGlyphs_uint32")]
        internal extern static bool _wrap_love_dll_type_Rasterizer_hasGlyphs_uint32(IntPtr t, uint glyph, out bool out_result);
        internal static bool wrap_love_dll_type_Rasterizer_hasGlyphs_uint32(IntPtr t, uint glyph, out bool out_result)
        {
            return CheckCAPIException(_wrap_love_dll_type_Rasterizer_hasGlyphs_uint32(t, glyph, out out_result));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Rasterizer_hasGlyphs_string")]
        internal extern static bool _wrap_love_dll_type_Rasterizer_hasGlyphs_string(IntPtr t, byte[] str, out bool out_result);
        internal static bool wrap_love_dll_type_Rasterizer_hasGlyphs_string(IntPtr t, byte[] str, out bool out_result)
        {
            return CheckCAPIException(_wrap_love_dll_type_Rasterizer_hasGlyphs_string(t, str, out out_result));
        }



        #endregion
        #region  type - Canvas

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Canvas_getFormat")]
        internal extern static void _wrap_love_dll_type_Canvas_getFormat(IntPtr canvas, out int out_format_type);
        internal static void wrap_love_dll_type_Canvas_getFormat(IntPtr canvas, out int out_format_type)
        {
            _wrap_love_dll_type_Canvas_getFormat(canvas, out out_format_type);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Canvas_getMSAA")]
        internal extern static void _wrap_love_dll_type_Canvas_getMSAA(IntPtr canvas, out int out_msaa);
        internal static void wrap_love_dll_type_Canvas_getMSAA(IntPtr canvas, out int out_msaa)
        {
            _wrap_love_dll_type_Canvas_getMSAA(canvas, out out_msaa);
        }



        #endregion
        #region  type - Font

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Font_getHeight")]
        internal extern static void _wrap_love_dll_type_Font_getHeight(IntPtr t, out int out_height);
        internal static void wrap_love_dll_type_Font_getHeight(IntPtr t, out int out_height)
        {
            _wrap_love_dll_type_Font_getHeight(t, out out_height);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Font_getWidth")]
        internal extern static bool _wrap_love_dll_type_Font_getWidth(IntPtr t, byte[] str, out int out_width);
        internal static bool wrap_love_dll_type_Font_getWidth(IntPtr t, byte[] str, out int out_width)
        {
            return CheckCAPIException(_wrap_love_dll_type_Font_getWidth(t, str, out out_width));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Font_getWrap")]
        internal extern static bool _wrap_love_dll_type_Font_getWrap(IntPtr t, BytePtr[] coloredStringText, Float4[] coloredStringColor, int coloredStringLength, float wrap, out int out_maxWidth, out IntPtr out_pws);
        internal static bool wrap_love_dll_type_Font_getWrap(IntPtr t, BytePtr[] coloredStringText, Float4[] coloredStringColor, int coloredStringLength, float wrap, out int out_maxWidth, out IntPtr out_pws)
        {
            return CheckCAPIException(_wrap_love_dll_type_Font_getWrap(t, coloredStringText, coloredStringColor, coloredStringLength, wrap, out out_maxWidth, out out_pws));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Font_setLineHeight")]
        internal extern static void _wrap_love_dll_type_Font_setLineHeight(IntPtr t, float h);
        internal static void wrap_love_dll_type_Font_setLineHeight(IntPtr t, float h)
        {
            _wrap_love_dll_type_Font_setLineHeight(t, h);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Font_getLineHeight")]
        internal extern static void _wrap_love_dll_type_Font_getLineHeight(IntPtr t, out float out_h);
        internal static void wrap_love_dll_type_Font_getLineHeight(IntPtr t, out float out_h)
        {
            _wrap_love_dll_type_Font_getLineHeight(t, out out_h);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Font_setFilter")]
        internal extern static bool _wrap_love_dll_type_Font_setFilter(IntPtr t, int min_type, int mag_type, float anisotropy);
        internal static bool wrap_love_dll_type_Font_setFilter(IntPtr t, int min_type, int mag_type, float anisotropy)
        {
            return CheckCAPIException(_wrap_love_dll_type_Font_setFilter(t, min_type, mag_type, anisotropy));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Font_getFilter")]
        internal extern static void _wrap_love_dll_type_Font_getFilter(IntPtr t, out int out_min_type, out int out_mag_type, out float out_anisotropy);
        internal static void wrap_love_dll_type_Font_getFilter(IntPtr t, out int out_min_type, out int out_mag_type, out float out_anisotropy)
        {
            _wrap_love_dll_type_Font_getFilter(t, out out_min_type, out out_mag_type, out out_anisotropy);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Font_getAscent")]
        internal extern static void _wrap_love_dll_type_Font_getAscent(IntPtr t, out int out_ascent);
        internal static void wrap_love_dll_type_Font_getAscent(IntPtr t, out int out_ascent)
        {
            _wrap_love_dll_type_Font_getAscent(t, out out_ascent);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Font_getDescent")]
        internal extern static void _wrap_love_dll_type_Font_getDescent(IntPtr t, out int out_descent);
        internal static void wrap_love_dll_type_Font_getDescent(IntPtr t, out int out_descent)
        {
            _wrap_love_dll_type_Font_getDescent(t, out out_descent);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Font_getBaseline")]
        internal extern static void _wrap_love_dll_type_Font_getBaseline(IntPtr t, out float out_baseline);
        internal static void wrap_love_dll_type_Font_getBaseline(IntPtr t, out float out_baseline)
        {
            _wrap_love_dll_type_Font_getBaseline(t, out out_baseline);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Font_hasGlyphs_uint32")]
        internal extern static bool _wrap_love_dll_type_Font_hasGlyphs_uint32(IntPtr t, uint chr, out bool out_result);
        internal static bool wrap_love_dll_type_Font_hasGlyphs_uint32(IntPtr t, uint chr, out bool out_result)
        {
            return CheckCAPIException(_wrap_love_dll_type_Font_hasGlyphs_uint32(t, chr, out out_result));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Font_hasGlyphs_string")]
        internal extern static bool _wrap_love_dll_type_Font_hasGlyphs_string(IntPtr t, byte[] str, out bool out_result);
        internal static bool wrap_love_dll_type_Font_hasGlyphs_string(IntPtr t, byte[] str, out bool out_result)
        {
            return CheckCAPIException(_wrap_love_dll_type_Font_hasGlyphs_string(t, str, out out_result));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Font_setFallbacks")]
        internal extern static bool _wrap_love_dll_type_Font_setFallbacks(IntPtr t, IntPtr[] fallback, int length);
        internal static bool wrap_love_dll_type_Font_setFallbacks(IntPtr t, IntPtr[] fallback, int length)
        {
            return CheckCAPIException(_wrap_love_dll_type_Font_setFallbacks(t, fallback, length));
        }



        #endregion
        #region  type - Image

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Image_setMipmapFilter")]
        internal extern static bool _wrap_love_dll_type_Image_setMipmapFilter(IntPtr image, int mipmap_type, float sharpness);
        internal static bool wrap_love_dll_type_Image_setMipmapFilter(IntPtr image, int mipmap_type, float sharpness)
        {
            return CheckCAPIException(_wrap_love_dll_type_Image_setMipmapFilter(image, mipmap_type, sharpness));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Image_getMipmapFilter")]
        internal extern static void _wrap_love_dll_type_Image_getMipmapFilter(IntPtr image, out int out_mipmap_type, out float out_sharpness);
        internal static void wrap_love_dll_type_Image_getMipmapFilter(IntPtr image, out int out_mipmap_type, out float out_sharpness)
        {
            _wrap_love_dll_type_Image_getMipmapFilter(image, out out_mipmap_type, out out_sharpness);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Image_isCompressed")]
        internal extern static void _wrap_love_dll_type_Image_isCompressed(IntPtr image, out bool out_result);
        internal static void wrap_love_dll_type_Image_isCompressed(IntPtr image, out bool out_result)
        {
            _wrap_love_dll_type_Image_isCompressed(image, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Image_replacePixels")]
        internal extern static bool _wrap_love_dll_type_Image_replacePixels(IntPtr image, IntPtr imageData, int slice, int mipmap, int x, int y, bool reloadmipmaps);
        internal static bool wrap_love_dll_type_Image_replacePixels(IntPtr image, IntPtr imageData, int slice, int mipmap, int x, int y, bool reloadmipmaps)
        {
            return CheckCAPIException(_wrap_love_dll_type_Image_replacePixels(image, imageData, slice, mipmap, x, y, reloadmipmaps));
        }



        #endregion
        #region  type - Mesh

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Mesh_setVertices")]
        internal extern static bool _wrap_love_dll_type_Mesh_setVertices(IntPtr p, int vertoffset, Float2[] pos, Float2[] uv, Float4[] color, int vertexCount);
        internal static bool wrap_love_dll_type_Mesh_setVertices(IntPtr p, int vertoffset, Float2[] pos, Float2[] uv, Float4[] color, int vertexCount)
        {
            return CheckCAPIException(_wrap_love_dll_type_Mesh_setVertices(p, vertoffset, pos, uv, color, vertexCount));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Mesh_setVertex")]
        internal extern static bool _wrap_love_dll_type_Mesh_setVertex(IntPtr p, int index, Float2 pos, Float2 uv, Float4 color);
        internal static bool wrap_love_dll_type_Mesh_setVertex(IntPtr p, int index, Float2 pos, Float2 uv, Float4 color)
        {
            return CheckCAPIException(_wrap_love_dll_type_Mesh_setVertex(p, index, pos, uv, color));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Mesh_getVertex")]
        internal extern static bool _wrap_love_dll_type_Mesh_getVertex(IntPtr p, int index, out Float2 pos, out Float2 uv, out Float4 color);
        internal static bool wrap_love_dll_type_Mesh_getVertex(IntPtr p, int index, out Float2 pos, out Float2 uv, out Float4 color)
        {
            return CheckCAPIException(_wrap_love_dll_type_Mesh_getVertex(p, index, out pos, out uv, out color));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Mesh_getVertexCount")]
        internal extern static void _wrap_love_dll_type_Mesh_getVertexCount(IntPtr p, out int out_result);
        internal static void wrap_love_dll_type_Mesh_getVertexCount(IntPtr p, out int out_result)
        {
            _wrap_love_dll_type_Mesh_getVertexCount(p, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Mesh_flush")]
        internal extern static void _wrap_love_dll_type_Mesh_flush(IntPtr p);
        internal static void wrap_love_dll_type_Mesh_flush(IntPtr p)
        {
            _wrap_love_dll_type_Mesh_flush(p);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Mesh_setVertexMap_nil")]
        internal extern static bool _wrap_love_dll_type_Mesh_setVertexMap_nil(IntPtr p);
        internal static bool wrap_love_dll_type_Mesh_setVertexMap_nil(IntPtr p)
        {
            return CheckCAPIException(_wrap_love_dll_type_Mesh_setVertexMap_nil(p));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Mesh_setVertexMap")]
        internal extern static bool _wrap_love_dll_type_Mesh_setVertexMap(IntPtr p, uint[] vertexmaps, int vertexmaps_length);
        internal static bool wrap_love_dll_type_Mesh_setVertexMap(IntPtr p, uint[] vertexmaps, int vertexmaps_length)
        {
            return CheckCAPIException(_wrap_love_dll_type_Mesh_setVertexMap(p, vertexmaps, vertexmaps_length));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Mesh_getVertexMap")]
        internal extern static bool _wrap_love_dll_type_Mesh_getVertexMap(IntPtr p, out bool out_has_vertex_map, out IntPtr out_vertexmaps, out int out_vertexmaps_length);
        internal static bool wrap_love_dll_type_Mesh_getVertexMap(IntPtr p, out bool out_has_vertex_map, out IntPtr out_vertexmaps, out int out_vertexmaps_length)
        {
            return CheckCAPIException(_wrap_love_dll_type_Mesh_getVertexMap(p, out out_has_vertex_map, out out_vertexmaps, out out_vertexmaps_length));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Mesh_setTexture_nil")]
        internal extern static void _wrap_love_dll_type_Mesh_setTexture_nil(IntPtr p);
        internal static void wrap_love_dll_type_Mesh_setTexture_nil(IntPtr p)
        {
            _wrap_love_dll_type_Mesh_setTexture_nil(p);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Mesh_setTexture_Texture")]
        internal extern static void _wrap_love_dll_type_Mesh_setTexture_Texture(IntPtr p, IntPtr tex);
        internal static void wrap_love_dll_type_Mesh_setTexture_Texture(IntPtr p, IntPtr tex)
        {
            _wrap_love_dll_type_Mesh_setTexture_Texture(p, tex);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Mesh_getTexture")]
        internal extern static bool _wrap_love_dll_type_Mesh_getTexture(IntPtr p, out IntPtr out_result);
        internal static bool wrap_love_dll_type_Mesh_getTexture(IntPtr p, out IntPtr out_result)
        {
            return CheckCAPIException(_wrap_love_dll_type_Mesh_getTexture(p, out out_result));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Mesh_setDrawMode")]
        internal extern static void _wrap_love_dll_type_Mesh_setDrawMode(IntPtr p, int mode_type);
        internal static void wrap_love_dll_type_Mesh_setDrawMode(IntPtr p, int mode_type)
        {
            _wrap_love_dll_type_Mesh_setDrawMode(p, mode_type);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Mesh_getDrawMode")]
        internal extern static void _wrap_love_dll_type_Mesh_getDrawMode(IntPtr p, out int out_mode_type);
        internal static void wrap_love_dll_type_Mesh_getDrawMode(IntPtr p, out int out_mode_type)
        {
            _wrap_love_dll_type_Mesh_getDrawMode(p, out out_mode_type);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Mesh_setDrawRange")]
        internal extern static void _wrap_love_dll_type_Mesh_setDrawRange(IntPtr p);
        internal static void wrap_love_dll_type_Mesh_setDrawRange(IntPtr p)
        {
            _wrap_love_dll_type_Mesh_setDrawRange(p);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Mesh_setDrawRange_minmax")]
        internal extern static bool _wrap_love_dll_type_Mesh_setDrawRange_minmax(IntPtr p, int rangemin, int rangemax);
        internal static bool wrap_love_dll_type_Mesh_setDrawRange_minmax(IntPtr p, int rangemin, int rangemax)
        {
            return CheckCAPIException(_wrap_love_dll_type_Mesh_setDrawRange_minmax(p, rangemin, rangemax));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Mesh_getDrawRange")]
        internal extern static bool _wrap_love_dll_type_Mesh_getDrawRange(IntPtr p, out int out_rangemin, out int out_rangemax);
        internal static bool wrap_love_dll_type_Mesh_getDrawRange(IntPtr p, out int out_rangemin, out int out_rangemax)
        {
            return CheckCAPIException(_wrap_love_dll_type_Mesh_getDrawRange(p, out out_rangemin, out out_rangemax));
        }



        #endregion
        #region  type - ParticleSystem

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_clone")]
        internal extern static bool _wrap_love_dll_type_ParticleSystem_clone(IntPtr p, out IntPtr out_clone);
        internal static bool wrap_love_dll_type_ParticleSystem_clone(IntPtr p, out IntPtr out_clone)
        {
            return CheckCAPIException(_wrap_love_dll_type_ParticleSystem_clone(p, out out_clone));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_setTexture")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_setTexture(IntPtr p, IntPtr tex);
        internal static void wrap_love_dll_type_ParticleSystem_setTexture(IntPtr p, IntPtr tex)
        {
            _wrap_love_dll_type_ParticleSystem_setTexture(p, tex);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_getTexture")]
        internal extern static bool _wrap_love_dll_type_ParticleSystem_getTexture(IntPtr p, out IntPtr out_texture);
        internal static bool wrap_love_dll_type_ParticleSystem_getTexture(IntPtr p, out IntPtr out_texture)
        {
            return CheckCAPIException(_wrap_love_dll_type_ParticleSystem_getTexture(p, out out_texture));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_setBufferSize")]
        internal extern static bool _wrap_love_dll_type_ParticleSystem_setBufferSize(IntPtr p, uint buffersize);
        internal static bool wrap_love_dll_type_ParticleSystem_setBufferSize(IntPtr p, uint buffersize)
        {
            return CheckCAPIException(_wrap_love_dll_type_ParticleSystem_setBufferSize(p, buffersize));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_getBufferSize")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_getBufferSize(IntPtr p, out uint out_buffersize);
        internal static void wrap_love_dll_type_ParticleSystem_getBufferSize(IntPtr p, out uint out_buffersize)
        {
            _wrap_love_dll_type_ParticleSystem_getBufferSize(p, out out_buffersize);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_setInsertMode")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_setInsertMode(IntPtr p, int mode_type);
        internal static void wrap_love_dll_type_ParticleSystem_setInsertMode(IntPtr p, int mode_type)
        {
            _wrap_love_dll_type_ParticleSystem_setInsertMode(p, mode_type);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_getInsertMode")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_getInsertMode(IntPtr p, out int out_mode_type);
        internal static void wrap_love_dll_type_ParticleSystem_getInsertMode(IntPtr p, out int out_mode_type)
        {
            _wrap_love_dll_type_ParticleSystem_getInsertMode(p, out out_mode_type);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_setEmissionRate")]
        internal extern static bool _wrap_love_dll_type_ParticleSystem_setEmissionRate(IntPtr p, float rate);
        internal static bool wrap_love_dll_type_ParticleSystem_setEmissionRate(IntPtr p, float rate)
        {
            return CheckCAPIException(_wrap_love_dll_type_ParticleSystem_setEmissionRate(p, rate));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_getEmissionRate")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_getEmissionRate(IntPtr p, out float out_rate);
        internal static void wrap_love_dll_type_ParticleSystem_getEmissionRate(IntPtr p, out float out_rate)
        {
            _wrap_love_dll_type_ParticleSystem_getEmissionRate(p, out out_rate);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_setEmitterLifetime")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_setEmitterLifetime(IntPtr p, float lifetime);
        internal static void wrap_love_dll_type_ParticleSystem_setEmitterLifetime(IntPtr p, float lifetime)
        {
            _wrap_love_dll_type_ParticleSystem_setEmitterLifetime(p, lifetime);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_getEmitterLifetime")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_getEmitterLifetime(IntPtr p, out float out_lifetime);
        internal static void wrap_love_dll_type_ParticleSystem_getEmitterLifetime(IntPtr p, out float out_lifetime)
        {
            _wrap_love_dll_type_ParticleSystem_getEmitterLifetime(p, out out_lifetime);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_setParticleLifetime")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_setParticleLifetime(IntPtr p, float min, float max);
        internal static void wrap_love_dll_type_ParticleSystem_setParticleLifetime(IntPtr p, float min, float max)
        {
            _wrap_love_dll_type_ParticleSystem_setParticleLifetime(p, min, max);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_getParticleLifetime")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_getParticleLifetime(IntPtr p, out int out_min, out int out_max);
        internal static void wrap_love_dll_type_ParticleSystem_getParticleLifetime(IntPtr p, out int out_min, out int out_max)
        {
            _wrap_love_dll_type_ParticleSystem_getParticleLifetime(p, out out_min, out out_max);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_setPosition")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_setPosition(IntPtr p, float x, float y);
        internal static void wrap_love_dll_type_ParticleSystem_setPosition(IntPtr p, float x, float y)
        {
            _wrap_love_dll_type_ParticleSystem_setPosition(p, x, y);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_getPosition")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_getPosition(IntPtr p, out float out_x, out float out_y);
        internal static void wrap_love_dll_type_ParticleSystem_getPosition(IntPtr p, out float out_x, out float out_y)
        {
            _wrap_love_dll_type_ParticleSystem_getPosition(p, out out_x, out out_y);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_moveTo")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_moveTo(IntPtr p, float x, float y);
        internal static void wrap_love_dll_type_ParticleSystem_moveTo(IntPtr p, float x, float y)
        {
            _wrap_love_dll_type_ParticleSystem_moveTo(p, x, y);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_setEmissionArea")]
        internal extern static bool _wrap_love_dll_type_ParticleSystem_setEmissionArea(IntPtr p, int distribution_type, float x, float y, float angle, bool directionRelativeToCenter);
        internal static bool wrap_love_dll_type_ParticleSystem_setEmissionArea(IntPtr p, int distribution_type, float x, float y, float angle, bool directionRelativeToCenter)
        {
            return CheckCAPIException(_wrap_love_dll_type_ParticleSystem_setEmissionArea(p, distribution_type, x, y, angle, directionRelativeToCenter));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_getAreaSpread")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_getAreaSpread(IntPtr p, out int out_distribution_type, out float out_x, out float out_y);
        internal static void wrap_love_dll_type_ParticleSystem_getAreaSpread(IntPtr p, out int out_distribution_type, out float out_x, out float out_y)
        {
            _wrap_love_dll_type_ParticleSystem_getAreaSpread(p, out out_distribution_type, out out_x, out out_y);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_setDirection")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_setDirection(IntPtr p, float direction);
        internal static void wrap_love_dll_type_ParticleSystem_setDirection(IntPtr p, float direction)
        {
            _wrap_love_dll_type_ParticleSystem_setDirection(p, direction);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_getDirection")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_getDirection(IntPtr p, out float out_direction);
        internal static void wrap_love_dll_type_ParticleSystem_getDirection(IntPtr p, out float out_direction)
        {
            _wrap_love_dll_type_ParticleSystem_getDirection(p, out out_direction);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_setSpread")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_setSpread(IntPtr p, float spread);
        internal static void wrap_love_dll_type_ParticleSystem_setSpread(IntPtr p, float spread)
        {
            _wrap_love_dll_type_ParticleSystem_setSpread(p, spread);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_getSpread")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_getSpread(IntPtr p, out float out_spread);
        internal static void wrap_love_dll_type_ParticleSystem_getSpread(IntPtr p, out float out_spread)
        {
            _wrap_love_dll_type_ParticleSystem_getSpread(p, out out_spread);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_setSpeed")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_setSpeed(IntPtr p, float min, float max);
        internal static void wrap_love_dll_type_ParticleSystem_setSpeed(IntPtr p, float min, float max)
        {
            _wrap_love_dll_type_ParticleSystem_setSpeed(p, min, max);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_getSpeed")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_getSpeed(IntPtr p, out float out_min, out float out_max);
        internal static void wrap_love_dll_type_ParticleSystem_getSpeed(IntPtr p, out float out_min, out float out_max)
        {
            _wrap_love_dll_type_ParticleSystem_getSpeed(p, out out_min, out out_max);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_setLinearAcceleration")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_setLinearAcceleration(IntPtr p, float xmin, float ymin, float xmax, float ymax);
        internal static void wrap_love_dll_type_ParticleSystem_setLinearAcceleration(IntPtr p, float xmin, float ymin, float xmax, float ymax)
        {
            _wrap_love_dll_type_ParticleSystem_setLinearAcceleration(p, xmin, ymin, xmax, ymax);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_getLinearAcceleration")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_getLinearAcceleration(IntPtr p, out float out_xmin, out float out_ymin, out float out_xmax, out float out_ymax);
        internal static void wrap_love_dll_type_ParticleSystem_getLinearAcceleration(IntPtr p, out float out_xmin, out float out_ymin, out float out_xmax, out float out_ymax)
        {
            _wrap_love_dll_type_ParticleSystem_getLinearAcceleration(p, out out_xmin, out out_ymin, out out_xmax, out out_ymax);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_setRadialAcceleration")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_setRadialAcceleration(IntPtr p, float min, float max);
        internal static void wrap_love_dll_type_ParticleSystem_setRadialAcceleration(IntPtr p, float min, float max)
        {
            _wrap_love_dll_type_ParticleSystem_setRadialAcceleration(p, min, max);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_getRadialAcceleration")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_getRadialAcceleration(IntPtr p, out int out_min, out int out_max);
        internal static void wrap_love_dll_type_ParticleSystem_getRadialAcceleration(IntPtr p, out int out_min, out int out_max)
        {
            _wrap_love_dll_type_ParticleSystem_getRadialAcceleration(p, out out_min, out out_max);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_setTangentialAcceleration")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_setTangentialAcceleration(IntPtr p, float min, float max);
        internal static void wrap_love_dll_type_ParticleSystem_setTangentialAcceleration(IntPtr p, float min, float max)
        {
            _wrap_love_dll_type_ParticleSystem_setTangentialAcceleration(p, min, max);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_getTangentialAcceleration")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_getTangentialAcceleration(IntPtr p, out int out_min, out int out_max);
        internal static void wrap_love_dll_type_ParticleSystem_getTangentialAcceleration(IntPtr p, out int out_min, out int out_max)
        {
            _wrap_love_dll_type_ParticleSystem_getTangentialAcceleration(p, out out_min, out out_max);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_setLinearDamping")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_setLinearDamping(IntPtr p, float min, float max);
        internal static void wrap_love_dll_type_ParticleSystem_setLinearDamping(IntPtr p, float min, float max)
        {
            _wrap_love_dll_type_ParticleSystem_setLinearDamping(p, min, max);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_getLinearDamping")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_getLinearDamping(IntPtr p, out int out_min, out int out_max);
        internal static void wrap_love_dll_type_ParticleSystem_getLinearDamping(IntPtr p, out int out_min, out int out_max)
        {
            _wrap_love_dll_type_ParticleSystem_getLinearDamping(p, out out_min, out out_max);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_setSizes")]
        internal extern static bool _wrap_love_dll_type_ParticleSystem_setSizes(IntPtr p, float[] sizearray, int sizearray_length);
        internal static bool wrap_love_dll_type_ParticleSystem_setSizes(IntPtr p, float[] sizearray, int sizearray_length)
        {
            return CheckCAPIException(_wrap_love_dll_type_ParticleSystem_setSizes(p, sizearray, sizearray_length));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_getSizes")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_getSizes(IntPtr p, out IntPtr out_sizearray, out int out_sizearray_length);
        internal static void wrap_love_dll_type_ParticleSystem_getSizes(IntPtr p, out IntPtr out_sizearray, out int out_sizearray_length)
        {
            _wrap_love_dll_type_ParticleSystem_getSizes(p, out out_sizearray, out out_sizearray_length);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_setSizeVariation")]
        internal extern static bool _wrap_love_dll_type_ParticleSystem_setSizeVariation(IntPtr p, float variation);
        internal static bool wrap_love_dll_type_ParticleSystem_setSizeVariation(IntPtr p, float variation)
        {
            return CheckCAPIException(_wrap_love_dll_type_ParticleSystem_setSizeVariation(p, variation));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_getSizeVariation")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_getSizeVariation(IntPtr p, out float out_variation);
        internal static void wrap_love_dll_type_ParticleSystem_getSizeVariation(IntPtr p, out float out_variation)
        {
            _wrap_love_dll_type_ParticleSystem_getSizeVariation(p, out out_variation);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_setRotation")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_setRotation(IntPtr p, float min, float max);
        internal static void wrap_love_dll_type_ParticleSystem_setRotation(IntPtr p, float min, float max)
        {
            _wrap_love_dll_type_ParticleSystem_setRotation(p, min, max);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_getRotation")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_getRotation(IntPtr p, out int out_min, out int out_max);
        internal static void wrap_love_dll_type_ParticleSystem_getRotation(IntPtr p, out int out_min, out int out_max)
        {
            _wrap_love_dll_type_ParticleSystem_getRotation(p, out out_min, out out_max);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_setSpin")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_setSpin(IntPtr p, float start, float end);
        internal static void wrap_love_dll_type_ParticleSystem_setSpin(IntPtr p, float start, float end)
        {
            _wrap_love_dll_type_ParticleSystem_setSpin(p, start, end);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_getSpin")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_getSpin(IntPtr p, out float out_start, out float out_end);
        internal static void wrap_love_dll_type_ParticleSystem_getSpin(IntPtr p, out float out_start, out float out_end)
        {
            _wrap_love_dll_type_ParticleSystem_getSpin(p, out out_start, out out_end);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_setSpinVariation")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_setSpinVariation(IntPtr p, float variation);
        internal static void wrap_love_dll_type_ParticleSystem_setSpinVariation(IntPtr p, float variation)
        {
            _wrap_love_dll_type_ParticleSystem_setSpinVariation(p, variation);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_getSpinVariation")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_getSpinVariation(IntPtr p, out float out_variation);
        internal static void wrap_love_dll_type_ParticleSystem_getSpinVariation(IntPtr p, out float out_variation)
        {
            _wrap_love_dll_type_ParticleSystem_getSpinVariation(p, out out_variation);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_setOffset")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_setOffset(IntPtr p, float x, float y);
        internal static void wrap_love_dll_type_ParticleSystem_setOffset(IntPtr p, float x, float y)
        {
            _wrap_love_dll_type_ParticleSystem_setOffset(p, x, y);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_getOffset")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_getOffset(IntPtr p, out float out_x, out float out_y);
        internal static void wrap_love_dll_type_ParticleSystem_getOffset(IntPtr p, out float out_x, out float out_y)
        {
            _wrap_love_dll_type_ParticleSystem_getOffset(p, out out_x, out out_y);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_setColors")]
        internal extern static bool _wrap_love_dll_type_ParticleSystem_setColors(IntPtr p, Int4[] colorarray, int colorarray_length);
        internal static bool wrap_love_dll_type_ParticleSystem_setColors(IntPtr p, Int4[] colorarray, int colorarray_length)
        {
            return CheckCAPIException(_wrap_love_dll_type_ParticleSystem_setColors(p, colorarray, colorarray_length));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_getColors")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_getColors(IntPtr p, out IntPtr out_colorarray, out int out_colorarray_length);
        internal static void wrap_love_dll_type_ParticleSystem_getColors(IntPtr p, out IntPtr out_colorarray, out int out_colorarray_length)
        {
            _wrap_love_dll_type_ParticleSystem_getColors(p, out out_colorarray, out out_colorarray_length);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_setQuads")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_setQuads(IntPtr p, IntPtr[] quadsarray, int quadsarray_length);
        internal static void wrap_love_dll_type_ParticleSystem_setQuads(IntPtr p, IntPtr[] quadsarray, int quadsarray_length)
        {
            _wrap_love_dll_type_ParticleSystem_setQuads(p, quadsarray, quadsarray_length);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_getQuads")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_getQuads(IntPtr p, out IntPtr out_quadsarray, out int out_quadsarray_length);
        internal static void wrap_love_dll_type_ParticleSystem_getQuads(IntPtr p, out IntPtr out_quadsarray, out int out_quadsarray_length)
        {
            _wrap_love_dll_type_ParticleSystem_getQuads(p, out out_quadsarray, out out_quadsarray_length);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_setRelativeRotation")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_setRelativeRotation(IntPtr p, bool enable);
        internal static void wrap_love_dll_type_ParticleSystem_setRelativeRotation(IntPtr p, bool enable)
        {
            _wrap_love_dll_type_ParticleSystem_setRelativeRotation(p, enable);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_hasRelativeRotation")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_hasRelativeRotation(IntPtr p, out bool out_enable);
        internal static void wrap_love_dll_type_ParticleSystem_hasRelativeRotation(IntPtr p, out bool out_enable)
        {
            _wrap_love_dll_type_ParticleSystem_hasRelativeRotation(p, out out_enable);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_getCount")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_getCount(IntPtr p, out uint out_count);
        internal static void wrap_love_dll_type_ParticleSystem_getCount(IntPtr p, out uint out_count)
        {
            _wrap_love_dll_type_ParticleSystem_getCount(p, out out_count);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_start")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_start(IntPtr p);
        internal static void wrap_love_dll_type_ParticleSystem_start(IntPtr p)
        {
            _wrap_love_dll_type_ParticleSystem_start(p);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_stop")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_stop(IntPtr p);
        internal static void wrap_love_dll_type_ParticleSystem_stop(IntPtr p)
        {
            _wrap_love_dll_type_ParticleSystem_stop(p);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_pause")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_pause(IntPtr p);
        internal static void wrap_love_dll_type_ParticleSystem_pause(IntPtr p)
        {
            _wrap_love_dll_type_ParticleSystem_pause(p);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_reset")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_reset(IntPtr p);
        internal static void wrap_love_dll_type_ParticleSystem_reset(IntPtr p)
        {
            _wrap_love_dll_type_ParticleSystem_reset(p);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_emit")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_emit(IntPtr p, int num);
        internal static void wrap_love_dll_type_ParticleSystem_emit(IntPtr p, int num)
        {
            _wrap_love_dll_type_ParticleSystem_emit(p, num);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_isActive")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_isActive(IntPtr p, out bool out_result);
        internal static void wrap_love_dll_type_ParticleSystem_isActive(IntPtr p, out bool out_result)
        {
            _wrap_love_dll_type_ParticleSystem_isActive(p, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_isPaused")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_isPaused(IntPtr p, out bool out_result);
        internal static void wrap_love_dll_type_ParticleSystem_isPaused(IntPtr p, out bool out_result)
        {
            _wrap_love_dll_type_ParticleSystem_isPaused(p, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_isStopped")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_isStopped(IntPtr p, out bool out_result);
        internal static void wrap_love_dll_type_ParticleSystem_isStopped(IntPtr p, out bool out_result)
        {
            _wrap_love_dll_type_ParticleSystem_isStopped(p, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ParticleSystem_update")]
        internal extern static void _wrap_love_dll_type_ParticleSystem_update(IntPtr p, float dt);
        internal static void wrap_love_dll_type_ParticleSystem_update(IntPtr p, float dt)
        {
            _wrap_love_dll_type_ParticleSystem_update(p, dt);
        }



        #endregion
        #region  type - Quad

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Quad_setViewport")]
        internal extern static void _wrap_love_dll_type_Quad_setViewport(IntPtr p, float x, float y, float w, float h);
        internal static void wrap_love_dll_type_Quad_setViewport(IntPtr p, float x, float y, float w, float h)
        {
            _wrap_love_dll_type_Quad_setViewport(p, x, y, w, h);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Quad_getViewport")]
        internal extern static void _wrap_love_dll_type_Quad_getViewport(IntPtr p, out float out_x, out float out_y, out float out_w, out float out_h);
        internal static void wrap_love_dll_type_Quad_getViewport(IntPtr p, out float out_x, out float out_y, out float out_w, out float out_h)
        {
            _wrap_love_dll_type_Quad_getViewport(p, out out_x, out out_y, out out_w, out out_h);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Quad_getTextureDimensions")]
        internal extern static void _wrap_love_dll_type_Quad_getTextureDimensions(IntPtr p, out double out_sw, out double out_sh);
        internal static void wrap_love_dll_type_Quad_getTextureDimensions(IntPtr p, out double out_sw, out double out_sh)
        {
            _wrap_love_dll_type_Quad_getTextureDimensions(p, out out_sw, out out_sh);
        }



        #endregion
        #region  type - Shader

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Shader_getWarnings")]
        internal extern static void _wrap_love_dll_type_Shader_getWarnings(IntPtr p, out IntPtr out_str);
        internal static void wrap_love_dll_type_Shader_getWarnings(IntPtr p, out IntPtr out_str)
        {
            _wrap_love_dll_type_Shader_getWarnings(p, out out_str);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Shader_sendColors")]
        internal extern static bool _wrap_love_dll_type_Shader_sendColors(IntPtr p, byte[] name, Float4[] valuearray, int value_lenght);
        internal static bool wrap_love_dll_type_Shader_sendColors(IntPtr p, byte[] name, Float4[] valuearray, int value_lenght)
        {
            return CheckCAPIException(_wrap_love_dll_type_Shader_sendColors(p, name, valuearray, value_lenght));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Shader_sendFloats")]
        internal extern static bool _wrap_love_dll_type_Shader_sendFloats(IntPtr p, byte[] name, float[] valuearray, int valuearray_lenght);
        internal static bool wrap_love_dll_type_Shader_sendFloats(IntPtr p, byte[] name, float[] valuearray, int valuearray_lenght)
        {
            return CheckCAPIException(_wrap_love_dll_type_Shader_sendFloats(p, name, valuearray, valuearray_lenght));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Shader_sendUints")]
        internal extern static bool _wrap_love_dll_type_Shader_sendUints(IntPtr p, byte[] name, uint[] valuearray, int valuearray_lenght);
        internal static bool wrap_love_dll_type_Shader_sendUints(IntPtr p, byte[] name, uint[] valuearray, int valuearray_lenght)
        {
            return CheckCAPIException(_wrap_love_dll_type_Shader_sendUints(p, name, valuearray, valuearray_lenght));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Shader_sendInts")]
        internal extern static bool _wrap_love_dll_type_Shader_sendInts(IntPtr p, byte[] name, int[] valuearray, int valuearray_lenght);
        internal static bool wrap_love_dll_type_Shader_sendInts(IntPtr p, byte[] name, int[] valuearray, int valuearray_lenght)
        {
            return CheckCAPIException(_wrap_love_dll_type_Shader_sendInts(p, name, valuearray, valuearray_lenght));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Shader_sendBooleans")]
        internal extern static bool _wrap_love_dll_type_Shader_sendBooleans(IntPtr p, byte[] name, bool[] valuearray, int valuearray_lenght);
        internal static bool wrap_love_dll_type_Shader_sendBooleans(IntPtr p, byte[] name, bool[] valuearray, int valuearray_lenght)
        {
            return CheckCAPIException(_wrap_love_dll_type_Shader_sendBooleans(p, name, valuearray, valuearray_lenght));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Shader_sendMatrices")]
        internal extern static bool _wrap_love_dll_type_Shader_sendMatrices(IntPtr p, byte[] name, float[] valueArray, int columns_lenght, int rows_length, int matrix_count);
        internal static bool wrap_love_dll_type_Shader_sendMatrices(IntPtr p, byte[] name, float[] valueArray, int columns_lenght, int rows_length, int matrix_count)
        {
            return CheckCAPIException(_wrap_love_dll_type_Shader_sendMatrices(p, name, valueArray, columns_lenght, rows_length, matrix_count));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Shader_sendTexture")]
        internal extern static bool _wrap_love_dll_type_Shader_sendTexture(IntPtr p, byte[] name, IntPtr[] texture, int texture_lenght);
        internal static bool wrap_love_dll_type_Shader_sendTexture(IntPtr p, byte[] name, IntPtr[] texture, int texture_lenght)
        {
            return CheckCAPIException(_wrap_love_dll_type_Shader_sendTexture(p, name, texture, texture_lenght));
        }



        #endregion
        #region  type - SpriteBatch

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_SpriteBatch_add")]
        internal extern static bool _wrap_love_dll_type_SpriteBatch_add(IntPtr p, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky, out int out_index);
        internal static bool wrap_love_dll_type_SpriteBatch_add(IntPtr p, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky, out int out_index)
        {
            return CheckCAPIException(_wrap_love_dll_type_SpriteBatch_add(p, x, y, a, sx, sy, ox, oy, kx, ky, out out_index));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_SpriteBatch_add_Quad")]
        internal extern static bool _wrap_love_dll_type_SpriteBatch_add_Quad(IntPtr p, IntPtr quad, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky, out int out_index);
        internal static bool wrap_love_dll_type_SpriteBatch_add_Quad(IntPtr p, IntPtr quad, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky, out int out_index)
        {
            return CheckCAPIException(_wrap_love_dll_type_SpriteBatch_add_Quad(p, quad, x, y, a, sx, sy, ox, oy, kx, ky, out out_index));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_SpriteBatch_set")]
        internal extern static bool _wrap_love_dll_type_SpriteBatch_set(IntPtr p, int index, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky);
        internal static bool wrap_love_dll_type_SpriteBatch_set(IntPtr p, int index, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky)
        {
            return CheckCAPIException(_wrap_love_dll_type_SpriteBatch_set(p, index, x, y, a, sx, sy, ox, oy, kx, ky));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_SpriteBatch_set_Quad")]
        internal extern static bool _wrap_love_dll_type_SpriteBatch_set_Quad(IntPtr p, int index, IntPtr quad, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky);
        internal static bool wrap_love_dll_type_SpriteBatch_set_Quad(IntPtr p, int index, IntPtr quad, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky)
        {
            return CheckCAPIException(_wrap_love_dll_type_SpriteBatch_set_Quad(p, index, quad, x, y, a, sx, sy, ox, oy, kx, ky));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_SpriteBatch_clear")]
        internal extern static void _wrap_love_dll_type_SpriteBatch_clear(IntPtr p);
        internal static void wrap_love_dll_type_SpriteBatch_clear(IntPtr p)
        {
            _wrap_love_dll_type_SpriteBatch_clear(p);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_SpriteBatch_flush")]
        internal extern static void _wrap_love_dll_type_SpriteBatch_flush(IntPtr p);
        internal static void wrap_love_dll_type_SpriteBatch_flush(IntPtr p)
        {
            _wrap_love_dll_type_SpriteBatch_flush(p);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_SpriteBatch_setTexture")]
        internal extern static void _wrap_love_dll_type_SpriteBatch_setTexture(IntPtr p, IntPtr tex);
        internal static void wrap_love_dll_type_SpriteBatch_setTexture(IntPtr p, IntPtr tex)
        {
            _wrap_love_dll_type_SpriteBatch_setTexture(p, tex);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_SpriteBatch_getTexture")]
        internal extern static bool _wrap_love_dll_type_SpriteBatch_getTexture(IntPtr p, out IntPtr out_texture);
        internal static bool wrap_love_dll_type_SpriteBatch_getTexture(IntPtr p, out IntPtr out_texture)
        {
            return CheckCAPIException(_wrap_love_dll_type_SpriteBatch_getTexture(p, out out_texture));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_SpriteBatch_setColor_nil")]
        internal extern static void _wrap_love_dll_type_SpriteBatch_setColor_nil(IntPtr p);
        internal static void wrap_love_dll_type_SpriteBatch_setColor_nil(IntPtr p)
        {
            _wrap_love_dll_type_SpriteBatch_setColor_nil(p);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_SpriteBatch_setColor")]
        internal extern static void _wrap_love_dll_type_SpriteBatch_setColor(IntPtr p, float r, float g, float b, float a);
        internal static void wrap_love_dll_type_SpriteBatch_setColor(IntPtr p, float r, float g, float b, float a)
        {
            _wrap_love_dll_type_SpriteBatch_setColor(p, r, g, b, a);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_SpriteBatch_getColor")]
        internal extern static void _wrap_love_dll_type_SpriteBatch_getColor(IntPtr p, out bool out_exist, out float out_r, out float out_g, out float out_b, out float out_a);
        internal static void wrap_love_dll_type_SpriteBatch_getColor(IntPtr p, out bool out_exist, out float out_r, out float out_g, out float out_b, out float out_a)
        {
            _wrap_love_dll_type_SpriteBatch_getColor(p, out out_exist, out out_r, out out_g, out out_b, out out_a);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_SpriteBatch_getCount")]
        internal extern static void _wrap_love_dll_type_SpriteBatch_getCount(IntPtr p, out int out_count);
        internal static void wrap_love_dll_type_SpriteBatch_getCount(IntPtr p, out int out_count)
        {
            _wrap_love_dll_type_SpriteBatch_getCount(p, out out_count);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_SpriteBatch_getBufferSize")]
        internal extern static void _wrap_love_dll_type_SpriteBatch_getBufferSize(IntPtr p, out int out_buffersize);
        internal static void wrap_love_dll_type_SpriteBatch_getBufferSize(IntPtr p, out int out_buffersize)
        {
            _wrap_love_dll_type_SpriteBatch_getBufferSize(p, out out_buffersize);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_SpriteBatch_attachAttribute")]
        internal extern static void _wrap_love_dll_type_SpriteBatch_attachAttribute(IntPtr p, byte[] name, IntPtr mesh);
        internal static void wrap_love_dll_type_SpriteBatch_attachAttribute(IntPtr p, byte[] name, IntPtr mesh)
        {
            _wrap_love_dll_type_SpriteBatch_attachAttribute(p, name, mesh);
        }



        #endregion
        #region  type - Text

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Text_set_coloredstring")]
        internal extern static bool _wrap_love_dll_type_Text_set_coloredstring(IntPtr p, BytePtr[] coloredStringText, Float4[] coloredStringColor, int coloredStringLength);
        internal static bool wrap_love_dll_type_Text_set_coloredstring(IntPtr p, BytePtr[] coloredStringText, Float4[] coloredStringColor, int coloredStringLength)
        {
            return CheckCAPIException(_wrap_love_dll_type_Text_set_coloredstring(p, coloredStringText, coloredStringColor, coloredStringLength));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Text_setf")]
        internal extern static bool _wrap_love_dll_type_Text_setf(IntPtr p, BytePtr[] coloredStringText, Float4[] coloredStringColor, int coloredStringLength, float wraplimit, int align_type);
        internal static bool wrap_love_dll_type_Text_setf(IntPtr p, BytePtr[] coloredStringText, Float4[] coloredStringColor, int coloredStringLength, float wraplimit, int align_type)
        {
            return CheckCAPIException(_wrap_love_dll_type_Text_setf(p, coloredStringText, coloredStringColor, coloredStringLength, wraplimit, align_type));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Text_add")]
        internal extern static bool _wrap_love_dll_type_Text_add(IntPtr p, BytePtr[] coloredStringText, Float4[] coloredStringColor, int coloredStringLength, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky, out int out_index);
        internal static bool wrap_love_dll_type_Text_add(IntPtr p, BytePtr[] coloredStringText, Float4[] coloredStringColor, int coloredStringLength, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky, out int out_index)
        {
            return CheckCAPIException(_wrap_love_dll_type_Text_add(p, coloredStringText, coloredStringColor, coloredStringLength, x, y, a, sx, sy, ox, oy, kx, ky, out out_index));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Text_addf")]
        internal extern static bool _wrap_love_dll_type_Text_addf(IntPtr p, BytePtr[] coloredStringText, Float4[] coloredStringColor, int coloredStringLength, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky, float wraplimit, int align_type, out int out_index);
        internal static bool wrap_love_dll_type_Text_addf(IntPtr p, BytePtr[] coloredStringText, Float4[] coloredStringColor, int coloredStringLength, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky, float wraplimit, int align_type, out int out_index)
        {
            return CheckCAPIException(_wrap_love_dll_type_Text_addf(p, coloredStringText, coloredStringColor, coloredStringLength, x, y, a, sx, sy, ox, oy, kx, ky, wraplimit, align_type, out out_index));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Text_clear")]
        internal extern static void _wrap_love_dll_type_Text_clear(IntPtr p);
        internal static void wrap_love_dll_type_Text_clear(IntPtr p)
        {
            _wrap_love_dll_type_Text_clear(p);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Text_setFont")]
        internal extern static bool _wrap_love_dll_type_Text_setFont(IntPtr p, IntPtr f);
        internal static bool wrap_love_dll_type_Text_setFont(IntPtr p, IntPtr f)
        {
            return CheckCAPIException(_wrap_love_dll_type_Text_setFont(p, f));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Text_getFont")]
        internal extern static void _wrap_love_dll_type_Text_getFont(IntPtr p, out IntPtr font);
        internal static void wrap_love_dll_type_Text_getFont(IntPtr p, out IntPtr font)
        {
            _wrap_love_dll_type_Text_getFont(p, out font);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Text_getWidth")]
        internal extern static void _wrap_love_dll_type_Text_getWidth(IntPtr p, int index, out int out_w);
        internal static void wrap_love_dll_type_Text_getWidth(IntPtr p, int index, out int out_w)
        {
            _wrap_love_dll_type_Text_getWidth(p, index, out out_w);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Text_getHeight")]
        internal extern static void _wrap_love_dll_type_Text_getHeight(IntPtr p, int index, out int out_h);
        internal static void wrap_love_dll_type_Text_getHeight(IntPtr p, int index, out int out_h)
        {
            _wrap_love_dll_type_Text_getHeight(p, index, out out_h);
        }



        #endregion
        #region  type - Texture

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Texture_getWidth")]
        internal extern static void _wrap_love_dll_type_Texture_getWidth(IntPtr p, out int out_w);
        internal static void wrap_love_dll_type_Texture_getWidth(IntPtr p, out int out_w)
        {
            _wrap_love_dll_type_Texture_getWidth(p, out out_w);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Texture_getHeight")]
        internal extern static void _wrap_love_dll_type_Texture_getHeight(IntPtr p, out int out_h);
        internal static void wrap_love_dll_type_Texture_getHeight(IntPtr p, out int out_h)
        {
            _wrap_love_dll_type_Texture_getHeight(p, out out_h);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Texture_setFilter")]
        internal extern static bool _wrap_love_dll_type_Texture_setFilter(IntPtr p, int filtermin_type, int filtermag_type, float anisotropy);
        internal static bool wrap_love_dll_type_Texture_setFilter(IntPtr p, int filtermin_type, int filtermag_type, float anisotropy)
        {
            return CheckCAPIException(_wrap_love_dll_type_Texture_setFilter(p, filtermin_type, filtermag_type, anisotropy));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Texture_getFilter")]
        internal extern static void _wrap_love_dll_type_Texture_getFilter(IntPtr p, out int out_filtermin_type, out int out_filtermag_type, out float out_anisotropy);
        internal static void wrap_love_dll_type_Texture_getFilter(IntPtr p, out int out_filtermin_type, out int out_filtermag_type, out float out_anisotropy)
        {
            _wrap_love_dll_type_Texture_getFilter(p, out out_filtermin_type, out out_filtermag_type, out out_anisotropy);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Texture_setWrap")]
        internal extern static void _wrap_love_dll_type_Texture_setWrap(IntPtr p, int wraphoriz_type, int wrapvert_type);
        internal static void wrap_love_dll_type_Texture_setWrap(IntPtr p, int wraphoriz_type, int wrapvert_type)
        {
            _wrap_love_dll_type_Texture_setWrap(p, wraphoriz_type, wrapvert_type);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Texture_getWrap")]
        internal extern static void _wrap_love_dll_type_Texture_getWrap(IntPtr p, out int out_wraphoriz_type, out int out_wrapvert_type);
        internal static void wrap_love_dll_type_Texture_getWrap(IntPtr p, out int out_wraphoriz_type, out int out_wrapvert_type)
        {
            _wrap_love_dll_type_Texture_getWrap(p, out out_wraphoriz_type, out out_wrapvert_type);
        }



        #endregion
        #region  type - Video

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Video_getStream")]
        internal extern static void _wrap_love_dll_type_Video_getStream(IntPtr p, out IntPtr out_videsStream);
        internal static void wrap_love_dll_type_Video_getStream(IntPtr p, out IntPtr out_videsStream)
        {
            _wrap_love_dll_type_Video_getStream(p, out out_videsStream);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Video_getSource")]
        internal extern static void _wrap_love_dll_type_Video_getSource(IntPtr p, out IntPtr out_source);
        internal static void wrap_love_dll_type_Video_getSource(IntPtr p, out IntPtr out_source)
        {
            _wrap_love_dll_type_Video_getSource(p, out out_source);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Video_setSource_nil")]
        internal extern static void _wrap_love_dll_type_Video_setSource_nil(IntPtr p);
        internal static void wrap_love_dll_type_Video_setSource_nil(IntPtr p)
        {
            _wrap_love_dll_type_Video_setSource_nil(p);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Video_setSource")]
        internal extern static void _wrap_love_dll_type_Video_setSource(IntPtr p, IntPtr source);
        internal static void wrap_love_dll_type_Video_setSource(IntPtr p, IntPtr source)
        {
            _wrap_love_dll_type_Video_setSource(p, source);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Video_getWidth")]
        internal extern static void _wrap_love_dll_type_Video_getWidth(IntPtr p, out int out_w);
        internal static void wrap_love_dll_type_Video_getWidth(IntPtr p, out int out_w)
        {
            _wrap_love_dll_type_Video_getWidth(p, out out_w);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Video_getHeight")]
        internal extern static void _wrap_love_dll_type_Video_getHeight(IntPtr p, out int out_h);
        internal static void wrap_love_dll_type_Video_getHeight(IntPtr p, out int out_h)
        {
            _wrap_love_dll_type_Video_getHeight(p, out out_h);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Video_setFilter")]
        internal extern static bool _wrap_love_dll_type_Video_setFilter(IntPtr p, int filtermin_type, int filtermag_type, float anisotropy);
        internal static bool wrap_love_dll_type_Video_setFilter(IntPtr p, int filtermin_type, int filtermag_type, float anisotropy)
        {
            return CheckCAPIException(_wrap_love_dll_type_Video_setFilter(p, filtermin_type, filtermag_type, anisotropy));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Video_getFilter")]
        internal extern static void _wrap_love_dll_type_Video_getFilter(IntPtr p, out int out_filtermin_type, out int out_filtermag_type, out float out_anisotropy);
        internal static void wrap_love_dll_type_Video_getFilter(IntPtr p, out int out_filtermin_type, out int out_filtermag_type, out float out_anisotropy)
        {
            _wrap_love_dll_type_Video_getFilter(p, out out_filtermin_type, out out_filtermag_type, out out_anisotropy);
        }



        #endregion
        #region  type - CompressedImageData

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_CompressedImageData_getWidth")]
        internal extern static bool _wrap_love_dll_type_CompressedImageData_getWidth(IntPtr p, int miplevel, out int out_w);
        internal static bool wrap_love_dll_type_CompressedImageData_getWidth(IntPtr p, int miplevel, out int out_w)
        {
            return CheckCAPIException(_wrap_love_dll_type_CompressedImageData_getWidth(p, miplevel, out out_w));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_CompressedImageData_getHeight")]
        internal extern static bool _wrap_love_dll_type_CompressedImageData_getHeight(IntPtr p, int miplevel, out int out_h);
        internal static bool wrap_love_dll_type_CompressedImageData_getHeight(IntPtr p, int miplevel, out int out_h)
        {
            return CheckCAPIException(_wrap_love_dll_type_CompressedImageData_getHeight(p, miplevel, out out_h));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_CompressedImageData_getMipmapCount")]
        internal extern static void _wrap_love_dll_type_CompressedImageData_getMipmapCount(IntPtr p, out int out_count);
        internal static void wrap_love_dll_type_CompressedImageData_getMipmapCount(IntPtr p, out int out_count)
        {
            _wrap_love_dll_type_CompressedImageData_getMipmapCount(p, out out_count);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_CompressedImageData_getFormat")]
        internal extern static void _wrap_love_dll_type_CompressedImageData_getFormat(IntPtr p, out int out_format_type);
        internal static void wrap_love_dll_type_CompressedImageData_getFormat(IntPtr p, out int out_format_type)
        {
            _wrap_love_dll_type_CompressedImageData_getFormat(p, out out_format_type);
        }



        #endregion
        #region  type - ImageData

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ImageData_getWidth")]
        internal extern static void _wrap_love_dll_type_ImageData_getWidth(IntPtr p, out int out_w);
        internal static void wrap_love_dll_type_ImageData_getWidth(IntPtr p, out int out_w)
        {
            _wrap_love_dll_type_ImageData_getWidth(p, out out_w);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ImageData_getHeight")]
        internal extern static void _wrap_love_dll_type_ImageData_getHeight(IntPtr p, out int out_h);
        internal static void wrap_love_dll_type_ImageData_getHeight(IntPtr p, out int out_h)
        {
            _wrap_love_dll_type_ImageData_getHeight(p, out out_h);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ImageData_getPixel")]
        internal extern static bool _wrap_love_dll_type_ImageData_getPixel(IntPtr p, int x, int y, out Pixel out_pixel);
        internal static bool wrap_love_dll_type_ImageData_getPixel(IntPtr p, int x, int y, out Pixel out_pixel)
        {
            return CheckCAPIException(_wrap_love_dll_type_ImageData_getPixel(p, x, y, out out_pixel));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ImageData_setPixel")]
        internal extern static bool _wrap_love_dll_type_ImageData_setPixel(IntPtr p, int x, int y, Pixel pixel);
        internal static bool wrap_love_dll_type_ImageData_setPixel(IntPtr p, int x, int y, Pixel pixel)
        {
            return CheckCAPIException(_wrap_love_dll_type_ImageData_setPixel(p, x, y, pixel));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ImageData_GetFormat")]
        internal extern static void _wrap_love_dll_type_ImageData_GetFormat(IntPtr p, out int out_pixelFormat);
        internal static void wrap_love_dll_type_ImageData_GetFormat(IntPtr p, out int out_pixelFormat)
        {
            _wrap_love_dll_type_ImageData_GetFormat(p, out out_pixelFormat);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ImageData_ext_getPixelUnsafe")]
        internal extern static void _wrap_love_dll_type_ImageData_ext_getPixelUnsafe(IntPtr p, int x, int y, out byte out_r, out byte out_g, out byte out_b, out byte out_a);
        internal static void wrap_love_dll_type_ImageData_ext_getPixelUnsafe(IntPtr p, int x, int y, out byte out_r, out byte out_g, out byte out_b, out byte out_a)
        {
            _wrap_love_dll_type_ImageData_ext_getPixelUnsafe(p, x, y, out out_r, out out_g, out out_b, out out_a);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ImageData_ext_setPixelUnsafe")]
        internal extern static void _wrap_love_dll_type_ImageData_ext_setPixelUnsafe(IntPtr p, int x, int y, byte r, byte g, byte b, byte a);
        internal static void wrap_love_dll_type_ImageData_ext_setPixelUnsafe(IntPtr p, int x, int y, byte r, byte g, byte b, byte a)
        {
            _wrap_love_dll_type_ImageData_ext_setPixelUnsafe(p, x, y, r, g, b, a);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ImageData_paste")]
        internal extern static void _wrap_love_dll_type_ImageData_paste(IntPtr p, IntPtr src_imageData, int dx, int dy, int sx, int sy, int sw, int sh);
        internal static void wrap_love_dll_type_ImageData_paste(IntPtr p, IntPtr src_imageData, int dx, int dy, int sx, int sy, int sw, int sh)
        {
            _wrap_love_dll_type_ImageData_paste(p, src_imageData, dx, dy, sx, sy, sw, sh);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_ImageData_encode")]
        internal extern static void _wrap_love_dll_type_ImageData_encode(IntPtr p, int format_type, bool writeToFile, byte[] filename, out IntPtr out_fileData);
        internal static void wrap_love_dll_type_ImageData_encode(IntPtr p, int format_type, bool writeToFile, byte[] filename, out IntPtr out_fileData)
        {
            _wrap_love_dll_type_ImageData_encode(p, format_type, writeToFile, filename, out out_fileData);
        }

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void inner_wrap_love_dll_type_ImageData_getPixelSize(IntPtr p, out int out_pixelSize);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void inner_wrap_love_dll_type_ImageData_lock(IntPtr p);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void inner_wrap_love_dll_type_ImageData_unlock(IntPtr p);


        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void inner_wrap_love_dll_type_ImageData_setPixels(IntPtr p, IntPtr data, int byteLenght);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void inner_wrap_love_dll_type_ImageData_setPixels_float4(IntPtr p, Float4[] src);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        internal extern static void inner_wrap_love_dll_type_ImageData_getPixels_float4(IntPtr p, IntPtr dest);

        #endregion
        #region  type - Cursor

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Cursor_getType")]
        internal extern static void _wrap_love_dll_type_Cursor_getType(IntPtr p, out int out_cursortype_type, out bool out_custom);
        internal static void wrap_love_dll_type_Cursor_getType(IntPtr p, out int out_cursortype_type, out bool out_custom)
        {
            _wrap_love_dll_type_Cursor_getType(p, out out_cursortype_type, out out_custom);
        }


        #endregion
        #region  type - Decoder

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Decoder_getChannelCount")]
        internal extern static void _wrap_love_dll_type_Decoder_getChannelCount(IntPtr p, out int out_channels);
        internal static void wrap_love_dll_type_Decoder_getChannelCount(IntPtr p, out int out_channels)
        {
            _wrap_love_dll_type_Decoder_getChannelCount(p, out out_channels);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Decoder_getBitDepth")]
        internal extern static void _wrap_love_dll_type_Decoder_getBitDepth(IntPtr p, out int out_bitDepth);
        internal static void wrap_love_dll_type_Decoder_getBitDepth(IntPtr p, out int out_bitDepth)
        {
            _wrap_love_dll_type_Decoder_getBitDepth(p, out out_bitDepth);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Decoder_getSampleRate")]
        internal extern static void _wrap_love_dll_type_Decoder_getSampleRate(IntPtr p, out int out_sampleRate);
        internal static void wrap_love_dll_type_Decoder_getSampleRate(IntPtr p, out int out_sampleRate)
        {
            _wrap_love_dll_type_Decoder_getSampleRate(p, out out_sampleRate);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Decoder_getDuration")]
        internal extern static void _wrap_love_dll_type_Decoder_getDuration(IntPtr p, out double out_duration);
        internal static void wrap_love_dll_type_Decoder_getDuration(IntPtr p, out double out_duration)
        {
            _wrap_love_dll_type_Decoder_getDuration(p, out out_duration);
        }



        #endregion
        #region  type - SoundData

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_SoundData_getChannelCount")]
        internal extern static void _wrap_love_dll_SoundData_getChannelCount(IntPtr p, out int out_channels);
        internal static void wrap_love_dll_SoundData_getChannelCount(IntPtr p, out int out_channels)
        {
            _wrap_love_dll_SoundData_getChannelCount(p, out out_channels);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_SoundData_getBitDepth")]
        internal extern static void _wrap_love_dll_SoundData_getBitDepth(IntPtr p, out int out_bitDepth);
        internal static void wrap_love_dll_SoundData_getBitDepth(IntPtr p, out int out_bitDepth)
        {
            _wrap_love_dll_SoundData_getBitDepth(p, out out_bitDepth);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_SoundData_getSampleRate")]
        internal extern static void _wrap_love_dll_SoundData_getSampleRate(IntPtr p, out int out_sampleRate);
        internal static void wrap_love_dll_SoundData_getSampleRate(IntPtr p, out int out_sampleRate)
        {
            _wrap_love_dll_SoundData_getSampleRate(p, out out_sampleRate);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_SoundData_getSampleCount")]
        internal extern static void _wrap_love_dll_SoundData_getSampleCount(IntPtr p, out int out_sampleCount);
        internal static void wrap_love_dll_SoundData_getSampleCount(IntPtr p, out int out_sampleCount)
        {
            _wrap_love_dll_SoundData_getSampleCount(p, out out_sampleCount);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_SoundData_getDuration")]
        internal extern static void _wrap_love_dll_SoundData_getDuration(IntPtr p, out double out_duration);
        internal static void wrap_love_dll_SoundData_getDuration(IntPtr p, out double out_duration)
        {
            _wrap_love_dll_SoundData_getDuration(p, out out_duration);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_SoundData_setSample")]
        internal extern static bool _wrap_love_dll_SoundData_setSample(IntPtr p, int i, float sample);
        internal static bool wrap_love_dll_SoundData_setSample(IntPtr p, int i, float sample)
        {
            return CheckCAPIException(_wrap_love_dll_SoundData_setSample(p, i, sample));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_SoundData_getSample")]
        internal extern static bool _wrap_love_dll_SoundData_getSample(IntPtr p, int i, out float out_sample);
        internal static bool wrap_love_dll_SoundData_getSample(IntPtr p, int i, out float out_sample)
        {
            return CheckCAPIException(_wrap_love_dll_SoundData_getSample(p, i, out out_sample));
        }



        #endregion
        #region  type - VideoStream

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_VideoStream_setSync")]
        internal extern static void _wrap_love_dll_type_VideoStream_setSync(IntPtr p, IntPtr source);
        internal static void wrap_love_dll_type_VideoStream_setSync(IntPtr p, IntPtr source)
        {
            _wrap_love_dll_type_VideoStream_setSync(p, source);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_VideoStream_getFilename")]
        internal extern static void _wrap_love_dll_type_VideoStream_getFilename(IntPtr p, out IntPtr out_filename);
        internal static void wrap_love_dll_type_VideoStream_getFilename(IntPtr p, out IntPtr out_filename)
        {
            _wrap_love_dll_type_VideoStream_getFilename(p, out out_filename);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_VideoStream_play")]
        internal extern static void _wrap_love_dll_type_VideoStream_play(IntPtr p);
        internal static void wrap_love_dll_type_VideoStream_play(IntPtr p)
        {
            _wrap_love_dll_type_VideoStream_play(p);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_VideoStream_pause")]
        internal extern static void _wrap_love_dll_type_VideoStream_pause(IntPtr p);
        internal static void wrap_love_dll_type_VideoStream_pause(IntPtr p)
        {
            _wrap_love_dll_type_VideoStream_pause(p);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_VideoStream_seek")]
        internal extern static void _wrap_love_dll_type_VideoStream_seek(IntPtr p, double offset);
        internal static void wrap_love_dll_type_VideoStream_seek(IntPtr p, double offset)
        {
            _wrap_love_dll_type_VideoStream_seek(p, offset);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_VideoStream_rewind")]
        internal extern static void _wrap_love_dll_type_VideoStream_rewind(IntPtr p);
        internal static void wrap_love_dll_type_VideoStream_rewind(IntPtr p)
        {
            _wrap_love_dll_type_VideoStream_rewind(p);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_VideoStream_tell")]
        internal extern static void _wrap_love_dll_type_VideoStream_tell(IntPtr p, out double out_position);
        internal static void wrap_love_dll_type_VideoStream_tell(IntPtr p, out double out_position)
        {
            _wrap_love_dll_type_VideoStream_tell(p, out out_position);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_VideoStream_isPlaying")]
        internal extern static void _wrap_love_dll_type_VideoStream_isPlaying(IntPtr p, out bool out_isplaying);
        internal static void wrap_love_dll_type_VideoStream_isPlaying(IntPtr p, out bool out_isplaying)
        {
            _wrap_love_dll_type_VideoStream_isPlaying(p, out out_isplaying);
        }



        #endregion
        #region  type - BezierCurve

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_BezierCurve_getDegree")]
        internal extern static void _wrap_love_dll_type_BezierCurve_getDegree(IntPtr p, out int out_degree);
        internal static void wrap_love_dll_type_BezierCurve_getDegree(IntPtr p, out int out_degree)
        {
            _wrap_love_dll_type_BezierCurve_getDegree(p, out out_degree);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_BezierCurve_getDerivative")]
        internal extern static void _wrap_love_dll_type_BezierCurve_getDerivative(IntPtr p, out IntPtr out_deriv);
        internal static void wrap_love_dll_type_BezierCurve_getDerivative(IntPtr p, out IntPtr out_deriv)
        {
            _wrap_love_dll_type_BezierCurve_getDerivative(p, out out_deriv);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_BezierCurve_getControlPoint")]
        internal extern static bool _wrap_love_dll_type_BezierCurve_getControlPoint(IntPtr p, int idx, out float out_x, out float out_y);
        internal static bool wrap_love_dll_type_BezierCurve_getControlPoint(IntPtr p, int idx, out float out_x, out float out_y)
        {
            return CheckCAPIException(_wrap_love_dll_type_BezierCurve_getControlPoint(p, idx, out out_x, out out_y));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_BezierCurve_setControlPoint")]
        internal extern static bool _wrap_love_dll_type_BezierCurve_setControlPoint(IntPtr p, int idx, float x, float y);
        internal static bool wrap_love_dll_type_BezierCurve_setControlPoint(IntPtr p, int idx, float x, float y)
        {
            return CheckCAPIException(_wrap_love_dll_type_BezierCurve_setControlPoint(p, idx, x, y));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_BezierCurve_insertControlPoint")]
        internal extern static bool _wrap_love_dll_type_BezierCurve_insertControlPoint(IntPtr p, int idx, float x, float y);
        internal static bool wrap_love_dll_type_BezierCurve_insertControlPoint(IntPtr p, int idx, float x, float y)
        {
            return CheckCAPIException(_wrap_love_dll_type_BezierCurve_insertControlPoint(p, idx, x, y));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_BezierCurve_removeControlPoint")]
        internal extern static bool _wrap_love_dll_type_BezierCurve_removeControlPoint(IntPtr p, int idx);
        internal static bool wrap_love_dll_type_BezierCurve_removeControlPoint(IntPtr p, int idx)
        {
            return CheckCAPIException(_wrap_love_dll_type_BezierCurve_removeControlPoint(p, idx));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_BezierCurve_getControlPointCount")]
        internal extern static void _wrap_love_dll_type_BezierCurve_getControlPointCount(IntPtr p, out int out_count);
        internal static void wrap_love_dll_type_BezierCurve_getControlPointCount(IntPtr p, out int out_count)
        {
            _wrap_love_dll_type_BezierCurve_getControlPointCount(p, out out_count);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_BezierCurve_translate")]
        internal extern static void _wrap_love_dll_type_BezierCurve_translate(IntPtr p, float dx, float dy);
        internal static void wrap_love_dll_type_BezierCurve_translate(IntPtr p, float dx, float dy)
        {
            _wrap_love_dll_type_BezierCurve_translate(p, dx, dy);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_BezierCurve_rotate")]
        internal extern static void _wrap_love_dll_type_BezierCurve_rotate(IntPtr p, double phi, float ox, float oy);
        internal static void wrap_love_dll_type_BezierCurve_rotate(IntPtr p, double phi, float ox, float oy)
        {
            _wrap_love_dll_type_BezierCurve_rotate(p, phi, ox, oy);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_BezierCurve_scale")]
        internal extern static void _wrap_love_dll_type_BezierCurve_scale(IntPtr p, double s, float ox, float oy);
        internal static void wrap_love_dll_type_BezierCurve_scale(IntPtr p, double s, float ox, float oy)
        {
            _wrap_love_dll_type_BezierCurve_scale(p, s, ox, oy);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_BezierCurve_evaluate")]
        internal extern static bool _wrap_love_dll_type_BezierCurve_evaluate(IntPtr p, double t, out float out_x, out float out_y);
        internal static bool wrap_love_dll_type_BezierCurve_evaluate(IntPtr p, double t, out float out_x, out float out_y)
        {
            return CheckCAPIException(_wrap_love_dll_type_BezierCurve_evaluate(p, t, out out_x, out out_y));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_BezierCurve_getSegment")]
        internal extern static bool _wrap_love_dll_type_BezierCurve_getSegment(IntPtr p, double t1, double t2, out IntPtr out_segment);
        internal static bool wrap_love_dll_type_BezierCurve_getSegment(IntPtr p, double t1, double t2, out IntPtr out_segment)
        {
            return CheckCAPIException(_wrap_love_dll_type_BezierCurve_getSegment(p, t1, t2, out out_segment));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_BezierCurve_render")]
        internal extern static bool _wrap_love_dll_type_BezierCurve_render(IntPtr p, int accuracy, out IntPtr out_points, out int out_points_lenght);
        internal static bool wrap_love_dll_type_BezierCurve_render(IntPtr p, int accuracy, out IntPtr out_points, out int out_points_lenght)
        {
            return CheckCAPIException(_wrap_love_dll_type_BezierCurve_render(p, accuracy, out out_points, out out_points_lenght));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_BezierCurve_renderSegment")]
        internal extern static bool _wrap_love_dll_type_BezierCurve_renderSegment(IntPtr p, double start, double end, int accuracy, out IntPtr out_points, out int out_points_lenght);
        internal static bool wrap_love_dll_type_BezierCurve_renderSegment(IntPtr p, double start, double end, int accuracy, out IntPtr out_points, out int out_points_lenght)
        {
            return CheckCAPIException(_wrap_love_dll_type_BezierCurve_renderSegment(p, start, end, accuracy, out out_points, out out_points_lenght));
        }



        #endregion
        #region  type - RandomGenerator

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_RandomGenerator_random")]
        internal extern static void _wrap_love_dll_type_RandomGenerator_random(IntPtr p, out double out_result);
        internal static void wrap_love_dll_type_RandomGenerator_random(IntPtr p, out double out_result)
        {
            _wrap_love_dll_type_RandomGenerator_random(p, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_RandomGenerator_randomNormal")]
        internal extern static void _wrap_love_dll_type_RandomGenerator_randomNormal(IntPtr p, double stddev, double mean, out double out_result);
        internal static void wrap_love_dll_type_RandomGenerator_randomNormal(IntPtr p, double stddev, double mean, out double out_result)
        {
            _wrap_love_dll_type_RandomGenerator_randomNormal(p, stddev, mean, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_RandomGenerator_setSeed")]
        internal extern static bool _wrap_love_dll_type_RandomGenerator_setSeed(IntPtr p, uint low, uint high);
        internal static bool wrap_love_dll_type_RandomGenerator_setSeed(IntPtr p, uint low, uint high)
        {
            return CheckCAPIException(_wrap_love_dll_type_RandomGenerator_setSeed(p, low, high));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_RandomGenerator_getSeed")]
        internal extern static void _wrap_love_dll_type_RandomGenerator_getSeed(IntPtr p, out uint out_low, out uint out_high);
        internal static void wrap_love_dll_type_RandomGenerator_getSeed(IntPtr p, out uint out_low, out uint out_high)
        {
            _wrap_love_dll_type_RandomGenerator_getSeed(p, out out_low, out out_high);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_RandomGenerator_setState")]
        internal extern static bool _wrap_love_dll_type_RandomGenerator_setState(IntPtr p, byte[] state);
        internal static bool wrap_love_dll_type_RandomGenerator_setState(IntPtr p, byte[] state)
        {
            return CheckCAPIException(_wrap_love_dll_type_RandomGenerator_setState(p, state));
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_RandomGenerator_getState")]
        internal extern static void _wrap_love_dll_type_RandomGenerator_getState(IntPtr p, out IntPtr out_str);
        internal static void wrap_love_dll_type_RandomGenerator_getState(IntPtr p, out IntPtr out_str)
        {
            _wrap_love_dll_type_RandomGenerator_getState(p, out out_str);
        }



        #endregion
        #region  type - Joystick

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Joystick_isConnected")]
        internal extern static void _wrap_love_dll_type_Joystick_isConnected(IntPtr p, out bool out_result);
        internal static void wrap_love_dll_type_Joystick_isConnected(IntPtr p, out bool out_result)
        {
            _wrap_love_dll_type_Joystick_isConnected(p, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Joystick_getName")]
        internal extern static void _wrap_love_dll_type_Joystick_getName(IntPtr p, out IntPtr out_str);
        internal static void wrap_love_dll_type_Joystick_getName(IntPtr p, out IntPtr out_str)
        {
            _wrap_love_dll_type_Joystick_getName(p, out out_str);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Joystick_getID")]
        internal extern static void _wrap_love_dll_type_Joystick_getID(IntPtr p, out int out_id);
        internal static void wrap_love_dll_type_Joystick_getID(IntPtr p, out int out_id)
        {
            _wrap_love_dll_type_Joystick_getID(p, out out_id);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Joystick_getInstanceID")]
        internal extern static void _wrap_love_dll_type_Joystick_getInstanceID(IntPtr p, out int out_instanceid);
        internal static void wrap_love_dll_type_Joystick_getInstanceID(IntPtr p, out int out_instanceid)
        {
            _wrap_love_dll_type_Joystick_getInstanceID(p, out out_instanceid);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Joystick_getGUID")]
        internal extern static void _wrap_love_dll_type_Joystick_getGUID(IntPtr p, out IntPtr out_str);
        internal static void wrap_love_dll_type_Joystick_getGUID(IntPtr p, out IntPtr out_str)
        {
            _wrap_love_dll_type_Joystick_getGUID(p, out out_str);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Joystick_getAxisCount")]
        internal extern static void _wrap_love_dll_type_Joystick_getAxisCount(IntPtr p, out int out_count);
        internal static void wrap_love_dll_type_Joystick_getAxisCount(IntPtr p, out int out_count)
        {
            _wrap_love_dll_type_Joystick_getAxisCount(p, out out_count);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Joystick_getButtonCount")]
        internal extern static void _wrap_love_dll_type_Joystick_getButtonCount(IntPtr p, out int out_count);
        internal static void wrap_love_dll_type_Joystick_getButtonCount(IntPtr p, out int out_count)
        {
            _wrap_love_dll_type_Joystick_getButtonCount(p, out out_count);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Joystick_getHatCount")]
        internal extern static void _wrap_love_dll_type_Joystick_getHatCount(IntPtr p, out int out_count);
        internal static void wrap_love_dll_type_Joystick_getHatCount(IntPtr p, out int out_count)
        {
            _wrap_love_dll_type_Joystick_getHatCount(p, out out_count);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Joystick_getAxis")]
        internal extern static void _wrap_love_dll_type_Joystick_getAxis(IntPtr p, int axisindex, out float out_axis);
        internal static void wrap_love_dll_type_Joystick_getAxis(IntPtr p, int axisindex, out float out_axis)
        {
            _wrap_love_dll_type_Joystick_getAxis(p, axisindex, out out_axis);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Joystick_getAxes")]
        internal extern static void _wrap_love_dll_type_Joystick_getAxes(IntPtr p, out IntPtr out_axes, out int out_axes_length);
        internal static void wrap_love_dll_type_Joystick_getAxes(IntPtr p, out IntPtr out_axes, out int out_axes_length)
        {
            _wrap_love_dll_type_Joystick_getAxes(p, out out_axes, out out_axes_length);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Joystick_getHat")]
        internal extern static void _wrap_love_dll_type_Joystick_getHat(IntPtr p, int hatindex, out int out_hat_type);
        internal static void wrap_love_dll_type_Joystick_getHat(IntPtr p, int hatindex, out int out_hat_type)
        {
            _wrap_love_dll_type_Joystick_getHat(p, hatindex, out out_hat_type);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Joystick_isDown")]
        internal extern static void _wrap_love_dll_type_Joystick_isDown(IntPtr p, int button, out bool out_result);
        internal static void wrap_love_dll_type_Joystick_isDown(IntPtr p, int button, out bool out_result)
        {
            _wrap_love_dll_type_Joystick_isDown(p, button, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Joystick_isGamepad")]
        internal extern static void _wrap_love_dll_type_Joystick_isGamepad(IntPtr p, out bool out_result);
        internal static void wrap_love_dll_type_Joystick_isGamepad(IntPtr p, out bool out_result)
        {
            _wrap_love_dll_type_Joystick_isGamepad(p, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Joystick_getGamepadAxis")]
        internal extern static void _wrap_love_dll_type_Joystick_getGamepadAxis(IntPtr p, int axis_type, out float out_gamepadaxis);
        internal static void wrap_love_dll_type_Joystick_getGamepadAxis(IntPtr p, int axis_type, out float out_gamepadaxis)
        {
            _wrap_love_dll_type_Joystick_getGamepadAxis(p, axis_type, out out_gamepadaxis);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Joystick_isGamepadDown")]
        internal extern static void _wrap_love_dll_type_Joystick_isGamepadDown(IntPtr p, int gamepadButton_type, out bool out_result);
        internal static void wrap_love_dll_type_Joystick_isGamepadDown(IntPtr p, int gamepadButton_type, out bool out_result)
        {
            _wrap_love_dll_type_Joystick_isGamepadDown(p, gamepadButton_type, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Joystick_isVibrationSupported")]
        internal extern static void _wrap_love_dll_type_Joystick_isVibrationSupported(IntPtr p, out bool out_result);
        internal static void wrap_love_dll_type_Joystick_isVibrationSupported(IntPtr p, out bool out_result)
        {
            _wrap_love_dll_type_Joystick_isVibrationSupported(p, out out_result);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Joystick_setVibration_nil")]
        internal extern static void _wrap_love_dll_type_Joystick_setVibration_nil(IntPtr p, out bool out_success);
        internal static void wrap_love_dll_type_Joystick_setVibration_nil(IntPtr p, out bool out_success)
        {
            _wrap_love_dll_type_Joystick_setVibration_nil(p, out out_success);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Joystick_setVibration")]
        internal extern static void _wrap_love_dll_type_Joystick_setVibration(IntPtr p, float left, float right, float duration, out bool out_success);
        internal static void wrap_love_dll_type_Joystick_setVibration(IntPtr p, float left, float right, float duration, out bool out_success)
        {
            _wrap_love_dll_type_Joystick_setVibration(p, left, right, duration, out out_success);
        }
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Joystick_getVibration")]
        internal extern static void _wrap_love_dll_type_Joystick_getVibration(IntPtr p, out float out_left, out float out_right);
        internal static void wrap_love_dll_type_Joystick_getVibration(IntPtr p, out float out_left, out float out_right)
        {
            _wrap_love_dll_type_Joystick_getVibration(p, out out_left, out out_right);
        }



        #endregion
        #region  type - Other

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Data_getSize")]
        internal extern static void _wrap_love_dll_type_Data_getSize(IntPtr data, out uint out_datasize);
        internal static void wrap_love_dll_type_Data_getSize(IntPtr data, out uint out_datasize)
        {
            _wrap_love_dll_type_Data_getSize(data, out out_datasize);
        }


        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "wrap_love_dll_type_Data_getPointer")]
        internal extern static void _wrap_love_dll_type_Data_getPointer(IntPtr data, out IntPtr out_pointer);
        internal static void wrap_love_dll_type_Data_getPointer(IntPtr data, out IntPtr out_pointer)
        {
            _wrap_love_dll_type_Data_getPointer(data, out out_pointer);
        }

        #endregion

    }


    [StructLayout(LayoutKind.Explicit)]
    public struct Half
    {
        [FieldOffset(0)] public UInt16 value;

        /// <summary>
        /// ignores the higher 16 bits
        /// <para>https://codereview.stackexchange.com/questions/45007/half-precision-reader-writer-for-c</para>
        /// </summary>
        /// <param name="hbits"></param>
        /// <returns></returns>
        public static float IntToFloat(int hbits)
        {
            int mant = hbits & 0x03ff;            // 10 bits mantissa
            int exp = hbits & 0x7c00;            // 5 bits exponent
            if (exp == 0x7c00)                   // NaN/Inf
                exp = 0x3fc00;                    // -> NaN/Inf
            else if (exp != 0)                   // normalized value
            {
                exp += 0x1c000;                   // exp - 15 + 127
                if (mant == 0 && exp > 0x1c400)  // smooth transition
                    return BitConverter.ToSingle(BitConverter.GetBytes((hbits & 0x8000) << 16
                                                    | exp << 13 | 0x3ff), 0);
            }
            else if (mant != 0)                  // && exp==0 -> subnormal
            {
                exp = 0x1c400;                    // make it normal
                do
                {
                    mant <<= 1;                   // mantissa * 2
                    exp -= 0x400;                 // decrease exp by 1
                } while ((mant & 0x400) == 0); // while not normal
                mant &= 0x3ff;                    // discard subnormal bit
            }                                     // else +/-0 -> +/-0
            return BitConverter.ToSingle(BitConverter.GetBytes(          // combine all parts
                (hbits & 0x8000) << 16          // sign  << ( 31 - 15 )
                | (exp | mant) << 13), 0);         // value << ( 23 - 10 )
        }
        /// <summary>
        /// returns all higher 16 bits as 0 for all results
        /// <para>https://codereview.stackexchange.com/questions/45007/half-precision-reader-writer-for-c</para>
        /// </summary>
        /// <param name="fval"></param>
        /// <returns></returns>
        public static int FloatToInt(float fval)
        {
            int fbits = BitConverter.ToInt32(BitConverter.GetBytes(fval), 0);
            int sign = fbits >> 16 & 0x8000;          // sign only
            int val = (fbits & 0x7fffffff) + 0x1000; // rounded value

            if (val >= 0x47800000)               // might be or become NaN/Inf
            {                                     // avoid Inf due to rounding
                if ((fbits & 0x7fffffff) >= 0x47800000)
                {                                 // is or must become NaN/Inf
                    if (val < 0x7f800000)        // was value but too large
                        return sign | 0x7c00;     // make it +/-Inf
                    return sign | 0x7c00 |        // remains +/-Inf or NaN
                        (fbits & 0x007fffff) >> 13; // keep NaN (and Inf) bits
                }
                return sign | 0x7bff;             // unrounded not quite Inf
            }
            if (val >= 0x38800000)               // remains normalized value
                return sign | val - 0x38000000 >> 13; // exp - 127 + 15
            if (val < 0x33000000)                // too small for subnormal
                return sign;                      // becomes +/-0
            val = (fbits & 0x7fffffff) >> 23;  // tmp exp for subnormal calc
            return sign | ((fbits & 0x7fffff | 0x800000) // add subnormal bit
                 + (0x800000 >> val - 102)     // round depending on cut off
              >> 126 - val);   // div by 2^(1-(exp-127+15)) and >> 13 | exp=0
        }


        public float ToFloat()
        {
            int bits = value;
            return IntToFloat(bits);
        }

        public static Half FromFloat(float v)
        {
            int bits = FloatToInt(v);
            Half half = new Half();
            half.value = (UInt16)bits;
            return half;
        }
    }


    [StructLayout(LayoutKind.Sequential)]
    public struct RGBA8I
    {
        public UInt8 r, g, b, a;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RGBA16I
    {
        public UInt16 r, g, b, a;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RGBA16F
    {
        public Half r, g, b, a;
    }


    [StructLayout(LayoutKind.Sequential)]
    public struct RGBA32F
    {
        public float r, g, b, a;
    }



    /// <summary>
    /// <code>
    /// union Pixel
    /// {
    ///     uint8 rgba8 [4];
    ///     uint16 rgba16 [4];
    ///     half rgba16f [4];
    ///     float  rgba32f [4];
    /// };
    /// </code>
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct Pixel
    {
        [FieldOffset(0)] public RGBA8I rgba8;
        [FieldOffset(0)] public RGBA16I rgba16;
        [FieldOffset(0)] public RGBA16F rgba16f;
        [FieldOffset(0)] public RGBA32F rgba32f;

        [FieldOffset(0)] public int intValue0;
        [FieldOffset(4)] public int intValue1;
        [FieldOffset(8)] public int intValue2;
        [FieldOffset(12)] public int intValue3;
        [FieldOffset(0)] public long longValue0;
        [FieldOffset(8)] public long longValue1;
        [FieldOffset(0)] public double doubleValue0;
        [FieldOffset(8)] public double doubleValue1;

        public static string StrRGBA8(Pixel p)
        {
            return $"({p.rgba8.r},{p.rgba8.g},{p.rgba8.b},{p.rgba8.a})";
        }
        public static string StrRGBA32F(Pixel p)
        {
            return $"({p.rgba32f.r},{p.rgba32f.g},{p.rgba32f.b},{p.rgba32f.a})";
        }
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
        public Int2(int x, int y) { this.x = x; this.y = y; }
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct Int4
    {
        public int x, y, z, w;
        public int r { get { return x; } }
        public int g { get { return y; } }
        public int b { get { return z; } }
        public int a { get { return w; } }
        public int Width { get { return z; } }
        public int Height { get { return w; } }
        public Int4(int x, int y, int z, int w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct Triangle
    {
        public Float2 a, b, c;
    }


    [StructLayout(LayoutKind.Sequential)]
    public struct Float2
    {
        public Float2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        public float x, y;

        public override string ToString()
        {
            return $"({x},{y})";
        }

        public static Float2[] FromFloats(params float[] points)
        {
            Check.ArgumentNull(points, "points");
            if (points.Length % 2 == 1)
            {
                throw new Exception("points must be an integer multiple of 2.");
            }

            var result = new Float2[points.Length / 2];
            for (int i = 0; i < result.Length; i++)
            {
                result[i].x = points[2 * i];
                result[i].y = points[2 * i + 1];
            }
            return result;
        }
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

        public override string ToString()
        {
            return $"({x}, {y}, {z})";
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Float4
    {
        public float x, y, z, w;
        public float r { set { x = value; } get { return x; } }
        public float g { set { y = value; } get { return y; } }
        public float b { set { z = value; } get { return z; } }
        public float a { set { w = value; } get { return w; } }
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
    public class Matrix22
    {
        public float m00 { set { data[0] = value; } get { return data[0]; } }
        public float m01 { set { data[1] = value; } get { return data[1]; } }
        public float m10 { set { data[2] = value; } get { return data[2]; } }
        public float m11 { set { data[3] = value; } get { return data[3]; } }
        internal readonly float[] data = new float[4];
    }

    [StructLayout(LayoutKind.Sequential)]
    public class Matrix33
    {
        public float m00 { set { data[0] = value; } get { return data[0]; } }
        public float m01 { set { data[1] = value; } get { return data[1]; } }
        public float m02 { set { data[2] = value; } get { return data[2]; } }
        public float m10 { set { data[3] = value; } get { return data[3]; } }
        public float m11 { set { data[4] = value; } get { return data[4]; } }
        public float m12 { set { data[5] = value; } get { return data[5]; } }
        public float m20 { set { data[6] = value; } get { return data[6]; } }
        public float m21 { set { data[7] = value; } get { return data[7]; } }
        public float m22 { set { data[8] = value; } get { return data[8]; } }
        internal readonly float[] data = new float[9];
    }

    [StructLayout(LayoutKind.Sequential)]
    public class Matrix44
    {
        public float m00 { set { data[0] = value; } get { return data[0]; } }
        public float m01 { set { data[1] = value; } get { return data[1]; } }
        public float m02 { set { data[2] = value; } get { return data[2]; } }
        public float m03 { set { data[3] = value; } get { return data[3]; } }
        public float m10 { set { data[4] = value; } get { return data[4]; } }
        public float m11 { set { data[5] = value; } get { return data[5]; } }
        public float m12 { set { data[6] = value; } get { return data[6]; } }
        public float m13 { set { data[7] = value; } get { return data[7]; } }
        public float m20 { set { data[8] = value; } get { return data[8]; } }
        public float m21 { set { data[9] = value; } get { return data[9]; } }
        public float m22 { set { data[10] = value; } get { return data[10]; } }
        public float m23 { set { data[11] = value; } get { return data[11]; } }
        public float m30 { set { data[12] = value; } get { return data[12]; } }
        public float m31 { set { data[13] = value; } get { return data[13]; } }
        public float m32 { set { data[14] = value; } get { return data[14]; } }
        public float m33 { set { data[15] = value; } get { return data[15]; } }
        internal readonly float[] data = new float[16];
    }
}
