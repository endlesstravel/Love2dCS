using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Love;

namespace LovePhysicsTestBed
{
    class T15_Pinball: Test
    {
        RevoluteJoint lrj, rrj;

        public override void ResetTranslation()
        {
            VScale = 20;
        }

        public override void Update(float dt)
        {
            //if (Keyboard.IsPressed(KeyConstant.Space))
            base.Update(dt);

            if (Keyboard.IsDown(KeyConstant.X))
            {
                lrj.SetMotorSpeed(20.0f);
                rrj.SetMotorSpeed(-20.0f);
            }
            else
            {
                lrj.SetMotorSpeed(-10.0f);
                rrj.SetMotorSpeed(10.0f);
            }

        }

        public override void Load()
        {
            // Ground body
            Body ground;
            {
                ground = Physics.NewBody(m_world);

                // outline
                Physics.NewFixture(ground,
                    Physics.NewChainShape(true, new Vector2[]
                    {
                        new Vector2(0.0f, -2.0f),
                        new Vector2(8.0f, 6.0f),
                        new Vector2(8.0f, 20.0f),
                        new Vector2(-8.0f, 20.0f),
                        new Vector2(-8.0f, 6.0f),
                    }), 0);
            }

            // Flippers
            {
                var p1 = new Vector2(-2.0f, 0.0f);
                var p2 = new Vector2(2.0f, 0.0f);


                Body leftFlipper = Physics.NewBody(m_world, p1.X, p1.Y, BodyType.Dynamic);
                Body rightFlipper = Physics.NewBody(m_world, p2.X, p2.Y, BodyType.Dynamic);

                var box = Physics.NewRectangleShape(1.75f * 2, 0.1f * 2);
                Physics.NewFixture(leftFlipper, box, 1.0f);
                Physics.NewFixture(rightFlipper, box, 1.0f);

                lrj = Physics.NewRevoluteJoint(ground, leftFlipper, p1);
                lrj.SetMotorSpeed(0);
                lrj.SetMotorEnabled(true);
                lrj.SetLimitsEnabled(true);
                lrj.SetMaxMotorTorque(1000.0f);
                lrj.SetLimits(-30.0f * Mathf.PI / 180.0f, 5.0f * Mathf.PI / 180.0f);

                rrj = Physics.NewRevoluteJoint(ground, rightFlipper, p2);
                rrj.SetMotorSpeed(0);
                rrj.SetMotorEnabled(true);
                rrj.SetLimitsEnabled(true);
                rrj.SetMaxMotorTorque(1000.0f);
                rrj.SetLimits(-5.0f * Mathf.PI / 180.0f, 30.0f * Mathf.PI / 180.0f);
            }

            // Circle character
            {
                var body = Physics.NewBody(m_world, 1.0f, 15.0f, BodyType.Dynamic);
                body.SetBullet(true);
                Physics.NewFixture(body, Physics.NewCircleShape(0.2f), 1.0f);
            }

        }
    }
}
