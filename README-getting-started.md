
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

5. Run game : `Debug/Start Debugging` or press `F5`
