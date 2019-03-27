using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Love;

namespace LovePhysicsTestBed
{
    class T17_ConveyorBelt: Test
    {
        Fixture m_platform;

        public override void ResetTranslation()
        {
            VScale = 20;
        }

        public override void Load()
        {
            // Ground
            {
                Physics.NewFixture(
                    Physics.NewBody(m_world),
                    Physics.NewEdgeShape(new Vector2(-20.0f, 0.0f), new Vector2(20.0f, 0.0f)),
                    0
                    );
            }

            // Platform
            {
                var body = Physics.NewBody(m_world, -5.0f, 5.0f);
                var shape = Physics.NewRectangleShape(10.0f * 2, 0.5f * 2);
                m_platform = Physics.NewFixture(body, shape, 0.8f);
            }

            // Boxes
            for (int i = 0; i < 5; ++i)
            {
                var body = Physics.NewBody(m_world, -10.0f + 2.0f * i, 7.0f, BodyType.Dynamic);
                var shape = Physics.NewRectangleShape(0.5f * 2, 0.5f * 2);
                Physics.NewFixture(body, shape, 20.0f);
            }

            m_world.SetCallbacks(null, null, MyPreSolve, null);
        }

        void MyPreSolve(Fixture fixtureA, Fixture fixtureB, Contact contact)
        {
            if (fixtureA.Equals(m_platform))
            {
                contact.SetTangentSpeed(5.0f);
            }

            if (fixtureB.Equals(m_platform))
            {
                contact.SetTangentSpeed(-5.0f);
            }
        }
    }
}
