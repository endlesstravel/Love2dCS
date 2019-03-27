using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Love
{
    public class FPSCounter
    {
        static List<double> frameInOneSeconed = new List<double>();

        internal static void Step()
        {
            double now = Timer.GetSystemTime();
            frameInOneSeconed = frameInOneSeconed.Where(item => item > (now - 1f)).ToList();
            frameInOneSeconed.Add(now);
        }

        /// <summary>
        /// Get the amount of frame from last one seconds.
        /// </summary>
        /// <returns></returns>
        public static int GetFPS()
        {
            return frameInOneSeconed.Count;
        }
    }
}
