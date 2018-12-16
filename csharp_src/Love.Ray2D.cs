
namespace Love
{
    public struct Ray2D
    {
        public Vector2 Original, Direction;

        public Ray2D(Vector2 origin, Vector2 direction)
        {
            this.Original = origin;
            this.Direction = direction;
        }
        
        public Ray2D(float originX, float originY, float directionX, float directionY)
        {
            this.Original = new Vector2(originX, originY);
            this.Direction = new Vector2(directionX, directionY);
        }


        const int NUM_DIMENSION = 2;
        const int QUADRANT_RIGHT = 0;
        const int QUADRANT_LEFT = 1;
        const int QUADRANT_MIDDLE = 3;

        /// <summary>
        /// Raycast for intersection of rectangle and ray, if any return true.
        /// <para> modify form this file : https://github.com/erich666/GraphicsGems/blob/master/gems/RayBox.c </para>
        /// <para> https://gamedev.stackexchange.com/questions/18436/most-efficient-aabb-vs-ray-collision-algorithms </para>
        /// <para> https://tavianator.com/fast-branchless-raybounding-box-intersections/ </para>
        /// </summary>
        /// <param name="rect">the input rect</param>
        /// <param name="ray">the ray to raycast</param>
        /// <param name="result">Intersection of rectangle and ray</param>
        /// <returns></returns>
        public static bool Intersects(Ray2D ray, RectangleF rect, out Vector2 result)
        {
            result = Vector2.Zero;
            float[] minB = { rect.Left, rect.Top };
            float[] maxB = { rect.Right, rect.Bottom };
            float[] origin = { ray.Original.X, ray.Original.Y };
            float[] dir = { ray.Direction.X, ray.Direction.Y };

            // 1. 找到候选边
            // 1. find candidate planes
            bool inside = true;
            int i;
            float[] maxT = { 0, 0 };
            float[] quadrant = { 0, 0 };
            float[] candidatePlane = { 0, 0 };

            // Find candidate planes; this loop can be avoided if
            // rays cast all from the eye(assume perpsective view)
            for (i = 0; i < NUM_DIMENSION; i++)
            {
                if (origin[i] < minB[i])
                {
                    quadrant[i] = QUADRANT_LEFT;
                    candidatePlane[i] = minB[i];
                    inside = false;
                }
                else if (origin[i] > maxB[i])
                {
                    quadrant[i] = QUADRANT_RIGHT;
                    candidatePlane[i] = maxB[i];
                    inside = false;
                }
                else
                {
                    quadrant[i] = QUADRANT_MIDDLE;
                }
            }

            // Ray origin inside bounding box
            if (inside)
            {
                result = ray.Original;
                return true;
            }

            // Calculate T distances to candidate planes
            for (i = 0; i < NUM_DIMENSION; i++)
            {
                if (quadrant[i] != QUADRANT_MIDDLE && dir[i] != 0f)
                    maxT[i] = (candidatePlane[i] - origin[i]) / dir[i];
                else
                    maxT[i] = -1f;
            }

            // Get largest of the maxT's for final choice of intersection
            int whichPlane = 0;
            // for (i = 1; i < NUM_DIMENSION; i++) only calc maxT[1]
            if (maxT[0] < maxT[1])
                whichPlane = 1;

            // Check final candidate actually inside box 
            if (maxT[whichPlane] < 0) return false;
            float[] coord = { 0, 0 };
            for (i = 0; i < NUM_DIMENSION; i++)
            {
                if (whichPlane != i)
                {
                    coord[i] = origin[i] + maxT[whichPlane] * dir[i];
                    if (coord[i] < minB[i] || coord[i] > maxB[i]) // Check final candidate actually inside box 
                        return false;
                }
                else // 这里把一个乘法和加法优化掉了…………
                {
                    coord[i] = candidatePlane[i];
                }
            }

            result.X = coord[0];
            result.Y = coord[1];
            return true;
        }

        /// <summary>
        /// Raycast for intersection of rectangle and ray, if any return true.
        /// </summary>
        /// <param name="rect">the rectangle to test</param>
        /// <param name="result">the intersection(if any)</param>
        /// <returns></returns>
        public bool Intersects(RectangleF rect, out Vector2 result)
        {
            return Intersects(this, rect, out result);
        }
    }
}