using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Love;
namespace LovePhysicsTestBed
{
    class T24_RopeJoint: Test
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
                EdgeShape shape = Physics.NewEdgeShape(-40.0f, 0.0f, 40.0f, 0.0f);
                Physics.NewFixture(ground, shape, 0);
            }

            {
                PolygonShape shape = Physics.NewRectangleShape(1, 0.25f);
                const int N = 10;
                const float y = 15.0f;

                Body prevBody = ground;
                for (int i = 0; i < N; ++i)
                {
                    Body body = Physics.NewBody(m_world, 0.5f + 1.0f * i, y, BodyType.Dynamic);
                    PolygonShape boxShape = shape;
                    if (i == N - 1)
                    {
                        boxShape = Physics.NewRectangleShape(2, 2);
                        body.SetPosition(1.0f * i, y);
                        var fixture = Physics.NewFixture(body, boxShape, 100.0f);
                        fixture.SetFriction(0.2f);
                        fixture.SetCategory(0x0002);
                        fixture.SetMask(0xFFFF & ~0x0002);
                    }
                    else
                    {
                        var fixture = Physics.NewFixture(body, boxShape, 20.0f);
                        fixture.SetFriction(0.2f);
                        fixture.SetCategory(0x0001);
                        fixture.SetMask(0xFFFF & ~0x0002);

                    }

                    RevoluteJoint jd = Physics.NewRevoluteJoint(prevBody, body,
                        new Vector2(i, y));

                    prevBody = body;
                }


                float extraLength = 0.01f;
                var rj = Physics.NewRopeJoint(ground, prevBody, 
                    new Vector2(0.0f, y),
                    prevBody.GetWorldPoint(Vector2.Zero), N - 1.0f + extraLength);
            }
        }
    }
}
