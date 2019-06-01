using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Love
{
    internal class PhysicsDebugWorldDraw
    {
        World world;
        int bodyCount = 0;
        internal PhysicsDebugWorldDraw(World world)
        {
            this.world = world;
        }
        public void DrawFixture(Fixture fixture, Color color)
        {
            var rawShape = fixture.GetShape();
            var fillColor = Color.FromRGBA(color.R * .5f, color.G * .5f, color.B * .5f, 0.5f);
            Graphics.SetColor(color);
            if (rawShape is CircleShape)
            {
                var shape = rawShape as CircleShape;

                var p = shape.GetPoint();
                var r = shape.GetRadius();

                Graphics.SetColor(fillColor);
                Graphics.Circle(DrawMode.Fill, p, r, bodyCount < 100 ? 100 : 10);
                Graphics.SetColor(color);
                Graphics.Circle(DrawMode.Line, p, r, bodyCount < 100 ? 100 : 10);
                if (bodyCount < 100)
                {
                    Graphics.Line(p, p + new Vector2(r, 0));
                }
            }
            else if (rawShape is EdgeShape)
            {
                var shape = rawShape as EdgeShape;
                var plist = shape.GetPoints();
                Graphics.Line(plist);
            }
            else if (rawShape is ChainShape)
            {
                var shape = rawShape as ChainShape;
                var plist = shape.GetPoints();
                Graphics.Line(plist);
                Graphics.Points(plist);
            }
            else if (rawShape is PolygonShape)
            {
                var shape = rawShape as PolygonShape;
                var points = shape.GetPoints();
                Graphics.SetColor(fillColor);
                Graphics.Polygon(DrawMode.Fill, points);
                Graphics.SetColor(color);
                Graphics.Polygon(DrawMode.Line, points);
            }
        }
        public Color ColorByBody(Body body)
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
        public void DrawBody(Body body)
        {
            Graphics.Push();
            Graphics.Translate(body.GetPosition());
            Graphics.Rotate(body.GetAngle());

            var fList = body.GetFixtureList();
            foreach (var f in fList)
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

                DrawFixture(f, c);
            }

            Graphics.Pop();
        }
        public void DrawJoint(Joint joint)
        {
            joint.GetBodies(out var body1, out var body2);
            joint.GetAnchors(out var p1, out var p2);
            var x1 = body1 != null ? body1.GetPosition() : Vector2.Zero;
            var x2 = body2 != null ? body2.GetPosition() : Vector2.Zero;
            switch (joint.GetJointType())
            {
                case JointType.Distance:
                    {
                        Graphics.Line(p1, p2);
                    }
                    break;
                case JointType.Pulley:
                    {
                        var pulleyJoint = joint as PulleyJoint;
                        pulleyJoint.GetGroundAnchors(out var s1, out var s2);
                        Graphics.Line(s1, p1);
                        Graphics.Line(s2, p2);
                        Graphics.Line(s1, s2);
                    }
                    break;
                case JointType.Mouse:
                    {
                        // don't draw this
                    }
                    break;
                default:
                    Graphics.Line(x1, p1);
                    Graphics.Line(x2, p2);
                    Graphics.Line(p1, p2);
                    break;
            }
        }
        public static void Draw(World world)
        {
            new PhysicsDebugWorldDraw(world).Draw();
        }
        public void Draw()
        {
            // draw body
            {
                bodyCount = world.GetBodyCount();
                foreach (var body in world.GetBodies())
                {
                    DrawBody(body);
                }
            }

            // draw joint
            {
                Graphics.SetColor(0.5f, 0.8f, 0.8f, 1);
                foreach (var joint in world.GetJoints())
                {
                    DrawJoint(joint);
                }
            }

            // draw contacts
            {
                Graphics.SetColor(Color.Red); // read
                List<Vector2> pointList = new List<Vector2>();
                var carray = world.GetContacts();
                for (int cidx = 0; cidx < carray.Length; cidx++)
                {
                    var c = carray[cidx];
                    foreach (var p in c.GetPositions())
                    {
                        if (p.X != 0)
                        {
                            pointList.Add(p);
                        }
                    }
                }

                Graphics.Points(pointList.ToArray());
            }
        }
    }
}
