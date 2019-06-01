using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Love;

namespace LovePhysicsTestBed
{
    class T20_ApplyForce: Test
    {
        Body m_body;

        public override void Update(float dt)
        {
            base.Update(dt);

            if (Keyboard.IsDown(KeyConstant.W))
            {
                var f = m_body.GetWorldVector(0.0f, -200.0f);
                var p = m_body.GetWorldPoint(0.0f, 2.0f);
                m_body.ApplyForce(f.X, f.Y, p.X, p.Y);
            }
            if (Keyboard.IsDown(KeyConstant.A))
            {
                m_body.ApplyTorque(50.0f);
            }
            if (Keyboard.IsDown(KeyConstant.D))
            {
                m_body.ApplyTorque(-50.0f);
            }
        }


        public override void ResetTranslation()
        {
            VScale = 10;
        }

        const float k_restitution = 0.4f;
        public override void Load()
        {
            m_world.SetGravity(0, 0);

            Physics.SetMeter(1);
            Body ground;
            {
                ground = Physics.NewBody(m_world, 0.0f, 20.0f);

                Physics.NewFixture(ground,
                    Physics.NewEdgeShape(-20.0f, -20.0f, -20.0f, 20.0f), 0)
                    .SetRestitution(k_restitution);

                Physics.NewFixture(ground,
                    Physics.NewEdgeShape(20.0f, -20.0f, 20.0f, 20.0f), 0)
                    .SetRestitution(k_restitution);

                Physics.NewFixture(ground,
                    Physics.NewEdgeShape(-20.0f, 20.0f, 20.0f, 20.0f), 0)
                    .SetRestitution(k_restitution);

                Physics.NewFixture(ground,
                    Physics.NewEdgeShape(-20.0f, -20.0f, 20.0f, -20.0f), 0)
                    .SetRestitution(k_restitution);
            }

            {
                PolygonShape poly1 = Physics.NewPolygonShape(new Vector2[3]
                {
                    new Vector2(0.0f, 0.5f),
                    new Vector2(0.0f, -0.5f),
                    new Vector2(1.0f, 1.0f),
                });
                PolygonShape poly2 = Physics.NewPolygonShape(new Vector2[3]
                {
                    new Vector2(0.0f, 0.5f),
                    new Vector2(0.0f, -0.5f),
                    new Vector2(-1.0f, 1.0f),
                });

                Body body = Physics.NewBody(m_world, 0.0f, 2.0f, BodyType.Dynamic);
                body.SetAngle(Mathf.PI);
                body.SetSleepingAllowed(false);
                body.SetAngularDamping(2.0f);
                body.SetLinearDamping(0.5f);

                Physics.NewFixture(body, poly1, 4.0f);
                Physics.NewFixture(body, poly2, 2.0f);

                m_body = body;
            }

            {
                PolygonShape shape = Physics.NewRectangleShape(1, 1);

                for (int i = 0; i < 10; ++i)
                {
                    Body body = Physics.NewBody(m_world, 0.0f, 5.0f + 1.54f * i, BodyType.Dynamic);
                    var fd = Physics.NewFixture(body, shape, 1.0f);
                    fd.SetFriction(0.3f);

                    float gravity = 10.0f;
                    float I = body.GetInertia();
                    float mass = body.GetMass();

                    // For a circle: I = 0.5 * m * r * r ==> r = sqrt(2 * I / m)
                    float radius = Mathf.Sqrt(2.0f * I / mass);

                    var jd = Physics.NewFrictionJoint(ground, body, Vector2.Zero, Vector2.Zero, true);
                    jd.SetMaxForce(mass * gravity);
                    jd.SetMaxTorque(mass * radius * gravity);
                }
            }
        }
    }
}
