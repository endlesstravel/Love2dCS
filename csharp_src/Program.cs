using System;
using System.Collections.Generic;
using Love;
using Love.Misc;
using System.Linq;

namespace LoveTest
{
    abstract class Stage
    {
        virtual public void OnLoad() { }
        virtual public void OnKeyPressed(KeyConstant key) { }
        virtual public void OnUpdate(float dt) { }
        virtual public void OnDraw() { }
        virtual public void OnPause() { }
        virtual public void OnReOpne() { }
        virtual public void OnTextEditing(string text, int start, int end) { }
        virtual public void OnTextInput(string text) { }
    }

    [StageName("Test SystemLove")]
    class TestSystemLove : Stage
    {
        string url = "https://www.cnblogs.com";
        string url2 = "C:/";
        public override void OnUpdate(float dt)
        {
            if (Keyboard.IsDown(KeyConstant.LCtrl) && Keyboard.IsPressed(KeyConstant.C))
            {
                Special.SetClipboardText(text);
            }
            else if (Keyboard.IsDown(KeyConstant.LCtrl) && Keyboard.IsPressed(KeyConstant.V))
            {

                text = Special.GetClipboardText();
            }

            if (Keyboard.IsPressed(KeyConstant.U))
            {
                Special.OpenURL(url);
            }
            if (Keyboard.IsPressed(KeyConstant.L))
            {
                Special.OpenURL(url2);
            }
        }

        string text = "init-test-text";
        public override void OnDraw()
        {

            var sb = new List<string>();

            Special.GetPowerInfo(out var state, out var percent, out var seconds);

            sb.Add($"-------------------- stattus --------------------");
            sb.Add($"OS name: {Special.GetOS()}");
            if (Special.GetOS() == "Windows")
            {
                sb.Add($"GetWin32Handle: {Special.GetWin32Handle()}");
            }
            sb.Add($"Processor Count: {Special.GetProcessorCount()}");
            sb.Add($"Power Info: {state}");
            sb.Add($"Power Info percent: {percent}");
            sb.Add($"Power Info seconds: {seconds}");
            sb.Add($"-------------------- current clipboard text --------------------");
            sb.Add($"{Special.GetClipboardText()}");
            sb.Add($"-------------------- operator --------------------");
            sb.Add($"[left-ctrl + c]: put text '{text}' to clipboard");
            sb.Add($"[U]: open url {url}");
            sb.Add($"[L]: open url {url2}");

            Graphics.SetColor(0, 0, 0);
            Graphics.Print(string.Join("\n", sb), 10, 10);
        }
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
        string keyPressedReleadedStr = "";

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
            foreach(var key in (KeyConstant[])Enum.GetValues( typeof(KeyConstant)) )
            {
                if (Keyboard.IsDown(key))
                {
                    keyThatWasDown.Add(key);
                }
                if (Keyboard.IsPressed(key))
                {
                    keyPressedReleadedStr += key.ToString() +  " is pressed \n";
                }
                if (Keyboard.IsReleased(key))
                {
                    keyPressedReleadedStr += key.ToString() + " is released \n";
                }
            }

            sb.Add($"key that down : {string.Join(",", keyThatWasDown)}");

            var scancodeThatWasDown = new List<Scancode>();
            foreach (var key in keyThatWasDown)
            {
                scancodeThatWasDown.Add(Keyboard.GetScancodeFromKey(key));
            }
            sb.Add($"scancode that down (convert from KeyConstant) : {string.Join(",", scancodeThatWasDown)}");
            sb.Add($"{keyPressedReleadedStr}\n");


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

            bool hasPreviousPos = (Mouse.GetPreviousX() != Mouse.GetX() || Mouse.GetPreviousY() != Mouse.GetY());

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
                $"down Mouse.LeftButton: {Mouse.IsDown(Mouse.LeftButton)}",
                $"down Mouse.RightButton: {Mouse.IsDown(Mouse.RightButton)}",
                $"down Mouse.MiddleButton: {Mouse.IsDown(Mouse.MiddleButton)}",
                $"down Mouse.ExtendedButton1: {Mouse.IsDown(Mouse.ExtendedButton1)}",
                $"down Mouse.ExtendedButton2: {Mouse.IsDown(Mouse.ExtendedButton2)}",
                $"down Mouse.ExtendedButton3: {Mouse.IsDown(Mouse.ExtendedButton3)}",
                $"scrool X: {Mouse.GetScrollX()}",
                $"scrool Y: {Mouse.GetScrollY()}",
                $"has previous:  " + (hasPreviousPos ?  "true -> " + Mouse.GetPreviousX() + ", " + Mouse.GetPreviousY() : "false"),
                "\n",
            };

            Graphics.Print(string.Join("\n", str), 10, 10);
            //Graphics.Line(Mouse.GetPreviousX(), Mouse.GetPreviousY() ,Mouse.GetX(), Mouse.GetY());
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



    [StageName("resource file")]
    class TestResourceFile : Stage
    {
        static string TEST_FILE_PATH = "test.txt";
        static string TEST_DIR_PATH = "a_dir";
        string recursiveEnumerateBuffer;
        public override void OnKeyPressed(KeyConstant key)
        {
            if (key == KeyConstant.A)
            {
                Resource.Append(TEST_FILE_PATH, "You have to be happiness.\n");
            }

            if (key == KeyConstant.C)
            {
                Resource.Write(TEST_FILE_PATH, "new file .... \n");
            }

            if (key == KeyConstant.D)
            {
                var success = Resource.CreateDirectory(TEST_DIR_PATH);
                Console.WriteLine("CreateDirectory success : {0}", success);
            }

            if (key == KeyConstant.X)
            {
                Console.WriteLine("remove dir success : {0}", Resource.Remove(TEST_DIR_PATH));
            }


            if (key == KeyConstant.E)
            {
                var success = Resource.Remove(TEST_FILE_PATH);
                Console.WriteLine("remove success : {0}", success);
            }

            if (key == KeyConstant.R)
            {
                recursiveEnumerateBuffer = recursiveEnumerate("/", "", "");
            }

            if (key == KeyConstant.S)
            {
                throw new Exception("no support yet");
            }
        }
        public override void OnLoad()
        {
            recursiveEnumerateBuffer = recursiveEnumerate(".", "", "");
        }
        public override void OnUpdate(float dt)
        {

            if (Keyboard.IsPressed(KeyConstant.T) || Keyboard.IsReleased(KeyConstant.T))
            {
                Resource.Append(TEST_FILE_PATH, "You have to be happiness.\n");
            }
        }

        public string recursiveEnumerate(string folder, string fileTree, string tab)
        {
            var list = Resource.GetDirectoryItems(folder);
            foreach (var item in list)
            {
                var file = folder + "/" + item;
                var info = Resource.GetInfo(file);
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
            var info = Resource.GetInfo(TEST_FILE_PATH);

            var sb = new List<string>();
            sb.Add($"-------------------- {TEST_FILE_PATH} --------------------");
            sb.Add($"exist : {info != null}");
            if (info != null)
            {
                string content = System.Text.Encoding.UTF8
                    .GetString(Resource.Read(TEST_FILE_PATH))
                    .Replace(Convert.ToChar(0x0).ToString(), " ");

                sb.Add($"info : {(info == null ? "" : info.ToString())}");
                sb.Add($"text content : {content}");

            }
            sb.Add($"-------------------- Operate --------------------");
            sb.Add($"[T/T]: Append to file  '{TEST_FILE_PATH}'  'You have to be happiness.(LR)' ");
            sb.Add($"[A]: Append to file  '{TEST_FILE_PATH}'  'You have to be happiness.(LR)' ");
            sb.Add($"[C]: (Re)create new file  '{TEST_FILE_PATH}' with content 'new file ....(LR)' ");
            sb.Add($"[E]: remove file  '{TEST_FILE_PATH}'  ");
            sb.Add($"[X]: remove dir  '{TEST_DIR_PATH}'  ");
            sb.Add($"[D]: create dir {TEST_DIR_PATH} ");
            sb.Add($"[S]: toogle symbolic link switch ");
            sb.Add($"-------------------- Status --------------------");
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

            Graphics.Line(
                10, 10,
                20, 20, 200);

            //Graphics.Line(
            //    10, 10,
            //    20, 10,
            //    20, 20,
            //    30, 20,
            //    30, 30,
            //    40, 30,
            //    40, 40);


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
            foreach (var s in sourceList)
            {
                sb.Add((currentControl == s ? "* ": "  ")
                    + "playing : " + (s.IsPlaying()) + " "
                    + dictionary[s]);
            }
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

        string textInputStr = "on-text-input\n";

        public override void OnTextEditing(string text, int start, int end)
        {
            Console.WriteLine($"OnTextEditing from {start} to {end}:" + text);
        }

        public override void OnTextInput(string text)
        {
            textInputStr += text + "\n";
        }

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
            Graphics.Print(textInputStr + $"\ntext width : {text.GetWidth()}", 0, 0);
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

            Graphics.GetCanvas();
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
            Graphics.Print("ABCDEFGHIJKLMNOPQRSTUVWXYZ\nabcdefghijklmnopqrstuvwxyz0123456789?.,", 0, 100);

            Graphics.SetFont(bmfFont);
            Graphics.Printf("如烟往事俱忘却，心底无私天地宽。——陶铸\n不应当急于求成，应当去熟悉自己的研究对象，锲而不舍，时间会成全一切。凡事开始最难，然而更难的是何以善终。——莎士比亚\n", 0, 200, 500);
        }
    }


    [StageName("test Joystick")]
    class TestJoystick : Stage
    {
        public override void OnDraw()
        {
            var jys = Joystick.GetJoysticks();
            if (jys.Length > 0)
            {
                var joy = jys[0];;
                Graphics.Print("has");

                if (joy.IsDown(0))
                {
                    joy.SetVibration(1, 0, 1);
                }
                else
                {
                    joy.SetVibration();
                }
            }
            else
            {
                Graphics.Print("has no");
            }
        }
    }


    [StageName("test lua 1")]
    class TestLua : Stage
    {
        public override void OnLoad()
        {
            Lua.Load("res/main.lua");

            Lua.RegisterFunction(typeof(Math), "Cos", "cos");
            Lua.RegisterFunction(typeof(TestLua), "StaticWriteLine", "staticWriteLine");
            Lua.RegisterFunction(this, "Add", "add");
            Lua.RegisterFunction(this, "PrintLine", "printLine");
            Lua.RegisterFunction(this, "PrintArrayLine", "printArrayLine");
            Lua.RegisterFunction(this, "PrintArrayLineFloat", "printArrayLineFloat");
            Lua.RegisterFunction(this, "ByteToStr");

            Lua.DoString(" love.sharp.staticWriteLine('staticWriteLine') ");

            Console.WriteLine("Math.Cos(3.14f / 2f)  : " +  Math.Cos(3.14f / 2f));
            Lua.DoString(" love.sharp.staticWriteLine('love.sharp.cos(3.14 / 2) : ' ..  love.sharp.cos(3.14 / 2)) ");
            Lua.DoString(" love.sharp.printLine('----------------------------') ");
            Lua.DoString(" love.sharp.printLine('byte to str  : ' ..  love.sharp.ByteToStr(1)) ");
            Lua.DoString(" love.sharp.printLine(love.sharp.add(123, 456)) ");
            Lua.DoString(" love.sharp.printArrayLine({ 'aaa', 'bbb' }) ");
            Lua.DoString(" love.sharp.printArrayLineFloat({ 1, 2 }) ");
        }

        public override void OnUpdate(float dt)
        {
            Lua.Update(dt);
        }
        public override void OnDraw()
        {
            Lua.Draw();
        }

        public static void StaticWriteLine(string info) => Console.WriteLine(info);
        public string ByteToStr(byte b) => b.ToString();
        public void PrintArrayLineFloat(float[] info) => Console.WriteLine(string.Join(",  ", info));
        public void PrintArrayLine(string[] info) => Console.WriteLine(string.Join(",  ", info));
        public void PrintLine(string info) => Console.WriteLine(info);
        public int Add(int a, int b) => a + b;
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
                if (lastIsDown == false && Mouse.IsDown(Mouse.LeftButton))
                {
                    isDown = true;
                }
                else
                {
                    isDown = false;
                }
                lastIsDown = Mouse.IsDown(Mouse.LeftButton);
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
            AddStage(new TestLua());
            AddStage(new TestMouse());
            AddStage(new TestKeyborad());
            AddStage(new TestFile());
            AddStage(new TestResourceFile());
            AddStage(new TestFont());
            AddStage(new TestImageData());
            AddStage(new TestAudio());
            AddStage(new TestVideo());
            AddStage(new TestStencil());
            AddStage(new TestText());
            AddStage(new TestSystemLove());
            AddStage(new TestOther());
            AddStage(new TestJoystick());
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

            FPSGraph.Update(dt);
            var gw = Graphics.GetWidth();
            var gh = Graphics.GetHeight();
            FPSGraph.Position = new Vector2(gw - FPSGraph.Size.width, gh - FPSGraph.Size.height);
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

        public override void TextEditing(string text, int start, int end)
        {
            if (currentStage != null)
            {
                currentStage.OnTextEditing(text, start, end);
            }
        }

        public override void TextInput(string text)
        {
            if (currentStage != null)
            {
                currentStage.OnTextInput(text);
            }
        }

        public override void Draw()
        {
            Graphics.Clear(136 / 255f, 193 / 255f, 206 / 255f, 1);
            FPSGraph.Draw();

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

        public override bool ErrorHandler(Exception e)
        {
            return base.ErrorHandler(e);
        }

        static void Main(string[] args)
        {
            try
            {
                Boot.Run(new Program(), new BootConfig
                {
                    WindowX = 100,
                    WindowY = 100,
                    //WindowFullscreen = true,
                    //WindowFullscreenType = FullscreenType.DeskTop,
                    WindowResizable = true,
                    WindowTitle = "test",
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }


    class TestQuadTreeQueryAreaSingle : TestQuadTreeScene
    {
        const float Width = 600, Height = 600;

        public override void Draw()
        {
            var x = Mouse.GetX();
            var y = Mouse.GetY();
            Graphics.Clear(0.7f, 0.7f, 0.7f);

            var msRect = new RectangleF(x, y, Width, Height);

            // draw rect selected
            Graphics.SetColor(1, 0, 0);
            foreach (var item in rectList)
            {
                if (item.IntersectsWith(msRect))
                {
                    Graphics.SetColor(Color.Red);
                }
                else
                {
                    Graphics.SetColor(Color.White);
                }
                if (m_drawDebugInfo)
                    Graphics.Rectangle(DrawMode.Line, item);
            }

            // draw mouse rect (BLUE)
            Graphics.SetColor(0, 0, 1);
            Graphics.Rectangle(DrawMode.Line, x, y, Width, Height);
            base.Draw();
        }
    }

    class TestQuadTreeQueryArea : TestQuadTreeScene
    {
        const float Width = 600, Height = 600;
        public override void Draw()
        {
            var x = Mouse.GetX();
            var y = Mouse.GetY();
            Graphics.Clear(0.7f, 0.7f, 0.7f);

            // draw tree
            if (m_drawDebugInfo)
                tree.DrawDebug();

            // query and draw container item (RED)
            for (int i = 0; i < 100; i++)
                tree.QueryArea(new RectangleF(x, y, Width, Height));
            var queryResult = tree.QueryArea(new RectangleF(x, y, Width, Height));

            // draw rect selected
            Graphics.SetColor(1, 0, 0);
            foreach (var item in queryResult)
            {
                if (m_drawDebugInfo)
                    Graphics.Rectangle(DrawMode.Line, item.Zone);
            }

            // draw mouse rect (BLUE)
            Graphics.SetColor(0, 0, 1);
            Graphics.Rectangle(DrawMode.Line, x, y, Width, Height);

            base.Draw();
        }
    }

    class TestSingleRectRaycast : TestQuadTreeScene
    {
        public override void Draw()
        {
            float x = 300, y = 200;
            x = Mouse.GetX() * 100;
            y = Mouse.GetY() * 100;
            Graphics.Clear(0.7f, 0.7f, 0.7f);

            // draw mouse line (RED)
            Graphics.SetColor(1, 0, 0);
            Graphics.Line(0, 0, x, y);

            // draw ray cast info (GREED)
            Graphics.SetPointSize(10);
            Graphics.SetColor(0, 1, 0);
            foreach (var item in rectList)
            {
                Ray2D ray = new Ray2D(0, 0, x, y);
                if (ray.Intersects(item, out var result))
                {
                    if (m_drawDebugInfo)
                        Graphics.Points(result.X, result.Y);
                }
            }

            base.Draw();
        }
    }

    class TestRectRaycastAll : TestQuadTreeScene
    {
        public override void Draw()
        {
            Graphics.Clear(0.7f, 0.7f, 0.7f);
            float x = 300, y = 200;
            x = Mouse.GetX() * 100;
            y = Mouse.GetY() * 100;

            Ray2D ray = new Ray2D(0, 0, x, y);

            Graphics.SetColor(Color.White);
            Graphics.Line(0, 0, x, y);

            var result = tree.RayCastAll(ray);

            if (m_drawDebugInfo)
                tree.DrawDebug();

            Graphics.SetColor(Color.Black);
            Graphics.SetPointSize(2);
            foreach (var cr in result)
            {
                Graphics.Points(cr.Intersection);
            }

            base.Draw();
        }
    }

    class TestRectRaycast : TestQuadTreeScene
    {
        public override void Draw()
        {
            Graphics.Clear(0.7f, 0.7f, 0.7f);
            float x = 300, y = 200;
            x = Mouse.GetX() * 100;
            y = Mouse.GetY() * 100;

            Graphics.SetColor(Color.White);
            Graphics.Line(0, 0, x, y);


            Ray2D ray = new Ray2D(0, 0, x, y);
            if (m_drawDebugInfo)
                tree.DrawDebug();

            Graphics.SetColor(Color.Black);
            Graphics.SetPointSize(4);
            if (tree.RayCast(ray, out var result))
            {
                Graphics.Points(result.Intersection);
                Graphics.Print(result.Intersection.ToString());
            }
            else
            {
                Graphics.Print("None");
            }

            base.Draw();
        }
    }


    class TestQuadTreeMoveSingle : TestQuadTreeScene
    {
        public override void Draw()
        {
            Graphics.Clear(0.7f, 0.7f, 0.7f);
            float x = 300, y = 200;
            x = Mouse.GetX() * 100;
            y = Mouse.GetY() * 100;

            Graphics.SetColor(Color.White);
            Graphics.Line(0, 0, x, y);


            Ray2D ray = new Ray2D(0, 0, x, y);
            if (m_drawDebugInfo)
                tree.DrawDebug();

            Graphics.SetColor(Color.Black);
            Graphics.SetPointSize(4);
            if (tree.RayCast(ray, out var result))
            {
                Graphics.Points(result.Intersection);
                Graphics.Print(result.Intersection.ToString());
            }
            else
            {
                Graphics.Print("None");
            }

            base.Draw();
        }
    }

    class TestQuadTreeScene : Scene
    {
        protected bool m_drawDebugInfo = false;
        const int NUM = 1 * 1000;

        #region Setup
        protected readonly List<RectangleF> rectList = new List<RectangleF>();

        RectangleF RandomGenItemToTree()
        {
            //var w = Mathf.Random(10, 500);
            //var h = Mathf.Random(10, 400);
            //var x = Mathf.Random(10, 1000 - w);
            //var y = Mathf.Random(10, 1000 - h);

            var w = Mathf.Random(10, 30);
            var h = Mathf.Random(10, 30);
            var x = Mathf.Random(10, 1000 - w);
            var y = Mathf.Random(10, 1000 - h);
            return new RectangleF(x, y, w, h);
        }

        QuadTree GenerateTree(int num = NUM)
        {
            var tree = new QuadTree();
            for (int i = 0; i < num; i++)
            {
                var rect = RandomGenItemToTree();
                rectList.Add(rect);
                tree.Add(new Leaf(rect));
            }
            this.tree = tree;
            return tree;
        }

        protected QuadTree tree;
        public override void Load()
        {
            Window.SetMode(1000, 1000);
            GenerateTree();
        }

        int flag = 0;
        public override void Update(float dt)
        {
            FPSGraph.Update(dt);
            flag += (int)(dt * 1000);
            if (flag > 1000)
            {
                //GenerateTree();
                flag = 0;
            }
        }

        public override void Draw()
        {
            FPSGraph.Draw();
        }

        public override bool ErrorHandler(System.Exception e)
        {
            Console.Write(e.StackTrace);
            return false;
        }
        #endregion
    }

    class TestMoonShine : Scene
    {
        Moonshine ms;
        Image img;

        public override void WheelMoved(int x, int y)
        {
            Console.WriteLine($"{x}, {y}");
        }

        public override void Load()
        {
            ms = Moonshine
                .China(Moonshine.CRT.Default)
                .Next(Moonshine.DMG.Default)
                .Next(Moonshine.Scanlines.Default)
                .Next(Moonshine.Vignette.Default)
                ;
            img = Graphics.NewImage("res/img.png");
        }

        public override void Update(float dt)
        {
            FPSGraph.Update(dt);
        }

        public override void Draw()
        {
            ms.Draw(() =>
            {
                //Graphics.Rectangle(DrawMode.Fill, 300, 200, 200, 200);
                Graphics.Draw(img);
            });
            //Graphics.Draw(img, 200, 200);
            FPSGraph.Draw();
        }
    }


}
