// Author : endlesstravel
// this part same as love2d's boot.lua

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using lf = Love.FileSystem;
using ls = Love.Sound;
using la = Love.Audio;
using li = Love.Image;
using lg = Love.Graphics;
using fnt = Love.Font;
using timer = Love.Timer;
using mouse = Love.Mouse;
using keyboard = Love.Keyboard;
using window = Love.Window;

namespace Love
{
    public class Scene
    {
        public virtual void KeyPressed(Keyboard.Key key, Keyboard.Scancode scancode, bool isRepeat) { }
        public virtual void KeyReleased(Keyboard.Key key, Keyboard.Scancode scancode) { }
        public virtual void MouseMoved(float x, float y, float dx, float dy, bool isTouch) { }
        public virtual void MousePressed(float x, float y, int button, bool isTouch) { }
        public virtual void MouseReleased(float x, float y, int button, bool isTouch) { }
        public virtual void MouseFocus(bool focus) { }
        public virtual void WheelMoved(int x, int y) { }

        public virtual void JoystickPressed(Joystick joystick, int button) { }
        public virtual void JoystickReleased(Joystick joystick, int button) { }
        public virtual void JoystickAxis(Joystick joystick, float axis, float value) { }
        public virtual void JoystickHat(Joystick joystick, int hat, Joystick.Hat direction) { }
        public virtual void JoystickGamepadPressed(Joystick joystick, Joystick.GamepadButton button) { }
        public virtual void JoystickGamepadReleased(Joystick joystick, Joystick.GamepadButton button) { }
        public virtual void JoystickGamepadAxis(Joystick joystick, Joystick.GamepadAxis axis, float value) { }
        public virtual void JoystickAdded(Joystick joystick) { }
        public virtual void JoystickRemoved(Joystick joystick) { }

        public virtual void TouchMoved(long id, float x, float y, float dx, float dy, float pressure) { }
        public virtual void TouchPressed(long id, float x, float y, float dx, float dy, float pressure) { }
        public virtual void TouchReleased(long id, float x, float y, float dx, float dy, float pressure) { }

        public virtual void TextEditing(string text, int start, int end) { }
        public virtual void TextInput(string text) { }

        public virtual void WindowFocus(bool focus) { }
        public virtual void WindowVisible(bool visible) { }
        public virtual void WindowResize(int w, int h) { }

        public virtual void DirectoryDropped(string path) { }
        public virtual void FileDropped(File file) { }

        public virtual bool Quit() { return true; }
        public virtual void LowMemory() { }

        public virtual void Load() {}
        public virtual void Update(float dt) {}
        public virtual void Draw() {}
    }

    static public class Boot
    {
        static void Init()
        {
            Mathf.Init();
            lf.Init("");
            ls.Init();
            la.Init();
            fnt.Init();
            window.Init();
            mouse.Init();
            keyboard.Init();
            Joystick.Init();
            Touch.Init();
            Event.Init();
            li.Init();
            lg.Init();
            timer.Init();

            window.SetMode(800, 600);

            string str = lf.GetExecutablePath();
            int index = str.LastIndexOf(@"\");
            string path = str.Substring(0, index);
            Console.WriteLine(path);
            lf.SetSource(path);
        }

        static void Loop()
        {
            scene.Load();
            while (true)
            {
                Event.Poll(scene);
                timer.Step();

                scene.Update(timer.GetDelta());

                var c = lg.GetBackgroundColor();
                lg.Clear(c.r, c.g, c.b, c.a);
                lg.Origin();
                scene.Draw();
                lg.Present();
                timer.Sleep(0.001f);
            }
        }

        static Scene scene;

        static public void Run(Scene sence = null)
        {
            try
            {
                Init();

                if (sence == null)
                {
                    scene = new Love2dNoGame();
                }
                else
                {
                    scene = sence;
                }
                Loop();
            }
            catch (Exception e)
            {
                window.ShowMessageBox("error", e.ToString(), Window.MessageBoxType.Error);
            }
        }
    }
}
