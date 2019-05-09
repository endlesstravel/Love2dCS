// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Love
{
    /// <summary>
    ///    Represents an ordered pair of x and y coordinates that
    ///    define a point in a two-dimensional plane.
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Point : IEquatable<Point>
    {
        /// <summary>
        ///    Creates a new instance of the <see cref='Love.Point'/> class
        ///    with member data left uninitialized.
        /// </summary>
        public static readonly Point Empty = new Point();

        public int x; // Do not rename (binary serialization)
        public int y; // Do not rename (binary serialization)

        /// <summary>
        ///    Initializes a new instance of the <see cref='Love.Point'/> class
        ///    with the specified coordinates.
        /// </summary>
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        ///    <para>
        ///       Initializes a new instance of the <see cref='Love.Point'/> class
        ///       from a <see cref='System.Drawing.Size'/> .
        ///    </para>
        /// </summary>
        public Point(Size sz)
        {
            x = sz.Width;
            y = sz.Height;
        }

        /// <summary>
        ///    Initializes a new instance of the Point class using
        ///    coordinates specified by an integer value.
        /// </summary>
        public Point(int dw)
        {
            x = LowInt16(dw);
            y = HighInt16(dw);
        }

        /// <summary>
        ///    <para>
        ///       Gets a value indicating whether this <see cref='Love.Point'/> is empty.
        ///    </para>
        /// </summary>
        [Browsable(false)]
        public bool IsEmpty => x == 0 && y == 0;

        /// <summary>
        ///    Gets the x-coordinate of this <see cref='Love.Point'/>.
        /// </summary>
        public int X
        {
            get { return x; }
            set { x = value; }
        }

        /// <summary>
        ///    <para>
        ///       Gets the y-coordinate of this <see cref='Love.Point'/>.
        ///    </para>
        /// </summary>
        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        /// <summary>
        ///    <para>
        ///       Creates a <see cref='Love.Vector2'/> with the coordinates of the specified
        ///    <see cref='Love.Point'/> 
        /// </para>
        /// </summary>
        public static implicit operator Vector2(Point p) => new Vector2(p.X, p.Y);

        /// <summary>
        ///    <para>
        ///       Creates a <see cref='Size'/> with the coordinates of the specified <see cref='Love.Point'/> .
        ///    </para>
        /// </summary>
        public static explicit operator Size(Point p) => new Size(p.X, p.Y);

        /// <summary>
        ///    <para>
        ///       Translates a <see cref='Love.Point'/> by a given <see cref='Point'/> .
        ///    </para>
        /// </summary>        
        public static Point operator +(Point pt, Size sz) => Add(pt, sz);

        /// <summary>
        ///    <para>
        ///       Translates a <see cref='Love.Point'/> by the negative of a given <see cref='Point'/> .
        ///    </para>
        /// </summary>        
        public static Point operator -(Point pt, Size sz) => Subtract(pt, sz);

        /// <summary>
        ///    <para>
        ///       Compares two <see cref='Love.Point'/> objects. The result specifies
        ///       whether the values of the <see cref='Love.Point.X'/> and <see cref='Love.Point.Y'/> properties of the two <see cref='Love.Point'/>
        ///       objects are equal.
        ///    </para>
        /// </summary>
        public static bool operator ==(Point left, Point right) => left.X == right.X && left.Y == right.Y;

        /// <summary>
        ///    <para>
        ///       Compares two <see cref='Love.Point'/> objects. The result specifies whether the values
        ///       of the <see cref='Love.Point.X'/> or <see cref='Love.Point.Y'/> properties of the two
        ///    <see cref='Love.Point'/> 
        ///    objects are unequal.
        /// </para>
        /// </summary>
        public static bool operator !=(Point left, Point right) => !(left == right);

        /// <summary>
        ///    <para>
        ///       Translates a <see cref='Love.Point'/> by a given <see cref='Point'/> .
        ///    </para>
        /// </summary>        
        public static Point Add(Point pt, Size sz) => new Point(unchecked(pt.X + sz.Width), unchecked(pt.Y + sz.Height));

        /// <summary>
        ///    <para>
        ///       Translates a <see cref='Love.Point'/> by the negative of a given <see cref='Point'/> .
        ///    </para>
        /// </summary>        
        public static Point Subtract(Point pt, Size sz) => new Point(unchecked(pt.X - sz.Width), unchecked(pt.Y - sz.Height));

        /// <summary>
        ///   Converts a PointF to a Point by performing a ceiling operation on
        ///   all the coordinates.
        /// </summary>
        public static Point Ceiling(Vector2 value) => new Point(unchecked((int)Math.Ceiling(value.X)), unchecked((int)Math.Ceiling(value.Y)));

        /// <summary>
        ///   Converts a PointF to a Point by performing a truncate operation on
        ///   all the coordinates.
        /// </summary>
        public static Point Truncate(Vector2 value) => new Point(unchecked((int)value.X), unchecked((int)value.Y));

        /// <summary>
        ///   Converts a PointF to a Point by performing a round operation on
        ///   all the coordinates.
        /// </summary>
        public static Point Round(Vector2 value) => new Point(unchecked((int)Math.Round(value.X)), unchecked((int)Math.Round(value.Y)));

        /// <summary>
        ///    <para>
        ///       Specifies whether this <see cref='Love.Point'/> contains
        ///       the same coordinates as the specified <see cref='System.Object'/>.
        ///    </para>
        /// </summary>
        public override bool Equals(object obj) => obj is Point && Equals((Point)obj);

        public bool Equals(Point other) => this == other;

        /// <summary>
        ///    <para>
        ///       Returns a hash code.
        ///    </para>
        /// </summary>
        public override int GetHashCode() => HashHelpers.Combine(X, Y);

        /// <summary>
        ///    Translates this <see cref='Love.Point'/> by the specified amount.
        /// </summary>
        public void Offset(int dx, int dy)
        {
            unchecked
            {
                X += dx;
                Y += dy;
            }
        }

        /// <summary>
        ///    Translates this <see cref='Love.Point'/> by the specified amount.
        /// </summary>
        public void Offset(Point p) => Offset(p.X, p.Y);

        /// <summary>
        ///    <para>
        ///       Converts this <see cref='Love.Point'/>
        ///       to a human readable
        ///       string.
        ///    </para>
        /// </summary>
        public override string ToString() => "{X=" + X.ToString() + ",Y=" + Y.ToString() + "}";

        private static short HighInt16(int n) => unchecked((short)((n >> 16) & 0xffff));

        private static short LowInt16(int n) => unchecked((short)(n & 0xffff));
    }
}