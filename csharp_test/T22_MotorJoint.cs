using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Love;
namespace LovePhysicsTestBed
{
    class T22_MotorJoint : Test
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
                EdgeShape shape = Physics.NewEdgeShape(-20.0f, 0.0f, 20.0f, 0.0f);
                Physics.NewFixture(ground, shape, 0);
            }

            // Define motorized body
            {
                Body body = Physics.NewBody(m_world, 0.0f, 8.0f, BodyType.Dynamic);
                PolygonShape shape = Physics.NewRectangleShape(4, 1);
                Physics.NewFixture(body, shape, 2.0f).SetFriction(0.6f);
                var m_joint = Physics.NewMotorJoint(ground, body);
                m_joint.SetMaxForce(1000.0f);
                m_joint.SetMaxTorque(1000.0f);

            }
        }
    }
}
