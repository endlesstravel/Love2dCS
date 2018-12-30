
using System.Collections.Generic;
using System.Linq;

namespace Love.Misc.FPSGraph
{
    public class FPSGraph
    {
        const int fontSize = 15;
        static readonly Font FPSGraph_FONT = Graphics.NewFont(fontSize);

		RectangleF m_rect; // position of the graph
		float m_inverval;
		bool m_draggable;
		readonly Queue<float> m_vals = new Queue<float>();
		float m_passedTime = 0;
		string m_label = "graph";

        private FPSGraph() {}

        public static FPSGraph CreateGraph(float x, float y, float width = 200, float height = 90, float interval = 0.5f, bool draggable = true)
        {
            FPSGraph fpsGraph = new FPSGraph();
            int wi = Mathf.FloorToInt(width / 2);
            for (int i = 0; i < wi; i++)
                fpsGraph.m_vals.Enqueue(0);

            fpsGraph.m_rect.x = x;
            fpsGraph.m_rect.y = y;
            fpsGraph.m_rect.Width = width;
            fpsGraph.m_rect.Height = height;
            fpsGraph.m_inverval = Mathf.Max(interval, 0.05f);
            fpsGraph.m_draggable = draggable;

            return fpsGraph;
        }

        float m_dx = 0;
        float m_dy = 0;
        bool m_lastIsDown = false;
        public void UpdateMouseDrag()
        {
            // code for draggable graphs
            if (m_draggable)
            {
                // get mouse position
                var mouseX = Mouse.GetX();
                var mouseY = Mouse.GetY();

                if (m_rect.Contains(Mouse.GetPosition()) || this.m_lastIsDown)
                {
                    if (Mouse.IsDown(1))
                    {
                        this.m_lastIsDown = true;
                        this.m_rect.X = mouseX - this.m_dx;
                        this.m_rect.Y = mouseY - this.m_dy;
                    }
                    else
                    {
                        this.m_lastIsDown = false;
                        this.m_dx = mouseX - this.m_rect.X;
                        this.m_dy = mouseY - this.m_rect.Y;
                    }
                }
            }
        }

        public void UpdateGraph(float val, string label, float dt)
        {
            UpdateMouseDrag();

            // update the current time of the graph
            m_passedTime += dt;

            // when current time is bigger than the delay
            while(this.m_passedTime >= this.m_inverval)
            {
                // subtract current time by delay
                this.m_passedTime = this.m_passedTime - this.m_inverval;

                // add new values to the graph while removing the first
                m_vals.Dequeue();
                m_vals.Enqueue(val);
                this.m_label = label;
            }
        } 

        public void UpdateFPS(float dt)
        {
            var fps = 0.75f * 1 / dt + 0.25f * Timer.GetFPS();
            UpdateGraph(fps, (Mathf.Floor(fps*10)/10).ToString("00.0"), dt);
        }

        /*

        // public void UpdateMem(float dt)
        // {
        //     var fps = 0.75f * 1 / dt + 0.25f * Timer.GetFPS();
	    //     UpdateGraph(graph, mem, "Memory (KB): " .. math.floor(mem*10)/10, dt)
        // }
        

        */

        public void DrawGraph()
        {
            Graphics.SetColor(0, 0,0, 0.6f);
            Graphics.Rectangle(DrawMode.Fill, m_rect);

            Graphics.SetColor(Color.White);
            // loop through all of the graphs
            var maxVal = m_vals.Max();
            var minVal = 0;
            var lastValue = m_vals.Peek();
            float stepX = m_rect.width / m_vals.Count;
            int index = 0;

            Vector2 lastPoint = new Vector2(m_rect.Left, m_rect.Bottom - m_rect.height * (lastValue / (maxVal - minVal)));

            foreach (var v in m_vals)
            {
                Vector2 currentPoint = new Vector2(index * stepX + m_rect.x, m_rect.Bottom - m_rect.height * (v / (maxVal - minVal)));
                Graphics.Line(lastPoint, currentPoint);

                lastValue = v;
                index++;
                lastPoint = currentPoint;
            }

            Graphics.SetFont(FPSGraph_FONT);
            Graphics.Print("MAX:" + maxVal.ToString("00.0") + "\nFPS:" + m_label, m_rect.x, m_rect.Top);
        }


        #region static feild
        readonly static FPSGraph m_Defalt = FPSGraph.CreateGraph(0, 0);

        public static void Config(float x, float y, float width = 200, float height = 90, float interval = 0.5f, bool draggable = true)
        {
            m_Defalt.m_rect.x = x;
            m_Defalt.m_rect.y = y;
            m_Defalt.m_rect.Width = width;
            m_Defalt.m_rect.Height = height;
            m_Defalt.m_inverval = Mathf.Max(interval, 0.05f);
            m_Defalt.m_draggable = draggable;
        }

        /// <summary>
        /// set or get graph position
        /// </summary>
        public static Vector2 Position
        {
            set { m_Defalt.m_rect.Location = value; }
            get { return m_Defalt.m_rect.Location; }
        }

        /// <summary>
        /// set or get graph size
        /// </summary>
        public static SizeF Size
        {
            set { m_Defalt.m_rect.Size = value; }
            get { return m_Defalt.m_rect.Size; }
        }

        /// <summary>
        /// set or get graph rect
        /// </summary>
        public static RectangleF Rect
        {
            set { m_Defalt.m_rect = value; }
            get { return m_Defalt.m_rect; }
        }

        /// <summary>
        /// update graph
        /// </summary>
        /// <param name="dt"></param>
        public static void Update(float dt)
        {
            m_Defalt.UpdateFPS(dt);
        }

        /// <summary>
        /// draw graph
        /// </summary>
        public static void Draw()
        {
            m_Defalt.DrawGraph();
        }
        #endregion
    }
}