using System;
using System.Collections.Generic;

namespace Love
{
    abstract class Stage
    {
        virtual public void OnLoad() { }
        virtual public void OnUpdate(float dt) { }
        virtual public void OnDraw() { }
        virtual public void OnPause() { }
        virtual public void OnReOpne() { }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class StageName : Attribute
    {
        public StageName(string name)
        {
            Name = name;
        }
        public readonly string Name;
    }

    [StageName("test other")]
    class TestOther : Stage
    {
        public override void OnLoad()
        {
        }
        public override void OnUpdate(float dt)
        {
        }
        public override void OnDraw()
        {
        }
    }

    [StageName("Sound")]
    class TestSound : Stage
    {
        Source bgm;
        public override void OnLoad()
        {
            // https://opengameart.org/content/prepare-your-swords
            bgm = Audio.NewSource("res/prepare_your_swords.ogg", SourceType.Stream);
            bgm.Play();
        }

        public override void OnUpdate(float dt)
        {
            if (Keyboard.IsDown(KeyConstant.P))
            {
                if( bgm.IsPlaying() == false )
                {
                    Audio.Play(bgm);
                }
                else
                {
                    bgm.Pause();
                }
            }
        }

        public override void OnDraw()
        {
            string[] strs =
            {
                "[P] : pause / play music",
                bgm.IsPlaying() ? "music playing ..." : "pause",
            };
            Graphics.SetColor(0, 0, 0);
            Graphics.Print(string.Join("\n", strs), 10, 100);
        }
    }

    [StageName("test video")]
    class TestVideo : Stage
    {
        Video video;

        public override void OnReOpne()
        {
            video.Play();
        }

        public override void OnLoad()
        {
            var vstream = Video.NewVideoStream("res/small.ogv");
            video = Graphics.NewVideo(vstream);
            video.Play();
        }

        public override void OnDraw()
        {
            Graphics.Draw(video, 10, 50);
        }

        public override void OnUpdate(float dt)
        {
        }
    }

    [StageName("test Text")]
    class TestText : Stage
    {
        float x = 0, y = 0;
        Text text;
        public override void OnLoad()
        {
            text = Graphics.NewText(Graphics.GetFont(), "hello LÖVE-2d-CS ?");
        }

        public override void OnUpdate(float dt)
        {
            x = (x + 0.1f) % 400f;
            y = (y + 0.1f) % 300f;
        }

        public override void OnDraw()
        {
            Graphics.SetColor(1, 1, 1);
            Graphics.Draw(text, (int)x, (int)y);
        }
    }

    [StageName("test stencil")]
    class TestStencil : Stage
    {
        public override void OnLoad()
        {
        }

        float rectX = 0;
        float rectY = 0;

        public override void OnUpdate(float dt)
        {
            rectX = (float)(200 * (1.0 + Math.Sin(Timer.GetTime() / 10f * Math.PI)));
            rectY = 300;// * (float)(1 + Math.Cos(Timer.GetTime() * Math.PI));

            Graphics.getCanvas();
        }

        void DrawStencil()
        {
            // draw a rectangle as a stencil.
            // Each pixel touched by the rectangle will have its stencil value set to 1. 
            // The rest will be 0.
            Graphics.Stencil(() =>
            {
                Graphics.Rectangle(DrawMode.Fill,
                    rectX - 150,
                    rectY - 155,
                    350, 310);
            },
            StencilAction.Replace, 1, true);
            Graphics.SetStencilTest(CompareMode.Greater, 0);
        }

        public override void OnDraw()
        {
            DrawStencil();

            float r = 30;
            for (float x = 0; x < 800f; x += r * 2)
            {
                for (float y = 0; y < 600f; y += r * 2)
                {
                    Graphics.SetColor((int)x / 800, (int)y / 600f, 1);
                    Graphics.Circle(DrawMode.Fill, x, y, r);
                }
            }

            // recovery stencil
            Graphics.SetStencilTest();
        }
    }


    [StageName("test font")]
    class TestFont : Stage
    {
        Font imageFont;
        Font bmfFont;
        public override void OnLoad()
        {

            imageFont = Graphics.NewImageFont("res/font_example.png", " ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789");
            bmfFont = Graphics.NewBMFont("res/ch3500.fnt");
        }

        public override void OnUpdate(float dt)
        {
        }

        public override void OnDraw()
        {
            Graphics.SetFont(imageFont);
            Graphics.Print("This is a test text", 0, 0);
            Graphics.Print("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789?.,", 0, 100);

            Graphics.SetFont(bmfFont);
            Graphics.Print("你好？？?.,", 0, 200);
        }
    }


    class Program : Scene
    {
        class StageContainer
        {
            Text name;
            Stage stg;
            int x, y, w, h;

            public Stage GetStage()
            {
                return stg;
            }

            public StageContainer(Stage stages, int x, int y, int w, int h)
            {
                var stageName = (StageName)(stages.GetType().GetCustomAttributes(typeof(StageName), false)[0]);
                this.name = Graphics.NewText(Graphics.GetFont(), stageName.Name);
                this.stg = stages;
                this.x = x;
                this.y = y;
                this.w = w;
                this.h = h;
            }

            public void Draw()
            {
                Graphics.Push(StackType.All);

                Graphics.SetLineWidth(3);
                Graphics.SetColor(1, 0, 0);
                Graphics.Rectangle(DrawMode.Line, x, y, w, h);
                Graphics.SetLineWidth(1);

                if (IsDown())
                {
                    Graphics.SetColor(1, 1, 1);
                }
                else if (IsHover())
                {
                    Graphics.SetColor(0.9f, 0.9f, 0.9f);
                }
                else
                {
                    Graphics.SetColor(0.8f, 0.8f, 0.8f);
                }
                Graphics.Rectangle(DrawMode.Fill, x, y, w, h);

                Graphics.SetColor(0, 0, 0);
                float xpos = x + (w - name.GetWidth()) * 0.5f;
                float ypos = y + (h - name.GetHeight()) * 0.5f;
                Graphics.Draw(name, xpos, ypos);

                Graphics.Pop();
            }


            public bool IsHover()
            {
                var mpos = Mouse.GetPosition();
                return x <= mpos.x && mpos.x <= x + w && y <= mpos.y && mpos.y <= y + h;
            }

            public bool IsDown()
            {
                var mdown = Mouse.IsDown(1);
                return mdown && IsHover();
            }
        }

        List<StageContainer> list = new List<StageContainer>();
        HashSet<object> loaded = new HashSet<object>();
        Stage currentStage = null;
        int btnCursor = 0;
        int anchorX = 20, anchorY = 20, anchorStageX = 150, anchorStageY = 20;

        void AddStage(Stage stage)
        {
            int x = anchorX;
            int y = anchorY + 50 * (btnCursor++);
            int w = 100;
            int h = 40;
            list.Add(new StageContainer(stage, x, y, w, h));
        }

        public override void Load()
        {
            AddStage(new TestFont());
            AddStage(new TestSound());
            AddStage(new TestVideo());
            AddStage(new TestStencil());
            AddStage(new TestText());
            AddStage(new TestOther());
        }

        public override void Update(float dt)
        {
            if (currentStage != null)
            {
                currentStage.OnUpdate(dt);
            }

            foreach (var container in list)
            {
                if (container.IsDown())
                {
                    currentStage = container.GetStage();
                    if (loaded.Contains(currentStage) == false)
                    {
                        currentStage.OnLoad();
                        loaded.Add(currentStage);
                    }
                    else
                    {
                        currentStage.OnReOpne();
                    }
                    break;
                }
            }
        }

        public override void KeyReleased(KeyConstant key, Scancode scancode)
        {
            if (key == KeyConstant.Space)
            {
                Window.SetFullscreen(!Window.GetFullscreen());
            }

            if (key == KeyConstant.Escape)
            {
                Event.Quit();
            }

            if (key == KeyConstant.B)
            {
                WindowSettings settings = Window.GetMode();
                settings.borderless = !settings.borderless;
                Window.SetMode(settings);
            }
        }

        public override void Draw()
        {
            Graphics.Clear(136 / 255f, 193 / 255f, 206 / 255f, 1);

            Graphics.Push(StackType.All);
            Graphics.Translate(anchorStageX, anchorStageY);
            if (currentStage != null)
            {
                currentStage.OnDraw();
            }

            // draw outline
            var gw = Graphics.GetWidth();
            var gh = Graphics.GetHeight();
            Graphics.SetColor(1, 1, 1);
            Graphics.SetLineWidth(2);
            Graphics.Rectangle(DrawMode.Line,
                0, 0,
                gw - anchorStageX - 5, gh - anchorStageY - 5);

            Graphics.Pop();

            foreach (var btn in list)
            {
                btn.Draw();
            }

            string[] strs = new string[]
            {
                $"FPS:{Timer.GetFPS()}",
                $"[Escape]:quit",
                $"[B]:bordless / not bordless window",
                $"[Space]:full screen / window",
            };
            Graphics.Print(string.Join("    ", strs), 0, 0);
        }

        static void Main(string[] args)
        {
            Boot.Run(new Program());
        }
    }
}
