Work in process [WIP]
*in development ...*

## The way of Love2DCS work

Love2DCS will binding C function and export to dynamic library to provide function to C# code. C# will use `System.Runtime.InteropServices` to interop with native libraries. : https://www.mono-project.com/docs/advanced/pinvoke/

## Setup develop environment

### 1. Windows [](img/window_logo.png)

a. tool

* Window 7/8/8.1/10
* `Viual Studio 2013` or `Visual Studio 2015` or `Visaul Studio 2017`  https://visualstudio.microsoft.com
* Cmake https://cmake.org/
* mercurial https://www.mercurial-scm.org/

b. build C Part :

* Follow the instructions at the [megasource](https://bitbucket.org/rude/megasource) repository page to build LÖVE 11.0+. if you use Visual studio 2017, just setup v140 toolset : https://blogs.msdn.microsoft.com/vcblog/2017/11/02/visual-studio-build-tools-now-include-the-vs2017-and-vs2015-msvc-toolsets/, and build megasource with VC++ 2015.3 v14.00 (v140) toolset : https://stackoverflow.com/questions/47154454/cmake-how-to-specify-vs2015-3-toolset-with-vs2017-installed. or `cmake -G "Visual Studio 15" -T v140`
* clone repository `git clone https://github.com/endlesstravel/Love2dCS`
* Add files `c_api_src/wrap_love_dll.cpp` and `c_api_src/wrap_love_dll.h` to your LÖVE project in `liblove`

![liblove-src](https://github.com/endlesstravel/Love2dCS/raw/master/img/006-liblove-src.png "liblove-src")
* follow example in `wrap_love_dll.cpp` and `wrap_love_dll.h` to modify / add more function
* this step will generate  `love.dll` / `lua51.dll` / `mpg123` / `OpenAL32.dll` / `SDL2.dll` , this is what we need.

c. build C# part :

* create C# console application with visual studio
* add all code `csharp_src/*.cs` to your C# project.
* set workspace to `love.dll` locate directory or just copy  `love.dll` / `lua51.dll` / `mpg123` / `OpenAL32.dll` / `SDL2.dll` to your `xxxxx.exe` locate directory.

### 2. Ubuntu [](img/ubuntu_logo.png)

a. tool

* ubuntu 14.4 (china mirrors source: https://mirrors.tuna.tsinghua.edu.cn/help/ubuntu/)

b.  build C Part :

* clone repository: `git clone https://github.com/love2d/love`
* to the root of the love srource: `cd love`
* then change CMake file
from
```CMake
set(LOVE_SRC_MODULE_LOVE
	src/modules/love/love.cpp
	src/modules/love/love.h
)
```
to
```CMake
set(LOVE_SRC_MODULE_LOVE
	src/modules/love/love.cpp
	src/modules/love/love.h
	src/modules/love/wrap_love_dll.cpp
	src/modules/love/wrap_love_dll.h
)
```
* download `wrap_love_dll.cpp` and `wrap_love_dll.h` to `/src/modules/love`
```shell
cd ./src/modules/love
wget -o src/modules/love/wrap_love_dll.cpp https://github.com/endlesstravel/Love2dCS/raw/master/c_api_src/wrap_love_dll.cpp
wget -o src/modules/love/wrap_love_dll.h https://github.com/endlesstravel/Love2dCS/raw/master/c_api_src/wrap_love_dll.h
```
* Follow the instructions at the [Building LÖVE](https://love2d.org/wiki/Building_L%C3%96VE) to build `LÖVE`
* library will finded at `src\.libs\love-11.1.so`, this is what we need.
* [Question about Linux binaries size](https://love2d.org/forums/viewtopic.php?f=4&t=85332&p=221386&hilit=build+LÖVE+liblove) :  call 'strip -s' on them, to remove (mostly) debug information.
``` bash
strip -s love-11.1.so
```

c. build C# Part :
* install monodevelop : https://www.monodevelop.com/download/#fndtn-download-lin-ubuntu
* open monodevelop, create C# Console Application, add all `*.cs` code under `csharp_src` folder to your C# project.
* copy `love-11.1.so` to your build path and rename to `liblove.so`
* build & run

### docker build
```bash
sed  '/set\s*(\s*LOVE_SRC_MODULE_LOVE/a\src/modules/love/wrap_love_dll.cpp\nsrc/modules/love/wrap_love_dll.h' love/CMakeLists.txt > love/CMakeLists.txt.tmp
mv   love/CMakeLists.txt              love/CMakeLists.txt.old
mv   love/CMakeLists.txt.tmp       love/CMakeLists.txt
wget -o  love/src/modules/love/wrap_love_dll.cpp  https://github.com/endlesstravel/Love2dCS/raw/master/c_api_src/wrap_love_dll.cpp
wget -o  love/src/modules/love/wrap_love_dll.h      https://github.com/endlesstravel/Love2dCS/raw/master/c_api_src/wrap_love_dll.h
ls   love/src/modules/love
```

## Code Convention
### UTF8

Love.Graphics.Print / Love.Graphics.Printf / Love.Graphics can receive 0-terminated byte array (https://docs.microsoft.com/en-us/cpp/cpp/string-and-character-literals-cpp?view=vs-2017#narrow-string-literals) to represent end of string input. for example :

``` C#
    var bytes = new bytes[] {65, 66, 67, 0}; // "ABC"
    Love.Graphics.Print(bytes, 0, 0); // print "ABC" at position (0, 0)
```
### Resource manage
