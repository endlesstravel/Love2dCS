using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Love;

namespace LovePhysicsTestBed
{
    class TestBed: Scene
    {
        List<Test> list;
        Test currentTest = null;
        int index = 0;

        public Test LoadTests()
        {
            list = new List<Test>()
            {
                new T10_RayCast(),
                new T23_Pulleys(),
                new T21_Gears(),
                new T24_RopeJoint(),
                new T25_Cantilever(),
                new T26_Car(),
                new T22_MotorJoint(),
                new T17_ConveyorBelt(),
                new T19_Web(),
                new T30_AddPair(),
                new T20_ApplyForce(),
                new T02_HeavyOnLight(),
                new T15_Pinball(),
                new T05_BasicSliderCrank(),
                new T01_Tiles(),
                new T09_Tumbler(),
                //new T01_Tiles_FV(),
                //new T02_HeavyOnLight_FV(),
            };

            return list[(++index) % list.Count];
        }

        public TestBed()
        {
            index = -1;
            currentTest = LoadTests();
        }


        public void DrawTextText()
        {
            StringBuilder sb = new StringBuilder();
            int lineCount = 0;
            foreach (var test in list)
            {
                sb.Append(test == currentTest ? "---> " : "    ");
                sb.Append(test.GetType().Name);
                sb.AppendLine();
                lineCount++;
            }
            sb.AppendLine("-----------------------------------");
            sb.AppendLine("Press [N] to change test scene.");
            sb.AppendLine("Left mouse button drag object.");
            sb.AppendLine("Right mouse button move views.");
            sb.AppendLine("Middle mouse wheel scale views.");
            string text = sb.ToString();
            var tw = Graphics.GetFont().GetWidth(text) + 10;
            var x = Graphics.GetWidth() - tw;
            var y = 0;
            //var th = Graphics.GetFont().GetHeight() * lineCount;
            var th = Graphics.GetHeight();
            Graphics.SetColor(0.2f, 0.2f, 0.2f, 0.5f);
            Graphics.Rectangle(DrawMode.Fill, x, y, tw, th);

            Graphics.SetColor(Color.Wheat);
            Graphics.Print(text, x + 5, y + 5);
        }


        public override void Load()
        {
            list.ForEach(item =>
            {
                item.Load();
                item.ResetTranslation();
            });
        }

        public override void Draw()
        {
            if (currentTest != null)
            {
                currentTest.Draw();
            }
            Love.Misc.FPSGraph.Draw();
            DrawTextText();
        }

        public override void WheelMoved(int x, int y)
        {
            if (currentTest != null)
            {
                if (y > 0)
                    currentTest.VScale *= 1.1f;
                if (y < 0)
                    currentTest.VScale *= 0.9f;
            }
        }

        Vector2 startMousePoint;
        Vector2 startOffsetPoint;
        bool isMoved = false;

        public override void MousePressed(float x, float y, int button, bool isTouch)
        {
            if (button == Mouse.RightButton || button == Mouse.MiddleButton)
            {
                startMousePoint = new Vector2(x, y);
                if (currentTest != null)
                {
                    startOffsetPoint = currentTest.VOffset;
                }
                isMoved = true;
            }


            if (button == Mouse.LeftButton)
            {
                if (currentTest != null)
                {
                    currentTest.MouseLeftPressed();
                }
            }
        }
        public override void MouseReleased(float x, float y, int button, bool isTouch)
        {
            if (button == Mouse.RightButton || button == Mouse.MiddleButton)
            {
                isMoved = false;
            }
            if (button == Mouse.LeftButton)
            {
                if (currentTest != null)
                {
                    currentTest.MouseLeftReleasd();
                }
            }
        }

        public override void KeyPressed(KeyConstant key, Scancode scancode, bool isRepeat)
        {
            if (key == KeyConstant.N)
            {
                currentTest = LoadTests();
                currentTest.LoadWorld();
                currentTest.Load();
                currentTest.ResetTranslation();
            }
        }

        public override void Update(float dt)
        {
            if (isMoved)
            {
                Mouse.SetCursor(SystemCursor.SizeAll);
            }
            else
            {
                Mouse.SetCursor();
            }

            if (currentTest != null && isMoved)
            {
                currentTest.VOffset = startOffsetPoint + (Mouse.GetPosition() - startMousePoint);
            }
            if (currentTest != null)
            {
                currentTest.Update(dt);
            }
            if (currentTest != null)
            {
                currentTest.MouseMoved();
            }
            Love.Misc.FPSGraph.Update(dt);
            Love.Misc.FPSGraph.Position = new Vector2(
                0, Graphics.GetHeight() - Love.Misc.FPSGraph.Size.height);

            foreach (var joy in Joystick.GetJoysticks())
            {
                for (int i = 0; i < joy.GetButtonCount(); i++)
                {
                    if (joy.IsPressed(i))
                    {
                        Console.WriteLine(joy.GetGUID() + " " + i + " is pressed");
                    }
                    if (joy.IsReleased(i))
                    {
                        Console.WriteLine(joy.GetGUID() + " " + i + " IsReleased");
                    }
                }

                foreach (var gbtn in (GamepadButton[])System.Enum.GetValues(typeof(GamepadButton)))
                {
                    if (joy.IsGamepadPressed(gbtn))
                    {
                        Console.WriteLine(joy.GetGUID() + " " + gbtn + " ggg is pressed");
                    }
                    if (joy.IsGamepadReleased(gbtn))
                    {
                        Console.WriteLine(joy.GetGUID() + " " + gbtn + " ggg IsReleased");
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            Boot.Init(new BootConfig()
            {
                WindowResizable = true,
                //WindowVsync = false,
            });

            Boot.Run(new TestBed());
        }
    }

    abstract class Test
    {
        abstract public void Load();

        Vector2 GetMousePosOnWorld()
        {
            var mp = new Vector2(
                -(Graphics.GetWidth() / 2 - Mouse.GetPosition().x),
                Graphics.GetHeight() / 2 - Mouse.GetPosition().y);
            var offet = new Vector2(-VOffset.x / VScale,  VOffset.y / VScale);
            return  (new Vector2(mp.x, mp.y)) / VScale + offet;
        }

        MouseJoint m_mouseJoint;
        Vector2 m_mouseJointStartDragOffset;
        public void MouseLeftPressed()
        {
            if (m_mouseJoint == null)
            {
                Vector2 p = GetMousePosOnWorld();
                // create
                Vector2 d = new Vector2(0.001f, 0.001f);
                var lower = p - d;
                var upper = p + d;

                m_world.QueryBoundingBox(lower.x, lower.y, upper.x, upper.y, (Fixture pfixture) =>
                {
                    var body = pfixture.GetBody();
                    m_mouseJoint = Physics.NewMouseJoint(body, p);
                    m_mouseJoint.SetMaxForce(1000.0f * body.GetMass());
                    m_mouseJointStartDragOffset = body.GetLocalPoint(p);
                    return false;
                });
            }
        }

        public void MouseMoved()
        {
            if (m_mouseJoint != null)
            {
                Vector2 pos = GetMousePosOnWorld();
                m_mouseJoint.SetTarget(pos);
            }
        }

        public void MouseLeftReleasd()
        {
            if (m_mouseJoint != null)
            {
                m_mouseJoint.Destroy();
                m_mouseJoint = null;
            }
        }

        public float VScale = 1;
        public Vector2 VOffset = new Vector2();

        public virtual void ResetTranslation()
        {
            VScale = 1;
            VOffset = new Vector2();
        }

        float GetVScal()
        {
            return VScale;
        }
        public virtual void Draw()
        {
            Graphics.Push();
            Graphics.SetBackgroundColor(Color.Gray);
            Graphics.Scale(VScale, -VScale);
            Graphics.Translate((Graphics.GetWidth() / 2 + VOffset.x) / VScale, 
                (Graphics.GetHeight() / 2 + VOffset.y) / -VScale);
            Graphics.SetLineWidth(1 / VScale);
            Graphics.SetPointSize(2);
            DrawGrid();
            DrawWorld();

            // draw mouse
            if (m_mouseJoint != null)
            {
                var b = m_mouseJoint.GetBodies()[0];
                var p = b.GetWorldPoint(m_mouseJointStartDragOffset);

                Graphics.SetColor(Color.YellowGreen);
                Graphics.Line(GetMousePosOnWorld(), p);
                Graphics.SetColor(Color.Red);
                Graphics.Points(GetMousePosOnWorld(), p);
            }
            Graphics.SetLineWidth(1);
            Graphics.SetPointSize(1);
            Graphics.Pop();
        }

        public virtual void DrawGrid()
        {
            Graphics.SetColor(Color.Black);
            for (int y = -100; y <= 100; y+=10)
            {
                Graphics.Line(-100, y, 100, y);
            }
            for (int x = -100; x <= 100; x+=10)
            {
                Graphics.Line(x, -100, x, 100);
            }
            Graphics.Circle(DrawMode.Fill, 0, 0, 0.3f, 20);
        }

        public virtual void DrawWorld()
        {
            m_world.DebugDraw();
            //DebugWorldDraw.Draw(m_world);
        }

        public virtual void Update(float dt)
        {
            //if (Keyboard.IsPressed(KeyConstant.Space))
                m_world.Update(dt);
        }

        public static float RandomFloat(float min, float max)
        {
            return Mathf.Random(min, max);
        }

        public void LoadWorld()
        {
            m_world = Physics.NewWorld(0.0f, -100.0f);
            m_groundBody = Physics.NewBody(m_world);
            m_world.SetCallbacks(null, null, this.PreSolve, null);
            //m_world.SetCallbacks(null, null, null, null);
        }

        public virtual void DestoryWorld()
        {
            m_world.Destroy();
        }

        public Test()
        {
            LoadWorld();
        }

        public void PreSolve(Fixture fixtureA, Fixture fixtureB, Contact contact)
        {
            Graphics.SetPointSize(3);
            Graphics.SetColor(Color.GreenYellow);
            Graphics.Points(contact.GetPositions());
        }

        public World m_world;
        protected Body m_bomb;
        protected Body m_groundBody;

        int m_textLine = 30;
        int m_pointCount = 0;
        bool m_bombSpawning;
        int m_stepCount;

        float hz = 60.0f;
		int velocityIterations = 8;
		int positionIterations = 3;

		bool drawShapes = true;
		bool drawJoints = true;
		bool drawAABBs = false;
		bool drawContactPoints = false;
		bool drawContactNormals = false;
		bool drawContactImpulse = false;
		bool drawFrictionImpulse = false;
		bool drawCOMs = false;
		bool drawStats = false;
		bool drawProfile = false;
		bool enableWarmStarting = true;
		bool enableContinuous = true;
		bool enableSubStepping = false;
		bool enableSleep = true;
		bool pause = false;
		bool singleStep = false;
    }

    class EmptyTest: Test
    {
        public override void Load()
        {
        }

        public override void DrawWorld()
        {
        }

        public override void Update(float dt)
        {
            base.Update(dt);
        }
    }
}
