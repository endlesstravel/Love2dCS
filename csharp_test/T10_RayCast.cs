using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Love;

namespace LovePhysicsTestBed
{
    class T10_RayCast: Test
    {
        public override void Load()
        {
            Physics.SetMeter(1);
            float size = 50;
            for (int i = 0;  i < 30; i++)
            {
                var shape = Physics.NewRectangleShape(
                    Mathf.Random(-size, size),
                    Mathf.Random(-size, size),
                    Mathf.Random(1f, 2f),
                    Mathf.Random(2f, 4f),
                    Mathf.Random(0f, 4f)
                    );

                Physics.NewFixture(Physics.NewBody(m_world), shape, 1.0f);


                var cshape = Physics.NewCircleShape(
                    Mathf.Random(-size, size),
                    Mathf.Random(-size, size),
                    Mathf.Random(1f, 3f)
                    );

                var b = Physics.NewBody(m_world);
                b.SetAngle(Mathf.Random(1f, 3f));
                Physics.NewFixture(b, cshape, 1.0f);
            }
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            angle += dt * 0.2f;
        }

        public override void ResetTranslation()
        {
            VScale = 5;
        }

        float angle = 0;

        public override void DrawWorld()
        {
            base.DrawWorld();
            var endPointX = Mathf.Cos(angle) * 100;
            var endPointY = Mathf.Sin(angle) * 100;
            Graphics.SetColor(Color.Wheat);
            Graphics.Line(0, 0, endPointX, endPointY);
            m_world.RayCast(0, 0, endPointX, endPointY,
                (Fixture pfixture, float x, float y, float nx, float ny, float fraction) =>
                {
                    Graphics.SetColor(Color.Yellow);
                    Graphics.SetPointSize(2);
                    Graphics.Points(x, y);
                    var p = new Vector2(x, y);
                    var n = new Vector2(nx, ny);
                    Graphics.SetColor(Color.Red);
                    Graphics.Line(p, p + n * 2);
                    return 0;
                });

        }
    }
}
