using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Love;

namespace LovePhysicsTestBed
{
    class T05_BasicSliderCrank: Test
    {
        const float scale = 10;
        public override void ResetTranslation()
        {
            VScale = 10f / scale;
        }

        public override void Load()
        {
            Body ground = Physics.NewBody(m_world, 0.0f, 17.0f * scale);
            {
                Body prevBody = ground;

                // Define crank.
                {
                    PolygonShape shape = Physics.NewRectangleShape(4.0f * 2 * scale, 1.0f * 2 * scale);
                    var body = Physics.NewBody(m_world, -8.0f * scale, 20.0f * scale, BodyType.Dynamic);
                    Physics.NewRevoluteJoint(prevBody, body, new Vector2(-12.0f, 20.0f) * scale);
                    Physics.NewFixture(body, shape, 2.0f * scale);
                    prevBody = body;
                }

                // Define connecting rod
                {
                    PolygonShape shape = Physics.NewRectangleShape(8.0f * 2 * scale, 1.0f * 2 * scale);
                    Body body = Physics.NewBody(m_world, 4.0f * scale, 20.0f * scale, BodyType.Dynamic);
                    Physics.NewFixture(body, shape, 2.0f * scale);
                    Physics.NewRevoluteJoint(prevBody, body, new Vector2(-4.0f, 20.0f) * scale);
                    prevBody = body;
                }

                // Define piston
                {
                    PolygonShape shape = Physics.NewRectangleShape(3.0f * 2 * scale, 3.0f * 2 * scale);
                    Body body = Physics.NewBody(m_world, 12.0f * scale, 20.0f * scale, BodyType.Dynamic);
                    body.SetFixedRotation(true);
                    Physics.NewFixture(body, shape, 2.0f * scale);
                    Physics.NewRevoluteJoint(prevBody, body, new Vector2(12.0f, 20.0f) * scale);

                    //Physics.NewPrismaticJoint(ground, body,
                    //    new Vector2(12.0f, 17.0f) * scale,
                    //    new Vector2(12.0f, 17.0f) * scale,
                    //    new Vector2(1.0f, 0.0f));
                    Physics.NewPrismaticJoint(ground, body,
                        new Vector2(12.0f, 17.0f) * scale,
                        new Vector2(12.0f, 17.0f) * scale,
                        new Vector2(-1.0f, 0.0f)
                        );
                }
            }
        }
    }
}
