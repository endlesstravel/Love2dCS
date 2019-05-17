using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq;

namespace Love
{
    /// <summary>
    /// Provides an interface to lua script.
    /// </summary>
    public partial class Lua
    {
        public enum SupportedTransType
        {
            Void,
            Boolean,
            Int,
            Float,
            String,
            ArrayInt,
            ArrayBoolean,
            ArrayFloat,
            ArrayString,
        }

        public static bool IsTransAbleType(Type type, out SupportedTransType result)
        {
            if (type == (typeof(void)))
            {
                result = SupportedTransType.Void;
                return true;
            }
            else if (type == (typeof(bool)))
            {
                result = SupportedTransType.Boolean;
                return true;
            }
            else if (
                type == (typeof(byte))
                || type == (typeof(sbyte))
                || type == (typeof(Int16))
                || type == (typeof(UInt16))
                || type == (typeof(int))
                || type == (typeof(uint))
                )
            {
                result = SupportedTransType.Int;
                return true;
            }
            else if (type == (typeof(float)) || type == (typeof(double)))
            {
                result = SupportedTransType.Float;
                return true;
            }
            else if (type == (typeof(char)) || type == (typeof(string)))
            {
                result = SupportedTransType.String;
                return true;
            }
            else if (type == (typeof(bool[])))
            {
                result = SupportedTransType.ArrayBoolean;
                return true;
            }
            else if (
                type == (typeof(byte[]))
                || type == (typeof(sbyte[]))
                || type == (typeof(Int16[]))
                || type == (typeof(UInt16[]))
                || type == (typeof(int[]))
                || type == (typeof(uint[]))
                )
            {
                result = SupportedTransType.ArrayInt;
                return true;
            }
            else if (type == (typeof(float[])) || type == (typeof(double[])))
            {
                result = SupportedTransType.ArrayFloat;
                return true;
            }
            else if (type == (typeof(char[])) || type == (typeof(string[])))
            {
                result = SupportedTransType.ArrayString;
                return true;
            }


            result = SupportedTransType.Void;
            return false;
        }

        public class CallbackCSharpTargetInfo
        {
            public class PairCastInfo
            {
                readonly public Type type;
                readonly public SupportedTransType stype;
                public PairCastInfo(Type type, SupportedTransType stype)
                {
                    this.type = type;
                    this.stype = stype;
                }
            }
            readonly public object target;
            readonly public string name;
            readonly public MethodInfo info;
            readonly public SupportedTransType returnType;
            readonly public List<PairCastInfo> argTypeList;

            public CallbackCSharpTargetInfo(object target, string name, MethodInfo info, SupportedTransType returnType, List<PairCastInfo> argTypeList)
            {
                this.target = target;
                this.name = name;
                this.info = info;
                this.returnType = returnType;
                this.argTypeList = argTypeList;
            }

            public static CallbackCSharpTargetInfo Prase(object target, string funcName)
            {
                if (target == null)
                    throw new ArgumentNullException("target");
                if (funcName == null)
                    throw new ArgumentNullException("funcName");

                var type = target is Type ? target as Type : target.GetType();
                var info = type.GetMethod(funcName,
                        BindingFlags.Instance
                        | BindingFlags.Public
                        | BindingFlags.Static
                        | BindingFlags.NonPublic
                        | BindingFlags.FlattenHierarchy);
                if (info == null)
                {
                    throw new Exception($"register function error: {funcName} is not exists !");
                }
                if (IsTransAbleType(info.ReturnType, out var returnType) == false)
                    return null;

                var parameterList = info.GetParameters();
                List<PairCastInfo> argttList = new List<PairCastInfo>(parameterList.Length);
                foreach (var param in parameterList)
                {
                    if (IsTransAbleType(param.ParameterType, out var tt) == false)
                        return null;
                    argttList.Add(new PairCastInfo(param.ParameterType, tt));
                }

                return new CallbackCSharpTargetInfo(target is Type ? null : target, funcName, info, returnType, argttList);
            }


            public int Call()
            {
                var luaStackTop = RawOperate.GetTop();
                var parameters = new object[argTypeList.Count];
                if (luaStackTop - 1 < argTypeList.Count)
                {
                    var info = $"the function require number of argument is {argTypeList.Count}, actual is {luaStackTop - 1}";
                    DoString($"error('{info}')");
                    throw new ArgumentException(info);
                }

                for (int i = 0; i < argTypeList.Count && (i + 2) <= luaStackTop; i++)
                {
                    var type = argTypeList[i].type;
                    if (type == typeof(bool))
                        parameters[i] = (bool)RawOperate.CheckToBoolean(i + 2);
                    else if (type == typeof(byte))
                        parameters[i] = (byte)RawOperate.CheckToInteger(i + 2);
                    else if (type == (typeof(sbyte)))
                        parameters[i] = (sbyte)RawOperate.CheckToInteger(i + 2);
                    else if (type == (typeof(Int16)))
                        parameters[i] = (Int16)RawOperate.CheckToInteger(i + 2);
                    else if (type == (typeof(UInt16)))
                        parameters[i] = (UInt16)RawOperate.CheckToInteger(i + 2);
                    else if (type == (typeof(int)))
                        parameters[i] = (int)RawOperate.CheckToInteger(i + 2);
                    else if (type == (typeof(uint)))
                        parameters[i] = (uint)RawOperate.CheckToInteger(i + 2);
                    else if (type == (typeof(float)))
                        parameters[i] = (float)RawOperate.CheckToNumber(i + 2);
                    else if (type == (typeof(double)))
                        parameters[i] = (double)RawOperate.CheckToNumber(i + 2);
                    else if (type == (typeof(string)))
                        parameters[i] = (string)RawOperate.CheckToString(i + 2);
                    else if (type == typeof(bool[]))
                        parameters[i] = (bool)RawOperate.CheckToBoolean(i + 2);
                    else if (type == typeof(byte[]))
                        parameters[i] = (byte[])RawOperate.CheckToArrayInt(i + 2).Select(item => (byte)item).ToArray();
                    else if (type == (typeof(sbyte[])))
                        parameters[i] = (sbyte[])RawOperate.CheckToArrayInt(i + 2).Select(item => (sbyte)item).ToArray();
                    else if (type == (typeof(Int16[])))
                        parameters[i] = (Int16[])RawOperate.CheckToArrayInt(i + 2).Select(item => (Int16)item).ToArray();
                    else if (type == (typeof(UInt16[])))
                        parameters[i] = (UInt16[])RawOperate.CheckToArrayInt(i + 2).Select(item => (UInt16)item).ToArray();
                    else if (type == (typeof(int[])))
                        parameters[i] = (int[])RawOperate.CheckToArrayInt(i + 2);
                    else if (type == (typeof(uint[])))
                        parameters[i] = (uint[])RawOperate.CheckToArrayInt(i + 2).Select(item => (uint)item).ToArray();
                    else if (type == (typeof(float[])))
                        parameters[i] = (float[])RawOperate.CheckToArrayNumber(i + 2);
                    else if (type == (typeof(double[])))
                        parameters[i] = (double[])RawOperate.CheckToArrayNumber(i + 2).Select(item => (double)item).ToArray();
                    else if (type == (typeof(string[])))
                        parameters[i] = (string[])RawOperate.CheckToArrayString(i + 2);
                }

                object returnValue = info.Invoke(target, parameters);
                if (returnValue      is bool) RawOperate.PushBoolean((bool)returnValue);
                else if (returnValue is byte) RawOperate.PushInteger((byte)returnValue);
                else if (returnValue is sbyte) RawOperate.PushInteger((sbyte)returnValue);
                else if (returnValue is Int16) RawOperate.PushInteger((Int16)returnValue);
                else if (returnValue is UInt16) RawOperate.PushInteger((UInt16)returnValue);
                else if (returnValue is int) RawOperate.PushInteger((int)returnValue);
                else if (returnValue is uint) RawOperate.PushInteger((int)((uint)returnValue));
                else if (returnValue is float) RawOperate.PushNumber((float)returnValue);
                else if (returnValue is double) RawOperate.PushNumber((float)((double)returnValue));
                else if (returnValue is string) RawOperate.PushString((string)returnValue);
                else if (returnValue is bool[]) RawOperate.PushBooleanArray(((bool[])returnValue));
                else if (returnValue is byte[]) RawOperate.PushIntegerArray(((byte[])returnValue).Select(item => (int)item).ToArray());
                else if (returnValue is sbyte[]) RawOperate.PushIntegerArray(((sbyte[])returnValue).Select(item => (int)item).ToArray());
                else if (returnValue is Int16[]) RawOperate.PushIntegerArray(((Int16[])returnValue).Select(item => (int)item).ToArray());
                else if (returnValue is UInt16[]) RawOperate.PushIntegerArray(((UInt16[])returnValue).Select(item => (int)item).ToArray());
                else if (returnValue is int[]) RawOperate.PushIntegerArray(((int[])returnValue));
                else if (returnValue is uint[]) RawOperate.PushIntegerArray(((uint[])returnValue).Select(item => (int)item).ToArray());
                else if (returnValue is float[]) RawOperate.PushNumberArray(((float[])returnValue));
                else if (returnValue is double[]) RawOperate.PushNumberArray(((double[])returnValue).Select(item => (float)item).ToArray());
                else if (returnValue is string[]) RawOperate.PushStringArray(((string[])returnValue));

                return info.ReturnType != typeof(void) ? 1 : 0;
            }
        }

        readonly static Dictionary<string, CallbackCSharpTargetInfo> funcDict = new Dictionary<string, CallbackCSharpTargetInfo>();


        /// <summary>
        /// register c# function on object, for example Lua.RegisterFunction(typeof(Math), "Cos", "cos"), 
        /// the you can do  `print(love.sharp.cos(3.14))` in lua code
        /// </summary>
        /// <param name="target"></param>
        /// <param name="functionName"></param>
        /// <param name="luaName">luaName to use</param>
        /// <returns></returns>
        public static bool RegisterFunction(object target, string functionName, string luaName)
        {
            var ccti = CallbackCSharpTargetInfo.Prase(target, functionName);
            if (ccti == null)
            {
                return false;
            }
            funcDict[luaName] = ccti;
            return true;
        }

        public static bool RegisterFunction(object target, string functionName)
        {
            return RegisterFunction(target, functionName, functionName);
        }

        static WrapCSharpCommunicationFuncDelegate static_WCSCFD = FunctionBack;
        static int FunctionBack(IntPtr functionNameStr)
        {
            //RawOperate.DebugStackDump();
            var name = DllTool.WSToStringAndRelease(functionNameStr);

            if (funcDict.TryGetValue(name, out var callbackCSharpTargetInfo))
            {
                return callbackCSharpTargetInfo.Call();
            }

            throw new Exception(" love.sharp." + name + "  is not exists !");
        }

        static void CheckLuaModleInitStatus()
        {
            if (IsInit == false)
            {
                throw new Exception("lua modue not init yet, please call `Love.Lua.Init` or `Love.Lua.Load` or `Love.Lua.LoadFromString` first");
            }
        }

        /// <summary>
        /// is lua module already init ?
        /// </summary>
        public static bool IsInit { private set; get; } = false;


        /// <summary>
        /// init lua moduel automatically, then do the lua file.
        /// </summary>
        /// <param name="filepath">file to execute</param>
        public static void Load(string filepath)
        {
            if (IsInit)
            {
                Boot.LogWarning("[warning] Lua.Load: lua moudle already init. if you want execute file, call `Love.Lua.DoFile` please");
                return;
            }
            Init();
            DoFile(filepath);
            DoString("if love.load then love.load() end");
        }

        /// <summary>
        /// init lua moduel automatically, then do the lua code.
        /// </summary>
        /// <param name="luaCode">code to execute</param>
        public static void LoadFromString(string luaCode)
        {
            if (IsInit)
            {
                Boot.LogWarning("[warning] Lua.LoadFromString: lua moudle already init. if you want execute file, call `Love.Lua.DoFile` please");
                return;
            }
            Init();
            DoString(luaCode);
            DoString("if love.load then love.load() end");
        }

        /// <summary>
        /// manual init lua module
        /// </summary>
        public static void Init()
        {
            InitInternal(IntPtr.Zero);
        }

        /// <summary>
        /// initiate lua module
        /// </summary>
        /// <param name="luaState">Assigning to internal Lua state, will be injected love function. if it is IntPtr.Zero, lua state will create automatic</param>
        public static void InitInternal(IntPtr luaState)
        {
            if (IsInit)
            {
                return;
            }
            // not Do not pass FunctionBack in directly ! the delegate variable from Automatically generate temporary variables!
            Love2dDll.wrap_love_dll_luasupport_init(luaState, static_WCSCFD);
            IsInit = true;
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

        /// <summary>
        /// execuate lua code with given file path.
        /// </summary>
        /// <param name="luaCode">file path to execuate</param>
        public static void DoFile(string filepath)
        {
            Love2dDll.wrap_love_dll_luasupport_doFile(DllTool.GetNullTailUTF8Bytes(filepath));
        }

        /// <summary>
        /// call love.update(dt)
        /// </summary>
        /// <param name="dt"></param>
        public static void Update(float dt)
        {
            CheckLuaModleInitStatus();
            DoString($"if love.update then love.update({dt}) end");
        }

        /// <summary>
        /// call love.draw()
        /// </summary>
        public static void Draw()
        {
            CheckLuaModleInitStatus();
            DoString("if love.draw then love.draw() end");
        }


        public static class RawOperate
        {
            public static void DebugStackDump()
            {
                Love2dDll.wrap_love_dll_luasupport_debugStackDump();
            }

            public static int GetTop()
            {
                int out_result;
                Love2dDll.wrap_love_dll_luasupport_getTop(out out_result);
                return out_result;
            }

            public static void SetTop(int idx)
            {
                Love2dDll.wrap_love_dll_luasupport_setTop(idx);
            }

            public static string CheckToString(int index)
            {
                IntPtr out_result;
                Love2dDll.wrap_love_dll_luasupport_checkToString(index, out out_result);
                return DllTool.WSToStringAndRelease(out_result);
            }

            public static float CheckToNumber(int index)
            {
                float out_result;
                Love2dDll.wrap_love_dll_luasupport_checkToNumber(index, out out_result);
                return out_result;
            }

            public static int CheckToInteger(int index)
            {
                int out_result;
                Love2dDll.wrap_love_dll_luasupport_checkToInteger(index, out out_result);
                return out_result;
            }

            public static bool CheckToBoolean(int index)
            {
                bool out_result;
                Love2dDll.wrap_love_dll_luasupport_checkToBoolean(index, out out_result);
                return out_result;
            }

            public static bool IsTable(int index)
            {
                bool out_result;
                Love2dDll.wrap_love_dll_luasupport_isTable(index, out out_result);
                return out_result;
            }

            public static void PushInteger(int num)
            {
                Love2dDll.wrap_love_dll_luasupport_pushInteger(num);
            }

            public static void PushNumber(float num)
            {
                Love2dDll.wrap_love_dll_luasupport_pushNumber(num);
            }

            public static void PushBoolean(bool v)
            {
                Love2dDll.wrap_love_dll_luasupport_pushBoolean(v);
            }

            public static void PushString(string str)
            {
                Love2dDll.wrap_love_dll_luasupport_pushString(DllTool.GetNullTailUTF8Bytes(str));
            }
            public static void PushString(byte[] str)
            {
                Love2dDll.wrap_love_dll_luasupport_pushString(str);
            }

            public static void PushIntegerArray(int[] num)
            {
                Love2dDll.wrap_love_dll_luasupport_pushIntegerArray(num, num.Length);
            }

            public static void PushNumberArray(float[] num)
            {
                Love2dDll.wrap_love_dll_luasupport_pushNumberArray(num, num.Length);
            }

            public static void PushBooleanArray(bool[] num)
            {
                Love2dDll.wrap_love_dll_luasupport_pushBooleanArray(num, num.Length);
            }

            public static void PushStringArray(string[] texts)
            {
                DllTool.ExecuteNullTailStringArray(texts, (pointers) =>
                {
                    Love2dDll.wrap_love_dll_luasupport_pushStringArray(pointers, pointers.Length);
                });
            }


            // TODO: finishe function wrap_love_dll_luasupport_checkToArray
            // TODO: finishe function wrap_love_dll_luasupport_checkTo
            // TODO: finishe function wrap_love_dll_luasupport_checkToArrayNumber
            public static bool[] CheckToArrayBoolean(int index)
            {
                IntPtr out_result;
                Love2dDll.wrap_love_dll_luasupport_checkToArrayBoolean(index, out out_result, out var len);
                return DllTool.ReadBooleansAndRelease(out_result, len);
            }
            public static int[] CheckToArrayInt(int index)
            {
                IntPtr out_result;
                Love2dDll.wrap_love_dll_luasupport_checkToArrayInt(index, out out_result, out var len);
                return DllTool.ReadInt32sAndRelease(out_result, len);
            }
            public static float[] CheckToArrayNumber(int index)
            {
                IntPtr out_result;
                Love2dDll.wrap_love_dll_luasupport_checkToArrayNumber(index, out out_result, out var len);
                return DllTool.ReadFloatsAndRelease(out_result, len);
            }
            public static string[] CheckToArrayString(int index)
            {
                IntPtr out_result;
                Love2dDll.wrap_love_dll_luasupport_checkToArrayString(index, out out_result);
                return DllTool.WSSToStringListAndRelease(out_result);
            }

        }
    }
}
