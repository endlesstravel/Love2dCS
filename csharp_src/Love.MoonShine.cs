using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Love
{
    /// <summary>
    /// Postprocessing effect repository for LÖVE.
    /// <para> adapt from https://github.com/vrld/moonshine .</para>
    /// <para> more resource https://www.love2d.org/forums/viewtopic.php?t=3733 .</para>
    /// </summary>
    public class MoonShine
    {
        public abstract class Effect
        {
            /// <summary>
            /// this effect is enabled
            /// </summary>
            public virtual bool Enable { get; set; } = true;

            public abstract void Draw(DoubleBufferCanvas buffer);
        }

        public class DoubleBufferCanvas
        {
            /// <summary>
            /// The destination it should render into.
            /// </summary>
            public Canvas Front;

            /// <summary>
            /// The previous result
            /// </summary>
            public Canvas Back;

            /// <summary>
            /// Swap Front and Back. (Back, Front = Front, Back)
            /// </summary>
            public void Swap()
            {
                var tmp = Front;
                Front = Back;
                Back = tmp;
            }

            /// <summary>
            /// Swap Front and Back with shader. (Back, Front = Front, Back)
            /// </summary>
            public void Swap(Shader shader)
            {
                DrawWithShader(Front, Back, shader);
                Swap();
            }
        }

        /// <summary>
        /// this function uses shader to draw the Source buffer to the Dest buffer with given Shader.
        /// </summary>
        /// <param name="dest">Canvas that ready to draw as target</param>
        /// <param name="source">Canvas to draw</param>
        /// <param name="shader">Shader used when draw source canvas to dest canvas</param>
        public static void DrawWithShader(Canvas source, Canvas destination, Shader shader)
        {
            Graphics.SetCanvas(destination);
            Graphics.Clear();
            if (shader != Graphics.GetShader())
            {
                Graphics.SetShader(shader);
            }
            Graphics.Draw(source);
        }


        /// <summary>
        /// Create MoonShine
        /// </summary>
        /// <param name="w"></param>
        /// <param name="h"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public static MoonShine Create(Effect e)
        {
            return new MoonShine().Chain(e);
        }

        /// <summary>
        /// Create MoonShine With width and height
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static MoonShine Create(int w, int h, Effect e)
        {
            return new MoonShine().Chain(w, h, e);
        }

        private MoonShine()
        {

        }

        readonly List<Effect> effects = new List<Effect>();
        readonly DoubleBufferCanvas buffer = new DoubleBufferCanvas();

        public DoubleBufferCanvas GetBuffer()
        {
            buffer.Swap();
            return buffer;
        }

        public MoonShine Resize(int w, int h)
        {
            buffer.Front = Graphics.NewCanvas(w, h);
            buffer.Back = Graphics.NewCanvas(w, h);
            return this;
        }

        public MoonShine Chain(int w, int h, Effect e)
        {
            if (e == null)
                throw new ArgumentNullException("e");

            Resize(w, h);
            effects.Add(e);
            return this;
        }

        public MoonShine Chain(Effect e)
        {
            if (e == null)
                throw new ArgumentNullException("e");

            Window.GetMode(out var w, out var h);
            Resize(w, h);
            effects.Add(e);
            return this;
        }

        public void Draw(Action drawFunc)
        {
            //  save state
            var canvas = Graphics.GetCanvas();
            var shader = Graphics.GetShader();
            var fg = Graphics.GetColor();

            //  draw scene to front buffer
            Graphics.SetCanvas(GetBuffer().Front);
            Graphics.Clear();

            drawFunc();

            // save more state
            Graphics.GetBlendMode(out var blendMode, out var blendAlphaMode);

            // process all shaders
            Graphics.SetColor(fg);
            Graphics.SetBlendMode(BlendMode.Alpha, BlendAlphaMode.PreMultiplied);
            effects.ForEach(item =>
            {
                if (item.Enable)
                {
                    item.Draw(buffer);
                }
            });

            // present result
            Graphics.SetShader();
            Graphics.SetCanvas(canvas);
            Graphics.Draw(buffer.Front);

            // restore state
            Graphics.SetBlendMode(blendMode, blendAlphaMode);
            Graphics.SetShader(shader);
        }

        /// <summary>
        ///  simple blurring
        /// </summary>
        public class BoxBlur : Effect
        {
            static readonly Shader m_shader = Graphics.NewShader(@"
                extern vec2 direction;
                extern number radius;
                vec4 effect(vec4 color, Image texture, vec2 tc, vec2 _) {
                    vec4 c = vec4(0.0f, 0.2f, 0.3f, 1);
                    for (float i = -radius; i <= radius; i += 1.0f)
                    {
                        c += Texel(texture, tc + i * direction);
                    }
                    return c / (2.0f * radius + 1.0f) * color;
                }
            ");

            /// <summary>
            /// default BoxBlur
            /// </summary>
            public static BoxBlur Default = new BoxBlur();
            public Vector2 Radius { get; set; }

            /// <summary>
            /// default BoxBlur constructor
            /// </summary>
            public BoxBlur() : this(3, 3)
            {
            }

            public BoxBlur(float radiusX, float radiusY)
            {
                SetRadius(radiusX, radiusY);
            }

            public void SetRadius(float radiusX, float radiusY)
            {
                Radius = new Vector2(radiusX, radiusY);
            }

            public override void Draw(DoubleBufferCanvas buffer)
            {
                m_shader.Send("direction", 1f / Graphics.GetWidth(), 0);
                m_shader.Send("radius", Mathf.Floor(Radius.x + .5f));
                buffer.Swap(m_shader);

                m_shader.Send("direction", 0, 1f / Graphics.GetHeight());
                m_shader.Send("radius", Mathf.Floor(Radius.y + .5f));
                buffer.Swap(m_shader);
            }

            public static BoxBlur Create()
            {
                return new BoxBlur();
            }

            public static BoxBlur Create(float radiusX, float radiusY)
            {
                return new BoxBlur(radiusX, radiusY);
            }
        }

        /// <summary>
        /// cheap/fake chromatic aberration
        /// </summary>
        public class Chromasep : Effect
        {
            
            static readonly Shader m_shader = Graphics.NewShader(@"
                extern vec2 direction;
                vec4 effect(vec4 color, Image texture, vec2 tc, vec2 _)
                {
                  return color * vec4(
                    Texel(texture, tc - direction).r,
                    Texel(texture, tc).g,
                    Texel(texture, tc + direction).b,
                    1.0);
                }
            ");


            /// <summary>
            /// Default Effect
            /// </summary>
            public static Chromasep Default = new Chromasep();

            public float Angle { set; get; }
            public float Radius { set; get; }

            public override void Draw(DoubleBufferCanvas buffer)
            {
                var dx = Mathf.Cos(Angle) * Radius / Graphics.GetWidth();
                var dy = Mathf.Sin(Angle) * Radius / Graphics.GetHeight();
                m_shader.Send("direction", dx, dy);
                buffer.Swap(m_shader);
            }
        }

        /// <summary>
        /// weighting of color channels
        /// </summary>
        public class ColorGradeSimple: Effect
        {
            
            static readonly Shader m_shader = Graphics.NewShader(@"
                extern vec3 factors;
                vec4 effect(vec4 color, Image texture, vec2 tc, vec2 _) {
                  return vec4(factors, 1.0f) * Texel(texture, tc) * color;
                }
            ");

            /// <summary>
            /// Default Effect
            /// </summary>
            public static ColorGradeSimple Default = new ColorGradeSimple();

            public Vector3 Factors = Vector3.One;

            public override void Draw(DoubleBufferCanvas buffer)
            {
                m_shader.Send("factors", Factors);
                buffer.Swap(m_shader);
            }
        }

        /// <summary>
        /// crt/barrel distortion
        /// </summary>
        public class CRT: Effect
        {
            
            static readonly Shader m_shader = Graphics.NewShader(@"
                extern vec2 distortionFactor;
                extern vec2 scaleFactor;
                extern number feather;

                vec4 effect(vec4 color, Image tex, vec2 uv, vec2 px) {
                  // to barrel coordinates
                  uv = uv * 2.0 - vec2(1.0);

                  // distort
                  uv *= scaleFactor;
                  uv += (uv.yx*uv.yx) * uv * (distortionFactor - 1.0);
                  number mask = (1.0 - smoothstep(1.0-feather,1.0,abs(uv.x)))
                              * (1.0 - smoothstep(1.0-feather,1.0,abs(uv.y)));

                  // to cartesian coordinates
                  uv = (uv + vec2(1.0)) / 2.0;

                  return color * Texel(tex, uv) * mask;
                }
            ");

            /// <summary>
            /// Default Effect
            /// </summary>
            public static CRT Default = new CRT();

            public Vector2 DistortionFactor = new Vector2(1.06f, 1.065f);
            public float Feather = 0.02f;
            public Vector2 ScaleFactor = new Vector2(1, 1);

            public override void Draw(DoubleBufferCanvas buffer)
            {
                m_shader.Send("distortionFactor", DistortionFactor.x, DistortionFactor.y);
                m_shader.Send("feather", Feather);
                m_shader.Send("scaleFactor", ScaleFactor);
                buffer.Swap(m_shader);
            }
        }

        /// <summary>
        /// desaturation and tinting
        /// </summary>
        public class Desaturate : Effect
        {
            
            static readonly Shader m_shader = Graphics.NewShader(@"
                extern vec4 tint;
                extern number strength;
                vec4 effect(vec4 color, Image texture, vec2 tc, vec2 _) {
                  color = Texel(texture, tc);
                  number luma = dot(vec3(0.299f, 0.587f, 0.114f), color.rgb);
                  return mix(color, tint * luma, strength);
                }
            ");

            /// <summary>
            /// Default Effect
            /// </summary>
            public static Desaturate Default = new Desaturate();

            public Vector3 Tint = Vector3.Zero;
            public float Strength = 0.5f;

            public override void Draw(DoubleBufferCanvas buffer)
            {
                m_shader.Send("tint", Tint.x, Tint.y, Tint.z, 1);
                m_shader.Send("strength", Strength);
                buffer.Swap(m_shader);
            }
        }

        /// <summary>
        /// Gameboy and other four color palettes
        /// </summary>
        public class DMG : Effect
        {
            
            static readonly Shader m_shader = Graphics.NewShader(@"
                extern vec3 palette[ 4 ];
                vec4 effect(vec4 color, Image texture, vec2 texture_coords, vec2 pixel_coords) {
                  vec4 pixel = Texel(texture, texture_coords);
                  float avg = min(0.9999,max(0.0001,(pixel.r + pixel.g + pixel.b)/3));
                  int index = int(avg*4);
                  return vec4(palette[index], pixel.a);
                }
            ");

            public struct GameBoyPalette
            {
                public Vector3 ColorA;
                public Vector3 ColorB;
                public Vector3 ColorC;
                public Vector3 ColorD;

                /// <summary>
                /// Default color palette. Source: http://en.wikipedia.org/wiki/List_of_video_game_console_palettes#Original_Game_Boy
                /// </summary>
                public static GameBoyPalette Default = new GameBoyPalette
                {
                    ColorA = new Vector3(0.05882f, 0.21961f, 0.05882f),
                    ColorB = new Vector3(0.18824f, 0.38431f, 0.18824f),
                    ColorC = new Vector3(0.54510f, 0.67451f, 0.05882f),
                    ColorD = new Vector3(0.60784f, 0.73725f, 0.05882f),
                };


                /// <summary>
                /// Hardcore color profiles. Source: http://www.hardcoregaming101.net/gbdebate/gbcolours.htm
                /// </summary>
                public static GameBoyPalette DarkYellow = new GameBoyPalette
                {
                    ColorA = new Vector3(0.12941f, 0.12549f, 0.06275f),
                    ColorB = new Vector3(0.41961f, 0.41176f, 0.19216f),
                    ColorC = new Vector3(0.70980f, 0.68235f, 0.29020f),
                    ColorD = new Vector3(1.00000f, 0.96863f, 0.48235f),
                };


                public static GameBoyPalette LightYellow = new GameBoyPalette
                {
                    ColorA = new Vector3(0.40000f, 0.40000f, 0.14510f),
                    ColorB = new Vector3(0.58039f, 0.58039f, 0.25098f),
                    ColorC = new Vector3(0.81569f, 0.81569f, 0.40000f),
                    ColorD = new Vector3(1.00000f, 1.00000f, 0.58039f),
                };


                public static GameBoyPalette Green = new GameBoyPalette
                {
                    ColorA = new Vector3(0.03137f, 0.21961f, 0.03137f),
                    ColorB = new Vector3(0.18824f, 0.37647f, 0.18824f),
                    ColorC = new Vector3(0.53333f, 0.65882f, 0.03137f),
                    ColorD = new Vector3(0.71765f, 0.86275f, 0.06667f),
                };


                public static GameBoyPalette Greyscale = new GameBoyPalette
                {
                    ColorA = new Vector3(0.21961f, 0.21961f, 0.21961f),
                    ColorB = new Vector3(0.45882f, 0.45882f, 0.45882f),
                    ColorC = new Vector3(0.69804f, 0.69804f, 0.69804f),
                    ColorD = new Vector3(0.93725f, 0.93725f, 0.93725f),
                };


                public static GameBoyPalette StarkBW = new GameBoyPalette
                {
                    ColorA = new Vector3(0.00000f, 0.00000f, 0.00000f),
                    ColorB = new Vector3(0.45882f, 0.45882f, 0.45882f),
                    ColorC = new Vector3(0.69804f, 0.69804f, 0.69804f),
                    ColorD = new Vector3(1.00000f, 1.00000f, 1.00000f),
                };


                public static GameBoyPalette Pocket = new GameBoyPalette
                {
                    ColorA = new Vector3(0.42353f, 0.42353f, 0.30588f),
                    ColorB = new Vector3(0.55686f, 0.54510f, 0.34118f),
                    ColorC = new Vector3(0.76471f, 0.76863f, 0.64706f),
                    ColorD = new Vector3(0.89020f, 0.90196f, 0.78824f),
                };
            }

            /// <summary>
            /// Default Effect
            /// </summary>
            public static DMG Default = new DMG();

            public GameBoyPalette Palette = GameBoyPalette.Default;

            public override void Draw(DoubleBufferCanvas buffer)
            {
                m_shader.Send("palette", Palette.ColorA, Palette.ColorB, Palette.ColorC, Palette.ColorD);
                buffer.Swap(m_shader);
            }
        }

        /// <summary>
        /// faster Gaussian blurring
        /// <para>Bilinear Gaussian blur filter as detailed here: http://rastergrid.com/blog/2010/09/efficient-gaussian-blur-with-linear-sampling/</para>
        /// <para>Produces near identical results to a standard Gaussian blur by using sub-pixel sampling, this allows us to do ~1/2 the number of pixel lookups.</para>
        /// <para>unroll convolution loop</para>
        /// </summary>
        private class FastGaussianBlur : Effect
        {
            
            public enum OffsetType
            {
                Weighted,
                Center,
            }

            public FastGaussianBlur()
            {
                RebuildShader();
            }

            public void RebuildShader()
            {
                throw new NotImplementedException();
                if (m_sigma < 1)
                {
                    m_sigma = (m_taps - 1) * m_offset / 6;
                }

                m_sigma = Math.Max(m_sigma, 1);
                int steps = (m_taps + 1) / 2;

                // Calculate gaussian function.
                float[] g_offsets = new float[steps + 1];
                float[] g_weights = new float[steps + 1];
                for (int i = 1; i <= steps; i++)
                {
                    g_offsets[i] = m_offset * (i - 1);

                    // We don't need to include the constant part of the gaussian function as we normalize later.
                    // 1 / math.sqrt(2 * sigma ^ math.pi) * math.exp(-0.5 * ((offset - 0) / sigma) ^ 2 )
                    g_weights[i] = Mathf.Exp(-0.5f * Mathf.Pow(g_offsets[i] - 0, 2) * 1 / Mathf.Pow(m_sigma, 2));
                }

                // Calculate offsets and weights for sub-pixel samples.
                float[] offsets = new float[steps + 4];
                float[] weights = new float[steps + 4];

                for (int i = steps; i >= 2; i -= 2)
                {
                    float oA = g_offsets[i], oB = g_offsets[i - 1];
                    float wA = g_weights[i], wB = g_weights[i - 1];

                    // On center tap the middle is getting sampled twice so half weight.
                    wB = oB == 0 ? wB / 2 : wB;

                    float weight = wA + wB;
                    offsets[steps + 1] = m_type == OffsetType.Center ? (oA + oB) / 2 : (oA * wA + oB * wB) / weight;
                    weights[steps + 1] = weight;
                }

                StringBuilder code = new StringBuilder();
                code.AppendLine(@"
                    extern vec2 direction;
                    vec4 effect(vec4 color, Image tex, vec2 tc, vec2 sc) {
                ");
                float norm = 0;

                if (steps % 2 == 0)
                    code.AppendLine("vec4 c = vec4( 0.0f );");
                else
                {
                    var weight = g_weights[1];
                    norm = norm + weight;
                    code.AppendLine($"vec4 c = {weight} * texture2D(tex, tc);");
                }

            }

            int m_taps = 7;
            float m_offset = 1;
            OffsetType m_type = OffsetType.Weighted;
            float m_sigma = -1;

            /// <summary>
            /// Number of effective samples to take per pass. e.g. 3-tap is the current pixel and the neighbors each side.
            /// <para>More taps = larger blur, but slower.</para>
            /// </summary>
            public int Taps
            {
                get
                {
                    return m_taps;
                }

                set
                {
                    if (value >= 3)
                        throw new ArgumentException("Invalid value for `taps': Must be >= 3");

                    if (value % 2 == 1)
                        throw new ArgumentException("Invalid value for `taps': Must be odd");

                    m_taps = value;
                    RebuildShader();
                }
            }

            /// <summary>
            /// <para>Offset of each tap.</para>
            /// <para>For highest quality this should be &lt;= 1 but if the image has low entropy we can approximate the blur with a number &gt; 1 and less taps, for better performance.</para>
            /// </summary>
            public float Offset
            {
                get
                {
                    return m_offset;
                }

                set
                {
                    m_offset = value;
                    RebuildShader();
                }
            }

            /// <summary>
            /// <para>Offset type, either 'weighted' or 'center'.</para>
            /// <para>'weighted' gives a more accurate gaussian decay but can introduce modulation for high frequency details.</para>
            /// </summary>
            public OffsetType Type
            {
                get
                {
                    return m_type;
                }

                set
                {
                    m_type = value;
                    RebuildShader();
                }
            }

            /// <summary>
            /// <para>Sigma value for gaussian distribution. You don't normally need to set this.</para>
            /// </summary>
            public float Sigma
            {
                get
                {
                    return m_sigma;
                }

                set
                {
                    m_sigma = value;
                    RebuildShader();
                }
            }

            public override void Draw(DoubleBufferCanvas buffer)
            {
                //m_shader.Send("direction", 1f / Graphics.GetWidth(), 0);
                //buffer.Swap(m_shader);

                //m_shader.Send("direction", 0, 1f / Graphics.GetHeight());
                //buffer.Swap(m_shader);
            }
        }

        /// <summary>
        /// image noise
        /// </summary>
        public class FilmGrain : Effect
        {

            static readonly Image nosiseTex;
            static readonly Shader m_shader = Graphics.NewShader(@"
                extern number opacity;
                extern number size;
                extern vec2 noise;
                extern Image noisetex;
                extern vec2 tex_ratio;

                float rand(vec2 co) {
                    return Texel(noisetex, mod(co * tex_ratio / vec2(size), vec2(1.0))).r;
                }

                vec4 effect(vec4 color, Image texture, vec2 tc, vec2 _) {
                    return color * Texel(texture, tc) * mix(1.0, rand(tc+vec2(noise)), opacity);
                }
            ");

            public static FilmGrain Default = new FilmGrain();
            public float Opacity = .3f, Size = 1;

            static FilmGrain()
            {
                var imgData = Image.NewImageData(256, 256, ImageDataPixelFormat.RGBA8);
                imgData.MapPixel((int x, int y, Pixel p) =>
                {
                    var pixel = new Pixel();
                    var v = (byte)(Mathf.Random() * 255);
                    pixel.rgba8.r = v;
                    pixel.rgba8.g = v;
                    pixel.rgba8.b = v;
                    pixel.rgba8.a = v;
                    return pixel;
                });

                nosiseTex = Graphics.NewImage(imgData);
                m_shader.Send("noisetex", nosiseTex);
                m_shader.Send("tex_ratio", Graphics.GetWidth() / (float)nosiseTex.GetWidth(), Graphics.GetHeight() / (float)nosiseTex.GetHeight());
            }

            public override void Draw(DoubleBufferCanvas buffer)
            {
                m_shader.Send("opacity", Mathf.Max(Opacity, 0));
                m_shader.Send("size", Mathf.Max(Size, 0));
                m_shader.Send("noise", Mathf.Random(), Mathf.Random());
                buffer.Swap(m_shader);
            }

        }

        /// <summary>
        /// gaussian blur
        /// </summary>
        public class GaussianBlur : Effect
        {
            Shader m_shader;
            public static GaussianBlur Default = new GaussianBlur();

            float m_sigma = 1;
            public float Sigma
            {
                get { return m_sigma;  }
                set { m_sigma = Mathf.Max(value, 0); ResetShader(); }
            }

            public GaussianBlur()
            {
                ResetShader();
            }

            void ResetShader()
            {
                float sigma = m_sigma;
                int support = Math.Max(1, Mathf.FloorToInt(3 * sigma + .5f));
                float one_by_sigma_sq = sigma > 0 ? 1 / (sigma * sigma) : 1;
                float norm = 0;

                StringBuilder code = new StringBuilder(support * 2 + 10);
                code.AppendLine(@"
                    extern vec2 direction;
                    vec4 effect(vec4 color, Image texture, vec2 tc, vec2 _)
                    { vec4 c = vec4(0.0f);
                ");

                for(int i = -support; i <= support; i++)
                {
                    float coeff = Mathf.Exp(-.5f * i * i * one_by_sigma_sq);
                    norm = norm + coeff;
                    code.AppendLine($"c += vec4({coeff}) * Texel(texture, tc + vec2({i}) * direction);");
                }

                code.AppendLine($"return c * vec4({(norm > 0 ? 1/norm : 1)}) * color;");
                code.AppendLine("}");

                m_shader = Graphics.NewShader(code.ToString());
            }

            public override void Draw(DoubleBufferCanvas buffer)
            {
                m_shader.Send("direction", 1f / Graphics.GetWidth(), 0f);
                buffer.Swap(m_shader);

                m_shader.Send("direction", 0f, 1f / Graphics.GetHeight());
                buffer.Swap(m_shader);
            }

        }

        /// <summary>
        /// aka (light bloom)
        /// </summary>
        public class Glow : Effect
        {
            Shader m_shaderBlur;
            Canvas m_scene = Graphics.NewCanvas();
            static readonly Shader m_shaderThreshold = Graphics.NewShader(@"
                extern number min_luma;
                vec4 effect(vec4 color, Image texture, vec2 tc, vec2 _) {
                    vec4 c = Texel(texture, tc);
                    number luma = dot(vec3(0.299, 0.587, 0.114), c.rgb);
                    return c * step(min_luma, luma) * color;
                }
            ");

            public static Glow Default = new Glow();

            float m_minLuma = 0.7f, m_strength = 5;
            public float MinLuma
            {
                get { return m_minLuma; }
                set { m_minLuma = Mathf.Clamp01(value); }
            }

            public float Strength
            {
                get { return m_strength; }
                set
                {
                    if (value != m_strength)
                    {
                        m_strength = Mathf.Max(0, value);
                        ResetShader();
                    }
                }
            }

            public Glow()
            {
                ResetShader();
            }

            void ResetShader()
            {
                float sigma = m_strength;
                int support = Math.Max(1, Mathf.FloorToInt(3 * sigma + .5f));
                float one_by_sigma_sq = sigma > 0 ? 1 / (sigma * sigma) : 1;
                float norm = 0;

                StringBuilder code = new StringBuilder();
                code.AppendLine(@"
                    extern vec2 direction;
                    vec4 effect(vec4 color, Image texture, vec2 tc, vec2 _) {
                    vec4 c = vec4(0.0f);
                ");

                for (int i = -support; i <= support; i++)
                {
                    float coeff = Mathf.Exp(-.5f * i * i * one_by_sigma_sq);
                    norm = norm + coeff;
                    code.AppendLine($"c += vec4({coeff}) * Texel(texture, tc + vec2({i}) * direction);");
                }

                code.AppendLine($"return c * vec4({1 / norm}) * color;");
                code.AppendLine("}");

                m_shaderBlur = Graphics.NewShader(code.ToString());
            }

            public override void Draw(DoubleBufferCanvas buffer)
            {
                buffer.Swap();
                var front = buffer.Front;
                var scene = buffer.Back;
                var back = m_scene;

                // 1st pass: draw scene with brightness threshold
                Graphics.SetCanvas(front);
                Graphics.Clear();
                Graphics.SetShader(m_shaderThreshold);
                Graphics.Draw(scene);

                //  2nd pass: apply blur shader in x
                m_shaderBlur.Send("direction", 1f / Graphics.GetWidth(), 0f);
                Graphics.SetCanvas(back);
                Graphics.Clear();
                Graphics.SetShader(m_shaderBlur);
                Graphics.Draw(front);

                // 3nd pass: apply blur shader in y and draw original and blurred scene
                Graphics.SetCanvas(front);
                Graphics.Clear();

                // original scene without blur shader
                Graphics.SetShader();
                Graphics.SetBlendMode(BlendMode.Add, BlendAlphaMode.PreMultiplied);
                Graphics.Draw(scene); // original scene

                // second pass of light blurring
                m_shaderBlur.Send("direction", 0f, 1f / Graphics.GetHeight());
                Graphics.SetShader(m_shaderBlur);
                Graphics.Draw(back);

                // restore things as they were before entering draw()
                Graphics.SetBlendMode(BlendMode.Alpha, BlendAlphaMode.PreMultiplied);
                m_scene = back;
            }

        }

    }
}
