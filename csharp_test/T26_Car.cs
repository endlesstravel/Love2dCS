using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Love;

namespace LovePhysicsTestBed
{
    class T26_Car: Test
    {
        public override void ResetTranslation()
        {
            VScale = 10;
        }

        public override void Load()
        {

            Physics.SetMeter(1);

            Body ground = Physics.NewBody(m_world);
            Physics.NewFixture(ground, Physics.NewEdgeShape(-100, 0, 100, 0), 0);

            PolygonShape chassis = Physics.NewPolygonShape(
                new Vector2(-1.5f, -0.5f),
                new Vector2(1.5f, -0.5f),
                new Vector2(1.5f, 0.0f),
                new Vector2(0.0f, 0.9f),
                new Vector2(-1.15f, 0.9f),
                new Vector2(-1.5f, 0.2f));

            CircleShape circle = Physics.NewCircleShape(0.4f);

            var m_car = Physics.NewBody(m_world, 0.0f, 1.0f, BodyType.Dynamic);
            Physics.NewFixture(m_car, chassis, 1.0f);

            var m_wheel1 = Physics.NewBody(m_world, -1.0f, 0.35f, BodyType.Dynamic);
            Physics.NewFixture(m_wheel1, circle, 1.0f).SetFriction(0.9f);

            var m_wheel2 = Physics.NewBody(m_world, 1.0f, 0.4f, BodyType.Dynamic);
            Physics.NewFixture(m_wheel2, circle, 1.0f).SetFriction(0.9f);


            m_hz = 4.0f;
            m_zeta = 0.7f;
            m_speed = 50.0f;

            var axis = new Vector2(0.0f, 1.0f);
            m_spring1 = Physics.NewWheelJoint(
                m_car, 
                m_wheel1,
                m_wheel1.GetPosition(),
                m_wheel1.GetPosition(),
                axis
                );
            m_spring1.SetMotorSpeed(0);
            m_spring1.SetMaxMotorTorque(20.0f);
            m_spring1.SetMotorEnabled(true);
            m_spring1.SetSpringFrequency(m_hz);
            m_spring1.SetSpringDampingRatio(m_zeta);

            m_spring2 = Physics.NewWheelJoint(m_car, 
                m_wheel2,
                m_wheel2.GetPosition(),
                m_wheel2.GetPosition(),
                axis
                );

            m_spring2.SetMotorSpeed(0);
            m_spring2.SetMaxMotorTorque(10.0f);
            m_spring2.SetMotorEnabled(false);
            m_spring2.SetSpringFrequency(m_hz);
            m_spring2.SetSpringDampingRatio(m_zeta);
        }

        WheelJoint m_spring2, m_spring1;

        float m_hz = 4.0f;
		float m_zeta = 0.7f;
		float m_speed = 50.0f;

        public override void Update(float dt)
        {
            base.Update(dt);

            if (Keyboard.IsPressed(KeyConstant.A))
            {
                m_spring1.SetMotorSpeed(m_speed);
            }

            if (Keyboard.IsPressed(KeyConstant.S))
            {
                m_spring1.SetMotorSpeed(0);
            }

            if (Keyboard.IsPressed(KeyConstant.D))
            {
                m_spring1.SetMotorSpeed(-m_speed);
            }

            if (Keyboard.IsPressed(KeyConstant.Q))
            {
                m_hz = Mathf.Max(0.0f, m_hz - 1.0f);
                m_spring1.SetSpringFrequency(m_hz);
                m_spring2.SetSpringFrequency(m_hz);
            }
        }
    }
}
