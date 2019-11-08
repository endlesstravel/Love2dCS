
![logo](https://github.com/endlesstravel/Love2dCS/raw/master/img/logo.png "logo") 

Love2dCS - C# Wrapper for LÖVE
---
赋予 C# 使用 [LÖVE 引擎](https://love2d.org/)的能力，一切方法都可以直接按照love2d含义使用 https://love2d.org/wiki/love。

请注意 Love2dCS 与 LÖVE 的不同点 ：

* Love2dCS 不打算提供线程模块，请使用 C# 中的线程。
* 在 LÖVE 中，为了遵循 lua 语言的习惯，索引是从 0 开始计算的。而 Love2dCS 中的索引遵循 C# 语言的使用习惯，从 0 开始记起。

# 当前已发布的平台
windows x86 / windows x64 / ubuntu x64 / MacOS x64

# 特点
* 容易使用
* 容易安装

# 安装

* 项目已经发布到 NuGet [https://www.nuget.org/packages/Love2dCS/]( https://www.nuget.org/packages/Love2dCS/)

# 下一步的开发计划

- [x] retain的函数调用
- [x] 完善C#中的love object继承关系
- [x] string 与 UTF8 编码
- [x] 完善C#实用性
- [x] 发布到NuGet
- [x] 添加对 win-x64 平台的支持
- [x] 添加物理模块
- [x] 完善文档
- [x] 写测试部分

# 起因
记得多年前，我想找一个合适的2D引擎做个小游戏，当时只会静态语言的我找到了love2d这个极其优秀的引擎。当时我是对lua这样的动态语言是保持敬而远之的态度的。但是现在，lua和love2d都成为了我最喜欢的东西之一。我一直都能感觉到love2d对新手是多么友好，而我也喜欢C#，于是我决定移植love2d到C#上。