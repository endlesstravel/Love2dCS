
// author : endlesstravel
// sumary : give we power call love2d with dynamic link library
// modify libs/love/CmakeList.txt
// in line 1427 add 
// 
// src/wrap_love_dll.cpp
// src/wrap_love_dll.h
//  
// and afeter that add
//
// source_group("src" FILES src/wrap_love_dll.cpp src/wrap_love_dll.h)

#ifndef LOVE_LOVE_WRAP_LOVE_DLL_H
#define LOVE_LOVE_WRAP_LOVE_DLL_H

#include "modules/filesystem/physfs/Filesystem.h"
#include "modules/filesystem/physfs/File.h"
#include "modules/filesystem/DroppedFile.h"
#include "modules/sound/lullaby/Sound.h"
#include "modules/sound/Decoder.h"
#include "modules/audio/openal/Audio.h"
#include "modules/font/Font.h"
#include "modules/font/freetype/Font.h"
#include "modules/audio/null/Audio.h"
#include "modules/audio/openal/Source.h"
#include "modules/audio/null/Source.h"
#include "modules/math/BezierCurve.h"
#include "modules/physics/box2d/Physics.h"
#include "common/Memoizer.h"

using namespace love::sound;
using namespace love::audio;
using namespace love::filesystem;
using namespace love::image;
using namespace love::mouse;
using namespace love::font;
using namespace love::video;
using namespace love::graphics;
using namespace love::math;
using namespace love::joystick;

using love::graphics::SpriteBatch;
using love::graphics::Text;
using love::graphics::opengl::Canvas;
using love::graphics::Mesh;
using love::graphics::Shader;


using love::physics::box2d::Physics;
using love::physics::box2d::Body;
using love::physics::box2d::World;
using love::physics::box2d::Contact;
using love::physics::box2d::Fixture;
using love::physics::box2d::Joint;
using love::physics::box2d::ChainShape;
using love::physics::box2d::EdgeShape;
using love::physics::box2d::CircleShape;
using love::physics::box2d::DistanceJoint;
using love::physics::box2d::Shape;
using love::physics::box2d::FrictionJoint;
using love::physics::box2d::GearJoint;
using love::Memoizer;

namespace love
{
namespace wrap
{
    struct BytePtr
    {
        char *data;
    };

	struct WrapString
	{
		char *data;
	};

	WrapString* new_WrapString(const char* str)
	{
        if (str == nullptr)
            return nullptr;

		WrapString* ws = new WrapString();
		size_t len = strlen(str);
		ws->data = new char[len + 1];
		strcpy(ws->data, str);
		return ws;
	}

	WrapString* new_WrapString(const std::string& str)
	{
		return new_WrapString(str.c_str());
	}

	enum Result
	{
		Success,
		Exception,
		IoException
	};

    // 用于传输一系列二维 int
    struct Int2
    {
        int x, y;
    };
    // 用于传输一系列四维 int
    struct Int4
    {
        union
        {
            struct { int x, y, z, w; };
            struct { int r, g, b, a; };
        };
    };
    // 用于传输一系列二维 float
    struct Float2
    {
        union
        {
            struct { float x, y; };
        };
    };
    // 用于传输一系列三维 float
    struct Float3
    {
        union
        {
            struct { float x, y, z; };
        };
    };
    // 用于传输一系列四维 float
    struct Float4
    {
        union
        {
            struct { float x, y, z, w; };
            struct { float r, g, b, a; };
        };
    };

    typedef char* pchar;
    typedef Int2* pInt2;
	typedef int bool4;
    typedef ImageData* pImageData;

    // 用于传输一系列字符串
	struct WrapSequenceString
	{
		int len;
		pchar* sequence;
	};

    WrapSequenceString* new_WrapSequenceString(std::vector<std::string>& lines)
    {
        WrapSequenceString* wss = new WrapSequenceString();
        wss->len = lines.size();
		wss->sequence = new pchar[lines.size()];

        for (int i = 0; i < wss->len; i++)
        {
            char* str = new char[lines[i].size() + 1];
            strcpy(str, lines[i].c_str());
            wss->sequence[i] = str;
        }

        return wss;
    }

	//typedef WrapString* pWrapString;
	//typedef WrapSequence* pWrapSequence;

#pragma region Runtime
	int wrap_ee(const char *fmt, ...);
	template <typename T>
	bool4 wrap_catchexcept(const T& func)
	{
		try
		{
			func();
		}
		catch (const std::exception &e)
		{
			wrap_ee("%s", e.what());
			return false;
		}

		return true;
	}

	template <typename T>
	bool4 wrap_catche_bool(const T& func)
	{
		try
		{
			return func();
		}
		catch (const std::exception &e)
		{
			wrap_ee("%s", e.what());
			return false;
		}
	}
#pragma endregion

#pragma region common region
    extern "C" LOVE_EXPORT void wrap_love_dll_common_getVersion(WrapString **out_str);
    extern "C" LOVE_EXPORT void wrap_love_dll_common_getVersionCodeName(WrapString **out_str);
#pragma endregion

#pragma region delete release region
	extern "C" LOVE_EXPORT void wrap_love_dll_last_error(WrapString **out_errormsg);
    extern "C" LOVE_EXPORT void wrap_love_dll_release_obj(Object *p);
    // extern "C" LOVE_EXPORT void wrap_love_dll_retain_obj(Object *p);
	extern "C" LOVE_EXPORT void wrap_love_dll_delete(void *p);
	extern "C" LOVE_EXPORT void wrap_love_dll_delete_array(void *p);
    extern "C" LOVE_EXPORT void wrap_love_dll_delete_WrapString(WrapString *ws);
    extern "C" LOVE_EXPORT void wrap_love_dll_delete_WrapSequenceString(WrapSequenceString *ws);
#pragma endregion

#pragma region *new* region

    extern "C" LOVE_EXPORT bool4 wrap_love_dll_filesystem_newFile(const char *filename, int fmode, File** out_file);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_filesystem_newFileData_content(const void *contents, int contents_length, const char *filename, FileData** out_filedata);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_filesystem_newFileData_file(File* file, FileData** out_filedata);

    extern "C" LOVE_EXPORT bool4 wrap_love_dll_sound_newDecoder_filedata(FileData* data, int bufferSize, Decoder** out_decoder);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_sound_newDecoder_file(File *file, int bufferSize, Decoder** out_decoder);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_sound_newSoundData_decoder(Decoder *decoder, SoundData** out_soundata);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_sound_newSoundData(int samples, int rate, int bits, int channels, SoundData** out_soundata);

    extern "C" LOVE_EXPORT bool4 wrap_love_dll_audio_newSource_decoder(Decoder *decoder, int type, Source** out_source);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_audio_newSource_sounddata(SoundData *sd, int type, Source** out_source);

    extern "C" LOVE_EXPORT bool4 wrap_love_dll_mouse_newCursor(ImageData *imageData, int hotx, int hoty, Cursor** out_cursor);

    extern "C" LOVE_EXPORT bool4 wrap_love_dll_image_newImageData_wh_data(int w, int h, const unsigned char* data, int dataLength, ImageData** out_imagedata);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_image_newImageData_filedata(FileData *fileData, ImageData** out_imagedata);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_image_newCompressedData(FileData *fileData, CompressedImageData** out_compressedimagedata);
    
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_font_newRasterizer(FileData *fileData, Rasterizer** out_Rasterizer);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_font_newTrueTypeRasterizer(int size, int hinting_type, Rasterizer** out_Rasterizer);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_font_newTrueTypeRasterizer_data(Data* data, int size, int hinting_type, Rasterizer** out_Rasterizer);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_font_newBMFontRasterizer(FileData *fileData, pImageData datas[], int dataLength, Rasterizer** out_Rasterizer);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_font_newImageRasterizer(ImageData *imageData, const char* glyphsStr, int extraspacing, Rasterizer** out_Rasterizer);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_font_newGlyphData_rasterizer_str(Rasterizer* r, const char* glyphStr, GlyphData** out_GlyphData);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_font_newGlyphData_rasterizer_num(Rasterizer* r, int glyphCode, GlyphData** out_GlyphData);

    extern "C" LOVE_EXPORT bool4 wrap_love_dll_newVideoStream(File *file, VideoStream** out_videostream);

    extern "C" LOVE_EXPORT bool4 wrap_love_dll_math_newRandomGenerator(RandomGenerator** out_RandomGenerator);
    extern "C" LOVE_EXPORT void wrap_love_dll_math_newBezierCurve(Float2* pointsList, int pointsList_lenght, BezierCurve** out_BezierCurve);

    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_Canvas_newImageData_xywh(love::graphics::Canvas *canvas, int slice, int mipmap, int x, int y, int w, int h, love::image::ImageData **out_img);
#pragma endregion
    
#pragma region timer

    extern "C" LOVE_EXPORT bool4 wrap_love_dll_timer_open_timer();
    extern "C" LOVE_EXPORT void wrap_love_dll_timer_step();
    extern "C" LOVE_EXPORT void wrap_love_dll_timer_getDelta(float *out_delta);
    extern "C" LOVE_EXPORT void wrap_love_dll_timer_getFPS(float *out_fps);
    extern "C" LOVE_EXPORT void wrap_love_dll_timer_getAverageDelta(float *out_averageDelta);
    extern "C" LOVE_EXPORT void wrap_love_dll_timer_sleep(float t);
    extern "C" LOVE_EXPORT void wrap_love_dll_timer_getTime(float *out_time);

#pragma endregion

#pragma region window
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_windows_open_love_window();
    extern "C" LOVE_EXPORT void wrap_love_dll_windows_getDisplayCount(int *out_count);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_windows_getDisplayName(int displayindex, WrapString** out_name);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_windows_setMode_w_h(int width, int height);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_windows_setMode_w_h_setting(int width, int height, bool4 fullscreen, int fstype, bool4 vsync, int msaa, int depth, bool4 stencil, bool4 resizable, int minwidth, int minheight, bool4 borderless, bool4 centered, int display, bool4 highdpi, double refreshrate, bool4 useposition, int x, int y);
    extern "C" LOVE_EXPORT void wrap_love_dll_windows_getMode(int *out_width, int *out_height, bool4 *out_fullscreen, int *out_fstype, bool4 *out_vsync, int *out_msaa, int *out_depth, bool4 *out_stencil, bool4 *out_resizable, int *out_minwidth, int *out_minheight, bool4 *out_borderless, bool4 *out_centered, int *out_display, bool4 *out_highdpi, double *out_refreshrate, bool4 *out_useposition, int *out_x, int *out_y);
    extern "C" LOVE_EXPORT void wrap_love_dll_windows_getFullscreenModes(int displayindex, Int2*** out_modes, int *out_modes_length);
    extern "C" LOVE_EXPORT void wrap_love_dll_windows_setFullscreen(bool4 fullscreen, int fstype, bool4 *out_success);
    extern "C" LOVE_EXPORT void wrap_love_dll_windows_getFullscreen(bool4 *out_fullscreen, int *out_fstype);
    extern "C" LOVE_EXPORT void wrap_love_dll_windows_isOpen(bool4 *out_isopen);
    extern "C" LOVE_EXPORT void wrap_love_dll_windows_close();
    extern "C" LOVE_EXPORT void wrap_love_dll_windows_getDesktopDimensions(int displayindex, int *out_width, int *out_height);
    extern "C" LOVE_EXPORT void wrap_love_dll_windows_setPosition(int x, int y, int displayindex);
    extern "C" LOVE_EXPORT void wrap_love_dll_windows_getPosition(int *out_x, int *out_y, int *out_displayindex);
    extern "C" LOVE_EXPORT void wrap_love_dll_windows_setIcon(ImageData *i, bool4 *out_success);
    extern "C" LOVE_EXPORT void wrap_love_dll_windows_getIcon(ImageData** out_imagedata);
    extern "C" LOVE_EXPORT void wrap_love_dll_windows_setDisplaySleepEnabled(bool4 enable);
    extern "C" LOVE_EXPORT void wrap_love_dll_windows_isDisplaySleepEnabled(bool4 *out_enable);
    extern "C" LOVE_EXPORT void wrap_love_dll_windows_setTitle(const char *titleStr);
    extern "C" LOVE_EXPORT void wrap_love_dll_windows_getTitle(WrapString** out_title);
    extern "C" LOVE_EXPORT void wrap_love_dll_windows_hasFocus(bool4* out_result);
    extern "C" LOVE_EXPORT void wrap_love_dll_windows_hasMouseFocus(bool4* out_result);
    extern "C" LOVE_EXPORT void wrap_love_dll_windows_isVisible(bool4* out_result);
    extern "C" LOVE_EXPORT void wrap_love_dll_windows_getDPIScale(double *out_result);
    extern "C" LOVE_EXPORT void wrap_love_dll_windows_toPixels(double value, double *out_result);
    extern "C" LOVE_EXPORT void wrap_love_dll_windows_fromPixels(double pixelvalue, double *out_result);
    extern "C" LOVE_EXPORT void wrap_love_dll_windows_minimize();
    extern "C" LOVE_EXPORT void wrap_love_dll_windows_maximize();
    extern "C" LOVE_EXPORT void wrap_love_dll_windows_isMaximized(bool4* out_result);
    extern "C" LOVE_EXPORT void wrap_love_dll_windows_showMessageBox(const char *title, const char *message, int type, bool4 attachToWindow, bool4* out_result);
	extern "C" LOVE_EXPORT void wrap_love_dll_windows_showMessageBox_list(const char *title, const char *message, BytePtr* buttons, int buttonsLength, int enterButtonIndex, int escapebuttonIndex, int type, bool4 attachToWindow, int* out_index_returned);
    extern "C" LOVE_EXPORT void wrap_love_dll_windows_requestAttention(bool4 continuous);
    extern "C" LOVE_EXPORT void wrap_love_dll_windows_windowToPixelCoords(double *out_x, double *out_y);
    extern "C" LOVE_EXPORT void wrap_love_dll_windows_pixelToWindowCoords(double *x, double *y);

#pragma endregion

#pragma region mouse

    extern "C" LOVE_EXPORT bool4 wrap_love_dll_open_love_mouse();
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_mouse_getSystemCursor(int sysctype, Cursor** out_cursor);
    extern "C" LOVE_EXPORT void wrap_love_dll_mouse_setCursor(Cursor *cursor);
    extern "C" LOVE_EXPORT void wrap_love_dll_mouse_getCursor(Cursor** out_cursor);
    extern "C" LOVE_EXPORT void wrap_love_dll_mouse_isCursorSupported(bool4 *out_result);
    extern "C" LOVE_EXPORT void wrap_love_dll_mouse_getX(double *out_x);
    extern "C" LOVE_EXPORT void wrap_love_dll_mouse_getY(double *out_y);
    extern "C" LOVE_EXPORT void wrap_love_dll_mouse_getPosition(double *out_x, double *out_y);
    extern "C" LOVE_EXPORT void wrap_love_dll_mouse_setX(double x);
    extern "C" LOVE_EXPORT void wrap_love_dll_mouse_setY(double y);
    extern "C" LOVE_EXPORT void wrap_love_dll_mouse_setPosition(double x, double y);
    extern "C" LOVE_EXPORT void wrap_love_dll_mouse_isDown(int buttonIndex, bool4 *out_result);
    extern "C" LOVE_EXPORT void wrap_love_dll_mouse_setVisible(bool4 visible);
    extern "C" LOVE_EXPORT void wrap_love_dll_mouse_isVisible(bool4 *out_result);
    extern "C" LOVE_EXPORT void wrap_love_dll_mouse_setGrabbed(bool4 grabbed);
    extern "C" LOVE_EXPORT void wrap_love_dll_mouse_isGrabbed(bool4 *out_result);
    extern "C" LOVE_EXPORT void wrap_love_dll_mouse_setRelativeMode(bool4 enable, bool4 *out_result);
    extern "C" LOVE_EXPORT void wrap_love_dll_mouse_getRelativeMode(bool4 *out_result);

#pragma endregion

#pragma region keyboard 

    extern "C" LOVE_EXPORT bool4 wrap_love_dll_keyboard_open_love_keyboard();
    extern "C" LOVE_EXPORT void wrap_love_dll_keyboard_setKeyRepeat(bool4 enable);
    extern "C" LOVE_EXPORT void wrap_love_dll_keyboard_hasKeyRepeat(bool4 *out_result);
    extern "C" LOVE_EXPORT void wrap_love_dll_keyboard_isDown(int key_type, bool4 *out_result);
    extern "C" LOVE_EXPORT void wrap_love_dll_keyboard_isScancodeDown(int scancode_type, bool4 *out_result);
    extern "C" LOVE_EXPORT void wrap_love_dll_keyboard_getScancodeFromKey(int key_type, int *out_scancode_type);
    extern "C" LOVE_EXPORT void wrap_love_dll_keyboard_getKeyFromScancode(int scancode_type, int *out_key_type);
    extern "C" LOVE_EXPORT void wrap_love_dll_keyboard_setTextInput(bool4 enable);
    extern "C" LOVE_EXPORT void wrap_love_dll_keyboard_setTextInput_xywh(bool4 enable, double x, double y, double w, double h);
    extern "C" LOVE_EXPORT void wrap_love_dll_keyboard_hasTextInput(bool4 *out_result);
    extern "C" LOVE_EXPORT void wrap_love_dll_keyboard_hasScreenKeyboard(bool4 *out_result);

#pragma endregion

#pragma region touch
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_touch_open_love_touch();
    extern "C" LOVE_EXPORT void wrap_love_dll_touch_open_love_getTouches(int64 **out_indexs, double **out_x, double **out_y, double **out_dx, double **out_dy, double **out_pressure, int *out_length);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_touch_open_love_getToucheInfo(int64 idx, double *out_x, double *out_y, double *out_dx, double *out_dy, double *out_pressure);

#pragma endregion

#pragma region joystick

    extern "C" LOVE_EXPORT bool4 wrap_love_dll_joystick_open_love_joystick();
    extern "C" LOVE_EXPORT void wrap_love_dll_joystick_getJoysticks(Joystick ***out_sticks, int *out_sticks_lenght);
    extern "C" LOVE_EXPORT void wrap_love_dll_joystick_getIndex(Joystick *j, int *out_index);
    extern "C" LOVE_EXPORT void wrap_love_dll_joystick_getJoystickCount(int *out_sticks_lenght);
    extern "C" LOVE_EXPORT void wrap_love_dll_joystick_setGamepadMapping(const char* guid, int gp_inputType_type, int j_inputType_type, int inputIndex, int hat_type, bool4 *out_success);
    extern "C" LOVE_EXPORT void wrap_love_dll_joystick_loadGamepadMappings(const char *str, bool4 *out_success);
    extern "C" LOVE_EXPORT void wrap_love_dll_joystick_saveGamepadMappings(WrapString **out_str);

#pragma endregion

#pragma region event

    extern "C" LOVE_EXPORT bool4 wrap_love_dll_event_open_love_event();
    extern "C" LOVE_EXPORT void wrap_love_dll_event_poll(bool4 *out_hasEvent, int *out_event_type, bool4 *out_down_or_up, bool4 *out_bool, int *out_idx, int *out_enum1_type, int *out_enum2_type, WrapString** out_str, Int4* out_int4, Float4 *out_float4, float *out_float_value, Joystick **out_joystick);
    extern "C" LOVE_EXPORT void wrap_love_dll_event_wait(bool4 *out_hasEvent, int *out_event_type, bool4 *out_down_or_up, bool4 *out_bool, int *out_idx, int *out_enum1_type, int *out_enum2_type, WrapString** out_str, Int4* out_int4, Float4 *out_float4, float *out_float_value, Joystick **out_joystick);

#pragma endregion

#pragma region filesystem

    extern "C" LOVE_EXPORT bool4 wrap_love_dll_filesystem_open_love_filesystem();
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_filesystem_init(const char* arg0);
	extern "C" LOVE_EXPORT void wrap_love_dll_filesystem_getInfo(const char *arg, int* out_filetype, int64* out_size, int64 *out_modtime, bool4 *out_result);
    extern "C" LOVE_EXPORT void wrap_love_dll_filesystem_setFused(bool4 flag);
    extern "C" LOVE_EXPORT void wrap_love_dll_filesystem_isFused(bool4 *out_result);
    extern "C" LOVE_EXPORT void wrap_love_dll_filesystem_setAndroidSaveExternal(bool4 useExternal);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_filesystem_setIdentity(const char *arg, bool4 append);
    extern "C" LOVE_EXPORT void wrap_love_dll_filesystem_getIdentity(WrapString** out_str);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_filesystem_setSource(const char *arg);
    extern "C" LOVE_EXPORT void wrap_love_dll_filesystem_getSource(WrapString** out_str);
    extern "C" LOVE_EXPORT void wrap_love_dll_filesystem_mount(const char *archive, const char *mountpoint, bool4 appendToPath, bool4 *out_result);
    extern "C" LOVE_EXPORT void wrap_love_dll_filesystem_unmount(const char *archive, bool4 *out_result);
    extern "C" LOVE_EXPORT void wrap_love_dll_filesystem_getWorkingDirectory(WrapString** out_str);
    extern "C" LOVE_EXPORT void wrap_love_dll_filesystem_getUserDirectory(WrapString** out_str);
    extern "C" LOVE_EXPORT void wrap_love_dll_filesystem_getAppdataDirectory(WrapString** out_str);
    extern "C" LOVE_EXPORT void wrap_love_dll_filesystem_getSaveDirectory(WrapString** out_str);
    extern "C" LOVE_EXPORT void wrap_love_dll_filesystem_getSourceBaseDirectory(WrapString** out_str);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_filesystem_getRealDirectory(const char *filename, WrapString** out_str);
    extern "C" LOVE_EXPORT void wrap_love_dll_filesystem_getExecutablePath(WrapString** out_str);
    extern "C" LOVE_EXPORT void wrap_love_dll_filesystem_createDirectory(const char *arg, bool4 *out_result);
    extern "C" LOVE_EXPORT void wrap_love_dll_filesystem_remove(const char *arg, bool4 *out_result);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_filesystem_read(const char *filename, int64 len, char **out_data, uint32 *out_data_length);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_filesystem_write(const char *filename, const void* input, size_t len);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_filesystem_append(const char *filename, const void* input, size_t len);
    extern "C" LOVE_EXPORT void wrap_love_dll_filesystem_getDirectoryItems(const char *dir, WrapSequenceString** out_wss);
    extern "C" LOVE_EXPORT void wrap_love_dll_filesystem_setSymlinksEnabled(bool4 enable);
    extern "C" LOVE_EXPORT void wrap_love_dll_filesystem_areSymlinksEnabled(bool4 *out_result);
    extern "C" LOVE_EXPORT void wrap_love_dll_filesystem_getRequirePath(WrapString** out_str);
    extern "C" LOVE_EXPORT void wrap_love_dll_filesystem_setRequirePath(const char* paths);
    extern "C" LOVE_EXPORT void wrap_love_dll_filesystem_ext_allowMountingForPath(const char* pathStr);
    extern "C" LOVE_EXPORT void wrap_love_dll_filesystem_ext_isRealDirectory(const char* pathStr, bool4 *out_result);

#pragma endregion

#pragma region sound
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_sound_luaopen_love_sound();
#pragma endregion

#pragma region audio

	extern "C" LOVE_EXPORT bool4 wrap_love_dll_audio_open_love_audio();
    extern "C" LOVE_EXPORT void wrap_love_dll_audio_getActiveSourceCount(int *out_reslut);
    extern "C" LOVE_EXPORT void wrap_love_dll_audio_stop();
    extern "C" LOVE_EXPORT void wrap_love_dll_audio_pause();
    extern "C" LOVE_EXPORT void wrap_love_dll_audio_play(Source** source_array, int source_array_length);
    extern "C" LOVE_EXPORT void wrap_love_dll_audio_setVolume(float v);
    extern "C" LOVE_EXPORT void wrap_love_dll_audio_getVolume(float *out_volume);
    extern "C" LOVE_EXPORT void wrap_love_dll_audio_setPosition(float x, float y, float z);
    extern "C" LOVE_EXPORT void wrap_love_dll_audio_getPosition(float *out_x, float *out_y, float *out_z);
    extern "C" LOVE_EXPORT void wrap_love_dll_audio_setOrientation(float x, float y, float z, float dx, float dy, float dz);
    extern "C" LOVE_EXPORT void wrap_love_dll_audio_getOrientation(float *out_x, float *out_y, float *out_z, float *out_dx, float *out_dy, float *out_dz);
    extern "C" LOVE_EXPORT void wrap_love_dll_audio_setVelocity(float x, float y, float z);
    extern "C" LOVE_EXPORT void wrap_love_dll_audio_getVelocity(float *out_x, float *out_y, float *out_z);
    extern "C" LOVE_EXPORT void wrap_love_dll_audio_setDopplerScale(float scale);
    extern "C" LOVE_EXPORT void wrap_love_dll_audio_getDopplerScale(float *out_scale);
    extern "C" LOVE_EXPORT void wrap_love_dll_audio_setDistanceModel(int distancemodel_type);
    extern "C" LOVE_EXPORT void wrap_love_dll_audio_getDistanceModel(int *out_distancemodel_type);


#pragma endregion

#pragma region image

    extern "C" LOVE_EXPORT bool4 wrap_love_dll_image_open_love_image();
    extern "C" LOVE_EXPORT void wrap_love_dll_image_isCompressed(FileData *data, bool4 *out_result);


#pragma endregion

#pragma region font

    extern "C" LOVE_EXPORT bool4 wrap_love_dll_font_open_love_font();

#pragma endregion

#pragma region video

    extern "C" LOVE_EXPORT bool4 wrap_love_dll_video_open_love_video();

#pragma endregion

#pragma region math

    extern "C" LOVE_EXPORT void wrap_love_dll_open_love_math();
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_math_triangulate(Float2* pointsList, int pointsList_lenght, float **out_triArray, int *out_triCount);
    extern "C" LOVE_EXPORT void wrap_love_dll_math_isConvex(Float2* pointsList, int pointsList_lenght, bool4 *out_result);
    extern "C" LOVE_EXPORT void wrap_love_dll_math_gammaToLinear(float gama, float *out_liner);
    extern "C" LOVE_EXPORT void wrap_love_dll_math_linearToGamma(float liner, float *out_gama);
    extern "C" LOVE_EXPORT void wrap_love_dll_math_noise_1(float x, float *out_result);
    extern "C" LOVE_EXPORT void wrap_love_dll_math_noise_2(float x, float y, float *out_result);
    extern "C" LOVE_EXPORT void wrap_love_dll_math_noise_3(float x, float y, float z, float *out_result);
    extern "C" LOVE_EXPORT void wrap_love_dll_math_noise_4(float x, float y, float z, float w, float *out_result);


#pragma endregion



#pragma region graphics Object Creation

    extern "C" LOVE_EXPORT bool4 wrap_love_dll_graphics_open_love_graphics();
	extern "C" LOVE_EXPORT bool4 wrap_love_dll_graphics_newImage_data(ImageDataBase **imageDataList, bool4* compressedTypeList, int imageDataListLength, bool4 flagMipmaps, bool4 flagLinear, love::graphics::Image** out_image);
    extern "C" LOVE_EXPORT void wrap_love_dll_graphics_newQuad(double x, double y, double w, double h, double sw, double sh, Quad** out_quad);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_graphics_newFont(Rasterizer *rasterizer, love::graphics::Font** out_font);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_graphics_newSpriteBatch(Texture *texture, int maxSprites, int usage_type, love::graphics::SpriteBatch** out_spriteBatch);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_graphics_newParticleSystem(Texture *texture, int buffer, ParticleSystem** out_particleSystem);
	extern "C" LOVE_EXPORT bool4 wrap_love_dll_graphics_newCanvas(int width, int height, int texture_type, int format_type, bool4 readable, int msaa, float dpiscale, int mipmaps, love::graphics::Canvas** out_canvas);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_graphics_newShader(const char* vertexCodeStr, const char* pixelCodeStr, Shader** out_shader);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_graphics_newMesh_specifiedVertices(Float2 pos[], Float2 uv[], Float4 color[], int vertexCount, int drawMode_type, int usage_type, Mesh** out_mesh);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_graphics_newMesh_count(int count, int drawMode_type, int usage_type, Mesh** out_mesh);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_graphics_newText(love::graphics::Font *font, BytePtr coloredStringText[], Float4 coloredStringColor[], int coloredStringLength, Text** out_text);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_graphics_newVideo(VideoStream *videoStream, float dpiScale, graphics::Video** out_video);



#pragma endregion

#pragma region graphics State

    extern "C" LOVE_EXPORT void wrap_love_dll_graphics_reset();
    extern "C" LOVE_EXPORT void wrap_love_dll_graphics_isActive(bool4 *out_result);
    extern "C" LOVE_EXPORT void wrap_love_dll_graphics_isGammaCorrect(bool4 *out_result);
    extern "C" LOVE_EXPORT void wrap_love_dll_graphics_setScissor();
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_graphics_setScissor_xywh(int x, int y, int w, int h);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_graphics_intersectScissor(int x, int y, int w, int h);
    extern "C" LOVE_EXPORT void wrap_love_dll_graphics_getScissor(int *out_x, int *out_y, int *out_w, int *out_h);
    extern "C" LOVE_EXPORT void wrap_love_dll_graphics_setStencilTest(int compare_type, int compareValue);
    extern "C" LOVE_EXPORT void wrap_love_dll_graphics_getStencilTest(int *out_compare_type, int *out_compareValue);
    extern "C" LOVE_EXPORT void wrap_love_dll_graphics_setColor(float r, float g, float b, float a);
    extern "C" LOVE_EXPORT void wrap_love_dll_graphics_getColor(float *out_r, float *out_g, float *out_b, float *out_a);
    extern "C" LOVE_EXPORT void wrap_love_dll_graphics_setBackgroundColor(int r, int g, int b, int a);
    extern "C" LOVE_EXPORT void wrap_love_dll_graphics_getBackgroundColor(int *out_r, int *out_g, int *out_b, int *out_a);
    extern "C" LOVE_EXPORT void wrap_love_dll_graphics_setFont(love::graphics::Font *font);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_graphics_getFont(love::graphics::Font** out_font);
    extern "C" LOVE_EXPORT void wrap_love_dll_graphics_setColorMask(bool4 r, bool4 g, bool4 b, bool4 a);
    extern "C" LOVE_EXPORT void wrap_love_dll_graphics_getColorMask(bool4 *out_r, bool4 *out_g, bool4 *out_b, bool4 *out_a);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_graphics_setBlendMode(int blendMode_type, int blendAlphaMode_type);
    extern "C" LOVE_EXPORT void wrap_love_dll_graphics_getBlendMode(int *out_blendMode_type, int *out_blendAlphaMode_type);
    extern "C" LOVE_EXPORT void wrap_love_dll_graphics_setDefaultFilter(int filterModeMagMin_type, int filterModeMag_type, int anisotropy);
    extern "C" LOVE_EXPORT void wrap_love_dll_graphics_getDefaultFilter(int *out_filterModeMagMin_type, int *out_filterModeMag_type, int *out_anisotropy);
    extern "C" LOVE_EXPORT void wrap_love_dll_graphics_setLineWidth(float width);
    extern "C" LOVE_EXPORT void wrap_love_dll_graphics_setLineStyle(int style_type);
    extern "C" LOVE_EXPORT void wrap_love_dll_graphics_setLineJoin(int join_type);
    extern "C" LOVE_EXPORT void wrap_love_dll_graphics_getLineWidth(float *out_result);
    extern "C" LOVE_EXPORT void wrap_love_dll_graphics_getLineStyle(int *out_lineStyle_type);
    extern "C" LOVE_EXPORT void wrap_love_dll_graphics_getLineJoin(int *out_lineJoinStyle_type);
    extern "C" LOVE_EXPORT void wrap_love_dll_graphics_setPointSize(float size);
    extern "C" LOVE_EXPORT void wrap_love_dll_graphics_getPointSize(float *out_size);
    extern "C" LOVE_EXPORT void wrap_love_dll_graphics_setWireframe(bool4 enable);
    extern "C" LOVE_EXPORT void wrap_love_dll_graphics_isWireframe(bool4 *out_isWireFrame);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_graphics_setCanvas(love::graphics::Canvas** canvaList, int canvaListLength);
    extern "C" LOVE_EXPORT void wrap_love_dll_graphics_getCanvas(love::graphics::Canvas*** out_canvas, int *out_canvas_lenght);
    extern "C" LOVE_EXPORT void wrap_love_dll_graphics_setShader(Shader *shader);
    extern "C" LOVE_EXPORT void wrap_love_dll_graphics_getShader(Shader** out_shader);
    extern "C" LOVE_EXPORT void wrap_love_dll_graphics_setDefaultShaderCode(const char **strPtr);

#pragma endregion

#pragma region graphics Coordinate System

    extern "C" LOVE_EXPORT void wrap_love_dll_graphics_push(int stack_type);
    extern "C" LOVE_EXPORT void wrap_love_dll_graphics_pop();
    extern "C" LOVE_EXPORT void wrap_love_dll_graphics_rotate(float angle);
    extern "C" LOVE_EXPORT void wrap_love_dll_graphics_scale(float sx, float sy);
    extern "C" LOVE_EXPORT void wrap_love_dll_graphics_translate(float x, float y);
    extern "C" LOVE_EXPORT void wrap_love_dll_graphics_shear(float kx, float ky);
    extern "C" LOVE_EXPORT void wrap_love_dll_graphics_origin();

#pragma endregion

#pragma region graphics drawing

    extern "C" LOVE_EXPORT void wrap_love_dll_graphics_ext_stencil_drawToStencilBuffer(int action_type, int stencilValue);
    extern "C" LOVE_EXPORT void wrap_love_dll_graphics_ext_stencil_stopDrawToStencilBuffer();

    extern "C" LOVE_EXPORT bool4 wrap_love_dll_graphics_clear_rgba(float r, float g, float b, float a, float stencil, bool4 enable_stencil, float depth, bool4 enable_depth);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_graphics_clear_rgbalist(Float4 colorList[], bool4 enableList[], int listLength, float stencil, bool4 enable_stencil, float depth, bool4 enable_depth);
    extern "C" LOVE_EXPORT void wrap_love_dll_graphics_discard(bool4 discardColors[], int discardColorsLength, bool4 discardStencil);
    extern "C" LOVE_EXPORT void wrap_love_dll_graphics_present();
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_graphics_draw_drawable(Drawable *drawable, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_graphics_draw_texture_quad(Quad *quad, Texture *texture, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_graphics_print(BytePtr coloredStringListStr[], Float4 coloredStringListColor[], int coloredStringListLength, float x, float y, float angle, float sx, float sy, float ox, float oy, float kx, float ky);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_graphics_printf(BytePtr coloredStringListStr[], Float4 coloredStringListColor[], int coloredStringListLength, float x, float y, float wrap, int align_type, float angle, float sx, float sy, float ox, float oy, float kx, float ky);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_graphics_rectangle(int mode_type, float x, float y, float w, float h);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_graphics_rectangle_with_rounded_corners(int mode_type, float x, float y, float w, float h, float rx, float ry, int points);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_graphics_circle(int mode_type, float x, float y, float radius, int points);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_graphics_ellipse(int mode_type, float x, float y, float a, float b, int points);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_graphics_arc(int mode_type, int arcmode_type, float x, float y, float radius, float angle1, float angle2);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_graphics_arc_points(int mode_type, int arcmode_type, float x, float y, float radius, float angle1, float angle2, int points);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_graphics_points(Float2 coords[], int coordsLength);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_graphics_points_colors(Float2 coords[], Int4 colors[], int coordsLength);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_graphics_line(Float2 coords[], int coordsLength);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_graphics_polygon(int mode_type, Float2 coords[], int coordsLength);

#pragma endregion

#pragma region graphics Window
    extern "C" LOVE_EXPORT void wrap_love_dll_graphics_isCreated(bool4 *out_reslut);
	extern "C" LOVE_EXPORT void wrap_love_dll_graphics_getDPIScale(double *out_dpiscale);
	extern "C" LOVE_EXPORT void wrap_love_dll_graphics_getWidth(int *out_width);
    extern "C" LOVE_EXPORT void wrap_love_dll_graphics_getHeight(int *out_height);
#pragma endregion

#pragma region graphics System Information
    extern "C" LOVE_EXPORT void wrap_love_dll_graphics_getSupported(int feature_type, bool4 *out_result);
    extern "C" LOVE_EXPORT void wrap_love_dll_graphics_getCanvasFormats(int format_type, bool4 *out_result);
    extern "C" LOVE_EXPORT void wrap_love_dll_graphics_getImageFormats(int format_type, bool4 *out_result);
    extern "C" LOVE_EXPORT void wrap_love_dll_graphics_getRendererInfo(WrapSequenceString** out_wss);
    extern "C" LOVE_EXPORT void wrap_love_dll_graphics_getSystemLimits(int limit_type, double *out_result);
    extern "C" LOVE_EXPORT void wrap_love_dll_graphics_getStats(int *out_drawCalls, int *out_canvasSwitches, int *out_shaderSwitches, int *out_canvases, int *out_images, int *out_fonts, int *out_textureMemory);
#pragma endregion


#pragma region type - Source
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_Source_clone(Source *t, Source **out_clone);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Source_play(Source *t, bool4 *out_success);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Source_stop(Source *t);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Source_pause(Source *t);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Source_resume(Source *t);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_Source_setPitch(Source *t, float pitch);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Source_getPitch(Source *t, float *out_pitch);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Source_setVolume(Source *t, float volume);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Source_getVolume(Source *t, float *out_volume);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Source_seek(Source *t, float offset, int unit_type);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Source_tell(Source *t, int unit_type, float *out_position);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Source_getDuration(Source *t, int unit_type, float *out_duration);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_Source_setPosition(Source *t, float x, float y, float z);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_Source_getPosition(Source *t, float *out_x, float *out_y, float *out_z);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_Source_setVelocity(Source *t, float x, float y, float z);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_Source_getVelocity(Source *t, float *out_x, float *out_y, float *out_z);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_Source_setDirection(Source *t, float x, float y, float z);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_Source_getDirection(Source *t, float *out_x, float *out_y, float *out_z);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_Source_setCone(Source *t, float innerAngle, float outerAngle, float outerVolume);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_Source_getCone(Source *t, float *out_innerAngle, float *out_outerAngle, float *out_outerVolume);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_Source_setRelative(Source *t, bool4 relative);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_Source_isRelative(Source *t, bool4 *out_relative);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Source_setLooping(Source *t, bool4 looping);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Source_isLooping(Source *t, bool4 *out_result);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Source_isPlaying(Source *t, bool4 *out_result);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_Source_setVolumeLimits(Source *t, float vmin, float vmax);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Source_getVolumeLimits(Source *t, float *out_vmin, float *out_vmax);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_Source_setAttenuationDistances(Source *t, float dref, float dmax);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_Source_getAttenuationDistances(Source *t, float *out_dref, float *out_dmax);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_Source_setRolloff(Source *t, float rolloff);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_Source_getRolloff(Source *t, float *out_rolloff);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Source_getChannelCount(Source *t, int *out_chanels);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Source_getType(Source *t, int *out_type);

#pragma endregion

#pragma region type - File

    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_File_getSize(File *file, double *out_size);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_File_open(File *file, int mode_type);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_File_close(File *file, bool4 *out_result);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_File_isOpen(File *file, bool4 *out_result);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_File_read(File *file, int64 size, void **out_data, int64 *out_readedSize);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_File_write_String(File *file, const char *data, int64 datasize);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_File_write_Data_datasize(File *file, Data *data, int64 datasize);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_File_flush(File *file);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_File_isEOF(File *file, bool4 *out_result);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_File_tell(File *file, int64 *out_pos);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_File_seek(File *file, int64 pos);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_File_setBuffer(File *file, int bufmode_type, int64 size);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_File_getBuffer(File *file, int *out_bufmode_type, int64 *out_size);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_File_getMode(File *file, int *out_mode_type);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_File_getFilename(File *file, WrapString **out_filename);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_File_getExtension(File *file, WrapString **out_extension);


#pragma endregion

#pragma region type - FileData
    extern "C" LOVE_EXPORT void wrap_love_dll_type_FileData_getFilename(FileData *t, WrapString **out_filename);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_FileData_getExtension(FileData *t, WrapString **out_extension);
#pragma endregion

#pragma region type - GlyphData

    extern "C" LOVE_EXPORT void wrap_love_dll_type_GlyphData_getWidth(GlyphData *t, int *out_width);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_GlyphData_getHeight(GlyphData *t, int *out_height);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_GlyphData_getGlyph(GlyphData *t, uint32 *out_glyph);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_GlyphData_getGlyphString(GlyphData *t, WrapString **out_str);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_GlyphData_getAdvance(GlyphData *t, int *out_advance);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_GlyphData_getBearing(GlyphData *t, int *out_bearingX, int *out_bearingY);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_GlyphData_getBoundingBox(GlyphData *t, int *out_minX, int *out_minY, int *out_width, int *out_height);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_GlyphData_getFormat(GlyphData *t, int *out_format_type);

#pragma endregion

#pragma region type - Rasterizer

    extern "C" LOVE_EXPORT void wrap_love_dll_type_Rasterizer_getHeight(Rasterizer *t, int *out_heigth);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Rasterizer_getAdvance(Rasterizer *t, int *out_advance);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Rasterizer_getAscent(Rasterizer *t, int *out_ascent);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Rasterizer_getDescent(Rasterizer *t, int *out_descent);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Rasterizer_getLineHeight(Rasterizer *t, int *out_lineHeight);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_Rasterizer_getGlyphData_uint32(Rasterizer *t, uint32 glyph, GlyphData **out_g);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_Rasterizer_getGlyphData_string(Rasterizer *t, const char *str, GlyphData **out_g);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Rasterizer_getGlyphCount(Rasterizer *t, int *out_count);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_Rasterizer_hasGlyphs_uint32(Rasterizer *t, uint32 glyph, bool4 *out_result);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_Rasterizer_hasGlyphs_string(Rasterizer *t, const char *str, bool4 *out_result);

#pragma endregion

#pragma region type - Canvas

    extern "C" LOVE_EXPORT void wrap_love_dll_type_Canvas_getFormat(love::graphics::Canvas *canvas, int *out_format_type);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Canvas_getMSAA(love::graphics::Canvas *canvas, int *out_msaa);

#pragma endregion

#pragma region type - Font

    extern "C" LOVE_EXPORT void wrap_love_dll_type_Font_getHeight(love::graphics::Font *t, int *out_height);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_Font_getWidth(love::graphics::Font *t, const char *str, int *out_width);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_Font_getWrap(love::graphics::Font *t,
        BytePtr coloredStringText[], Float4 coloredStringColor[], int coloredStringLength, float wrap,
        int *out_maxWidth, WrapSequenceString **out_pws);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Font_setLineHeight(love::graphics::Font *t, float h);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Font_getLineHeight(love::graphics::Font *t, float *out_h);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_Font_setFilter(love::graphics::Font *t, int min_type, int mag_type, float anisotropy);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Font_getFilter(love::graphics::Font *t, int *out_min_type, int *out_mag_type, float *out_anisotropy);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Font_getAscent(love::graphics::Font *t, int *out_ascent);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Font_getDescent(love::graphics::Font *t, int *out_descent);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Font_getBaseline(love::graphics::Font *t, float*out_baseline);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_Font_hasGlyphs_uint32(love::graphics::Font *t, uint32 chr, bool4 *out_result);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_Font_hasGlyphs_string(love::graphics::Font *t, const char *str, bool4 *out_result);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_Font_setFallbacks(love::graphics::Font *t, love::graphics::Font **fallback, int length);


#pragma endregion

#pragma region type - Image
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_Image_setMipmapFilter(love::graphics::opengl::Image *i, int mipmap_type, float sharpness);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Image_getMipmapFilter(love::graphics::opengl::Image *i, int *out_mipmap_type, float *out_sharpness);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Image_isCompressed(love::graphics::opengl::Image *i, bool4 *out_result);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_Image_replacePixels(love::graphics::opengl::Image *i, ImageData *imgData, int slice, int mipmap, int x, int y, bool4 reloadmipmaps);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Image_getFlags(love::graphics::opengl::Image* i, bool4 *out_mipmaps, bool4 *out_linear);

#pragma endregion

#pragma region type - Mesh
    using love::graphics::Mesh;
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_Mesh_setVertices(Mesh *t, int vertoffset, Float2 pos[], Float2 uv[], Float4 color[], int vertexCount);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_Mesh_setVertex(Mesh *t, int index, Float2 pos, Float2 uv, Float4 color);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_Mesh_getVertex(Mesh *t, int index, Float2* out_pos, Float2* out_uv, Float4* out_color);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Mesh_getVertexCount(Mesh *t, int *out_result);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Mesh_flush(Mesh *t);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_Mesh_setVertexMap_nil(Mesh *t);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_Mesh_setVertexMap(Mesh *t, uint32 *vertexmaps, int vertexmaps_length);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_Mesh_getVertexMap(Mesh *t, bool4 *out_has_vertex_map, uint32 **out_vertexmaps, int *out_vertexmaps_length);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Mesh_setTexture_nil(Mesh *t);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Mesh_setTexture_Texture(Mesh *t, Texture *tex);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_Mesh_getTexture(Mesh *t, Texture **out_result);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Mesh_setDrawMode(Mesh *t, int mode_type);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Mesh_getDrawMode(Mesh *t, int *out_mode_type);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Mesh_setDrawRange(Mesh *t);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_Mesh_setDrawRange_minmax(Mesh *t, int rangemin, int rangemax);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_Mesh_getDrawRange(Mesh *t, int *out_rangemin, int *out_rangemax);

#pragma endregion

#pragma region type - ParticleSystem

    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_ParticleSystem_clone(ParticleSystem *t, ParticleSystem **out_clone);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ParticleSystem_setTexture(ParticleSystem *t, Texture *tex);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_ParticleSystem_getTexture(ParticleSystem *t, Texture **out_texture);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_ParticleSystem_setBufferSize(ParticleSystem *t, uint32 buffersize);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ParticleSystem_getBufferSize(ParticleSystem *t, uint32 *out_buffersize);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ParticleSystem_setInsertMode(ParticleSystem *t, int mode_type);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ParticleSystem_getInsertMode(ParticleSystem *t, int *out_mode_type);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_ParticleSystem_setEmissionRate(ParticleSystem *t, float rate);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ParticleSystem_getEmissionRate(ParticleSystem *t, float *out_rate);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ParticleSystem_setEmitterLifetime(ParticleSystem *t, float lifetime);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ParticleSystem_getEmitterLifetime(ParticleSystem *t, float *out_lifetime);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ParticleSystem_setParticleLifetime(ParticleSystem *t, float min, float max);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ParticleSystem_getParticleLifetime(ParticleSystem *t, int *out_min, int *out_max);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ParticleSystem_setPosition(ParticleSystem *t, float x, float y);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ParticleSystem_getPosition(ParticleSystem *t, float *out_x, float *out_y);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ParticleSystem_moveTo(ParticleSystem *t, float x, float y);
	extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_ParticleSystem_setEmissionArea(ParticleSystem *t, int distribution_type, float x, float y, float angle, bool4 directionRelativeToCenter);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ParticleSystem_getAreaSpread(ParticleSystem *t, int *out_distribution_type, float *out_x, float *out_y);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ParticleSystem_setDirection(ParticleSystem *t, float direction);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ParticleSystem_getDirection(ParticleSystem *t, float *out_direction);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ParticleSystem_setSpread(ParticleSystem *t, float spread);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ParticleSystem_getSpread(ParticleSystem *t, float *out_spread);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ParticleSystem_setSpeed(ParticleSystem *t, float min, float max);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ParticleSystem_getSpeed(ParticleSystem *t, float *out_min, float *out_max);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ParticleSystem_setLinearAcceleration(ParticleSystem *t, float xmin, float ymin, float xmax, float ymax);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ParticleSystem_getLinearAcceleration(ParticleSystem *t, float *out_xmin, float *out_ymin, float *out_xmax, float *out_ymax);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ParticleSystem_setRadialAcceleration(ParticleSystem *t, float min, float max);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ParticleSystem_getRadialAcceleration(ParticleSystem *t, int *out_min, int *out_max);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ParticleSystem_setTangentialAcceleration(ParticleSystem *t, float min, float max);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ParticleSystem_getTangentialAcceleration(ParticleSystem *t, int *out_min, int *out_max);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ParticleSystem_setLinearDamping(ParticleSystem *t, float min, float max);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ParticleSystem_getLinearDamping(ParticleSystem *t, int *out_min, int *out_max);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_ParticleSystem_setSizes(ParticleSystem *t, float *sizearray, int sizearray_length);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ParticleSystem_getSizes(ParticleSystem *t, float **out_sizearray, int *out_sizearray_length);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_ParticleSystem_setSizeVariation(ParticleSystem *t, float variation);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ParticleSystem_getSizeVariation(ParticleSystem *t, float *out_variation);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ParticleSystem_setRotation(ParticleSystem *t, float min, float max);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ParticleSystem_getRotation(ParticleSystem *t, int *out_min, int *out_max);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ParticleSystem_setSpin(ParticleSystem *t, float start, float end);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ParticleSystem_getSpin(ParticleSystem *t, float *out_start, float *out_end);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ParticleSystem_setSpinVariation(ParticleSystem *t, float variation);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ParticleSystem_getSpinVariation(ParticleSystem *t, float *out_variation);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ParticleSystem_setOffset(ParticleSystem *t, float x, float y);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ParticleSystem_getOffset(ParticleSystem *t, float *out_x, float *out_y);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_ParticleSystem_setColors(ParticleSystem *t, Int4 *colorarray, int colorarray_length);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ParticleSystem_getColors(ParticleSystem *t, Int4 **out_colorarray, int *out_colorarray_length);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ParticleSystem_setQuads(ParticleSystem *t, Quad** quadsarray, int quadsarray_length);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ParticleSystem_getQuads(ParticleSystem *t, Quad ***out_quadsarray, int *out_quadsarray_length);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ParticleSystem_setRelativeRotation(ParticleSystem *t, bool4 enable);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ParticleSystem_hasRelativeRotation(ParticleSystem *t, bool4 *out_enable);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ParticleSystem_getCount(ParticleSystem *t, uint32 *out_count);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ParticleSystem_start(ParticleSystem *t);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ParticleSystem_stop(ParticleSystem *t);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ParticleSystem_pause(ParticleSystem *t);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ParticleSystem_reset(ParticleSystem *t);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ParticleSystem_emit(ParticleSystem *t, int num);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ParticleSystem_isActive(ParticleSystem *t, bool4 *out_result);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ParticleSystem_isPaused(ParticleSystem *t, bool4 *out_result);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ParticleSystem_isStopped(ParticleSystem *t, bool4 *out_result);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ParticleSystem_update(ParticleSystem *t, float dt);


#pragma endregion

#pragma region type - Quad

    extern "C" LOVE_EXPORT void wrap_love_dll_type_Quad_setViewport(Quad *quad, float x, float y, float w, float h);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Quad_getViewport(Quad *quad, float *out_x, float *out_y, float *out_w, float *out_h);
    //extern "C" LOVE_EXPORT void wrap_love_dll_type_Quad_getTextureDimensions(Quad *quad, double *out_sw, double *out_sh);

#pragma endregion

#pragma region type - Shader

    extern "C" LOVE_EXPORT void wrap_love_dll_type_Shader_getWarnings(Shader *shader, WrapString **out_str);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_Shader_sendColors(Shader *shader, const char *name, Float4 *valuearray, int value_lenght);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_Shader_sendFloats(Shader *shader, const char *name, float *valuearray, int valuearray_lenght);
	extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_Shader_sendUints(Shader *shader, const char *name, uint32 *valuearray, int valuearray_lenght);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_Shader_sendInts(Shader *shader, const char *name, int *valuearray, int valuearray_lenght);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_Shader_sendBooleans(Shader *shader, const char *name, bool4 *valuearray, int valuearray_lenght);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_Shader_sendMatrices(Shader *shader, const char *name, float *valueArray, int columns_lenght, int rows_length, int matrix_count);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_Shader_sendTexture(Shader *shader, const char *name, Texture **texture, int texture_lenght);

#pragma endregion

#pragma region type - SpriteBatch

    using love::graphics::SpriteBatch;

    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_SpriteBatch_add(SpriteBatch *t, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky, int *out_index);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_SpriteBatch_add_Quad(SpriteBatch *t, Quad *quad, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky, int *out_index);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_SpriteBatch_set(SpriteBatch *t, int index, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_SpriteBatch_set_Quad(SpriteBatch *t, int index, Quad *quad, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_SpriteBatch_clear(SpriteBatch *t);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_SpriteBatch_flush(SpriteBatch *t);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_SpriteBatch_setTexture(SpriteBatch *t, Texture *tex);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_SpriteBatch_getTexture(SpriteBatch *t, Texture **out_texture);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_SpriteBatch_setColor_nil(SpriteBatch *t);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_SpriteBatch_setColor(SpriteBatch *t, float r, float g, float b, float a);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_SpriteBatch_getColor(SpriteBatch *t, bool4 *out_exist, float *out_r, float *out_g, float *out_b, float *out_a);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_SpriteBatch_getCount(SpriteBatch *t, int *out_count);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_SpriteBatch_getBufferSize(SpriteBatch *t, int *out_buffersize);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_SpriteBatch_attachAttribute(SpriteBatch *t, const char *name, Mesh *m);

#pragma endregion

#pragma region type - Text
    using love::graphics::Text;

    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_Text_set_coloredstring(Text *t, BytePtr coloredStringText[], Float4 coloredStringColor[], int coloredStringLength);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_Text_setf(Text *t, BytePtr coloredStringText[], Float4 coloredStringColor[], int coloredStringLength, float wraplimit, int align_type);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_Text_add(Text *t, BytePtr coloredStringText[], Float4 coloredStringColor[], int coloredStringLength,
        float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky, int *out_index);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_Text_addf(Text *t, BytePtr coloredStringText[], Float4 coloredStringColor[], int coloredStringLength,
        float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky, float wraplimit, int align_type, int *out_index);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Text_clear(Text *t);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_Text_setFont(Text *t, love::graphics::Font *f);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Text_getFont(Text *t, love::graphics::Font **font);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Text_getWidth(Text *t, int index, int *out_w);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Text_getHeight(Text *t, int index, int *out_h);


#pragma endregion

#pragma region type - Texture
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Texture_getWidth(Texture *t, int *out_w);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Texture_getHeight(Texture *t, int *out_h);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_Texture_setFilter(Texture *t, int filtermin_type, int filtermag_type, float anisotropy);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Texture_getFilter(Texture *t, int *out_filtermin_type, int *out_filtermag_type, float *out_anisotropy);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Texture_setWrap(Texture *t, int wraphoriz_type, int wrapvert_type);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Texture_getWrap(Texture *t, int *out_wraphoriz_type, int *out_wrapvert_type);
#pragma endregion

#pragma region type - Video

    extern "C" LOVE_EXPORT void wrap_love_dll_type_Video_getStream(love::graphics::Video *video, VideoStream **out_videsStream);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Video_getSource(love::graphics::Video *video, Source **out_source);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Video_setSource_nil(love::graphics::Video *video);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Video_setSource(love::graphics::Video *video, Source *source);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Video_getWidth(love::graphics::Video *video, int *out_w);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Video_getHeight(love::graphics::Video *video, int *out_h);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_Video_setFilter(love::graphics::Video *video, int filtermin_type, int filtermag_type, float anisotropy);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Video_getFilter(love::graphics::Video *video, int *out_filtermin_type, int *out_filtermag_type, float *out_anisotropy);


#pragma endregion

#pragma region type - CompressedImageData

    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_CompressedImageData_getWidth(CompressedImageData *t, int miplevel, int *out_w);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_CompressedImageData_getHeight(CompressedImageData *t, int miplevel, int *out_h);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_CompressedImageData_getMipmapCount(CompressedImageData *t, int *out_count);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_CompressedImageData_getFormat(CompressedImageData *t, int *out_format_type);

#pragma endregion

#pragma region type - ImageData

    extern "C" LOVE_EXPORT void wrap_love_dll_type_ImageData_getWidth(ImageData *t, int *out_w);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ImageData_getHeight(ImageData *t, int *out_h);
	extern "C" LOVE_EXPORT void wrap_love_dll_type_ImageData_GetFormat(ImageData *t, int *out_pixelFormat);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_ImageData_getPixel(ImageData *t, int x, int y, Pixel *out_pixel);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_ImageData_setPixel(ImageData *t, int x, int y, Pixel pixel);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ImageData_paste(ImageData *t, ImageData* src, int dx, int dy, int sx, int sy, int sw, int sh);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_ImageData_encode(ImageData *t, int format_type, const char* filename);
	typedef Pixel (__cdecl *ImageData_mapPixel_func)(int x, int y, Pixel p);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_ImageData_mapPixel(ImageData *t, int x, int y, int width, int height,ImageData_mapPixel_func func);

#pragma endregion

#pragma region type - Cursor
    
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Cursor_getType(Cursor *cursor, int *out_cursortype_type, bool4* out_custom);

#pragma endregion

#pragma region type - Decoder

    extern "C" LOVE_EXPORT void wrap_love_dll_type_Decoder_getChannelCount(Decoder *t, int *out_channels);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Decoder_getBitDepth(Decoder *t, int *out_bitDepth);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Decoder_getSampleRate(Decoder *t, int *out_sampleRate);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Decoder_getDuration(Decoder *t, double *out_duration);

#pragma endregion

#pragma region type - SoundData

    extern "C" LOVE_EXPORT void wrap_love_dll_SoundData_getChannelCount(SoundData *t, int *out_channels);
    extern "C" LOVE_EXPORT void wrap_love_dll_SoundData_getBitDepth(SoundData *t, int *out_bitDepth);
    extern "C" LOVE_EXPORT void wrap_love_dll_SoundData_getSampleRate(SoundData *t, int *out_sampleRate);
    extern "C" LOVE_EXPORT void wrap_love_dll_SoundData_getSampleCount(SoundData *t, int *out_sampleCount);
    extern "C" LOVE_EXPORT void wrap_love_dll_SoundData_getDuration(SoundData *t, double *out_duration);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_SoundData_setSample(SoundData *t, int i, float sample);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_SoundData_getSample(SoundData *t, int i, float *out_sample);


#pragma endregion

#pragma region type - VideoStream
	extern "C" LOVE_EXPORT void wrap_love_dll_type_VideoStream_setSync(VideoStream *stream, Source* source);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_VideoStream_getFilename(VideoStream *stream, WrapString **out_filename);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_VideoStream_play(VideoStream *stream);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_VideoStream_pause(VideoStream *stream);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_VideoStream_seek(VideoStream *stream, double offset);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_VideoStream_rewind(VideoStream *stream);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_VideoStream_tell(VideoStream *stream, double *out_position);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_VideoStream_isPlaying(VideoStream *stream, bool4 *out_isplaying);

#pragma endregion

#pragma region type - BezierCurve

    extern "C" LOVE_EXPORT void wrap_love_dll_type_BezierCurve_getDegree(BezierCurve *curve, int *out_degree);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_BezierCurve_getDerivative(BezierCurve *curve, BezierCurve **out_deriv);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_BezierCurve_getControlPoint(BezierCurve *curve, int idx, float *out_x, float *out_y);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_BezierCurve_setControlPoint(BezierCurve *curve, int idx, float x, float y);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_BezierCurve_insertControlPoint(BezierCurve *curve, int idx, float x, float y);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_BezierCurve_removeControlPoint(BezierCurve *curve, int idx);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_BezierCurve_getControlPointCount(BezierCurve *curve, int *out_count);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_BezierCurve_translate(BezierCurve *curve, float dx, float dy);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_BezierCurve_rotate(BezierCurve *curve, double phi, float ox, float oy);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_BezierCurve_scale(BezierCurve *curve, double s, float ox, float oy);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_BezierCurve_evaluate(BezierCurve *curve, double t, float *out_x, float *out_y);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_BezierCurve_getSegment(BezierCurve *curve, double t1, double t2, BezierCurve **out_segment);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_BezierCurve_render(BezierCurve *curve, int accuracy, Float2** out_points, int *out_points_lenght);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_BezierCurve_renderSegment(BezierCurve *curve, double start, double end, int accuracy, Float2** out_points, int *out_points_lenght);

#pragma endregion


#pragma region type - RandomGenerator

    extern "C" LOVE_EXPORT void wrap_love_dll_type_RandomGenerator_random(RandomGenerator *rng, double *out_result);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_RandomGenerator_randomNormal(RandomGenerator *rng, double stddev, double mean, double *out_result);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_RandomGenerator_setSeed(RandomGenerator *rng, uint32 low, uint32 high);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_RandomGenerator_getSeed(RandomGenerator *rng, uint32 *out_low, uint32 *out_high);
    extern "C" LOVE_EXPORT bool4 wrap_love_dll_type_RandomGenerator_setState(RandomGenerator *rng, const char *state);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_RandomGenerator_getState(RandomGenerator *rng, WrapString **out_str);

#pragma endregion

#pragma region type - joystick

    extern "C" LOVE_EXPORT void wrap_love_dll_type_Joystick_isConnected(Joystick *j, bool4 *out_result);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Joystick_getName(Joystick *j, WrapString **out_str);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Joystick_getID(Joystick *j, int *out_id);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Joystick_getInstanceID(Joystick *j, int *out_instanceid);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Joystick_getGUID(Joystick *j, WrapString **out_str);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Joystick_getAxisCount(Joystick *j, int *out_count);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Joystick_getButtonCount(Joystick *j, int *out_count);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Joystick_getHatCount(Joystick *j, int *out_count);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Joystick_getAxis(Joystick *j, int axisindex, float *out_axis);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Joystick_getAxes(Joystick *j, float **out_axes, int *out_axes_length);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Joystick_getHat(Joystick *j, int hatindex, int *out_hat_type);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Joystick_isDown(Joystick *j, int button, bool4 *out_result);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Joystick_isGamepad(Joystick *j, bool4 *out_result);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Joystick_getGamepadAxis(Joystick *j, int axis_type, float *out_gamepadaxis);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Joystick_isGamepadDown(Joystick *j, int gamepadButton_type, bool4 *out_result);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Joystick_isVibrationSupported(Joystick *j, bool4 *out_result);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Joystick_setVibration_nil(Joystick *j, bool4 *out_success);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Joystick_setVibration(Joystick *j, float left, float right, float duration, bool4 *out_success);
    extern "C" LOVE_EXPORT void wrap_love_dll_type_Joystick_getVibration(Joystick *j, float *out_left, float *out_right);


#pragma endregion


#pragma region type - Other

    extern "C" LOVE_EXPORT void wrap_love_dll_type_Data_getSize(Data* data, uint32 *out_datasize);

#pragma endregion


#pragma region type - Body
#pragma endregion
}
}
#endif
