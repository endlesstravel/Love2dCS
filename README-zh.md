
![logo](https://gitee.com/endlesstravel/love2dCS/raw/master/img/logo.png "logo") 

Love2dCS - C# Wrapper for LÖVE
---
[English](README.md) |
赋予 C# 使用 [LÖVE 引擎](https://love2d.org/)的能力，一切方法都可以直接按照love2d含义使用 https://love2d.org/wiki/love

* Nuget 包(17MB 左右) : https://www.nuget.org/packages/Love2dCS
* 仅 Windows(x64/x86) 的 Nuget 包(6MB 左右): https://www.nuget.org/packages/Love2dCS-win
* 仅 Ubuntu(x64) 的 Nuget 包(6MB 左右): https://www.nuget.org/packages/Love2dCS-ubuntu
* 仅 MacOS(x64) 的 Nuget 包(6MB 左右): https://www.nuget.org/packages/Love2dCS-mac

链接
---
* [文档 https://endlesstravel.github.io](https://endlesstravel.github.io)
* [love wiki](https://love2d.org/wiki/love)
* [安装介绍](https://endlesstravel.github.io/#/tutorial/01.install)
* [Aseprite 支持库](https://gitee.com/endlesstravel/LoveMetaSprite)
* [Spine 支持库](https://gitee.com/endlesstravel/spine-lovecs)
* [与Lua同时工作](https://endlesstravel.github.io/#/tutorial/05.use-lua)
* [测试文件示例](csharp_src/Program.cs) / [物理模块测试文件示例](csharp_test/README.md)


支持平台
---
* `windows-x86` / `windows-x64` / `ubuntu-16 x64` / `ubuntu-18 x64` / `MacOS 10.12+ x64` 加上 `.net startard 1.2` (`.NET Core` | `.NET Framework 4.5.1`  | `Mono 4.6`)

状态
---
Love2dCS API尽可能与原来的 `LÖVE 引擎`保持一致，但任然有一些和原始 `LÖVE` 不一致的地方：


* 可以使用 `Love.XXXX.New*` 访问任意文件夹里的资源内容. 原始 `LÖVE` 仅可以访问当前文件夹的内容。
* 因为 float 的精度问题， `Timer.GetTime` 在本库中表示的是游戏开始到现在的时间。
* LÖVE 中的 `love.math` 在 Love2dCS 被改名为 `Love.Mathf`
* LÖVE 中的  `love.system` 在 Love2dCS 被改名为 `Love.Special`
* Love2dCS 不打算提供线程模块，请使用 C# 中的线程。
* 在 LÖVE 中，为了遵循 lua 语言的习惯，索引是从 1 开始的。而 Love2dCS 中的索引遵循 C# 语言的使用习惯，从 0 开始记起。
* Love2dCS 提供了更多的[内置模块](https://endlesstravel.github.io/#/module/build-in-module-index)，以供使用.
* Love2dCS 可以同时和lua进行工作，详情请参考 ： https://endlesstravel.github.io/#/tutorial/05.use-lua
* Love2dCS 当前基于 [LÖVE 11.1](https://love2d.org/wiki/11.1)

例子
---

绘制文字
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

下一步的开发计划
---

- [ ] 完善文档
- [x] retain的函数调用
- [x] 完善C#中的love object继承关系
- [x] string 与 UTF8 编码
- [x] 完善C#实用性
- [x] 发布到NuGet
- [x] 添加对 win-x64 平台的支持
- [x] 添加物理模块
- [x] 写测试部分

起因
---
记得多年前，我想找一个合适的2D引擎做个小游戏，当时只会静态语言的我找到了love2d这个极其优秀的引擎。当时我是对lua这样的动态语言是保持敬而远之的态度的。但是现在，lua和love2d都成为了我最喜欢的东西之一。我一直都能感觉到love2d对新手是多么友好，而我也喜欢C#，于是我决定移植love2d到C#上。