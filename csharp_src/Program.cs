using Love;
using System;

namespace Example
{
    class Program: Scene
    {
        float x = 0, y = 0;
        Text text;

        public override void KeyPressed(Keyboard.Key key, Keyboard.Scancode scancode, bool isRepeat)
        {
            //Window.SetMode(0, 0);
            int w, h;
            WindowSettings settings = Window.GetMode(out w, out h);
            settings.borderless = !settings.borderless;
            Console.WriteLine("SetMode : {0}", Window.SetMode(w, h, settings));
        }

        public override void Load()
        {
            text = Graphics.NewText(Graphics.GetFont(), "hello");
        }

        public override void Update(float dt)
        {
            x = (x + 0.1f) % 400f;
            y = (y + 0.1f) % 300f;
        }

        public override void Draw()
        {
            Graphics.SetColor(1, 1, 1);
            Graphics.Print("hello LÖVE-2d-CS ?", (int)x, (int)y);
            Graphics.Draw(text, 0, 0);

            Graphics.Line(new Float2(10, 0), new Float2(10, 100), new Float2(210, 80));
        }

        static void Main(string[] args)
        {
            var config = new BootConfig();
            config.WindowResizable = true;
            Boot.Run(new Program(), config);
        }
    }
}

