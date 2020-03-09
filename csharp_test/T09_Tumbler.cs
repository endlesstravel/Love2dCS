using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Love;
using static Love.Misc.MeshUtils;

namespace LovePhysicsTestBed
{
    class T09_Tumbler: Test
    {
        const float scale = 1;
        int m_count = 0;
        float genDT  = 0 ;
        public override void ResetTranslation()
        {
            VScale = 20f / scale;
        }

        List<KeyValuePair<Body, Mesh>> listToDraw = new List<KeyValuePair<Body, Mesh>>();

        public override void Update(float dt)
        {
            {
                base.Update(dt);
                if (m_count < 200)
                {
                    genDT += dt;
                    if (genDT >= 0.02)
                    {
                        genDT = 0;
                        var b = Physics.NewBody(m_world, 0.0f * scale, 10.0f * scale, BodyType.Dynamic);
                        var r = NewRectShape(b, 0, 0, 0.125f * 2 * scale, 0.125f * 2 * scale);
                        Physics.NewFixture(b, r, 1.0f * scale);
                    }
                    m_count++;
                }
            }
        }

        public override void DrawWorld()
        {
            listToDraw.ForEach((Action<KeyValuePair<Body, Mesh>>)((KeyValuePair<Body, Mesh> item) =>
            {
                var b = item.Key;
                var m = item.Value;
                var p = b.GetPosition();
                Graphics.SetColor(T01_Tiles.ColorByBody(b));
                Graphics.Draw(m, (float)p.X, p.Y, b.GetAngle());
            }));
        }


        PolygonShape NewRectShape(Body body, float x, float y, float w, float h)
        {
            var rr=  Physics.NewRectangleShape(x, y, w, h, 0);
            var pp = rr.GetPoints();
            var rawData = new Vertex[]{
                new Vertex(pp[0].X, pp[0].Y),
                new Vertex(pp[1].X, pp[1].Y),
                new Vertex(pp[2].X, pp[2].Y),
                new Vertex(pp[3].X, pp[3].Y),
            };
            var sinfo = Love.Misc.MeshUtils.StandardVertexDescribe;
            listToDraw.Add(new KeyValuePair<Body, Mesh>(body, Graphics.NewMesh(
                sinfo,
                sinfo.TransToByte(rawData),
                MeshDrawMode.Fan, SpriteBatchUsage.Static)));
            return rr;
        }

        public override void Load()
        {
            genDT = 0;
            Body ground = Physics.NewBody(m_world);

            {
                Body body = Physics.NewBody(m_world, 0.0f * scale, 10.0f * scale, BodyType.Dynamic);
                body.SetSleepingAllowed(false);

                Physics.NewFixture(body, 
                    NewRectShape(body, 10.0f * scale, 0.0f * scale, 0.5f * 2 * scale, 10.0f * 2 * scale), 
                    5.0f * scale);

                Physics.NewFixture(body,
                    NewRectShape(body , - 10.0f * scale, 0.0f * scale, 0.5f * 2 * scale, 10.0f * 2 * scale),
                    5.0f * scale);

                Physics.NewFixture(body,
                    NewRectShape(body, 0.0f * scale, 10.0f * scale, 10.0f * 2 * scale, 0.5f * 2 * scale),
                    5.0f * scale);

                Physics.NewFixture(body,
                    NewRectShape(body, 0.0f * scale, -10.0f * scale, 10.0f * 2 * scale, 0.5f * 2 * scale),
                    5.0f * scale);

                //RevoluteJoint jd = Physics.NewRevoluteJoint(ground, body, 
                //    new Vector2(0.0f, 10.0f) * scale,
                //    new Vector2(0.0f, 0.0f) * scale
                //    );
                RevoluteJoint jd = Physics.NewRevoluteJoint(ground, body,
                    new Vector2(0.0f, 10.0f) * scale,
                    new Vector2(0.0f, 10.0f) * scale
                    );
                jd.SetMotorSpeed(-0.05f * Mathf.PI);
                jd.SetMaxMotorTorque(1e8f * scale);
                jd.SetMotorEnabled(true);
            }

            m_count = 0;
        }
    }
}
