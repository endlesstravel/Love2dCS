using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Love
{
    /// <summary>
    /// Postprocessing effect repository for LÖVE.
    /// <para>adapt from https://github.com/vrld/moonshine</para>
    /// </summary>
    public class MoonShine
    {
        public abstract class Effect
        {
            /// <summary>
            /// this effect is enabled
            /// </summary>
            public virtual bool Enable { get; set; } = true;

            protected Shader m_shader;

            public virtual void Draw(DoubleBufferCanvas buffer)
            {
                buffer.Swap(m_shader);
            }
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
            static string shaderCode = @"
extern vec2 direction;
extern number radius;
vec4 effect(vec4 color, Image texture, vec2 tc, vec2 _) {
    vec4 c = vec4(0.0f, 0.2f, 0.3f, 1);
return c;
    for (float i = -radius; i <= radius; i += 1.0f)
    {
        c += Texel(texture, tc + i * direction);
    }
    return c / (2.0f * radius + 1.0f) * color;
}
            ";

            /// <summary>
            /// default BoxBlur
            /// </summary>
            public static BoxBlur Default = new BoxBlur();
            Vector2 m_radius;

            /// <summary>
            /// default BoxBlur constructor
            /// </summary>
            public BoxBlur() : this(3, 3)
            {
            }

            public BoxBlur(float radiusX, float radiusY)
            {
                SetRadius(radiusX, radiusY);
                m_shader = Graphics.NewShader(shaderCode);
            }

            public void SetRadius(float radiusX, float radiusY)
            {
                m_radius.x = radiusX;
                m_radius.y = radiusY;
            }

            public void SetRadius(Vector2 radius)
            {
                m_radius = radius;
            }

            public Vector2 GetRadius()
            {
                return m_radius;
            }

            public override void Draw(DoubleBufferCanvas buffer)
            {
                m_shader.Send("direction", 1f / Graphics.GetWidth(), 0);
                m_shader.Send("radius", Mathf.Floor(m_radius.x + .5f));
                buffer.Swap(m_shader);

                m_shader.Send("direction", 0, 1f / Graphics.GetHeight());
                m_shader.Send("radius", Mathf.Floor(m_radius.y + .5f));
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
    }
}
