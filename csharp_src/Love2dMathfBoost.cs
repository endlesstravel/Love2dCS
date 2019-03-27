/// <summary>
/// Love.Mathf boost code
/// <para>
/// copy adapt from XNA.Mathf
/// </para>
/// 
/// </summary>

namespace Love
{
    using System;
    using System.Linq;
    public partial class Mathf
    {
        /// <summary>
        /// Degrees-to-radians conversion constant.
        /// </summary>
        public const float Deg2Rad = 0.0174533f;
        /// <summary>
        /// Degrees-to-grad conversion constant.
        /// </summary>
        public const float Deg2Grad = 1.1111111f;
        /// <summary>
        /// A tiny floating point value.
        /// </summary>
        public const float Epsilon = 1.4013e-045f;
        /// <summary>
        /// Exponential e.
        /// </summary>
        public const float ExponentialE = 2.71828f;
        /// <summary>
        /// The golden ratio. Oooooh!
        /// </summary>
        public const float GoldenRatio = 1.61803f;
        /// <summary>
        /// Grad-to-degrees conversion constant.
        /// </summary>
        public const float Grad2Deg = 0.9f;
        /// <summary>
        /// Grad-to-radians conversion constant.
        /// </summary>
        public const float Grad2Rad = 0.015708f;
        /// <summary>
        /// A representation of positive infinity.
        /// </summary>
        public const float Infinity = 1.0f / 0.0f;
        /// <summary>
        /// A representation of negative infinity.
        /// </summary>
        public const float NegativeInfinity = -1.0f / 0.0f;
        /// <summary>
        /// The infamous 3.14159265358979... value.
        /// </summary>
        public const float PI = 3.14159f;
        /// <summary>
        /// Radians-to-degrees conversion constant.
        /// </summary>
        public const float Rad2Deg = 57.2958f;
        /// <summary>
        /// Radians-to-grad conversion constant.
        /// </summary>
        public const float Rad2Grad = 63.6619772f;
        /// <summary>
        /// The not-so-infamous TAU value.
        /// </summary>
        public const float TAU = PI * 2;

        /// <summary>
        /// Returns the absolute value of a.
        /// </summary>
        /// <param name="a">The value</param>
        public static int Abs(int a) { return Math.Abs(a); }
        /// <summary>
        /// Returns the absolute value of a.
        /// </summary>
        /// <param name="a">The value</param>
        public static float Abs(float a) { return (float)Math.Abs(a); }
        /// <summary>
        /// Returns the arc-cosine of a - the angle in radians whose cosine is a.
        /// </summary>
        /// <param name="a">The value</param>
        public static float Acos(float a) { return (float)Math.Acos(a); }
        /// <summary>
        /// Returns if the two values are approximately close to eachother
        /// </summary>
        /// <param name="a">First value</param>
        /// <param name="b">Second value</param>
        public static bool Approximately(float a, float b) { return RoughlyEqual(a, b, 0.1f); }
        /// <summary>
        /// Returns the arc-sine of a - the angle in radians whose sine is a.
        /// </summary>
        /// <param name="a">The value</param>
        public static float Asin(float a) { return (float)Math.Asin(a); }
        /// <summary>
        /// Returns the arc-tangent of a - the angle in radians whose tangent is a.
        /// </summary>
        /// <param name="a">The value</param>
        public static float Atan(float a) { return (float)Math.Atan(a); }
        /// <summary>
        /// Returns the angle in radians whose Tan is y/x.
        /// </summary>
        /// <param name="y">The y value</param>
        /// <param name="x">The x value</param>
        public static float Atan2(float y, float x) { return (float)Math.Atan2(y, x); }
        /// <summary>
        /// Returns the smallest integer greater to or equal to a.
        /// </summary>
        /// <param name="a">The value</param>
        public static float Ceil(float a) { return (float)Math.Ceiling(a); }
        /// <summary>
        /// Returns the smallest integer greater to or equal to a.
        /// </summary>
        /// <param name="a">The value</param>
        public static int CeilToInt(float a) { return (int)Ceil(a); }
        /// <summary>
        /// Clamps a value between a minimum int and maximum int value.
        /// </summary>
        /// <param name="value">The value</param>
        /// <param name="min">The minimum value</param>
        /// <param name="max">The maximum value</param>
        public static int Clamp(int value, int min, int max)
        {
            return (int)Clamp((float)value, (float)min, (float)max);
        }
        /// <summary>
        /// Clamps a value between a minimum float and maximum float value.
        /// </summary>
        /// <param name="value">The value</param>
        /// <param name="min">The minimum value</param>
        /// <param name="max">The maximum value</param>
        public static float Clamp(float value, float min, float max)
        {
            if (max <= min)
                return min;
            return value < min ? min : value > max ? max : value;
        }
        /// <summary>
        /// Clamps value between 0 and 1 and returns value.
        /// </summary>
        /// <param name="value">The value</param>
        public static float Clamp01(float value) { return value < 0 ? 0 : value > 1 ? 1 : value; }
        /// <summary>
        /// Returns the closest power of two to a value.
        /// </summary>
        /// <param name="a">The value</param>
        public static int ClosestPowerOfTwo(int a)
        {
            int b = NextPowerOfTwo(a),
                c = b / 2;
            return a - c < b - a ? c : b;
        }
        /// <summary>
        /// Returns the cosine of angle f in radians.
        /// </summary>
        /// <param name="a">The value</param>
        public static float Cos(float a) { return (float)Math.Cos(a); }
        /// <summary>
        /// Returns e raised to the specified power.
        /// </summary>
        /// <param name="power">The power</param>
        public static float Exp(float power) { return (float)Math.Exp(power); }
        /// <summary>
        /// Returns the largest integer smaller to or equal to a.
        /// </summary>
        /// <param name="a">The value</param>
        public static float Floor(float a) { return (float)Math.Floor(a); }
        /// <summary>
        /// Returns the largest integer smaller to or equal to a.
        /// </summary>
        /// <param name="a">The value</param>
        public static int FloorToInt(float a) { return (int)Floor(a); }
        /// <summary>
        /// Returns if the value is powered by two.
        /// </summary>
        /// <param name="value">A value</param>
        public static bool IsPowerOfTwo(int value) { return (value > 0) && ((value & (value - 1)) == 0); }
        /// <summary>
        /// Interpolates between from and to by t. t is clamped between 0 and 1.
        /// </summary>
        /// <param name="from">The from value</param>
        /// <param name="to">The to value</param>
        /// <param name="t">The t value</param>
        public static float Lerp(float from, float to, float t) { return t >= 1 ? to : t < 0 ? from : from + (to - from) * t; }
        /// <summary>
        /// Returns the natural (base e) logarithm of a specified value.
        /// </summary>
        /// <param name="value">The value</param>
        public static float Log(float value) { return (float)Math.Log(value); }
        /// <summary>
        /// Returns the base 10 logarithm of a specified value.
        /// </summary>
        /// <param name="value">The value</param>
        public static float Log10(float value) { return (float)Math.Log10(value); }
        /// <summary>
        /// Returns the largest of two integer values.
        /// </summary>
        /// <param name="a">First value</param>
        /// <param name="b">Second value</param>
        public static int Max(int a, int b) { return Math.Max(a, b); }
        /// <summary>
        /// Returns the largest of two float values.
        /// </summary>
        /// <param name="a">First value</param>
        /// <param name="b">Second value</param>
        public static float Max(float a, float b) { return Math.Max(a, b); }
        /// <summary>
        /// Returns the largest of a set of integer values.
        /// </summary>
        /// <param name="values">The set of values</param>
        public static int Max(params int[] values) { return values.Max(); }
        /// <summary>
        /// Returns the largest of a set of float values.
        /// </summary>
        /// <param name="values">The set of values</param>
        public static float Max(params float[] values) { return values.Max(); }
        /// <summary>
        /// Returns the smaller of two integer values.
        /// </summary>
        /// <param name="a">First value</param>
        /// <param name="b">Second value</param>
        public static int Min(int a, int b) { return Math.Min(a, b); }
        /// <summary>
        /// Returns the smaller of two float values.
        /// </summary>
        /// <param name="a">First value</param>
        /// <param name="b">Second value</param>
        public static float Min(float a, float b) { return Math.Min(a, b); }
        /// <summary>
        /// Get the next power of two after a value.
        /// </summary>
        /// <param name="a">The value</param>
        public static int NextPowerOfTwo(int a)
        {
            if (a < 0)
                return 0;
            a |= a >> 1;
            a |= a >> 2;
            a |= a >> 4;
            a |= a >> 8;
            a |= a >> 16;
            return a + 1;
        }
        /// <summary>
        /// Returns f raised to power p.
        /// </summary>
        /// <param name="f">The value to raise</param>
        /// <param name="p">The power</param>
        public static float Pow(float f, float p) { return (float)Math.Pow(f, p); }
        /// <summary>
        /// Compares two floating point values if they are similar.
        /// </summary>
        /// <param name="a">First value</param>
        /// <param name="b">Second value</param>
        /// <param name="threshold">The threshold of similarity</param>
        /// <returns>True if the values are similar, otherwise false.</returns>
        public static bool RoughlyEqual(float a, float b, float threshold = 0.01f) { return Mathf.Abs(a - b) <= threshold; }
        /// <summary>
        /// Returns f rounded to the nearest integer.
        /// </summary>
        /// <param name="f">The value</param>
        public static float Round(float f) { return (float)Math.Round(f); }
        /// <summary>
        /// Rounds a floating-point value to a specified number of fractional digits. 
        /// </summary>
        /// <param name="f">The value</param>
        /// <param name="decimals">The number of fractional digits to round to</param>
        public static float Round(float f, int decimals) { return (float)Math.Round(f, decimals); }
        /// <summary>
        /// Rounds a floating-point value to a specified number of fractional digits. A parameter specifies how to round a value if it is midway between two other numbers.
        /// </summary>
        /// <param name="f">The value</param>
        /// <param name="decimals">The number of fractional digits to round to</param>
        /// <param name="mode">The rounding mode to use</param>
        public static float Round(float f, int decimals, MidpointRounding mode) { return (float)Math.Round(f, decimals, mode); }
        /// <summary>
        /// Returns f rounded to the nearest integer.
        /// </summary>
        /// <param name="f">The value to round</param>
        public static int RoundToInt(float f) { return (int)Round(f); }
        /// <summary>
        ///  Rounds a floating-point value. A parameter specifies how to round a value if it is midway between two other numbers.
        /// </summary>
        /// <param name="f">The value</param>
        /// <param name="mode">The rounding mode to use</param>
        public static int RoundToInt(float f, MidpointRounding mode) { return (int)Round(f, 0, mode); }
        /// <summary>
        /// Returns the sign of f.
        /// </summary>
        /// <param name="f">The value</param>
        public static float Sign(float f) { return (float)Math.Sign(f); }
        /// <summary>
        /// Returns the sine of angle f in radians.
        /// </summary>
        /// <param name="f">The value</param>
        public static float Sin(float f) { return (float)Math.Sin(f); }
        /// <summary>
        /// Returns square root of f.
        /// </summary>
        /// <param name="f">The value</param>
        public static float Sqrt(float f) { return (float)Math.Sqrt(f); }
        /// <summary>
        /// Returns the tangent of angle f in radians.
        /// </summary>
        /// <param name="f">The value</param>
        public static float Tan(float f) { return (float)Math.Tan(f); }


        /// <summary>
        /// Interpolates between min and max with smoothing at the limits.
        /// This function interpolates between min and max in a similar way to Lerp.
        /// However, the interpolation will gradually speed up from the start and slow down toward the end.
        /// This is useful for creating natural-looking animation, fading and other transitions.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static float SmoothStep(float value1, float value2, float amount)
        {
            // It is expected that 0 < amount < 1
            // If amount < 0, return value1
            // If amount > 1, return value2
            float result = Mathf.Clamp(amount, 0f, 1f);
            result = Mathf.Hermite(value1, 0f, value2, 0f, result);
            return result;
        }

        public static float Hermite(float value1, float tangent1, float value2, float tangent2, float amount)
        {
            // All transformed to double not to lose precission
            // Otherwise, for high numbers of param:amount the result is NaN instead of Infinity
            float v1 = value1, v2 = value2, t1 = tangent1, t2 = tangent2, s = amount, result;
            float sCubed = s * s * s;
            float sSquared = s * s;

            if (amount == 0f)
                result = value1;
            else if (amount == 1f)
                result = value2;
            else
                result = (2 * v1 - 2 * v2 + t2 + t1) * sCubed +
                    (3 * v2 - 3 * v1 - 2 * t1 - t2) * sSquared +
                    t1 * s +
                    v1;
            return result;
        }

        public static float Barycentric(float value1, float value2, float value3, float amount1, float amount2)
        {
            return value1 + (value2 - value1) * amount1 + (value3 - value1) * amount2;
        }

        /// <summary>
        /// Using formula from http://www.mvps.org/directx/articles/catmull/
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <param name="value3"></param>
        /// <param name="value4"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static float CatmullRom(float value1, float value2, float value3, float value4, float amount)
        {
            // Using formula from http://www.mvps.org/directx/articles/catmull/
            // Internally using doubles not to lose precission
            float amountSquared = amount * amount;
            float amountCubed = amountSquared * amount;
            return (0.5f * (2.0f * value2 +
                (value3 - value1) * amount +
                (2.0f * value1 - 5.0f * value2 + 4.0f * value3 - value4) * amountSquared +
                (3.0f * value2 - value1 - 3.0f * value3 + value4) * amountCubed));
        }
    }
}
