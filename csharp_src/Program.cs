using Love;

namespace Example
{
    class Program: Scene
    {
        float x = 0, y = 0;
        Text text;

        public override void Load()
        {
            text = Graphics.NewText(Graphics.GetFont(), "hello");
        }

        public override void Update(float dt)
        {
            x = (x + 0.2f) % 800f;
            y = (y + 0.2f) % 600f;
        }

        public override void Draw()
        {
            Graphics.SetColor(1, 1, 1);
            Graphics.Print("hello LÖVE-2d-CS ?", x, y);
        }

        static void Main(string[] args)
        {
            var config = new BootConfig();
            config.WindowResizable = true;
            Boot.Run(new Program(), config);
        }
    }
}

