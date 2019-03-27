//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using FarseerPhysics.Collision.Shapes;
//using FarseerPhysics.Dynamics;
//using FarseerPhysics.Factories;
//using Microsoft.Xna.Framework;

//namespace LovePhysicsTestBed
//{
//    class DebugWorldDraw_FV
//    {
//        World world;
//        int bodyCount = 0;
//        public DebugWorldDraw_FV(World world)
//        {
//            this.world = world;
//        }

//        public void DrawFixture(Fixture fixture, Love.Color color)
//        {
//            var rawShape = fixture.Shape;
//            var fillColor = Love.Color.FromRGBA(color.R * .5f, color.G * .5f, color.B * .5f, 0.5f);
//            Love.Graphics.SetColor(color);
//            if (rawShape is CircleShape)
//            {
//                var shape = rawShape as CircleShape;
//                Love.Graphics.SetColor(fillColor);
//                Love.Graphics.Circle(Love.DrawMode.Fill, shape.Position.X, shape.Position.Y, shape.Radius, bodyCount < 100 ? 100 : 10);
//                Love.Graphics.SetColor(color);
//                Love.Graphics.Circle(Love.DrawMode.Line, shape.Position.X, shape.Position.Y, shape.Radius, bodyCount < 100 ? 100 : 10);
//            }
//            else if (rawShape is EdgeShape)
//            {
//                var shape = rawShape as EdgeShape;
//                var plist = new Vector2[] { shape.Vertex1, shape.Vertex2 };
//                Love.Graphics.Line(plist.Select(p => new Love.Vector2(p.X, p.Y)).ToArray());
//            }
//            else if (rawShape is ChainShape)
//            {
//                var shape = rawShape as ChainShape;
//                var plist = shape.Vertices;
//                Love.Graphics.Line(plist.Select(p => new Love.Vector2(p.X, p.Y)).ToArray());
//                Love.Graphics.Points(plist.Select(p => new Love.Vector2(p.X, p.Y)).ToArray());
//            }
//            else if (rawShape is PolygonShape)
//            {
//                var shape = rawShape as PolygonShape;
//                var points = shape.Vertices;
//                Love.Graphics.SetColor(fillColor);
//                Love.Graphics.Polygon(Love.DrawMode.Fill, points.Select(p => new Love.Vector2(p.X, p.Y)).ToArray());
//                Love.Graphics.SetColor(color);
//                Love.Graphics.Polygon(Love.DrawMode.Line, points.Select(p => new Love.Vector2(p.X, p.Y)).ToArray());
//            }
//        }

//        public static Love.Color ColorByBody(FarseerPhysics.Dynamics.Body body)
//        {
//            Love.Color c = Love.Color.FromRGBA(0.9f, 0.7f, 0.7f, 1.0f);
//            //if (body == false)
//            //{
//            //    c = Color.FromRGBA(0.5f, 0.5f, 0.3f, 1.0f);
//            //}
//            //else 
//            if (body.BodyType == FarseerPhysics.Dynamics.BodyType.Static)
//            {
//                c = Love.Color.FromRGBA(0.5f, 0.9f, 0.5f, 1.0f);
//            }
//            else if (body.BodyType == FarseerPhysics.Dynamics.BodyType.Kinematic)
//            {
//                c = Love.Color.FromRGBA(0.5f, 0.5f, 0.9f, 1.0f);
//            }
//            else if (body.Awake == false)
//            {
//                c = Love.Color.FromRGBA(0.6f, 0.6f, 0.6f, 1.0f);
//            }

//            return c;
//        }


//        public void DrawBody(Body body)
//        {
//            Love.Graphics.Push();
//            Love.Graphics.Translate(body.Position.X, body.Position.Y);
//            Love.Graphics.Rotate(body.Rotation);

//            var fList = body.FixtureList;
//            foreach (var f in fList)
//            {
//                var c = ColorByBody(body);
//                DrawFixture(f, c);
//            }

//            Love.Graphics.Pop();
//        }

//        public static void Draw(World world)
//        {
//            new DebugWorldDraw_FV(world).Draw();
//        }
//        public void Draw()
//        {
//            // draw body
//            {
//                bodyCount = world.BodyList.Count;
//                foreach (var body in world.BodyList)
//                {
//                    DrawBody(body);
//                }
//            }

//            // draw joint
//            {
//                Love.Graphics.SetColor(0.5f, 0.8f, 0.8f, 1);
//                foreach (var joint in world.JointList)
//                {
//                    var aaa = joint.WorldAnchorA;
//                    var aab = joint.WorldAnchorA;
//                    float x1 = aaa.X;
//                    float y1 = aaa.Y;
//                    float x2 = aab.X;
//                    float y2 = aab.Y;
//                    if (x1 != 0 && x2 != 0)
//                        Love.Graphics.Line(x1, y1, x2, y2);
//                    else
//                    {
//                        if (x1 != 0)
//                            Love.Graphics.Points(x1, y1);
//                        if (x2 != 0)
//                            Love.Graphics.Points(x2, y2);
//                    }
//                }
//            }

//            // draw contacts
//            {
//                Love.Graphics.SetColor(Love.Color.Red); // read
//                List<Vector2> pointList = new List<Vector2>();
//                var carray = world.ContactList;
//                for (int cidx = 0; cidx < carray.Count; cidx++)
//                {
//                    var c = carray[cidx];
//                    c.GetWorldManifold(out var n, out var points);
//                    if (points[0].X != 0)
//                        pointList.Add(points[0]);
//                    if (points[1].X != 0)
//                        pointList.Add(points[1]);
//                }

//                Love.Graphics.Points(pointList.Select(p => new Love.Vector2(p.X, p.Y)).ToArray());
//            }
//        }
//    }
//}
