// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Love
{
    /// <summary>
    ///    <para>
    ///       Stores the location and size of a rectangular region.
    ///    </para>
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct RectangleF : IEquatable<RectangleF>
    {
        /// <summary>
        ///    Initializes a new instance of the <see cref='Love.RectangleF'/>
        ///    class.
        /// </summary>
        public static readonly RectangleF Empty = new RectangleF();

        public float X; // Do not rename (binary serialization) 
        public float Y; // Do not rename (binary serialization) 
        public float Width; // Do not rename (binary serialization) 
        public float Height; // Do not rename (binary serialization) 

        /// <summary>
        ///    <para>
        ///       Initializes a new instance of the <see cref='Love.RectangleF'/>
        ///       class with the specified location and size.
        ///    </para>
        /// </summary>
        public RectangleF(float x, float y, float width, float height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        /// <summary>
        ///    <para>
        ///       Initializes a new instance of the <see cref='Love.RectangleF'/>
        ///       class with the specified location
        ///       and size.
        ///    </para>
        /// </summary>
        public RectangleF(Vector2 location, SizeF size)
        {
            X = location.X;
            Y = location.Y;
            Width = size.Width;
            Height = size.Height;
        }

        /// <summary>
        ///    <para>
        ///       Creates a new <see cref='Love.RectangleF'/> with
        ///       the specified location and size.
        ///    </para>
        /// </summary>
        public static RectangleF FromLTRB(float left, float top, float right, float bottom) =>
            new RectangleF(left, top, right - left, bottom - top);

        /// <summary>
        ///    <para>
        ///       Gets or sets the coordinates of the upper-left corner of
        ///       the rectangular region represented by this <see cref='Love.RectangleF'/>.
        ///    </para>
        /// </summary>
        public Vector2 Location
        {
            get { return new Vector2(X, Y); }
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        /// <summary>
        ///    <para>
        ///       Gets or sets the size of this <see cref='Love.RectangleF'/>.
        ///    </para>
        /// </summary>
        public SizeF Size
        {
            get { return new SizeF(Width, Height); }
            set
            {
                Width = value.Width;
                Height = value.Height;
            }
        }

        /// <summary>
        ///    <para>
        ///       Gets the x-coordinate of the upper-left corner of the
        ///       rectangular region defined by this <see cref='Love.RectangleF'/> .
        ///    </para>
        /// </summary>
        public float Left { get => X; set => X = value; }

        /// <summary>
        ///    <para>
        ///       Gets the y-coordinate of the upper-left corner of the
        ///       rectangular region defined by this <see cref='Love.RectangleF'/>.
        ///    </para>
        /// </summary>
        public float Top { get => Y; set => Y = value; }

        /// <summary>
        ///    <para>
        ///       Gets the x-coordinate of the lower-right corner of the
        ///       rectangular region defined by this <see cref='Love.RectangleF'/>.
        ///    </para>
        /// </summary>
        public float Right { get => X + Width; set => X = value - Width; }

        /// <summary>
        ///    <para>
        ///       Gets the y-coordinate of the lower-right corner of the
        ///       rectangular region defined by this <see cref='Love.RectangleF'/>.
        ///    </para>
        /// </summary>
        public float Bottom { get => Y + Height; set => Y = value - Height; } 

        /// <summary>
        ///    <para>
        ///       Tests whether this <see cref='Love.RectangleF'/> has a <see cref='Love.RectangleF.Width'/> or a <see cref='Love.RectangleF.Height'/> of 0.
        ///    </para>
        /// </summary>
        public bool IsEmpty => (Width <= 0) || (Height <= 0);

        /// <summary>
        ///    <para>
        ///       Tests whether <paramref name="obj"/> is a <see cref='Love.RectangleF'/> with the same location and size of this
        ///    <see cref='Love.RectangleF'/>.
        ///    </para>
        /// </summary>
        public override bool Equals(object obj) => obj is RectangleF && Equals((RectangleF)obj);

        public bool Equals(RectangleF other) => this == other;

        /// <summary>
        ///    <para>
        ///       Tests whether two <see cref='Love.RectangleF'/>
        ///       objects have equal location and size.
        ///    </para>
        /// </summary>
        public static bool operator ==(RectangleF left, RectangleF right) =>
            left.X == right.X && left.Y == right.Y && left.Width == right.Width && left.Height == right.Height;

        /// <summary>
        ///    <para>
        ///       Tests whether two <see cref='Love.RectangleF'/>
        ///       objects differ in location or size.
        ///    </para>
        /// </summary>
        public static bool operator !=(RectangleF left, RectangleF right) => !(left == right);

        /// <summary>
        ///    <para>
        ///       Determines if the specified point is contained within the
        ///       rectangular region defined by this <see cref='Love.Rectangle'/> .
        ///    </para>
        /// </summary>
        public bool Contains(float x, float y) => X <= x && x < X + Width && Y <= y && y < Y + Height;

        /// <summary>
        ///    <para>
        ///       Determines if the specified point is contained within the
        ///       rectangular region defined by this <see cref='Love.Rectangle'/> .
        ///    </para>
        /// </summary>
        public bool Contains(Vector2 pt) => Contains(pt.X, pt.Y);

        /// <summary>
        ///    <para>
        ///       Determines if the rectangular region represented by
        ///    <paramref name="rect"/> is entirely contained within the rectangular region represented by 
        ///       this <see cref='Love.Rectangle'/> .
        ///    </para>
        /// </summary>
        public bool Contains(RectangleF rect) =>
            (X <= rect.X) && (rect.X + rect.Width <= X + Width) && (Y <= rect.Y) && (rect.Y + rect.Height <= Y + Height);

        /// <summary>
        ///    Gets the hash code for this <see cref='Love.RectangleF'/>.
        /// </summary>
        public override int GetHashCode() =>
            HashHelpers.Combine(
                HashHelpers.Combine(HashHelpers.Combine(X.GetHashCode(), Y.GetHashCode()), Width.GetHashCode()),
                Height.GetHashCode());

        /// <summary>
        ///    <para>
        ///       Inflates this <see cref='Love.Rectangle'/>
        ///       by the specified amount.
        ///    </para>
        /// </summary>
        public void Inflate(float x, float y)
        {
            X -= x;
            Y -= y;
            Width += 2 * x;
            Height += 2 * y;
        }

        /// <summary>
        ///    Inflates this <see cref='Love.Rectangle'/> by the specified amount.
        /// </summary>
        public void Inflate(SizeF size) => Inflate(size.Width, size.Height);

        /// <summary>
        ///    <para>
        ///       Creates a <see cref='Love.Rectangle'/>
        ///       that is inflated by the specified amount.
        ///    </para>
        /// </summary>
        public static RectangleF Inflate(RectangleF rect, float x, float y)
        {
            RectangleF r = rect;
            r.Inflate(x, y);
            return r;
        }

        /// <summary>
        /// Lead this rectangle intersection with other rectangle.
        /// </summary>
        public void Intersect(RectangleF rect)
        {
            RectangleF result = Intersect(rect, this);

            X = result.X;
            Y = result.Y;
            Width = result.Width;
            Height = result.Height;
        }

        /// <summary>
        ///    Creates a rectangle that represents the intersection between a and
        ///    b. If there is no intersection, null is returned.
        /// </summary>
        public static RectangleF Intersect(RectangleF a, RectangleF b)
        {
            float x1 = Math.Max(a.X, b.X);
            float x2 = Math.Min(a.X + a.Width, b.X + b.Width);
            float y1 = Math.Max(a.Y, b.Y);
            float y2 = Math.Min(a.Y + a.Height, b.Y + b.Height);

            if (x2 >= x1 && y2 >= y1)
            {
                return new RectangleF(x1, y1, x2 - x1, y2 - y1);
            }

            return Empty;
        }

        /// <summary>
        ///    Determines if this rectangle intersects with rect.
        /// </summary>
        public bool IntersectsWith(RectangleF rect) =>
            (rect.X < X + Width) && (X < rect.X + rect.Width) && (rect.Y < Y + Height) && (Y < rect.Y + rect.Height);

        /// <summary>
        ///    Creates a rectangle that represents the union between a and
        ///    b.
        /// </summary>
        public static RectangleF Union(RectangleF a, RectangleF b)
        {
            float x1 = Math.Min(a.X, b.X);
            float x2 = Math.Max(a.X + a.Width, b.X + b.Width);
            float y1 = Math.Min(a.Y, b.Y);
            float y2 = Math.Max(a.Y + a.Height, b.Y + b.Height);

            return new RectangleF(x1, y1, x2 - x1, y2 - y1);
        }

        /// <summary>
        ///    Adjusts the location of this rectangle by the specified amount.
        /// </summary>
        public void Offset(Vector2 pos) => Offset(pos.X, pos.Y);

        /// <summary>
        ///    Adjusts the location of this rectangle by the specified amount.
        /// </summary>
        public void Offset(float x, float y)
        {
            X += x;
            Y += y;
        }

        /// <summary>
        ///    Converts the specified <see cref='Love.Rectangle'/> to a
        /// <see cref='Love.RectangleF'/>.
        /// </summary>
        public static implicit operator RectangleF(Rectangle r) => new RectangleF(r.X, r.Y, r.Width, r.Height);

        /// <summary>
        ///    Converts the <see cref='Love.RectangleF.Location'/> and <see cref='Love.RectangleF.Size'/> of this <see cref='Love.RectangleF'/> to a
        ///    human-readable string.
        /// </summary>
        public override string ToString() =>
            "{X=" + X.ToString() + ",Y=" + Y.ToString() +
            ",Width=" + Width.ToString() + ",Height=" + Height.ToString() + "}";
    }
}