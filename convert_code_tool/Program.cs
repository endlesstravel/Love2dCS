using System;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using System.Collections.Generic;

namespace convert_code_tool
{
    class Program
    {
        // void|bool   wrap_love_dll_xxxxxxxxxx(.......)
        static Regex firstRegex = new Regex(@"(\w+)\s+wrap_love_dll_(\w+)\(([^\)]*)\)", RegexOptions.Singleline);

        // (int **x, float d, Body** out_)
        static Regex paramsRegex = new Regex(@"(\s*(\(|,)\s*(\w+)\s*(\**)\s*(\w+))", RegexOptions.Singleline);

        //  wrap_love_dll_xxxxxxxxxx
        static Regex typeStructRegex = new Regex(@"^[a-zA-Z0-9](_)$", RegexOptions.Singleline);

        struct TypeStructDefine
        {
            public readonly bool isType;

            /// <summary>
            /// wrap_love_dll_type_Fixture_getPost -> Fixture
            /// wrap_love_dll_phyiscs_getPost -> phyiscs
            /// </summary>
            public readonly string topName;


            /// <summary> wrap_love_dll_type_Fixture_getPost_x -> getPost_x </summary>
            public readonly string methodName;

            public TypeStructDefine(string str)
            {
                if (str.StartsWith("type_"))
                {
                    str = str.Substring("type_".Length);
                    isType = true;
                }
                else
                {
                    isType = false;
                }

                var sp = str.Split("_");

                topName = sp[0];
                methodName = string.Join("_", sp.Skip(1));
            }
        }


        static void GenCppDefine()
        {
            var str = File.ReadAllText("./code");
            foreach(Match match in firstRegex.Matches(str))
            {
                Console.WriteLine("extern \"C\" LOVE_EXPORT " + match.Value + ";");
            }
        }

        static Dictionary<string, string> nameDict = new Dictionary<string, string>() {
            {"Body*", "pBody"},
            {"World*", "pWorld"},
            {"Contact*", "pContact"},
            {"Fixture*", "pFixture"},
            {"Joint*", "pJoint"},
            {"DistanceJoint*", "pDistanceJoint"},
            {"FrictionJoint*", "pFrictionJoint"},
            {"GearJoint*", "pGearJoint"},
            {"MotorJoint*", "pMotorJoint"},
            {"MouseJoint*", "pMouseJoint"},
            {"PrismaticJoint*", "pPrismaticJoint"},
            {"PulleyJoint*", "pPulleyJoint"},
            {"RevoluteJoint*", "pRevoluteJoint"},
            {"RopeJoint*", "pRopeJoint"},
            {"WeldJoint*", "pWeldJoint"},
            {"WheelJoint*", "pWheelJoint"},
            {"Shape*", "pShape"},
            {"ChainShape*", "pChainShape"},
            {"CircleShape*", "pCircleShape"},
            {"EdgeShape*", "pEdgeShape"},
            {"PolygonShape*", "pPolygonShape"},
        };



        static Dictionary<string, string> nameCppToCSTypeDict = new Dictionary<string, string>() {
            {"Body*", "Body"},
            {"World*", "World"},
            {"Contact*", "Contact"},
            {"Fixture*", "Fixture"},
            {"Joint*", "Joint"},
            {"DistanceJoint*", "DistanceJoint"},
            {"FrictionJoint*", "FrictionJoint"},
            {"GearJoint*", "GearJoint"},
            {"MotorJoint*", "MotorJoint"},
            {"MouseJoint*", "MouseJoint"},
            {"PrismaticJoint*", "PrismaticJoint"},
            {"PulleyJoint*", "PulleyJoint"},
            {"RevoluteJoint*", "RevoluteJoint"},
            {"RopeJoint*", "RopeJoint"},
            {"WeldJoint*", "WeldJoint"},
            {"WheelJoint*", "WheelJoint"},
            {"Shape*", "Shape"},
            {"ChainShape*", "ChainShape"},
            {"CircleShape*", "CircleShape"},
            {"EdgeShape*", "EdgeShape"},
            {"PolygonShape*", "PolygonShape"},
        };



        struct CSParams
        {
            public readonly bool OutString;
            public readonly string TypeString;

            public CSParams(bool hasOut, string t)
            {
                OutString = hasOut;
                TypeString = t;
            }

            /// <summary> out float speed </summary>
            public string ToDefString(Params p)
            {
                if (OutString)
                {
                    return "out " + TypeString + " " + p.ValueString.Substring("out_".Length);
                }

                if (p.ValueString.StartsWith("out"))
                    throw new Exception("nnnnnnnnnnnnnnn out!");

                return ToNoOutDefString(p);
            }


            /// <summary> out float speed </summary>
            public string ToNoOutDefString(Params p)
            {
                if (p.ValueString.Length < 2 && nameDict.ContainsKey(p.TypeString + p.RefString))
                    return TypeString + " " + nameDict[p.TypeString + p.RefString];

                return TypeString + " " + p.ValueString;
            }


            /// <summary> out speed </summary>
            public string ToInputString(Params p)
            {
                if (OutString)
                {
                    return "out " + p.ValueString.Substring("out_".Length);
                }

                if (p.ValueString.StartsWith("out"))
                    throw new Exception("nnnnnnnnnnnnnnn out!");

                if (p.ValueString.Length < 2 && nameDict.ContainsKey(p.TypeString + p.RefString))
                    return nameDict[p.TypeString + p.RefString];

                return p.ValueString;
            }

        }
        struct Params
        {
            /// <summary> float **out_x --> float </summary>
            public string TypeString;

            /// <summary> float **out_x --> ** </summary>
            public string RefString;

            /// <summary> float **out_x --> out_x </summary>
            public string ValueString;

            public Params(string t, string b, string c)
            {
                TypeString = t;
                RefString = b;
                ValueString = c;
            }

            public string GetOutStr()
            {
                return ValueString.StartsWith("out_") ? " out" : "";
            }

            public string ToDefString()
            {
                return paramDict[TypeString + RefString + GetOutStr()].ToDefString(this);
            }
            public string ToInputString()
            {
                return paramDict[TypeString + RefString + GetOutStr()].ToInputString(this);
            }
            public CSParams ToCSParams()
            {
                return paramDict[TypeString + RefString + GetOutStr()];
            }
        }

        static Dictionary<string, CSParams> paramDict = new Dictionary<string, CSParams>() {
            {"WrapString** out",  new CSParams(true, "IntPtr")},
            {"char*",  new CSParams(false, "byte[]")},

            {"void*",  new CSParams(false, "byte[]")},
            {"void** out",  new CSParams(true, "IntPtr")},

            {"Mesh*", new CSParams(false, "IntPtr")},
            {"Texture*", new CSParams(false, "IntPtr")},
            {"Quad*", new CSParams(false, "IntPtr")},
            {"Canvas**", new CSParams(false, "IntPtr[]")},
            {"Canvas*** out", new CSParams(true, "IntPtr")},

            {"Body*", new CSParams(false, "IntPtr")},
            {"Body**", new CSParams(true, "IntPtr")},

            {"WrapSequenceString** out", new CSParams(true, "IntPtr")},


            {"uint16", new CSParams(false, "UInt16")},
            {"uint16* out", new CSParams(true, "UInt16")},

            {"int", new CSParams(false, "int")},
            {"int*", new CSParams(false, "int[]")},
            {"int* out", new CSParams(true, "int")},
            {"int** out", new CSParams(true, "IntPtr")},

            {"bool4", new CSParams(false, "bool")},
            {"bool4*", new CSParams(false, "bool[]")},
            {"bool4* out", new CSParams(true, "bool")},
            {"bool4** out", new CSParams(true, "IntPtr")},

            {"float", new CSParams(false, "float")},
            {"float*", new CSParams(false, "float[]")},
            {"float* out", new CSParams(true, "float")},
            {"float** out", new CSParams(true, "IntPtr")},

            {"Float2", new CSParams(false, "Vector2")},
            {"Float2*", new CSParams(false, "Vector2[]")},
            {"Float2* out", new CSParams(true, "Vector2")},

            // out get array
            {"Float2** out", new CSParams(true, "IntPtr")},

            {"Float3*", new CSParams(false, "Vector3[]")},
            {"Float3* out", new CSParams(true, "Vector3")},


            {"World*", new CSParams(false, "IntPtr")},
            {"Contact*", new CSParams(false, "IntPtr")},
            {"Fixture*", new CSParams(false, "IntPtr")},

            {"Joint*", new CSParams(false, "IntPtr")},
            {"DistanceJoint*", new CSParams(false, "IntPtr")},
            {"FrictionJoint*", new CSParams(false, "IntPtr")},
            {"GearJoint*", new CSParams(false, "IntPtr")},
            {"MotorJoint*", new CSParams(false, "IntPtr")},
            {"MouseJoint*", new CSParams(false, "IntPtr")},
            {"PrismaticJoint*", new CSParams(false, "IntPtr")},
            {"PulleyJoint*", new CSParams(false, "IntPtr")},
            {"RevoluteJoint*", new CSParams(false, "IntPtr")},
            {"RopeJoint*", new CSParams(false, "IntPtr")},
            {"WeldJoint*", new CSParams(false, "IntPtr")},
            {"WheelJoint*", new CSParams(false, "IntPtr")},

            {"Shape*", new CSParams(false, "IntPtr")},
            {"ChainShape*", new CSParams(false, "IntPtr")},
            {"CircleShape*", new CSParams(false, "IntPtr")},
            {"EdgeShape*", new CSParams(false, "IntPtr")},
            {"PolygonShape*", new CSParams(false, "IntPtr")},

            // out normal
            {"Fixture** out", new CSParams(true, "IntPtr")},
            {"Body** out", new CSParams(true, "IntPtr")},
            {"World** out", new CSParams(true, "IntPtr")},

            {"Shape** out", new CSParams(true, "IntPtr")},
            {"ChainShape** out", new CSParams(true, "IntPtr")},
            {"CircleShape** out", new CSParams(true, "IntPtr")},
            {"EdgeShape** out", new CSParams(true, "IntPtr")},
            {"PolygonShape** out", new CSParams(true, "IntPtr")},

            {"Joint** out", new CSParams(true, "IntPtr")},
            {"DistanceJoint** out", new CSParams(true, "IntPtr")},
            {"FrictionJoint** out", new CSParams(true, "IntPtr")},
            {"GearJoint** out", new CSParams(true, "IntPtr")},
            {"MotorJoint** out", new CSParams(true, "IntPtr")},
            {"MouseJoint** out", new CSParams(true, "IntPtr")},
            {"PrismaticJoint** out", new CSParams(true, "IntPtr")},
            {"PulleyJoint** out", new CSParams(true, "IntPtr")},
            {"RevoluteJoint** out", new CSParams(true, "IntPtr")},
            {"RopeJoint** out", new CSParams(true, "IntPtr")},
            {"WeldJoint** out", new CSParams(true, "IntPtr")},
            {"WheelJoint** out", new CSParams(true, "IntPtr")},


            // out array
            {"Fixture*** out", new CSParams(true, "IntPtr")},
            {"Joint*** out", new CSParams(true, "IntPtr")},
            {"Body*** out", new CSParams(true, "IntPtr")},
            {"Contact*** out", new CSParams(true, "IntPtr")},

            // callback
            {"WrapShapeMassCallbackFunc", new CSParams(false, "WrapShapeComputeMassCallbackDelegate")},
            {"WrapShapeAABBCallbackFunc", new CSParams(false, "WrapShapeComputeAABBCallbackDelegate")},
            {"WrapShapeRayCastCallbackFunc", new CSParams(false, "WrapShapeRayCastCallbackDelegate")},
            {"WrapRayCastCallbackFunc", new CSParams(false, "WrapWorldRayCastCallbackDelegate")},
            {"WrapQueryBoundingBoxCallbackFunc", new CSParams(false, "WrapWorldQueryBoundingBoxCallbackDelegate")},
            {"WrapContactCallbackFunc", new CSParams(false, "WrapWorldContactCallbackDelegate")},
            {"WrapContactFilterFunc", new CSParams(false, "WrapWorldContactFilterCallbackDelegate")},
        };

        /// <param name="csRetType">bool or void</param>
        /// <param name="funcName">wrap_love_dll_xxxxxxxxxxxxx</param>
        /// <param name="paramsDef">out IntPtr p, Vector4[] colorarray, int colorarray_length</param>
        /// <param name="paramsInput">out p, colorarray, colorarray_length</param>
        static string CSDLLImportTemplate(string csRetType, string funcName, string paramsDef, string paramsInput)
        {
            if (csRetType != "bool" && csRetType != "void")
                throw new Exception("csRetType" + csRetType);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"[DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = \"{funcName}\")]");
            sb.AppendLine($"internal extern static {csRetType} _{funcName}({paramsDef});");
            sb.AppendLine($"internal static {csRetType} {funcName}({paramsDef})");
            sb.AppendLine("{");
            if (csRetType == "bool")
                sb.AppendLine($"    return CheckCAPIException(_{funcName}({paramsInput}));");
            else
                sb.AppendLine($"    _{funcName}({paramsInput});");

            sb.AppendLine("}");

            return sb.ToString();
        }


        static void GenCSDefine()
        {
            var str = File.ReadAllText("./code");
            foreach(Match match in firstRegex.Matches(str))
            {
                var retValue = match.Groups[1].Value;
                if (retValue != "bool4" && retValue != "void")
                    throw new Exception("retValue" + retValue);

                var funcName = "wrap_love_dll_" + match.Groups[2].Value;
                var paramsDefBox = paramsRegex.Matches("(" + match.Groups[3].Value)
                    .Select(m => new Params(m.Groups[3].Value, m.Groups[4].Value, m.Groups[5].Value))
                    .Select(p => paramDict[p.TypeString + p.RefString + p.GetOutStr()].ToDefString(p))
                    ;
                var paramsDef = string.Join(", ", paramsDefBox);
                var paramsInputBox = paramsRegex.Matches("(" + match.Groups[3].Value)
                    .Select(m => new Params(m.Groups[3].Value, m.Groups[4].Value, m.Groups[5].Value))
                    .Select(p => paramDict[p.TypeString + p.RefString + p.GetOutStr()].ToInputString(p))
                    ;
                var paramsInput = string.Join(", ", paramsInputBox);

                var line = CSDLLImportTemplate(retValue == "void" ? "void" : "bool", funcName, paramsDef, paramsInput);

                Console.WriteLine(line);
            }

        }

        static void CheckLastOutpu(IEnumerable<Params> prs)
        {
            List<Params> list = new List<Params>(prs.Reverse());
            bool lastIsOut = true;
            foreach (var p in list)
            {
                if (lastIsOut == false && p.ToCSParams().OutString)
                    throw new Exception("out is not last !");
                lastIsOut = p.ToCSParams().OutString;
            }
        }

        static void SplitByInputAndOutput(IEnumerable<Params> prs, out IEnumerable<Params> inPrs, out IEnumerable<Params> outPrs)
        {
            CheckLastOutpu(prs);
            List<Params> list = new List<Params>(prs.Reverse());
            bool lastIsOut = true;
            foreach (var p in list)
            {
                if (lastIsOut == false && p.ToCSParams().OutString)
                    throw new Exception("out is not last !");
                lastIsOut = p.ToCSParams().OutString;
            }

            inPrs = prs.Where(p => !p.ToCSParams().OutString);
            outPrs = prs.Where(p => p.ToCSParams().OutString);
        }

        /// <summary>getPos ---> GetPos</summary>
        static string FuncStrHeadToUpper(string str)
        {
            return str.Substring(0, 1).ToUpper() + str.Substring(1, str.Length - 1);
        }


        /// <param name="csRetType">bool or void</param>
        /// <param name="funcName">wrap_love_dll_xxxxxxxxxxxxx</param>
        /// <param name="paramsDef">out IntPtr p, Vector4[] colorarray, int colorarray_length</param>
        /// <param name="paramsInput">out p, colorarray, colorarray_length</param>
        static string CSDLLImplementTemplate(string funcName, TypeStructDefine tsd, IEnumerable<Params> prs)
        {
            SplitByInputAndOutput(prs, out var inPrs, out var outPrs);

            if (tsd.isType)
            {
                if (nameCppToCSTypeDict.ContainsKey(prs.First(p => true).TypeString)) {
                    throw new Exception("type first is not IntPtr !");
                }

                if(outPrs.Count() == 0) // no return value, type of them
                {
                    var ppp = prs.Skip(1);
                    var paramsDef = string.Join(", ", ppp.Select(p => p.ToDefString()));
                    var inputDef = string.Join(", ", ppp.Select(p => p.ToInputString()).Prepend("p") );

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine($"    public void {FuncStrHeadToUpper(tsd.methodName)}({paramsDef})");
                    sb.AppendLine("    {");
                    sb.AppendLine($"        Love2dDll.{funcName}({inputDef});");
                    sb.AppendLine("    }");
                    return sb.ToString();
                }
                else if(outPrs.Count() == 1) // no return value, type of them
                {
                    var ppp = inPrs.Skip(1);
                    var paramsDef = string.Join(", ", ppp.Select(p => p.ToDefString()));
                    var inputDef = string.Join(", ", ppp.Select(p => p.ToInputString()).Prepend("p") );
                    var outppp = outPrs.First(p => true);

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine($"    public {outppp.ToCSParams().TypeString} {FuncStrHeadToUpper(tsd.methodName)}({paramsDef})");
                    sb.AppendLine("    {");
                    sb.AppendLine($"        {outppp.ToCSParams().TypeString} {outppp.ValueString};");
                    sb.AppendLine($"        Love2dDll.{funcName}({inputDef}, out {outppp.ValueString});");
                    sb.AppendLine($"        return {outppp.ValueString};");
                    sb.AppendLine("    }");
                    return sb.ToString();
                }
            }
            else
            {
                if(outPrs.Count() == 0) // no return value, type of them
                {
                    var ppp = prs;
                    var paramsDef = string.Join(", ", ppp.Select(p => p.ToDefString()));
                    var inputDef = string.Join(", ", ppp.Select(p => p.ToInputString()) );

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine($"    public static void {FuncStrHeadToUpper(tsd.methodName)}({paramsDef})");
                    sb.AppendLine("    {");
                    sb.AppendLine($"        Love2dDll.{funcName}({inputDef});");
                    sb.AppendLine("    }");
                    return sb.ToString();
                }
                else if(outPrs.Count() == 1) // no return value, type of them
                {
                    var ppp = inPrs;
                    var paramsDef = string.Join(", ", ppp.Select(p => p.ToDefString()));
                    var inputDef = string.Join(", ", ppp.Select(p => p.ToInputString()));
                    if (!"".Equals(inputDef))
                        inputDef += ", ";

                    var outppp = outPrs.First(p => true);

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine($"    public static {outppp.ToCSParams().TypeString} {FuncStrHeadToUpper(tsd.methodName)}({paramsDef})");
                    sb.AppendLine("    {");
                    sb.AppendLine($"        {outppp.ToCSParams().TypeString} {outppp.ValueString};");
                    sb.AppendLine($"        Love2dDll.{funcName}({inputDef}out {outppp.ValueString});");
                    sb.AppendLine($"        return {outppp.ValueString};");
                    sb.AppendLine("    }");
                    return sb.ToString();
                }
            }

            return "    // TODO: finishe function " + funcName;
        }

        class ClassOfThem
        {
            public string className;
            public LinkedList<string> list = new LinkedList<string>();

            public ClassOfThem(string name)
            {
                this.className = name;
            }
        }

        static void GenCSImplement()
        {
            var str = File.ReadAllText("./code");
            Dictionary<string, LinkedList<string>> dict = new Dictionary<string, LinkedList<string>>();
            List<ClassOfThem> alllist = new List<ClassOfThem>();

            foreach(Match match in firstRegex.Matches(str))
            {
                var retValue = match.Groups[1].Value;
                if (retValue != "bool4" && retValue != "void")
                    throw new Exception("retValue" + retValue);
                var funcName = "wrap_love_dll_" + match.Groups[2].Value;
                var tsd = new TypeStructDefine(match.Groups[2].Value);

                var li = alllist.Find(item => item.className == tsd.topName);
                if (li == null)
                {
                    li = new ClassOfThem(tsd.topName);
                    alllist.Add(li);
                }

                var paramList = paramsRegex.Matches("(" + match.Groups[3].Value)
                    .Select(m => new Params(m.Groups[3].Value, m.Groups[4].Value, m.Groups[5].Value));
                var line = CSDLLImplementTemplate(funcName, tsd, paramList);
                li.list.AddLast(line);
            }

            List<string> sortIndex = new List<string>{
                "Body",
                "Contact",
                "Fixture",
                "Shape",
                "Joint",
                "World",
                "physics",
                "ChainShape",
                "CircleShape",
                "EdgeShape",
                "PolygonShape",
                "DistanceJoint",
                "FrictionJoint",
                "GearJoint",
                "MotorJoint",
                "MouseJoint",
                "PrismaticJoint",
                "PulleyJoint",
                "RevoluteJoint",
                "WeldJoint",
                "WheelJoint",
            };

            var lll = alllist
                .Select(a => a)
                .OrderBy(ca => sortIndex.IndexOf(ca.className));

            foreach (var c in lll)
            {
                var comeFrom = "LoveObject";
                if (c.className != "Shape" && c.className.EndsWith("Shape"))
                    comeFrom = "Shape";
                if (c.className != "Joint" && c.className.EndsWith("Joint"))
                    comeFrom = "Joint";

                Console.WriteLine($"public class {c.className}: {comeFrom}");
                Console.WriteLine("{");
                Console.WriteLine("    /// <summary>");
                Console.WriteLine("    /// disable construct");
                Console.WriteLine("    /// </summary>");
                Console.WriteLine($"    protected {c.className}() {{ }}");
                Console.WriteLine(string.Join("\n", c.list));
                Console.WriteLine("}");
            }
        }


        static void Main(string[] args)
        {
            GenCSImplement(); // C# implement code
            // GenCSDefine();
            // GenCppDefine();
        }
    }
}
