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
    class T01_Tiles: Test
    {
        const float scale = 1;
        const int e_count = 20;
        int m_fixtureCount = 0;
        
        List<KeyValuePair<Body, PolygonShape>> listToDraw = new List<KeyValuePair<Body, PolygonShape>>();
        List<KeyValuePair<Body, PolygonShape>> groundToDraw = new List<KeyValuePair<Body, PolygonShape>>();
        List<KeyValuePair<Body, Mesh>> meshToDraw = new List<KeyValuePair<Body, Mesh>>();


        public void Draw(List<KeyValuePair<Body, PolygonShape>> ddlistraw)
        {
            List<Vector2> list = new List<Vector2>(ddlistraw.Count);
            List<RectangleF> listx = new List<RectangleF>(ddlistraw.Count);
            for (int i = 0; i < ddlistraw.Count; i++)
            {
                var body = ddlistraw[i].Key;
                var rect = ddlistraw[i].Value;
                var pos = ((body.GetPosition() + rect.GetPoints()[0]));
                list.Add(pos);

                var size = rect.GetPoints()[2] - rect.GetPoints()[0];
                var r = new RectangleF(pos, new SizeF(size));
                listx.Add(r);
                Graphics.SetColor(ColorByBody(body));
                Graphics.Rectangle(DrawMode.Line, r);
            }
        }

        public static Color ColorByBody(Body body)
        {
            Color c = Color.FromRGBA(0.9f, 0.7f, 0.7f, 1.0f);
            if (body.IsActive() == false)
            {
                c = Color.FromRGBA(0.5f, 0.5f, 0.3f, 1.0f);
            }
            else if (body.GetBodyType() == BodyType.Static)
            {
                c = Color.FromRGBA(0.5f, 0.9f, 0.5f, 1.0f);
            }
            else if (body.GetBodyType() == BodyType.Kinematic)
            {
                c = Color.FromRGBA(0.5f, 0.5f, 0.9f, 1.0f);
            }
            else if (body.IsAwake() == false)
            {
                c = Color.FromRGBA(0.6f, 0.6f, 0.6f, 1.0f);
            }

            return c;
        }
        public override void DrawWorld()
        {

            meshToDraw.ForEach(item => {
                var p = item.Key.GetPosition();
                Graphics.SetColor(ColorByBody(item.Key));
                Graphics.Draw(item.Value, p.x, p.y);
            });
            
        }

        public override void ResetTranslation()
        {
            VScale = 10;
            VOffset = new Vector2();
        }

        public override void Load()
        {
            //Physics.SetMeter(10);
            //m_world.SetGravity(0, -100);
            //float scale = 1 / Physics.GetMeter();
            {
                float a = 0.5f * scale;
                var ground = Physics.NewBody(m_world, 0, -a);
                int N = 200;
                int M = 10;
                var position = new Vector2();
                for (int32 j = 0; j < M; ++j)
                {
                    position.x = -N * a;
                    for (int32 i = 0; i < N; ++i)
                    {
                        var shape = Physics.NewRectangleShape(position.x * scale, position.y * scale, a * 2 * scale, a * 2 * scale, 0);
                        Physics.NewFixture(ground, shape, 0);
                        ++m_fixtureCount;
                        groundToDraw.Add(new KeyValuePair<Body, PolygonShape>(ground, shape));

                        position.x += 2.0f * a;
                    }
                    position.y -= 2.0f * a;
                }
            }

            {
                float32 a = 0.5f * scale;
                var shape = Physics.NewRectangleShape(0, 0, a * 2 * scale, a * 2 * scale, 0);
                //var shape = Physics.NewRectangleShape(0, 0, a, a, 0);

                var x = new Vector2(-7.0f, 0.75f) * scale;
                var y = new Vector2();
                var deltaX = new Vector2(0.5625f, 1.25f) * scale;
                var deltaY = new Vector2(1.125f, 0.0f) * scale;

                for (int32 i = 0; i < e_count; ++i)
                {
                    y = x;

                    for (int32 j = i; j < e_count; ++j)
                    {
                        var body = Physics.NewBody(m_world, y.x * scale, y.y * scale, BodyType.Dynamic);
                        Physics.NewFixture(body, shape, 5.0f * scale);


                        ++m_fixtureCount;
                        listToDraw.Add(new KeyValuePair<Body, PolygonShape>(body, shape));

                        y += deltaY;
                    }

                    x += deltaX;
                }
            }
            // end of Load


            foreach (var body in m_world.GetBodies())
            {
                foreach (var f in body.GetFixtureList())
                {
                    PolygonShape sp = f.GetShape() as PolygonShape;
                    var pp = sp.GetPoints();
                    var m = Graphics.NewMesh(new Vertex[]{
                        new Vertex(pp[0]),
                        new Vertex(pp[1]),
                        new Vertex(pp[2]),
                        new Vertex(pp[3]),
                    });
                    meshToDraw.Add(new KeyValuePair<Body, Mesh>(body, m));
                }
            }
        }
    }
}
