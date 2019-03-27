//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Love;

//namespace LovePhysicsTestBed
//{
//    class T02_HeavyOnLight_FV : Test
//    {
//        FarseerPhysics.Dynamics.World world = new FarseerPhysics
//            .Dynamics
//            .World(new Microsoft.Xna.Framework.Vector2(0, -10));

//        FarseerPhysics.Dynamics.Body b1, b2;
//        FarseerPhysics.Collision.Shapes.CircleShape cs1, cs2;

//        public static Color ColorByBody(FarseerPhysics.Dynamics.Body body)
//        {
//            Color c = Color.FromRGBA(0.9f, 0.7f, 0.7f, 1.0f);
//            //if (body == false)
//            //{
//            //    c = Color.FromRGBA(0.5f, 0.5f, 0.3f, 1.0f);
//            //}
//            //else 
//            if (body.BodyType == FarseerPhysics.Dynamics.BodyType.Static)
//            {
//                c = Color.FromRGBA(0.5f, 0.9f, 0.5f, 1.0f);
//            }
//            else if (body.BodyType == FarseerPhysics.Dynamics.BodyType.Kinematic)
//            {
//                c = Color.FromRGBA(0.5f, 0.5f, 0.9f, 1.0f);
//            }
//            else if (body.Awake == false)
//            {
//                c = Color.FromRGBA(0.6f, 0.6f, 0.6f, 1.0f);
//            }

//            return c;
//        }
//        void DrawCirsleShape(FarseerPhysics.Dynamics.Body body, FarseerPhysics.Collision.Shapes.CircleShape circleShape)
//        {
//            var r = circleShape.Radius;
//            Graphics.SetColor(ColorByBody(body));
//            var p = body.Position + circleShape.Position;
//            Graphics.Circle(DrawMode.Line, p.X, p.Y, r, 100);
//            Graphics.Points(p.X, p.Y);
//        }

//        public override void ResetTranslation()
//        {
//            VScale = 1;
//            VOffset = new Vector2(0, 100);
//        }

//        public override void Update(float dt)
//        {
//            base.Update(dt);
//            world.Step(dt);
//        }

//        public override void DrawWorld()
//        {
//            DebugWorldDraw_FV.Draw(world);
//        }

//        public override void Load()
//        {
//            var ground = FarseerPhysics.Factories.BodyFactory.CreateBody(world,
//                new Microsoft.Xna.Framework.Vector2(0f, 0f));
//            var es = new FarseerPhysics.Collision.Shapes.EdgeShape(
//                new Microsoft.Xna.Framework.Vector2(-40.0f, 0.0f),
//                new Microsoft.Xna.Framework.Vector2(40.0f, 0.0f)
//                );
//            ground.CreateFixture(es);

//            b1 = FarseerPhysics.Factories.BodyFactory.CreateBody(world,
//                new Microsoft.Xna.Framework.Vector2(0.0f, 0.5f));
//            b1.BodyType = FarseerPhysics.Dynamics.BodyType.Dynamic;
//            cs1 = new FarseerPhysics.Collision.Shapes.CircleShape(0.5f, 10);
//            b1.CreateFixture(cs1);

//            b2 = FarseerPhysics.Factories.BodyFactory.CreateBody(world,
//                new Microsoft.Xna.Framework.Vector2(0, 10.0f));
//            b2.BodyType = FarseerPhysics.Dynamics.BodyType.Dynamic;
//            cs2 = new FarseerPhysics.Collision.Shapes.CircleShape(5f, 10f);
//            b2.CreateFixture(cs2);
//        }
//    }
//}
