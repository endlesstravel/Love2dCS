using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Love;

namespace LovePhysicsTestBed
{
    class T25_Cantilever: Test
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

                Body prevBody = null;
                for (int i = 0; i < 8; ++i)
                {
                    Body body = Physics.NewBody(m_world, -14.5f + 1.0f * i, 5.0f, BodyType.Dynamic);
                    Physics.NewFixture(body, shape, 20.0f);
                    //Physics.NewWeldJoint(prevBody, body,
                    //    prevBody.GetWorldPoint(new Vector2(-15.0f + 1.0f * i, 5.0f)),
                    //    body.GetWorldPoint(new Vector2(-15.0f + 1.0f * i, 5.0f))
                    //    );
                    if (prevBody != null)
                    {
                        Physics.NewWeldJoint(prevBody, body,
                            prevBody.GetWorldPoint(new Vector2(0.5f, 0)),
                            body.GetWorldPoint(new Vector2(-0.5f, 0))
                            );
                    }
                    prevBody = body;
                }
            }
        }
    }
}
