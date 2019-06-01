using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Love;

namespace LovePhysicsTestBed
{
    class T19_Web: Test
    {
        const float scale = 10;

        DistanceJoint CreateJoint(Body a, Body b,
            Vector2 localAnchorA, Vector2 localAnchorB)
        {
            var jd = Physics.NewDistanceJoint(a, b,
                a.GetPosition().X + localAnchorA.X,
                a.GetPosition().Y + localAnchorA.Y,
                b.GetPosition().X + localAnchorB.X, 
                b.GetPosition().Y + localAnchorB.Y);
            jd.SetFrequency(2.0f);
            jd.SetDampingRatio(0);

            var p1 = a.GetWorldPoint(localAnchorA);
            var p2 = b.GetWorldPoint(localAnchorB);
            var d = p2 - p1;
            jd.SetLength(d.Length());
            return jd;
        }

        Body[] m_bodies = new Body[4];
        Joint[] m_joints = new Joint[8];
        public override void Load()
        {
            //Physics.SetMeter(1);
            Body ground;
            {
                ground = Physics.NewBody(m_world);
                Physics.NewFixture(ground, Physics.NewEdgeShape(-40.0f * scale, 0.0f, 40.0f * scale, 0.0f), 0);
            }
            {
                PolygonShape shape = Physics.NewRectangleShape(0.5f * 2 * scale, 0.5f * 2 * scale);

                m_bodies[0] = Physics.NewBody(m_world, -5.0f * scale, 5.0f * scale, BodyType.Dynamic);
                Physics.NewFixture(m_bodies[0], shape, 5.0f * scale);

                m_bodies[1] = Physics.NewBody(m_world, 5.0f * scale, 5.0f * scale, BodyType.Dynamic);
                Physics.NewFixture(m_bodies[1], shape, 5.0f * scale);

                m_bodies[2] = Physics.NewBody(m_world, 5.0f * scale, 15.0f * scale, BodyType.Dynamic);
                Physics.NewFixture(m_bodies[2], shape, 5.0f * scale);

                m_bodies[3] = Physics.NewBody(m_world, -5.0f * scale, 15.0f * scale, BodyType.Dynamic);
                Physics.NewFixture(m_bodies[3], shape, 5.0f * scale);


                m_joints[0] = CreateJoint(ground, m_bodies[0],
                    new Vector2(-10.0f * scale, 0.0f * scale), new Vector2(-0.5f * scale, -0.5f * scale));
                m_joints[1] = CreateJoint(ground, m_bodies[1],
                    new Vector2(10.0f * scale, 0.0f * scale), new Vector2(0.5f * scale, -0.5f * scale));
                m_joints[2] = CreateJoint(ground, m_bodies[2],
                    new Vector2(10.0f * scale, 20.0f * scale), new Vector2(0.5f * scale, 0.5f * scale));
                m_joints[3] = CreateJoint(ground, m_bodies[3],
                    new Vector2(-10.0f * scale, 20.0f * scale), new Vector2(-0.5f * scale, 0.5f * scale));

                m_joints[4] = CreateJoint(m_bodies[0], m_bodies[1],
                    new Vector2(0.5f * scale, 0.0f * scale), new Vector2(-0.5f * scale, 0.0f * scale));
                m_joints[5] = CreateJoint(m_bodies[1], m_bodies[2],
                    new Vector2(0.0f * scale, 0.5f * scale), new Vector2(0.0f * scale, -0.5f * scale));
                m_joints[6] = CreateJoint(m_bodies[2], m_bodies[3],
                    new Vector2(-0.5f * scale, 0.0f * scale), new Vector2(0.5f * scale, 0.0f * scale));
                m_joints[7] = CreateJoint(m_bodies[3], m_bodies[0],
                    new Vector2(0.0f * scale, -0.5f * scale), new Vector2(0.0f * scale, 0.5f * scale));

            }
        }
    }
}
