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


    [StageName("test image data")]
    class TestImageData : Stage
    {
        static int W = 1000, H = 1000;
        ImageData imageData;
        Image image;

        void PrintExecTime(string name, Action action)
        {
            var start = DateTime.Now.Ticks;
            action();
            Console.WriteLine($"{name}, consume time: { (DateTime.Now.Ticks - start) / TimeSpan.TicksPerMillisecond }");
        }

        public override void OnLoad()
        {
            byte[] data = new byte[W * H * 4];

            for (int x = 0; x < W; x++)
            {
                for (int y = 0; y < H; y++)
                {
                    data[(x + y * W) * 4 + 0] = 255;
                    data[(x + y * W) * 4 + 1] = 0;
                    data[(x + y * W) * 4 + 2] = 0;
                    data[(x + y * W) * 4 + 3] = 255;
                }
            }

            imageData = Image.NewImageData(W, H, ImageDataPixelFormat.RGBA8);
            var format = imageData.GetFormat();
            int w = imageData.GetWidth();
            int h = imageData.GetHeight();

            PrintExecTime("imageData.MapPixel((int x, int y, Float4 pixel, x, y, w, h )  ", () =>
            {
                imageData.MapPixel((int x, int y, Vector4 pixel) =>
                {
                    pixel.r = 0;
                    pixel.g = 0;
                    pixel.b = 0.9f;
                    pixel.a = 1;
                    return pixel;
                }, 0, 0, W, H);
            });

            PrintExecTime("imageData.MapPixel((int x, int y, Float4 pixel)  ", () =>
            {
                imageData.MapPixel((int x, int y, Vector4 pixel) =>
                {
                    pixel.r = 0;
                    pixel.g = 0;
                    pixel.b = 0.9f;
                    pixel.a = 1;
                    return pixel;
                });
            });

            PrintExecTime("imageData.MapPixel((int x, int y, Pixel p), x, y, w, h ) =>  ", () =>
            {
                imageData.MapPixel((int x, int y, Pixel p) =>
                {
                    if (format == PixelFormat.RGBA8)
                    {
                        p.rgba8.r = 145;
                        p.rgba8.g = 0;
                        p.rgba8.b = 255;
                        p.rgba8.a = 255;
                    }
                    else if (format == PixelFormat.RGBA16)
                    {
                        p.rgba16.r = 0;
                        p.rgba16.g = 0;
                        p.rgba16.b = (ushort)(0.9f * ushort.MaxValue);
                        p.rgba16.a = (ushort)(0.99f * ushort.MaxValue);
                    }
                    else if (format == PixelFormat.RGBA16F)
                    {
                        p.rgba16f.r = Half.FromFloat(0.2f);
                        p.rgba16f.g = Half.FromFloat(1);
                        p.rgba16f.b = Half.FromFloat(0);
                        p.rgba16f.a = Half.FromFloat(1);
                    }
                    else if (format == PixelFormat.RGBA32F)
                    {
                        p.rgba32f.r = 0.9f;
                        p.rgba32f.g = 0.3f;
                        p.rgba32f.b = 0.6f;
                        p.rgba32f.a = 0.9f;
                    }

                    return p;
                }, 0, 0, W, H);
            });

            PrintExecTime("imageData.MapPixel((int x, int y, Pixel p) =>  ", () =>
            {
                imageData.MapPixel((int x, int y, Pixel p) =>
                {
                    if (format == PixelFormat.RGBA8)
                    {
                        p.rgba8.r = 145;
                        p.rgba8.g = 0;
                        p.rgba8.b = 255;
                        p.rgba8.a = 255;
                    }
                    else if (format == PixelFormat.RGBA16)
                    {
                        p.rgba16.r = 0;
                        p.rgba16.g = 0;
                        p.rgba16.b = (ushort)(0.9f * ushort.MaxValue);
                        p.rgba16.a = (ushort)(0.99f * ushort.MaxValue);
                    }
                    else if (format == PixelFormat.RGBA16F)
                    {
                        p.rgba16f.r = Half.FromFloat(0.2f);
                        p.rgba16f.g = Half.FromFloat(1);
                        p.rgba16f.b = Half.FromFloat(0);
                        p.rgba16f.a = Half.FromFloat(1);
                    }
                    else if (format == PixelFormat.RGBA32F)
                    {
                        p.rgba32f.r = 0.9f;
                        p.rgba32f.g = 0.3f;
                        p.rgba32f.b = 0.6f;
                        p.rgba32f.a = 0.9f;
                    }

                    return p;
                });
            });

            PrintExecTime("Float[] imageData.GetPixelsFloat() =>  ", () =>
            {
                var p = imageData.GetPixelsFloat();
            });

            PrintExecTime("imageData.SetPixelsFloat( Float4[] p) =>  ", () =>
            {
                Vector4[] pixelBuffer = new Vector4[w * h];
                for (int x = 0; x < w; x++)
                {
                    for (int y = 0; y < h; y++)
                    {
                        Vector4 pixel = new Vector4();
                        pixel.r = 0.5f;
                        pixel.g = 0.4f;
                        pixel.b = 0.1f;
                        pixel.a = 1;
                        pixelBuffer[y * w + x] = pixel;
                    }
                }
                imageData.SetPixelsFloat(pixelBuffer);
            });

            PrintExecTime("Pixel[] imageData.GetPixels() =>  ", () =>
            {
                var p = imageData.GetPixels();
            });

            PrintExecTime("imageData.SetPixels( Pixel[] p) =>  ", () =>
            {
                Pixel[] pixelBuffer = new Pixel[w * h];
                for (int x = 0; x < w; x++)
                {
                    for (int y = 0; y < h; y++)
                    {
                        Pixel p = new Pixel();
                        if (format == PixelFormat.RGBA8)
                        {
                            p.rgba8.r = 145;
                            p.rgba8.g = 0;
                            p.rgba8.b = 255;
                            p.rgba8.a = 255;
                        }
                        else if (format == PixelFormat.RGBA16)
                        {
                            p.rgba16.r = 0;
                            p.rgba16.g = 0;
                            p.rgba16.b = (ushort)(0.9f * ushort.MaxValue);
                            p.rgba16.a = (ushort)(0.99f * ushort.MaxValue);
                        }
                        else if (format == PixelFormat.RGBA16F)
                        {
                            p.rgba16f.r = Half.FromFloat(0.2f);
                            p.rgba16f.g = Half.FromFloat(1);
                            p.rgba16f.b = Half.FromFloat(0);
                            p.rgba16f.a = Half.FromFloat(1);
                        }
                        else if (format == PixelFormat.RGBA32F)
                        {
                            p.rgba32f.r = 0.9f;
                            p.rgba32f.g = 0.3f;
                            p.rgba32f.b = 0.6f;
                            p.rgba32f.a = 0.9f;
                        }

                        pixelBuffer[y * w + x] = p;
                    }
                }
                imageData.SetPixels(pixelBuffer);
            });

            image = Graphics.NewImage(imageData);
        }
        public override void OnUpdate(float dt)
        {
        }
        public override void OnDraw()
        {
            Graphics.SetColor(0, 0, 0);
            Graphics.Print($"{image.GetWidth()} {image.GetHeight()} {imageData.GetFormat()} {imageData.GetSize()}", 0, 0);

            Graphics.SetColor(1, 1, 1);
            Graphics.Draw(image, 10, 10);
        }
    }


    [StageName("test shader")]
    class TestShader : Stage
    {
        Shader shader;
        Image img;
        public override void OnLoad()
        {
            //shader = new Shader();
        }
        public override void OnUpdate(float dt)
        {
        }
        public override void OnDraw()
        {
          
        }
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

    [StageName("test key board")]
    class TestKeyborad : Stage
    {
        public override void OnKeyPressed(KeyConstant key)
        {
            if (key == KeyConstant.R)
            {
                Keyboard.SetKeyRepeat(!Keyboard.HasKeyRepeat());
            }

            if (key == KeyConstant.T)
            {
                Keyboard.SetTextInput(!Keyboard.HasTextInput());
            }
        }

        public override void OnDraw()
        {
            var sb = new List<string>();

            sb.Add($"-------------------- status --------------------");
            sb.Add($" whether key repeat is enabled : {Keyboard.HasKeyRepeat()}");
            sb.Add($" whether text input events are enabled : {Keyboard.HasTextInput()}");

            sb.Add($"-------------------- recursive enumerate files --------------------");
            sb.Add($"[R]: toogle Key Repeat");
            sb.Add($"[T]: toogle Text Input");

            var keyThatWasDown = new List<KeyConstant>();
            foreach(var key in Enum.GetValues( typeof(KeyConstant)) )
            {
                if (Keyboard.IsDown((KeyConstant)key))
                {
                    keyThatWasDown.Add((KeyConstant)key);
                }
            }

            sb.Add($"key that down : {string.Join(",", keyThatWasDown)}");

            var scancodeThatWasDown = new List<Scancode>();
            foreach (var key in keyThatWasDown)
            {
                scancodeThatWasDown.Add(Keyboard.GetScancodeFromKey(key));
            }
            sb.Add($"scancode that down (convert from KeyConstant) : {string.Join(",", scancodeThatWasDown)}");


            Graphics.SetColor(0, 0, 0);
            Graphics.Print(string.Join("\n", sb), 10, 10);
        }
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


    [StageName("test file")]
    class TestFile : Stage
    {
        static string TEST_FILE_PATH = "test.txt";
        static string IDENTIFY_PATH = "test_for_my_work";
        static string MOUNT_PATH = "E:\\Animate";
        string recursiveEnumerateBuffer;
        public override void OnKeyPressed(KeyConstant key)
        {
            if (key == KeyConstant.A)
            {
                FileSystem.Append(TEST_FILE_PATH, "You have to be happiness.\n");
            }

            if (key == KeyConstant.I)
            {
                FileSystem.SetIdentity(IDENTIFY_PATH);
            }

            if (key == KeyConstant.C)
            {
                FileSystem.Write(TEST_FILE_PATH, "new file .... \n");
            }


            if (key == KeyConstant.D)
            {
                var success = FileSystem.CreateDirectory("a_dir");
                Console.WriteLine("CreateDirectory success : {0}", success);
            }


            if (key == KeyConstant.E)
            {
                var success = FileSystem.Remove(TEST_FILE_PATH);
                Console.WriteLine("remove success : {0}", success);
            }

            if (key == KeyConstant.M)
            {
                var success = FileSystem.Mount(MOUNT_PATH, "win_boot_ressources");
                Console.WriteLine("mount success : {0}", success);
            }

            if (key == KeyConstant.R)
            {
                recursiveEnumerateBuffer = recursiveEnumerate("", "", "");
            }

            if (key == KeyConstant.S)
            {
                throw new Exception("no support yet");
            }
        }
        public override void OnLoad()
        {
            recursiveEnumerateBuffer = recursiveEnumerate("", "", "");
        }
        public override void OnUpdate(float dt)
        {
        }

        public string recursiveEnumerate(string folder, string fileTree, string tab)
        {
            var list = FileSystem.GetDirectoryItems(folder);
            foreach (var item in list)
            {
                var file = folder + "/" + item;
                var info = FileSystem.GetInfo(file);
                if (info != null)
                {
                    if (info.Type == FileType.File)
                    {
                        fileTree = fileTree + "\n" + tab + file;
                    }
                    else if (info.Type == FileType.Directory)
                    {
                        fileTree = fileTree + "\n" + file + " (DIR)";
                        fileTree = recursiveEnumerate(file, fileTree, tab + "  ");
                    }
                }
            }

            return fileTree;
        }


        public override void OnDraw()
        {
            var info = FileSystem.GetInfo(TEST_FILE_PATH);

            var sb = new List<string>();
            sb.Add($"-------------------- {TEST_FILE_PATH} --------------------");
            sb.Add($"exist : {info != null}");
            if (info != null)
            {
                string content = System.Text.Encoding.UTF8
                    .GetString(FileSystem.Read(TEST_FILE_PATH))
                    .Replace(Convert.ToChar(0x0).ToString(), " ");

                sb.Add($"info : {info}");
                sb.Add($"realpath : {FileSystem.GetRealDirectory(TEST_FILE_PATH)}");
                sb.Add($"text content : {content}");

            }
            sb.Add($"-------------------- Operate --------------------");
            sb.Add($"[A]: Append to file  '{TEST_FILE_PATH}'  'You have to be happiness.(LR)' ");
            sb.Add($"[C]: (Re)create new file  '{TEST_FILE_PATH}' with content 'new file ....(LR)' ");
            sb.Add($"[E]: remove file  '{TEST_FILE_PATH}'  ");
            sb.Add($"[I]: Set identify path to  '{IDENTIFY_PATH}' ");
            sb.Add($"[S]: toogle symbolic link switch ");
            sb.Add($"-------------------- Status --------------------");
            sb.Add($"whether the game is in fused mode or not: {FileSystem.IsFused()}");
            sb.Add($"current working directory: {FileSystem.GetWorkingDirectory()}");
            sb.Add($"whether love.filesystem follows symbolic links: {FileSystem.AreSymlinksEnabled()}");
            sb.Add($"application data directory: {FileSystem.GetAppdataDirectory()}");
            sb.Add($"the full path to the designated save directory: {FileSystem.GetSaveDirectory()}");
            sb.Add($"the full path to the source or directory.: {FileSystem.GetSource()}");
            sb.Add($"Require Path: {FileSystem._GetRequirePath()}");
            sb.Add($"the full path to the directory containing the .love file.: {FileSystem._GetSourceBaseDirectory()}");
            sb.Add($"the path of the user's directory: {FileSystem.GetUserDirectory()}");
            sb.Add($"the write directory name for your game: {FileSystem.GetIdentity()}");
            sb.Add($"-------------------- recursive enumerate files --------------------");
            sb.Add($"[R]: refresh file list");
            sb.Add($"{recursiveEnumerateBuffer}");


            Graphics.SetColor(0, 0, 0);
            Graphics.Print(string.Join("\n", sb), 10, 10);
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
            Graphics.SetLineWidth(2);
            Graphics.SetColor(1, 0, 0);
            Vector2[] list =
            {
                new Vector2(10, 10),
                new Vector2(20, 10),
                new Vector2(20, 20),
                new Vector2(30, 20),
                new Vector2(30, 30),
                new Vector2(40, 30),
                new Vector2(40, 40),
            };
            Graphics.Line(list);


            // Graphics.Rectangle(DrawMode.Fill, 10, 10, 200, 200);

            Graphics.SetPointSize(5);

            var cplist = new ColoredPoint[]
            {
                new ColoredPoint(new Vector2(200, 150), new Vector4(0.5f, 0, 0, 1)),
                new ColoredPoint(new Vector2(200, 200), new Vector4(1, 0, 0, 1)),
                new ColoredPoint(new Vector2(300, 300), new Vector4(0, 1, 0, 1)),
                new ColoredPoint(new Vector2(400, 400), new Vector4(0, 0, 1, 1)),
            };

            Graphics.Points(cplist);
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
            var sb = new List<string>();
            sb.Add($"audio active source count : {Audio.GetActiveSourceCount()}");
            sb.Add($"audio distance modle : {Audio.GetDistanceModel()}");
            sb.Add($"audio Doppler Scale : {Audio.GetDopplerScale()}");
            sb.Add($"audio Orientation : {Audio.GetOrientation()}");
            sb.Add($"audio Velocity : {Audio.GetVelocity()}");
            sb.Add($"--------------------------------------------------");
            sb.Add($"current source controled : { dictionary[currentControl] }");
            sb.Add($"[1-3] : switch control source index");
            sb.Add("\n");
            sb.Add($"playing : {currentControl.IsPlaying()}");
            sb.Add($"tell : {currentControl.Tell(TimeUnit.Seconds)}");
            sb.Add($"channel count : {currentControl.GetChannelCount()}");
            sb.Add("\n------------------------------------------------");
            sb.Add("[P] : pause / play");
            sb.Add("[S] : seek to 0");

            if (currentControl.GetChannelCount() == 1)
            {
                sb.Add("\n");
                sb.Add($"pos : {currentControl.GetPosition()}");
                sb.Add("[R] : toggle Relative");
                sb.Add("[Left] : set pos x - 1");
                sb.Add("[Right] :set pos x + 1");
                sb.Add("[Down] : set pos y - 1");
                sb.Add("[Up] : set pos y + 1");
            }
            else
            {
            }
            Graphics.SetColor(0, 0, 0);
            Graphics.Print(string.Join("\n", sb), 10, 100);
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
            Graphics.Print($"text width : {text.GetWidth()}", 0, 0);
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
            Graphics.Printf("如烟往事俱忘却，心底无私天地宽。——陶铸\n不应当急于求成，应当去熟悉自己的研究对象，锲而不舍，时间会成全一切。凡事开始最难，然而更难的是何以善终。——莎士比亚\n", 0, 200, 500);
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
            AddStage(new TestKeyborad());
            AddStage(new TestFile());
            AddStage(new TestFont());
            AddStage(new TestImageData());
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
