using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

using Love;

namespace Example
{
    //class Program : Love.Scene
    //{
    //    Font font;

    //    public override void Load()
    //    {

    //        //var filedata = FileSystem.NewFileData("NotoSansHans-Regular.otf");
    //        //var rasterizer = Font.NewRasterizer(filedata);
    //        //font = Graphics.NewFont(rasterizer);

    //        font = Graphics.NewFont("NotoSansHans-Regular.otf", 22);

    //        Graphics.SetFont(font); // set font
    //    }

    //    float rectX = 0;
    //    float rectY = 0;

    //    public override void Update(float dt)
    //    {
    //        rectX = 400 * (float)(1 + Math.Sin(Timer.GetTime() / 10 * Math.PI));
    //        rectY = 300;// * (float)(1 + Math.Cos(Timer.GetTime() * Math.PI));

    //        Graphics.getCanvas();

    //        if (Keyboard.IsDown(Keyboard.Key.Escape))
    //        {
    //            Event.Quit();
    //        }

    //        if (Keyboard.IsDown(Keyboard.Key.Return))
    //        {
    //            Window.SetFullscreen(!Window.GetFullscreen());
    //        }
    //    }

    //    void DrawStencil()
    //    {
    //        // draw a rectangle as a stencil. Each pixel touched by the rectangle will have its stencil value set to 1. The rest will be 0.
    //        Graphics.Stencil(() =>
    //        {
    //            Graphics.Rectangle(Graphics.DrawMode.Fill,
    //                rectX - 150,
    //                rectY - 155,
    //                350, 310);
    //        }, Graphics.StencilAction.Replace, 1);
    //        Graphics.SetStencilTest(Graphics.CompareMode.Greater, 0);

    //        float r = 30;
    //        for (float x = 0; x < 800f; x += r * 2)
    //        {
    //            for (float y = 0; y < 600f; y += r * 2)
    //            {
    //                Graphics.SetColor((int)x, (int)y, (int)((x + y) % 255));
    //                Graphics.Circle(Graphics.DrawMode.Fill, x, y, r);
    //            }
    //        }

    //        Graphics.SetStencilTest();
    //    }

    //    public override void Draw()
    //    {
    //        Graphics.Print("Hello World!", 400, 300);
    //        Shader shader = Graphics.GetShader();

    //        Graphics.Print(shader == null ? "null" : "not null", 100, 100);
    //        return;

    //        DrawStencil();
    //        return;

    //        //Graphics.Print("你好", 0, 0);

    //        Graphics.Rectangle(Graphics.DrawMode.Fill, 20, 50, 60, 120);

    //        var color = new ColoredString.ColoredStringItem[] {
    //            ColoredString.Item("你", 255, 255, 0, 255),
    //            ColoredString.Item("好", 255, 0, 0, 255),
    //        };

    //        Graphics.Print(color, 0, 0);

    //        Graphics.Line(
    //            new Float2(200, 50), new Float2(400, 50), new Float2(500, 300),
    //            new Float2(100, 300), new Float2(200, 50)
    //        );
    //    }

    //    static void Main(string[] args)
    //    {
    //        var config = new BootConfig();
    //        config.WindowFullscreen = Window.FullscreenType.Exclusive;

    //        Boot.Run(new Program(), config);
    //        Console.WriteLine("........" + new CB() + "..............");

    //        Console.ReadKey();
    //    }
    //}


    class Program : Love.Scene
    {
        float x, y, w, h;
        public override void Load()
        {
            x = 20;
            y = 20;
            w = 60;
            h = 20;
        }

        public override void Update(float dt)
        {
            w = w + 1;
            h = h + 1;
        }

        public override void Draw()
        {
            Graphics.SetColor(0, 100 / 255f, 100 / 255f);
            Graphics.Rectangle(DrawMode.Fill, x, y, w, h);
        }

        static void Main(string[] args)
        {
            Boot.Run();
        }
    }
}

class Desc
{
    public Desc(string str)
    {
        Console.WriteLine(str);
    }
}

class CA
{
    Desc descA = new Desc("member of CA is init");

    public CA()
    {
        Console.WriteLine("CA init");
    }
}
class CB : CA
{
    Desc descB = new Desc("member of CB is init");

    public CB()
    {
        Console.WriteLine("CB init");
    }
}

//namespace Love2d
//{
//    class Program
//    {
//        static void boot()
//        {
//            string exepath = fs.getExecutablePath();

//            fs.setSource(exepath);
//        }

//        static float emx = 0, emy = 0;
//        class MyEventHandler : EventHandler
//        {
//            public override void MouseMoved(float x, float y, float dx, float dy, bool isTouch)
//            {
//                emx = x;
//                emy = y;
//                Console.WriteLine("{0}, {1}", x, y);
//                //Graphics.print(new string[] { "" + x + "," + y }, new Int4[] { new Int4() }, 0, 0, 0);
//            }
//        }

//        static void Main(string[] args)
//        {
//            //Boot.Run(new TestSence1());
//            Boot.Run();

//            //Console.WriteLine("init Timer , result :" + Timer.init());
//            //// Event.quit();

//            //TestFSystem();

//            //testAll();
//        }

//        static bool quit_test()
//        {
//            return true;
//        }

//        static void test_new_image()
//        {
//            var img = Graphics.newImage("logo.png");
//            print("test_new_image" + img.ToString());
//        }

//        static void testAll()
//        {
//            println("\n\n------------------- test all -------------------");

//            sd.init();
//            au.init();

//            Window.init();
//            FontModule.init();
//            ImageModule.init();
//            println("init graphics : " + Graphics.init());

//            println("init Mouse : " + Mouse.init());
//            println("init Keyboard : " + Keyboard.init());
//            JoystickModule.init();
//            Touch.init();


//            Window.setMode(400, 300);

//            Encoding big5 = Encoding.GetEncoding("big5");
//            Encoding gb2312 = Encoding.GetEncoding("gb2312");
//            Encoding utf8 = Encoding.GetEncoding("utf-8");
//            Window.setTitle("标题……");

//            Console.WriteLine("title --- " + Window.getTitle());

//            // https://opengameart.org/content/oves-essential-game-audio-pack-collection-160-files-updated
//            // Ove Melaa - ItaloLoopDikkoDikko

//            var sourceFiledata = FileSystem.newFileData("Ove Melaa - ItaloLoopDikkoDikko.ogg");
//            var decoder = Sound.newDecoder(sourceFiledata);
//            Source x = au.newSource(decoder);
//            x.play();// au.play(x);
//            println("total time :" + x.getDuration(Source.TimeUnit.UNIT_SECONDS));
//            Console.WriteLine("volume : " + au.getVolume());

//            var img_file = fs.newFileData("logo.png");
//            var img_filedata = ImageModule.newImageData(img_file);
//            var img = Graphics.newImage(new ImageData[] { img_filedata }, false, false);

//            test_new_image();

//            Random r = new Random();
//            float current = 0;
//            while (true)
//            {
//                Event.poll(new MyEventHandler());

//                Timer.sleep((r.Next() % 30) / 1000.0f);
//                Timer.step();

//                current += Timer.getDelta();
//                if (current >= 1)
//                {
//                    current = 0;
//                    Console.Write("time:" + Timer.getTime() +"  fps:" + Timer.getFPS() + "   ");
//                }

//                Graphics.clear(0, 0, 0, 255);

//                Graphics.setColor(r.Next() % 255, r.Next() % 255, r.Next() % 255, 255);

//                double mx = 0;
//                double my = 0;
//                Mouse.getPosition(out mx, out my);
//                //mx = emx; my = emy;
//                Float2[] points = { new Float2(0,0), new Float2((float)mx, (float)my) };
//                Graphics.line(points);

//                var bres = Graphics.draw(img, (float)mx, (float)my);


//                if (Mouse.isDown(1) || Keyboard.isDown(Keyboard.Key.KEY_SPACE))
//                {
//                    Graphics.circle(0, (float)mx, (float)my, 10, 20);
//                }
//                if (Mouse.isDown(2))
//                {
//                    Graphics.rectangle(0, (float)mx, (float)my, 30, 30);
//                }

//                Graphics.circle(0, 100, 100, 10, 20);

//                Graphics.print("" + mx + "," + my, 0, 0);

//                Graphics.present();
//            }
//        }
//        static void TestFSystem()
//        {
//            println("init fs:" + fs.init("love"));

//            println("set source:");
//            fs.setSource(@"D:\StudySpace\Love2DBuild\TestDll\TestDll\bin\Debug");

//            println("create dir:");
//            fs.createDirectory("jskdfjlkasd");
//            println("getLoveLastError:" + DllTool.getLoveLastError());

//            println("set Identity:");
//            fs.setIdentity("xxxfaf");
//            println("Identity:" + fs.getIdentity());

//            var youtext = fs.newFile("youtext.txt", File.Mode.MODE_APPEND);
//            youtext.write(("be \nyou are text").ToUTF8Bytes());
//            youtext.flush();
//            youtext.close();
//            println("youtext.txt real dir:" + fs.getRealDirectory("youtext.txt"));


//            println("getExecutablePath:" + fs.getExecutablePath());
//            println("appendStr:"); fs.append("my1.txt", "a\nfm".ToUTF8Bytes());

//            byte[] bs = fs.read("my.txt");
//            foreach (byte b in bs)
//            {
//                print((char)b);
//            }

//            println("test fs  end ...");
//        }

//        static void print(object o)
//        {
//            Console.Write(o);
//        }
//        static void println(object o)
//        {
//            Console.WriteLine("------------");
//            Console.WriteLine(o);
//        }
//    }
//}
