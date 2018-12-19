using System;
using System.Collections.Generic;

namespace Love.Misc.QuadTree
{
    /// <summary>
    /// 一个矩形区域的象限划分：
    /// 0---------------------------> x
    /// |                   |
    /// |        leftTop(0) | rightTopNode(1)
    /// |  -----------------|---------
    /// | leftBottomNode(2) | RightBottomNode(3)
    /// |                   |
    /// | 
    /// \/ y
    /// 以下对该象限类型的枚举
    /// </summary>
    enum QuadrantEnum
    {
        leftTop = 0,
        rightTop = 1,
        leftBottom = 2,
        RightBottom = 3
    };

    public class Leaf
    {
        public RectangleF Zone { get; }

        public Leaf(float x, float y, float width, float height)
        {
            Zone = new RectangleF(x, y, width, height);
        }
        public Leaf(RectangleF rect)
        {
            Zone = rect;
        }
    }

    public struct RayCastResult
    {
        public Vector2 Intersection;
        public Leaf Leaf;

        public RayCastResult(Vector2 intersection, Leaf leaf)
        {
            Intersection = intersection;
            Leaf = leaf;
        }
    }

    //internal static class Collision
    //{
    //    //public static void RectangleCollision(RectangleF moved, RectangleF target, Vector2 targetPoint)
    //    //{
    //    //    bool overLaps = false;
    //    //    var diffX = moved.x - target.x - target.width;
    //    //    var diffY = moved.y - target.y - target.height;
    //    //    var diffW = moved.width + target.width;
    //    //    var diffH = moved.height + target.height;

    //    //    overLaps = !(diffX < );

    //    //    RectangleF.Intersect()
    //    //}


    //    public class RectSegmentIntersection
    //    {
    //        public readonly RectangleF originalRect;
    //        public readonly Segment orginalSegment;
    //        public readonly float ti1, ti2;
    //        public readonly Vector2 n1, n2;

    //        public RectSegmentIntersection(RectangleF originalRect, Segment orginalSegment, float ti1, float ti2, Vector2 n1, Vector2 n2)
    //        {
    //            this.originalRect = originalRect;
    //            this.orginalSegment = orginalSegment;
    //            this.ti1 = ti1;
    //            this.ti2 = ti2;
    //            this.n1 = n1;
    //            this.n2 = n2;
    //        }

    //        public Segment GetIntersectionSegment()
    //        {
    //            var vec = orginalSegment.P2 - orginalSegment.P1;
    //            var p1 = orginalSegment.P1 + ti1 * vec;
    //            var p2 = orginalSegment.P1 + ti2 * vec;
    //            return Segment.Create(p1, p2);
    //        }
    //    }


    //    /// <summary>
    //    /// This is a generalized implementation of the liang-barsky algorithm, which also returns
    //    /// the normals of the sides where the segment intersects.
    //    /// Returns null if the segment never touches the rect
    //    /// Notice that normals are only guaranteed to be accurate when initially ti1, ti2 == -math.huge, math.huge
    //    /// https://blog.csdn.net/soulmeetliang/article/details/79185603
    //    /// </summary>
    //    public RectSegmentIntersection GetSegmentIntersectionIndices(RectangleF rect, float x1, float y1, float x2, float y2, float ti1 = 0, float ti2 = 1)
    //    {
    //        float nx, ny; // side noarmal 当前矩形边的法线
    //        var dx = x2 - x1;
    //        var dy = y2 - y1;
    //        float p, q;  // 当凸多边形恰为矩形，且矩形的边平行于坐标轴时，每个边的法向量仅有一个非零分量，所以法向量与任意矢量的内积，等于该向量的相应 x 或 y 分量。

    //        for (int side = 1; side <= 4; side++)
    //        {
    //            if (side == 1) // left
    //            {
    //                nx = -1;
    //                ny = 0;
    //                p = segment.P1.X - segment.P2.X;
    //                q = segment.P1.X - Left;
    //            }
    //            else if (side == 2) // right
    //            {
    //                nx = 1;
    //                ny = 0;
    //                p = segment.P2.X - segment.P1.X;
    //                q = Right - segment.P1.X;
    //            }
    //            else if (side == 3) // top
    //            {
    //                nx = 0;
    //                ny = -1;
    //                p = segment.P1.Y - segment.P2.Y;
    //                q = segment.P1.Y - Top;
    //            }
    //            else // if (side == 4) // bottom 
    //            {
    //                nx = 0;
    //                ny = 1;
    //                p = segment.P2.Y - segment.P1.Y;
    //                q = Bottom - segment.P1.Y;
    //            }
    //            // q < 0, P(t) 在多边形外侧
    //            // 
    //            if (p == 0)
    //            {
    //                if (q <= 0) return null;
    //            }
    //            else
    //            {
    //                float t = q / p; // t [ti1, ti2] <-- [0, 1] 线段上的 P(t)
    //                if (p < 0) // 此点在多边形内部
    //                {
    //                    if (t > ti2) return null;
    //                    else if (t > ti1)
    //                    {
    //                        ti1 = t;
    //                        n1 = new Vector2(nx, ny);
    //                    }
    //                }
    //                else // if (p > 0) // 此点在多边形外部
    //                {
    //                    if (t < ti1) return null;
    //                    else if (t < ti2)
    //                    {
    //                        ti2 = t;
    //                        n2 = new Vector2(nx, ny);
    //                    }
    //                }
    //            }
    //        }
    //        return new RectSegmentIntersection(this, segment, ti1, ti2, n1, n2);
    //    }
    //}

    class QuadTreeNode
    {
        internal readonly HashSet<Leaf> managedItems = new HashSet<Leaf>();
        internal readonly QuadTreeNode[] childern = new QuadTreeNode[4];
        private QuadTreeNode leftTopNode { get { return childern[0]; } }
        private QuadTreeNode rightTopNode { get { return childern[1]; } }
        private QuadTreeNode leftBottomNode { get { return childern[2]; } }
        private QuadTreeNode rightBottomNode { get { return childern[3]; } }
        private RectangleF m_zone;

        internal QuadTreeNode(RectangleF zone)
        {
            this.m_zone = zone;
        }

        public RectangleF Zone
        {
            get
            {
                return this.m_zone;
            }
        }

        public IEnumerable<Leaf> ManagedItems
        {
            get { return this.managedItems; }
        }

        RectangleF[] GetSubSpaceRect()
        {
            var middleW = m_zone.width / 2;
            var middleH = m_zone.height / 2;
            var middleX = m_zone.X + middleW;
            var middleY = m_zone.Y + middleH;
            return new RectangleF[] {
                new RectangleF(m_zone.Left, m_zone.Top, middleW, middleH),
                new RectangleF(middleX, m_zone.Top, middleW, middleH),
                new RectangleF(m_zone.Left, middleY, middleW, middleH),
                new RectangleF(middleX, middleY, middleW, middleH),
            };
        }

        /// <summary>
        /// 尝试插入 item 到此节点或者其子节点中，当 item 大于此区域插入失败
        /// </summary>
        /// <param name="item"></param>
        internal bool Insert(Leaf item, float MIN_UNIT)
        {
            // 当item不被此节点包含时插入失败
            if (m_zone.Contains(item.Zone) == false)
            {
                return false;
            }

            var subSpace = GetSubSpaceRect();

            // 防止子节点过小
            if (m_zone.Width > MIN_UNIT && m_zone.Height > MIN_UNIT)
            {
                // 优先插入子节点
                for (int i = 0; i < 4; i++)
                {
                    if (childern[i] == null)
                    {
                        if (subSpace[i].Contains(item.Zone))
                            childern[i] = new QuadTreeNode(subSpace[i]);
                    }

                    if (childern[i] != null && childern[i].Insert(item, MIN_UNIT))
                    {
                        return true;
                    }
                }
            }

            // 那么就是一个跨越子节点的 item，插入自己的节点
            managedItems.Add(item);
            return true;
        }

        /// <summary>
        /// 当此节点不在所给 RectangleF 时返回 null
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        internal LinkedList<Leaf> QueryArea(RectangleF rect)
        {
            LinkedList<Leaf> outList = new LinkedList<Leaf>();
            Queue<QuadTreeNode> queue = new Queue<QuadTreeNode>();
            queue.Enqueue(this);

            while (queue.Count != 0)
            {
                var leaf = queue.Dequeue();

                // 当此节点 Zone 与所给 RectangleF 不相交时返回 null
                if (rect.IntersectsWith(leaf.m_zone) == false)
                    continue;

                // 计算相交区域
                RectangleF intersectionRect = RectangleF.Intersect(rect, leaf.m_zone);

                // 查看是否与此节点内的 item 有覆盖
                foreach (var item in leaf.managedItems)
                {
                    if (intersectionRect.IntersectsWith(item.Zone))
                        outList.AddLast(item);
                }

                // 查找子节点
                for (int i = 0; i < 4; i++)
                {
                    if (leaf.childern[i] != null && intersectionRect.IntersectsWith(leaf.childern[i].Zone))
                    {
                        queue.Enqueue(leaf.childern[i]);
                    }
                }
            }

            return outList;
        }

        public LinkedList<RayCastResult> RayCastAll(Ray2D ray)
        {
            var outList = new LinkedList<RayCastResult>();
            Queue<QuadTreeNode> queue = new Queue<QuadTreeNode>();
            queue.Enqueue(this);

            Vector2 castResult;
            while (queue.Count != 0)
            {
                var leaf = queue.Dequeue();

                // 当此节点 Zone 与所给 RectangleF 不相交时返回 null
                if (ray.Intersects(leaf.m_zone, out castResult) == false)
                    continue;

                // 查看是否与此节点内的 item 有覆盖
                foreach (var item in leaf.managedItems)
                {
                    if (ray.Intersects(item.Zone, out castResult))
                        outList.AddLast(new RayCastResult(castResult, item));
                }

                // 查找子节点
                for (int i = 0; i < 4; i++)
                {
                    if (leaf.childern[i] != null && ray.Intersects(leaf.childern[i].Zone, out castResult))
                    {
                        queue.Enqueue(leaf.childern[i]);
                    }
                }
            }

            return outList;
        }


        public bool RayCast(Ray2D ray, out RayCastResult result)
        {
            bool existResult = false;
            result = new RayCastResult();
            Queue<QuadTreeNode> queue = new Queue<QuadTreeNode>();
            queue.Enqueue(this);

            Vector2 hitPoint;
            while (queue.Count != 0)
            {
                var leaf = queue.Dequeue();

                // 当此节点 Zone 与所给 RectangleF 不相交时返回 null
                if (ray.Intersects(leaf.m_zone, out hitPoint) == false)
                    continue;

                // 查看是否与此节点内的 item 有覆盖
                foreach (var item in leaf.managedItems)
                {
                    if (ray.Intersects(item.Zone, out hitPoint)
                        && (existResult == false || Vector2.DistanceSquared(ray.Original, hitPoint) < Vector2.DistanceSquared(ray.Original, result.Intersection)))
                    {
                        existResult = true;
                        result = new RayCastResult(hitPoint, item);
                    }
                }

                // 查找子节点
                for (int i = 0; i < 4; i++)
                {
                    if (leaf.childern[i] != null && ray.Intersects(leaf.childern[i].Zone, out hitPoint))
                    {
                        queue.Enqueue(leaf.childern[i]);
                    }
                }
            }

            return existResult;
        }


        /// <summary>
        /// 尝试从此节点里删除一个 item
        /// 当item不被此节点区域包含时删除失败
        /// 当集合里没有此 item 时也将删除失败
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        internal bool Remove(Leaf item)
        {
            // 当item不被此节点区域包含时删除失败
            if (m_zone.Contains(item.Zone) == false)
            {
                return false;
            }

            // 优先尝试删除自己集合里的
            if (managedItems.Remove(item))
            {
                return true;
            }

            // 尝试删除子节点
            for (int i = 0; i < 4; i++)
            {
                if (childern[i] != null)
                {
                    if (childern[i].Remove(item))
                        return true;
                }
            }

            // 子节点里也没有
            return false;
        }
    }

    /// <summary>
    /// RectangleF based Quad tree
    /// </summary>
    public class QuadTree
    {
        /// <summary>
        /// root node
        /// </summary>
        readonly QuadTreeNode m_root;

        float m_minUnitSize = 100;

        /// <summary>
        /// left that out of word
        /// </summary>
        readonly HashSet<Leaf> overFlowLeaf = new HashSet<Leaf>();

        public QuadTree(float regionSize = 10000, float minUnitSize = 100)
        {
            //cellSize = float.MaxValue / 4;
            m_root = new QuadTreeNode(new RectangleF(-regionSize, -regionSize, regionSize * 2, regionSize * 2));
            m_minUnitSize = minUnitSize;
        }

        /// <summary>
        /// 插入到四叉树中
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public void Add(Leaf item)
        {
            Check.ArgumentNull(item, "item");
            if (m_root.Zone.Contains(item.Zone) == false)
            {
                overFlowLeaf.Add(item);
            }
            else
            {
                m_root.Insert(item, m_minUnitSize);
            }
        }

        public LinkedList<RayCastResult> RayCastAll(Ray2D ray)
        {
            var outList = m_root.RayCastAll(ray);

            Vector2 castResult;
            foreach (var leaf in overFlowLeaf)
            {
                if (ray.Intersects(leaf.Zone, out castResult))
                {
                    outList.AddLast(new RayCastResult(castResult, leaf));
                }
            }

            return outList;
        }


        public bool RayCast(Ray2D ray, out RayCastResult result)
        {
            bool existResult = m_root.RayCast(ray, out result);

            // check leaf not in world
            foreach (var leaf in overFlowLeaf)
            {
                if (ray.Intersects(leaf.Zone, out Vector2 hitPoint)
                    && (existResult == false || Vector2.DistanceSquared(ray.Original, hitPoint) < Vector2.DistanceSquared(ray.Original, result.Intersection)))
                {
                    existResult = true;
                    result = new RayCastResult(hitPoint, leaf);
                }
            }

            return existResult;
        }

        /// <summary>
        /// 区域查询
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public LinkedList<Leaf> QueryArea(RectangleF rect)
        {
            // check leaf in world
            var result = m_root.QueryArea(rect);

            // check leaf not in world
            foreach (var leaf in overFlowLeaf)
            {
                if (rect.IntersectsWith(leaf.Zone))
                {
                    result.AddFirst(leaf);
                }
            }

            return result;
        }

        /// <summary>
        /// 移除一个节点
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(Leaf item)
        {
            Check.ArgumentNull(item, "item");
            return m_root.Remove(item) || overFlowLeaf.Remove(item);
        }

        /// <summary>
        /// 便利所有节点
        /// </summary>
        /// <param name="action"></param>
		public void Traverse(Action<RectangleF, IEnumerable<Leaf>> action)
        {
            Check.ArgumentNull(action, "action");
            var queue = new Queue<QuadTreeNode>();
            queue.Enqueue(m_root);

            action(m_root.Zone, overFlowLeaf);

            while (queue.Count > 0)
            {
                var currentNode = queue.Dequeue();
                action(currentNode.Zone, currentNode.ManagedItems);
                foreach (var child in currentNode.childern)
                {
                    if (child != null)
                    {
                        queue.Enqueue(child);
                    }
                }
            }
        }

        /// <summary>
        /// 绘制 Debug 信息
        /// </summary>
        public void DrawDebug()
        {
            int totalLeafCount = 0;
            Traverse((rect, leafList) =>
            {
                Graphics.SetColor(Color.Gray);
                Graphics.Rectangle(DrawMode.Line, rect);
                foreach (var left in leafList)
                {
                    Graphics.SetColor(Color.Green);
                    Graphics.Rectangle(DrawMode.Line, left.Zone);
                }
                totalLeafCount++;
            });

            Graphics.Print($"leaf count : {totalLeafCount}", 10, 10);
        }
    }

}
