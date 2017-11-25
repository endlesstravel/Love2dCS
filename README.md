
![logo](https://github.com/endlesstravel/Love2dCS/raw/master/img/logo.png "logo") 

love2dCS - C# Wrapper for Love2d
---

Feature
---

*In development ...*

Setup
---

Available on NuGet: https://www.nuget.org/packages/Love2dCS/

And **REMEBER** enable native code debugging in VS : `Configuration Properties/Debugging/Enable native code debugging`

More detail please reference [Setup-Detail](README-Install.md)

Usage
---

1. Emptye projcet
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
using Love2d.Module;
namespace Example
{
    class Program : Love2d.Scene
    {
        public override void Draw()
        {
            Graphics.print("Hello World!", 400, 300);
        }

        static void Main(string[] args)
        {
            Love2d.Boot.Run(new Program());
        }
    }
}
```

3. Drawing an image
``` C#
using Love2d.Type;
using Love2d.Module;
namespace Example
{
    class Program : Love2d.Scene
    {
        Image img = null;

        public override void Load()
        {
            img = Graphics.newImage("logo.png");
        }

        public override void Draw()
        {
            Graphics.draw(img, 300, 200);
        }

        static void Main(string[] args)
        {
            Love2d.Boot.Run(new Program());
        }
    }
}
```

4. Playing a sound
``` C#
using Love2d.Type;
using Love2d.Module;
namespace Example
{
    class Program : Love2d.Scene
    {
        Source source = null;

        public override void Load()
        {
            source = Audio.newSource("music.mp3");
            source.play();
        }

        static void Main(string[] args)
        {
            Love2d.Boot.Run(new Program());
        }
    }
}
```

Develpoment
---

*In development ...*