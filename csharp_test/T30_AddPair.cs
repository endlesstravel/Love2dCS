using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Love;
using int32 = System.Int32;
using float32 = System.Single;

namespace LovePhysicsTestBed
{
    // !!!!!!!!!! perfomance problem
    class T30_AddPair : Test
    {
        List<KeyValuePair<Body, CircleShape>> bodyToDraw = new List<KeyValuePair<Body, CircleShape>>();
        Body bullet;
        PolygonShape bulletShape;

        //public override void DrawWorld()
        //{
        //    // base.Draw();
        //    var p = bullet.GetPosition();
        //    //Graphics.Points(bodyToDraw.Select(body => body.GetPosition()).ToArray());

        //    for (int i = 0; i < bodyToDraw.Count; i++)
        //    {
        //        var body = bodyToDraw[i].Key;
        //        var shape = bodyToDraw[i].Value;
        //        Graphics.Circle(DrawMode.Line, body.GetPosition() + shape.GetPoint(), shape.GetRadius());
        //    }
        //    Graphics.SetLineWidth(1 / VScale);
        //    Graphics.Rectangle(DrawMode.Line, new RectangleF(bulletShape.GetPoints()[0] + p, new SizeF(bulletShape.GetPoints()[2])));
        //}


        //public override void DrawWorld()
        //{
        //    foreach (var b in m_world.GetBodies())
        //    {
        //        Graphics.Points(b.GetPosition());
        //    }

        //}

        public override void ResetTranslation()
        {
            VScale = 20;
        }

        public override void Load()
        {
            Physics.SetMeter(1);
            m_world.SetGravity(0.0f, 0.0f);
            {
                var shape = Physics.NewCircleShape(0, 0, 0.1f);
                float minX = -6.0f;
                float maxX = 0.0f;
                float minY = 4.0f;
                float maxY = 6.0f;

                for (int i = 0; i < 400; ++i)
                {
                    var body = Physics.NewBody(m_world, RandomFloat(minX, maxX), RandomFloat(minY, maxY), BodyType.Dynamic);
                    Physics.NewFixture(body, shape, 0.01f);
                    bodyToDraw.Add(new KeyValuePair<Body, CircleShape>(body, shape));
                }
            }

            {
                bulletShape = Physics.NewRectangleShape(0, 0, 3, 3, 0);
                bullet = Physics.NewBody(m_world, -40.0f, 5.0f, BodyType.Dynamic);
                bullet.SetBullet(true);
                Physics.NewFixture(bullet, bulletShape, 1.0f);
                bullet.SetLinearVelocity(150.0f, 0.0f);
            }

            // end of Load
        }
    }
}
