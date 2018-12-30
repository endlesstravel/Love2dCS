using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Love
{
    /// <summary>
    /// Provides an interface to lua script.
    /// </summary>
    public partial class Lua
    {
        /// <summary>
        /// initiate lua module
        /// </summary>
        /// <param name="luaState">Assigning to internal Lua state, will be injected love function. if it is IntPtr.Zero, lua state will create automatic</param>
        public static void Init(IntPtr luaState)
        {
            Love2dDll.wrap_love_dll_luasupport_init(luaState);
            var loveModule = new string[]{
                "data",
                "thread",
                "timer",
                "event",
                "keyboard",
                "joystick",
                "mouse",
                "touch",
                "sound",
                "system",
                "audio",
                "image",
                "video",
                "font",
                "window",
                "graphics",
                "math",
                "physics",
            };
            DoString($"require('love'); ");
            DoString(string.Join("\n", loveModule.Select(module => $"require('love.{module}');")));
        }

        /// <summary>
        /// execuate lua code.
        /// </summary>
        /// <param name="luaCode">lua code to execuate</param>
        public static void DoString(params string[] luaCode)
        {
            if (luaCode != null)
            {
                DoString(string.Join(";\n", luaCode));
            }
        }

        /// <summary>
        /// execuate lua code.
        /// </summary>
        /// <param name="luaCode">lua code to execuate</param>
        public static void DoString(string luaCode)
        {
            Love2dDll.wrap_love_dll_luasupport_doString(DllTool.GetNullTailUTF8Bytes(luaCode));
        }


        static string LuaLoveMainFileName = null;

        /// <summary>
        /// call internall, load lua file if <see cref="BootConfig.LuaLoveMainFile"/> is not null 
        /// </summary>
        /// <param name="fileName"></param>
        internal static void LoadLoveMainFile(string fileName)
        {
            if (LuaLoveMainFileName == null)
            {
                LuaLoveMainFileName = fileName;
                DoString($"require('{LuaLoveMainFileName}')");
                DoString($"if love.load then love.load() end");
            }
        }

        /// <summary>
        /// call love.update(dt) 
        /// </summary>
        /// <param name="dt"></param>
        public static void Update(float dt)
        {
            if (LuaLoveMainFileName != null)
            {
                DoString($"if love.update then love.update({dt}) end");
            }
            else
            {
                throw new Exception("boot config of LuaLoveMainFile was not specified");
            }
        }

        /// <summary>
        /// call love.draw()
        /// </summary>
        public static void Draw()
        {
            if (LuaLoveMainFileName != null)
            {
                DoString("if love.draw then love.draw() end");
            }
            else
            {
                throw new Exception("boot config of LuaLoveMainFile was not specified");
            }
        }
    }
}
