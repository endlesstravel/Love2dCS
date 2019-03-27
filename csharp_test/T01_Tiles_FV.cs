
//using System;
//using System.Diagnostics;
//using FarseerPhysics.Collision;
//using FarseerPhysics.Collision.Shapes;
//using FarseerPhysics.Common;
//using FarseerPhysics.Dynamics;
//using FarseerPhysics.Factories;
//using Microsoft.Xna.Framework;


//namespace LovePhysicsTestBed
//{
//    class T01_Tiles_FV: Test
//    {
//        private const int Count = 20;
//        int _fixtureCount = 0;

//        FarseerPhysics.Dynamics.World world = new FarseerPhysics
//            .Dynamics
//            .World(new Microsoft.Xna.Framework.Vector2(0, -10));

//        public override void ResetTranslation()
//        {
//            VScale = 1;
//        }

//        public override void DrawWorld()
//        {
//            //DebugWorldDraw_FV.Draw(world);
//            foreach(var body in world.BodyList)
//            {
//                Love.Graphics.Points(body.Position.X, body.Position.Y);
//            }
//        }
//        public override void Update(float dt)
//        {
//            base.Update(dt);
//            world.Step(dt);
//        }

//        public override void Load()
//        {
//            {
//                const float a = 0.5f;
//                Body ground = BodyFactory.CreateBody(world, new Vector2(0, -a));
//                const int N = 200;
//                const int M = 10;
//                Vector2 position = Vector2.Zero;
//                position.Y = 0.0f;
//                for (int j = 0; j < M; ++j)
//                {
//                    position.X = -N * a;
//                    for (int i = 0; i < N; ++i)
//                    {
//                        PolygonShape shape = new PolygonShape(0);
//                        shape.Vertices = PolygonTools.CreateRectangle(a, a, position, 0.0f);
//                        ground.CreateFixture(shape);
//                        ++_fixtureCount;
//                        position.X += 2.0f * a;
//                    }
//                    position.Y -= 2.0f * a;
//                }
//            }

//            {
//                const float a = 0.5f;
//                Vertices box = PolygonTools.CreateRectangle(a, a);
//                PolygonShape shape = new PolygonShape(box, 5);

//                Vector2 x = new Vector2(-7.0f, 0.75f);
//                Vector2 deltaX = new Vector2(0.5625f, 1.25f);
//                Vector2 deltaY = new Vector2(1.125f, 0.0f);

//                for (int i = 0; i < Count; ++i)
//                {
//                    Vector2 y = x;

//                    for (int j = i; j < Count; ++j)
//                    {
//                        Body body = BodyFactory.CreateBody(world);
//                        body.BodyType = BodyType.Dynamic;
//                        body.Position = y;
//                        body.CreateFixture(shape);
//                        ++_fixtureCount;
//                        y += deltaY;
//                    }

//                    x += deltaX;
//                }
//            }
//            // end of Load
//        }
//    }
//}
