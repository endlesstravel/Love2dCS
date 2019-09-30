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


    [StageName("test image data map")]
    class TestImageDataMap : Stage
    {
        static int W = 202, H = 200;
        PixelFormat pixelFormat = PixelFormat.RGBA8;
        ImageDataPixelFormat imageDataPixelFormat = ImageDataPixelFormat.RGBA8;
        Image[] image;


        private Color MapPixel_01(int x, int y, Color p)
        {
            var col = new Color(x / (float)W, y / (float)H, 1f, 1f);
            return new Color(col.Rf, col.Gf, col.Bf, col.Af);
        }
        private Vector4 MapPixel_02(int x, int y, Vector4 p)
        {
            var col = new Color(x / (float)W, y / (float)H, 1f, 1f);
            return new Vector4(col.Rf, col.Gf, col.Bf, col.Af);
        }
        private Pixel MapPixel_03(int x, int y, Pixel p)
        {
            var col = new Color(x / (float)W, y / (float)H, 1f, 1f);


            if (pixelFormat == PixelFormat.RGBA8)
            {
                p.rgba8.r = (byte)(col.Rf * byte.MaxValue);
                p.rgba8.g = (byte)(col.Gf * byte.MaxValue);
                p.rgba8.b = (byte)(col.Bf * byte.MaxValue);
                p.rgba8.a = (byte)(col.Af * byte.MaxValue);
            }
            else if (pixelFormat == PixelFormat.RGBA16)
            {
                p.rgba16.r = (ushort)(col.Rf * ushort.MaxValue);
                p.rgba16.g = (ushort)(col.Gf * ushort.MaxValue);
                p.rgba16.b = (ushort)(col.Bf * ushort.MaxValue);
                p.rgba16.a = (ushort)(col.Af * ushort.MaxValue);
            }
            else if (pixelFormat == PixelFormat.RGBA16F)
            {
                p.rgba16f.r = Half.FromFloat(col.Rf);
                p.rgba16f.g = Half.FromFloat(col.Gf);
                p.rgba16f.b = Half.FromFloat(col.Bf);
                p.rgba16f.a = Half.FromFloat(col.Af);
            }
            else if (pixelFormat == PixelFormat.RGBA32F)
            {
                p.rgba32f.r = col.Rf;
                p.rgba32f.g = col.Gf;
                p.rgba32f.b = col.Bf;
                p.rgba32f.a = col.Af;
            }

            return p;
        }


        public T[] GenData<T>(Func<int, int, T, T> func, T rawT)
        {
            T[] data = new T[W * H];
            for (int y = 0; y < H; y++)
            {
                for (int x = 0; x < W; x++)
                {
                    data[x + y * W] = func(x, y, rawT);
                }
            }

            return data;
        }

        public override void OnLoad()
        {
            var imgd01 = Image.NewImageData(W, H, imageDataPixelFormat);
            imgd01.MapPixel(MapPixel_01);

            var imgd02 = Image.NewImageData(W, H, imageDataPixelFormat);
            imgd02.MapPixel(MapPixel_02);

            var imgd03 = Image.NewImageData(W, H, imageDataPixelFormat);
            imgd03.MapPixel(MapPixel_03);

            //var imgd04 = Image.NewImageData(W, H, imageDataPixelFormat);
            //imgd04.SetPixel(GenData(MapPixel_01, Color.White));


            var imgd04 = Image.NewImageData(W, H, imageDataPixelFormat);
            {
                for (int  y = 0; y < H / 2; y++)
                    for (int x = 0; x < W / 2; x++)
                        imgd04.SetPixel(x, y, MapPixel_01(x, y, Color.White));

                // get & set
                var buffer = new Color[W, H];
                for (int y = 0; y < H / 2; y++)
                    for (int x = 0; x < W / 2; x++)
                    {
                        buffer[x, y] = imgd04.GetPixel(x, y);
                        imgd04.SetPixel(W/2 + x, H/2 + y, buffer[x, y]);
                    }
            }

            image = new Image[]
            {
                Graphics.NewImage(imgd01),
                Graphics.NewImage(imgd02),
                Graphics.NewImage(imgd03),
                Graphics.NewImage(imgd04),
            };
        }
        public override void OnUpdate(float dt)
        {
        }
        public override void OnDraw()
        {
            Graphics.SetColor(1, 1, 1);
            for (int i = 0; i < image.Length; i++)
            {
                Graphics.Draw(image[i], W * 0, H * i);
            }

            Graphics.SetColor(Color.Red);
            Graphics.SetLineWidth(5);
            for (int i = 0; i < image.Length; i++)
            {
                Graphics.Rectangle(DrawMode.Line, W * 0, H * i, W, H);
            }
            Graphics.SetLineWidth(1);
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
                var p = imageData.GetPixelsRaw();
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
                imageData.SetPixelsRaw(pixelBuffer);
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
            if (Mouse.IsPressed(0))
                Console.WriteLine("Mouse.IsPressed(0)");
            if (Mouse.IsReleased(0))
                Console.WriteLine("Mouse.IsReleased(0)");

            Graphics.SetColor(0, 0, 0);
            bool hasPreviousPos = (Mouse.GetPreviousX() != Mouse.GetX() || Mouse.GetPreviousY() != Mouse.GetY());
            Mouse.GetPreviousX();
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
                currentControl.SetPosition(pos.X - 1, pos.Y, pos.Z);
            }

            if (key == KeyConstant.Right)
            {
                var pos = currentControl.GetPosition();
                currentControl.SetPosition(pos.X + 1, pos.Y, pos.Z);
            }

            if (key == KeyConstant.Down)
            {
                var pos = currentControl.GetPosition();
                currentControl.SetPosition(pos.X, pos.Y - 1, pos.Z);
            }

            if (key == KeyConstant.Up)
            {
                var pos = currentControl.GetPosition();
                currentControl.SetPosition(pos.X, pos.Y + 1, pos.Z);
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


            int width = 800, height = 600;
            var normalCanvas = Graphics.NewCanvas(width, height, new Graphics.Settings()
            {
                format = PixelFormat.RGBA8
            });
            var depthStencil = Graphics.NewCanvas(width, height, new Graphics.Settings()
            {
                format = PixelFormat.DEPTH24
            });
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

    [StageName("TestMatrix")]
    class TestMatrix : Stage
    {
        public override void OnLoad()
        {
        }

        public override void OnUpdate(float dt)
        {

        }
        public override void OnDraw()
        {

            var mt = Matrix44.CreateTranslation(Vector3.Zero);
            var mr = Quaternion.ToMatrix(Quaternion.Identity);
            Graphics.Push();
            Graphics.ReplaceTransform(mt * mr);
            Graphics.Pop();
        }
    }

    [StageName("test depth buffer")]
    class TestDepthBuffer : Stage
    {
        const string sharderStr = @"
    extern float depth;
    varying vec4 vpos;

    #ifdef VERTEX
    vec4 position(mat4 transform_projection, vec4 vertex_position)
    {
        vpos = transform_projection * vertex_position;
        vpos.z = depth;
        return vpos;
    }
    #endif
    #ifdef PIXEL

    void effect()
    {
        love_Canvases[0] = VaryingColor;
    }

    #endif
";
        Shader shader;
        Canvas depthBuffer = null, renderTarget1 = null;
        public override void OnLoad()
        {
            int width = 1000, height = 1000;
            renderTarget1 = Graphics.NewCanvas(width, height, new Graphics.Settings()
            {
                format = PixelFormat.RGBA8
            });
            depthBuffer = Graphics.NewCanvas(width, height, new Graphics.Settings()
            {
                format = PixelFormat.DEPTH16
            });
            shader = Graphics.NewShader(sharderStr);
        }

        public override void OnUpdate(float dt)
        {

        }
        public override void OnDraw()
        {
            Graphics.SetCanvas(RenderTargetInfo.CreateWithDepthStencil(depthBuffer, renderTarget1));
            Graphics.Clear(0.4f, 0.4f, 1.0f, 0f);

            Graphics.SetShader(shader);
            shader.Send("depth", 1.0f);
            Graphics.SetDepthMode(CompareMode.Less, true);

            shader.Send("depth", 0.5f);
            Graphics.SetColor(Color.Red);
            Graphics.Rectangle(DrawMode.Fill, 200, 200, 100, 100);

            shader.Send("depth", 0.8f);
            Graphics.SetColor(Color.Green);
            Graphics.Rectangle(DrawMode.Fill, 250, 250, 100, 100);

            Graphics.Reset();

            Graphics.Draw(renderTarget1);
            Graphics.Draw(depthBuffer, 0f, 0f, 0f, 0.1f, 0.1f);
            Graphics.SetCanvas();
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
            //Lua.DoString(" love.sharp.cos() "); // throw error
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
                return x <= mpos.X && mpos.X <= x + w && y <= mpos.Y && mpos.Y <= y + h;
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

        Love.Misc.Moonshine ms;

        public override void Load()
        {
            AddStage(new TestImageDataMap());
            AddStage(new TestMatrix());
            AddStage(new TestDepthBuffer());
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



            ms = Love.Misc.Moonshine.China(Love.Misc.Moonshine.CRT.Default)
                    .Next(Love.Misc.Moonshine.DMG.Default)
                    .Next(Love.Misc.Moonshine.Scanlines.Default)
                    .Next(Love.Misc.Moonshine.Vignette.Default)
                ;

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
            FPSGraph.Position = new Vector2(gw - FPSGraph.Size.Width, gh - FPSGraph.Size.Height);
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

            //ms.Draw(() => { });
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


}
