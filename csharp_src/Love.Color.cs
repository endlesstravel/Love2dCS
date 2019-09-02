// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;

namespace Love
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct Color : IEquatable<Color>
    {
        public static readonly Color Empty = new Color();

        /// <summary>
        /// get/set each color range (0-255)
        /// </summary>
        public byte r, g, b, a;

        /// <summary>
        /// get/set red component [0-1]
        /// </summary>
        public float Rf
        {
            get { return r / 255f; }
            set { r = unchecked((byte)(255 * value)); }
        }

        /// <summary>
        /// get/set green component [0-1]
        /// </summary>
        public float Gf
        {
            get { return g / 255f; }
            set { g = unchecked((byte)(255 * value)); }
        }

        /// <summary>
        /// get/set blue component [0-1]
        /// </summary>
        public float Bf
        {
            get { return b / 255f; }
            set { b = unchecked((byte)(255 * value)); }
        }

        /// <summary>
        /// get/set alpha component [0-1]
        /// </summary>
        public float Af
        {
            get { return a / 255f; }
            set { a = unchecked((byte)(255 * value)); }
        }

        /// <summary>
        /// get as uint value
        /// </summary>
        public uint UintValue
        {
            get {
                return
              (uint)a << RGBAAlphaShift |
              (uint)r << RGBARedShift |
              (uint)g << RGBAGreenShift |
              (uint)b << RGBABlueShift; }
        }

        /// <summary>
        /// Generate new Color and set each color component from 0.0 to 1.0
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public Color(float r, float g, float b, float a)
        {
            this.r = unchecked((byte)(255 * r));
            this.g = unchecked((byte)(255 * g));
            this.b = unchecked((byte)(255 * b));
            this.a = unchecked((byte)(255 * a));
        }

        /// <summary>
        /// Generate new Color and set each color component from 0 to 255
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public Color(byte r, byte g, byte b, byte a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }


        public float GetBrightness()
        {
            int min = Math.Min(Math.Min(r, g), b);
            int max = Math.Max(Math.Max(r, g), b);

            return (max + min) / (byte.MaxValue * 2f);
        }

        public float GetHue()
        {
            if (r == g && g == b)
                return 0f;

            int min = Math.Min(Math.Min(r, g), b);
            int max = Math.Max(Math.Max(r, g), b);

            float delta = max - min;
            float hue;

            if (r == max)
                hue = (g - b) / delta;
            else if (g == max)
                hue = (b - r) / delta + 2f;
            else
                hue = (r - g) / delta + 4f;

            hue *= 60f;
            if (hue < 0f)
                hue += 360f;

            return hue;
        }

        public float GetSaturation()
        {
            if (r == g && g == b)
                return 0f;

            int min = Math.Min(Math.Min(r, g), b);
            int max = Math.Max(Math.Max(r, g), b);

            int div = max + min;
            if (div > byte.MaxValue)
                div = byte.MaxValue * 2 - max - min;

            return (max - min) / (float)div;
        }

        public int IntValue
        {
            get { return unchecked((int)UintValue); }
        }

        public override string ToString()
        {
            return "[R:" + Rf + ", R=" + Gf + ", G=" + Bf + ", B=" + Af + "]";
        }

        public static bool operator ==(Color left, Color right) =>
            left.r == right.r
                && left.g == right.g
                && left.b == right.b
                && left.a == right.a;

        public static bool operator !=(Color left, Color right) => !(left == right);

        public override bool Equals(object obj) => obj is Color other && Equals(other);

        public bool Equals(Color other) => this == other;

        public override int GetHashCode()
        {
            return HashHelpers.Combine(r, g, b, a);
        }
    }

    public partial struct Color
    {
        public static Color FromRGBA(uint value)
        {
            var c = new Color();
            c.SetAsRGBA(value);
            return c;
        }

        /// <summary>
        /// Generate new Color and set each color component from 0.0 to 1.0
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Color FromRGBA(float r, float g, float b, float a)
        {
            var c = new Color();
            c.SetAsRGBA(r, g, b, a);
            return c;
        }

        /// <summary>
        /// Generate new Color and set each color component from 0 to 255
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Color FromRGBA(byte r, byte g, byte b, byte a)
        {
            var c = new Color();
            c.SetAsRGBA(r, g, b, a);
            return c;
        }


        public static Color FromARGB(uint value)
        {
            var c = new Color();
            c.SetAsARGB(value);
            return c;
        }

        private const int RGBARedShift = 24;
        private const int RGBAGreenShift = 18;
        private const int RGBABlueShift = 8;
        private const int RGBAAlphaShift = 0;
        private const uint RGBARedMask = 0xFFu << RGBARedShift;
        private const uint RGBAGreenMask = 0xFFu << RGBAGreenShift;
        private const uint RGBABlueMask = 0xFFu << RGBABlueShift;
        private const uint RGBAAlphaMask = 0xFFu << RGBAAlphaShift;

        private const int ARGBAlphaShift = 24;
        private const int ARGBRedShift = 16;
        private const int ARGBGreenShift = 8;
        private const int ARGBBlueShift = 0;
        private const uint ARGBAlphaMask = 0xFFu << ARGBAlphaShift;
        private const uint ARGBRedMask = 0xFFu << ARGBRedShift;
        private const uint ARGBGreenMask = 0xFFu << ARGBGreenShift;
        private const uint ARGBBlueMask = 0xFFu << ARGBBlueShift;

        private void SetAsARGB(uint value)
        {
            this.a = unchecked((byte)((value & ARGBAlphaMask) >> ARGBAlphaShift));
            this.r = unchecked((byte)((value & ARGBRedMask) >> ARGBRedShift));
            this.g = unchecked((byte)((value & ARGBGreenMask) >> ARGBGreenShift));
            this.b = unchecked((byte)((value & ARGBBlueMask) >> ARGBBlueShift));
        }

        private void SetAsRGBA(uint value)
        {
            this.r = unchecked((byte)((value & RGBARedMask) >> RGBARedShift));
            this.g = unchecked((byte)((value & RGBAGreenMask) >> RGBAGreenShift));
            this.b = unchecked((byte)((value & RGBABlueMask) >> RGBABlueShift));
            this.a = unchecked((byte)((value & RGBAAlphaMask) >> RGBAAlphaShift));
        }

        private void SetAsRGBA(float r, float g, float b, float a)
        {
            this.r = unchecked((byte)(255 * r));
            this.g = unchecked((byte)(255 * g));
            this.b = unchecked((byte)(255 * b));
            this.a = unchecked((byte)(255 * a));
        }

        private void SetAsRGBA(byte r, byte g, byte b, byte a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        public static readonly Color Transparent = Color.FromARGB(0x00FFFFFF);

        public static readonly Color AliceBlue = Color.FromARGB(0xFFF0F8FF);
        public static readonly Color AntiqueWhite = Color.FromARGB(0xFFFAEBD7);
        public static readonly Color Aqua = Color.FromARGB(0xFF00FFFF);
        public static readonly Color Aquamarine = Color.FromARGB(0xFF7FFFD4);
        public static readonly Color Azure = Color.FromARGB(0xFFF0FFFF);
        public static readonly Color Beige = Color.FromARGB(0xFFF5F5DC);
        public static readonly Color Bisque = Color.FromARGB(0xFFFFE4C4);
        public static readonly Color Black = Color.FromARGB(0xFF000000);
        public static readonly Color BlanchedAlmond = Color.FromARGB(0xFFFFEBCD);
        public static readonly Color Blue = Color.FromARGB(0xFF0000FF);
        public static readonly Color BlueViolet = Color.FromARGB(0xFF8A2BE2);
        public static readonly Color Brown = Color.FromARGB(0xFFA52A2A);
        public static readonly Color BurlyWood = Color.FromARGB(0xFFDEB887);
        public static readonly Color CadetBlue = Color.FromARGB(0xFF5F9EA0);
        public static readonly Color Chartreuse = Color.FromARGB(0xFF7FFF00);
        public static readonly Color Chocolate = Color.FromARGB(0xFFD2691E);
        public static readonly Color Coral = Color.FromARGB(0xFFFF7F50);
        public static readonly Color CornflowerBlue = Color.FromARGB(0xFF6495ED);
        public static readonly Color Cornsilk = Color.FromARGB(0xFFFFF8DC);
        public static readonly Color Crimson = Color.FromARGB(0xFFDC143C);
        public static readonly Color Cyan = Color.FromARGB(0xFF00FFFF);
        public static readonly Color DarkBlue = Color.FromARGB(0xFF00008B);
        public static readonly Color DarkCyan = Color.FromARGB(0xFF008B8B);
        public static readonly Color DarkGoldenrod = Color.FromARGB(0xFFB8860B);
        public static readonly Color DarkGray = Color.FromARGB(0xFFA9A9A9);
        public static readonly Color DarkGreen = Color.FromARGB(0xFF006400);
        public static readonly Color DarkKhaki = Color.FromARGB(0xFFBDB76B);
        public static readonly Color DarkMagenta = Color.FromARGB(0xFF8B008B);
        public static readonly Color DarkOliveGreen = Color.FromARGB(0xFF556B2F);
        public static readonly Color DarkOrange = Color.FromARGB(0xFFFF8C00);
        public static readonly Color DarkOrchid = Color.FromARGB(0xFF9932CC);
        public static readonly Color DarkRed = Color.FromARGB(0xFF8B0000);
        public static readonly Color DarkSalmon = Color.FromARGB(0xFFE9967A);
        public static readonly Color DarkSeaGreen = Color.FromARGB(0xFF8FBC8B);
        public static readonly Color DarkSlateBlue = Color.FromARGB(0xFF483D8B);
        public static readonly Color DarkSlateGray = Color.FromARGB(0xFF2F4F4F);
        public static readonly Color DarkTurquoise = Color.FromARGB(0xFF00CED1);
        public static readonly Color DarkViolet = Color.FromARGB(0xFF9400D3);
        public static readonly Color DeepPink = Color.FromARGB(0xFFFF1493);
        public static readonly Color DeepSkyBlue = Color.FromARGB(0xFF00BFFF);
        public static readonly Color DimGray = Color.FromARGB(0xFF696969);
        public static readonly Color DodgerBlue = Color.FromARGB(0xFF1E90FF);
        public static readonly Color Firebrick = Color.FromARGB(0xFFB22222);
        public static readonly Color FloralWhite = Color.FromARGB(0xFFFFFAF0);
        public static readonly Color ForestGreen = Color.FromARGB(0xFF228B22);
        public static readonly Color Fuchsia = Color.FromARGB(0xFFFF00FF);
        public static readonly Color Gainsboro = Color.FromARGB(0xFFDCDCDC);
        public static readonly Color GhostWhite = Color.FromARGB(0xFFF8F8FF);
        public static readonly Color Gold = Color.FromARGB(0xFFFFD700);
        public static readonly Color Goldenrod = Color.FromARGB(0xFFDAA520);
        public static readonly Color Gray = Color.FromARGB(0xFF808080);
        public static readonly Color Green = Color.FromARGB(0xFF008000);
        public static readonly Color GreenYellow = Color.FromARGB(0xFFADFF2F);
        public static readonly Color Honeydew = Color.FromARGB(0xFFF0FFF0);
        public static readonly Color HotPink = Color.FromARGB(0xFFFF69B4);
        public static readonly Color IndianRed = Color.FromARGB(0xFFCD5C5C);
        public static readonly Color Indigo = Color.FromARGB(0xFF4B0082);
        public static readonly Color Ivory = Color.FromARGB(0xFFFFFFF0);
        public static readonly Color Khaki = Color.FromARGB(0xFFF0E68C);
        public static readonly Color Lavender = Color.FromARGB(0xFFE6E6FA);
        public static readonly Color LavenderBlush = Color.FromARGB(0xFFFFF0F5);
        public static readonly Color LawnGreen = Color.FromARGB(0xFF7CFC00);
        public static readonly Color LemonChiffon = Color.FromARGB(0xFFFFFACD);
        public static readonly Color LightBlue = Color.FromARGB(0xFFADD8E6);
        public static readonly Color LightCoral = Color.FromARGB(0xFFF08080);
        public static readonly Color LightCyan = Color.FromARGB(0xFFE0FFFF);
        public static readonly Color LightGoldenrodYellow = Color.FromARGB(0xFFFAFAD2);
        public static readonly Color LightGray = Color.FromARGB(0xFFD3D3D3);
        public static readonly Color LightGreen = Color.FromARGB(0xFF90EE90);
        public static readonly Color LightPink = Color.FromARGB(0xFFFFB6C1);
        public static readonly Color LightSalmon = Color.FromARGB(0xFFFFA07A);
        public static readonly Color LightSeaGreen = Color.FromARGB(0xFF20B2AA);
        public static readonly Color LightSkyBlue = Color.FromARGB(0xFF87CEFA);
        public static readonly Color LightSlateGray = Color.FromARGB(0xFF778899);
        public static readonly Color LightSteelBlue = Color.FromARGB(0xFFB0C4DE);
        public static readonly Color LightYellow = Color.FromARGB(0xFFFFFFE0);
        public static readonly Color Lime = Color.FromARGB(0xFF00FF00);
        public static readonly Color LimeGreen = Color.FromARGB(0xFF32CD32);
        public static readonly Color Linen = Color.FromARGB(0xFFFAF0E6);
        public static readonly Color Magenta = Color.FromARGB(0xFFFF00FF);
        public static readonly Color Maroon = Color.FromARGB(0xFF800000);
        public static readonly Color MediumAquamarine = Color.FromARGB(0xFF66CDAA);
        public static readonly Color MediumBlue = Color.FromARGB(0xFF0000CD);
        public static readonly Color MediumOrchid = Color.FromARGB(0xFFBA55D3);
        public static readonly Color MediumPurple = Color.FromARGB(0xFF9370DB);
        public static readonly Color MediumSeaGreen = Color.FromARGB(0xFF3CB371);
        public static readonly Color MediumSlateBlue = Color.FromARGB(0xFF7B68EE);
        public static readonly Color MediumSpringGreen = Color.FromARGB(0xFF00FA9A);
        public static readonly Color MediumTurquoise = Color.FromARGB(0xFF48D1CC);
        public static readonly Color MediumVioletRed = Color.FromARGB(0xFFC71585);
        public static readonly Color MidnightBlue = Color.FromARGB(0xFF191970);
        public static readonly Color MintCream = Color.FromARGB(0xFFF5FFFA);
        public static readonly Color MistyRose = Color.FromARGB(0xFFFFE4E1);
        public static readonly Color Moccasin = Color.FromARGB(0xFFFFE4B5);
        public static readonly Color NavajoWhite = Color.FromARGB(0xFFFFDEAD);
        public static readonly Color Navy = Color.FromARGB(0xFF000080);
        public static readonly Color OldLace = Color.FromARGB(0xFFFDF5E6);
        public static readonly Color Olive = Color.FromARGB(0xFF808000);
        public static readonly Color OliveDrab = Color.FromARGB(0xFF6B8E23);
        public static readonly Color Orange = Color.FromARGB(0xFFFFA500);
        public static readonly Color OrangeRed = Color.FromARGB(0xFFFF4500);
        public static readonly Color Orchid = Color.FromARGB(0xFFDA70D6);
        public static readonly Color PaleGoldenrod = Color.FromARGB(0xFFEEE8AA);
        public static readonly Color PaleGreen = Color.FromARGB(0xFF98FB98);
        public static readonly Color PaleTurquoise = Color.FromARGB(0xFFAFEEEE);
        public static readonly Color PaleVioletRed = Color.FromARGB(0xFFDB7093);
        public static readonly Color PapayaWhip = Color.FromARGB(0xFFFFEFD5);
        public static readonly Color PeachPuff = Color.FromARGB(0xFFFFDAB9);
        public static readonly Color Peru = Color.FromARGB(0xFFCD853F);
        public static readonly Color Pink = Color.FromARGB(0xFFFFC0CB);
        public static readonly Color Plum = Color.FromARGB(0xFFDDA0DD);
        public static readonly Color PowderBlue = Color.FromARGB(0xFFB0E0E6);
        public static readonly Color Purple = Color.FromARGB(0xFF800080);
        public static readonly Color Red = Color.FromARGB(0xFFFF0000);
        public static readonly Color RosyBrown = Color.FromARGB(0xFFBC8F8F);
        public static readonly Color RoyalBlue = Color.FromARGB(0xFF4169E1);
        public static readonly Color SaddleBrown = Color.FromARGB(0xFF8B4513);
        public static readonly Color Salmon = Color.FromARGB(0xFFFA8072);
        public static readonly Color SandyBrown = Color.FromARGB(0xFFF4A460);
        public static readonly Color SeaGreen = Color.FromARGB(0xFF2E8B57);
        public static readonly Color SeaShell = Color.FromARGB(0xFFFFF5EE);
        public static readonly Color Sienna = Color.FromARGB(0xFFA0522D);
        public static readonly Color Silver = Color.FromARGB(0xFFC0C0C0);
        public static readonly Color SkyBlue = Color.FromARGB(0xFF87CEEB);
        public static readonly Color SlateBlue = Color.FromARGB(0xFF6A5ACD);
        public static readonly Color SlateGray = Color.FromARGB(0xFF708090);
        public static readonly Color Snow = Color.FromARGB(0xFFFFFAFA);
        public static readonly Color SpringGreen = Color.FromARGB(0xFF00FF7F);
        public static readonly Color SteelBlue = Color.FromARGB(0xFF4682B4);
        public static readonly Color Tan = Color.FromARGB(0xFFD2B48C);
        public static readonly Color Teal = Color.FromARGB(0xFF008080);
        public static readonly Color Thistle = Color.FromARGB(0xFFD8BFD8);
        public static readonly Color Tomato = Color.FromARGB(0xFFFF6347);
        public static readonly Color Turquoise = Color.FromARGB(0xFF40E0D0);
        public static readonly Color Violet = Color.FromARGB(0xFFEE82EE);
        public static readonly Color Wheat = Color.FromARGB(0xFFF5DEB3);
        public static readonly Color White = Color.FromARGB(0xFFFFFFFF);
        public static readonly Color WhiteSmoke = Color.FromARGB(0xFFF5F5F5);
        public static readonly Color Yellow = Color.FromARGB(0xFFFFFF00);
        public static readonly Color YellowGreen = Color.FromARGB(0xFF9ACD32);
    }
}