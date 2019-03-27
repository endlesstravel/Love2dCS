using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Love;
namespace LovePhysicsTestBed
{
    class T21_Gears: Test
    {
        public override void ResetTranslation()
        {
            VScale = 10;
        }

        public override void Load()
        {
            Physics.SetMeter(1);

            Body ground;
            {
                ground = Physics.NewBody(m_world);
                Physics.NewEdgeShape(50.0f, 0.0f, -50.0f, 0.0f);
            }

            {
                CircleShape circle1 = Physics.NewCircleShape(1.0f);
                PolygonShape box = Physics.NewRectangleShape(1f, 10f);
                CircleShape circle2 = Physics.NewCircleShape(2.0f);

                var body1 = Physics.NewBody(m_world, 10.0f, 9.0f, BodyType.Static);
                Physics.NewFixture(body1, circle1, 5.0f);

                var body2 = Physics.NewBody(m_world, 10.0f, 8.0f, BodyType.Dynamic);
                Physics.NewFixture(body2, box, 5.0f);

                var body3 = Physics.NewBody(m_world, 10.0f, 6.0f, BodyType.Dynamic);
                Physics.NewFixture(body3, circle2, 5.0f);

                var joint1 = Physics.NewRevoluteJoint(body2, body1, body1.GetPosition());
                var joint2 = Physics.NewRevoluteJoint(body2, body3, body3.GetPosition());
                var gj = Physics.NewGearJoint(joint1, joint2, circle2.GetRadius() / circle1.GetRadius());
            }

            {
                CircleShape circle1 = Physics.NewCircleShape(1.0f);
                CircleShape circle2 = Physics.NewCircleShape(2.0f);
                PolygonShape box = Physics.NewRectangleShape(1, 10);
                Body body1 = Physics.NewBody(m_world, -3.0f, 12.0f, BodyType.Dynamic);
                Physics.NewFixture(body1, circle1, 5.0f);

                RevoluteJoint m_joint1 = Physics.NewRevoluteJoint(ground, body1,
                    body1.GetWorldCenter(),
                    body1.GetWorldCenter(),
                    false,
                    body1.GetAngle() - ground.GetAngle());

                Body body2 = Physics.NewBody(m_world, 0.0f, 12.0f, BodyType.Dynamic);
                Physics.NewFixture(body2, circle2, 5.0f);

                RevoluteJoint m_joint2 = Physics.NewRevoluteJoint(ground, body2, body2.GetPosition());

                Body body3 = Physics.NewBody(m_world, 2.5f, 12.0f, BodyType.Dynamic);
                Physics.NewFixture(body3, box, 5.0f);

                PrismaticJoint m_joint3 = Physics.NewPrismaticJoint(ground,
                    body3, Vector2.Zero, Vector2.Zero, new Vector2(0.0f, 1.0f));

                m_joint3.SetLimits(-0.5f, 0.5f);
                m_joint3.SetLimitsEnabled(true);

                GearJoint m_joint4 = Physics.NewGearJoint(m_joint1, m_joint2,
                    circle2.GetRadius() / circle1.GetRadius());

                GearJoint m_joint5 = Physics.NewGearJoint(m_joint2, m_joint3, -1.0f / circle2.GetRadius());
            }
        }
    }
}
