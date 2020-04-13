using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Love
{
    class TestSence1 : Scene
    {
        SpriteBatch batch;

        public override void Load()
        {
            var img = Graphics.NewImage("logo.png");
            var quad = Graphics.NewQuad(0, 0, 10, 10, 100, 100);
            batch = Graphics.NewSpriteBatch(img, 10, SpriteBatchUsage.Stream);
            batch.Add(quad, 10, 10);
        }

        public override void Draw()
        {
            Graphics.Draw(batch, 0, 0);
        }
    }
}
