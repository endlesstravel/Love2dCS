
![logo](https://github.com/endlesstravel/Love2dCS/raw/master/img/logo.png "logo") 

Love2dCS - C# Wrapper for LÖVE
---
Love2dCS is a C# Wrapper for [LÖVE game engine](https://love2d.org/), it can be used your C# based Application. 

Love2dCS was designed to be as close as possible to the original LÖVE API, as such the documentation provided from LÖVE largely covers usage of Love2dCS. There is a difference between Love2dCS and LÖVE where is :

* The `love.physics` module in LÖVE is not included in Love2dCS yet.
* The `love.math` module in LÖVE is named `Love.Mathf` in Love2dCS
* The `love.thread` module in LÖVE is not supply, you can use [Threading.Thread](https://docs.microsoft.com/en-us/dotnet/api/system.threading.thread) in C# instead.
* Most index begin from 1 at LÖVE. However, index will begin from 0 at Love2dCS
* Love2dCS provide more [build-in module](https://endlesstravel.github.io/#/module/README?id=addition-modules) to convience use.

Love2dCS currently based on [LÖVE 11.1](https://love2d.org/wiki/11.1)

Love2dCS currently supports Windows x86 / x64. Linux and OSX temporarily was not supported.

Physics module temporarily not support.

You can work with lua as will, but only `love.load` `love.update` and `love.draw` are native supported : [Work with lua](https://endlesstravel.github.io/#/tutorial/05.use-lua)

Feature
---
* [Easy to install with Visual Studio (install introduce)](https://endlesstravel.github.io/#/tutorial/01.install) 
* [Work with lua](https://endlesstravel.github.io/#/tutorial/05.use-lua)
* [Work with ubuntu + mono-develop](develop.md)

Documentation
---
* [Document https://endlesstravel.github.io](https://endlesstravel.github.io)
* [love wiki](https://love2d.org/wiki/love)

Examples
---

Drawing text
``` C#
using Love;
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
```
* [Getting Start](https://endlesstravel.github.io/#/tutorial/README)
* [Test file example](csharp_src/Program.cs)

Next to development
---
 - Fully support Love 11.0 : in development
 - Improve the document : in development
 - Support call lua function : Love.Lua.Call(name, ...arg) / Love.Lua.LoadString / Love.Lua.LoadFile
 - Support Func(Vector2) on Func(x, y)
 - Support Ubuntu : in development
 - Add support for Physics
 - Add all SetXXX(float x, float y) / GetXXX(out float x, out float y)  with override SetXXX(Vector2) / Vector2 GetXXX().

 Finished:
 -[x] Support helpper function : Love.Keyboard.IsPressed /Love.Keyboard.IsReleased

| Module        | Process | code comment   |     Test      |   ubuntu Test    | Remark         |
| ------------- |--------:|---------------:|--------------:|----------------:| --------------:|
| Audio         | 80%     |      80%       |  50% passed   |                 |                |
| Data          | 00%     |      00%       |               |                 | Need to binding               |
| Event         | 50%     |      00%       |               |                 |                |
| FileSystem    | 80%     |      80%       |  90% passed   |                 | [detail](Module-devlop-log.md#filesystem)           |
| Font          | 80%     |      90%       |               |                 |                |
| Graphics      | 80%     |      00%       |               |                 |                |
| Image         | 80%     |      90%       |               |                 |                |
| Joystick      | 80%     |      00%       |               |                 |                |
| Keyborad      | 95%     |      95%       |   95% Passed  |                 | [detail](Module-devlop-log.md#keyboard)               |
| Mathf         | 80%     |      90%       |               |                 |                |
| Mouse         | 90%     |      90%       |   90% Passed  |                 |   `Mouse.SetRelativeMode` will crash, need to repair               |
| Physics       |  /      |      /         |               |                 | Not supported               |
| Sound         | 90%     |      90%       |               |                 |                |
| System        | 00%     |      00%       |               |                 | Need to binding               |
| Thread        |   /     |        /       |               |                 | Not supported               |
| Timer         | 95%     |      95%       |               |                 |                |
| Touch         | 80%     |      00%       |               |                 |                |
| Video         | 80%     |      80%       |   90% passed  |                 |                |
| Window        | 80%     |      80%       |               |                 |                |

[develop log](Module-devlop-log.md)

[change log](changelog.txt)

Distribute
---
*WIP*

Development
---
[Develop document](develop.md)
