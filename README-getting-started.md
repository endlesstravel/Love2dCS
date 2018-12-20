
Getting Started
---

1. Create a C# console application

2. Please [install](https://endlesstravel.github.io/#/tutorial/01.Install) Love2dCS from NuGet to your Visual Studio first.

3. Put the following code in the file(maybe Program.cs), and save it.
``` C#
using Love;
namespace Example
{
    class Program : Scene
    {
        public override void Draw()
        {
            Graphics.Print("Hello World!", 400, 300);
        }

        public override void Update(float dt)
        {
            // update every frame here ..
        }

        static void Main(string[] args)
        {
            Boot.Run(new Program());
        }
    }
}
```

the method of `Scene` will  be called just as it is named :

``` C#
KeyPressed(KeyConstant key, Scancode scancode, bool isRepeat)
KeyReleased(KeyConstant key, Scancode scancode)
MouseMoved(float x, float y, float dx, float dy, bool isTouch)
MousePressed(float x, float y, int button, bool isTouch)
MouseReleased(float x, float y, int button, bool isTouch)
MouseFocus(bool focus)
WheelMoved(int x, int y)
JoystickPressed(Joystick joystick, int button)
JoystickReleased(Joystick joystick, int button)
JoystickAxis(Joystick joystick, float axis, float value)
JoystickHat(Joystick joystick, int hat, Joystick.Hat direction)
JoystickGamepadPressed(Joystick joystick, Joystick.GamepadButton button)
JoystickGamepadReleased(Joystick joystick, Joystick.GamepadButton button)
JoystickGamepadAxis(Joystick joystick, Joystick.GamepadAxis axis, float value)
JoystickAdded(Joystick joystick)
JoystickRemoved(Joystick joystick)
TouchMoved(long id, float x, float y, float dx, float dy, float pressure)
TouchPressed(long id, float x, float y, float dx, float dy, float pressure)
TouchReleased(long id, float x, float y, float dx, float dy, float pressure)
TextEditing(string text, int start, int end)
TextInput(string text)
WindowFocus(bool focus)
WindowVisible(bool visible)
WindowResize(int w, int h)
DirectoryDropped(string path)
FileDropped(File file)
Quit()
LowMemory()
Load()
Update(float dt)
Draw()
```

5. Run game : `Debug/Start Debugging` or press `F5`

More examples
---

* Drawing an image
``` C#
using Love;
namespace Example
{
    class Program : Scene
    {
        Image img = null;

        public override void Load()
        {
            img = Graphics.NewImage("logo.png");
        }

        public override void Update(float dt)
        {
            // update every frame here ..
        }

        public override void Draw()
        {
            Graphics.Draw(img, 300, 200);
        }

        static void Main(string[] args)
        {
            Boot.Run(new Program());
        }
    }
}
```

* Playing a sound
``` C#
using Love;
namespace Example
{
    class Program : Scene
    {
        Source source = null;

        public override void Load()
        {
            source = Audio.NewSource("music.mp3");
            source.play();
        }

        public override void Update(float dt)
        {
            // update every frame here ..
        }

        static void Main(string[] args)
        {
            Boot.Run(new Program());
        }
    }
}
```

* Key event handle - Press `Escape` to exit
``` C#
using Love;
namespace Example
{
    class Program : Scene
    {
        public override void KeyPressed(KeyConstant key, Scancode scancode, bool isRepeat)
        {
            if (KeyConstant.Escape == key)
                Event.Quit();
        }

        public override void Update(float dt)
        {
            // update every frame here ..
        }

        static void Main(string[] args)
        {
            Boot.Run(new Program());
        }
    }
}
```

* Emptye projcet - `no-game` Sence will automatically run
``` C#
class Program
{
    static void Main(string[] args)
    {
        Love.Boot.Run();
    }
}
```