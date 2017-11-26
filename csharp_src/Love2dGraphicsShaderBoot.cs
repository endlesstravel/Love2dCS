// Author : endlesstravel
// this part is C# version of 'wrap_Graphics.lua'

using System;
using System.Collections.Generic;
using System.Text;

namespace Love2d
{
    class Love2dGraphicsShaderBoot
    {
        enum Stage
        {
            Vertex,
            Pixel,
        };
        class StageString
        {
            public string HEADER = "";
            public string FUNCTIONS = "";
            public string FOOTER = "";
            public string FOOTER_MULTI_CANVAS = "";
        }
        static Dictionary<string, string> GLSL = new Dictionary<string, string>();
        static Dictionary<Stage, StageString> GLSLStageStr = new Dictionary<Stage, StageString>();
        static void InitGLSLStrings()
        {
            GLSL["VERSION"] = "#version 120";
            GLSL["VERSION_ES"] = "#version 100";
            GLSL["SYNTAX"] = @"
#ifndef GL_ES
#define lowp
#define mediump
#define highp
#endif
#define number float
#define Image sampler2D
#define extern uniform
#define Texel texture2D
#pragma optionNV(strict on)";

            // Uniforms shared by the vertex and pixel shader stages.
            GLSL["UNIFORMS"] = @"
#ifdef GL_ES
// According to the GLSL ES 1.0 spec, uniform precision must match between stages,
// but we can't guarantee that highp is always supported in fragment shaders...
// We *really* don't want to use mediump for these in vertex shaders though.
#if defined(VERTEX) || defined(GL_FRAGMENT_PRECISION_HIGH)
#define LOVE_UNIFORM_PRECISION highp
#else
#define LOVE_UNIFORM_PRECISION mediump
#endif
uniform LOVE_UNIFORM_PRECISION mat4 TransformMatrix;
uniform LOVE_UNIFORM_PRECISION mat4 ProjectionMatrix;
uniform LOVE_UNIFORM_PRECISION mat4 TransformProjectionMatrix;
uniform LOVE_UNIFORM_PRECISION mat3 NormalMatrix;
#else
#define TransformMatrix gl_ModelViewMatrix
#define ProjectionMatrix gl_ProjectionMatrix
#define TransformProjectionMatrix gl_ModelViewProjectionMatrix
#define NormalMatrix gl_NormalMatrix
#endif
uniform mediump vec4 love_ScreenSize;";

            GLSL["FUNCTIONS"] = @"
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

// pow(x, 2.2) isn't an amazing approximation, but at least it's efficient...
mediump float gammaToLinearFast(mediump float c) { return pow(max(c, 0.0), 2.2); }
mediump vec3 gammaToLinearFast(mediump vec3 c) { return pow(max(c, vec3(0.0)), vec3(2.2)); }
mediump vec4 gammaToLinearFast(mediump vec4 c) { return vec4(gammaToLinearFast(c.rgb), c.a); }
mediump float linearToGammaFast(mediump float c) { return pow(max(c, 0.0), 1.0 / 2.2); }
mediump vec3 linearToGammaFast(mediump vec3 c) { return pow(max(c, vec3(0.0)), vec3(1.0 / 2.2)); }
mediump vec4 linearToGammaFast(mediump vec4 c) { return vec4(linearToGammaFast(c.rgb), c.a); }

#ifdef LOVE_PRECISE_GAMMA
#define gammaToLinear gammaToLinearPrecise
#define linearToGamma linearToGammaPrecise
#else
#define gammaToLinear gammaToLinearFast
#define linearToGamma linearToGammaFast
#endif

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
            StageString VERTEX_StageString = new StageString();
            VERTEX_StageString.HEADER = @"
#define VERTEX
#define LOVE_PRECISE_GAMMA

attribute vec4 VertexPosition;
attribute vec4 VertexTexCoord;
attribute vec4 VertexColor;
attribute vec4 ConstantColor;

varying vec4 VaryingTexCoord;
varying vec4 VaryingColor;

#ifdef GL_ES
uniform mediump float love_PointSize;
#endif";
            VERTEX_StageString.FOOTER = @"
void main() {
	VaryingTexCoord = VertexTexCoord;
	VaryingColor = gammaCorrectColor(VertexColor) * ConstantColor;
#ifdef GL_ES
	gl_PointSize = love_PointSize;
#endif
	gl_Position = position(TransformProjectionMatrix, VertexPosition);
}";

            StageString PIXEL_StageString = new StageString();
            PIXEL_StageString.HEADER = @"
#define PIXEL

#ifdef GL_ES
precision mediump float;
#endif

varying mediump vec4 VaryingTexCoord;
varying mediump vec4 VaryingColor;

#define love_Canvases gl_FragData

uniform sampler2D _tex0_;";
            PIXEL_StageString.FUNCTIONS = @"
uniform sampler2D love_VideoYChannel;
uniform sampler2D love_VideoCbChannel;
uniform sampler2D love_VideoCrChannel;

vec4 VideoTexel(vec2 texcoords)
{
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
}";
            PIXEL_StageString.FOOTER = @"
void main() {
	// fix crashing issue in OSX when _tex0_ is unused within effect()
	float dummy = Texel(_tex0_, vec2(.5)).r;

	// See Shader::checkSetScreenParams in Shader.cpp.
	vec2 pixelcoord = vec2(gl_FragCoord.x, (gl_FragCoord.y * love_ScreenSize.z) + love_ScreenSize.w);

	gl_FragColor = effect(VaryingColor, _tex0_, VaryingTexCoord.st, pixelcoord);
}";
            PIXEL_StageString.FOOTER_MULTI_CANVAS = @"
void main() {
	// fix crashing issue in OSX when _tex0_ is unused within effect()
	float dummy = Texel(_tex0_, vec2(.5)).r;

	// See Shader::checkSetScreenParams in Shader.cpp.
	vec2 pixelcoord = vec2(gl_FragCoord.x, (gl_FragCoord.y * love_ScreenSize.z) + love_ScreenSize.w);

	effects(VaryingColor, _tex0_, VaryingTexCoord.st, pixelcoord);
}";

            GLSLStageStr[Stage.Vertex] = VERTEX_StageString;
            GLSLStageStr[Stage.Pixel] = PIXEL_StageString;
        }

        static string defaultcode_vertex = @"
vec4 position(mat4 transform_proj, vec4 vertpos) {
	return transform_proj * vertpos;
}";
        static string defaultcode_pixel = @"
vec4 effect(mediump vec4 vcolor, Image tex, vec2 texcoord, vec2 pixcoord) {
	return Texel(tex, texcoord) * vcolor;
}";
        static string defaultcode_videopixel = @"
vec4 effect(mediump vec4 vcolor, Image tex, vec2 texcoord, vec2 pixcoord) {
	return VideoTexel(texcoord) * vcolor;
}";
        static string createShaderStageCode(Stage stage, string code, string lang, bool gammacorrect, bool multicanvas)
        {
            string[] lines = new string[10] {
                lang == "glsles" ? GLSL["VERSION_ES"] : GLSL["VERSION"],
                GLSL["SYNTAX"],
                gammacorrect ? "#define LOVE_GAMMA_CORRECT 1" : "",
                GLSLStageStr[stage].HEADER,
                GLSL["UNIFORMS"],
                GLSL["FUNCTIONS"],
                GLSLStageStr[stage].FUNCTIONS,
                lang == "glsles" ? "#line 1" : "#line 0",
                code,
                multicanvas ? GLSLStageStr[stage].FOOTER_MULTI_CANVAS : GLSLStageStr[stage].FOOTER,
            };

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < lines.Length; i++)
            {
                sb.Append(lines[i]);
                sb.Append("\n");
            }
            return sb.ToString();
        }

        static Love2dGraphicsShaderBoot()
        {
            InitGLSLStrings();

            var langs = new string[2] { "glsl", "glsles" };
            var gammacorrects = new bool[2] { false, true };
            string[] codeStr = new string[2 * 2 * 3];

            int index = 0;
            foreach (var lang in langs)
            {
                foreach (var gammacorrect in gammacorrects)
                {
                    codeStr[index++] = createShaderStageCode(Stage.Vertex, defaultcode_vertex, lang, gammacorrect, false);
                    codeStr[index++] = createShaderStageCode(Stage.Pixel, defaultcode_pixel, lang, gammacorrect, false);
                    codeStr[index++] = createShaderStageCode(Stage.Pixel, defaultcode_videopixel, lang, gammacorrect, false);
                }
            }

            Love2d.Love2dDll.wrap_love_dll_graphics_setDefaultShaderCode(
                DllTool.ToUTF8Bytes(codeStr[00]), DllTool.ToUTF8Bytes(codeStr[01]), DllTool.ToUTF8Bytes(codeStr[02]),
                DllTool.ToUTF8Bytes(codeStr[03]), DllTool.ToUTF8Bytes(codeStr[04]), DllTool.ToUTF8Bytes(codeStr[05]),
                DllTool.ToUTF8Bytes(codeStr[06]), DllTool.ToUTF8Bytes(codeStr[07]), DllTool.ToUTF8Bytes(codeStr[08]),
                DllTool.ToUTF8Bytes(codeStr[09]), DllTool.ToUTF8Bytes(codeStr[10]), DllTool.ToUTF8Bytes(codeStr[11])
                );
        }

        static bool isVertexCode(string code)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(code, @"vec4\s+position\s*\(");
        }
        static bool isPixelCode(string code, out bool isMultiCanvas)
        {
            isMultiCanvas = false;
            if (System.Text.RegularExpressions.Regex.IsMatch(code, @"vec4\s+effect\s*\("))
            {
                return true;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(code, @"void\s+effect\s*\("))
            {
                isMultiCanvas = true;
                return true;
            }

            return false;
        }

        public static void shaderCodeToGLSL(string arg1, string arg2, out string out_vertexcode, out string out_pixelcode)
        {
            string vertexcode = null;
            string pixelcode = null;
            bool is_multicanvas = false; // whether pixel code has "effects" function instead of "effect"

            if (arg1 != null)
            {
                if (isVertexCode(arg1))
                {
                    vertexcode = arg1; // first arg contains vertex shader code
                }

                bool isMultiCanvas;
                bool ispixel = isPixelCode(arg1, out isMultiCanvas);

                if (ispixel)
                {
                    pixelcode = arg1; // first arg contains pixel shader code
                    is_multicanvas = isMultiCanvas;

                }
            }

            if (arg2 != null)
            {
                if (isVertexCode(arg2))
                {
                    vertexcode = arg2;// second arg contains vertex shader code
                }

                bool isMultiCanvas;
                bool ispixel = isPixelCode(arg2, out isMultiCanvas);

                if (ispixel)
                {
                    pixelcode = arg2;// second arg contains pixel shader code
                    is_multicanvas = isMultiCanvas;

                }
            }

            string lang = "glsl";
            string info0, info1, info2, info3;
            Love2d.Graphics.GetRendererInfo(out info0, out info1, out info2, out info3);
            if (info0 == "OpenGL ES")
            {
                lang = "glsles";

            }

            bool gammacorrect = Love2d.Graphics.IsGammaCorrect();
            if (vertexcode != null)
            {
                vertexcode = createShaderStageCode(Stage.Vertex, vertexcode, lang, gammacorrect, false);
            }

            if (pixelcode != null)
            {
                pixelcode = createShaderStageCode(Stage.Pixel, pixelcode, lang, gammacorrect, is_multicanvas);
            }

            out_vertexcode = vertexcode;
            out_pixelcode = pixelcode;
        }

        public static void init()
        {
            Console.WriteLine("init shader code boot ...");
        }
    }
}
