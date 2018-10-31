
![logo](https://github.com/endlesstravel/Love2dCS/raw/master/img/logo.png "logo") 

Love2dCS - C# Wrapper for LÖVE
---
Love2dCS is a C# Wrapper for [LÖVE game engine](https://love2d.org/), it can be used your C# based Application. 

Love2dCS was designed to be as close as possible to the original LÖVE API, as such the documentation provided from LÖVE largely covers usage of Love2dCS. There is a slight difference between Love2dCS and LÖVE where is :

* The `love.physics` module in LÖVE is not included in Love2dCS
* The `love.math` module in LÖVE is replaced by `Love.Mathf` in Love2dCS
* The `love.thread` module in LÖVE is not ready to supply, you can use threads in C# instead.
* Most index begin from 1 at LÖVE. However, index will begin from 0 at Love2dCS
* *More in development ... *

Love2dCS currently based on [LÖVE 11.0](https://love2d.org/wiki/11.0)

Love2dCS currently supports Windows x86. The next step will be to support windows x64. Linux and OSX temporarily was not supported.

Physics module temporarily not support.

Feature
---

* [Easy to install with Visual Studio (install introduce)](README-Install.md) / 
* Easy to use [Getting Start](README-getting-started.md) / [wiki](https://github.com/endlesstravel/Love2dCS/wiki)

Other
---

* [work with ubuntu + mono-develop](develop.md)

Examples
---

Drawing text
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
[More examples](README-getting-started.md#more-examples)

Next to development
---
 - Support Love 11.0 : in development
 - Improve the document : in development
 - Add support for win-x64 platform
 - Support Ubuntu : in development
 - Add support for Physics

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
| Keyborad      | 95%     |      50%       |   95% Passed  |                 | [detail](Module-devlop-log.md#keyboard)               |
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

Documentation
---
* [wiki](https://github.com/endlesstravel/Love2dCS/wiki)

* [love wiki](https://love2d.org/wiki/love)

Distribute
---
*In development ...*

Development
---
[Develop document](develop.md)
