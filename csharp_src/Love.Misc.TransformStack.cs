using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Love.Misc
{
    public class TransformStack
    {
        public LinkedList<Matrix44?> list = new LinkedList<Matrix44?>();

        public TransformStack()
        {
            Clear();
        }

        /// <summary>
        /// remove all transform
        /// </summary>
        public void Clear()
        {
            list.Clear();
            Push();
        }

        public void Pop()
        {
            if (list.Count == 1)
                throw new System.Exception(" stack is empty ! ");

            list.RemoveLast();
        }

        public void Push()
        {
            list.AddLast(Matrix44.Identity);
        }

        public void Effect(Matrix44 effect)
        {
            var m = list.Last.Value.Value;
            list.RemoveLast();
            //list.AddLast(m * effect);
            list.AddLast(effect * m);
        }

        public void Scale(float sx, float sy)
        {
            Effect(Matrix44.CreateScale(sx, sy, 1));
        }

        public void Translate(float ox, float oy)
        {
            Effect(Matrix44.CreateTranslation(ox, oy, 0));
        }

        public void Rotate(float r)
        {
            Effect(Matrix44.CreateRotationZ(r));
        }

        public Vector2 TransformPoint(Vector2 p)
        {
            var m = list.Last.Value.Value;
            Vector2Transform(ref p, ref m, out var res);
            return res;

            var v3 = TransformVector3(new Vector3(p, 0), list.Last.Value.Value);
            return new Vector2(v3.X, v3.Y);
        }

        public Vector2 TransformPointInvert(Vector2 p)
        {
            Matrix44.Invert(list.Last.Value.Value, out var result);
            Vector2Transform(ref p, ref result, out var res);
            return res;

            var v3 = TransformVector3(new Vector3(p, 0), result);
            return new Vector2(v3.X, v3.Y);
        }

        public Matrix44 CurrentMatrix => list.Last.Value.Value;

        public static Vector3 TransformVector3(Vector3 vector, Matrix44 matrix)
        {
            return new Vector3(
                vector.X * matrix.M11 + vector.Y * matrix.M21 + vector.Z * matrix.M31 + 1 * matrix.M41,
                vector.X * matrix.M12 + vector.Y * matrix.M22 + vector.Z * matrix.M32 + 1 * matrix.M42,
                vector.X * matrix.M13 + vector.Y * matrix.M23 + vector.Z * matrix.M33 + 1 * matrix.M43);
        }


        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains a transformation of 2d-vector by the specified <see cref="Matrix"/>.
        /// </summary>
        /// <param name="position">Source <see cref="Vector2"/>.</param>
        /// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
        /// <param name="result">Transformed <see cref="Vector2"/> as an output parameter.</param>
        public static void Vector2Transform(ref Vector2 position, ref Matrix44 matrix, out Vector2 result)
        {
            var x = (position.X * matrix.M11) + (position.Y * matrix.M21) + matrix.M41;
            var y = (position.X * matrix.M12) + (position.Y * matrix.M22) + matrix.M42;
            result.X = x;
            result.Y = y;
        }

    }
}