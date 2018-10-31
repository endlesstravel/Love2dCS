// Author : endlesstravel
// this part is C# version of 'wrap_Graphics.lua'

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Love
{
    class Love2dGraphicsShaderBoot
    {
        static Dictionary<string, string> GLSL_VERSION = new Dictionary<string, string>();
        static string GLSL_SYNTAX, GLSL_UNIFORMS, GLSL_FUNCTIONS;
        static Dictionary<string, string> GLSL_MAP = new Dictionary<string, string>();

        static void InitGLSLStrings()
        {
            // GLSL_VERSION
            GLSL_VERSION.Add("glsl1,F", "#version 120");
            GLSL_VERSION.Add("glsl1,T", "#version 100");
            GLSL_VERSION.Add("glsl3,F", "#version 330 core");
            GLSL_VERSION.Add("glsl3,T", "#version 300 es");

            // GLSL_SYNTAX
            GLSL_SYNTAX = @"
                #if !defined(GL_ES) && __VERSION__ < 140
	                #define lowp
	                #define mediump
	                #define highp
                #endif
                #if defined(VERTEX) || __VERSION__ > 100 || defined(GL_FRAGMENT_PRECISION_HIGH)
	                #define LOVE_HIGHP_OR_MEDIUMP highp
                #else
	                #define LOVE_HIGHP_OR_MEDIUMP mediump
                #endif
                #define number float
                #define Image sampler2D
                #define ArrayImage sampler2DArray
                #define CubeImage samplerCube
                #define VolumeImage sampler3D
                #if __VERSION__ >= 300 && !defined(LOVE_GLSL1_ON_GLSL3)
	                #define DepthImage sampler2DShadow
	                #define DepthArrayImage sampler2DArrayShadow
	                #define DepthCubeImage samplerCubeShadow
                #endif
                #define extern uniform
                #ifdef GL_EXT_texture_array
                #extension GL_EXT_texture_array : enable
                #endif
                #ifdef GL_OES_texture_3D
                #extension GL_OES_texture_3D : enable
                #endif
                #ifdef GL_OES_standard_derivatives
                #extension GL_OES_standard_derivatives : enable
                #endif
            ";

            GLSL_UNIFORMS = @"
                // According to the GLSL ES 1.0 spec, uniform precision must match between stages,
                // but we can't guarantee that highp is always supported in fragment shaders...
                // We *really* don't want to use mediump for these in vertex shaders though.
                uniform LOVE_HIGHP_OR_MEDIUMP mat4 ViewSpaceFromLocal;
                uniform LOVE_HIGHP_OR_MEDIUMP mat4 ClipSpaceFromView;
                uniform LOVE_HIGHP_OR_MEDIUMP mat4 ClipSpaceFromLocal;
                uniform LOVE_HIGHP_OR_MEDIUMP mat3 ViewNormalFromLocal;
                uniform LOVE_HIGHP_OR_MEDIUMP vec4 love_ScreenSize;

                // Compatibility
                #define TransformMatrix ViewSpaceFromLocal
                #define ProjectionMatrix ClipSpaceFromView
                #define TransformProjectionMatrix ClipSpaceFromLocal
                #define NormalMatrix ViewNormalFromLocal
            ";

            GLSL_FUNCTIONS = @"
                #ifdef GL_ES
	                #if __VERSION__ >= 300 || defined(GL_EXT_texture_array)
		                precision lowp sampler2DArray;
	                #endif
	                #if __VERSION__ >= 300 || defined(GL_OES_texture_3D)
		                precision lowp sampler3D;
	                #endif
	                #if __VERSION__ >= 300
		                precision lowp sampler2DShadow;
		                precision lowp samplerCubeShadow;
		                precision lowp sampler2DArrayShadow;
	                #endif
                #endif

                #if __VERSION__ >= 130 && !defined(LOVE_GLSL1_ON_GLSL3)
	                #define Texel texture
                #else
	                #if __VERSION__ >= 130
		                #define texture2D Texel
		                #define texture3D Texel
		                #define textureCube Texel
		                #define texture2DArray Texel
		                #define love_texture2D texture
		                #define love_texture3D texture
		                #define love_textureCube texture
		                #define love_texture2DArray texture
	                #else
		                #define love_texture2D texture2D
		                #define love_texture3D texture3D
		                #define love_textureCube textureCube
		                #define love_texture2DArray texture2DArray
	                #endif
	                vec4 Texel(sampler2D s, vec2 c) { return love_texture2D(s, c); }
	                vec4 Texel(samplerCube s, vec3 c) { return love_textureCube(s, c); }
	                #if __VERSION__ > 100 || defined(GL_OES_texture_3D)
		                vec4 Texel(sampler3D s, vec3 c) { return love_texture3D(s, c); }
	                #endif
	                #if __VERSION__ >= 130 || defined(GL_EXT_texture_array)
		                vec4 Texel(sampler2DArray s, vec3 c) { return love_texture2DArray(s, c); }
	                #endif
	                #ifdef PIXEL
		                vec4 Texel(sampler2D s, vec2 c, float b) { return love_texture2D(s, c, b); }
		                vec4 Texel(samplerCube s, vec3 c, float b) { return love_textureCube(s, c, b); }
		                #if __VERSION__ > 100 || defined(GL_OES_texture_3D)
			                vec4 Texel(sampler3D s, vec3 c, float b) { return love_texture3D(s, c, b); }
		                #endif
		                #if __VERSION__ >= 130 || defined(GL_EXT_texture_array)
			                vec4 Texel(sampler2DArray s, vec3 c, float b) { return love_texture2DArray(s, c, b); }
		                #endif
	                #endif
	                #define texture love_texture
                #endif

                float gammaToLinearPrecise(float c) {
	                return c <= 0.04045 ? c * 0.077399380804954 : pow((c + 0.055) * 0.9478672985782, 2.4);
                }
                vec3 gammaToLinearPrecise(vec3 c) {
	                bvec3 leq = lessThanEqual(c, vec3(0.04045));
	                c.r = leq.r ? c.r * 0.077399380804954 : pow((c.r + 0.055) * 0.9478672985782, 2.4);
	                c.g = leq.g ? c.g * 0.077399380804954 : pow((c.g + 0.055) * 0.9478672985782, 2.4);
	                c.b = leq.b ? c.b * 0.077399380804954 : pow((c.b + 0.055) * 0.9478672985782, 2.4);
	                return c;
                }
                vec4 gammaToLinearPrecise(vec4 c) { return vec4(gammaToLinearPrecise(c.rgb), c.a); }
                float linearToGammaPrecise(float c) {
	                return c < 0.0031308 ? c * 12.92 : 1.055 * pow(c, 1.0 / 2.4) - 0.055;
                }
                vec3 linearToGammaPrecise(vec3 c) {
	                bvec3 lt = lessThanEqual(c, vec3(0.0031308));
	                c.r = lt.r ? c.r * 12.92 : 1.055 * pow(c.r, 1.0 / 2.4) - 0.055;
	                c.g = lt.g ? c.g * 12.92 : 1.055 * pow(c.g, 1.0 / 2.4) - 0.055;
	                c.b = lt.b ? c.b * 12.92 : 1.055 * pow(c.b, 1.0 / 2.4) - 0.055;
	                return c;
                }
                vec4 linearToGammaPrecise(vec4 c) { return vec4(linearToGammaPrecise(c.rgb), c.a); }

                // http://chilliant.blogspot.com.au/2012/08/srgb-approximations-for-hlsl.html?m=1

                mediump float gammaToLinearFast(mediump float c) { return c * (c * (c * 0.305306011 + 0.682171111) + 0.012522878); }
                mediump vec3 gammaToLinearFast(mediump vec3 c) { return c * (c * (c * 0.305306011 + 0.682171111) + 0.012522878); }
                mediump vec4 gammaToLinearFast(mediump vec4 c) { return vec4(gammaToLinearFast(c.rgb), c.a); }

                mediump float linearToGammaFast(mediump float c) { return max(1.055 * pow(max(c, 0.0), 0.41666666) - 0.055, 0.0); }
                mediump vec3 linearToGammaFast(mediump vec3 c) { return max(1.055 * pow(max(c, vec3(0.0)), vec3(0.41666666)) - 0.055, vec3(0.0)); }
                mediump vec4 linearToGammaFast(mediump vec4 c) { return vec4(linearToGammaFast(c.rgb), c.a); }

                #define gammaToLinear gammaToLinearFast
                #define linearToGamma linearToGammaFast

                #ifdef LOVE_GAMMA_CORRECT
	                #define gammaCorrectColor gammaToLinear
	                #define unGammaCorrectColor linearToGamma
	                #define gammaCorrectColorPrecise gammaToLinearPrecise
	                #define unGammaCorrectColorPrecise linearToGammaPrecise
	                #define gammaCorrectColorFast gammaToLinearFast
	                #define unGammaCorrectColorFast linearToGammaFast
                #else
	                #define gammaCorrectColor
	                #define unGammaCorrectColor
	                #define gammaCorrectColorPrecise
	                #define unGammaCorrectColorPrecise
	                #define gammaCorrectColorFast
	                #define unGammaCorrectColorFast
                #endif
            ";


            GLSL_MAP["VERTEX,HEADER"] = @"
                #define love_Position gl_Position

                #if __VERSION__ >= 130
	                #define attribute in
	                #define varying out
	                #ifndef LOVE_GLSL1_ON_GLSL3
		                #define love_VertexID gl_VertexID
		                #define love_InstanceID gl_InstanceID
	                #endif
                #endif

                #ifdef GL_ES
	                uniform mediump float love_PointSize;
                #endif
            ";

            GLSL_MAP["VERTEX,FUNCTIONS"] = @"
                void setPointSize() {
                #ifdef GL_ES
	                gl_PointSize = love_PointSize;
                #endif
                }
            ";

            GLSL_MAP["VERTEX,MAIN"] = @"
                attribute vec4 VertexPosition;
                attribute vec4 VertexTexCoord;
                attribute vec4 VertexColor;
                attribute vec4 ConstantColor;

                varying vec4 VaryingTexCoord;
                varying vec4 VaryingColor;

                vec4 position(mat4 clipSpaceFromLocal, vec4 localPosition);

                void main() {
	                VaryingTexCoord = VertexTexCoord;
	                VaryingColor = gammaCorrectColor(VertexColor) * ConstantColor;
	                setPointSize();
	                love_Position = position(ClipSpaceFromLocal, VertexPosition);
                }
            ";

            GLSL_MAP["PIXEL,HEADER"] = @"
                #ifdef GL_ES
	                precision mediump float;
                #endif

                #define love_MaxCanvases gl_MaxDrawBuffers

                #if __VERSION__ >= 130
	                #define varying in
	                layout(location = 0) out vec4 love_Canvases[love_MaxCanvases];
	                #define love_PixelColor love_Canvases[0]
                #else
	                #define love_Canvases gl_FragData
	                #define love_PixelColor gl_FragColor
                #endif

                // See Shader::updateScreenParams in Shader.cpp.
                #define love_PixelCoord (vec2(gl_FragCoord.x, (gl_FragCoord.y * love_ScreenSize.z) + love_ScreenSize.w))
            ";

            GLSL_MAP["PIXEL,FUNCTIONS"] = @"
                uniform sampler2D love_VideoYChannel;
                uniform sampler2D love_VideoCbChannel;
                uniform sampler2D love_VideoCrChannel;

                vec4 VideoTexel(vec2 texcoords) {
	                vec3 yuv;
	                yuv[0] = Texel(love_VideoYChannel, texcoords).r;
	                yuv[1] = Texel(love_VideoCbChannel, texcoords).r;
	                yuv[2] = Texel(love_VideoCrChannel, texcoords).r;
	                yuv += vec3(-0.0627451017, -0.501960814, -0.501960814);

	                vec4 color;
	                color.r = dot(yuv, vec3(1.164,  0.000,  1.596));
	                color.g = dot(yuv, vec3(1.164, -0.391, -0.813));
	                color.b = dot(yuv, vec3(1.164,  2.018,  0.000));
	                color.a = 1.0;

	                return gammaCorrectColor(color);
                }
            ";

            GLSL_MAP["PIXEL,MAIN"] = @"
                uniform sampler2D MainTex;
                varying LOVE_HIGHP_OR_MEDIUMP vec4 VaryingTexCoord;
                varying mediump vec4 VaryingColor;

                vec4 effect(vec4 vcolor, Image tex, vec2 texcoord, vec2 pixcoord);

                void main() {
	                love_PixelColor = effect(VaryingColor, MainTex, VaryingTexCoord.st, love_PixelCoord);
                }
            ";

            GLSL_MAP["PIXEL,MAIN_CUSTOM"] = @"
                varying LOVE_HIGHP_OR_MEDIUMP vec4 VaryingTexCoord;
                varying mediump vec4 VaryingColor;

                void effect();

                void main() {
	                effect();
                }
            ";
        }

        static string defaultcode_vertex = @"
            vec4 position(mat4 clipSpaceFromLocal, vec4 localPosition) {
	            return clipSpaceFromLocal * localPosition;
            }
        ";
        static string defaultcode_pixel = @"
            vec4 effect(vec4 vcolor, Image tex, vec2 texcoord, vec2 pixcoord) {
	            return Texel(tex, texcoord) * vcolor;
            }
        ";
        static string defaultcode_videopixel = @"
            void effect() {
	            love_PixelColor = VideoTexel(VaryingTexCoord.xy) * VaryingColor;
            }
        ";
        static string defaultcode_arraypixel = @"
            uniform ArrayImage MainTex;
            void effect() {
	            love_PixelColor = Texel(MainTex, VaryingTexCoord.xyz) * VaryingColor;
            }
        ";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stage">"VERTEX" or "PIXEL"</param>
        /// <param name="code">input string</param>
        /// <param name="lang">glsl1 or glsl3</param>
        /// <param name="gles"></param>
        /// <param name="glsl1on3"></param>
        /// <param name="gammacorrect"></param>
        /// <param name="custom"></param>
        /// <returns></returns>
        static string createShaderStageCode(string stage, string code, string lang, bool gles, bool glsl1on3, bool gammacorrect, bool custom)
        {
            string custom_part = "";
            if (GLSL_MAP.ContainsKey(stage + ",MAIN_CUSTOM"))
            {
                custom_part = custom ? GLSL_MAP[stage + ",MAIN_CUSTOM"] : GLSL_MAP[stage + ",MAIN"];
            }
            else
            {
                custom_part = GLSL_MAP[stage + ",MAIN"];
            }

            string[] parts = new string[]
            {
                GLSL_VERSION[lang + "," + (gles ? "T" : "F")],
                "#define "  + stage + " " + stage,
                glsl1on3 ? "#define LOVE_GLSL1_ON_GLSL3 1" : "",
                gammacorrect ? "#define LOVE_GAMMA_CORRECT 1" :  "",
                GLSL_SYNTAX,
                GLSL_MAP[stage + ",HEADER"],
                GLSL_UNIFORMS,
                GLSL_FUNCTIONS,
                GLSL_MAP[stage + ",FUNCTIONS"],
                custom_part,
                ((lang == "glsl1" || glsl1on3) && !gles) ? "#line 0" : "#line 1" ,
                code,
            };

            return string.Join("\n", parts);
        }

        static void InitGraphicsShader()
        {
            InitGLSLStrings();

            var langs_index = new string[] { "glsl1", "essl1", "glsl3", "essl3", };
            var langs_dictionnary = new Dictionary<string, Tuple<string, bool>>();
            langs_dictionnary.Add("glsl1", Tuple.Create("glsl1", false));
            langs_dictionnary.Add("essl1", Tuple.Create("glsl1", true));
            langs_dictionnary.Add("glsl3", Tuple.Create("glsl3", false));
            langs_dictionnary.Add("essl3", Tuple.Create("glsl3", true));

            var gammacorrects = new bool[] { false, true };
            string[] codeStr = new string[2 * 4 * 4];

            int index = 0;
            foreach (var gammacorrect in gammacorrects)
            {
                foreach (var langIndex in langs_index)
                {
                    var lang = langs_dictionnary[langIndex];
                    string target = lang.Item1;
                    bool gles = lang.Item2;

                    // vertex
                    codeStr[index++] = createShaderStageCode("VERTEX", defaultcode_vertex, target, gles, false, gammacorrect, false);
                    // pixel
                    codeStr[index++] = createShaderStageCode("PIXEL", defaultcode_pixel, target, gles, false, gammacorrect, false);
                    // videopixel
                    codeStr[index++] = createShaderStageCode("PIXEL", defaultcode_videopixel, target, gles, false, gammacorrect, true);
                    // arraypixel
                    codeStr[index++] = createShaderStageCode("PIXEL", defaultcode_arraypixel, target, gles, false, gammacorrect, true);
                }
            }

            DllTool.ExecuteNullTailStringArray(codeStr, (tmp) =>
            {
                Love2dDll.wrap_love_dll_graphics_setDefaultShaderCode(tmp);
            });
            
        }

        static bool isVertexCode(string code)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(code, @"vec4\s+position\s*\(");
        }
        static Tuple<bool, bool> isPixelCode(string code)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(code, @"vec4\s+effect\s*\("))
            {
                return Tuple.Create(true, false);
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(code, @"void\s+effect\s*\("))
            {
                return Tuple.Create(true, true);
            }

            return Tuple.Create(false, false);
        }

        static Regex getLanguageTarget_regex = new Regex(@"\s*#pragma language (\w+)");
        static string getLanguageTarget(string code)
        {
            if (code == null)
            {
                return null;
            }

            Match match = getLanguageTarget_regex.Match(code);
            if ( match.Success )
            {
                return match.Groups[1].Value;
            }

            return "glsl1";
        }

        public static void shaderCodeToGLSL(bool gles, string arg1, string arg2, out string out_vertexcode, out string out_pixelcode)
        {
            string vertexcode = null, pixelcode = null;
            bool is_custompixel = false; // whether pixel code has "effects" function instead of "effect"

            if (arg1 != null)
            {
                if (isVertexCode(arg1))
                {
                    vertexcode = arg1; // first arg contains vertex shader code
                }

                var res = isPixelCode(arg1);
                bool ispixel = res.Item1;
                bool isCustomPixel = res.Item2;

                if (ispixel)
                {
                    pixelcode = arg1; // first arg contains pixel shader code
                    is_custompixel = isCustomPixel;
                }
            }

            if (arg2 != null)
            {
                if (isVertexCode(arg2))
                {
                    vertexcode = arg2;// second arg contains vertex shader code
                }

                var res = isPixelCode(arg2);
                bool ispixel = res.Item1;
                bool isCustomPixel = res.Item2;

                if (ispixel)
                {
                    pixelcode = arg2;// second arg contains pixel shader code
                    is_custompixel = isCustomPixel;
                }
            }

            var supportsGLSL3 = Graphics.GetSupported(Feature.GLSL3);
            var gammacorrect = Graphics.IsGammaCorrect();

            var targetlang = getLanguageTarget(pixelcode != null ? pixelcode : vertexcode);
            if (getLanguageTarget(vertexcode != null ? vertexcode : pixelcode) != targetlang)
            {
                throw new Exception("vertex and pixel shader languages must match");
            }

            if (targetlang == "glsl3" && !supportsGLSL3)
            {
                throw new Exception("GLSL 3 shaders are not supported on this system!");
            }

            if (targetlang != null && !(targetlang != "glsl1" || targetlang != "glsl3"))
            {
                throw new Exception("Invalid shader language: " + targetlang);
            }

            var lang = targetlang != null? targetlang : "glsl1";
            var glsl1on3 = false;
            if (lang == "glsl1" && supportsGLSL3)
            {
                lang = "glsl3";
                glsl1on3 = true;
            }

            if (vertexcode != null)
            {
                vertexcode = createShaderStageCode("VERTEX", vertexcode, lang, gles, glsl1on3, gammacorrect, false);
            }

            if (pixelcode != null)
            {
                pixelcode = createShaderStageCode("PIXEL", pixelcode, lang, gles, glsl1on3, gammacorrect, is_custompixel);
            }

            out_vertexcode = vertexcode;
            out_pixelcode = pixelcode;
        }

        public static void Init()
        {
            Console.WriteLine("init shader code boot ...");
            InitGraphicsShader();
            Console.WriteLine("init shader code success");
        }
    }
}
