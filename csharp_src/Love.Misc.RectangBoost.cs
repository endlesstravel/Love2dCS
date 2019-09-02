
namespace Love
{
    /// <summary>
    /// the extend class of RectangleF
    /// </summary>
    public static class RectangBoost
    {
        /// <summary>
        /// Generate a new RectangleF with new vertical center
        /// </summary>
        /// <param name="r"></param>
        /// <param name="verticalCenter">the new vertical center</param>
        /// <returns></returns>
        public static RectangleF DefVerticalCenter(this RectangleF r, float verticalCenter)
        {
            return new RectangleF(verticalCenter - r.Width / 2, r.Y, r.Width, r.Height);
        }
        /// <summary>
        /// Generate a new RectangleF with new horizontal center
        /// </summary>
        /// <param name="r"></param>
        /// <param name="horizontalCenter">the new horizontal center</param>
        /// <returns></returns>
        public static RectangleF DefHorizontalCenter(this RectangleF r, float horizontalCenter)
        {
            return new RectangleF(r.X, horizontalCenter - r.Height / 2, r.Width, r.Height);
        }

        /// <summary>
        /// Generate a new RectangleF with new center
        /// </summary>
        /// <param name="r"></param>
        /// <param name="pos">the center</param>
        /// <returns></returns>
        public static RectangleF DefCenter(this RectangleF r, Vector2 pos) => DefCenter(r, pos.X, pos.Y);


        /// <summary>
        /// Generate a new RectangleF with new center
        /// </summary>
        /// <param name="r"></param>
        /// <param name="vertical">vertical of the center</param>
        /// <param name="horizontal">horizontal of the center</param>
        /// <returns></returns>
        public static RectangleF DefCenter(this RectangleF r, float vertical, float horizontal)
        {
            return new RectangleF(vertical - r.Width / 2, horizontal - r.Height / 2, r.Width, r.Height);
        }

        /// <summary>
        /// Generate a new RectangleF with new Location
        /// </summary>
        /// <param name="r"></param>
        /// <param name="pos">Location of new RectangleF</param>
        /// <returns></returns>
        public static RectangleF DefLocation(this RectangleF r, Vector2 pos) => DefLocation(r, pos.X, pos.Y);

        /// <summary>
        /// Generate a new RectangleF with new Location
        /// </summary>
        /// <param name="r"></param>
        /// <param name="x">the x of the Location of the new RectangleF</param>
        /// <param name="y">the y of the Location of the new RectangleF</param>
        /// <returns></returns>
        public static RectangleF DefLocation(this RectangleF r, float x, float y)
        {
            return new RectangleF(x, y, r.Width, r.Height);
        }

        /// <summary>
        /// get the center of the RectangleF
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public static Vector2 Center(this RectangleF r)
        {
            return new Vector2(r.X + r.Width / 2, r.Y + r.Height / 2);
        }

        /// <summary>
        /// Generate a new RectangleF with new left
        /// </summary>
        /// <param name="r"></param>
        /// <param name="left">left of new RectangleF</param>
        /// <returns></returns>
        public static RectangleF DefLeft(this RectangleF r, float left)
        {
            return new RectangleF(left, r.Y, r.Width, r.Height);
        }

        /// <summary>
        /// Generate a new RectangleF with new right
        /// </summary>
        /// <param name="r"></param>
        /// <param name="right">right of new RectangleF</param>
        /// <returns></returns>
        public static RectangleF DefRight(this RectangleF r, float right)
        {
            return new RectangleF(right - r.Width, r.Y, r.Width, r.Height);
        }


        /// <summary>
        /// Generate a new RectangleF with new top
        /// </summary>
        /// <param name="r"></param>
        /// <param name="top">top of new RectangleF</param>
        /// <returns></returns>
        public static RectangleF DefTop(this RectangleF r, float top)
        {
            return new RectangleF(r.X, top, r.Width, r.Height);
        }

        /// <summary>
        /// Generate a new RectangleF with new bottom
        /// </summary>
        /// <param name="r"></param>
        /// <param name="bottom">bottom of new RectangleF</param>
        /// <returns></returns>
        public static RectangleF DefBottom(this RectangleF r, float bottom)
        {
            return new RectangleF(r.X, bottom - r.Height, r.Width, r.Height);
        }

        /// <summary>
        /// Generate a new RectangleF with padding
        /// </summary>
        /// <param name="r"></param>
        /// <param name="padding">padding of new RectangleF</param>
        /// <returns></returns>
        public static RectangleF DefPadding(this RectangleF r, float padding)
        {
            float pw = padding;
            float ph = padding;
            return new RectangleF(r.X + pw, r.Y + ph, r.Width - 2 * pw, r.Height - 2 * ph);
        }

        /// <summary>
        /// Generate a new RectangleF with precent padding
        /// </summary>
        /// <param name="r"></param>
        /// <param name="paddingScaleX">the x of precent padding of new RectangleF to use</param>
        /// <param name="paddingScaleY">the y of precent padding of new RectangleF to use</param>
        /// <returns></returns>
        public static RectangleF Padding(this RectangleF r, float paddingScaleX, float paddingScaleY)
        {
            float pw = paddingScaleX * r.Width;
            float ph = paddingScaleY * r.Height;
            return new RectangleF(r.X + pw, r.Y + ph, r.Width - 2 * pw, r.Height - 2 * ph);
        }

        /// <summary>
        /// Generate a new RectangleF with precent padding
        /// </summary>
        /// <param name="r"></param>
        /// <param name="paddingScale">precent padding of new RectangleF to use</param>
        /// <returns></returns>
        public static RectangleF Padding(this RectangleF r, float paddingScale) => Padding(r, paddingScale, paddingScale);


        public static RectangleF PaddingHorizontal(this RectangleF r, float scale) => ExtendHorizontal(r, -scale);
        public static RectangleF PaddingVertical(this RectangleF r, float scale) => ExtendVertical(r, -scale);


        /// <summary>
        /// Generate a new RectangleF with top part of the original rect.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="precent">precent to use</param>
        /// <returns></returns>
        public static RectangleF Top(this RectangleF r, float precent)
        {
            float h = r.Height * precent;
            return new RectangleF(r.X, r.Y, r.Width, h);
        }

        /// <summary>
        /// Generate a new RectangleF with bottom part of the original rect.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="precent">precent to use</param>
        /// <returns></returns>
        public static RectangleF Bottom(this RectangleF r, float precent)
        {
            float h = r.Height * precent;
            return new RectangleF(r.X, r.Y + (r.Height - h), r.Width, h);
        }

        /// <summary>
        /// Generate a new RectangleF with left part of the original rect.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="precent">precent to use</param>
        /// <returns></returns>
        public static RectangleF Left(this RectangleF r, float precent)
        {
            float w = r.Width * precent;
            return new RectangleF(r.X, r.Y, w, r.Height);
        }

        /// <summary>
        /// Generate a new RectangleF with right part of the original rect.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="precent">precent to use</param>
        /// <returns></returns>
        public static RectangleF Right(this RectangleF r, float precent)
        {
            float w = r.Width * precent;
            return new RectangleF(r.X + (r.Width - w), r.Y, w, r.Height);
        }


        /// <summary>
        /// Splits a RectangleF horizontally into the specified number of sub-rects, and returns a sub-rect for the specified index.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="count">The amount of subrects the RectangleF should be split into.</param>
        /// <param name="index">The index for the subrect. Includes 0, and excludes count.</param>
        /// <returns></returns>
        public static RectangleF SplitX(this RectangleF r, int count, int index)
        {
            float w = r.Width / count;
            return new RectangleF(r.X + index * w, r.Y, w, r.Height);
        }

        /// <summary>
        /// Splits a RectangleF vertically into the specified number of sub-rects, and returns a sub-rect for the specified index.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="count">The amount of subrects the RectangleF should be split into.</param>
        /// <param name="index">The index for the subrect. Includes 0, and excludes count.</param>
        /// <returns></returns>
        public static RectangleF SplitY(this RectangleF r, int count, int index)
        {
            float h = r.Height / count;
            return new RectangleF(r.X, r.Y + index * h, r.Width, h);
        }


        /// <summary>
        /// Generate a new RectangleF with center part of the original rect.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="horizontal">the horizontal part of original rect</param>
        /// <param name="vertical">the vertical part of original rect</param>
        /// <returns></returns>
        public static RectangleF SplitCenter(this RectangleF r, float horizontal, float vertical)
        {
            float w = r.Width * horizontal;
            float h = r.Height * vertical;
            return new RectangleF(r.X + (r.Width - w) * 0.5f, r.Y + (r.Height - h) * 0.5f, w, h);
        }

        /// <summary>
        /// Splits a RectangleF into a grid from left to right and then down.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="widthCount"> The width of a grid cell.</param>
        /// <param name="heightCount"> The height of a grid cell.</param>
        /// <param name="column">The x of the grid cell.</param>
        /// <param name="row">The y of the grid cell.</param>
        /// <returns></returns>
        public static RectangleF Grid(this RectangleF r, int widthCount, int heightCount, int column, int row)
        {
            float w = r.Width / widthCount;
            float h = r.Height / heightCount;
            return new RectangleF(r.X + column * w, r.Y + row * h, w, h);
        }

        /// <summary>
        /// Returns a RectangleF that has been expanded by the specified amount.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="scale">The desired prcent of expansion on the right.</param>
        /// <returns></returns>
        public static RectangleF ExtendRight(this RectangleF r, float scale)
        {
            float size = scale * r.Width;
            return new RectangleF(r.Left, r.Top, r.Width + size, r.Height);
        }

        /// <summary>
        /// Returns a RectangleF that has been expanded by the specified amount.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="scale">The desired prcent of expansion on the left.</param>
        /// <returns></returns>
        public static RectangleF ExtendLeft(this RectangleF r, float scale)
        {
            float size = scale * r.Width;
            return new RectangleF(r.Left - size, r.Top, r.Width + size, r.Height);
        }

        /// <summary>
        /// Returns a RectangleF that has been expanded by the specified amount.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="scale">The desired prcent of expansion on the top.</param>
        /// <returns></returns>
        public static RectangleF ExtendTop(this RectangleF r, float scale)
        {
            float size = scale * r.Height;
            return new RectangleF(r.Left, r.Top - size, r.Width, r.Height + size);
        }

        /// <summary>
        /// Returns a RectangleF that has been expanded by the specified amount.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="scale">The desired prcent of expansion on the bottom.</param>
        /// <returns></returns>
        public static RectangleF ExtendBottom(this RectangleF r, float scale)
        {
            float size = scale * r.Height;
            return new RectangleF(r.Left, r.Top, r.Width, r.Height + size);
        }



        /// <summary>
        /// Returns a RectangleF that has been expanded by the specified amount.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="scale">The desired prcent of expansion on the horizontal.</param>
        /// <returns></returns>
        public static RectangleF ExtendHorizontal(this RectangleF r, float scale)
        {
            float size = scale * 2 * r.Width;
            return new RectangleF(r.Left - size / 2, r.Top, r.Width + size, r.Height);
        }

        /// <summary>
        /// Returns a RectangleF that has been expanded by the specified amount.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="scale">The desired prcent of expansion on the vertical.</param>
        /// <returns></returns>
        public static RectangleF ExtendVertical(this RectangleF r, float scale)
        {
            float size = scale * 2 * r.Height;
            return new RectangleF(r.Left, r.Top - size / 2, r.Width, r.Height + size);
        }

        /// <summary>
        /// get the diagonal length of the RectangleF
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public static float DiagonalLength(this RectangleF r)
        {
            return Mathf.Sqrt(r.Width * r.Width + r.Height * r.Height);
        }


        /// <summary>
        /// Returns a Rectangle that Floor each part of RectangleF.
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public static Rectangle FloorToRectangle(this RectangleF r)
        {
            return new Rectangle(Mathf.FloorToInt(r.X), Mathf.FloorToInt(r.Y), Mathf.FloorToInt(r.Width), Mathf.FloorToInt(r.Height));
        }

        /// <summary>
        /// Returns a Rectangle that Ceil each part of RectangleF.
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public static Rectangle CeilToRectangle(this RectangleF r)
        {
            return new Rectangle(Mathf.CeilToInt(r.X), Mathf.CeilToInt(r.Y), Mathf.CeilToInt(r.Width), Mathf.CeilToInt(r.Height));
        }


        /// <summary>
        /// Returns a Rectangle that Round each part of RectangleF.
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public static Rectangle RoundToRectangle(this RectangleF r)
        {
            return new Rectangle(Mathf.RoundToInt(r.X), Mathf.RoundToInt(r.Y), Mathf.RoundToInt(r.Width), Mathf.RoundToInt(r.Height));
        }
    }
}
