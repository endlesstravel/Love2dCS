using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Love;

namespace LovePhysicsTestBed
{
    class T02_HeavyOnLight : Test
    {
        CircleShape s1, s2;
        Body b1, b2;

        void DrawCirsleShape(Body body, CircleShape circleShape)
        {
            var p = circleShape.GetPoint() + body.GetPosition();
            var r = circleShape.GetRadius();
            Graphics.Circle(DrawMode.Line, p.x, p.y, r, 100);
        }

        public override void ResetTranslation()
        {
            VScale = 10;
            VOffset = new Vector2(0, 100);
        }

        public override void Update(float dt)
        {
            base.Update(dt);
        }

        //public override void Draw()
        //{
        //    Graphics.Push();
        //    Graphics.Scale(10, 10);
        //    Graphics.SetLineWidth(0.1f);
        //    Graphics.Translate(30, 40);
        //    DrawCirsleShape(b1, s1);
        //    DrawCirsleShape(b2, s2);
        //    Graphics.Pop();
        //}

        public override void Load()
        {
            Physics.NewFixture(
                Physics.NewBody(m_world),
                Physics.NewEdgeShape(-400.0f, 0.0f, 400.0f, 0.0f),
                0.0f);

            //b1 = Physics.NewBody(m_world, 0, 5f, BodyType.Dynamic);
            //s1 = Physics.NewCircleShape(0.0f, 5f, 5f);
            //Physics.NewFixture(
            //    b1,
            //    s1,
            //    100);

            //s2 = Physics.NewCircleShape(0, 60f, 50.0f);
            //b2 = Physics.NewBody(m_world, 0.0f, 60.0f, BodyType.Dynamic);
            //Physics.NewFixture(
            //    b2,
            //    s2,
            //    100);



            //Physics.SetMeter(1);
            b1 = Physics.NewBody(m_world, 0, 5f * 0.1f, BodyType.Dynamic);
            s1 = Physics.NewCircleShape(0.0f, 5f * 0.1f, 5f * 0.1f);
            Physics.NewFixture(
                b1,
                s1,
                100);
            s2 = Physics.NewCircleShape(0, 60f * 0.1f, 50.0f * 0.1f);
            b2 = Physics.NewBody(m_world, 0.0f, 60.0f * 0.1f, BodyType.Dynamic);
            Physics.NewFixture(
                b2,
                s2,
                100);
        }
    }
}
