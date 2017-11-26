
![logo](https://github.com/endlesstravel/Love2dCS/raw/master/img/logo.png "logo") 

Love2dCS - C# Wrapper for LÖVE
---
Love2dCS is a C# Wrapper for [LÖVE game engine](https://love2d.org/), it can be used your C# based Application. 

Love2dCS was designed to be as close as possible to the original LÖVE API, as such the documentation provided from LÖVE largely covers usage of Love2dCS. There is a slight difference between Love2dCS and LÖVE where is :

* The `love.math` module in Love2dCS is `Love2D.mathf`
* Most index begin from 1 at LÖVE. However, index will begin from 0 at Love2dCS
* *More in development ... *

Love2dCS currently supports Windows x86. The next step will be to support windows x64. Linux and OSX temporarily was not supported.

Physical module temporary not support.

Feature
---

* Easy to install
* Easy to use

*In development ...*

Setup
---

1. Available on NuGet: https://www.nuget.org/packages/Love2dCS/

2. And **REMEBER** enable native code debugging in VS : `Configuration Properties/Debugging/Enable native code debugging`

3. More detail please reference [Setup-Detail](README-Install.md)

Usage
---

1. Emptye projcet - No game Sence will automatically run
``` C#
class Program
{
    static void Main(string[] args)
    {
        Love2d.Boot.Run();
    }
}
```

2. Drawing text
``` C#
using Love2d;
namespace Example
{
    class Program : Love2d.Scene
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

3. Drawing an image
``` C#
using Love2d;
namespace Example
{
    class Program : Love2d.Scene
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

4. Playing a sound
``` C#
using Love2d;
namespace Example
{
    class Program : Love2d.Scene
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

5. Key event handle - Press `Escape` to exit
``` C#
using Love2d;
namespace Example
{
    class Program : Scene
    {
        class ExitEventHandler : EventHandler
        {
            public override void KeyPressed(Keyboard.Key key, Keyboard.Scancode scancode, bool isRepeat)
            {
                if (Keyboard.Key.KEY_ESCAPE == key)
                    Event.Quit();
            }
        }

        static void Main(string[] args)
        {
            Boot.Run(new Program(), new ExitEventHandler());
        }
    }
}
```

Doucment
---
*In development ...*

Temporarily please reference [love wiki](https://love2d.org/wiki/love)

Distribute
---
*In development ...*

Develpoment
---

*In development ...*