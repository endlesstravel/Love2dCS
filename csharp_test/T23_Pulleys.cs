using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Love;
namespace LovePhysicsTestBed
{
    class T23_Pulleys: Test
    {
        public override void ResetTranslation()
        {
            VScale = 10;
        }

        public override void Load()
        {
            Physics.SetMeter(1);

            float y = 16.0f;
            float L = 12.0f;
            float a = 1.0f;
            float b = 2.0f;

            Body ground;
            {
                ground = Physics.NewBody(m_world);

                EdgeShape edge = Physics.NewEdgeShape(-40.0f, 0.0f, 40.0f, 0.0f);
                CircleShape circle = Physics.NewCircleShape(2.0f);

                circle.SetPoint(-10.0f, y + b + L);
                Physics.NewFixture(ground, circle, 0);

                circle.SetPoint(10.0f, y + b + L);
                Physics.NewFixture(ground, circle, 0);
            }

            {
                PolygonShape shape = Physics.NewRectangleShape(a * 2, b * 2);

                Body body1 = Physics.NewBody(m_world, -10.0f, y, BodyType.Dynamic);
                Physics.NewFixture(body1, shape, 5.0f);

                Body body2 = Physics.NewBody(m_world, 10.0f, y, BodyType.Dynamic);
                Physics.NewFixture(body2, shape, 5.0f);


                Vector2 anchor1 = new Vector2(-10.0f, y + b);
                Vector2 anchor2 = new Vector2(10.0f, y + b);
                Vector2 groundAnchor1 = new Vector2(-10.0f, y + b + L);
                Vector2 groundAnchor2 = new Vector2(10.0f, y + b + L);

                PulleyJoint m_joint1 = Physics.NewPulleyJoint(body1, body2,
                    groundAnchor1, groundAnchor2, anchor1, anchor2, 1.5f);
            }
        }
    }
}
