
// author : Yang Xiao, yx33355555@163.com
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
//
// part of C resonse for retain
// part of C# response for release

#include "common/Module.h"
#include "modules/timer/sdl/Timer.h"
#include "modules/window/sdl/Window.h"
#include "modules/mouse/sdl/Mouse.h"
#include "modules/keyboard/sdl/Keyboard.h"
#include "modules/event/sdl/Event.h"
#include "modules/filesystem/physfs/Filesystem.h"
#include "modules/filesystem/physfs/File.h"
#include "modules/filesystem/DroppedFile.h"
#include "modules/sound/lullaby/Sound.h"
#include "modules/sound/Decoder.h"
#include "modules/audio/openal/Audio.h"
#include "modules/audio/null/Audio.h"
#include "modules/audio/openal/Source.h"
#include "modules/audio/null/Source.h"
#include "modules/image/magpie/Image.h"
#include "modules/font/freetype/Font.h"
#include "modules/video/theora/Video.h"
#include "modules/graphics/opengl/Graphics.h"
#include "modules/math/MathModule.h"
#include "modules/math/BezierCurve.h"
#include "modules/touch/sdl/Touch.h"
#include "modules/joystick/sdl/Joystick.h"
#include "modules/joystick/sdl/JoystickModule.h"

//#include "common/runtime.h"
#include "common/config.h"
#include "common/version.h"
#include "wrap_love_dll.h"

#include <stdio.h>
#include <iostream>
#include <sstream>

using namespace love::timer;
using namespace love::window;
using namespace love::mouse;
using namespace love::keyboard;
using namespace love::event;
using namespace love::sound;
using namespace love::audio;
using namespace love::filesystem;
using namespace love::math;
using namespace love::touch;

using namespace love::font;
using namespace love::video;
using namespace love::joystick;

using namespace love::graphics;
using love::graphics::opengl::Shader;
using love::graphics::opengl::SpriteBatch;
using love::graphics::opengl::Text;
using love::graphics::opengl::Canvas;
using love::graphics::opengl::Mesh;

using love::math::BezierCurve;

//#define bool // 禁止使用 bool，只能使用 bool4

namespace love
{
namespace wrap
{

#pragma region ErrorPint

	const int MaxBuffer = 2048;
	char errStrBuffer[MaxBuffer] = "no error.";

    void wrap_love_dll_last_error(WrapString **out_errormsg)
	{
        *out_errormsg = new_WrapString(errStrBuffer);
	}

	int wrap_ee(const char *fmt, ...)
	{
		int ret;
		va_list va;
		va_start(va, fmt);
		ret = vprintf(fmt, va);
		vsprintf_s(errStrBuffer, MaxBuffer, fmt, va);
		va_end(va);

		printf("\n");
		return ret;
	}
#pragma endregion


#pragma region common region

    void wrap_love_dll_common_getVersion(WrapString **out_str)
    {
        *out_str = new_WrapString(LOVE_VERSION_STRING);
    }
    void wrap_love_dll_common_getVersionCodeName(WrapString **out_str)
    {
        *out_str = new_WrapString(VERSION_CODENAME);
    }

#pragma endregion


#pragma region delete release region

    // part of C resonse for retain
    // part of C# response for release
    //void wrap_love_dll_retain_obj(Object *p)
    //{
    //    p->retain();
    //}

	void wrap_love_dll_release_obj(Object *p)
	{
		p->release();
	}

	void wrap_love_dll_delete(void *p)
	{
		delete p;
	}

	void wrap_love_dll_delete_array(void *p)
	{
		delete[] p;
	}
    
    void wrap_love_dll_delete_WrapString(WrapString *ws)
    {
        if (ws != nullptr)
        {
            delete [](ws->data);
            delete ws;
        }
    }

    void wrap_love_dll_delete_WrapSequenceString(WrapSequenceString *wss)
    {
        if (wss != nullptr)
        {
            for (int i = 0; i < wss->len; i++)
            {
                delete[] (wss->sequence[i]);
            }
            delete[] wss->sequence;
            delete wss;
        }
    }

#pragma endregion

#pragma region *new* region

    Timer *timerInstance = nullptr;
    Window *windowInstance = nullptr;
    Mouse *mouseInstance = nullptr;
    Keyboard *keyboardInstance = nullptr;
    love::touch::sdl::Touch *touchInstance = nullptr;
    love::joystick::sdl::JoystickModule *joystickInstance = nullptr;
    Event *eventInstance = nullptr;
    Filesystem *fsInstance = nullptr;
    Sound* soundInstance = nullptr;
    Audio *audioInstance = nullptr;
    Image *imageInstance = nullptr;
    Font *fontInstance = nullptr;
    Video *videoInstance = nullptr;
    love::graphics::opengl::Graphics *graphicsInstance = nullptr;

    bool4 wrap_love_dll_filesystem_newFile(const char *filename, int fmode, File** out_file)
    {
        return wrap_catchexcept([&]() {

            File::Mode mode = (File::Mode)fmode;
            File *t = fsInstance->newFile(filename);

            if (mode != File::MODE_CLOSED)
            {
                if (!t->open(mode))
                    throw love::Exception("Could not open file.");
            }

            *out_file = t;
        });
    }
    bool4 wrap_love_dll_filesystem_newFileData_content(const char *contents, int contents_length, const char *filename, int decoder, FileData** out_filedata)
    {
        return wrap_catchexcept([&]() {
            size_t length = strlen(contents);
            FileData *t = nullptr;

            switch ((FileData::Decoder)decoder)
            {
            case FileData::FILE:
                t = fsInstance->newFileData((void *)contents, (int)length, filename);
                break;
            case FileData::BASE64:
                t = fsInstance->newFileData(contents, filename);
                break;
            }
            *out_filedata = t;
        });
    }
    bool4 wrap_love_dll_filesystem_newFileData_file(File *file, FileData** out_filedata)
    {
        return wrap_catchexcept([&]() {
            *out_filedata = file->read();
        });
    }


    bool4 wrap_love_dll_sound_newDecoder_filedata(FileData* data, int buffsize, Decoder** out_decoder)
    {
        return wrap_catchexcept([&]() { 
            Decoder *t = soundInstance->newDecoder(data, buffsize); // default is buffsize = data->getSize()
            if (t == nullptr && data != nullptr)
            {
                throw love::Exception("Extension \"%s\" not supported.", data->getExtension().c_str());
            }
            *out_decoder = t;
        });
    }
    bool4 wrap_love_dll_sound_newDecoder_file(File *file, int bufferSize, Decoder** out_decoder)
    {
        return wrap_catchexcept([&]() {
            if (file == nullptr)
            {
                throw love::Exception("null point error");
            }

            FileData* data = file->read();
            Decoder *t =  soundInstance->newDecoder(data, bufferSize);
            if (t == nullptr && data != nullptr)
            {
                throw love::Exception("Extension \"%s\" not supported.", data->getExtension().c_str());
            }

            if (data != nullptr)
                data->release();

            *out_decoder = t;
        });
    }
    bool4 wrap_love_dll_sound_newSoundData_decoder(Decoder *decoder, SoundData** out_soundata)
    {
        return wrap_catchexcept([&]() { *out_soundata = soundInstance->newSoundData(decoder); });
    }
    bool4 wrap_love_dll_sound_newSoundData(int samples, int rate, int bits, int channels, SoundData** out_soundata)
    {
        return wrap_catchexcept([&]() { *out_soundata = soundInstance->newSoundData(samples, rate, bits, channels); });
    }
    

    bool4 wrap_love_dll_audio_newSource_decoder(Decoder *decoder, int type, Source** out_source)
    {
        return wrap_catchexcept([&]() {
            Source::Type stype = (Source::Type)type;
            *out_source = audioInstance->newSource(decoder);
        });
    }
    bool4 wrap_love_dll_audio_newSource_sounddata(SoundData *sd, int type, Source** out_source)
    {
        return wrap_catchexcept([&]() {
            Source::Type stype = (Source::Type)type;
            *out_source = audioInstance->newSource(sd);
        });
    }

    bool4 wrap_love_dll_mouse_newCursor(ImageData *imageData, int hotx, int hoty, Cursor** out_cursor)
    {
        return wrap_catchexcept([&]() { *out_cursor = mouseInstance->newCursor(imageData, hotx, hoty); });
    }

    bool4 wrap_love_dll_image_newImageData_wh_data(int w, int h, const unsigned char* data, int dataLength, ImageData** out_imagedata)
    {
        return wrap_catchexcept([&]() {
            if (w <= 0 || h <= 0)
            {
                throw love::Exception("Invalid image size.");
            }

            ImageData *t = imageInstance->newImageData(w, h);

            if (data)
            {
                if (dataLength != t->getSize())
                {
                    t->release();
                    throw love::Exception("The size of the raw byte string must match the ImageData's actual size in bytes.");
                }

                memcpy(t->getData(), data, t->getSize());
            }

            *out_imagedata = t;
        });
    }
    bool4 wrap_love_dll_image_newImageData_filedata(FileData *data, ImageData** out_imagedata)
    {
        return wrap_catchexcept([&]() { *out_imagedata = imageInstance->newImageData(data); });
    }
    bool4 wrap_love_dll_image_newCompressedData(FileData *data, CompressedImageData** out_compressedimagedata)
    {
        return wrap_catchexcept([&]() { 
            *out_compressedimagedata = imageInstance->newCompressedData(data); 
        });
    }

    bool4 wrap_love_dll_font_newRasterizer(FileData *fileData, Rasterizer** out_Rasterizer)
    {
        return wrap_catchexcept([&]() { *out_Rasterizer = fontInstance->newRasterizer(fileData); });
    }
    bool4 wrap_love_dll_font_newTrueTypeRasterizer(int size, int hinting_type, Rasterizer** out_Rasterizer)
    {
        TrueTypeRasterizer::Hinting hinting = (TrueTypeRasterizer::Hinting)hinting_type; //TrueTypeRasterizer::HINTING_NORMAL;
        return wrap_catchexcept([&]() { *out_Rasterizer = fontInstance->newTrueTypeRasterizer(size, hinting); });
    }
    bool4 wrap_love_dll_font_newBMFontRasterizer(FileData *fileData, pImageData datas[], int dataLength, Rasterizer** out_Rasterizer)
    {
        std::vector<image::ImageData *> images;
        for (int i = 0; i < dataLength; i++)
            images.push_back(datas[i]);

        return wrap_catchexcept([&]() { *out_Rasterizer = fontInstance->newBMFontRasterizer(fileData, images); });
    }
    bool4 wrap_love_dll_font_newImageRasterizer(ImageData *imageData, const char* glyphsStr, int extraspacing, Rasterizer** out_Rasterizer)
    {
        std::string glyphs(glyphsStr);
        return wrap_catchexcept([&]() { *out_Rasterizer = fontInstance->newImageRasterizer(imageData, glyphs, extraspacing); });
    }
    bool4 wrap_love_dll_font_newGlyphData_rasterizer_str(Rasterizer* r, const char* glyphStr, GlyphData** out_GlyphData)
    {
        std::string glyph(glyphStr);
        return wrap_catchexcept([&]() { *out_GlyphData = fontInstance->newGlyphData(r, glyph); });
    }
    bool4 wrap_love_dll_font_newGlyphData_rasterizer_num(Rasterizer* r, int glyphCode, GlyphData** out_GlyphData)
    {
        uint32 g = (uint32)glyphCode;
        return wrap_catchexcept([&]() { *out_GlyphData = fontInstance->newGlyphData(r, g);});
    }

    bool4 wrap_love_dll_newVideoStream(File *file, VideoStream** out_videostream)
    {
        return wrap_catchexcept([&]() {
            // Can't check if open for reading
            if (!file->isOpen() && !file->open(love::filesystem::File::MODE_READ))
                throw love::Exception("File is not open and cannot be opened");

            *out_videostream = videoInstance->newVideoStream(file);
        });
    }

    bool4 wrap_love_dll_math_newRandomGenerator(RandomGenerator** out_RandomGenerator)
    {
        return wrap_catchexcept([&]() { *out_RandomGenerator = Math::instance.newRandomGenerator();});
    }

    void wrap_love_dll_math_newBezierCurve(Float2* pointsList, int pointsList_lenght, BezierCurve** out_BezierCurve)
    {
        std::vector<Vector> points;
        points.reserve(pointsList_lenght);
        for (int i = 0; i < pointsList_lenght; i += 2)
        {
            Vector v;
            v.x = pointsList[i].x;
            v.y = pointsList[i].y;
            points.push_back(v);
        }
        *out_BezierCurve = Math::instance.newBezierCurve(points);
    }

    bool4 wrap_love_dll_type_Canvas_newImageData(Canvas *canvas, love::image::ImageData **out_img)
    {
        int x = 0;
        int y = 0;
        int w = canvas->getWidth();
        int h = canvas->getHeight();

        return wrap_catchexcept([&]() {
            *out_img = canvas->newImageData(imageInstance, x, y, w, h);
        });
    }

    bool4 wrap_love_dll_type_Canvas_newImageData_xywh(Canvas *canvas, int x, int y, int w, int h, love::image::ImageData **out_img)
    {
        return wrap_catchexcept([&]() {
            *out_img = canvas->newImageData(imageInstance, x, y, w, h);
        });
    }
#pragma endregion

#pragma region timer

    bool4 wrap_love_dll_timer_open_timer()
    {
        timerInstance = Module::getInstance<Timer>(Module::M_TIMER);
        if (timerInstance == nullptr)
        {
            return wrap_catchexcept([&]() {
                timerInstance = new love::timer::sdl::Timer(); 
                Module::registerInstance(timerInstance);
            });
        }

        return timerInstance == nullptr ? true : false;
    }

    void wrap_love_dll_timer_step()
    {
        timerInstance->step();
    }

    void wrap_love_dll_timer_getDelta(float *out_delta)
    {
        *out_delta = timerInstance->getDelta();
    }

    void wrap_love_dll_timer_getFPS(float *out_fps)
    {
        *out_fps = timerInstance->getFPS();
    }

    void wrap_love_dll_timer_getAverageDelta(float *out_averageDelta)
    {
        *out_averageDelta = timerInstance->getAverageDelta();
    }

    void wrap_love_dll_timer_sleep(float t)
    {
        timerInstance->sleep(t);
    }

    void wrap_love_dll_timer_getTime(float *out_time)
    {
        *out_time = timerInstance->getTime();
    }
#pragma endregion

#pragma region window

    bool4 wrap_love_dll_windows_open_love_window()
    {
        windowInstance = Module::getInstance<Window>(Module::M_WINDOW);
        if (windowInstance == nullptr)
        {
            return wrap_catchexcept([&]() {
                windowInstance = new love::window::sdl::Window();
                Module::registerInstance(windowInstance);
            });
        }

        return true;
    }

    void wrap_love_dll_windows_getDisplayCount(int *out_count)
    {
        *out_count = windowInstance->getDisplayCount();
    }

    bool4 wrap_love_dll_windows_getDisplayName(int displayindex, WrapString** out_name)
    {
        return wrap_catchexcept([&]() { 
           auto name = windowInstance->getDisplayName(displayindex);
           *out_name = new_WrapString(name);
        });
    }

    bool4 wrap_love_dll_windows_setMode_w_h(int width, int height)
    {
        return wrap_catche_bool([&]() {
            return windowInstance->setWindow(width, height, nullptr);
        });
    }

    bool4 wrap_love_dll_windows_setMode_w_h_setting(int width, int height, bool4 fullscreen, int fstype, bool4 vsync, int msaa, bool4 resizable, int minwidth, int minheight, bool4 borderless, bool4 centered, int display, bool4 highdpi, double refreshrate, bool4 useposition, int x, int y)
    {
        WindowSettings settings;

        settings.fullscreen = fullscreen;
        settings.fstype = (Window::FullscreenType)fstype; // default is Window::FULLSCREEN_DESKTOP
        settings.vsync = vsync;
        settings.msaa = msaa;
        settings.resizable = resizable;
        settings.minwidth = minwidth;
        settings.minheight = minheight;
        settings.borderless = borderless;
        settings.centered = centered;
        settings.display = display;
        settings.highdpi = highdpi;
        settings.refreshrate = refreshrate;
        settings.useposition = useposition;
        settings.x = x;
        settings.y = y;

        return wrap_catche_bool([&]() { 
            return windowInstance->setWindow(width, height, &settings);
        });
    }

    void wrap_love_dll_windows_getMode(int *out_width, int *out_height, bool4 *out_fullscreen,int *out_fstype, bool4 *out_vsync, int *out_msaa, bool4 *out_resizable, int *out_minwidth, int *out_minheight, bool4 *out_borderless, bool4 *out_centered, int *out_display, bool4 *out_highdpi, double *out_refreshrate, bool4 *out_useposition, int *out_x, int *out_y)
    {
        WindowSettings settings;
        windowInstance->getWindow(*out_width, *out_height, settings);

        *out_fullscreen = settings.fullscreen;
        *out_fstype = settings.fstype;
        *out_vsync = settings.vsync;
        *out_msaa = settings.msaa;
        *out_resizable = settings.resizable;
        *out_minwidth = settings.minwidth;
        *out_minheight = settings.minheight;
        *out_borderless = settings.borderless;
        *out_centered = settings.centered;
        *out_display = settings.display;
        *out_highdpi = settings.highdpi;
        *out_refreshrate = settings.refreshrate;
        *out_useposition = settings.useposition;
        *out_x = settings.x;
        *out_y = settings.y;
    }

    void wrap_love_dll_windows_getFullscreenModes(int displayindex, Int2*** out_modes, int *out_modes_length)
    {
        std::vector<Window::WindowSize> modes = windowInstance->getFullscreenSizes(displayindex);
        *out_modes_length = modes.size();
        *out_modes = new Int2*[modes.size()];
        for (size_t i = 0; i <  modes.size(); i++)
        {
            (*out_modes)[i] = new Int2();
            (*out_modes)[i]->x = modes[i].width;
            (*out_modes)[i]->y = modes[i].height;
        }
    }

    void wrap_love_dll_windows_setFullscreen(bool4 fullscreen, int fstype, bool4 *out_success)
    {
        // fstype default is Window::FullscreenType fstype
        if (fstype == Window::FULLSCREEN_MAX_ENUM)
            *out_success = windowInstance->setFullscreen(fullscreen);
        else
            *out_success = windowInstance->setFullscreen(fullscreen, (Window::FullscreenType)fstype);
    }

    void wrap_love_dll_windows_getFullscreen(bool4 *out_fullscreen, int *out_fstype)
    {
        int w, h;
        WindowSettings settings;
        windowInstance->getWindow(w, h, settings);
        *out_fullscreen = settings.fullscreen;
        *out_fstype = settings.fstype;
    }

    void wrap_love_dll_windows_isOpen(bool4 *out_isopen)
    {
        *out_isopen = windowInstance->isOpen();
    }

    void wrap_love_dll_windows_close()
    {
        windowInstance->close();
    }

    void wrap_love_dll_windows_getDesktopDimensions(int displayindex, int *out_width, int *out_height)
    {
        windowInstance->getDesktopDimensions(displayindex, *out_width, *out_height);
    }

    void wrap_love_dll_windows_setPosition(int x, int y, int displayindex)
    {
        windowInstance->setPosition(x, y, displayindex);
    }

    void wrap_love_dll_windows_getPosition(int *out_x, int *out_y, int *out_displayindex)
    {
        windowInstance->getPosition(*out_x, *out_y, *out_displayindex);
    }

    void wrap_love_dll_windows_setIcon(ImageData *i, bool4 *out_success)
    {
        *out_success = windowInstance->setIcon(i);
    }

    void wrap_love_dll_windows_getIcon(ImageData** out_imagedata)
    {
        *out_imagedata = windowInstance->getIcon();
        (*out_imagedata)->retain();
    }

    void wrap_love_dll_windows_setDisplaySleepEnabled(bool4 enable)
    {
        windowInstance->setDisplaySleepEnabled(enable);
    }

    void wrap_love_dll_windows_isDisplaySleepEnabled(bool4 *out_enable)
    {
         *out_enable = windowInstance->isDisplaySleepEnabled();
    }

    void wrap_love_dll_windows_setTitle(const char *titleStr)
    {
        std::string title(titleStr);
        windowInstance->setWindowTitle(title);
    }

    void wrap_love_dll_windows_getTitle(WrapString** out_title)
    {
        *out_title = new_WrapString(windowInstance->getWindowTitle());
    }

    void wrap_love_dll_windows_hasFocus(bool4* out_result)
    {
        *out_result = windowInstance->hasFocus();
    }

    void wrap_love_dll_windows_hasMouseFocus(bool4* out_result)
    {
        *out_result = windowInstance->hasMouseFocus();
    }

    void wrap_love_dll_windows_isVisible(bool4* out_result)
    {
        *out_result = windowInstance->isVisible();
    }

    void wrap_love_dll_windows_getPixelScale(double *out_result)
    {
        *out_result = windowInstance->getPixelScale();
    }

    void wrap_love_dll_windows_toPixels(double value, double *out_result)
    {
        *out_result = windowInstance->toPixels(value);
    }

    void wrap_love_dll_windows_fromPixels(double pixelvalue, double *out_result)
    {
        *out_result = windowInstance->fromPixels(pixelvalue);
    }

    void wrap_love_dll_windows_minimize()
    {
        windowInstance->minimize();
    }

    void wrap_love_dll_windows_maximize()
    {
        windowInstance->maximize();
    }

    void wrap_love_dll_windows_isMaximized(bool4* out_result)
    {
        *out_result = windowInstance->isMaximized();
    }

    void wrap_love_dll_windows_showMessageBox(const char *title, const char *message, int type, bool4 attachToWindow, bool4* out_result)
    {
        *out_result = windowInstance->showMessageBox(title, message, (Window::MessageBoxType)type, attachToWindow);
    }

    void wrap_love_dll_windows_requestAttention(bool4 continuous)
    {
        windowInstance->requestAttention(continuous);
    }

    void wrap_love_dll_windows_windowToPixelCoords(double *out_x, double *out_y)
    {
        windowInstance->windowToPixelCoords(out_x, out_y);
    }

    void wrap_love_dll_windows_pixelToWindowCoords(double *x, double *y)
    {
        windowInstance->pixelToWindowCoords(x, y);
    }

    void wrap_love_dll_windows_getPixelDimensions(int *out_w, int *out_h)
    {
        windowInstance->getPixelDimensions(*out_w, *out_h);
    }

#pragma endregion

#pragma region mouse

    bool4 wrap_love_dll_open_love_mouse()
    {
        if (mouseInstance == nullptr)
        {
            return wrap_catchexcept([&]() {
                mouseInstance = new love::mouse::sdl::Mouse(); 
                Module::registerInstance(mouseInstance);
            });
        }

        return mouseInstance != nullptr;
    }

    bool4 wrap_love_dll_mouse_getSystemCursor(int sysctype, Cursor** out_cursor)
    {

        return wrap_catchexcept([&]() {
            Cursor::SystemCursor systemCursor = (Cursor::SystemCursor)sysctype;
            *out_cursor = mouseInstance->getSystemCursor(systemCursor);
        });
    }

    void wrap_love_dll_mouse_setCursor(Cursor *cursor)
    {
        // Revert to the default system cursor if no argument is given.
        if (cursor == nullptr)
        {
            mouseInstance->setCursor();
            return;
        }

        mouseInstance->setCursor(cursor);
    }

    void wrap_love_dll_mouse_getCursor(Cursor** out_cursor)
    {
        *out_cursor = mouseInstance->getCursor();
        (*out_cursor)->retain();
    }

    void wrap_love_dll_mouse_hasCursor(bool4 *out_result)
    {
        *out_result = mouseInstance->hasCursor();
    }

    void wrap_love_dll_mouse_getX(double *out_x)
    {
        *out_x = mouseInstance->getX();
    }

    void wrap_love_dll_mouse_getY(double *out_y)
    {
        *out_y = mouseInstance->getY();
    }

    void wrap_love_dll_mouse_getPosition(double *out_x, double *out_y)
    {
        mouseInstance->getPosition(*out_x, *out_y);
    }

    void wrap_love_dll_mouse_setX(double x)
    {
        mouseInstance->setX(x);
    }

    void wrap_love_dll_mouse_setY(double y)
    {
        mouseInstance->setY(y);
    }

    void wrap_love_dll_mouse_setPosition(double x, double y)
    {
        mouseInstance->setPosition(x, y);
    }

    void wrap_love_dll_mouse_isDown(int buttonIndex, bool4 *out_result)
    {
        std::vector<int> buttons;
        buttons.push_back(buttonIndex);
        *out_result = mouseInstance->isDown(buttons);
    }

    void wrap_love_dll_mouse_setVisible(bool4 visible)
    {
        mouseInstance->setVisible(visible);
    }

    void wrap_love_dll_mouse_isVisible(bool4 *out_result)
    {
        *out_result = mouseInstance->isVisible();
    }

    void wrap_love_dll_mouse_setGrabbed(bool4 grabbed)
    {
        mouseInstance->setGrabbed(grabbed);
    }

    void wrap_love_dll_mouse_isGrabbed(bool4 *out_result)
    {
        *out_result = mouseInstance->isGrabbed();
    }

    void wrap_love_dll_mouse_setRelativeMode(bool4 enable, bool4 *out_result)
    {
        *out_result = mouseInstance->setRelativeMode(enable);
    }

    void wrap_love_dll_mouse_getRelativeMode(bool4 *out_result)
    {
        *out_result = mouseInstance->getRelativeMode();
    }

#pragma endregion 

#pragma region keyboard

    bool4 wrap_love_dll_keyboard_open_love_keyboard()
    {
        keyboardInstance = Module::getInstance<Keyboard>(Module::M_KEYBOARD);
        if (keyboardInstance == nullptr)
        {
            return wrap_catchexcept([&]() { 
                keyboardInstance = new love::keyboard::sdl::Keyboard(); 
                Module::registerInstance(keyboardInstance);
            });
        }

        return true;
    }

    void wrap_love_dll_keyboard_setKeyRepeat(bool4 enable)
    {
        keyboardInstance->setKeyRepeat(enable);
    }

    void wrap_love_dll_keyboard_hasKeyRepeat(bool4 *out_result)
    {
        *out_result = keyboardInstance->hasKeyRepeat();
    }

    void wrap_love_dll_keyboard_isDown(int key_type, bool4 *out_result)
    {
        std::vector<Keyboard::Key> keylist;
        keylist.push_back((Keyboard::Key)key_type);
        *out_result = keyboardInstance->isDown(keylist);
    }

    void wrap_love_dll_keyboard_isScancodeDown(int scancode_type, bool4 *out_result)
    {
        std::vector<Keyboard::Scancode> scancodelist;
        scancodelist.push_back((Keyboard::Scancode)scancode_type);
        *out_result = keyboardInstance->isScancodeDown(scancodelist);
    }

    void wrap_love_dll_keyboard_getScancodeFromKey(int key_type, int *out_scancode_type)
    {
        Keyboard::Scancode scancode = keyboardInstance->getScancodeFromKey((Keyboard::Key)key_type);
        *out_scancode_type = scancode;
    }

    void wrap_love_dll_keyboard_getKeyFromScancode(int scancode_type, int *out_key_type)
    {
        Keyboard::Key key = keyboardInstance->getKeyFromScancode((Keyboard::Scancode )scancode_type);
        *out_key_type = key;
    }

    void wrap_love_dll_keyboard_setTextInput(bool4 enable)
    {
        keyboardInstance->setTextInput(enable);
    }

    void wrap_love_dll_keyboard_setTextInput_xywh(bool4 enable, double x, double y, double w, double h)
    {
        keyboardInstance->setTextInput(enable, x, y, w, h);
    }

    void wrap_love_dll_keyboard_hasTextInput(bool4 *out_result)
    {
        *out_result = keyboardInstance->hasTextInput();
    }

    void wrap_love_dll_keyboard_hasScreenKeyboard(bool4 *out_result)
    {
        *out_result = keyboardInstance->hasScreenKeyboard();
    }

#pragma endregion

#pragma region touch

    bool4 wrap_love_dll_touch_open_love_touch()
    {
        if (touchInstance == nullptr)
        {
            return wrap_catchexcept([&]() {
                touchInstance = new love::touch::sdl::Touch();
                Module::registerInstance(touchInstance);
            });
        }

        return touchInstance != nullptr;
    }

    void wrap_love_dll_touch_open_love_getTouches(int64 **out_indexs, double **out_x, double **out_y, double **out_dx, double **out_dy, double **out_pressure, int *out_length)
    {
        const std::vector<Touch::TouchInfo> &touches = touchInstance->getTouches();
        int size = touches.size();
        *out_indexs = new int64[size];
        *out_x = new double[size];
        *out_y = new double[size];
        *out_dx = new double[size];
        *out_dy = new double[size];
        *out_pressure = new double[size];

        for (size_t i = 0; i < touches.size(); i++)
        {
            (*out_indexs)[i] = touches[i].id;
            (*out_x)[i] = touches[i].x;
            (*out_y)[i] = touches[i].y;
            (*out_dx)[i] = touches[i].dx;
            (*out_dy)[i] = touches[i].dy;
            (*out_pressure)[i] = touches[i].pressure;
        }
    }

    bool4 wrap_love_dll_touch_open_love_getToucheInfo(int64 idx, double *out_x, double *out_y, double *out_dx, double *out_dy, double *out_pressure)
    {
        return wrap_catchexcept([&]() { 
            Touch::TouchInfo touch = touchInstance->getTouch(idx);
            (*out_x) = touch.x;
            (*out_y) = touch.y;
            (*out_dx) = touch.dx;
            (*out_dy) = touch.dy;
            (*out_pressure) = touch.pressure;
        });
    }

#pragma endregion

#pragma region joystick

    bool4 wrap_love_dll_joystick_open_love_joystick()
    {
        if (joystickInstance == nullptr)
        {
            return wrap_catchexcept([&]() {
                joystickInstance = new love::joystick::sdl::JoystickModule();
                Module::registerInstance(joystickInstance);
            });
        }

        return joystickInstance != nullptr;
    }


    void wrap_love_dll_joystick_getJoysticks(Joystick ***out_sticks, int *out_sticks_lenght)
    {
        int stickcount = joystickInstance->getJoystickCount();

        *out_sticks = new Joystick*[stickcount];
        for (int i = 0; i < stickcount; i++)
        {
            Joystick *stick = joystickInstance->getJoystick(i);
            (*out_sticks)[i] = stick;
            stick->retain();
        }

        *out_sticks_lenght = stickcount;
    }

    void wrap_love_dll_joystick_getIndex(Joystick *j, int *out_index)
    {
        *out_index = joystickInstance->getIndex(j);
    }

    void wrap_love_dll_joystick_getJoystickCount(int *out_sticks_lenght)
    {
        *out_sticks_lenght = joystickInstance->getJoystickCount();
    }

    void wrap_love_dll_joystick_setGamepadMapping(const char* guid, int gp_inputType_type, int j_inputType_type, int inputIndex, int hat_type, bool4 *out_success)
    {
        // Only accept a GUID string. We don't accept a Joystick object because
        // the gamepad mapping applies to all joysticks with the same GUID (e.g. all
        // Xbox 360 controllers on the system), rather than individual objects.

        Joystick::GamepadInput gpinput;
        //gpinput.type = Joystick::INPUT_TYPE_AXIS;
        //gpinput.type = Joystick::INPUT_TYPE_BUTTON;
        gpinput.type = (Joystick::InputType)gp_inputType_type;

        Joystick::JoystickInput jinput;
        jinput.type = (Joystick::InputType)j_inputType_type;

        switch (jinput.type)
        {
        case Joystick::INPUT_TYPE_AXIS:
            jinput.axis = inputIndex;
            break;
        case Joystick::INPUT_TYPE_BUTTON:
            jinput.button = inputIndex;
            break;
        case Joystick::INPUT_TYPE_HAT:
            // Hats need both a hat index and a hat value.
            jinput.hat.index = inputIndex;
            jinput.hat.value = (Joystick::Hat)hat_type;
            break;
        }

        *out_success = false;
        wrap_catchexcept([&]() { *out_success = joystickInstance->setGamepadMapping(guid, gpinput, jinput); });
    }

    void wrap_love_dll_joystick_loadGamepadMappings(const char *str, bool4 *out_success)
    {
        std::string mappings(str);
        *out_success = wrap_catchexcept([&]() { joystickInstance->loadGamepadMappings(mappings); });
    }

    void wrap_love_dll_joystick_saveGamepadMappings(WrapString **out_str)
    {
        std::string mappings = joystickInstance->saveGamepadMappings();
        *out_str = new_WrapString(mappings);
    }


#pragma endregion

#pragma region event

    enum WrapEventType
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

    std::map<SDL_Scancode, love::keyboard::Keyboard::Scancode> wrap_love_dll_event_createScancodeMap()
    {
        using love::keyboard::Keyboard;
        std::map<SDL_Scancode, Keyboard::Scancode> scan;


        scan[SDL_SCANCODE_UNKNOWN] = Keyboard::Scancode::SCANCODE_UNKNOWN;

        scan[SDL_SCANCODE_A] = Keyboard::Scancode::SCANCODE_A;
        scan[SDL_SCANCODE_B] = Keyboard::Scancode::SCANCODE_B;
        scan[SDL_SCANCODE_C] = Keyboard::Scancode::SCANCODE_C;
        scan[SDL_SCANCODE_D] = Keyboard::Scancode::SCANCODE_D;
        scan[SDL_SCANCODE_E] = Keyboard::Scancode::SCANCODE_E;
        scan[SDL_SCANCODE_F] = Keyboard::Scancode::SCANCODE_F;
        scan[SDL_SCANCODE_G] = Keyboard::Scancode::SCANCODE_G;
        scan[SDL_SCANCODE_H] = Keyboard::Scancode::SCANCODE_H;
        scan[SDL_SCANCODE_I] = Keyboard::Scancode::SCANCODE_I;
        scan[SDL_SCANCODE_J] = Keyboard::Scancode::SCANCODE_J;
        scan[SDL_SCANCODE_K] = Keyboard::Scancode::SCANCODE_K;
        scan[SDL_SCANCODE_L] = Keyboard::Scancode::SCANCODE_L;
        scan[SDL_SCANCODE_M] = Keyboard::Scancode::SCANCODE_M;
        scan[SDL_SCANCODE_N] = Keyboard::Scancode::SCANCODE_N;
        scan[SDL_SCANCODE_O] = Keyboard::Scancode::SCANCODE_O;
        scan[SDL_SCANCODE_P] = Keyboard::Scancode::SCANCODE_P;
        scan[SDL_SCANCODE_Q] = Keyboard::Scancode::SCANCODE_Q;
        scan[SDL_SCANCODE_R] = Keyboard::Scancode::SCANCODE_R;
        scan[SDL_SCANCODE_S] = Keyboard::Scancode::SCANCODE_S;
        scan[SDL_SCANCODE_T] = Keyboard::Scancode::SCANCODE_T;
        scan[SDL_SCANCODE_U] = Keyboard::Scancode::SCANCODE_U;
        scan[SDL_SCANCODE_V] = Keyboard::Scancode::SCANCODE_V;
        scan[SDL_SCANCODE_W] = Keyboard::Scancode::SCANCODE_W;
        scan[SDL_SCANCODE_X] = Keyboard::Scancode::SCANCODE_X;
        scan[SDL_SCANCODE_Y] = Keyboard::Scancode::SCANCODE_Y;
        scan[SDL_SCANCODE_Z] = Keyboard::Scancode::SCANCODE_Z;

        scan[SDL_SCANCODE_1] = Keyboard::Scancode::SCANCODE_1;
        scan[SDL_SCANCODE_2] = Keyboard::Scancode::SCANCODE_2;
        scan[SDL_SCANCODE_3] = Keyboard::Scancode::SCANCODE_3;
        scan[SDL_SCANCODE_4] = Keyboard::Scancode::SCANCODE_4;
        scan[SDL_SCANCODE_5] = Keyboard::Scancode::SCANCODE_5;
        scan[SDL_SCANCODE_6] = Keyboard::Scancode::SCANCODE_6;
        scan[SDL_SCANCODE_7] = Keyboard::Scancode::SCANCODE_7;
        scan[SDL_SCANCODE_8] = Keyboard::Scancode::SCANCODE_8;
        scan[SDL_SCANCODE_9] = Keyboard::Scancode::SCANCODE_9;
        scan[SDL_SCANCODE_0] = Keyboard::Scancode::SCANCODE_0;

        scan[SDL_SCANCODE_RETURN] = Keyboard::Scancode::SCANCODE_RETURN;
        scan[SDL_SCANCODE_ESCAPE] = Keyboard::Scancode::SCANCODE_ESCAPE;
        scan[SDL_SCANCODE_BACKSPACE] = Keyboard::Scancode::SCANCODE_BACKSPACE;
        scan[SDL_SCANCODE_TAB] = Keyboard::Scancode::SCANCODE_TAB;
        scan[SDL_SCANCODE_SPACE] = Keyboard::Scancode::SCANCODE_SPACE;

        scan[SDL_SCANCODE_MINUS] = Keyboard::Scancode::SCANCODE_MINUS;
        scan[SDL_SCANCODE_EQUALS] = Keyboard::Scancode::SCANCODE_EQUALS;
        scan[SDL_SCANCODE_LEFTBRACKET] = Keyboard::Scancode::SCANCODE_LEFTBRACKET;
        scan[SDL_SCANCODE_RIGHTBRACKET] = Keyboard::Scancode::SCANCODE_RIGHTBRACKET;
        scan[SDL_SCANCODE_BACKSLASH] = Keyboard::Scancode::SCANCODE_BACKSLASH;
        scan[SDL_SCANCODE_NONUSHASH] = Keyboard::Scancode::SCANCODE_NONUSHASH;
        scan[SDL_SCANCODE_SEMICOLON] = Keyboard::Scancode::SCANCODE_SEMICOLON;
        scan[SDL_SCANCODE_APOSTROPHE] = Keyboard::Scancode::SCANCODE_APOSTROPHE;
        scan[SDL_SCANCODE_GRAVE] = Keyboard::Scancode::SCANCODE_GRAVE;
        scan[SDL_SCANCODE_COMMA] = Keyboard::Scancode::SCANCODE_COMMA;
        scan[SDL_SCANCODE_PERIOD] = Keyboard::Scancode::SCANCODE_PERIOD;
        scan[SDL_SCANCODE_SLASH] = Keyboard::Scancode::SCANCODE_SLASH;

        scan[SDL_SCANCODE_CAPSLOCK] = Keyboard::Scancode::SCANCODE_CAPSLOCK;

        scan[SDL_SCANCODE_F1] = Keyboard::Scancode::SCANCODE_F1;
        scan[SDL_SCANCODE_F2] = Keyboard::Scancode::SCANCODE_F2;
        scan[SDL_SCANCODE_F3] = Keyboard::Scancode::SCANCODE_F3;
        scan[SDL_SCANCODE_F4] = Keyboard::Scancode::SCANCODE_F4;
        scan[SDL_SCANCODE_F5] = Keyboard::Scancode::SCANCODE_F5;
        scan[SDL_SCANCODE_F6] = Keyboard::Scancode::SCANCODE_F6;
        scan[SDL_SCANCODE_F7] = Keyboard::Scancode::SCANCODE_F7;
        scan[SDL_SCANCODE_F8] = Keyboard::Scancode::SCANCODE_F8;
        scan[SDL_SCANCODE_F9] = Keyboard::Scancode::SCANCODE_F9;
        scan[SDL_SCANCODE_F10] = Keyboard::Scancode::SCANCODE_F10;
        scan[SDL_SCANCODE_F11] = Keyboard::Scancode::SCANCODE_F11;
        scan[SDL_SCANCODE_F12] = Keyboard::Scancode::SCANCODE_F12;

        scan[SDL_SCANCODE_PRINTSCREEN] = Keyboard::Scancode::SCANCODE_PRINTSCREEN;
        scan[SDL_SCANCODE_SCROLLLOCK] = Keyboard::Scancode::SCANCODE_SCROLLLOCK;
        scan[SDL_SCANCODE_PAUSE] = Keyboard::Scancode::SCANCODE_PAUSE;
        scan[SDL_SCANCODE_INSERT] = Keyboard::Scancode::SCANCODE_INSERT;
        scan[SDL_SCANCODE_HOME] = Keyboard::Scancode::SCANCODE_HOME;
        scan[SDL_SCANCODE_PAGEUP] = Keyboard::Scancode::SCANCODE_PAGEUP;
        scan[SDL_SCANCODE_DELETE] = Keyboard::Scancode::SCANCODE_DELETE;
        scan[SDL_SCANCODE_END] = Keyboard::Scancode::SCANCODE_END;
        scan[SDL_SCANCODE_PAGEDOWN] = Keyboard::Scancode::SCANCODE_PAGEDOWN;
        scan[SDL_SCANCODE_RIGHT] = Keyboard::Scancode::SCANCODE_RIGHT;
        scan[SDL_SCANCODE_LEFT] = Keyboard::Scancode::SCANCODE_LEFT;
        scan[SDL_SCANCODE_DOWN] = Keyboard::Scancode::SCANCODE_DOWN;
        scan[SDL_SCANCODE_UP] = Keyboard::Scancode::SCANCODE_UP;

        scan[SDL_SCANCODE_NUMLOCKCLEAR] = Keyboard::Scancode::SCANCODE_NUMLOCKCLEAR;
        scan[SDL_SCANCODE_KP_DIVIDE] = Keyboard::Scancode::SCANCODE_KP_DIVIDE;
        scan[SDL_SCANCODE_KP_MULTIPLY] = Keyboard::Scancode::SCANCODE_KP_MULTIPLY;
        scan[SDL_SCANCODE_KP_MINUS] = Keyboard::Scancode::SCANCODE_KP_MINUS;
        scan[SDL_SCANCODE_KP_PLUS] = Keyboard::Scancode::SCANCODE_KP_PLUS;
        scan[SDL_SCANCODE_KP_ENTER] = Keyboard::Scancode::SCANCODE_KP_ENTER;
        scan[SDL_SCANCODE_KP_1] = Keyboard::Scancode::SCANCODE_KP_1;
        scan[SDL_SCANCODE_KP_2] = Keyboard::Scancode::SCANCODE_KP_2;
        scan[SDL_SCANCODE_KP_3] = Keyboard::Scancode::SCANCODE_KP_3;
        scan[SDL_SCANCODE_KP_4] = Keyboard::Scancode::SCANCODE_KP_4;
        scan[SDL_SCANCODE_KP_5] = Keyboard::Scancode::SCANCODE_KP_5;
        scan[SDL_SCANCODE_KP_6] = Keyboard::Scancode::SCANCODE_KP_6;
        scan[SDL_SCANCODE_KP_7] = Keyboard::Scancode::SCANCODE_KP_7;
        scan[SDL_SCANCODE_KP_8] = Keyboard::Scancode::SCANCODE_KP_8;
        scan[SDL_SCANCODE_KP_9] = Keyboard::Scancode::SCANCODE_KP_9;
        scan[SDL_SCANCODE_KP_0] = Keyboard::Scancode::SCANCODE_KP_0;
        scan[SDL_SCANCODE_KP_PERIOD] = Keyboard::Scancode::SCANCODE_KP_PERIOD;

        scan[SDL_SCANCODE_NONUSBACKSLASH] = Keyboard::Scancode::SCANCODE_NONUSBACKSLASH;
        scan[SDL_SCANCODE_APPLICATION] = Keyboard::Scancode::SCANCODE_APPLICATION;
        scan[SDL_SCANCODE_POWER] = Keyboard::Scancode::SCANCODE_POWER;
        scan[SDL_SCANCODE_KP_EQUALS] = Keyboard::Scancode::SCANCODE_KP_EQUALS;
        scan[SDL_SCANCODE_F13] = Keyboard::Scancode::SCANCODE_F13;
        scan[SDL_SCANCODE_F14] = Keyboard::Scancode::SCANCODE_F14;
        scan[SDL_SCANCODE_F15] = Keyboard::Scancode::SCANCODE_F15;
        scan[SDL_SCANCODE_F16] = Keyboard::Scancode::SCANCODE_F16;
        scan[SDL_SCANCODE_F17] = Keyboard::Scancode::SCANCODE_F17;
        scan[SDL_SCANCODE_F18] = Keyboard::Scancode::SCANCODE_F18;
        scan[SDL_SCANCODE_F19] = Keyboard::Scancode::SCANCODE_F19;
        scan[SDL_SCANCODE_F20] = Keyboard::Scancode::SCANCODE_F20;
        scan[SDL_SCANCODE_F21] = Keyboard::Scancode::SCANCODE_F21;
        scan[SDL_SCANCODE_F22] = Keyboard::Scancode::SCANCODE_F22;
        scan[SDL_SCANCODE_F23] = Keyboard::Scancode::SCANCODE_F23;
        scan[SDL_SCANCODE_F24] = Keyboard::Scancode::SCANCODE_F24;
        scan[SDL_SCANCODE_EXECUTE] = Keyboard::Scancode::SCANCODE_EXECUTE;
        scan[SDL_SCANCODE_HELP] = Keyboard::Scancode::SCANCODE_HELP;
        scan[SDL_SCANCODE_MENU] = Keyboard::Scancode::SCANCODE_MENU;
        scan[SDL_SCANCODE_SELECT] = Keyboard::Scancode::SCANCODE_SELECT;
        scan[SDL_SCANCODE_STOP] = Keyboard::Scancode::SCANCODE_STOP;
        scan[SDL_SCANCODE_AGAIN] = Keyboard::Scancode::SCANCODE_AGAIN;
        scan[SDL_SCANCODE_UNDO] = Keyboard::Scancode::SCANCODE_UNDO;
        scan[SDL_SCANCODE_CUT] = Keyboard::Scancode::SCANCODE_CUT;
        scan[SDL_SCANCODE_COPY] = Keyboard::Scancode::SCANCODE_COPY;
        scan[SDL_SCANCODE_PASTE] = Keyboard::Scancode::SCANCODE_PASTE;
        scan[SDL_SCANCODE_FIND] = Keyboard::Scancode::SCANCODE_FIND;
        scan[SDL_SCANCODE_MUTE] = Keyboard::Scancode::SCANCODE_MUTE;
        scan[SDL_SCANCODE_VOLUMEUP] = Keyboard::Scancode::SCANCODE_VOLUMEUP;
        scan[SDL_SCANCODE_VOLUMEDOWN] = Keyboard::Scancode::SCANCODE_VOLUMEDOWN;
        scan[SDL_SCANCODE_KP_COMMA] = Keyboard::Scancode::SCANCODE_KP_COMMA;
        scan[SDL_SCANCODE_KP_EQUALSAS400] = Keyboard::Scancode::SCANCODE_KP_EQUALSAS400;

        scan[SDL_SCANCODE_INTERNATIONAL1] = Keyboard::Scancode::SCANCODE_INTERNATIONAL1;
        scan[SDL_SCANCODE_INTERNATIONAL2] = Keyboard::Scancode::SCANCODE_INTERNATIONAL2;
        scan[SDL_SCANCODE_INTERNATIONAL3] = Keyboard::Scancode::SCANCODE_INTERNATIONAL3;
        scan[SDL_SCANCODE_INTERNATIONAL4] = Keyboard::Scancode::SCANCODE_INTERNATIONAL4;
        scan[SDL_SCANCODE_INTERNATIONAL5] = Keyboard::Scancode::SCANCODE_INTERNATIONAL5;
        scan[SDL_SCANCODE_INTERNATIONAL6] = Keyboard::Scancode::SCANCODE_INTERNATIONAL6;
        scan[SDL_SCANCODE_INTERNATIONAL7] = Keyboard::Scancode::SCANCODE_INTERNATIONAL7;
        scan[SDL_SCANCODE_INTERNATIONAL8] = Keyboard::Scancode::SCANCODE_INTERNATIONAL8;
        scan[SDL_SCANCODE_INTERNATIONAL9] = Keyboard::Scancode::SCANCODE_INTERNATIONAL9;
        scan[SDL_SCANCODE_LANG1] = Keyboard::Scancode::SCANCODE_LANG1;
        scan[SDL_SCANCODE_LANG2] = Keyboard::Scancode::SCANCODE_LANG2;
        scan[SDL_SCANCODE_LANG3] = Keyboard::Scancode::SCANCODE_LANG3;
        scan[SDL_SCANCODE_LANG4] = Keyboard::Scancode::SCANCODE_LANG4;
        scan[SDL_SCANCODE_LANG5] = Keyboard::Scancode::SCANCODE_LANG5;
        scan[SDL_SCANCODE_LANG6] = Keyboard::Scancode::SCANCODE_LANG6;
        scan[SDL_SCANCODE_LANG7] = Keyboard::Scancode::SCANCODE_LANG7;
        scan[SDL_SCANCODE_LANG8] = Keyboard::Scancode::SCANCODE_LANG8;
        scan[SDL_SCANCODE_LANG9] = Keyboard::Scancode::SCANCODE_LANG9;

        scan[SDL_SCANCODE_ALTERASE] = Keyboard::Scancode::SCANCODE_ALTERASE;
        scan[SDL_SCANCODE_SYSREQ] = Keyboard::Scancode::SCANCODE_SYSREQ;
        scan[SDL_SCANCODE_CANCEL] = Keyboard::Scancode::SCANCODE_CANCEL;
        scan[SDL_SCANCODE_CLEAR] = Keyboard::Scancode::SCANCODE_CLEAR;
        scan[SDL_SCANCODE_PRIOR] = Keyboard::Scancode::SCANCODE_PRIOR;
        scan[SDL_SCANCODE_RETURN2] = Keyboard::Scancode::SCANCODE_RETURN2;
        scan[SDL_SCANCODE_SEPARATOR] = Keyboard::Scancode::SCANCODE_SEPARATOR;
        scan[SDL_SCANCODE_OUT] = Keyboard::Scancode::SCANCODE_OUT;
        scan[SDL_SCANCODE_OPER] = Keyboard::Scancode::SCANCODE_OPER;
        scan[SDL_SCANCODE_CLEARAGAIN] = Keyboard::Scancode::SCANCODE_CLEARAGAIN;
        scan[SDL_SCANCODE_CRSEL] = Keyboard::Scancode::SCANCODE_CRSEL;
        scan[SDL_SCANCODE_EXSEL] = Keyboard::Scancode::SCANCODE_EXSEL;

        scan[SDL_SCANCODE_KP_00] = Keyboard::Scancode::SCANCODE_KP_00;
        scan[SDL_SCANCODE_KP_000] = Keyboard::Scancode::SCANCODE_KP_000;
        scan[SDL_SCANCODE_THOUSANDSSEPARATOR] = Keyboard::Scancode::SCANCODE_THOUSANDSSEPARATOR;
        scan[SDL_SCANCODE_DECIMALSEPARATOR] = Keyboard::Scancode::SCANCODE_DECIMALSEPARATOR;
        scan[SDL_SCANCODE_CURRENCYUNIT] = Keyboard::Scancode::SCANCODE_CURRENCYUNIT;
        scan[SDL_SCANCODE_CURRENCYSUBUNIT] = Keyboard::Scancode::SCANCODE_CURRENCYSUBUNIT;
        scan[SDL_SCANCODE_KP_LEFTPAREN] = Keyboard::Scancode::SCANCODE_KP_LEFTPAREN;
        scan[SDL_SCANCODE_KP_RIGHTPAREN] = Keyboard::Scancode::SCANCODE_KP_RIGHTPAREN;
        scan[SDL_SCANCODE_KP_LEFTBRACE] = Keyboard::Scancode::SCANCODE_KP_LEFTBRACE;
        scan[SDL_SCANCODE_KP_RIGHTBRACE] = Keyboard::Scancode::SCANCODE_KP_RIGHTBRACE;
        scan[SDL_SCANCODE_KP_TAB] = Keyboard::Scancode::SCANCODE_KP_TAB;
        scan[SDL_SCANCODE_KP_BACKSPACE] = Keyboard::Scancode::SCANCODE_KP_BACKSPACE;
        scan[SDL_SCANCODE_KP_A] = Keyboard::Scancode::SCANCODE_KP_A;
        scan[SDL_SCANCODE_KP_B] = Keyboard::Scancode::SCANCODE_KP_B;
        scan[SDL_SCANCODE_KP_C] = Keyboard::Scancode::SCANCODE_KP_C;
        scan[SDL_SCANCODE_KP_D] = Keyboard::Scancode::SCANCODE_KP_D;
        scan[SDL_SCANCODE_KP_E] = Keyboard::Scancode::SCANCODE_KP_E;
        scan[SDL_SCANCODE_KP_F] = Keyboard::Scancode::SCANCODE_KP_F;
        scan[SDL_SCANCODE_KP_XOR] = Keyboard::Scancode::SCANCODE_KP_XOR;
        scan[SDL_SCANCODE_KP_POWER] = Keyboard::Scancode::SCANCODE_KP_POWER;
        scan[SDL_SCANCODE_KP_PERCENT] = Keyboard::Scancode::SCANCODE_KP_PERCENT;
        scan[SDL_SCANCODE_KP_LESS] = Keyboard::Scancode::SCANCODE_KP_LESS;
        scan[SDL_SCANCODE_KP_GREATER] = Keyboard::Scancode::SCANCODE_KP_GREATER;
        scan[SDL_SCANCODE_KP_AMPERSAND] = Keyboard::Scancode::SCANCODE_KP_AMPERSAND;
        scan[SDL_SCANCODE_KP_DBLAMPERSAND] = Keyboard::Scancode::SCANCODE_KP_DBLAMPERSAND;
        scan[SDL_SCANCODE_KP_VERTICALBAR] = Keyboard::Scancode::SCANCODE_KP_VERTICALBAR;
        scan[SDL_SCANCODE_KP_DBLVERTICALBAR] = Keyboard::Scancode::SCANCODE_KP_DBLVERTICALBAR;
        scan[SDL_SCANCODE_KP_COLON] = Keyboard::Scancode::SCANCODE_KP_COLON;
        scan[SDL_SCANCODE_KP_HASH] = Keyboard::Scancode::SCANCODE_KP_HASH;
        scan[SDL_SCANCODE_KP_SPACE] = Keyboard::Scancode::SCANCODE_KP_SPACE;
        scan[SDL_SCANCODE_KP_AT] = Keyboard::Scancode::SCANCODE_KP_AT;
        scan[SDL_SCANCODE_KP_EXCLAM] = Keyboard::Scancode::SCANCODE_KP_EXCLAM;
        scan[SDL_SCANCODE_KP_MEMSTORE] = Keyboard::Scancode::SCANCODE_KP_MEMSTORE;
        scan[SDL_SCANCODE_KP_MEMRECALL] = Keyboard::Scancode::SCANCODE_KP_MEMRECALL;
        scan[SDL_SCANCODE_KP_MEMCLEAR] = Keyboard::Scancode::SCANCODE_KP_MEMCLEAR;
        scan[SDL_SCANCODE_KP_MEMADD] = Keyboard::Scancode::SCANCODE_KP_MEMADD;
        scan[SDL_SCANCODE_KP_MEMSUBTRACT] = Keyboard::Scancode::SCANCODE_KP_MEMSUBTRACT;
        scan[SDL_SCANCODE_KP_MEMMULTIPLY] = Keyboard::Scancode::SCANCODE_KP_MEMMULTIPLY;
        scan[SDL_SCANCODE_KP_MEMDIVIDE] = Keyboard::Scancode::SCANCODE_KP_MEMDIVIDE;
        scan[SDL_SCANCODE_KP_PLUSMINUS] = Keyboard::Scancode::SCANCODE_KP_PLUSMINUS;
        scan[SDL_SCANCODE_KP_CLEAR] = Keyboard::Scancode::SCANCODE_KP_CLEAR;
        scan[SDL_SCANCODE_KP_CLEARENTRY] = Keyboard::Scancode::SCANCODE_KP_CLEARENTRY;
        scan[SDL_SCANCODE_KP_BINARY] = Keyboard::Scancode::SCANCODE_KP_BINARY;
        scan[SDL_SCANCODE_KP_OCTAL] = Keyboard::Scancode::SCANCODE_KP_OCTAL;
        scan[SDL_SCANCODE_KP_DECIMAL] = Keyboard::Scancode::SCANCODE_KP_DECIMAL;
        scan[SDL_SCANCODE_KP_HEXADECIMAL] = Keyboard::Scancode::SCANCODE_KP_HEXADECIMAL;

        scan[SDL_SCANCODE_LCTRL] = Keyboard::Scancode::SCANCODE_LCTRL;
        scan[SDL_SCANCODE_LSHIFT] = Keyboard::Scancode::SCANCODE_LSHIFT;
        scan[SDL_SCANCODE_LALT] = Keyboard::Scancode::SCANCODE_LALT;
        scan[SDL_SCANCODE_LGUI] = Keyboard::Scancode::SCANCODE_LGUI;
        scan[SDL_SCANCODE_RCTRL] = Keyboard::Scancode::SCANCODE_RCTRL;
        scan[SDL_SCANCODE_RSHIFT] = Keyboard::Scancode::SCANCODE_RSHIFT;
        scan[SDL_SCANCODE_RALT] = Keyboard::Scancode::SCANCODE_RALT;
        scan[SDL_SCANCODE_RGUI] = Keyboard::Scancode::SCANCODE_RGUI;

        scan[SDL_SCANCODE_MODE] = Keyboard::Scancode::SCANCODE_MODE;

        scan[SDL_SCANCODE_AUDIONEXT] = Keyboard::Scancode::SCANCODE_AUDIONEXT;
        scan[SDL_SCANCODE_AUDIOPREV] = Keyboard::Scancode::SCANCODE_AUDIOPREV;
        scan[SDL_SCANCODE_AUDIOSTOP] = Keyboard::Scancode::SCANCODE_AUDIOSTOP;
        scan[SDL_SCANCODE_AUDIOPLAY] = Keyboard::Scancode::SCANCODE_AUDIOPLAY;
        scan[SDL_SCANCODE_AUDIOMUTE] = Keyboard::Scancode::SCANCODE_AUDIOMUTE;
        scan[SDL_SCANCODE_MEDIASELECT] = Keyboard::Scancode::SCANCODE_MEDIASELECT;
        scan[SDL_SCANCODE_WWW] = Keyboard::Scancode::SCANCODE_WWW;
        scan[SDL_SCANCODE_MAIL] = Keyboard::Scancode::SCANCODE_MAIL;
        scan[SDL_SCANCODE_CALCULATOR] = Keyboard::Scancode::SCANCODE_CALCULATOR;
        scan[SDL_SCANCODE_COMPUTER] = Keyboard::Scancode::SCANCODE_COMPUTER;
        scan[SDL_SCANCODE_AC_SEARCH] = Keyboard::Scancode::SCANCODE_AC_SEARCH;
        scan[SDL_SCANCODE_AC_HOME] = Keyboard::Scancode::SCANCODE_AC_HOME;
        scan[SDL_SCANCODE_AC_BACK] = Keyboard::Scancode::SCANCODE_AC_BACK;
        scan[SDL_SCANCODE_AC_FORWARD] = Keyboard::Scancode::SCANCODE_AC_FORWARD;
        scan[SDL_SCANCODE_AC_STOP] = Keyboard::Scancode::SCANCODE_AC_STOP;
        scan[SDL_SCANCODE_AC_REFRESH] = Keyboard::Scancode::SCANCODE_AC_REFRESH;
        scan[SDL_SCANCODE_AC_BOOKMARKS] = Keyboard::Scancode::SCANCODE_AC_BOOKMARKS;

        scan[SDL_SCANCODE_BRIGHTNESSDOWN] = Keyboard::Scancode::SCANCODE_BRIGHTNESSDOWN;
        scan[SDL_SCANCODE_BRIGHTNESSUP] = Keyboard::Scancode::SCANCODE_BRIGHTNESSUP;
        scan[SDL_SCANCODE_DISPLAYSWITCH] = Keyboard::Scancode::SCANCODE_DISPLAYSWITCH;
        scan[SDL_SCANCODE_KBDILLUMTOGGLE] = Keyboard::Scancode::SCANCODE_KBDILLUMTOGGLE;
        scan[SDL_SCANCODE_KBDILLUMDOWN] = Keyboard::Scancode::SCANCODE_KBDILLUMDOWN;
        scan[SDL_SCANCODE_KBDILLUMUP] = Keyboard::Scancode::SCANCODE_KBDILLUMUP;
        scan[SDL_SCANCODE_EJECT] = Keyboard::Scancode::SCANCODE_EJECT;
        scan[SDL_SCANCODE_SLEEP] = Keyboard::Scancode::SCANCODE_SLEEP;

        scan[SDL_SCANCODE_APP1] = Keyboard::Scancode::SCANCODE_APP1;
        scan[SDL_SCANCODE_APP2] = Keyboard::Scancode::SCANCODE_APP2;

        return scan;

    }
    std::map<SDL_Scancode, love::keyboard::Keyboard::Scancode> wrap_love_dll_event_scancodeMap = wrap_love_dll_event_createScancodeMap();
   
#undef KEY_EXECUTE
    std::map<SDL_Keycode, love::keyboard::Keyboard::Key> wrap_love_dll_event_createKeyMap()
    {
        using love::keyboard::Keyboard;

        std::map<SDL_Keycode, Keyboard::Key> k;

        k[SDLK_UNKNOWN] = Keyboard::KEY_UNKNOWN;

        k[SDLK_RETURN] = Keyboard::KEY_RETURN;
        k[SDLK_ESCAPE] = Keyboard::KEY_ESCAPE;
        k[SDLK_BACKSPACE] = Keyboard::KEY_BACKSPACE;
        k[SDLK_TAB] = Keyboard::KEY_TAB;
        k[SDLK_SPACE] = Keyboard::KEY_SPACE;
        k[SDLK_EXCLAIM] = Keyboard::KEY_EXCLAIM;
        k[SDLK_QUOTEDBL] = Keyboard::KEY_QUOTEDBL;
        k[SDLK_HASH] = Keyboard::KEY_HASH;
        k[SDLK_PERCENT] = Keyboard::KEY_PERCENT;
        k[SDLK_DOLLAR] = Keyboard::KEY_DOLLAR;
        k[SDLK_AMPERSAND] = Keyboard::KEY_AMPERSAND;
        k[SDLK_QUOTE] = Keyboard::KEY_QUOTE;
        k[SDLK_LEFTPAREN] = Keyboard::KEY_LEFTPAREN;
        k[SDLK_RIGHTPAREN] = Keyboard::KEY_RIGHTPAREN;
        k[SDLK_ASTERISK] = Keyboard::KEY_ASTERISK;
        k[SDLK_PLUS] = Keyboard::KEY_PLUS;
        k[SDLK_COMMA] = Keyboard::KEY_COMMA;
        k[SDLK_MINUS] = Keyboard::KEY_MINUS;
        k[SDLK_PERIOD] = Keyboard::KEY_PERIOD;
        k[SDLK_SLASH] = Keyboard::KEY_SLASH;
        k[SDLK_0] = Keyboard::KEY_0;
        k[SDLK_1] = Keyboard::KEY_1;
        k[SDLK_2] = Keyboard::KEY_2;
        k[SDLK_3] = Keyboard::KEY_3;
        k[SDLK_4] = Keyboard::KEY_4;
        k[SDLK_5] = Keyboard::KEY_5;
        k[SDLK_6] = Keyboard::KEY_6;
        k[SDLK_7] = Keyboard::KEY_7;
        k[SDLK_8] = Keyboard::KEY_8;
        k[SDLK_9] = Keyboard::KEY_9;
        k[SDLK_COLON] = Keyboard::KEY_COLON;
        k[SDLK_SEMICOLON] = Keyboard::KEY_SEMICOLON;
        k[SDLK_LESS] = Keyboard::KEY_LESS;
        k[SDLK_EQUALS] = Keyboard::KEY_EQUALS;
        k[SDLK_GREATER] = Keyboard::KEY_GREATER;
        k[SDLK_QUESTION] = Keyboard::KEY_QUESTION;
        k[SDLK_AT] = Keyboard::KEY_AT;

        k[SDLK_LEFTBRACKET] = Keyboard::KEY_LEFTBRACKET;
        k[SDLK_BACKSLASH] = Keyboard::KEY_BACKSLASH;
        k[SDLK_RIGHTBRACKET] = Keyboard::KEY_RIGHTBRACKET;
        k[SDLK_CARET] = Keyboard::KEY_CARET;
        k[SDLK_UNDERSCORE] = Keyboard::KEY_UNDERSCORE;
        k[SDLK_BACKQUOTE] = Keyboard::KEY_BACKQUOTE;
        k[SDLK_a] = Keyboard::KEY_A;
        k[SDLK_b] = Keyboard::KEY_B;
        k[SDLK_c] = Keyboard::KEY_C;
        k[SDLK_d] = Keyboard::KEY_D;
        k[SDLK_e] = Keyboard::KEY_E;
        k[SDLK_f] = Keyboard::KEY_F;
        k[SDLK_g] = Keyboard::KEY_G;
        k[SDLK_h] = Keyboard::KEY_H;
        k[SDLK_i] = Keyboard::KEY_I;
        k[SDLK_j] = Keyboard::KEY_J;
        k[SDLK_k] = Keyboard::KEY_K;
        k[SDLK_l] = Keyboard::KEY_L;
        k[SDLK_m] = Keyboard::KEY_M;
        k[SDLK_n] = Keyboard::KEY_N;
        k[SDLK_o] = Keyboard::KEY_O;
        k[SDLK_p] = Keyboard::KEY_P;
        k[SDLK_q] = Keyboard::KEY_Q;
        k[SDLK_r] = Keyboard::KEY_R;
        k[SDLK_s] = Keyboard::KEY_S;
        k[SDLK_t] = Keyboard::KEY_T;
        k[SDLK_u] = Keyboard::KEY_U;
        k[SDLK_v] = Keyboard::KEY_V;
        k[SDLK_w] = Keyboard::KEY_W;
        k[SDLK_x] = Keyboard::KEY_X;
        k[SDLK_y] = Keyboard::KEY_Y;
        k[SDLK_z] = Keyboard::KEY_Z;

        k[SDLK_CAPSLOCK] = Keyboard::KEY_CAPSLOCK;

        k[SDLK_F1] = Keyboard::KEY_F1;
        k[SDLK_F2] = Keyboard::KEY_F2;
        k[SDLK_F3] = Keyboard::KEY_F3;
        k[SDLK_F4] = Keyboard::KEY_F4;
        k[SDLK_F5] = Keyboard::KEY_F5;
        k[SDLK_F6] = Keyboard::KEY_F6;
        k[SDLK_F7] = Keyboard::KEY_F7;
        k[SDLK_F8] = Keyboard::KEY_F8;
        k[SDLK_F9] = Keyboard::KEY_F9;
        k[SDLK_F10] = Keyboard::KEY_F10;
        k[SDLK_F11] = Keyboard::KEY_F11;
        k[SDLK_F12] = Keyboard::KEY_F12;

        k[SDLK_PRINTSCREEN] = Keyboard::KEY_PRINTSCREEN;
        k[SDLK_SCROLLLOCK] = Keyboard::KEY_SCROLLLOCK;
        k[SDLK_PAUSE] = Keyboard::KEY_PAUSE;
        k[SDLK_INSERT] = Keyboard::KEY_INSERT;
        k[SDLK_HOME] = Keyboard::KEY_HOME;
        k[SDLK_PAGEUP] = Keyboard::KEY_PAGEUP;
        k[SDLK_DELETE] = Keyboard::KEY_DELETE;
        k[SDLK_END] = Keyboard::KEY_END;
        k[SDLK_PAGEDOWN] = Keyboard::KEY_PAGEDOWN;
        k[SDLK_RIGHT] = Keyboard::KEY_RIGHT;
        k[SDLK_LEFT] = Keyboard::KEY_LEFT;
        k[SDLK_DOWN] = Keyboard::KEY_DOWN;
        k[SDLK_UP] = Keyboard::KEY_UP;

        k[SDLK_NUMLOCKCLEAR] = Keyboard::KEY_NUMLOCKCLEAR;
        k[SDLK_KP_DIVIDE] = Keyboard::KEY_KP_DIVIDE;
        k[SDLK_KP_MULTIPLY] = Keyboard::KEY_KP_MULTIPLY;
        k[SDLK_KP_MINUS] = Keyboard::KEY_KP_MINUS;
        k[SDLK_KP_PLUS] = Keyboard::KEY_KP_PLUS;
        k[SDLK_KP_ENTER] = Keyboard::KEY_KP_ENTER;
        k[SDLK_KP_0] = Keyboard::KEY_KP_0;
        k[SDLK_KP_1] = Keyboard::KEY_KP_1;
        k[SDLK_KP_2] = Keyboard::KEY_KP_2;
        k[SDLK_KP_3] = Keyboard::KEY_KP_3;
        k[SDLK_KP_4] = Keyboard::KEY_KP_4;
        k[SDLK_KP_5] = Keyboard::KEY_KP_5;
        k[SDLK_KP_6] = Keyboard::KEY_KP_6;
        k[SDLK_KP_7] = Keyboard::KEY_KP_7;
        k[SDLK_KP_8] = Keyboard::KEY_KP_8;
        k[SDLK_KP_9] = Keyboard::KEY_KP_9;
        k[SDLK_KP_PERIOD] = Keyboard::KEY_KP_PERIOD;
        k[SDLK_KP_COMMA] = Keyboard::KEY_KP_COMMA;
        k[SDLK_KP_EQUALS] = Keyboard::KEY_KP_EQUALS;

        k[SDLK_APPLICATION] = Keyboard::KEY_APPLICATION;
        k[SDLK_POWER] = Keyboard::KEY_POWER;
        k[SDLK_F13] = Keyboard::KEY_F13;
        k[SDLK_F14] = Keyboard::KEY_F14;
        k[SDLK_F15] = Keyboard::KEY_F15;
        k[SDLK_F16] = Keyboard::KEY_F16;
        k[SDLK_F17] = Keyboard::KEY_F17;
        k[SDLK_F18] = Keyboard::KEY_F18;
        k[SDLK_F19] = Keyboard::KEY_F19;
        k[SDLK_F20] = Keyboard::KEY_F20;
        k[SDLK_F21] = Keyboard::KEY_F21;
        k[SDLK_F22] = Keyboard::KEY_F22;
        k[SDLK_F23] = Keyboard::KEY_F23;
        k[SDLK_F24] = Keyboard::KEY_F24;
        k[SDLK_EXECUTE] = Keyboard::KEY_EXECUTE;
        k[SDLK_HELP] = Keyboard::KEY_HELP;
        k[SDLK_MENU] = Keyboard::KEY_MENU;
        k[SDLK_SELECT] = Keyboard::KEY_SELECT;
        k[SDLK_STOP] = Keyboard::KEY_STOP;
        k[SDLK_AGAIN] = Keyboard::KEY_AGAIN;
        k[SDLK_UNDO] = Keyboard::KEY_UNDO;
        k[SDLK_CUT] = Keyboard::KEY_CUT;
        k[SDLK_COPY] = Keyboard::KEY_COPY;
        k[SDLK_PASTE] = Keyboard::KEY_PASTE;
        k[SDLK_FIND] = Keyboard::KEY_FIND;
        k[SDLK_MUTE] = Keyboard::KEY_MUTE;
        k[SDLK_VOLUMEUP] = Keyboard::KEY_VOLUMEUP;
        k[SDLK_VOLUMEDOWN] = Keyboard::KEY_VOLUMEDOWN;

        k[SDLK_ALTERASE] = Keyboard::KEY_ALTERASE;
        k[SDLK_SYSREQ] = Keyboard::KEY_SYSREQ;
        k[SDLK_CANCEL] = Keyboard::KEY_CANCEL;
        k[SDLK_CLEAR] = Keyboard::KEY_CLEAR;
        k[SDLK_PRIOR] = Keyboard::KEY_PRIOR;
        k[SDLK_RETURN2] = Keyboard::KEY_RETURN2;
        k[SDLK_SEPARATOR] = Keyboard::KEY_SEPARATOR;
        k[SDLK_OUT] = Keyboard::KEY_OUT;
        k[SDLK_OPER] = Keyboard::KEY_OPER;
        k[SDLK_CLEARAGAIN] = Keyboard::KEY_CLEARAGAIN;

        k[SDLK_THOUSANDSSEPARATOR] = Keyboard::KEY_THOUSANDSSEPARATOR;
        k[SDLK_DECIMALSEPARATOR] = Keyboard::KEY_DECIMALSEPARATOR;
        k[SDLK_CURRENCYUNIT] = Keyboard::KEY_CURRENCYUNIT;
        k[SDLK_CURRENCYSUBUNIT] = Keyboard::KEY_CURRENCYSUBUNIT;

        k[SDLK_LCTRL] = Keyboard::KEY_LCTRL;
        k[SDLK_LSHIFT] = Keyboard::KEY_LSHIFT;
        k[SDLK_LALT] = Keyboard::KEY_LALT;
        k[SDLK_LGUI] = Keyboard::KEY_LGUI;
        k[SDLK_RCTRL] = Keyboard::KEY_RCTRL;
        k[SDLK_RSHIFT] = Keyboard::KEY_RSHIFT;
        k[SDLK_RALT] = Keyboard::KEY_RALT;
        k[SDLK_RGUI] = Keyboard::KEY_RGUI;

        k[SDLK_MODE] = Keyboard::KEY_MODE;

        k[SDLK_AUDIONEXT] = Keyboard::KEY_AUDIONEXT;
        k[SDLK_AUDIOPREV] = Keyboard::KEY_AUDIOPREV;
        k[SDLK_AUDIOSTOP] = Keyboard::KEY_AUDIOSTOP;
        k[SDLK_AUDIOPLAY] = Keyboard::KEY_AUDIOPLAY;
        k[SDLK_AUDIOMUTE] = Keyboard::KEY_AUDIOMUTE;
        k[SDLK_MEDIASELECT] = Keyboard::KEY_MEDIASELECT;
        k[SDLK_WWW] = Keyboard::KEY_WWW;
        k[SDLK_MAIL] = Keyboard::KEY_MAIL;
        k[SDLK_CALCULATOR] = Keyboard::KEY_CALCULATOR;
        k[SDLK_COMPUTER] = Keyboard::KEY_COMPUTER;
        k[SDLK_AC_SEARCH] = Keyboard::KEY_APP_SEARCH;
        k[SDLK_AC_HOME] = Keyboard::KEY_APP_HOME;
        k[SDLK_AC_BACK] = Keyboard::KEY_APP_BACK;
        k[SDLK_AC_FORWARD] = Keyboard::KEY_APP_FORWARD;
        k[SDLK_AC_STOP] = Keyboard::KEY_APP_STOP;
        k[SDLK_AC_REFRESH] = Keyboard::KEY_APP_REFRESH;
        k[SDLK_AC_BOOKMARKS] = Keyboard::KEY_APP_BOOKMARKS;

        k[SDLK_BRIGHTNESSDOWN] = Keyboard::KEY_BRIGHTNESSDOWN;
        k[SDLK_BRIGHTNESSUP] = Keyboard::KEY_BRIGHTNESSUP;
        k[SDLK_DISPLAYSWITCH] = Keyboard::KEY_DISPLAYSWITCH;
        k[SDLK_KBDILLUMTOGGLE] = Keyboard::KEY_KBDILLUMTOGGLE;
        k[SDLK_KBDILLUMDOWN] = Keyboard::KEY_KBDILLUMDOWN;
        k[SDLK_KBDILLUMUP] = Keyboard::KEY_KBDILLUMUP;
        k[SDLK_EJECT] = Keyboard::KEY_EJECT;
        k[SDLK_SLEEP] = Keyboard::KEY_SLEEP;

#ifdef LOVE_ANDROID
        k[SDLK_AC_BACK] = Keyboard::KEY_ESCAPE;
#endif

        return k;
    }
    std::map<SDL_Keycode, love::keyboard::Keyboard::Key> wrap_love_dll_event_keys = wrap_love_dll_event_createKeyMap();

    // TODO: add joystick event deal code
    void wrap_love_dll_event_inner_convert_JoystickEvent(const SDL_Event &e, int *out_event_type, bool4 *out_down_or_up, bool4 *out_bool, int *out_idx, int *out_enum1_type, int *out_enum2_type, WrapString** out_str, Int4* out_int4, Float4 *out_float4, float *out_float_value, Joystick **out_joystick)
    {
        love::joystick::Joystick::Hat hat;
        love::joystick::Joystick::GamepadButton padbutton;
        love::joystick::Joystick::GamepadAxis padaxis;

        switch (e.type)
        {
        case SDL_JOYBUTTONDOWN:
        case SDL_JOYBUTTONUP:
            *out_joystick = joystickInstance->getJoystickFromID(e.jbutton.which);
            if (!*out_joystick)
                break;

            *out_idx = (e.jbutton.button);
            *out_down_or_up = (e.type == SDL_JOYBUTTONDOWN);
            *out_event_type = WRAP_EVENT_TYPE_JOYSTICK_BUTTON;
            break;
        case SDL_JOYAXISMOTION:
        {
            *out_joystick = joystickInstance->getJoystickFromID(e.jbutton.which);
            if (!*out_joystick)
                break;

            *out_idx = (e.jaxis.axis);
            *out_float_value = joystick::Joystick::clampval(e.jaxis.value / 32768.0f);
            *out_event_type = WRAP_EVENT_TYPE_JOYSTICK_AXIS_MOTION;
        }
        break;
        case SDL_JOYHATMOTION:

            if (!joystick::sdl::Joystick::getConstant(e.jhat.value, hat))
            {
                break;
            }
            if (!(0 <= hat && hat < Joystick::Hat::HAT_MAX_ENUM))
            {
                break;
            }

            *out_joystick = joystickInstance->getJoystickFromID(e.jbutton.which);
            if (!*out_joystick)
                break;

            *out_idx = e.jhat.hat;
            *out_enum1_type = hat;
            *out_event_type = WRAP_EVENT_TYPE_JOYSTICK_HAT_MOTION;
            break;
        case SDL_CONTROLLERBUTTONDOWN:
        case SDL_CONTROLLERBUTTONUP:
            if (!joystick::sdl::Joystick::getConstant((SDL_GameControllerButton)e.cbutton.button, padbutton))
                break;

            if (!(0 < padbutton && padbutton < Joystick::GamepadButton::GAMEPAD_BUTTON_MAX_ENUM))
                break;

            *out_joystick = joystickInstance->getJoystickFromID(e.jbutton.which);
            if (!*out_joystick)
                break;

            *out_down_or_up = (e.type == SDL_CONTROLLERBUTTONDOWN);
            *out_enum1_type = padbutton;
            *out_event_type = WRAP_EVENT_TYPE_JOYSTICK_CONTROLLER_BUTTON;
            break;
        case SDL_CONTROLLERAXISMOTION:
            if (joystick::sdl::Joystick::getConstant((SDL_GameControllerAxis)e.caxis.axis, padaxis))
            {
                if (!(0 < padaxis && padaxis < Joystick::GamepadAxis::GAMEPAD_AXIS_MAX_ENUM))
                    break;

                *out_joystick = joystickInstance->getJoystickFromID(e.jbutton.which);
                if (!*out_joystick)
                    break;

                *out_enum1_type = padaxis;
                *out_float_value = joystick::Joystick::clampval(e.caxis.value / 32768.0f);
                *out_event_type = WRAP_EVENT_TYPE_JOYSTICK_CONTROLLER_AXIS_MOTION;
            }
            break;
        case SDL_JOYDEVICEADDED:
        case SDL_JOYDEVICEREMOVED:

            *out_joystick = joystickInstance->addJoystick(e.jdevice.which);
            if (!*out_joystick)
                break;

            *out_bool = (e.type == SDL_JOYDEVICEADDED);
            *out_event_type = WRAP_EVENT_TYPE_JOYSTICK_DEVICE_ADDED_OR_REMOVED;
            break;
        default:
            break;
        }

    }

    void wrap_love_dll_event_inner_normalizedToPixelCoords(double *x, double *y)
    {
        int w = 1, h = 1;

        if (windowInstance)
            windowInstance->getPixelDimensions(w, h);

        if (x)
            *x = ((*x) * (double)w);
        if (y)
            *y = ((*y) * (double)h);
    }

    void wrap_love_dll_event_inner_convert_WindowEvent(const SDL_Event &e, int *out_event_type, bool4 *out_down_or_up, bool4 *out_bool, int *out_idx, int *out_enum1_type, int *out_enum2_type, WrapString** out_str, Int4* out_int4, Float4 *out_float4, float *out_float_value, Joystick **out_joystick)
    {
        if (e.type != SDL_WINDOWEVENT)
            return;

        switch (e.window.event)
        {
        case SDL_WINDOWEVENT_FOCUS_GAINED:
        case SDL_WINDOWEVENT_FOCUS_LOST:
            *out_bool = (e.window.event == SDL_WINDOWEVENT_FOCUS_GAINED);
            *out_event_type = WRAP_EVENT_TYPE_WINDOW_VISIBLE;
            break;
        case SDL_WINDOWEVENT_ENTER:
        case SDL_WINDOWEVENT_LEAVE:
            *out_bool = (e.window.event == SDL_WINDOWEVENT_ENTER);
            *out_event_type = WRAP_EVENT_TYPE_WINDOW_ENTER_OR_LEAVE;
            break;
        case SDL_WINDOWEVENT_SHOWN:
        case SDL_WINDOWEVENT_HIDDEN:
            *out_bool = (e.window.event == SDL_WINDOWEVENT_SHOWN);
            *out_event_type = WRAP_EVENT_TYPE_WINDOW_SHOWN_OR_HIDDEN;
            break;
        case SDL_WINDOWEVENT_RESIZED:
        {
            int px_w = e.window.data1;
            int px_h = e.window.data2;

            SDL_Window *sdlwin = SDL_GetWindowFromID(e.window.windowID);
            if (sdlwin)
                SDL_GL_GetDrawableSize(sdlwin, &px_w, &px_h);

            (*out_int4).x = px_w;
            (*out_int4).y = px_h;
            *out_event_type = WRAP_EVENT_TYPE_WINDOW_RESIZED;
        }
        break;
        case SDL_WINDOWEVENT_SIZE_CHANGED:
            if (windowInstance)
                windowInstance->onSizeChanged(e.window.data1, e.window.data2);
            break;
        case SDL_WINDOWEVENT_MINIMIZED:
        case SDL_WINDOWEVENT_RESTORED:
#ifdef LOVE_ANDROID
        {
            if (audioInstance)
            {
                if (e.window.event == SDL_WINDOWEVENT_MINIMIZED)
                    audioInstance->pause();
                else if (e.window.event == SDL_WINDOWEVENT_RESTORED)
                    audioInstance->resume();
            }
        }
#endif
        break;
        }
    }

    void wrap_love_dll_event_inner_convert_event(const SDL_Event &e, int *out_event_type, bool4 *out_down_or_up, bool4 *out_bool, int *out_idx, int *out_enum1_type, int *out_enum2_type, WrapString** out_str, Int4* out_int4, Float4 *out_float4, float *out_float_value, Joystick **out_joystick)
    {
        *out_str = 0;
        *out_event_type = WRAP_EVENT_TYPE_UNKNOW;

        love::keyboard::Keyboard::Key key = love::keyboard::Keyboard::KEY_UNKNOWN;
        love::keyboard::Keyboard::Scancode scancode = love::keyboard::Keyboard::SCANCODE_UNKNOWN;
        std::map<SDL_Keycode, love::keyboard::Keyboard::Key>::const_iterator keyit;
        std::map<SDL_Scancode, love::keyboard::Keyboard::Scancode>::const_iterator scancodeit;

#ifndef LOVE_MACOSX
        Touch::TouchInfo touchinfo;
#endif

#ifdef LOVE_LINUX
        static bool touchNormalizationBug = false;
#endif

        switch (e.type)
        {
        case SDL_KEYDOWN:
        case SDL_KEYUP:
            if (e.key.repeat)
            {
                if (keyboardInstance && !keyboardInstance->hasKeyRepeat())
                    break;
            }

            keyit = wrap_love_dll_event_keys.find(e.key.keysym.sym);
            if (keyit != wrap_love_dll_event_keys.end())
            {
                key = keyit->second;
                *out_enum1_type = key;
            }

            scancodeit = wrap_love_dll_event_scancodeMap.find(e.key.keysym.scancode);
            if (scancodeit != wrap_love_dll_event_scancodeMap.end())
            {
                scancode = scancodeit->second;
                *out_enum2_type = scancode;
            }
            *out_down_or_up = (e.type == SDL_KEYDOWN);
            *out_bool = e.key.repeat;
            *out_event_type = WRAP_EVENT_TYPE_KEY;
            break;
        case SDL_TEXTINPUT:
            *out_str = new_WrapString(e.text.text);
            *out_event_type = WRAP_EVENT_TYPE_TEXTINPUT;
            break;
        case SDL_TEXTEDITING:
            *out_str = new_WrapString(e.edit.text);
            (*out_int4).x = e.edit.start;
            (*out_int4).y = e.edit.length;
            *out_event_type = WRAP_EVENT_TYPE_TEXTEDITING;
            break;
        case SDL_MOUSEMOTION:
        {
            double x = (double)e.motion.x;
            double y = (double)e.motion.y;
            double xrel = (double)e.motion.xrel;
            double yrel = (double)e.motion.yrel;
            if (windowInstance) windowInstance->windowToPixelCoords(&x, &y);
            if (windowInstance) windowInstance->windowToPixelCoords(&xrel, &yrel);

            (*out_float4).x = x;
            (*out_float4).y = y;
            (*out_float4).z = xrel;
            (*out_float4).w = yrel;
            *out_bool = (e.motion.which == SDL_TOUCH_MOUSEID);
            *out_event_type = WRAP_EVENT_TYPE_MOUSE_MOTION;
        }
        break;
        case SDL_MOUSEBUTTONDOWN:
        case SDL_MOUSEBUTTONUP:
        {
            // SDL uses button index 3 for the right mouse button, but we use
            // index 2.
            int button = e.button.button;
            switch (button)
            {
            case SDL_BUTTON_RIGHT:
                button = 2;
                break;
            case SDL_BUTTON_MIDDLE:
                button = 3;
                break;
            }

            double x = (double)e.button.x;
            double y = (double)e.button.y;
            if (windowInstance) windowInstance->windowToPixelCoords(&x, &y);
            (*out_float4).x = x;
            (*out_float4).y = y;
            *out_idx = button;
            *out_bool = (e.button.which == SDL_TOUCH_MOUSEID);
            *out_down_or_up = (e.type == SDL_MOUSEBUTTONDOWN);
            *out_event_type = WRAP_EVENT_TYPE_MOUSE_BUTTON;
        }
        break;
        case SDL_MOUSEWHEEL:
            (*out_int4).x = e.wheel.x;
            (*out_int4).y = e.wheel.y;
            *out_event_type = WRAP_EVENT_TYPE_MOUSE_WHEEL;
            break;
        case SDL_FINGERDOWN:
        case SDL_FINGERUP:
        case SDL_FINGERMOTION:
            // Touch events are disabled in OS X because we only actually want touch
            // screen events, but most touch devices in OS X aren't touch screens
            // (and SDL doesn't differentiate.) Non-screen touch devices like Mac
            // trackpads won't give touch coords in the window's coordinate-space.
#ifndef LOVE_MACOSX
            touchinfo.id = (int64)e.tfinger.fingerId;
            touchinfo.x = e.tfinger.x;
            touchinfo.y = e.tfinger.y;
            touchinfo.dx = e.tfinger.dx;
            touchinfo.dy = e.tfinger.dy;
            touchinfo.pressure = e.tfinger.pressure;

#ifdef LOVE_LINUX
            // FIXME: hacky workaround for SDL not normalizing touch coordinates in
            // its X11 backend: https://bugzilla.libsdl.org/show_bug.cgi?id=2307
            if (touchNormalizationBug || fabs(touchinfo.x) >= 1.5 || fabs(touchinfo.y) >= 1.5 || fabs(touchinfo.dx) >= 1.5 || fabs(touchinfo.dy) >= 1.5)
            {
                touchNormalizationBug = true;
                windowToPixelCoords(&touchinfo.x, &touchinfo.y);
                windowToPixelCoords(&touchinfo.dx, &touchinfo.dy);
            }
            else
#endif
            {
                // SDL's coords are normalized to [0, 1], but we want them in pixels.
                wrap_love_dll_event_inner_normalizedToPixelCoords(&touchinfo.x, &touchinfo.y);
                wrap_love_dll_event_inner_normalizedToPixelCoords(&touchinfo.dx, &touchinfo.dy);
            }

            // We need to update the love.touch.sdl internal state from here.
            if (touchInstance) touchInstance->onEvent(e.type, touchinfo);

            // This is a bit hackish and we lose the higher 32 bits of the id on
            // 32-bit systems, but SDL only ever gives id's that at most use as many
            // bits as can fit in a pointer (for now.)
            // We use lightuserdata instead of a lua_Number (double) because doubles
            // can't represent all possible id values on 64-bit systems.
            *out_idx = touchinfo.id;
            (*out_float4).x = touchinfo.x;
            (*out_float4).y = touchinfo.y;
            (*out_float4).z = touchinfo.dx;
            (*out_float4).w = touchinfo.dy;
            *out_float_value = touchinfo.pressure;

            if (e.type == SDL_FINGERDOWN)
                *out_event_type = WRAP_EVENT_TYPE_TOUCH_PRESSED;
            else if (e.type == SDL_FINGERUP)
                *out_event_type = WRAP_EVENT_TYPE_TOUCH_RELEASED;
            else
                *out_event_type = WRAP_EVENT_TYPE_TOUCH_MOVED;
#endif
            break;
        case SDL_JOYBUTTONDOWN:
        case SDL_JOYBUTTONUP:
        case SDL_JOYAXISMOTION:
        case SDL_JOYBALLMOTION:
        case SDL_JOYHATMOTION:
        case SDL_JOYDEVICEADDED:
        case SDL_JOYDEVICEREMOVED:
        case SDL_CONTROLLERBUTTONDOWN:
        case SDL_CONTROLLERBUTTONUP:
        case SDL_CONTROLLERAXISMOTION:
            wrap_love_dll_event_inner_convert_JoystickEvent(e, out_event_type, out_down_or_up, out_bool, out_idx, out_enum1_type, out_enum2_type, out_str, out_int4, out_float4, out_float_value, out_joystick);
            break;
        case SDL_WINDOWEVENT:
            wrap_love_dll_event_inner_convert_WindowEvent(e, out_event_type, out_down_or_up, out_bool, out_idx, out_enum1_type, out_enum2_type, out_str, out_int4, out_float4, out_float_value, out_joystick);
            break;
        case SDL_DROPFILE:
            if (fsInstance != nullptr)
            {
                // Allow mounting any dropped path, so zips or dirs can be mounted.
                fsInstance->allowMountingForPath(e.drop.file);
                *out_str = new_WrapString(e.drop.file);
                *out_event_type = WRAP_EVENT_TYPE_DROPPED;
            }
            SDL_free(e.drop.file);
            break;
        case SDL_QUIT:
        case SDL_APP_TERMINATING:
            *out_event_type = WRAP_EVENT_TYPE_QUIT;
            break;
        case SDL_APP_LOWMEMORY:
            *out_event_type = WRAP_EVENT_TYPE_LOWMEMORY;
            break;
        default:
            break;
        }
    }

    bool4 wrap_love_dll_event_open_love_event()
    {
       eventInstance = (Module::getInstance<Event>(Module::M_EVENT));

        if (eventInstance == nullptr)
        {
            return wrap_catchexcept([&]() {
                eventInstance = new love::event::sdl::Event();
                Module::registerInstance(eventInstance);
            });
        }

        return eventInstance != nullptr;
    }

    void wrap_love_dll_event_poll(bool4 *out_hasEvent, int *out_event_type, bool4 *out_down_or_up, bool4 *out_bool, int *out_idx, int *out_enum1_type, int *out_enum2_type, WrapString** out_str, Int4* out_int4, Float4 *out_float4, float *out_float_value, Joystick **out_joystick)
    {
        SDL_Event e;
        *out_hasEvent = SDL_PollEvent(&e);
        if (*out_hasEvent)
        {
            wrap_love_dll_event_inner_convert_event(e, out_event_type, out_down_or_up, out_bool, out_idx, out_enum1_type, out_enum2_type, out_str, out_int4, out_float4, out_float_value, out_joystick);
            if (*out_joystick)
            {
                (*out_joystick)->retain();
            }
        }
    }

    void wrap_love_dll_event_wait(bool4 *out_hasEvent, int *out_event_type, bool4 *out_down_or_up, bool4 *out_bool, int *out_idx, int *out_enum1_type, int *out_enum2_type, WrapString** out_str, Int4* out_int4, Float4 *out_float4, float *out_float_value, Joystick **out_joystick)
    {
        SDL_Event e;
        *out_hasEvent = SDL_WaitEvent(&e);
        if (*out_hasEvent)
        {
            wrap_love_dll_event_inner_convert_event(e, out_event_type, out_down_or_up, out_bool, out_idx, out_enum1_type, out_enum2_type, out_str, out_int4, out_float4, out_float_value, out_joystick);
            if (*out_joystick)
            {
                (*out_joystick)->retain();
            }
        }
    }

#pragma endregion

#pragma region fileSystem

    bool4 wrap_love_dll_filesystem_open_love_filesystem()
    {
        fsInstance = Module::getInstance<Filesystem>(Module::M_FILESYSTEM);

        if (fsInstance == nullptr)
        {
            return wrap_catchexcept([&]() { 
                fsInstance = new physfs::Filesystem();
                Module::registerInstance(fsInstance);
            });
        }

        return true;
    }

	bool4 wrap_love_dll_filesystem_init(const char* arg0)
	{
		return wrap_catchexcept([&]() { fsInstance->init(arg0); });
	}

	void wrap_love_dll_filesystem_setFused(bool4 flag)
	{
		fsInstance->setFused(flag);
	}

	void wrap_love_dll_filesystem_isFused(bool4 *out_result)
	{
		*out_result = fsInstance->isFused();
	}

	void wrap_love_dll_filesystem_setAndroidSaveExternal(bool4 useExternal)
	{
		fsInstance->setAndroidSaveExternal(useExternal);
	}

	bool4 wrap_love_dll_filesystem_setIdentity(const char *arg, bool4 append)
	{
		if (!fsInstance->setIdentity(arg, append))
		{
			wrap_ee("Could not set write directory.");
			return false;
		}

		return true;
	}

	void wrap_love_dll_filesystem_getIdentity(WrapString** out_str)
	{
		*out_str = new_WrapString(fsInstance->getIdentity());
	}

	bool4 wrap_love_dll_filesystem_setSource(const char *arg)
	{
		if (!fsInstance->setSource(arg))
		{
			wrap_ee("Could not set source.");
			return false;
		}

		return true;
	}

    void wrap_love_dll_filesystem_getSource(WrapString** out_str)
	{
        *out_str = new_WrapString(fsInstance->getSource());
	}

    void wrap_love_dll_filesystem_mount(const char *archive, const char *mountpoint, bool4 appendToPath, bool4 *out_result)
	{
        *out_result = fsInstance->mount(archive, mountpoint, appendToPath);
	}

	void wrap_love_dll_filesystem_unmount(const char *archive, bool4 *out_result)
	{
        *out_result = fsInstance->unmount(archive);
	}

    void wrap_love_dll_filesystem_getWorkingDirectory(WrapString** out_str)
	{
		*out_str = new_WrapString(fsInstance->getWorkingDirectory());
	}

    void wrap_love_dll_filesystem_getUserDirectory(WrapString** out_str)
	{
        *out_str = new_WrapString(fsInstance->getUserDirectory());
	}

    void wrap_love_dll_filesystem_getAppdataDirectory(WrapString** out_str)
	{
        *out_str = new_WrapString(fsInstance->getAppdataDirectory());
	}

    void wrap_love_dll_filesystem_getSaveDirectory(WrapString** out_str)
	{
        *out_str = new_WrapString(fsInstance->getSaveDirectory());
	}

    void wrap_love_dll_filesystem_getSourceBaseDirectory(WrapString** out_str)
	{
        *out_str = new_WrapString(fsInstance->getSourceBaseDirectory());
	}

    bool4 wrap_love_dll_filesystem_getRealDirectory(const char *filename, WrapString** out_str)
	{
		return wrap_catchexcept([&]() {
			*out_str = new_WrapString(fsInstance->getRealDirectory(filename));
		});
	}

    void wrap_love_dll_filesystem_getExecutablePath(WrapString** out_str)
	{
        *out_str = new_WrapString(fsInstance->getExecutablePath());
	}

    void wrap_love_dll_filesystem_exists(const char *arg, bool4 *out_result)
	{
        *out_result = fsInstance->exists(arg);
	}

    void wrap_love_dll_filesystem_isDirectory(const char *arg, bool4 *out_result)
	{
        *out_result = fsInstance->isDirectory(arg);
	}

    void wrap_love_dll_filesystem_isFile(const char *arg, bool4 *out_result)
	{
        *out_result = fsInstance->isFile(arg);
	}

    void wrap_love_dll_filesystem_isSymlink(const char *filename, bool4 *out_result)
	{
        *out_result = fsInstance->isSymlink(filename);
	}

	bool4 wrap_love_dll_filesystem_createDirectory(const char *arg)
	{
		return fsInstance->createDirectory(arg);
	}

	bool4 wrap_love_dll_filesystem_remove(const char *arg)
	{
		return fsInstance->remove(arg);
	}

	bool4 wrap_love_dll_filesystem_read(const char *filename, int64 len, char **out_data, uint32 *out_data_length)
	{
		return wrap_catchexcept([&]() {
            Data *data = fsInstance->read(filename, len);
            if (data == 0)
            {
                throw love::Exception("cont't read from %s", filename);
            }

            *out_data = new char[data->getSize()];
            memcpy(*out_data, data->getData(), data->getSize());
            *out_data_length = data->getSize();

            delete data;
		});
	}

    bool4 wrap_love_dll_filesystem_write(const char *filename, const void* input, size_t len)
    {
        return wrap_catchexcept([&]() {
            fsInstance->write(filename, input, len);
        });
    }

    bool4 wrap_love_dll_filesystem_append(const char *filename, const void* input, size_t len)
    {
        return wrap_catchexcept([&]() {
            fsInstance->append(filename, input, len);
        });
    }

	void wrap_love_dll_filesystem_getDirectoryItems(const char *dir, WrapSequenceString** out_wss)
	{
		std::vector<std::string> items;
		fsInstance->getDirectoryItems(dir, items);
        *out_wss = new_WrapSequenceString(items);
	}

	bool4 wrap_love_dll_filesystem_getLastModified(const char *filename, int64 *out_time)
	{
        return wrap_catchexcept([&]() {
            *out_time = fsInstance->getLastModified(filename);
        });
	}

	bool4 wrap_love_dll_filesystem_getSize(const char *filename, int64 *out_size)
	{
        return wrap_catchexcept([&]() {
		    *out_size = fsInstance->getSize(filename);
        });
	}

	void wrap_love_dll_filesystem_setSymlinksEnabled(bool4 enable)
	{
		fsInstance->setSymlinksEnabled(enable);
	}

    void wrap_love_dll_filesystem_areSymlinksEnabled(bool4 *out_result)
	{
        *out_result = fsInstance->areSymlinksEnabled();
	}

    void wrap_love_dll_filesystem_getRequirePath(WrapString** out_str)
	{
		std::stringstream path;
		bool4 seperator = false;
		for (auto &element : fsInstance->getRequirePath())
		{
			if (seperator)
				path << ";";
			else
				seperator = true;

			path << element;
		}

		*out_str = new_WrapString(path.str());
	}

	void wrap_love_dll_filesystem_setRequirePath(const char* paths)
	{
		std::string element(paths);
		auto &requirePath = fsInstance->getRequirePath();

		requirePath.clear();
		std::stringstream path;
		path << element;

		while (std::getline(path, element, ';'))
			requirePath.push_back(element);
	}

    void wrap_love_dll_filesystem_ext_allowMountingForPath(const char* pathStr)
    {
        std::string path(pathStr);
        fsInstance->allowMountingForPath(path);
    }

    void wrap_love_dll_filesystem_ext_isRealDirectory(const char* pathStr, bool4 *out_result)
    {
        std::string path(pathStr);
        *out_result = fsInstance->isRealDirectory(path);
    }


#pragma endregion

#pragma region sound

    bool4 wrap_love_dll_sound_luaopen_love_sound()
    {
        soundInstance = Module::getInstance<Sound>(Module::M_SOUND);
        if (soundInstance == nullptr)
        {
            return wrap_catchexcept([&]() { 
                soundInstance = new lullaby::Sound();
                Module::registerInstance(soundInstance);
            });
        }

        return true;
    }
#pragma endregion

#pragma region audio

	bool4 wrap_love_dll_audio_open_love_audio()
	{
		audioInstance = Module::getInstance<love::audio::Audio>(Module::M_AUDIO);

#ifdef LOVE_ENABLE_AUDIO_OPENAL
		if (audioInstance == nullptr)
		{
			// Try OpenAL first.
			try
			{
				audioInstance = new love::audio::openal::Audio();
			}
			catch (love::Exception &e)
			{
				std::cout << e.what() << std::endl;
			}
		}
#endif

#ifdef LOVE_ENABLE_AUDIO_NULL
		if (audioInstance == nullptr)
		{
			// Fall back to nullaudio.
			try
			{
				audioInstance = new love::audio::null::Audio();
			}
			catch (love::Exception &e)
			{
				std::cout << e.what() << std::endl;
			}
		}
#endif

		if (audioInstance == nullptr)
		{
			return false;
			return wrap_ee("Could not open any audio module.");
		}
        else
        {
            Module::registerInstance(audioInstance);
        }

		return true;
	}

	void wrap_love_dll_audio_getSourceCount(int *out_reslut)
	{
		*out_reslut = audioInstance->getSourceCount();
	}
	void wrap_love_dll_audio_stop()
	{
		audioInstance->stop();
	}
	void wrap_love_dll_audio_pause()
	{
		audioInstance->pause();
	}
	void wrap_love_dll_audio_resume()
	{
		audioInstance->resume();
	}
	void wrap_love_dll_audio_rewind()
	{
		audioInstance->rewind();
	}
	void wrap_love_dll_audio_setVolume(float v)
	{
		audioInstance->setVolume(v);
	}
	void wrap_love_dll_audio_getVolume(float *out_volume)
	{
		*out_volume = audioInstance->getVolume();
	}

	void wrap_love_dll_audio_setPosition(float x, float y, float z)
	{
        float v[3] = { x, y, z };
		audioInstance->setPosition(v);
	}

    void wrap_love_dll_audio_getPosition(float *out_x, float *out_y, float *out_z)
	{
		float v[3];
		audioInstance->getPosition(v);
        *out_x = v[0];
        *out_y = v[1];
        *out_z = v[2];
	}

	void wrap_love_dll_audio_setOrientation(float x, float y, float z, float dx, float dy, float dz)
	{
        float v[6] = { x, y, z, dx, dy, dz };
        audioInstance->setOrientation(v);
	}

    void wrap_love_dll_audio_getOrientation(float *out_x, float *out_y, float *out_z, float *out_dx, float *out_dy, float *out_dz)
	{
		float v[6];
		audioInstance->getOrientation(v);
        *out_x = v[0];
        *out_y = v[1];
        *out_z = v[2];
        *out_dx = v[3];
        *out_dy = v[4];
        *out_dz = v[5];
	}

	void wrap_love_dll_audio_setVelocity(float x, float y, float z)
	{
        float v[3] = { x, y, z };
		audioInstance->setVelocity(v);
	}

    void wrap_love_dll_audio_getVelocity(float *out_x, float *out_y, float *out_z)
	{
        float v[3];
		audioInstance->getVelocity(v);
        *out_x = v[0];
        *out_y = v[1];
        *out_z = v[2];
	}

	void wrap_love_dll_audio_setDopplerScale(float scale)
	{
		audioInstance->setDopplerScale(scale);
	}

    void wrap_love_dll_audio_getDopplerScale(float *out_scale)
	{
        *out_scale = audioInstance->getDopplerScale();
	}

	void wrap_love_dll_audio_canRecord(bool4 *out_result)
	{
        *out_result = audioInstance->canRecord();
	}

	void wrap_love_dll_audio_setDistanceModel(int distancemodel_type)
	{
		Audio::DistanceModel distanceModel = (Audio::DistanceModel)distancemodel_type;
		audioInstance->setDistanceModel(distanceModel);
	}

    void wrap_love_dll_audio_getDistanceModel(int *out_distancemodel_type)
	{
        *out_distancemodel_type = audioInstance->getDistanceModel();
	}

#pragma endregion

#pragma region image

    bool4 wrap_love_dll_image_open_love_image()
    {
        imageInstance = Module::getInstance<Image>(Module::M_IMAGE);
        if (imageInstance == nullptr)
        {
            wrap_catchexcept([&]() { 
                imageInstance = new love::image::magpie::Image(); 
                Module::registerInstance(imageInstance);
            });
        }

        return true;
    }

    void wrap_love_dll_image_isCompressed(FileData *data, bool4 *out_result)
    {
        *out_result = imageInstance->isCompressed(data);
    }

#pragma endregion

#pragma region font

    bool4 wrap_love_dll_font_open_love_font()
    {
        fontInstance = Module::getInstance<Font>(Module::M_FONT);
        if (fontInstance == nullptr)
        {
            return wrap_catchexcept([&]() { 
                fontInstance = new freetype::Font();
                Module::registerInstance(fontInstance);
            });
        }

        return true;
    }

#pragma endregion

#pragma region video

    bool4 wrap_love_dll_video_open_love_video()
    {
        videoInstance = Module::getInstance<Video>(Module::M_VIDEO);
        if (videoInstance == nullptr)
        {
            return wrap_catchexcept([&]() {
                videoInstance = new love::video::theora::Video();
                Module::registerInstance(videoInstance);
            });
        }

        return true;
    }

#pragma endregion

#pragma region math

    void wrap_love_dll_open_love_math()
    {
        Math::instance.retain();
    }

    bool4 wrap_love_dll_math_triangulate(Float2* pointsList, int pointsList_lenght, float **out_triArray, int *out_triCount)
    {
        return wrap_catchexcept([&]() {
            std::vector<Vector> points;
            points.reserve(pointsList_lenght);
            for (int i = 0; i < pointsList_lenght; i += 2)
            {
                Vector v;
                v.x = pointsList[i].x;
                v.y = pointsList[i].y;
                points.push_back(v);
            }
            std::vector<love::Vector> &vertices = points;

            if (vertices.size() < 3)
                throw love::Exception("Need at least 3 vertices to triangulate");

            std::vector<Triangle> triangles;

            if (vertices.size() == 3)
                triangles.push_back(Triangle(vertices[0], vertices[1], vertices[2]));
            else
                triangles = Math::instance.triangulate(vertices);

            *out_triCount = triangles.size();
            *out_triArray = new float[(*out_triCount) * 6];

            for (int i = 0; i < (int)triangles.size(); ++i)
            {
                const Triangle &tri = triangles[i];
                *out_triArray[0] = tri.a.x;
                *out_triArray[1] = tri.a.y;
                *out_triArray[2] = tri.b.x;
                *out_triArray[3] = tri.b.y;
                *out_triArray[4] = tri.c.x;
                *out_triArray[5] = tri.c.y;
            }
        });
    }

    void wrap_love_dll_math_isConvex(Float2* pointsList, int pointsList_lenght, bool4 *out_result)
    {
        std::vector<Vector> points;
        points.reserve(pointsList_lenght);
        for (int i = 0; i < pointsList_lenght; i += 2)
        {
            Vector v;
            v.x = pointsList[i].x;
            v.y = pointsList[i].y;
            points.push_back(v);
        }
        *out_result = Math::instance.isConvex(points);
    }

    void wrap_love_dll_math_gammaToLinear(float gama, float *out_liner)
    {
        *out_liner = Math::instance.gammaToLinear(gama);
    }

    void wrap_love_dll_math_linearToGamma(float liner, float *out_gama)
    {
        *out_gama = Math::instance.linearToGamma(liner);
    }

    void wrap_love_dll_math_noise_1(float x, float *out_result)
    {
        *out_result = Math::instance.noise(x);
    }
    void wrap_love_dll_math_noise_2(float x, float y, float *out_result)
    {
        *out_result = Math::instance.noise(x, y);
    }
    void wrap_love_dll_math_noise_3(float x, float y, float z, float *out_result)
    {
        *out_result = Math::instance.noise(x, y, z);
    }
    void wrap_love_dll_math_noise_4(float x, float y, float z, float w, float *out_result)
    {
        *out_result = Math::instance.noise(x, y, z, w);
    }

    bool4 wrap_love_dll_math_compress_str(const char* str, int str_size, int format_type, int level, CompressedData **out_compressedData)
    {
        // default is Compressor::FORMAT_LZ4;
        Compressor::Format format = (Compressor::Format)format_type; 
        // level default is -1

        return wrap_catchexcept([&]() {
            *out_compressedData = Math::instance.compress(format, str, str_size, level);
            // no need for xxxx->retain()
        });
    }

    bool4 wrap_love_dll_math_compress_data(Data *data, int format_type, int level, CompressedData **out_compressedData)
    {
        Compressor::Format format = (Compressor::Format)format_type; // default is Compressor::FORMAT_LZ4;
                                                                     // level default is -1
        return wrap_catchexcept([&]() { 
            *out_compressedData = Math::instance.compress(format, data, level);
            // no need for xxxx->retain()
        });
    }

    bool4 wrap_love_dll_math_decompress_data(CompressedData *data, char **out_datas, int *out_datas_length)
    {
        return wrap_catchexcept([&]() {
            size_t rawsize = data->getDecompressedSize();
            *out_datas = Math::instance.decompress(data, rawsize);
            *out_datas_length = rawsize;
        });
    }

    bool4 wrap_love_dll_math_decompress_bytes(int format_type, const char *cbytes, int cbytes_length, char **out_datas, int *out_datas_length)
    {
        char *rawbytes = nullptr;
        size_t rawsize = 0;

        Compressor::Format format = (Compressor::Format)format_type; // default is Compressor::FORMAT_LZ4;
        size_t compressedsize = cbytes_length;

        return wrap_catchexcept([&]() { 
            *out_datas = Math::instance.decompress(format, cbytes, compressedsize, rawsize);
            *out_datas_length = rawsize;
        });
    }

#pragma endregion


#pragma region graphics Object Creation

    bool4 wrap_love_dll_graphics_open_love_graphics()
    {
        graphicsInstance = Module::getInstance<love::graphics::opengl::Graphics>(Module::M_GRAPHICS);
        if (graphicsInstance == nullptr)
        {
            return wrap_catchexcept([&]() {
                graphicsInstance = new love::graphics::opengl::Graphics();
                Module::registerInstance(graphicsInstance);
            });
        }

        // TODO :
        // init code ...

        //

        return true;
    }

    bool4 wrap_love_dll_graphics_newImage_file(ImageData **imageDataList, int imageDataListLength, bool4 flagMipmaps, bool4 flagLinear, love::graphics::opengl::Image** out_image)
    {
        std::vector<love::image::ImageData *> data;
        for (int i = 0; i < imageDataListLength; i++)
            data.push_back(imageDataList[i]);

        love::graphics::opengl::Image::Flags flags;
        flags.mipmaps = flagMipmaps;
        flags.linear = flagLinear;

        return wrap_catchexcept([&]() { *out_image = graphicsInstance->newImage(data, flags);});
    }
    bool4 wrap_love_dll_graphics_newImage(CompressedImageData **compressedImageDataList, int compressedImageDataListLength, bool4 flagMipmaps, bool4 flagLinear, love::graphics::opengl::Image** out_image)
    {
        std::vector<love::image::CompressedImageData *> cdata;
        for (int i = 0; i < compressedImageDataListLength; i++)
            cdata.push_back(compressedImageDataList[i]);

        love::graphics::opengl::Image::Flags flags;
        flags.mipmaps = flagMipmaps;
        flags.linear = flagLinear;

        return wrap_catchexcept([&]() { *out_image = graphicsInstance->newImage(cdata, flags);});
    }
    void wrap_love_dll_graphics_newQuad(double x, double y, double w, double h, double sw , double sh, Quad** out_quad)
    {
        Quad::Viewport v;
        v.x = x;
        v.y = y;
        v.w = w;
        v.h = h;
        *out_quad = graphicsInstance->newQuad(v, sw, sh);
    }
    bool4 wrap_love_dll_graphics_newFont(Rasterizer *rasterizer, love::graphics::opengl::Font** out_font)
    {
        return wrap_catchexcept( [&]() {
            *out_font = graphicsInstance->newFont(rasterizer, graphicsInstance->getDefaultFilter()); 
        });
    }
    bool4 wrap_love_dll_graphics_newSpriteBatch(Texture *texture, int maxSprites, int usage_type, love::graphics::opengl::SpriteBatch** out_spriteBatch)
    {
        love::graphics::opengl::Mesh::Usage usage = (love::graphics::opengl::Mesh::Usage)usage_type;//love::graphics::opengl::Mesh::USAGE_DYNAMIC;
        return wrap_catchexcept([&]() {
            *out_spriteBatch = graphicsInstance->newSpriteBatch(texture, maxSprites, usage); 
        });
    }
    bool4 wrap_love_dll_graphics_newParticleSystem(Texture *texture, int buffer, ParticleSystem** out_particleSystem)
    {
        return wrap_catchexcept([&]() {
            *out_particleSystem = graphicsInstance->newParticleSystem(texture, buffer); 
        });
    }
    bool4 wrap_love_dll_graphics_newCanvas(int width, int height, int format_type, int msaa, Canvas** out_canvas)
    {
        Canvas::Format format = (love::graphics::opengl::Canvas::Format)format_type;
        return wrap_catchexcept( [&]() {
            *out_canvas = graphicsInstance->newCanvas(width, height, format, msaa);
            if (*out_canvas == nullptr)
            {
                throw love::Exception("Canvas not created, but no error thrown. I don't even...");
            }
        });
    }
    bool4 wrap_love_dll_graphics_newShader(const char* vertexCodeStr, const char* pixelCodeStr, Shader** out_shader)
    {
        Shader::ShaderSource source;
        source.pixel = pixelCodeStr;
        source.pixel = vertexCodeStr;

        return wrap_catchexcept([&]() { *out_shader = graphicsInstance->newShader(source);});
    }
    bool4 wrap_love_dll_graphics_newMesh_specifiedVertices(Float2 pos[], Float2 uv[], Int4 color[], int vertexCount, int drawMode_type, int usage_type, Mesh** out_mesh)
    {
        love::graphics::opengl::Mesh::DrawMode drawMode = (love::graphics::opengl::Mesh::DrawMode)drawMode_type;
        love::graphics::opengl::Mesh::Usage usage = (love::graphics::opengl::Mesh::Usage)usage_type;

        std::vector<Vertex> vertices;
        vertices.reserve(vertexCount);

        for (size_t i = 0; i < vertexCount; i++)
        {

            Vertex v;

            v.x = pos[i].x;
            v.y = pos[i].y;

            v.s = uv[i].x;
            v.t = uv[i].y;
                
            v.r = color[i].r;
            v.g = color[i].g;
            v.b = color[i].b;
            v.a = color[i].a;

            vertices.push_back(v);
        }

        return wrap_catchexcept( [&]() { *out_mesh = graphicsInstance->newMesh(vertices, drawMode, usage); });
    }
    bool4 wrap_love_dll_graphics_newMesh_count(int count, int drawMode_type, int usage_type, Mesh** out_mesh)
    {
        love::graphics::opengl::Mesh::DrawMode drawMode = (love::graphics::opengl::Mesh::DrawMode)drawMode_type;
        love::graphics::opengl::Mesh::Usage usage = (love::graphics::opengl::Mesh::Usage)usage_type;
        return wrap_catchexcept([&]() { *out_mesh = graphicsInstance->newMesh(count, drawMode, usage); });
    }
    bool4 wrap_love_dll_graphics_newText(love::graphics::opengl::Font *font, Int4 coloredStringColor[], const pchar coloredStringText[],  int coloredStringLength, Text** out_text)
    {
        std::vector<love::graphics::opengl::Font::ColoredString> strings;
        strings.reserve(coloredStringLength);

        for (int i = 0; i < coloredStringLength; i++)
        {
            love::graphics::opengl::Font::ColoredString coloredstr;
            coloredstr.color.r = coloredStringColor[i].r;
            coloredstr.color.g = coloredStringColor[i].g;
            coloredstr.color.b = coloredStringColor[i].b;
            coloredstr.color.a = coloredStringColor[i].a;
            coloredstr.str = coloredStringText[i];
        }

        return wrap_catchexcept( [&]() { *out_text = graphicsInstance->newText(font, strings); });
    }
    bool4 wrap_love_dll_graphics_newVideo(VideoStream *videoStream, love::graphics::opengl::Video** out_video)
    {
        return wrap_catchexcept( [&]() { *out_video = graphicsInstance->newVideo(videoStream); });
    }
    bool4 wrap_love_dll_graphics_newScreenshot(bool4 copyAlpha, ImageData** out_imageData)
    {
        return wrap_catchexcept([&]() { *out_imageData = graphicsInstance->newScreenshot(imageInstance, copyAlpha); });
    }

#pragma endregion

#pragma region graphics State

    void wrap_love_dll_graphics_reset()
    {
        graphicsInstance->reset();
    }
    void wrap_love_dll_graphics_isActive(bool4 *out_result)
    {
        *out_result = graphicsInstance->isActive();
    }
    void wrap_love_dll_graphics_isGammaCorrect(bool4 *out_result)
    {
        *out_result = graphicsInstance->isGammaCorrect();
    }
    void wrap_love_dll_graphics_setScissor()
    {
        graphicsInstance->setScissor();
    }
    bool4 wrap_love_dll_graphics_setScissor_xywh(int x, int y, int w, int h)
    {
        return wrap_catchexcept([&]() {
            if (w < 0 || h < 0)
            {
                throw love::Exception("Can't set scissor with negative width and/or height.");
            }
            graphicsInstance->setScissor(x, y, w, h);
        });
    }
    bool4 wrap_love_dll_graphics_intersectScissor(int x, int y, int w, int h)
    {
        return wrap_catchexcept([&]() {
            if (w < 0 || h < 0)
            {
                throw love::Exception("Can't set scissor with negative width and/or height.");
            }
            graphicsInstance->intersectScissor(x, y, w, h);
        });
    }
    void wrap_love_dll_graphics_getScissor(int *out_x, int *out_y, int *out_w, int *out_h)
    {
        graphicsInstance->getScissor(*out_x, *out_y, *out_w, *out_h);
    }
    void wrap_love_dll_graphics_setStencilTest(int compare_type, int compareValue)
    {
        // COMPARE_ALWAYS effectively disables stencil testing.
        Graphics::CompareMode compare = (Graphics::CompareMode)compare_type;// default is Graphics::COMPARE_ALWAYS;
        graphicsInstance->setStencilTest(compare, compareValue);
    }
    void wrap_love_dll_graphics_getStencilTest(int *out_compare_type, int *out_compareValue)
    {
        Graphics::CompareMode compare = Graphics::COMPARE_ALWAYS;
        graphicsInstance->getStencilTest(compare, *out_compareValue);
        *out_compare_type = compare;
    }
    void wrap_love_dll_graphics_setColor(int r, int g, int b, int a)
    {
        Colorf c(r, g, b, a);
        graphicsInstance->setColor(c);
    }
    void wrap_love_dll_graphics_getColor(int *out_r, int *out_g, int *out_b, int *out_a)
    {
        Colorf c = graphicsInstance->getColor();
        *out_r = c.r;
        *out_g = c.g;
        *out_b = c.b;
        *out_a = c.a;
    }
    void wrap_love_dll_graphics_setBackgroundColor(int r, int g, int b, int a)
    {
        Colorf c(r, g, b, a);
        graphicsInstance->setBackgroundColor(c);
    }
    void wrap_love_dll_graphics_getBackgroundColor(int *out_r, int *out_g, int *out_b, int *out_a)
    {
        Colorf c = graphicsInstance->getBackgroundColor();
        *out_r = c.r;
        *out_g = c.g;
        *out_b = c.b;
        *out_a = c.a;
    }
    void wrap_love_dll_graphics_setFont(love::graphics::opengl::Font *font)
    {
        graphicsInstance->setFont(font);
    }
    bool4 wrap_love_dll_graphics_getFont(love::graphics::opengl::Font** out_font)
    {
        return wrap_catchexcept([&]() { *out_font = graphicsInstance->getFont(); });
    }
    void wrap_love_dll_graphics_setColorMask(bool4 r, bool4 g, bool4 b, bool4 a)
    {
        Graphics::ColorMask mask;
        mask.r = r;
        mask.g = g;
        mask.b = b;
        mask.a = a;
        graphicsInstance->setColorMask(mask);
    }
    void wrap_love_dll_graphics_getColorMask(bool4 *out_r, bool4 *out_g, bool4 *out_b, bool4 *out_a)
    {
        Graphics::ColorMask mask = graphicsInstance->getColorMask();
        *out_r = mask.r;
        *out_g = mask.g;
        *out_b = mask.b;
        *out_a = mask.a;
    }
    bool4 wrap_love_dll_graphics_setBlendMode(int blendMode_type, int blendAlphaMode_type)
    {
        Graphics::BlendMode mode = (Graphics::BlendMode)blendMode_type;
        Graphics::BlendAlpha alphamode = (Graphics::BlendAlpha)blendAlphaMode_type;// default is Graphics::BLENDALPHA_MULTIPLY;
        return wrap_catchexcept([&]() { graphicsInstance->setBlendMode(mode, alphamode); });
    }
    void wrap_love_dll_graphics_getBlendMode(int *out_blendMode_type, int *out_blendAlphaMode_type)
    {
        Graphics::BlendAlpha alphamode;
        Graphics::BlendMode mode = graphicsInstance->getBlendMode(alphamode);

        *out_blendMode_type = mode;
        *out_blendAlphaMode_type = alphamode;
    }
    void wrap_love_dll_graphics_setDefaultFilter(int filterModeMagMin_type, int filterModeMag_type, int anisotropy)
    {
        Texture::Filter filter;
        filter.min = (Texture::FilterMode)filterModeMagMin_type;
        filter.mag = (Texture::FilterMode)filterModeMag_type;
        filter.anisotropy = anisotropy;

        graphicsInstance->setDefaultFilter(filter);
    }
    void wrap_love_dll_graphics_getDefaultFilter(int *out_filterModeMagMin_type, int *out_filterModeMag_type, int *out_anisotropy)
    {
        const Texture::Filter &filter = graphicsInstance->getDefaultFilter();
        *out_filterModeMagMin_type = filter.min;
        *out_filterModeMag_type = filter.mag;
        *out_anisotropy = filter.anisotropy;
    }
    void wrap_love_dll_graphics_setLineWidth(float width)
    {
        graphicsInstance->setLineWidth(width);
    }
    void wrap_love_dll_graphics_setLineStyle(int style_type)
    {
        Graphics::LineStyle style = (Graphics::LineStyle)style_type;
        graphicsInstance->setLineStyle(style);
    }
    void wrap_love_dll_graphics_setLineJoin(int join_type)
    {
        Graphics::LineJoin join = (Graphics::LineJoin)join_type;
        graphicsInstance->setLineJoin(join);
    }
    void wrap_love_dll_graphics_getLineWidth(float *out_result)
    {
        *out_result = graphicsInstance->getLineWidth();
    }
    void wrap_love_dll_graphics_getLineStyle(int *out_lineStyle_type)
    {
        *out_lineStyle_type = graphicsInstance->getLineStyle();
    }
    void wrap_love_dll_graphics_getLineJoin(int *out_lineJoinStyle_type)
    {
        *out_lineJoinStyle_type = graphicsInstance->getLineJoin();
    }
    void wrap_love_dll_graphics_setPointSize(float size)
    {
        graphicsInstance->setPointSize(size);
    }
    void wrap_love_dll_graphics_getPointSize(float *out_size)
    {
        *out_size = graphicsInstance->getPointSize();
    }
    void wrap_love_dll_graphics_setWireframe(bool4 enable)
    {
        graphicsInstance->setWireframe(enable);
    }
    void wrap_love_dll_graphics_isWireframe(bool4 *out_isWireFrame)
    {
        *out_isWireFrame = graphicsInstance->isWireframe();
    }
    bool4 wrap_love_dll_graphics_setCanvas(Canvas** canvaList, int canvaListLength)
    {
        // Disable stencil writes.
        graphicsInstance->stopDrawToStencilBuffer();

        std::vector<Canvas*> canvases;
        canvases.reserve(canvaListLength);

        for (int i = 0; i < canvaListLength; i++)
        {
            canvases.push_back(canvaList[i]);
        }

        return wrap_catchexcept([&]() {
            if (canvases.size() > 0)
                graphicsInstance->setCanvas(canvases);
            else
                graphicsInstance->setCanvas();
        });
    }
    void wrap_love_dll_graphics_getCanvas(Canvas*** out_canvas, int *out_canvas_length)
    {
        const std::vector<Canvas *> canvases = graphicsInstance->getCanvas();
        Canvas** canvaList = new Canvas*[canvases.size()];

        int n = 0;
        for (love::graphics::opengl::Canvas *c : canvases)
        {
            canvaList[n] = c;
            c->retain();
            n++;
        }
        *out_canvas = canvaList;
        *out_canvas_length = canvases.size();
    }
    void wrap_love_dll_graphics_setShader(Shader *shader)
    {
        graphicsInstance->setShader(shader);
    }
    void wrap_love_dll_graphics_getShader(Shader** out_shader)
    {
        *out_shader = graphicsInstance->getShader();
        (*out_shader)->retain();
    }
    void wrap_love_dll_graphics_setDefaultShaderCode(
        const char* glsl_codeVertexStr, const char* glsl_codePixelxStr, const char* glsl_videoPixelCodeStr,
        const char* glsl_codeVertexStr_gammacorrect, const char* glsl_codePixelxStr_gammacorrect, const char* glsl_videoPixelCodeStr_gammacorrect,
        const char* glsles_codeVertexStr, const char* glsles_codePixelxStr, const char* glsles_videoPixelCodeStr,
        const char* glsles_codeVertexStr_gammacorrect, const char* glsles_codePixelxStr_gammacorrect, const char* glsles_videoPixelCodeStr_gammacorrect
    )
    {
        const char* codesStr[4][3] = {
            {glsl_codeVertexStr, glsl_codePixelxStr, glsl_videoPixelCodeStr},
            {glsl_codeVertexStr_gammacorrect, glsl_codePixelxStr_gammacorrect, glsl_videoPixelCodeStr_gammacorrect },
            {glsles_codeVertexStr, glsles_codePixelxStr, glsles_videoPixelCodeStr},
            {glsles_codeVertexStr_gammacorrect, glsles_codePixelxStr_gammacorrect, glsles_videoPixelCodeStr_gammacorrect }
        };

        for (int i = 0; i < 2; i++)
        {
            for (int renderer = 0; renderer < Graphics::RENDERER_MAX_ENUM; renderer++)
            {
                int glsl_base = (renderer == Graphics::RENDERER_OPENGLES ? 2 : 0);
                int base = glsl_base + i;

                love::graphics::opengl::Shader::ShaderSource code;
                code.vertex = codesStr[base][0];
                code.pixel = codesStr[base][1];

                love::graphics::opengl::Shader::ShaderSource videocode;
                videocode.vertex = codesStr[base][0];
                videocode.pixel = codesStr[base][2];

                love::graphics::opengl::Shader::defaultCode[renderer][i] = code;
                love::graphics::opengl::Shader::defaultVideoCode[renderer][i] = videocode;
            }
        }
    }

#pragma endregion

#pragma region graphics Coordinate System

    void wrap_love_dll_graphics_push(int stack_type)
    {
        auto stype = (Graphics::StackType)stack_type; // default is Graphics::STACK_TRANSFORM;
        wrap_catchexcept([&]() { graphicsInstance->push(stype); });
    }
    void wrap_love_dll_graphics_pop()
    {
        wrap_catchexcept([&]() { graphicsInstance->pop(); });
    }
    void wrap_love_dll_graphics_rotate(float angle)
    {
        graphicsInstance->rotate(angle);
    }
    void wrap_love_dll_graphics_scale(float sx, float sy)
    {
        graphicsInstance->scale(sx, sy);
    }
    void wrap_love_dll_graphics_translate(float x, float y)
    {
        graphicsInstance->translate(x, y);
    }
    void wrap_love_dll_graphics_shear(float kx, float ky)
    {
        graphicsInstance->shear(kx, ky);
    }
    void wrap_love_dll_graphics_origin()
    {
        graphicsInstance->origin();
    }

#pragma endregion

#pragma region graphics Drwing

    // graphics.stencil part 1
    void wrap_love_dll_graphics_ext_stencil_clearStencil()
    {
        graphicsInstance->clearStencil();
    }
    // graphics.stencil part 2
    void wrap_love_dll_graphics_ext_stencil_drawToStencilBuffer(int action_type, int stencilValue)
    {
        Graphics::StencilAction action = (Graphics::StencilAction)action_type;//Graphics::STENCIL_REPLACE;
        graphicsInstance->drawToStencilBuffer(action, stencilValue);

        // Call func here ...

        // *DO NOT forget* graphicsInstance->stopDrawToStencilBuffer();
    }
    // graphics.stencil part 3
    void wrap_love_dll_graphics_ext_stencil_stopDrawToStencilBuffer()
    {
        graphicsInstance->stopDrawToStencilBuffer();
    }

    bool4 wrap_love_dll_graphics_clear_rgba(float r, float g, float b, float a)
    {
        Colorf color(r, g, b, a);
        return wrap_catchexcept([&]() { graphicsInstance->clear(color); });
    }
    bool4 wrap_love_dll_graphics_clear_rgbalist(Float4 colorList[], bool4 enableList[], int listLength)
    {
        return wrap_catchexcept([&]() {

            std::vector<love::graphics::opengl::Graphics::OptionalColorf> colors;
            colors.reserve(listLength);

            for (int i = 0; i < listLength; i++)
            {
                Float4 &c = colorList[i];
                colors[i].enabled = enableList[i];
                colors[i].r = c.r;
                colors[i].g = c.g;
                colors[i].b = c.b;
                colors[i].a = c.a;
            }

            graphicsInstance->clear(colors);
        });
    }
    void wrap_love_dll_graphics_discard(bool4 discardColors[], int discardColorsLength, bool4 discardStencil)
    {
        std::vector<bool> colorbuffers;

        for (size_t i = 0; i < discardColorsLength; i++)
        {
            colorbuffers.push_back(discardColors[i]);
        }

        graphicsInstance->discard(colorbuffers, discardStencil);
    }
    void wrap_love_dll_graphics_present()
    {
        graphicsInstance->present();
    }

    bool4 wrap_love_dll_graphics_draw_drawable(Drawable *drawable, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky)
    {
        Quad *quad = nullptr;

        return wrap_catchexcept([&]() {
            drawable->draw(x, y, a, sx, sy, ox, oy, kx, ky);
        });
    }
    bool4 wrap_love_dll_graphics_draw_texture_quad(Quad *quad, Texture *texture, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky)
    {
        return wrap_catchexcept([&]() {
            texture->drawq(quad, x, y, a, sx, sy, ox, oy, kx, ky);
        });
    }
    bool4 wrap_love_dll_graphics_print(const char **coloredStringListStr, Int4 coloredStringListColor[], int coloredStringListLength, float x, float y, float angle, float sx, float sy, float ox, float oy, float kx, float ky)
    {
        std::vector<love::graphics::opengl::Font::ColoredString> coloredStrings;
        coloredStrings.reserve(coloredStringListLength);

        for (int i = 0; i < coloredStringListLength; i++)
        {
            love::graphics::opengl::Font::ColoredString cs;
            cs.str = coloredStringListStr[i];
            cs.color.r = coloredStringListColor[i].r;
            cs.color.g = coloredStringListColor[i].g;
            cs.color.b = coloredStringListColor[i].b;
            cs.color.a = coloredStringListColor[i].a;

            coloredStrings.push_back(cs);
        }

        return wrap_catchexcept([&]() { graphicsInstance->print(coloredStrings, x, y, angle, sx, sy, ox, oy, kx, ky); });
    }
    bool4 wrap_love_dll_graphics_printf(const char **coloredStringListStr, Int4 coloredStringListColor[], int coloredStringListLength, float x, float y, float wrap, int align_type, float angle, float sx, float sy, float ox, float oy, float kx, float ky)
    {
        std::vector<love::graphics::opengl::Font::ColoredString> coloredStrings;
        coloredStrings.reserve(coloredStringListLength);

        for (int i = 0; i < coloredStringListLength; i++)
        {
            love::graphics::opengl::Font::ColoredString cs;
            cs.str = coloredStringListStr[i];
            cs.color.r = coloredStringListColor[i].r;
            cs.color.g = coloredStringListColor[i].g;
            cs.color.b = coloredStringListColor[i].b;
            cs.color.a = coloredStringListColor[i].a;

            coloredStrings.push_back(cs);
        }

        auto align = (love::graphics::opengl::Font::AlignMode) align_type;//Font::ALIGN_LEFT;

        return wrap_catchexcept([&]() { graphicsInstance->printf(coloredStrings, x, y, wrap, align, angle, sx, sy, ox, oy, kx, ky); });
    }
    bool4 wrap_love_dll_graphics_rectangle(int mode_type, float x, float y, float w, float h)
    {
        auto mode = (Graphics::DrawMode)mode_type;
        return wrap_catchexcept([&]() {graphicsInstance->rectangle(mode, x, y, w, h);});
    }
    bool4 wrap_love_dll_graphics_rectangle_with_rounded_corners(int mode_type, float x, float y, float w, float h, float rx, float ry, int points)
    {
        auto mode = (Graphics::DrawMode)mode_type;
        points = std::max(rx, ry) > 20.0 ? (int)(std::max(rx, ry) / 2) : 10;
        return wrap_catchexcept([&]() {graphicsInstance->rectangle(mode, x, y, w, h, rx, ry, points);});
    }
    bool4 wrap_love_dll_graphics_circle(int mode_type, float x, float y, float radius, int points)
    {
        auto mode = (Graphics::DrawMode)mode_type;
        points = radius > 10 ? (int)(radius) : 10;
        return wrap_catchexcept([&]() {graphicsInstance->circle(mode, x, y, radius, points);});
    }
    bool4 wrap_love_dll_graphics_ellipse(int mode_type, float x, float y, float a, float b, int points)
    {
        auto mode = (Graphics::DrawMode)mode_type;
        points = a + b > 30 ? (int)((a + b) / 2) : 15;

        return wrap_catchexcept([&]() {graphicsInstance->ellipse(mode, x, y, a, b, points);});
    }
    bool4 wrap_love_dll_graphics_arc(int mode_type, int arcmode_type, float x, float y, float radius, float angle1, float angle2)
    {
        auto drawmode = (Graphics::DrawMode)mode_type;
        auto arcmode = (Graphics::ArcMode)arcmode_type;//Graphics::ARC_PIE;

        int points = (int)radius;
        float angle = fabs(angle1 - angle2);

        // The amount of points is based on the fraction of the circle created by the arc.
        if (angle < 2.0f * (float)LOVE_M_PI)
            points *= angle / (2.0f * (float)LOVE_M_PI);

        points = std::max(points, 10);

        return wrap_catchexcept([&]() {graphicsInstance->arc(drawmode, arcmode, x, y, radius, angle1, angle2, points);});
    }
    bool4 wrap_love_dll_graphics_arc_points(int mode_type, int arcmode_type, float x, float y, float radius, float angle1, float angle2, int points)
    {
        auto drawmode = (Graphics::DrawMode)mode_type;
        auto arcmode = (Graphics::ArcMode)arcmode_type; //Graphics::ARC_PIE;
        return wrap_catchexcept([&]() {graphicsInstance->arc(drawmode, arcmode, x, y, radius, angle1, angle2, points);});
    }

    bool4 wrap_love_dll_graphics_points(Float2 coords[], int coordsLength)
    {
        int numpoints = coordsLength;
        float *coords_buffer = new float[numpoints * 2];

        for (int i = 0; i < coordsLength; i++)
        {
            coords_buffer[i * 2] = coords[i].x;
            coords_buffer[i * 2 + 1] = coords[i].y;
        }

        bool res = wrap_catchexcept([&]() {graphicsInstance->points(coords_buffer, nullptr, numpoints);});
        delete[] coords_buffer;
        return res;
    }
    bool4 wrap_love_dll_graphics_points_colors(Float2 coords[], Int4 colors[], int coordsLength)
    {
        int numpoints = coordsLength;
        float *coords_buffer = new float[numpoints * 2];
        uint8 *colors_buffer = new uint8[numpoints * 4];

        for (int i = 0; i < coordsLength; i++)
        {
            coords_buffer[i * 2] = coords[i].x;
            coords_buffer[i * 2 + 1] = coords[i].y;
            
            colors_buffer[i * 4 + 0] = colors[i].r;
            colors_buffer[i * 4 + 1] = colors[i].g;
            colors_buffer[i * 4 + 2] = colors[i].b;
            colors_buffer[i * 4 + 3] = colors[i].a;
        }

        bool res = wrap_catchexcept([&]() {graphicsInstance->points(coords_buffer, colors_buffer, numpoints);});

        delete[] coords_buffer;
        delete[] colors_buffer;
        return res;
    }
    bool4 wrap_love_dll_graphics_line(Float2 coords[], int coordsLength)
    {
        if (coordsLength < 2)
        {
            wrap_ee("Need at least two vertices to draw a line");
            return false;
        }

        int numpoints = coordsLength;
        float *coords_buffer = new float[numpoints * 2];

        for (int i = 0; i < coordsLength; i++)
        {
            coords_buffer[i * 2] = coords[i].x;
            coords_buffer[i * 2 + 1] = coords[i].y;
        }

        bool res = wrap_catchexcept([&]() {graphicsInstance->polyline(coords_buffer, numpoints * 2);});

        delete[] coords_buffer;
        return res;
    }
    bool4 wrap_love_dll_graphics_polygon(int mode_type, Float2 coords[], int coordsLength)
    {
        if (coordsLength < 3)
        {
            wrap_ee("%s", "Need at least three vertices to draw a polygon");
            return false;
        }

        auto mode = (Graphics::DrawMode)mode_type;

        // fetch coords
        float *coords_buffer = new float[coordsLength * 2 + 2];
        for (int i = 0; i < coordsLength; ++i)
        {
            coords_buffer[i * 2 + 0] = coords[i].x;
            coords_buffer[i * 2 + 1] = coords[i].y;
        }
        // make a closed loop
        coords_buffer[coordsLength * 2] = coords[0].x;
        coords_buffer[coordsLength * 2 + 1] = coords[0].y;

        bool res = wrap_catchexcept([&]() {graphicsInstance->polygon(mode, coords_buffer, coordsLength * 2 + 2);});
        delete[] coords_buffer;

        return res;
    }

#pragma endregion

#pragma region graphics Window

    void wrap_love_dll_graphics_isCreated(bool4 *out_reslut)
    {
        *out_reslut = graphicsInstance->isCreated();
    }
    void wrap_love_dll_graphics_getWidth(int *out_width)
    {
        *out_width = graphicsInstance->getWidth();
    }
    void wrap_love_dll_graphics_getHeight(int *out_height)
    {
        *out_height = graphicsInstance->getHeight();
    }

#pragma endregion

#pragma region graphics System Information

    void wrap_love_dll_graphics_getSupported(int feature_type, bool4 *out_result)
    {
        auto feature = (Graphics::Feature) feature_type;
        *out_result = graphicsInstance->isSupported(feature);
    }
    void wrap_love_dll_graphics_getCanvasFormats(int format_type, bool4 *out_result)
    {
        auto format = (love::graphics::opengl::Canvas::Format)format_type;
        *out_result = love::graphics::opengl::Canvas::isFormatSupported(format);
    }
    void wrap_love_dll_graphics_getCompressedImageFormats(int format_type, bool4 *out_result)
    {
        auto format = (image::CompressedImageData::Format) format_type;
        *out_result = love::graphics::opengl::Image::hasCompressedTextureSupport(format, false);
    }
    void wrap_love_dll_graphics_getRendererInfo(WrapSequenceString** out_wss)
    {
        Graphics::RendererInfo info;
        wrap_catchexcept([&]() { info = graphicsInstance->getRendererInfo(); });

        std::vector<std::string> infoStr;
        infoStr.reserve(4);
        infoStr.push_back(info.name);
        infoStr.push_back(info.version);
        infoStr.push_back(info.vendor);
        infoStr.push_back(info.device);

        *out_wss = new_WrapSequenceString(infoStr);
    }
    void wrap_love_dll_graphics_getSystemLimits(int limit_type, double *out_result)
    {
        Graphics::SystemLimit limit = (Graphics::SystemLimit)limit_type;
        *out_result = graphicsInstance->getSystemLimit(limit);
    }
    void wrap_love_dll_graphics_getStats(int *out_drawCalls, int *out_canvasSwitches, int *out_shaderSwitches, int *out_canvases, int *out_images, int *out_fonts, int *out_textureMemory)
    {
        Graphics::Stats stats = graphicsInstance->getStats();
        *out_drawCalls = stats.drawCalls;
        *out_canvasSwitches = stats.canvasSwitches;
        *out_shaderSwitches = stats.shaderSwitches;
        *out_canvases = stats.canvases;
        *out_images = stats.images;
        *out_fonts = stats.fonts;
        *out_textureMemory = stats.textureMemory;
    }

#pragma endregion



#pragma region type - Source

    bool4 wrap_love_dll_type_Source_clone(Source *t, Source **out_clone)
    {
        return  wrap_catchexcept( [&]() { *out_clone = t->clone();  });
    }

    void wrap_love_dll_type_Source_play(Source *t, bool4 *out_success)
    {
        *out_success = t->play();
    }

    void wrap_love_dll_type_Source_stop(Source *t)
    {
        t->stop();
    }

    void wrap_love_dll_type_Source_pause(Source *t)
    {
        t->pause();
    }

    void wrap_love_dll_type_Source_resume(Source *t)
    {
        t->resume();
    }

    void wrap_love_dll_type_Source_rewind(Source *t)
    {
        t->rewind();
    }

    bool4 wrap_love_dll_type_Source_setPitch(Source *t, float pitch)
    {
        if (pitch > std::numeric_limits<float>::max() ||
            pitch < std::numeric_limits<float>::min())
        {
            wrap_ee("Pitch has to be finite and not NaN.");
            return false;
        }
        t->setPitch(pitch);
        return true;
    }

    void wrap_love_dll_type_Source_getPitch(Source *t, float *out_pitch)
    {
        *out_pitch = t->getPitch();
    }

    void wrap_love_dll_type_Source_setVolume(Source *t, float volume)
    {
        t->setVolume(volume);
    }

    void wrap_love_dll_type_Source_getVolume(Source *t, float *out_volume)
    {
        *out_volume = t->getVolume();
    }

    void wrap_love_dll_type_Source_seek(Source *t, float offset, int unit_type)
    {
        Source::Unit u = (Source::Unit)unit_type;//Source::UNIT_SECONDS;
        t->seek(offset, u);
    }

    void wrap_love_dll_type_Source_tell(Source *t, int unit_type, float *out_position)
    {
        Source::Unit u = (Source::Unit)unit_type;//Source::UNIT_SECONDS;
        *out_position = t->tell(u);
    }

    void wrap_love_dll_type_Source_getDuration(Source *t, int unit_type, float *out_duration)
    {
        Source::Unit u = (Source::Unit)unit_type;//Source::UNIT_SECONDS;
        *out_duration = t->getDuration(u);
    }

    bool4 wrap_love_dll_type_Source_setPosition(Source *t, float x, float y, float z)
    {
        float v[3];
        v[0] = x;
        v[1] = y;
        v[2] = x;
        return wrap_catchexcept([&]() { t->setPosition(v); });
    }

    bool4 wrap_love_dll_type_Source_getPosition(Source *t, float *out_x, float *out_y, float *out_z)
    {
        float v[3];
        return wrap_catchexcept( [&]() { 
            t->getPosition(v);
            *out_x = v[0];
            *out_y = v[1];
            *out_z = v[2];
        });
    }

    bool4 wrap_love_dll_type_Source_setVelocity(Source *t, float x, float y, float z)
    {
        float v[3];
        v[0] = x;
        v[1] = y;
        v[2] = x;
        return wrap_catchexcept([&]() { t->setVelocity(v); });
    }

    bool4 wrap_love_dll_type_Source_getVelocity(Source *t, float *out_x, float *out_y, float *out_z)
    {
        float v[3];
        return wrap_catchexcept( [&]() { 
            t->getVelocity(v);
            *out_x = v[0];
            *out_y = v[1];
            *out_z = v[2];
        });
    }

    bool4 wrap_love_dll_type_Source_setDirection(Source *t, float x, float y, float z)
    {
        float v[3];
        v[0] = x;
        v[1] = y;
        v[2] = x;
        return wrap_catchexcept( [&]() { t->setDirection(v); });
    }

    bool4 wrap_love_dll_type_Source_getDirection(Source *t, float *out_x, float *out_y, float *out_z)
    {
        float v[3];
        return wrap_catchexcept( [&]() { 
            t->getDirection(v);
            *out_x = v[0];
            *out_y = v[1];
            *out_z = v[2];
        });
    }

    bool4 wrap_love_dll_type_Source_setCone(Source *t, float innerAngle, float outerAngle, float outerVolume)
    {
        return wrap_catchexcept( [&]() { t->setCone(innerAngle, outerAngle, outerVolume); });
    }

    bool4 wrap_love_dll_type_Source_getCone(Source *t, float *out_innerAngle, float *out_outerAngle, float *out_outerVolume)
    {
        return wrap_catchexcept( [&]() { t->getCone(*out_innerAngle, *out_outerAngle, *out_outerVolume); });
    }

    bool4 wrap_love_dll_type_Source_setRelative(Source *t, bool4 relative)
    {
        return wrap_catchexcept( [&]() { t->setRelative(relative); });
    }

    bool4 wrap_love_dll_type_Source_isRelative(Source *t, bool4 *out_relative)
    {
        return wrap_catchexcept( [&]() {  *out_relative = t->isRelative(); });
    }

    void wrap_love_dll_type_Source_setLooping(Source *t, bool4 looping)
    {
        t->setLooping(looping);
    }

    void wrap_love_dll_type_Source_isLooping(Source *t, bool4 *out_result)
    {
        *out_result = t->isLooping();
    }

    void wrap_love_dll_type_Source_isStopped(Source *t, bool4 *out_result)
    {
        *out_result = t->isStopped();
    }

    void wrap_love_dll_type_Source_isPaused(Source *t, bool4 *out_result)
    {
        *out_result = t->isPaused();
    }

    void wrap_love_dll_type_Source_isPlaying(Source *t, bool4 *out_result)
    {
        *out_result = !t->isStopped() && !t->isPaused();
    }

    bool4 wrap_love_dll_type_Source_setVolumeLimits(Source *t, float vmin, float vmax)
    {
        if (vmin < .0f || vmin > 1.f || vmax < .0f || vmax > 1.f)
        {
            wrap_ee("Invalid volume limits: [%f:%f]. Must be in [0:1]", vmin, vmax);
            return false;
        }
        t->setMinVolume(vmin);
        t->setMaxVolume(vmax);
        return true;
    }

    void wrap_love_dll_type_Source_getVolumeLimits(Source *t, float *out_vmin, float *out_vmax)
    {
        *out_vmin = t->getMinVolume();
        *out_vmax = t->getMaxVolume();
    }

    bool4 wrap_love_dll_type_Source_setAttenuationDistances(Source *t, float dref, float dmax)
    {
        if (dref < .0f || dmax < .0f)
        {
            wrap_ee("Invalid distances: %f, %f. Must be > 0", dref, dmax);
            return false;
        }
        return wrap_catchexcept( [&]() {
            t->setReferenceDistance(dref);
            t->setMaxDistance(dmax);
        });
    }

    bool4 wrap_love_dll_type_Source_getAttenuationDistances(Source *t, float *out_dref, float *out_dmax)
    {
        return wrap_catchexcept( [&]() {
            *out_dref = t->getReferenceDistance();
            *out_dmax = t->getMaxDistance();
        });
    }

    bool4 wrap_love_dll_type_Source_setRolloff(Source *t, float rolloff)
    {
        if (rolloff < .0f)
        {
            wrap_ee("Invalid rolloff: %f. Must be > 0.", rolloff);
            return false;
        }
        return wrap_catchexcept( [&]() { t->setRolloffFactor(rolloff); });
    }

    bool4 wrap_love_dll_type_Source_getRolloff(Source *t, float *out_rolloff)
    {
        return wrap_catchexcept( [&]() { *out_rolloff = t->getRolloffFactor(); });
    }

    void wrap_love_dll_type_Source_getChannels(Source *t, int *out_chanels)
    {
        *out_chanels = t->getChannels();
    }

    void wrap_love_dll_type_Source_getType(Source *t, int *out_type)
    {
        Source::Type type = t->getType();
        *out_type = type;
    }
#pragma endregion

#pragma region type - File

    bool4 wrap_love_dll_type_File_getSize(File *file, double *out_size)
    {
        int64 size = -1;
        try
        {
            size = file->getSize();
        }
        catch (love::Exception &e)
        {
            return wrap_ee("%s", e.what());
        }

        if (size == -1)
        {
            wrap_ee("Could not determine file size.");
            return false;
        }
        else if (size >= 0x20000000000000LL)
        {
            wrap_ee("Size is too large.");
            return false;
        }

        *out_size = size;
        return true;
    }

    bool4 wrap_love_dll_type_File_open(File *file, int mode_type)
    {
        File::Mode mode = (File::Mode)mode_type;
        return wrap_catchexcept([&]() {file->open(mode);});
    }

    void wrap_love_dll_type_File_close(File *file, bool4 *out_result)
    {
        *out_result = file->close();
    }

    void wrap_love_dll_type_File_isOpen(File *file, bool4 *out_result)
    {
        *out_result = file->isOpen();
    }

    bool4 wrap_love_dll_type_File_read(File *file, int64 size, void **out_data, int64 *out_readedSize)
    {
        StrongRef<Data> d = nullptr;

        return wrap_catchexcept([&]() {
            d.set(file->read(size), Acquire::NORETAIN);
            *out_data = d->getData();
            *out_readedSize = d->getSize();
        });
    }

    bool4 wrap_love_dll_type_File_write_String(File *file, const char *data, int64 datasize)
    {
        return wrap_catche_bool([&]() {
            return file->write(data, datasize);
        });
    }

    bool4 wrap_love_dll_type_File_write_Data_datasize(File *file, Data *data, int64 datasize)
    {
        return wrap_catche_bool([&]() {
            return file->write(data, datasize);
        });
    }

    bool4 wrap_love_dll_type_File_flush(File *file)
    {
        return wrap_catche_bool([&]() {
            return file->flush();
        });
    }

    void wrap_love_dll_type_File_isEOF(File *file, bool4 *out_result)
    {
        *out_result = file->isEOF();
    }

    bool4 wrap_love_dll_type_File_tell(File *file, int64 *out_pos)
    {
        *out_pos = file->tell();
        // Push nil on failure or if pos does not fit into a double precision floating-point number.
        if (*out_pos == -1)
        {
            wrap_ee("Invalid position.");
            return false;
        }
        else if (*out_pos >= 0x20000000000000LL)
        {
            wrap_ee("Number is too large.");
            return false;
        }
        return true;
    }

    bool4 wrap_love_dll_type_File_seek(File *file, int64 pos)
    {
        // Push false on negative and precision-problematic numbers.
        // Better fail than seek to an unknown position.
        if (pos < 0.0 || pos >= 9007199254740992.0)
        {
            return false;
        }

        return file->seek((uint64)pos);
    }

    bool4 wrap_love_dll_type_File_setBuffer(File *file, int bufmode_type, int64 size)
    {
        return wrap_catche_bool([&]() {
            File::BufferMode bufmode = (File::BufferMode)bufmode_type;
            return file->setBuffer(bufmode, size);
        });
    }

    void wrap_love_dll_type_File_getBuffer(File *file, int *out_bufmode_type, int64 *out_size)
    {
        File::BufferMode bufmode = file->getBuffer(*out_size);
        *out_bufmode_type = (int)bufmode;
    }

    void wrap_love_dll_type_File_getMode(File *file, int *out_mode_type)
    {
        File::Mode mode = file->getMode();
        *out_mode_type = (int)mode;
    }

    void wrap_love_dll_type_File_getFilename(File *file, WrapString **out_filename)
    {
        *out_filename = new_WrapString(file->getFilename().c_str());
    }

    void wrap_love_dll_type_File_getExtension(File *file, WrapString **out_extension)
    {
        *out_extension = new_WrapString(file->getExtension());
    }
#pragma endregion

#pragma region type - FileData

    void wrap_love_dll_type_FileData_getFilename(FileData *t, WrapString **out_filename)
    {
        *out_filename = new_WrapString(t->getFilename());
    }

    void wrap_love_dll_type_FileData_getExtension(FileData *t, WrapString **out_extension)
    {
        *out_extension = new_WrapString(t->getExtension());
    }
#pragma endregion

#pragma region type - GlyphData

    void wrap_love_dll_type_GlyphData_getWidth(GlyphData *t, int *out_width)
    {
        *out_width = t->getWidth();
    }

    void wrap_love_dll_type_GlyphData_getHeight(GlyphData *t, int *out_height)
    {
        *out_height = t->getHeight();
    }

    void wrap_love_dll_type_GlyphData_getGlyph(GlyphData *t, uint32 *out_glyph)
    {
        *out_glyph = t->getGlyph();
    }

    bool4 wrap_love_dll_type_GlyphData_getGlyphString(GlyphData *t, WrapString **out_str)
    {
        return wrap_catchexcept([&]() { *out_str = new_WrapString(t->getGlyphString()); });
    }

    void wrap_love_dll_type_GlyphData_getAdvance(GlyphData *t, int *out_advance)
    {
        *out_advance = t->getAdvance();
    }

    void wrap_love_dll_type_GlyphData_getBearing(GlyphData *t, int *out_bearingX, int *out_bearingY)
    {
        *out_bearingX = t->getBearingX();
        *out_bearingY = t->getBearingY();
    }

    void wrap_love_dll_type_GlyphData_getBoundingBox(GlyphData *t, int *out_minX, int *out_minY, int *out_width, int *out_height)
    {
        *out_minX = t->getMinX();
        *out_minY = t->getMinY();
        int maxX = t->getMaxX();
        int maxY = t->getMaxY();

        *out_width = maxX - *out_minX;
        *out_height = maxY - *out_minY;
    }

    void wrap_love_dll_type_GlyphData_getFormat(GlyphData *t, int *out_format_type)
    {
        *out_format_type = t->getFormat();
    }

#pragma endregion

#pragma region type - Rasterizer

    void wrap_love_dll_type_Rasterizer_getHeight(Rasterizer *t, int *out_heigth)
    {
        *out_heigth = t->getHeight();
    }

    void wrap_love_dll_type_Rasterizer_getAdvance(Rasterizer *t, int *out_advance)
    {
        *out_advance = t->getAdvance();
    }

    void wrap_love_dll_type_Rasterizer_getAscent(Rasterizer *t, int *out_ascent)
    {
        *out_ascent = t->getAscent();
    }

    void wrap_love_dll_type_Rasterizer_getDescent(Rasterizer *t, int *out_descent)
    {
        *out_descent = t->getDescent();
    }

    void wrap_love_dll_type_Rasterizer_getLineHeight(Rasterizer *t, int *out_lineHeight)
    {
        *out_lineHeight = t->getLineHeight();
    }

    bool4 wrap_love_dll_type_Rasterizer_getGlyphData_uint32(Rasterizer *t, uint32 glyph, GlyphData **out_g)
    {
        return wrap_catchexcept([&]() {
            *out_g = t->getGlyphData(glyph);
        });
    }

    bool4 wrap_love_dll_type_Rasterizer_getGlyphData_string(Rasterizer *t, const char *str, GlyphData **out_g)
    {
        return wrap_catchexcept([&]() {
            std::string glyph(str);
            *out_g = t->getGlyphData(glyph);
            (*out_g)->retain();
        });
    }

    void wrap_love_dll_type_Rasterizer_getGlyphCount(Rasterizer *t, int *out_count)
    {
        *out_count = t->getGlyphCount();
    }

    bool4 wrap_love_dll_type_Rasterizer_hasGlyphs_uint32(Rasterizer *t, uint32 glyph, bool4 *out_result)
    {
        return wrap_catchexcept([&]() {
            *out_result = t->hasGlyph(glyph);
        });
    }

    bool4 wrap_love_dll_type_Rasterizer_hasGlyphs_string(Rasterizer *t, const char *str, bool4 *out_result)
    {
        return wrap_catchexcept([&]() {
            std::string glyph(str);
            *out_result = t->hasGlyphs(glyph);
        });
    }

#pragma endregion

#pragma region type - Canvas
    

    void wrap_love_dll_type_Canvas_getFormat(Canvas *canvas, int *out_format_type)
    {
        Canvas::Format format = canvas->getTextureFormat();
        *out_format_type = format;
    }

    void wrap_love_dll_type_Canvas_getMSAA(Canvas *canvas, int *out_msaa)
    {
        *out_msaa = canvas->getMSAA();
    }

#pragma endregion

#pragma region type - Font

    void wrap_love_dll_type_Font_getHeight(love::graphics::opengl::Font *t, int *out_height)
    {
        *out_height = t->getHeight();
    }

    bool4 wrap_love_dll_type_Font_getWidth(love::graphics::opengl::Font *t, const char *str, int *out_width)
    {
        return wrap_catchexcept([&]() { *out_width = t->getWidth(str); });
    }

    bool4 wrap_love_dll_type_Font_getWrap(love::graphics::opengl::Font *t, 
        Int4 coloredStringColor[], const pchar coloredStringText[], int coloredStringLength, float wrap,
        int *out_maxWidth, WrapSequenceString **out_pws)
    {
        std::vector<love::graphics::opengl::Font::ColoredString> strings;
        strings.reserve(coloredStringLength);

        for (int i = 0; i < coloredStringLength; i++)
        {
            love::graphics::opengl::Font::ColoredString coloredstr;
            coloredstr.color.r = coloredStringColor[i].r;
            coloredstr.color.g = coloredStringColor[i].g;
            coloredstr.color.b = coloredStringColor[i].b;
            coloredstr.color.a = coloredStringColor[i].a;
            coloredstr.str = coloredStringText[i];
        }


        int max_width = 0;
        std::vector<std::string> lines;
        std::vector<int> widths;

        return wrap_catchexcept([&]() { 
            t->getWrap(strings, wrap, lines, &widths); 


            for (int width : widths)
                max_width = std::max(max_width, width);

            *out_maxWidth = max_width;
            *out_pws = new_WrapSequenceString(lines);
        });
    }

    void wrap_love_dll_type_Font_setLineHeight(love::graphics::opengl::Font *t, float h)
    {
        t->setLineHeight(h);
    }

    void wrap_love_dll_type_Font_getLineHeight(love::graphics::opengl::Font *t, float *out_h)
    {
        *out_h = t->getLineHeight();
    }

    bool4 wrap_love_dll_type_Font_setFilter(love::graphics::opengl::Font *t, int min_type, int mag_type, float anisotropy)
    {
        Texture::Filter f = t->getFilter();

        f.min = (Texture::FilterMode)min_type;
        f.mag = (Texture::FilterMode)mag_type;
        f.anisotropy = anisotropy;

        return wrap_catchexcept([&]() { t->setFilter(f); });
    }

    void wrap_love_dll_type_Font_getFilter(love::graphics::opengl::Font *t, int *out_min_type, int *out_mag_type, float *out_anisotropy)
    {
        const Texture::Filter f = t->getFilter();
        *out_min_type = f.min;
        *out_mag_type = f.mag;
        *out_anisotropy = f.anisotropy;
    }

    void wrap_love_dll_type_Font_getAscent(love::graphics::opengl::Font *t, int *out_ascent)
    {
        *out_ascent = t->getAscent();
    }

    void wrap_love_dll_type_Font_getDescent(love::graphics::opengl::Font *t, int *out_descent)
    {
        *out_descent = t->getDescent();
    }

    void wrap_love_dll_type_Font_getBaseline(love::graphics::opengl::Font *t, float*out_baseline)
    {
        *out_baseline = t->getBaseline();
    }

    bool4 wrap_love_dll_type_Font_hasGlyphs_uint32(love::graphics::opengl::Font *t, uint32 chr, bool4 *out_result)
    {
        return wrap_catchexcept([&]() {
            *out_result = t->hasGlyph(chr);
        });
    }

    bool4 wrap_love_dll_type_Font_hasGlyphs_string(love::graphics::opengl::Font *t, const char *str, bool4 *out_result)
    {
        return wrap_catchexcept([&]() {
            std::string text(str);
            *out_result = t->hasGlyphs(text);
        });
    }

    bool4 wrap_love_dll_type_Font_setFallbacks(love::graphics::opengl::Font *t, love::graphics::opengl::Font **fallback, int length)
    {
        std::vector<love::graphics::opengl::Font *> fallbacks;

        for (int i = 0; i < length; i++)
        {
            fallbacks.push_back(fallback[i]);
        }

        return wrap_catchexcept([&]() { t->setFallbacks(fallbacks); });
    }

#pragma endregion

#pragma region type - Image

    bool4 wrap_love_dll_type_Image_setMipmapFilter(love::graphics::opengl::Image *i, int mipmap_type,float sharpness)
    {
        Texture::Filter f = i->getFilter();
        f.mipmap = (Texture::FilterMode)mipmap_type;
        return wrap_catchexcept([&]() { 
            i->setFilter(f);
            i->setMipmapSharpness(sharpness);
        });
    }

    void wrap_love_dll_type_Image_getMipmapFilter(love::graphics::opengl::Image *i, int *out_mipmap_type, float *out_sharpness)
    {
        const Texture::Filter &f = i->getFilter();
        *out_mipmap_type = f.mipmap;
        *out_sharpness = i->getMipmapSharpness();
    }

    void wrap_love_dll_type_Image_isCompressed(love::graphics::opengl::Image *i, bool4 *out_result)
    {
        *out_result = i->isCompressed();
    }

    bool4 wrap_love_dll_type_Image_refresh(love::graphics::opengl::Image *i, int xoffset, int yoffset, int w, int h)
    {
        return wrap_catchexcept([&]() { i->refresh(xoffset, yoffset, w, h); });
    }

    void wrap_love_dll_type_Image_getData(love::graphics::opengl::Image *i, Data ***out_datas, int *out_datas_lenght)
    {
        int n = 0;
        if (i->isCompressed())
        {
            *out_datas = new Data*[i->getCompressedData().size()];
            for (const auto &cdata : i->getCompressedData())
            {
                (*out_datas)[n] = cdata.get();
                (*out_datas)[n]->retain();
                n++;
            }
        }
        else
        {
            *out_datas = new Data*[i->getImageData().size()];
            for (const auto &data : i->getImageData())
            {
                (*out_datas)[n] = data.get();
                (*out_datas)[n]->retain();
                n++;
            }
        }

        *out_datas_lenght = n;
    }

    void wrap_love_dll_type_Image_getFlags(love::graphics::opengl::Image* i, bool4 *out_mipmaps, bool4 *out_linear)
    {
        love::graphics::opengl::Image::Flags flags = i->getFlags();
        *out_mipmaps = flags.mipmaps;
        *out_linear = flags.linear;
    }

#pragma endregion

#pragma region type - Mesh


    static inline size_t inline_type_Mesh_writeByteData(char *destData, char *srcData, int components)
    {
        uint8 *componentdata = (uint8 *)destData;

        for (int i = 0; i < components; i++)
            componentdata[i] = ((uint8*)srcData)[i];

        return sizeof(uint8) * components;
    }

    static inline size_t inline_type_Mesh_writeFloatData(char *destData, char *srcData, int components)
    {
        float *componentdata = (float *)destData;

        for (int i = 0; i < components; i++)
            componentdata[i] = ((float*)srcData)[i];

        return sizeof(float) * components;
    }

    int inline_type_Mesh_writeAttributeData(char *destData, char *srcData, Mesh::DataType type, int components)
    {
        switch (type)
        {
        case Mesh::DATA_BYTE:
            return inline_type_Mesh_writeByteData(destData, srcData, components );
        case Mesh::DATA_FLOAT:
            return inline_type_Mesh_writeFloatData(destData, srcData, components);
        default:
            return 0;
        }
    }

    bool4 wrap_love_dll_type_Mesh_setVertices_data(Mesh *t, Data *d, uint32 vertoffset)
    {
        if (vertoffset >= t->getVertexCount())
        {
            wrap_ee("Invalid vertex start index (must be between 1 and %d)", (int)t->getVertexCount());
            return false;
        }

        size_t stride = t->getVertexStride();
        size_t byteoffset = vertoffset * stride;

        size_t datasize = std::min(d->getSize(), (t->getVertexCount() - vertoffset) * stride);
        char *bytedata = (char *)t->mapVertexData() + byteoffset;

        memcpy(bytedata, d->getData(), datasize);

        t->unmapVertexData(byteoffset, datasize);
        return true;
    }

    bool4 wrap_love_dll_type_Mesh_setVertices(Mesh *t, uint32 vertoffset, char *srcData, uint32 nvertices)
    {
        if (vertoffset >= t->getVertexCount())
        {
            wrap_ee("Invalid vertex start index (must be between 1 and %d)", (int)t->getVertexCount());
            return false;
        }

        size_t stride = t->getVertexStride();
        size_t byteoffset = vertoffset * stride;

        if (vertoffset + nvertices > t->getVertexCount())
        {
            wrap_ee("Too many vertices (expected at most %d, got %d)", (int)t->getVertexCount() - (int)vertoffset, (int)nvertices);
            return false;
        }

        const std::vector<Mesh::AttribFormat> &vertexformat = t->getVertexFormat();
        char *data = (char *)t->mapVertexData() + byteoffset;

        for (size_t i = 0; i < nvertices; i++)
        {
            const Mesh::AttribFormat &format = vertexformat[i];
            int offset = inline_type_Mesh_writeAttributeData(data, srcData, format.type, format.components);
            data += offset;
            srcData += offset;
        }

        t->unmapVertexData(byteoffset, nvertices * stride);
        return true;
    }

    bool4 wrap_love_dll_type_Mesh_setVertex(Mesh *t, uint32 index, const char* data, int data_length)
    {
        return wrap_catchexcept([&]() { 
            t->setVertex(index, data, data_length);
        });
    }

    bool4 wrap_love_dll_type_Mesh_getVertex(Mesh *t, uint32 index, char **out_data, int *out_data_length, int *out_count)
    {
        const std::vector<Mesh::AttribFormat> &vertexformat = t->getVertexFormat();

        char *data = (char *)t->getVertexScratchBuffer();

        return wrap_catchexcept([&]() { 
            t->getVertex(index, data, t->getVertexStride());

            uint32 size = 0;
            for (int i = index; i < t->getVertexCount(); i++)
            {
                auto& format = vertexformat[i];
                if (format.type == Mesh::DATA_BYTE)
                {
                    size += sizeof(uint8) * format.components;
                }
                else if (format.type == Mesh::DATA_FLOAT)
                {
                    size += sizeof(float) * format.components;
                }
            }
            char *buffer = new char[size];

            uint32 srcOffset = 0;
            for (int i = 0; i < index; i++)
            {
                auto& format = vertexformat[i];
                if (format.type == Mesh::DATA_BYTE)
                {
                    srcOffset += sizeof(uint8) * format.components;
                }
                else if (format.type == Mesh::DATA_FLOAT)
                {
                    srcOffset += sizeof(float) * format.components;
                }
            }

            uint32 destOffset = 0;
            for (int i = index; i < t->getVertexCount(); i++)
            {
                auto& format = vertexformat[i];
                int offset = inline_type_Mesh_writeAttributeData(buffer + destOffset, data + srcOffset, format.type, format.components);
                srcOffset += offset;
                destOffset += offset;
            }

            *out_data = buffer;
            *out_data_length = size;
            *out_count = t->getVertexCount() - index;
        });
    }

    bool4 wrap_love_dll_type_Mesh_setVertexAttribute(Mesh *t, uint32 vertindex, int attribindex, uint32 data0, uint32 data1, uint32 data2, uint32 data3)
    {
        return wrap_catchexcept([&]() {

            int components;
            Mesh::DataType type = t->getAttributeInfo(attribindex, components);

            // Maximum possible size for a single vertex attribute.
            char data[sizeof(float) * 4];
            ((uint32*)data)[0] = data0;
            ((uint32*)data)[1] = data1;
            ((uint32*)data)[2] = data2;
            ((uint32*)data)[3] = data3;

             t->setVertexAttribute(vertindex, attribindex, data, sizeof(float) * 4); 
        });
    }

    bool4 wrap_love_dll_type_Mesh_getVertexAttribute(Mesh *t, uint32 vertindex, int attribindex, int *out_type, int *out_components,
        uint32 *out_data0, uint32 *out_data1, uint32 *out_data2, uint32 *out_data3)
    {
        return wrap_catchexcept([&]() { 

            Mesh::DataType type;
            int components;
            type = t->getAttributeInfo(attribindex, components);

            *out_type = type;
            *out_components = components;

            // Maximum possible size for a single vertex attribute.
            char data[sizeof(float) * 4];

            t->getVertexAttribute(vertindex, attribindex, data, sizeof(float) * 4);

            *out_data0 = ((uint32*)data)[0];
            *out_data1 = ((uint32*)data)[1];
            *out_data2 = ((uint32*)data)[2];
            *out_data3 = ((uint32*)data)[3];
        });
    }

    void wrap_love_dll_type_Mesh_getVertexCount(Mesh *t, int *out_result)
    {
        *out_result = t->getVertexCount();
    }

    void wrap_love_dll_type_Mesh_getVertexFormat(Mesh *t, WrapSequenceString **out_names, int **out_datatype, int **out_components, int *out_length)
    {
        const std::vector<Mesh::AttribFormat> &vertexformat = t->getVertexFormat();
        const char *tname = nullptr;
        auto wss = new WrapSequenceString();
        *out_length = vertexformat.size();

        *out_names = new WrapSequenceString();
        (*out_names)->len = vertexformat.size();
        (*out_names)->sequence = new pchar[vertexformat.size()];
        *out_datatype = new int[vertexformat.size()];
        *out_components = new int[vertexformat.size()];

        for (size_t i = 0; i < vertexformat.size(); i++)
        {
            (*out_names)->sequence[i] = new char[vertexformat[i].name.size() + 1];
            strcpy((*out_names)->sequence[i], vertexformat[i].name.c_str());

            (*out_datatype)[i] = vertexformat[i].type;
            (*out_components)[i] = vertexformat[i].components;
        }
    }

    bool4 wrap_love_dll_type_Mesh_setAttributeEnabled(Mesh *t, const char *name, bool4 enable)
    {
        return wrap_catchexcept([&]() { t->setAttributeEnabled(name, enable); });
    }

    bool4 wrap_love_dll_type_Mesh_isAttributeEnabled(Mesh *t, const char *name, bool4 *out_result)
    {
        return wrap_catchexcept([&]() { *out_result = t->isAttributeEnabled(name); });
    }

    bool4 wrap_love_dll_type_Mesh_attachAttribute(Mesh *t, const char *name, Mesh *otherMesh)
    {
        return wrap_catchexcept([&]() { t->attachAttribute(name, otherMesh); });
    }

    void wrap_love_dll_type_Mesh_flush(Mesh *t)
    {
        t->flush();
    }

    bool4 wrap_love_dll_type_Mesh_setVertexMap_nil(Mesh *t)
    {
        // Disable the vertex map / index buffer.
        return wrap_catchexcept([&]() { t->setVertexMap(); });
    }

    bool4 wrap_love_dll_type_Mesh_setVertexMap(Mesh *t, uint32 *vertexmaps, int vertexmaps_length)
    {
        std::vector<uint32> vertexmap;
        vertexmap.reserve(vertexmaps_length);
        for (int i = 0; i < vertexmaps_length; i++)
        {
            vertexmap.push_back(vertexmaps[i]);
        }

        return wrap_catchexcept([&]() { t->setVertexMap(vertexmap); });
    }

    bool4 wrap_love_dll_type_Mesh_getVertexMap(Mesh *t, bool4 *out_has_vertex_map, uint32 **out_vertexmaps, int *out_vertexmaps_length)
    {
        std::vector<uint32> vertex_map;
        return wrap_catchexcept([&]() { 

            *out_has_vertex_map = false;
            *out_has_vertex_map = t->getVertexMap(vertex_map);

            if (!*out_has_vertex_map)
            {
                return;
            }

            int element_count = (int)vertex_map.size();
            *out_vertexmaps_length = element_count;
            *out_vertexmaps = new uint32[element_count];

            for (int i = 0; i < element_count; i++)
            {
                (*out_vertexmaps)[i] = vertex_map[i];
            }
        });
    }

    void wrap_love_dll_type_Mesh_setTexture_nil(Mesh *t)
    {
        t->setTexture();
    }

    void wrap_love_dll_type_Mesh_setTexture_Texture(Mesh *t, Texture *tex)
    {
        t->setTexture(tex);
    }

    bool4 wrap_love_dll_type_Mesh_getTexture(Mesh *t, Texture **out_texture)
    {
        Texture *tex = t->getTexture();

        if (tex == nullptr)
            return 0;

        // FIXME: big hack right here.
        if (typeid(*tex) == typeid(love::graphics::opengl::Image)) {}
        else if (typeid(*tex) == typeid(Canvas)) {}
        else
        {
            wrap_ee("Unable to determine texture type.");
            return false; 
        }

        *out_texture = tex;
        tex->retain();
        return true;
    }

    void wrap_love_dll_type_Mesh_setDrawMode(Mesh *t, int mode_type)
    {
        Mesh::DrawMode mode = (Mesh::DrawMode)mode_type;
        t->setDrawMode(mode);
    }

    void wrap_love_dll_type_Mesh_getDrawMode(Mesh *t, int *out_mode_type)
    {
        Mesh::DrawMode mode = t->getDrawMode();
        *out_mode_type = mode;
    }

    void wrap_love_dll_type_Mesh_setDrawRange(Mesh *t)
    {
        t->setDrawRange();
    }

    bool4 wrap_love_dll_type_Mesh_setDrawRange_minmax(Mesh *t, int rangemin, int rangemax)
    {
        return wrap_catchexcept([&]() { t->setDrawRange(rangemin, rangemax); });
    }

    bool4 wrap_love_dll_type_Mesh_getDrawRange(Mesh *t, int *out_rangemin, int *out_rangemax)
    {
        int rangemin = -1;
        int rangemax = -1;
        t->getDrawRange(rangemin, rangemax);

        if (rangemin < 0 || rangemax < 0)
            return false;

        *out_rangemin = rangemin;
        *out_rangemax = rangemax;

        return true;
    }

#pragma endregion

#pragma region type - ParticleSystem

    bool4 wrap_love_dll_type_ParticleSystem_clone(ParticleSystem *t, ParticleSystem **out_clone)
    {
        return wrap_catchexcept([&]() { *out_clone = t->clone(); }); // no need for retain
    }

    void wrap_love_dll_type_ParticleSystem_setTexture(ParticleSystem *t, Texture *tex)
    {
        t->setTexture(tex);
    }

    bool4 wrap_love_dll_type_ParticleSystem_getTexture(ParticleSystem *t, Texture **out_texture)
    {
        Texture *tex = t->getTexture();

        // FIXME: big hack right here.
        if (typeid(*tex) == typeid(love::graphics::opengl::Image)) {}
        else if (typeid(*tex) == typeid(Canvas)) {}
        else
        {
            wrap_ee("Unable to determine texture type.");
            return false;
        }

        *out_texture = tex;
        tex->retain();
        return true;
    }

    bool4 wrap_love_dll_type_ParticleSystem_setBufferSize(ParticleSystem *t, uint32 buffersize)
    {
        if (buffersize < 1 || buffersize > ParticleSystem::MAX_PARTICLES)
        {
            wrap_ee("Invalid buffer size");
            return false;
        }
        
        return wrap_catchexcept([&]() { t->setBufferSize(buffersize); });
    }

    void wrap_love_dll_type_ParticleSystem_getBufferSize(ParticleSystem *t, uint32 *out_buffersize)
    {
        *out_buffersize = t->getBufferSize();
    }

    void wrap_love_dll_type_ParticleSystem_setInsertMode(ParticleSystem *t, int mode_type)
    {
        ParticleSystem::InsertMode mode = (ParticleSystem::InsertMode)mode_type;
        t->setInsertMode(mode);
    }

    void wrap_love_dll_type_ParticleSystem_getInsertMode(ParticleSystem *t, int *out_mode_type)
    {
        ParticleSystem::InsertMode mode;
        mode = t->getInsertMode();
        *out_mode_type = mode;
    }

    bool4 wrap_love_dll_type_ParticleSystem_setEmissionRate(ParticleSystem *t, float rate)
    {
        return wrap_catchexcept([&]() { t->setEmissionRate(rate); });
    }

    void wrap_love_dll_type_ParticleSystem_getEmissionRate(ParticleSystem *t, float *out_rate)
    {
        *out_rate = t->getEmissionRate();
    }

    void wrap_love_dll_type_ParticleSystem_setEmitterLifetime(ParticleSystem *t, float lifetime)
    {
        t->setEmitterLifetime(lifetime);
    }

    void wrap_love_dll_type_ParticleSystem_getEmitterLifetime(ParticleSystem *t, float *out_lifetime)
    {
        *out_lifetime = t->getEmitterLifetime();
    }

    void wrap_love_dll_type_ParticleSystem_setParticleLifetime(ParticleSystem *t, float min, float max)
    {
        t->setParticleLifetime(min, max);
    }

    void wrap_love_dll_type_ParticleSystem_getParticleLifetime(ParticleSystem *t, int *out_min, int *out_max)
    {
        float min, max;
        t->getParticleLifetime(min, max);
        *out_min = min;
        *out_max = max;
    }

    void wrap_love_dll_type_ParticleSystem_setPosition(ParticleSystem *t, float x, float y)
    {
        t->setPosition(x, y);
    }

    void wrap_love_dll_type_ParticleSystem_getPosition(ParticleSystem *t, float *out_x, float *out_y)
    {
        love::Vector pos = t->getPosition();
        *out_x =  pos.getX();
        *out_y =  pos.getY();
    }

    void wrap_love_dll_type_ParticleSystem_moveTo(ParticleSystem *t, float x, float y)
    {
        t->moveTo(x, y);
    }

    bool4 wrap_love_dll_type_ParticleSystem_setAreaSpread(ParticleSystem *t, int distribution_type, float x, float y)
    {
        ParticleSystem::AreaSpreadDistribution distribution = ParticleSystem::DISTRIBUTION_NONE;
        distribution = (ParticleSystem::AreaSpreadDistribution)distribution_type;

        if (distribution != ParticleSystem::DISTRIBUTION_NONE)
        {
            if (x < 0.0f || y < 0.0f)
            {
                wrap_ee("Invalid area spread parameters (must be >= 0)");
                return false; 
            }
        }

        t->setAreaSpread(distribution, x, y);
        return true;
    }

    void wrap_love_dll_type_ParticleSystem_getAreaSpread(ParticleSystem *t, int *out_distribution_type, float *out_x, float *out_y)
    {
        ParticleSystem::AreaSpreadDistribution distribution = t->getAreaSpreadDistribution();
        const love::Vector &p = t->getAreaSpreadParameters();

        *out_distribution_type = distribution;
        *out_x = p.getX();
        *out_y = p.getY();
    }

    void wrap_love_dll_type_ParticleSystem_setDirection(ParticleSystem *t, float direction)
    {
        t->setDirection(direction);
    }

    void wrap_love_dll_type_ParticleSystem_getDirection(ParticleSystem *t, float *out_direction)
    {
        *out_direction = t->getDirection();
    }

    void wrap_love_dll_type_ParticleSystem_setSpread(ParticleSystem *t, float spread)
    {
        t->setSpread(spread);
    }

    void wrap_love_dll_type_ParticleSystem_getSpread(ParticleSystem *t, float *out_spread)
    {
        *out_spread = t->getSpread();
    }

    void wrap_love_dll_type_ParticleSystem_setSpeed(ParticleSystem *t, float min, float max)
    {
        t->setSpeed(min, max);
    }

    void wrap_love_dll_type_ParticleSystem_getSpeed(ParticleSystem *t, float *out_min, float *out_max)
    {
        float min, max;
        t->getSpeed(min, max);
        *out_min = min;
        *out_max = max;
    }

    void wrap_love_dll_type_ParticleSystem_setLinearAcceleration(ParticleSystem *t, float xmin, float ymin, float xmax, float ymax)
    {
        t->setLinearAcceleration(xmin, ymin, xmax, ymax);
    }

    void wrap_love_dll_type_ParticleSystem_getLinearAcceleration(ParticleSystem *t, float *out_xmin, float *out_ymin, float *out_xmax, float *out_ymax)
    {
        love::Vector min, max;
        t->getLinearAcceleration(min, max);
        *out_xmin = min.x;
        *out_ymin = min.y;
        *out_xmax = max.x;
        *out_ymax = max.y;
    }

    void wrap_love_dll_type_ParticleSystem_setRadialAcceleration(ParticleSystem *t, float min, float max)
    {
        t->setRadialAcceleration(min, max);
    }

    void wrap_love_dll_type_ParticleSystem_getRadialAcceleration(ParticleSystem *t, int *out_min, int *out_max)
    {
        float min, max;
        t->getRadialAcceleration(min, max);
        *out_min = min;
        *out_max = max;
    }

    void wrap_love_dll_type_ParticleSystem_setTangentialAcceleration(ParticleSystem *t, float min, float max)
    {
        t->setTangentialAcceleration(min, max);
    }

    void wrap_love_dll_type_ParticleSystem_getTangentialAcceleration(ParticleSystem *t, int *out_min, int *out_max)
    {
        float min, max;
        t->getTangentialAcceleration(min, max);
        *out_min = min;
        *out_max = max;
    }

    void wrap_love_dll_type_ParticleSystem_setLinearDamping(ParticleSystem *t, float min, float max)
    {
        t->setLinearDamping(min, max);
    }

    void wrap_love_dll_type_ParticleSystem_getLinearDamping(ParticleSystem *t, int *out_min, int *out_max)
    {
        float min, max;
        t->getLinearDamping(min, max);
        *out_min = min;
        *out_max = max;
    }

    bool4 wrap_love_dll_type_ParticleSystem_setSizes(ParticleSystem *t, float *sizearray, int sizearray_length)
    {
        if (sizearray_length > 8)
        {
            wrap_ee("At most eight (8) sizes may be used.");
            return false;
        }

        if (sizearray_length <= 1)
        {
            t->setSize(sizearray[0]);
        }
        else
        {
            std::vector<float> sizes(sizearray_length);
            for (size_t i = 0; i < sizearray_length; ++i)
                sizes[i] = sizearray[i];

            t->setSizes(sizes);
        }
        return true;
    }

    void wrap_love_dll_type_ParticleSystem_getSizes(ParticleSystem *t, float **out_sizearray, int *out_sizearray_length)
    {
        const std::vector<float> &sizes = t->getSizes();
        *out_sizearray = new float[sizes.size()];
        *out_sizearray_length = sizes.size();

        for (size_t i = 0; i < sizes.size(); i++)
        {
            (*out_sizearray)[i] = sizes[i];
        }
    }

    bool4 wrap_love_dll_type_ParticleSystem_setSizeVariation(ParticleSystem *t, float variation)
    {
        if (variation < 0.0f || variation > 1.0f)
        {
            wrap_ee("Size variation has to be between 0 and 1, inclusive.");
            return false;
        }

        t->setSizeVariation(variation);
        return true;
    }

    void wrap_love_dll_type_ParticleSystem_getSizeVariation(ParticleSystem *t, float *out_variation)
    {
        *out_variation = t->getSizeVariation();
    }

    void wrap_love_dll_type_ParticleSystem_setRotation(ParticleSystem *t, float min, float max)
    {
        t->setRotation(min, max);
    }

    void wrap_love_dll_type_ParticleSystem_getRotation(ParticleSystem *t, int *out_min, int *out_max)
    {
        float min, max;
        t->getRotation(min, max);
        *out_min = min;
        *out_max = max;
    }

    void wrap_love_dll_type_ParticleSystem_setSpin(ParticleSystem *t, float start, float end)
    {
        t->setSpin(start, end);
    }

    void wrap_love_dll_type_ParticleSystem_getSpin(ParticleSystem *t, float *out_start, float *out_end)
    {
        float start, end;
        t->getSpin(start, end);
        *out_start = start;
        *out_end = end;
    }

    void wrap_love_dll_type_ParticleSystem_setSpinVariation(ParticleSystem *t, float variation)
    {
        t->setSpinVariation(variation);
    }

    void wrap_love_dll_type_ParticleSystem_getSpinVariation(ParticleSystem *t, float *out_variation)
    {
        *out_variation = t->getSpinVariation();
    }

    void wrap_love_dll_type_ParticleSystem_setOffset(ParticleSystem *t, float x, float y)
    {
        t->setOffset(x, y);
    }

    void wrap_love_dll_type_ParticleSystem_getOffset(ParticleSystem *t, float *out_x, float *out_y)
    {
        love::Vector offset = t->getOffset();
        *out_x = offset.getX();
        *out_y = offset.getY();
    }

    bool4 wrap_love_dll_type_ParticleSystem_setColors(ParticleSystem *t, Int4 *colorarray, int colorarray_length)
    {
        if (colorarray_length > 8)
        {
            wrap_ee("At most eight (8) colors may be used.");
            return false;
        }

        std::vector<Colorf> colors(colorarray_length);
        for (int i = 0; i < colorarray_length; i++)
        {
            colors[i].r = colorarray[i].r;
            colors[i].g = colorarray[i].g;
            colors[i].b = colorarray[i].b;
            colors[i].a = colorarray[i].a;
        }

        t->setColor(colors);
        return true;
    }

    void wrap_love_dll_type_ParticleSystem_getColors(ParticleSystem *t, Int4 **out_colorarray, int *out_colorarray_length)
    {
        const std::vector<Colorf> &colors = t->getColor();
        *out_colorarray = new Float4[colors.size()];
        *out_colorarray_length = colors.size();

        for (size_t i = 0; i < colors.size(); i++)
        {
            (*out_colorarray)[i].r = colors[i].r;
            (*out_colorarray)[i].g = colors[i].g;
            (*out_colorarray)[i].b = colors[i].b;
            (*out_colorarray)[i].a = colors[i].a;
        }
    }

    void wrap_love_dll_type_ParticleSystem_setQuads(ParticleSystem *t, Quad** quadsarray, int quadsarray_length)
    {
        std::vector<Quad *> quads(quadsarray_length);
        {
            for (int i = 0; i < quadsarray_length; i++)
            {
                quads[i] = quadsarray[i];
            }
        }

        t->setQuads(quads);
    }

    void wrap_love_dll_type_ParticleSystem_getQuads(ParticleSystem *t, Quad ***out_quadsarray, int *out_quadsarray_length)
    {
        const std::vector<Quad *> quads = t->getQuads();
        *out_quadsarray_length = (int)quads.size();
        *out_quadsarray = new Quad*[*out_quadsarray_length];

        for (int i = 0; i < *out_quadsarray_length; i++)
        {
            (*out_quadsarray)[i] = quads[i];
            (*out_quadsarray)[i]->retain();
        }
    }

    void wrap_love_dll_type_ParticleSystem_setRelativeRotation(ParticleSystem *t, bool4 enable)
    {
        t->setRelativeRotation(enable);
    }

    void wrap_love_dll_type_ParticleSystem_hasRelativeRotation(ParticleSystem *t, bool4 *out_enable)
    {
        *out_enable = t->hasRelativeRotation();
    }

    void wrap_love_dll_type_ParticleSystem_getCount(ParticleSystem *t, uint32 *out_count)
    {
        *out_count = t->getCount();
    }

    void wrap_love_dll_type_ParticleSystem_start(ParticleSystem *t)
    {
        t->start();
    }

    void wrap_love_dll_type_ParticleSystem_stop(ParticleSystem *t)
    {
        t->stop();
    }

    void wrap_love_dll_type_ParticleSystem_pause(ParticleSystem *t)
    {
        t->pause();
    }

    void wrap_love_dll_type_ParticleSystem_reset(ParticleSystem *t)
    {
        t->reset();
    }

    void wrap_love_dll_type_ParticleSystem_emit(ParticleSystem *t, int num)
    {
        t->emit(num);
    }

    void wrap_love_dll_type_ParticleSystem_isActive(ParticleSystem *t, bool4 *out_result)
    {
        *out_result = t->isActive();
    }

    void wrap_love_dll_type_ParticleSystem_isPaused(ParticleSystem *t, bool4 *out_result)
    {
        *out_result = t->isPaused();
    }

    void wrap_love_dll_type_ParticleSystem_isStopped(ParticleSystem *t, bool4 *out_result)
    {
        *out_result = t->isStopped();
    }

    void wrap_love_dll_type_ParticleSystem_update(ParticleSystem *t, float dt)
    {
        t->update(dt);
    }
#pragma endregion

#pragma region type - Quad

    void wrap_love_dll_type_Quad_setViewport(Quad *quad, float x, float y, float w, float h)
    {
        Quad::Viewport v;
        v.x = x;
        v.y = y;
        v.w = w;
        v.h = h;
        quad->setViewport(v);
    }

    void wrap_love_dll_type_Quad_getViewport(Quad *quad, float *out_x, float *out_y, float *out_w, float *out_h)
    {
        Quad::Viewport v = quad->getViewport();
        *out_x = v.x;
        *out_y = v.y;
        *out_w = v.w;
        *out_h = v.h;
    }

    void wrap_love_dll_type_Quad_getTextureDimensions(Quad *quad, float *out_sw, float *out_sh)
    {
        double sw = quad->getTextureWidth();
        double sh = quad->getTextureHeight();
        *out_sw = sw;
        *out_sh = sh;
    }
#pragma endregion

#pragma region type - Shader


    static int inline_type_Shader_getCount(const Shader::UniformInfo *info, int count)
    {
        return std::min(std::max(count, 1), info->count);
    }

    void wrap_love_dll_type_Shader_getWarnings(Shader *shader, WrapString **out_str)
    {
        std::string warnings = shader->getWarnings();
        *out_str = new_WrapString(warnings);
    }

    bool4 wrap_love_dll_type_Shader_sendColors(Shader *shader, const char *name, Int4 *valuearray, int value_lenght)
    {
        const Shader::UniformInfo *info = shader->getUniformInfo(name);
        int count = inline_type_Shader_getCount(info, value_lenght);
        int components = info->components;
        bool gammacorrect = love::graphics::isGammaCorrect();
        const auto &m = love::math::Math::instance;

        float *values = shader->getScratchBuffer<float>(count * components);

        for (int i = 0; i < count; i++)
        {
            for (int j = 0; j < components; j++)
            {
                float rawvalue = 0;
                if (j == 0) rawvalue = valuearray[i].r;
                else if (j == 1) rawvalue = valuearray[i].g;
                else if (j == 2) rawvalue = valuearray[i].b;
                else if (j == 3) rawvalue = valuearray[i].a;

                // the fourth component (alpha) is always already linear, if it exists.
                if (gammacorrect && j < 3)
                    values[i * components + j] = m.gammaToLinear(rawvalue / 255.0f);
                else
                    values[i * components + j] = rawvalue / 255.0f;
            }
        }

        return wrap_catchexcept([&]() { shader->sendFloats(info, values, count); });
    }

    bool4 wrap_love_dll_type_Shader_sendFloats(Shader *shader, const char *name, float *valuearray, int valuearray_lenght)
    {
        const Shader::UniformInfo *info = shader->getUniformInfo(name);
        int count = inline_type_Shader_getCount(info, valuearray_lenght);
        return wrap_catchexcept([&]() { shader->sendFloats(info, valuearray, count); });
    }

    bool4 wrap_love_dll_type_Shader_sendInts(Shader *shader, const char *name, int *valuearray, int valuearray_lenght)
    {
        const Shader::UniformInfo *info = shader->getUniformInfo(name);
        int count = inline_type_Shader_getCount(info, valuearray_lenght);
        return wrap_catchexcept([&]() { shader->sendInts(info, valuearray, count); });
    }

    bool4 wrap_love_dll_type_Shader_sendBooleans(Shader *shader, const char *name, bool4 *valuearray, int valuearray_lenght)
    {
        const Shader::UniformInfo *info = shader->getUniformInfo(name);
        int count = inline_type_Shader_getCount(info, valuearray_lenght);
        float *values = shader->getScratchBuffer<float>(valuearray_lenght);
        for (int i = 0; i < valuearray_lenght; i++) { values[i] = valuearray[i]; }
        return wrap_catchexcept([&]() { shader->sendFloats(info, values, count); });
    }

    bool4 wrap_love_dll_type_Shader_sendMatrices(Shader *shader, const char *name, float *valuearray, int valuearray_lenght)
    {
        const Shader::UniformInfo *info = shader->getUniformInfo(name);
        int count = inline_type_Shader_getCount(info, valuearray_lenght);
        return wrap_catchexcept([&]() { shader->sendMatrices(info, valuearray, count); });
    }

    bool4 wrap_love_dll_type_Shader_sendTexture(Shader *shader, const char *name, Texture *texture)
    {
        const Shader::UniformInfo *info = shader->getUniformInfo(name);
        return wrap_catchexcept([&]() { shader->sendTexture(info, texture); });
    }

    void wrap_love_dll_type_Shader_getExternVariable(Shader *shader, const char *name, bool4 *out_variableExists, int *out_uniform_type, int *out_components, int *out_arrayelements)
    {
        int components = 0;
        int arrayelements = 0;
        Shader::UniformType type = Shader::UNIFORM_UNKNOWN;
        type = shader->getExternVariable(name, components, arrayelements);

        *out_variableExists = (components > 0);
        *out_uniform_type = type;
        *out_components = components;
        *out_arrayelements = arrayelements;
    }


#pragma endregion

#pragma region type - SpriteBatch

    bool4 wrap_love_dll_type_SpriteBatch_add(SpriteBatch *t, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky, int *out_index)
    {
        return wrap_catchexcept([&]() {
            *out_index = t->add(x, y, a, sx, sy, ox, oy, kx, ky, -1);
        });
    }

    bool4 wrap_love_dll_type_SpriteBatch_add_Quad(SpriteBatch *t, Quad *quad, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky, int *out_index)
    {
        return wrap_catchexcept([&]() {
            *out_index = t->addq(quad, x, y, a, sx, sy, ox, oy, kx, ky, -1);
        });
    }

    bool4 wrap_love_dll_type_SpriteBatch_set(SpriteBatch *t, int index, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky)
    {
        return wrap_catchexcept([&]() {
            t->add(x, y, a, sx, sy, ox, oy, kx, ky, index);
        });
    }

    bool4 wrap_love_dll_type_SpriteBatch_set_Quad(SpriteBatch *t, int index, Quad *quad, float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky)
    {
        return wrap_catchexcept([&]() {
            t->addq(quad, x, y, a, sx, sy, ox, oy, kx, ky, index);
        });
    }

    void wrap_love_dll_type_SpriteBatch_clear(SpriteBatch *t)
    {
        t->clear();
    }

    void wrap_love_dll_type_SpriteBatch_flush(SpriteBatch *t)
    {
        t->flush();
    }

    void wrap_love_dll_type_SpriteBatch_setTexture(SpriteBatch *t, Texture *tex)
    {
        t->setTexture(tex);
    }

    bool4 wrap_love_dll_type_SpriteBatch_getTexture(SpriteBatch *t, Texture **out_texture)
    {
        Texture *tex = t->getTexture();

        // FIXME: big hack right here.
        if (typeid(*tex) == typeid(love::graphics::opengl::Image)) {}
        else if (typeid(*tex) == typeid(Canvas)) {}
        else
        {
            wrap_ee("Unable to determine texture type.");
            return false;
        }
        *out_texture = tex;
        tex->retain();
        return true;
    }

    void wrap_love_dll_type_SpriteBatch_setColor_nil(SpriteBatch *t)
    {
        t->setColor();
    }

    void wrap_love_dll_type_SpriteBatch_setColor(SpriteBatch *t, int r, int g, int b, int a)
    {
        Color c;
        c.r = r;
        c.g = g;
        c.b = b;
        c.a = a;
        t->setColor(c);
    }

    void wrap_love_dll_type_SpriteBatch_getColor(SpriteBatch *t, bool4 *out_exist, int *out_r, int *out_g, int *out_b, int *out_a)
    {
        const Color *color = t->getColor();

        // getColor returns null if no color is set.
        if (!color)
        {
            *out_exist = false;
            return;
        }
        *out_exist = true;
        *out_r = color->r;
        *out_g = color->g;
        *out_b = color->b;
        *out_a = color->a;
    }

    void wrap_love_dll_type_SpriteBatch_getCount(SpriteBatch *t, int *out_count)
    {
        *out_count = t->getCount();
    }

    bool4 wrap_love_dll_type_SpriteBatch_setBufferSize(SpriteBatch *t, int size)
    {
        return wrap_catchexcept([&]() {t->setBufferSize(size); });
    }

    void wrap_love_dll_type_SpriteBatch_getBufferSize(SpriteBatch *t, int *out_buffersize)
    {
        *out_buffersize = t->getBufferSize();
    }

    void wrap_love_dll_type_SpriteBatch_attachAttribute(SpriteBatch *t, const char *name, Mesh *m)
    {
        wrap_catchexcept([&]() { t->attachAttribute(name, m); });
    }

#pragma endregion 

#pragma region type - Text

    void wrap_love_dll_type_Text_set_nil(Text *t)
    {
        // No argument: clear all current text.
        wrap_catchexcept([&]() { t->set(); });
    }

    bool4 wrap_love_dll_type_Text_set_coloredstring(Text *t, Int4 coloredStringColor[], const pchar coloredStringText[], int coloredStringLength)
    {
        // Single argument: unformatted text.
        std::vector<love::graphics::opengl::Font::ColoredString> strings;
        strings.reserve(coloredStringLength);

        for (int i = 0; i < coloredStringLength; i++)
        {
            love::graphics::opengl::Font::ColoredString coloredstr;
            coloredstr.color.r = coloredStringColor[i].r;
            coloredstr.color.g = coloredStringColor[i].g;
            coloredstr.color.b = coloredStringColor[i].b;
            coloredstr.color.a = coloredStringColor[i].a;
            coloredstr.str = coloredStringText[i];
        }

        return wrap_catchexcept([&]() { t->set(strings); });
    }

    bool4 wrap_love_dll_type_Text_setf(Text *t, Int4 coloredStringColor[], const pchar coloredStringText[], int coloredStringLength, float wraplimit, int align_type)
    {
        std::vector<love::graphics::opengl::Font::ColoredString> strings;
        strings.reserve(coloredStringLength);

        for (int i = 0; i < coloredStringLength; i++)
        {
            love::graphics::opengl::Font::ColoredString coloredstr;
            coloredstr.color.r = coloredStringColor[i].r;
            coloredstr.color.g = coloredStringColor[i].g;
            coloredstr.color.b = coloredStringColor[i].b;
            coloredstr.color.a = coloredStringColor[i].a;
            coloredstr.str = coloredStringText[i];
        }

        auto align = (love::graphics::opengl::Font::AlignMode)align_type;
        return wrap_catchexcept([&]() { t->set(strings, wraplimit, align); });
    }

    bool4 wrap_love_dll_type_Text_add(Text *t, Int4 coloredStringColor[], const pchar coloredStringText[], int coloredStringLength,
        float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky, int *out_index)
    {
        std::vector<love::graphics::opengl::Font::ColoredString> strings;
        strings.reserve(coloredStringLength);

        for (int i = 0; i < coloredStringLength; i++)
        {
            love::graphics::opengl::Font::ColoredString coloredstr;
            coloredstr.color.r = coloredStringColor[i].r;
            coloredstr.color.g = coloredStringColor[i].g;
            coloredstr.color.b = coloredStringColor[i].b;
            coloredstr.color.a = coloredStringColor[i].a;
            coloredstr.str = coloredStringText[i];
        }

        return wrap_catchexcept([&]() { *out_index = t->add(strings, x, y, a, sx, sy, ox, oy, kx, ky); });
    }

    bool4 wrap_love_dll_type_Text_addf(Text *t, Int4 coloredStringColor[], const pchar coloredStringText[], int coloredStringLength,
        float x, float y, float a, float sx, float sy, float ox, float oy, float kx, float ky, float wraplimit, int align_type, int *out_index)
    {
        std::vector<love::graphics::opengl::Font::ColoredString> strings;
        strings.reserve(coloredStringLength);

        for (int i = 0; i < coloredStringLength; i++)
        {
            love::graphics::opengl::Font::ColoredString coloredstr;
            coloredstr.color.r = coloredStringColor[i].r;
            coloredstr.color.g = coloredStringColor[i].g;
            coloredstr.color.b = coloredStringColor[i].b;
            coloredstr.color.a = coloredStringColor[i].a;
            coloredstr.str = coloredStringText[i];
        }
        auto align = (love::graphics::opengl::Font::AlignMode)align_type;
        return wrap_catchexcept([&]() { *out_index = t->addf(strings, wraplimit, align, x, y, a, sx, sy, ox, oy, kx, ky); });
    }

    void wrap_love_dll_type_Text_clear(Text *t)
    {
        wrap_catchexcept([&]() { t->clear(); });
    }

    bool4 wrap_love_dll_type_Text_setFont(Text *t, love::graphics::opengl::Font *f)
    {
         return wrap_catchexcept([&]() { t->setFont(f); });
    }

    void wrap_love_dll_type_Text_getFont(Text *t, love::graphics::opengl::Font **font)
    {
        *font = t->getFont();
        (*font)->retain();
    }

    void wrap_love_dll_type_Text_getWidth(Text *t, int index, int *out_w)
    {
        *out_w = t->getWidth(index);
    }

    void wrap_love_dll_type_Text_getHeight(Text *t, int index, int *out_h)
    {
        *out_h = t->getHeight(index);
    }

#pragma endregion

#pragma region type - Texture

    void wrap_love_dll_type_Texture_getWidth(Texture *t, int *out_w)
    {
        *out_w = t->getWidth();
    }

    void wrap_love_dll_type_Texture_getHeight(Texture *t, int *out_h)
    {
        *out_h = t->getHeight();
    }

    bool4 wrap_love_dll_type_Texture_setFilter(Texture *t, int filtermin_type, int filtermag_type, float anisotropy)
    {
        Texture::Filter f = t->getFilter();
        f.min = (Texture::FilterMode)filtermin_type;
        f.mag = (Texture::FilterMode)filtermag_type;
        f.anisotropy = anisotropy;
        return wrap_catchexcept([&]() { t->setFilter(f); });
    }

    void wrap_love_dll_type_Texture_getFilter(Texture *t, int *out_filtermin_type, int *out_filtermag_type, float *out_anisotropy)
    {
        const Texture::Filter f = t->getFilter();
        *out_filtermin_type = f.min;
        *out_filtermag_type = f.mag;
        *out_anisotropy = f.anisotropy;
    }

    void wrap_love_dll_type_Texture_setWrap(Texture *t, int wraphoriz_type, int wrapvert_type)
    {
        Texture::Wrap w;
        w.s = (Texture::WrapMode)wraphoriz_type;
        w.t = (Texture::WrapMode)wrapvert_type;
        t->setWrap(w);
    }

    void wrap_love_dll_type_Texture_getWrap(Texture *t, int *out_wraphoriz_type, int *out_wrapvert_type)
    {
        const Texture::Wrap w = t->getWrap();
        *out_wraphoriz_type = w.s;
        *out_wrapvert_type = w.t;
    }


#pragma endregion

#pragma region type - Video

    void wrap_love_dll_type_Video_getStream(love::graphics::opengl::Video *video, VideoStream **out_videsStream)
    {
        *out_videsStream = video->getStream();
        (*out_videsStream)->retain();
    }

    void wrap_love_dll_type_Video_getSource(love::graphics::opengl::Video *video, Source **out_source)
    {
        *out_source = video->getSource();
        (*out_source)->retain();
    }

    void wrap_love_dll_type_Video_setSource_nil(love::graphics::opengl::Video *video)
    {
        video->setSource(nullptr);
    }

    void wrap_love_dll_type_Video_setSource(love::graphics::opengl::Video *video, Source *source)
    {
        video->setSource(source);
    }

    void wrap_love_dll_type_Video_getWidth(love::graphics::opengl::Video *video, int *out_w)
    {
        *out_w = video->getWidth();
    }

    void wrap_love_dll_type_Video_getHeight(love::graphics::opengl::Video *video, int *out_h)
    {
        *out_h = video->getHeight();
    }

    bool4 wrap_love_dll_type_Video_setFilter(love::graphics::opengl::Video *video, int filtermin_type, int filtermag_type, float anisotropy)
    {
        Texture::Filter f = video->getFilter();
        f.min = (Texture::FilterMode)filtermin_type;
        f.mag = (Texture::FilterMode)filtermag_type;
        f.anisotropy = anisotropy;

        return wrap_catchexcept([&]() { video->setFilter(f); });
    }

    void wrap_love_dll_type_Video_getFilter(love::graphics::opengl::Video *video, int *out_filtermin_type, int *out_filtermag_type, float *out_anisotropy)
    {
        const Texture::Filter f = video->getFilter();
        *out_filtermin_type = f.min;
        *out_filtermag_type = f.mag;
        *out_anisotropy = f.anisotropy;
    }


#pragma endregion

#pragma region type - CompressedImageData

    bool4 wrap_love_dll_type_CompressedImageData_getWidth(CompressedImageData *t, int miplevel, int *out_w)
    {
        return wrap_catchexcept([&](){ *out_w = t->getWidth(miplevel); });
    }

    bool4 wrap_love_dll_type_CompressedImageData_getHeight(CompressedImageData *t, int miplevel, int *out_h)
    {
        return wrap_catchexcept([&](){ *out_h = t->getHeight(miplevel); });
    }

    void wrap_love_dll_type_CompressedImageData_getMipmapCount(CompressedImageData *t, int *out_count)
    {
        *out_count = t->getMipmapCount();
    }

    void wrap_love_dll_type_CompressedImageData_getFormat(CompressedImageData *t, int *out_format_type)
    {
        image::CompressedImageData::Format format = t->getFormat();
        *out_format_type = format;
    }


#pragma endregion

#pragma region type - ImageData

    void wrap_love_dll_type_ImageData_getWidth(ImageData *t, int *out_w)
    {
        *out_w = t->getWidth();
    }

    void wrap_love_dll_type_ImageData_getHeight(ImageData *t, int *out_h)
    {
        *out_h = t->getHeight();
    }

    bool4 wrap_love_dll_type_ImageData_getPixel(ImageData *t, int x, int y, uint8 *out_r, uint8 *out_g, uint8 *out_b, uint8 *out_a)
    {
        pixel c;
        return wrap_catchexcept([&]() { 
            c = t->getPixel(x, y);
            *out_r = c.r;
            *out_g = c.g;
            *out_b = c.b;
            *out_a = c.a;
        });
    }

    bool4 wrap_love_dll_type_ImageData_setPixel(ImageData *t, int x, int y, uint8 r, uint8 g, uint8 b, uint8 a)
    {
        pixel c;
        c.r = r;
        c.g = g;
        c.b = b;
        c.a = a;

        return wrap_catchexcept([&]() { t->setPixel(x, y, c); });
    }

    void wrap_love_dll_type_ImageData_paste(ImageData *t, ImageData* src,int dx, int dy, int sx, int sy, int sw, int sh)
    {
        t->paste((love::image::ImageData *)src, dx, dy, sx, sy, sw, sh);
    }

    void wrap_love_dll_type_ImageData_encode(ImageData *t, int format_type, const char* filename)
    {
        ImageData::EncodedFormat format = (ImageData::EncodedFormat)format_type;
        wrap_catchexcept([&]() { 
            love::filesystem::FileData *filedata = t->encode(format, filename);
            
            wrap_love_dll_filesystem_write(filename, filedata->getData(), filedata->getSize());
        });
    }
    
    void wrap_love_dll_type_ImageData_ext_mutexLock(ImageData *t, love::thread::Lock **out_lock)
    {
         *out_lock = new love::thread::Lock(t->getMutex());
    }
    void wrap_love_dll_type_ImageData_ext_mutexUnlock(ImageData *t, love::thread::Lock *ptrlock)
    {
        delete ptrlock;
    }
    void wrap_love_dll_type_ImageData_ext_getPixelUnsafe(ImageData *t, int x, int y, uint8 *out_r, uint8 *out_g, uint8 *out_b, uint8 *out_a)
    {
        pixel c = t->getPixelUnsafe(x, y);
        *out_r = c.r;
        *out_g = c.g;
        *out_b = c.b;
        *out_a = c.a;
    }

    void wrap_love_dll_type_ImageData_ext_setPixelUnsafe(ImageData *t, int x, int y, uint8 r, uint8 g, uint8 b, uint8 a)
    {
        pixel c;
        c.r = r;
        c.g = g;
        c.b = b;
        c.a = a;
        t->setPixelUnsafe(x, y, c);
    }


#pragma endregion

#pragma region type - Cursor

    void wrap_love_dll_type_Cursor_getType(Cursor *cursor, int *out_cursortype_type)
    {
        Cursor::CursorType ctype = cursor->getType();
        *out_cursortype_type = ctype;
    }

    void wrap_love_dll_type_Cursor_getSystemType(Cursor *cursor, int *out_systype_type)
    {
        Cursor::SystemCursor systype = cursor->getSystemType();
        *out_systype_type = systype;
    }

#pragma endregion

#pragma region type - Decoder

    void wrap_love_dll_type_Decoder_getChannels(Decoder *t, int *out_channels)
    {
        *out_channels = t->getChannels();
    }

    void wrap_love_dll_type_Decoder_getBitDepth(Decoder *t, int *out_bitDepth)
    {
        *out_bitDepth = t->getBitDepth();
    }

    void wrap_love_dll_type_Decoder_getSampleRate(Decoder *t, int *out_sampleRate)
    {
        *out_sampleRate = t->getSampleRate();
    }

    void wrap_love_dll_type_Decoder_getDuration(Decoder *t, double *out_duration)
    {
        *out_duration = t->getDuration();
    }


#pragma endregion

#pragma region type - SoundData

    void wrap_love_dll_SoundData_getChannels(SoundData *t, int *out_channels)
    {
        *out_channels = t->getChannels();
    }

    void wrap_love_dll_SoundData_getBitDepth(SoundData *t, int *out_bitDepth)
    {
        *out_bitDepth = t->getBitDepth();
    }

    void wrap_love_dll_SoundData_getSampleRate(SoundData *t, int *out_sampleRate)
    {
        *out_sampleRate = t->getSampleRate();
    }

    void wrap_love_dll_SoundData_getSampleCount(SoundData *t, int *out_sampleCount)
    {
        *out_sampleCount = t->getSampleCount();
    }

    void wrap_love_dll_SoundData_getDuration(SoundData *t, double *out_duration)
    {
        *out_duration = t->getDuration();
    }

    bool4 wrap_love_dll_SoundData_setSample(SoundData *t, int i, float sample)
    {
        return wrap_catchexcept([&]() { t->setSample(i, sample); });
    }

    bool4 wrap_love_dll_SoundData_getSample(SoundData *t, int i, float *out_sample)
    {
        return wrap_catchexcept([&]() { *out_sample = t->getSample(i); });
    }


#pragma endregion

#pragma region type - VideoStream

    void wrap_love_dll_type_VideoStream_getFilename(VideoStream *stream, WrapString **out_filename)
    {
        *out_filename = new_WrapString(stream->getFilename());
    }

    void wrap_love_dll_type_VideoStream_play(VideoStream *stream)
    {
        stream->play();
    }

    void wrap_love_dll_type_VideoStream_pause(VideoStream *stream)
    {
        stream->pause();
    }

    void wrap_love_dll_type_VideoStream_seek(VideoStream *stream, double offset)
    {
        stream->seek(offset);
    }

    void wrap_love_dll_type_VideoStream_rewind(VideoStream *stream)
    {
        stream->seek(0);
    }

    void wrap_love_dll_type_VideoStream_tell(VideoStream *stream, double *out_position)
    {
        *out_position = stream->tell();
    }

    void wrap_love_dll_type_VideoStream_isPlaying(VideoStream *stream, bool4 *out_isplaying)
    {
        *out_isplaying = stream->isPlaying();
    }

#pragma endregion

#pragma region type - BezierCurve


    void wrap_love_dll_type_BezierCurve_getDegree(BezierCurve *curve, int *out_degree)
    {
        *out_degree = curve->getDegree();
    }

    void wrap_love_dll_type_BezierCurve_getDerivative(BezierCurve *curve, BezierCurve **out_deriv)
    {
        *out_deriv = new BezierCurve(curve->getDerivative());
    }

    bool4 wrap_love_dll_type_BezierCurve_getControlPoint(BezierCurve *curve, int idx, float *out_x, float *out_y)
    {
        return wrap_catchexcept([&]() {
            Vector v = curve->getControlPoint(idx);
            *out_x = v.x;
            *out_y = v.y;
        });
    }

    bool4 wrap_love_dll_type_BezierCurve_setControlPoint(BezierCurve *curve, int idx, float x, float y)
    {
        return wrap_catchexcept([&]() { curve->setControlPoint(idx, Vector(x, y)); });
    }

    bool4 wrap_love_dll_type_BezierCurve_insertControlPoint(BezierCurve *curve, int idx, float x, float y)
    {
        return wrap_catchexcept([&]() { curve->insertControlPoint(Vector(x, y), idx); });
    }

    bool4 wrap_love_dll_type_BezierCurve_removeControlPoint(BezierCurve *curve, int idx)
    {
        return wrap_catchexcept([&]() {curve->removeControlPoint(idx); });
    }

    void wrap_love_dll_type_BezierCurve_getControlPointCount(BezierCurve *curve, int *out_count)
    {
        *out_count = curve->getControlPointCount();
    }

    void wrap_love_dll_type_BezierCurve_translate(BezierCurve *curve, float dx, float dy)
    {
        curve->translate(Vector(dx, dy));
    }

    void wrap_love_dll_type_BezierCurve_rotate(BezierCurve *curve, double phi, float ox, float oy)
    {
        curve->rotate(phi, Vector(ox, oy));
    }

    void wrap_love_dll_type_BezierCurve_scale(BezierCurve *curve, double s, float ox, float oy)
    {
        curve->scale(s, Vector(ox, oy));
    }

    bool4 wrap_love_dll_type_BezierCurve_evaluate(BezierCurve *curve, double t, float *out_x, float *out_y)
    {
        return wrap_catchexcept([&]() {
            Vector v = curve->evaluate(t);
            *out_x = v.x;
            *out_y = v.y;
        });
    }

    bool4 wrap_love_dll_type_BezierCurve_getSegment(BezierCurve *curve, double t1, double t2, BezierCurve **out_segment)
    {
        return wrap_catchexcept([&]() { *out_segment = curve->getSegment(t1, t2); });
    }

    bool4 wrap_love_dll_type_BezierCurve_render(BezierCurve *curve, int accuracy, Float2** out_points, int *out_points_lenght)
    {
        
        return wrap_catchexcept([&]() { 
            std::vector<Vector> points = curve->render(accuracy);
            *out_points_lenght = (int)points.size();
            *out_points = new Float2[(int)points.size()];
            for (int i = 0; i < (int)points.size(); ++i)
            {
                (*out_points)[i].x = points[i].x;
                (*out_points)[i].y = points[i].y;
            }
        });
    }

    bool4 wrap_love_dll_type_BezierCurve_renderSegment(BezierCurve *curve, double start, double end, int accuracy, Float2** out_points, int *out_points_lenght)
    {

        return wrap_catchexcept([&]() {
            std::vector<Vector> points = curve->renderSegment(start, end, accuracy);
            *out_points_lenght = (int)points.size();
            *out_points = new Float2[(int)points.size()];
            for (int i = 0; i < (int)points.size(); ++i)
            {
                (*out_points)[i].x = points[i].x;
                (*out_points)[i].y = points[i].y;
            }
        });
    }

#pragma endregion

#pragma region type - RandomGenerator

    void wrap_love_dll_type_RandomGenerator_random(RandomGenerator *rng, double *out_result)
    {
        *out_result = rng->random();
    }

    void wrap_love_dll_type_RandomGenerator_randomNormal(RandomGenerator *rng, double stddev, double mean, double *out_result)
    {
        double r = rng->randomNormal(stddev);
        *out_result = r + mean;
    }

    bool4 wrap_love_dll_type_RandomGenerator_setSeed(RandomGenerator *rng, uint32 low, uint32 high)
    {
        RandomGenerator::Seed s;
        s.b32.low = low;
        s.b32.high = high;
        return wrap_catchexcept([&]() { rng->setSeed(s); });
    }

    void wrap_love_dll_type_RandomGenerator_getSeed(RandomGenerator *rng, uint32 *out_low, uint32 *out_high)
    {
        RandomGenerator::Seed s = rng->getSeed();
        *out_low = s.b32.low;
        *out_high = s.b32.high;
    }

    bool4 wrap_love_dll_type_RandomGenerator_setState(RandomGenerator *rng, const char *state)
    {
        return wrap_catchexcept([&]() { rng->setState(state); });
    }

    void wrap_love_dll_type_RandomGenerator_getState(RandomGenerator *rng, WrapString **out_str)
    {
        *out_str = new_WrapString(rng->getState());
    }

#pragma endregion

#pragma region type - Joystick

    void wrap_love_dll_type_Joystick_isConnected(Joystick *j, bool4 *out_result)
    {
        *out_result = j->isConnected();
    }

    void wrap_love_dll_type_Joystick_getName(Joystick *j, WrapString **out_str)
    {
        *out_str = new_WrapString(j->getName());
    }

    void wrap_love_dll_type_Joystick_getID(Joystick *j, int *out_id)
    {
        // IDs are 0-based in C#.
        *out_id = j->getID();
    }
    void wrap_love_dll_type_Joystick_getInstanceID(Joystick *j, int *out_instanceid)
    {
        *out_instanceid = j->getInstanceID();
    }

    void wrap_love_dll_type_Joystick_getGUID(Joystick *j, WrapString **out_str)
    {
        *out_str = new_WrapString(j->getGUID());
    }

    void wrap_love_dll_type_Joystick_getAxisCount(Joystick *j, int *out_count)
    {
        *out_count = j->getAxisCount();
    }

    void wrap_love_dll_type_Joystick_getButtonCount(Joystick *j, int *out_count)
    {
        *out_count = j->getButtonCount();
    }

    void wrap_love_dll_type_Joystick_getHatCount(Joystick *j, int *out_count)
    {
        *out_count = j->getHatCount();
    }

    void wrap_love_dll_type_Joystick_getAxis(Joystick *j, int axisindex, float *out_axis)
    {
        *out_axis = j->getAxis(axisindex);
    }

    void wrap_love_dll_type_Joystick_getAxes(Joystick *j, float **out_axes, int *out_axes_length)
    {
        std::vector<float> axes = j->getAxes();
        *out_axes = new float[axes.size()];
        *out_axes_length = axes.size();

        for (int i = 0; i < axes.size(); i++)
        {
            (*out_axes)[i] = axes[i];
        }
    }

    void wrap_love_dll_type_Joystick_getHat(Joystick *j, int hatindex, int *out_hat_type)
    {
        Joystick::Hat h = j->getHat(hatindex);
        *out_hat_type = h;
    }

    void wrap_love_dll_type_Joystick_isDown(Joystick *j, int button, bool4 *out_result)
    {
        std::vector<int> buttons;
        buttons.push_back(button);
        *out_result = j->isDown(buttons);
    }

    void wrap_love_dll_type_Joystick_isGamepad(Joystick *j, bool4 *out_result)
    {
        *out_result = j->isGamepad();
    }

    void wrap_love_dll_type_Joystick_getGamepadAxis(Joystick *j, int axis_type, float *out_gamepadaxis)
    {
        *out_gamepadaxis = j->getGamepadAxis((Joystick::GamepadAxis)axis_type);
    }

    void wrap_love_dll_type_Joystick_isGamepadDown(Joystick *j, int gamepadButton_type, bool4 *out_result)
    {
        std::vector<Joystick::GamepadButton> buttons;
        buttons.push_back((Joystick::GamepadButton)gamepadButton_type);
        *out_result = j->isGamepadDown(buttons);
    }

    void wrap_love_dll_type_Joystick_isVibrationSupported(Joystick *j, bool4 *out_result)
    {
        *out_result = j->isVibrationSupported();
    }

    void wrap_love_dll_type_Joystick_setVibration_nil(Joystick *j, bool4 *out_success)
    {
        // Disable joystick vibration if no argument is given.
        *out_success = j->setVibration();
    }
    void wrap_love_dll_type_Joystick_setVibration(Joystick *j, float left, float right, float duration, bool4 *out_success)
    {
        *out_success = j->setVibration(left, right, duration);
    }

    void wrap_love_dll_type_Joystick_getVibration(Joystick *j, float *out_left, float *out_right)
    {
        float left, right;
        j->getVibration(left, right);
        *out_left = left;
        *out_right = right;
    }

#pragma endregion

#pragma region type - Other

    void wrap_love_dll_type_Data_getSize(Data* data, uint32 *out_datasize)
    {
        *out_datasize = data->getSize();
    }

#pragma endregion

}
}

// wrap_love_dll_
//#undef bool