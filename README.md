
![logo](https://github.com/endlesstravel/Love2dCS/raw/master/img/logo.png "logo") 

Love2dCS - C# Wrapper for LÖVE
---
Love2dCS is a C# Wrapper for [LÖVE game engine](https://love2d.org/), it can be used your C# based Application. 

Love2dCS was designed to be as close as possible to the original LÖVE API, as such the documentation provided from LÖVE largely covers usage of Love2dCS. There is a slight difference between Love2dCS and LÖVE where is :

* The `love.math` module in love is replaced by `Love.Mathf` in Love2dCS
* The `love.thread` module in love is not ready to supply, you can use threads in C # instead.
* Most index begin from 1 at LÖVE. However, index will begin from 0 at Love2dCS
* *More in development ... *

Love2dCS currently based on [LÖVE 0.10.1](https://love2d.org/wiki/0.10.1)

Love2dCS currently supports Windows x86. The next step will be to support windows x64. Linux and OSX temporarily was not supported.

Physics module temporary not support.

Feature
---

* Easy to install
* Easy to use

*In development ...*

Setup & Getting Started
---

* Please read [README-getting-started.md](README-getting-started.md) for more detail.

1. Available on NuGet: https://www.nuget.org/packages/Love2dCS/

2. And **REMEBER** enable native code debugging in VS : `Configuration Properties/Debugging/Enable native code debugging`

Next to development
---
 - [ ] Add support for Physics 
 - [ ] Improve the document
 - [ ] Add support for win-x64 platform

Examples
---

1. Emptye projcet - `no-game` Sence will automatically run
``` C#
class Program
{
    static void Main(string[] args)
    {
        Love.Boot.Run();
    }
}
```

2. Drawing text
``` C#
using Love;
namespace Example
{
    class Program : Love.Scene
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
using Love;
namespace Example
{
    class Program : Love.Scene
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
using Love;
namespace Example
{
    class Program : Love.Scene
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
using Love;
namespace Example
{
    class Program : Scene
    {
        class ExitEventHandler : EventHandler
        {
            public override void KeyPressed(Keyboard.Key key, Keyboard.Scancode scancode, bool isRepeat)
            {
                if (Keyboard.Key.Escape == key)
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

Documentation
---
*In development ...*

Temporarily please reference [love wiki](https://love2d.org/wiki/love)

Distribute
---
*In development ...*

Develpoment
---

1. clone git repository `git clone https://github.com/endlesstravel/Love2dCS`

2. Build C part :

* Follow the instructions at the [megasource](https://bitbucket.org/rude/megasource) repository page to build LÖVE 0.10.1+.
* modify file at megasource/libs/love/CmakeList.txt
* at line 1427 modif to :
``` cmake
<... other code ... >
src/wrap_love_dll.cpp
src/wrap_love_dll.h)

source_group("src" FILES src/wrap_love_dll.cpp src/wrap_love_dll.h)

<... other code ....>
```
* Remake project `cmake -G "Visual Studio 12" -H. -Bbuild`

3. Build C# part :

* Create a C# library project
* Add all cshapr_src/*.cs to your C# library project.
* **REMEBER** enable native code debugging in VS : `Configuration Properties/Debugging/Enable native code debugging`

*In development ...*