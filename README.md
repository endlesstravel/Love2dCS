
![logo](https://github.com/endlesstravel/Love2dCS/raw/master/img/logo.png "logo")

Love2dCS - C# Wrapper for LÖVE
---
[中文版](README-zh.md) | [gitee-国内访问速度较快](https://gitee.com/endlesstravel/love2dCS)

Love2dCS is a C#/F# Wrapper for [LÖVE game engine](https://love2d.org/), it can be used your C#/F# based Application.

* Nuget Package - ~17MB: https://www.nuget.org/packages/Love2dCS
* Windows(x64/x86) Only Nuget Package - ~6MB: https://www.nuget.org/packages/Love2dCS-win
* Ubuntu(x64) Only Nuget Package - ~6MB: https://www.nuget.org/packages/Love2dCS-ubuntu
* MacOS(x64) Only Nuget Package - ~6MB: https://www.nuget.org/packages/Love2dCS-mac

Links
---
* 📃 [Document at https://endlesstravel.github.io](https://endlesstravel.github.io) | [中文文档（还没写完）](https://endlesstravel.gitee.io/lovesharpdocument/)
* 📕 [love wiki](https://love2d.org/wiki/love)
* [ImGui](https://github.com/ocornut/imgui) Support: [here](https://gitee.com/endlesstravel/DearLoveGUI), based on [ImGui.NET](https://github.com/mellinoe/ImGui.NET)
* [Aseprite](https://www.aseprite.org/) Runtime Support : [here](https://gitee.com/endlesstravel/LoveMetaSprite)
* [Spine](http://esotericsoftware.com/) Runtime Support : [here](https://gitee.com/endlesstravel/spine-lovecs)
* [Test file example](csharp_src/Program.cs) / [Physics Test example](csharp_test/README.md)
* [Easy to install with Visual Studio/MonoDevelop (install introduce)](https://endlesstravel.github.io/#/tutorial/01.install)


Support Platform
---
* `windows-x86` / `windows-x64` / `ubuntu-16 x64` / `ubuntu-18 x64` / `MacOS 10.12+ x64` with `.net startard 1.2` (`.NET Core` | `.NET Framework 4.5.1`  | `Mono 4.6`)

Status
---
Love2dCS was designed to be as close as possible to the original LÖVE API, as such the documentation provided from LÖVE largely covers usage of Love2dCS. There is a difference between Love2dCS and LÖVE where is :

* The `Love.XXX.New*` in C# can access to files anywhere. In the original functin can only access the contents of the current folder.
* The `love.timer.getTime` in C# as `The time in seconds since the start of the game` beacuse of `C# double to float precision`
* The `love.math` module in LÖVE is named `Love.Mathf` in Love2dCS
* The `love.system` module in LÖVE is named `Love.Special` in Love2dCS
* The `love.thread` module in LÖVE is not supply, you can use [Threading.Thread](https://docs.microsoft.com/en-us/dotnet/api/system.threading.thread) in C# instead.
* Most index begin from 1 at LÖVE. However, but index will begin from 0 at Love2dCS
* Love2dCS provide more [build-in module](https://endlesstravel.github.io/#/module/build-in-module-index) to convience use.
* You can work with lua as well, but only `love.load` `love.update` and `love.draw` are native supported : [Work with lua](https://endlesstravel.github.io/#/tutorial/05.use-lua). The rest of callback function is not supported.
* Love2dCS currently based on [LÖVE 11.1](https://love2d.org/wiki/11.1)

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

Next to development
---
 - Improve the document : in development
 - Fully support Love 11.2 : in development

 Finished:
 - [√] Support Ubuntu : now support ubuntu 16/18
 - [√] Add support for .net core
 - [√] Support call lua function : Love.Lua.Call(name, ...arg) / Love.Lua.LoadString / Love.Lua.LoadFile ([here](https://endlesstravel.github.io/#/tutorial/05.use-lua))
 - [√] Support helpper function : `Keyboard.IsPressed` / `Keyboard.IsReleased` / `Joystick.IsPressed` / `Joystick.IsReleased` / `Joystick.IsGamepadPressed`  /`Joystick.IsGamepadReleased`
 - [√] Add support for Physics

Test
---
| Module                | Process | code comment   |     Test      |   ubuntu Test    | Remark         |
| ----------------------|--------:|---------------:|--------------:|----------------:| --------------:|
| Audio                 | 80%     |      80%       |  50% test     |                 |                |
| Data                  |   /     |        /       |               |                 | Need to binding               |
| Event                 | 50%     |        /       |               |                 |                |
| FileSystem            | 80%     |      80%       |  90% test     |                 | [detail](Module-devlop-log.md#filesystem)           |
| Font                  | 80%     |      90%       |               |                 |                |
| Graphics              | 80%     |      00%       |               |                 |                |
| Image                 | 80%     |      90%       |               |                 |                |
| Joystick              | 80%     |      00%       |               |                 | Need add code comment               |
| Keyborad              | 95%     |      95%       |   95% test    |                 | [detail](Module-devlop-log.md#keyboard)               |
| Mathf (love.math)     | 80%     |      90%       |               |                 |                |
| Mouse                 | 90%     |      90%       |   90% test    |                 | `Mouse.SetRelativeMode` will crash, need to repair               |
| Physics               | 80%     |        /       |   20% test    |                 | Need more [test case](csharp_test/README.md)     |
| Sound                 | 90%     |      90%       |               |                 |                |
| Special (love.system) | 90%     |      90%       |   90% test    |                 |                |
| Thread                |   /     |        /       |               |                 | Not supported / No need to support   |
| Timer                 | 95%     |      95%       |               |                 |                |
| Touch                 | 80%     |      00%       |               |                 |                |
| Video                 | 80%     |      80%       |   90% test    |                 |                |
| Window                | 80%     |      80%       |               |                 |                |

[develop log](Module-devlop-log.md)

[change log](changelog.txt)

Distribute
---
* (recommand) on .net 2.2  : use tool https://github.com/Hubert-Rybak/dotnet-warp to publish single executable.
* (recommand) on .net 3.0+ : Self-contained can be used to publish single executable

* ref: https://www.hanselman.com/blog/MakingATinyNETCore30EntirelySelfcontainedSingleExecutable.aspx
* publish ref: https://executecommands.com/publishing-single-executable-exe-in-net-core-3-0/
* msdn ref: https://docs.microsoft.com/en-us/dotnet/core/whats-new/dotnet-core-3-0#single-file-executables
* msdn ref `<PublishTrimmed/>` : https://docs.microsoft.com/en-us/dotnet/core/whats-new/dotnet-core-3-0#assembly-linking
* designs file: https://github.com/dotnet/designs/blob/master/accepted/single-file/design.md

Development
---
[Develop document](develop.md)

Contributor
---
* thanks [matej-zajacik](https://github.com/matej-zajacik) for his contribute on add and improve [document](https://github.com/endlesstravel/endlesstravel.github.io). Without him, the grammatical errors of documents would flourish everywhere.