
Getting Started
---

1. Create a C# console application

2. Please [install](README-Install.md) Love2dCS form NuGet to your Visual Studio first.

3. Config project : And **REMEBER** enable native code debugging on your Visual Studio project : `Configuration Properties/Debugging/Enable native code debugging`

4. Put the following code in the file(maybe Program.cs), and save it.
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

        static void Main(string[] args)
        {
            Boot.Run(new Program());
        }
    }
}
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
        public override void KeyPressed(Keyboard.Key key, Keyboard.Scancode scancode, bool isRepeat)
        {
            if (Keyboard.Key.Escape == key)
                Event.Quit();
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