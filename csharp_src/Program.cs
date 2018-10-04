using System;
using System.Collections.Generic;

namespace Love
{
    abstract class Stage
    {
        virtual public void OnLoad() { }
        virtual public void OnKeyPressed(KeyConstant key) { }
        virtual public void OnUpdate(float dt) { }
        virtual public void OnDraw() { }
        virtual public void OnPause() { }
        virtual public void OnReOpne() { }
    }

    class Button
    {
        public void Update(float dt)
        {

        }
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

    [StageName("test mouse")]
    class TestMouse : Stage
    {
        Cursor cursor;
        public override void OnLoad()
        {
            // https://opengameart.org/content/bw-ornamental-cursor-19x19
            cursor = Mouse.NewCursor("res/pointer.png", 0, 0);
            Mouse.SetCursor(cursor);
        }
        public override void OnUpdate(float dt)
        {
        }
        public override void OnReOpne()
        {
            Mouse.SetCursor(cursor);
        }
        public override void OnKeyPressed(KeyConstant key)
        {
            var dic = new Dictionary<KeyConstant, SystemCursor>();
            dic.Add(KeyConstant.A, SystemCursor.Arrow);
            dic.Add(KeyConstant.B, SystemCursor.Crosshair);
            dic.Add(KeyConstant.C, SystemCursor.Hand);
            dic.Add(KeyConstant.D, SystemCursor.Ibeam);
            dic.Add(KeyConstant.E, SystemCursor.No);
            dic.Add(KeyConstant.F, SystemCursor.SizeAll);
            dic.Add(KeyConstant.G, SystemCursor.SizeNESW);
            dic.Add(KeyConstant.H, SystemCursor.SizeNS);
            dic.Add(KeyConstant.I, SystemCursor.SizeNWSE);
            dic.Add(KeyConstant.J, SystemCursor.SizeWE);
            dic.Add(KeyConstant.K, SystemCursor.Wait);
            dic.Add(KeyConstant.L, SystemCursor.WaitArrow);

            if (dic.ContainsKey(key))
            {
                Mouse.SetCursor(Mouse.GetSystemCursor(dic[key]));
            }

            if (key == KeyConstant.Number1)
            {
                Mouse.SetCursor(cursor);
            }

            if (key == KeyConstant.Number2)
            {
                Mouse.SetGrabbed(!Mouse.IsGrabbed());
            }

            if (key == KeyConstant.Number3)
            {
                Mouse.SetPosition(10, 20);
            }

            if (key == KeyConstant.Number4)
            {
                Mouse.SetVisible(!Mouse.IsVisible());
            }

            if (key == KeyConstant.Number5)
            {
                Mouse.SetRelativeMode(!Mouse.GetRelativeMode());
            }
        }
        public override void OnDraw()
        {
            Graphics.SetColor(0, 0, 0);

            string[] str =
            {
                "[A-L]: set cursor to different system curosr",
                "[1]: set cursor custom curosr",
                "[2]: toggle grabs the mouse and confines it to the window. ",
                "[3]: change pos to (0,0). ",
                "[4]: toggle visible / invisible. ",
                "[5]: toggle relative mode. ",
                "\n",
                $"cursor cursor type: {Mouse.GetCursor().GetType()}",
                $"Is Grabbed         : {Mouse.IsGrabbed()}",
                $"pos   : {Mouse.GetPosition()}",
                $"Get Relative Mode   : {Mouse.GetRelativeMode()}",
                $"Is Visible         : {Mouse.IsVisible()}",
                $"Is Cursor Supported : {Mouse.IsCursorSupported()}",
                $"down 1: {Mouse.IsDown(1)}",
                $"down 2: {Mouse.IsDown(2)}",
                $"down 3: {Mouse.IsDown(3)}",
                $"down 4: {Mouse.IsDown(4)}",
                $"down 5: {Mouse.IsDown(5)}",
                "\n",
            };

            Graphics.Print(string.Join("\n", str), 10, 10);
        }
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

    [StageName("Audio")]
    class TestAudio : Stage
    {
        Source[] sourceList = new Source[3];
        Dictionary<Source, string> dictionary = new Dictionary<Source, string>();
        Source currentControl;
        public override void OnLoad()
        {
            // https://opengameart.org/content/prepare-your-swords
            sourceList[0] = Audio.NewSource("res/prepare_your_swords_mono.mp3", SourceType.Stream);
            dictionary.Add(sourceList[0], "res/prepare_your_swords_mono.mp3");

            // https://opengameart.org/content/prepare-your-swords
            sourceList[1] = Audio.NewSource("res/prepare_your_swords.ogg", SourceType.Stream);
            dictionary.Add(sourceList[1], "res/prepare_your_swords.ogg");

            // https://opengameart.org/content/gui-sound-effects
            sourceList[2] = Audio.NewSource("res/negative.mp3", SourceType.Static);
            dictionary.Add(sourceList[2], "res/negative.mp3");

            // set to 0
            currentControl = sourceList[0];
        }

        public override void OnPause()
        {
            Audio.Pause();
        }

        public override void OnKeyPressed(KeyConstant key)
        {
            if (key == KeyConstant.Number1)
            {
                currentControl = sourceList[0];
            }

            if (key == KeyConstant.Number2)
            {
                currentControl = sourceList[1];
            }

            if (key == KeyConstant.Number3)
            {
                currentControl = sourceList[2];
            }

            if (key == KeyConstant.P)
            {
                if (currentControl.IsPlaying() == false)
                {
                    currentControl.Play();
                }
                else
                {
                    currentControl.Pause();
                }
            }

            if (key == KeyConstant.S)
            {
                currentControl.Seek(0, TimeUnit.Seconds);
            }

            if (key == KeyConstant.R)
            {
                currentControl.SetRelative(!currentControl.IsRelative());
            }

            if (key == KeyConstant.Left)
            {
                var pos = currentControl.GetPosition();
                currentControl.SetPosition(pos.x - 1, pos.y, pos.z);
            }

            if (key == KeyConstant.Right)
            {
                var pos = currentControl.GetPosition();
                currentControl.SetPosition(pos.x + 1, pos.y, pos.z);
            }

            if (key == KeyConstant.Down)
            {
                var pos = currentControl.GetPosition();
                currentControl.SetPosition(pos.x, pos.y - 1, pos.z);
            }

            if (key == KeyConstant.Up)
            {
                var pos = currentControl.GetPosition();
                currentControl.SetPosition(pos.x, pos.y + 1, pos.z);
            }
        }

        public override void OnDraw()
        {

            string[] strs =
            {
                $"current source controled : { dictionary[currentControl] }",
                $"[1-3]:switch control source index",
                "\n",
                $"playing : {currentControl.IsPlaying()}",
                $"tell : {currentControl.Tell(TimeUnit.Seconds)}",
                $"channel count : {currentControl.GetChannelCount()}",
                $"pos : {currentControl.GetPosition()}",
                "\n",
                "[P] :pause / play",
                "[R] :set telative true",
                "[Left] :set pos x - 1",
                "[Right] :set pos x + 1",
                "[Down] :set pos y - 1",
                "[Up] :set pos y + 1",
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
            if (video != null)
            {
                video.Rewind();
                video.Play();
            }
        }

        public override void OnPause()
        {
            if (video != null)
            {
                video.Pause();
            }
        }

        public override void OnLoad()
        {
            var vstream = Video.NewVideoStream("res/small.ogv");
            video = Graphics.NewVideo(vstream);
            video.Play();
            var source = video.GetSource();
            Console.Write(source.ToString());
        }

        public override void OnDraw()
        {
            Graphics.Draw(video, 10, 50);
            string[] str =
            {
                "[R]: seek(0)",
                $"name:{video.GetStream().GetFilename()}",
                $"playing:{video.IsPlaying()}",
                $"tell:{video.Tell()}",
            };
            Graphics.Print(string.Join("   ", str), 10, 5);
        }

        public override void OnUpdate(float dt)
        {
            video.SetSource(video.GetSource());
        }

        public override void OnKeyPressed(KeyConstant key)
        {
            if (key == KeyConstant.R)
            {
                video.Seek(0);
            }
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

            bool lastIsDown = false;
            bool isDown = false;
            public void Update()
            {
                if (lastIsDown == false && Mouse.IsDown(1))
                {
                    isDown = true;
                }
                else
                {
                    isDown = false;
                }
                lastIsDown = Mouse.IsDown(1);
            }

            public bool IsHover()
            {
                var mpos = Mouse.GetPosition();
                return x <= mpos.x && mpos.x <= x + w && y <= mpos.y && mpos.y <= y + h;
            }

            public bool IsDown()
            {
                return isDown && IsHover();
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
            AddStage(new TestMouse());
            AddStage(new TestFont());
            AddStage(new TestAudio());
            AddStage(new TestVideo());
            AddStage(new TestStencil());
            AddStage(new TestText());
            AddStage(new TestOther());
        }

        public override void Update(float dt)
        {
            foreach (var container in list)
            {
                container.Update();
            }

            foreach (var container in list)
            {
                if (container.IsDown())
                {
                    if ( currentStage != container.GetStage() )
                    {
                        if (currentStage != null)
                        {
                            currentStage.OnPause();
                        }

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
                    }
                    return;
                }
            }


            if (currentStage != null)
            {
                currentStage.OnUpdate(dt);
            }
        }

        public override void KeyPressed(KeyConstant key, Scancode scancode, bool isRepeat)
        {
            if (currentStage != null)
            {
                currentStage.OnKeyPressed(key);
            }
        }

        public override void KeyReleased(KeyConstant key, Scancode scancode)
        {
            if (key == KeyConstant.Escape)
            {
                Event.Quit();
            }

            if (key == KeyConstant.F1)
            {
                WindowSettings settings = Window.GetMode();
                settings.borderless = !settings.borderless;
                Window.SetMode(settings);
            }

            if (key == KeyConstant.F2)
            {
                Window.SetFullscreen(!Window.GetFullscreen());
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
                $"[F1]:bordless / not bordless window",
                $"[F2]:full screen / window",
            };
            Graphics.Print(string.Join("    ", strs), 0, 0);
        }

        static void Main(string[] args)
        {
            Boot.Run(new Program());
        }
    }
}
