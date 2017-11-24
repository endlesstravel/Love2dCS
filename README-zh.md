
love2dCS - C# Wrapper for Love2d
赋予 C# 使用 love2d 引擎的能力，LÖVE 官网 https://love2d.org/

# 当前测试的平台
windows x86

# Next to do
- [x] retain的函数调用
- [x] 完善C#中的love object继承关系
- [x] string 与 UTF8 编码
- [x] 完善C#实用性
- [ ] 写测试部分
- [ ] 发布到NuGet

# 特点
用 C# 调用 love2d 引擎，一切方法都可以直接按照love2d含义使用 https://love2d.org/wiki/love

# 依赖项目
* [love2d](https://love2d.org/)

# 起因
记得多年前，我想找一个合适的2D引擎做个小游戏，当时只会静态语言的我找到了love2d这个极其优秀的引擎。当时我是对lua这样的动态语言是保持敬而远之的态度的。但是现在，lua和love2d都成为了我最喜欢的东西之一。我一直都能感觉到love2d对新手是多么友好，而我也喜欢C#，于是我决定移植love2d到C#上。