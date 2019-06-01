using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Love.Misc
{
    ///// <summary>
    ///// A structure encapsulating a 4x4 matrix.
    ///// </summary>
    //[StructLayout(LayoutKind.Sequential)]
    //public struct Matrix4x4
    //{
    //    private const float BillboardEpsilon = 1e-4f;
    //    private const float BillboardMinAngle = 1.0f - (0.1f * (Mathf.PI / 180.0f)); // 0.1 degrees
    //    private const float DecomposeEpsilon = 0.0001f;

    //    #region Public Fields
    //    /// <summary>
    //    /// Value at row 1, column 1 of the matrix.
    //    /// </summary>
    //    public float M11;
    //    /// <summary>
    //    /// Value at row 1, column 2 of the matrix.
    //    /// </summary>
    //    public float M12;
    //    /// <summary>
    //    /// Value at row 1, column 3 of the matrix.
    //    /// </summary>
    //    public float M13;
    //    /// <summary>
    //    /// Value at row 1, column 4 of the matrix.
    //    /// </summary>
    //    public float M14;

    //    /// <summary>
    //    /// Value at row 2, column 1 of the matrix.
    //    /// </summary>
    //    public float M21;
    //    /// <summary>
    //    /// Value at row 2, column 2 of the matrix.
    //    /// </summary>
    //    public float M22;
    //    /// <summary>
    //    /// Value at row 2, column 3 of the matrix.
    //    /// </summary>
    //    public float M23;
    //    /// <summary>
    //    /// Value at row 2, column 4 of the matrix.
    //    /// </summary>
    //    public float M24;

    //    /// <summary>
    //    /// Value at row 3, column 1 of the matrix.
    //    /// </summary>
    //    public float M31;
    //    /// <summary>
    //    /// Value at row 3, column 2 of the matrix.
    //    /// </summary>
    //    public float M32;
    //    /// <summary>
    //    /// Value at row 3, column 3 of the matrix.
    //    /// </summary>
    //    public float M33;
    //    /// <summary>
    //    /// Value at row 3, column 4 of the matrix.
    //    /// </summary>
    //    public float M34;

    //    /// <summary>
    //    /// Value at row 4, column 1 of the matrix.
    //    /// </summary>
    //    public float M41;
    //    /// <summary>
    //    /// Value at row 4, column 2 of the matrix.
    //    /// </summary>
    //    public float M42;
    //    /// <summary>
    //    /// Value at row 4, column 3 of the matrix.
    //    /// </summary>
    //    public float M43;
    //    /// <summary>
    //    /// Value at row 4, column 4 of the matrix.
    //    /// </summary>
    //    public float M44;
    //    #endregion Public Fields

    //    private static readonly Matrix4x4 _identity = new Matrix4x4
    //    (
    //        1f, 0f, 0f, 0f,
    //        0f, 1f, 0f, 0f,
    //        0f, 0f, 1f, 0f,
    //        0f, 0f, 0f, 1f
    //    );

    //    /// <summary>
    //    /// Returns the multiplicative identity matrix.
    //    /// </summary>
    //    public static Matrix4x4 Identity
    //    {
    //        get { return _identity; }
    //    }

    //    /// <summary>
    //    /// Returns whether the matrix is the identity matrix.
    //    /// </summary>
    //    public bool IsIdentity
    //    {
    //        get
    //        {
    //            return M11 == 1f && M22 == 1f && M33 == 1f && M44 == 1f && // Check diagonal element first for early out.
    //                                M12 == 0f && M13 == 0f && M14 == 0f &&
    //                   M21 == 0f && M23 == 0f && M24 == 0f &&
    //                   M31 == 0f && M32 == 0f && M34 == 0f &&
    //                   M41 == 0f && M42 == 0f && M43 == 0f;
    //        }
    //    }

    //    /// <summary>
    //    /// Gets or sets the translation component of this matrix.
    //    /// </summary>
    //    public Vector3 Translation
    //    {
    //        get
    //        {
    //            return new Vector3(M41, M42, M43);
    //        }
    //        set
    //        {
    //            M41 = value.X;
    //            M42 = value.Y;
    //            M43 = value.Z;
    //        }
    //    }

    //    /// <summary>
    //    /// Constructs a Matrix4x4 from the given components.
    //    /// </summary>
    //    public Matrix4x4(float m11, float m12, float m13, float m14,
    //                     float m21, float m22, float m23, float m24,
    //                     float m31, float m32, float m33, float m34,
    //                     float m41, float m42, float m43, float m44)
    //    {
    //        this.M11 = m11;
    //        this.M12 = m12;
    //        this.M13 = m13;
    //        this.M14 = m14;

    //        this.M21 = m21;
    //        this.M22 = m22;
    //        this.M23 = m23;
    //        this.M24 = m24;

    //        this.M31 = m31;
    //        this.M32 = m32;
    //        this.M33 = m33;
    //        this.M34 = m34;

    //        this.M41 = m41;
    //        this.M42 = m42;
    //        this.M43 = m43;
    //        this.M44 = m44;
    //    }




    //    /// <summary>
    //    /// Creates a translation matrix.
    //    /// </summary>
    //    /// <param name="xPosition">The amount to translate on the X-axis.</param>
    //    /// <param name="yPosition">The amount to translate on the Y-axis.</param>
    //    /// <param name="zPosition">The amount to translate on the Z-axis.</param>
    //    /// <returns>The translation matrix.</returns>
    //    public static Matrix4x4 CreateTranslation(float xPosition, float yPosition, float zPosition)
    //    {
    //        Matrix4x4 result;

    //        result.M11 = 1.0f;
    //        result.M12 = 0.0f;
    //        result.M13 = 0.0f;
    //        result.M14 = 0.0f;
    //        result.M21 = 0.0f;
    //        result.M22 = 1.0f;
    //        result.M23 = 0.0f;
    //        result.M24 = 0.0f;
    //        result.M31 = 0.0f;
    //        result.M32 = 0.0f;
    //        result.M33 = 1.0f;
    //        result.M34 = 0.0f;

    //        result.M41 = xPosition;
    //        result.M42 = yPosition;
    //        result.M43 = zPosition;
    //        result.M44 = 1.0f;

    //        return result;
    //    }

    //    /// <summary>
    //    /// Creates a scaling matrix.
    //    /// </summary>
    //    /// <param name="xScale">Value to scale by on the X-axis.</param>
    //    /// <param name="yScale">Value to scale by on the Y-axis.</param>
    //    /// <param name="zScale">Value to scale by on the Z-axis.</param>
    //    /// <returns>The scaling matrix.</returns>
    //    public static Matrix4x4 CreateScale(float xScale, float yScale, float zScale)
    //    {
    //        Matrix4x4 result;

    //        result.M11 = xScale;
    //        result.M12 = 0.0f;
    //        result.M13 = 0.0f;
    //        result.M14 = 0.0f;
    //        result.M21 = 0.0f;
    //        result.M22 = yScale;
    //        result.M23 = 0.0f;
    //        result.M24 = 0.0f;
    //        result.M31 = 0.0f;
    //        result.M32 = 0.0f;
    //        result.M33 = zScale;
    //        result.M34 = 0.0f;
    //        result.M41 = 0.0f;
    //        result.M42 = 0.0f;
    //        result.M43 = 0.0f;
    //        result.M44 = 1.0f;

    //        return result;
    //    }

    //    /// <summary>
    //    /// Creates a matrix for rotating points around the Z-axis.
    //    /// </summary>
    //    /// <param name="radians">The amount, in radians, by which to rotate around the Z-axis.</param>
    //    /// <returns>The rotation matrix.</returns>
    //    public static Matrix4x4 CreateRotationZ(float radians)
    //    {
    //        Matrix4x4 result;

    //        float c = Mathf.Cos(radians);
    //        float s = Mathf.Sin(radians);

    //        // [  c  s  0  0 ]
    //        // [ -s  c  0  0 ]
    //        // [  0  0  1  0 ]
    //        // [  0  0  0  1 ]
    //        result.M11 = c;
    //        result.M12 = s;
    //        result.M13 = 0.0f;
    //        result.M14 = 0.0f;
    //        result.M21 = -s;
    //        result.M22 = c;
    //        result.M23 = 0.0f;
    //        result.M24 = 0.0f;
    //        result.M31 = 0.0f;
    //        result.M32 = 0.0f;
    //        result.M33 = 1.0f;
    //        result.M34 = 0.0f;
    //        result.M41 = 0.0f;
    //        result.M42 = 0.0f;
    //        result.M43 = 0.0f;
    //        result.M44 = 1.0f;

    //        return result;
    //    }
    //    public static Matrix4x4 operator *(Matrix4x4 value1, Matrix4x4 value2)
    //    {
    //        Matrix4x4 m;

    //        // First row
    //        m.M11 = value1.M11 * value2.M11 + value1.M12 * value2.M21 + value1.M13 * value2.M31 + value1.M14 * value2.M41;
    //        m.M12 = value1.M11 * value2.M12 + value1.M12 * value2.M22 + value1.M13 * value2.M32 + value1.M14 * value2.M42;
    //        m.M13 = value1.M11 * value2.M13 + value1.M12 * value2.M23 + value1.M13 * value2.M33 + value1.M14 * value2.M43;
    //        m.M14 = value1.M11 * value2.M14 + value1.M12 * value2.M24 + value1.M13 * value2.M34 + value1.M14 * value2.M44;

    //        // Second row
    //        m.M21 = value1.M21 * value2.M11 + value1.M22 * value2.M21 + value1.M23 * value2.M31 + value1.M24 * value2.M41;
    //        m.M22 = value1.M21 * value2.M12 + value1.M22 * value2.M22 + value1.M23 * value2.M32 + value1.M24 * value2.M42;
    //        m.M23 = value1.M21 * value2.M13 + value1.M22 * value2.M23 + value1.M23 * value2.M33 + value1.M24 * value2.M43;
    //        m.M24 = value1.M21 * value2.M14 + value1.M22 * value2.M24 + value1.M23 * value2.M34 + value1.M24 * value2.M44;

    //        // Third row
    //        m.M31 = value1.M31 * value2.M11 + value1.M32 * value2.M21 + value1.M33 * value2.M31 + value1.M34 * value2.M41;
    //        m.M32 = value1.M31 * value2.M12 + value1.M32 * value2.M22 + value1.M33 * value2.M32 + value1.M34 * value2.M42;
    //        m.M33 = value1.M31 * value2.M13 + value1.M32 * value2.M23 + value1.M33 * value2.M33 + value1.M34 * value2.M43;
    //        m.M34 = value1.M31 * value2.M14 + value1.M32 * value2.M24 + value1.M33 * value2.M34 + value1.M34 * value2.M44;

    //        // Fourth row
    //        m.M41 = value1.M41 * value2.M11 + value1.M42 * value2.M21 + value1.M43 * value2.M31 + value1.M44 * value2.M41;
    //        m.M42 = value1.M41 * value2.M12 + value1.M42 * value2.M22 + value1.M43 * value2.M32 + value1.M44 * value2.M42;
    //        m.M43 = value1.M41 * value2.M13 + value1.M42 * value2.M23 + value1.M43 * value2.M33 + value1.M44 * value2.M43;
    //        m.M44 = value1.M41 * value2.M14 + value1.M42 * value2.M24 + value1.M43 * value2.M34 + value1.M44 * value2.M44;

    //        return m;
    //    }

    //    public static Vector3 TransformVector3(Vector3 vector, Matrix4x4 matrix)
    //    {
    //        return new Vector3(
    //            vector.X * matrix.M11 + vector.Y * matrix.M21 + vector.Z * matrix.M31 + 1 * matrix.M41,
    //            vector.X * matrix.M12 + vector.Y * matrix.M22 + vector.Z * matrix.M32 + 1 * matrix.M42,
    //            vector.X * matrix.M13 + vector.Y * matrix.M23 + vector.Z * matrix.M33 + 1 * matrix.M43);
    //    }



    //    /// <summary>
    //    /// Attempts to calculate the inverse of the given matrix. If successful, result will contain the inverted matrix.
    //    /// </summary>
    //    /// <param name="matrix">The source matrix to invert.</param>
    //    /// <param name="result">If successful, contains the inverted matrix.</param>
    //    /// <returns>True if the source matrix could be inverted; False otherwise.</returns>
    //    public static bool Invert(Matrix4x4 matrix, out Matrix4x4 result)
    //    {
    //        //                                       -1
    //        // If you have matrix M, inverse Matrix M   can compute
    //        //
    //        //     -1       1
    //        //    M   = --------- A
    //        //            det(M)
    //        //
    //        // A is adjugate (adjoint) of M, where,
    //        //
    //        //      T
    //        // A = C
    //        //
    //        // C is Cofactor matrix of M, where,
    //        //           i + j
    //        // C   = (-1)      * det(M  )
    //        //  ij                    ij
    //        //
    //        //     [ a b c d ]
    //        // M = [ e f g h ]
    //        //     [ i j k l ]
    //        //     [ m n o p ]
    //        //
    //        // First Row
    //        //           2 | f g h |
    //        // C   = (-1)  | j k l | = + ( f ( kp - lo ) - g ( jp - ln ) + h ( jo - kn ) )
    //        //  11         | n o p |
    //        //
    //        //           3 | e g h |
    //        // C   = (-1)  | i k l | = - ( e ( kp - lo ) - g ( ip - lm ) + h ( io - km ) )
    //        //  12         | m o p |
    //        //
    //        //           4 | e f h |
    //        // C   = (-1)  | i j l | = + ( e ( jp - ln ) - f ( ip - lm ) + h ( in - jm ) )
    //        //  13         | m n p |
    //        //
    //        //           5 | e f g |
    //        // C   = (-1)  | i j k | = - ( e ( jo - kn ) - f ( io - km ) + g ( in - jm ) )
    //        //  14         | m n o |
    //        //
    //        // Second Row
    //        //           3 | b c d |
    //        // C   = (-1)  | j k l | = - ( b ( kp - lo ) - c ( jp - ln ) + d ( jo - kn ) )
    //        //  21         | n o p |
    //        //
    //        //           4 | a c d |
    //        // C   = (-1)  | i k l | = + ( a ( kp - lo ) - c ( ip - lm ) + d ( io - km ) )
    //        //  22         | m o p |
    //        //
    //        //           5 | a b d |
    //        // C   = (-1)  | i j l | = - ( a ( jp - ln ) - b ( ip - lm ) + d ( in - jm ) )
    //        //  23         | m n p |
    //        //
    //        //           6 | a b c |
    //        // C   = (-1)  | i j k | = + ( a ( jo - kn ) - b ( io - km ) + c ( in - jm ) )
    //        //  24         | m n o |
    //        //
    //        // Third Row
    //        //           4 | b c d |
    //        // C   = (-1)  | f g h | = + ( b ( gp - ho ) - c ( fp - hn ) + d ( fo - gn ) )
    //        //  31         | n o p |
    //        //
    //        //           5 | a c d |
    //        // C   = (-1)  | e g h | = - ( a ( gp - ho ) - c ( ep - hm ) + d ( eo - gm ) )
    //        //  32         | m o p |
    //        //
    //        //           6 | a b d |
    //        // C   = (-1)  | e f h | = + ( a ( fp - hn ) - b ( ep - hm ) + d ( en - fm ) )
    //        //  33         | m n p |
    //        //
    //        //           7 | a b c |
    //        // C   = (-1)  | e f g | = - ( a ( fo - gn ) - b ( eo - gm ) + c ( en - fm ) )
    //        //  34         | m n o |
    //        //
    //        // Fourth Row
    //        //           5 | b c d |
    //        // C   = (-1)  | f g h | = - ( b ( gl - hk ) - c ( fl - hj ) + d ( fk - gj ) )
    //        //  41         | j k l |
    //        //
    //        //           6 | a c d |
    //        // C   = (-1)  | e g h | = + ( a ( gl - hk ) - c ( el - hi ) + d ( ek - gi ) )
    //        //  42         | i k l |
    //        //
    //        //           7 | a b d |
    //        // C   = (-1)  | e f h | = - ( a ( fl - hj ) - b ( el - hi ) + d ( ej - fi ) )
    //        //  43         | i j l |
    //        //
    //        //           8 | a b c |
    //        // C   = (-1)  | e f g | = + ( a ( fk - gj ) - b ( ek - gi ) + c ( ej - fi ) )
    //        //  44         | i j k |
    //        //
    //        // Cost of operation
    //        // 53 adds, 104 muls, and 1 div.
    //        float a = matrix.M11, b = matrix.M12, c = matrix.M13, d = matrix.M14;
    //        float e = matrix.M21, f = matrix.M22, g = matrix.M23, h = matrix.M24;
    //        float i = matrix.M31, j = matrix.M32, k = matrix.M33, l = matrix.M34;
    //        float m = matrix.M41, n = matrix.M42, o = matrix.M43, p = matrix.M44;

    //        float kp_lo = k * p - l * o;
    //        float jp_ln = j * p - l * n;
    //        float jo_kn = j * o - k * n;
    //        float ip_lm = i * p - l * m;
    //        float io_km = i * o - k * m;
    //        float in_jm = i * n - j * m;

    //        float a11 = +(f * kp_lo - g * jp_ln + h * jo_kn);
    //        float a12 = -(e * kp_lo - g * ip_lm + h * io_km);
    //        float a13 = +(e * jp_ln - f * ip_lm + h * in_jm);
    //        float a14 = -(e * jo_kn - f * io_km + g * in_jm);

    //        float det = a * a11 + b * a12 + c * a13 + d * a14;

    //        if (Mathf.Abs(det) < float.Epsilon)
    //        {
    //            result = new Matrix4x4(float.NaN, float.NaN, float.NaN, float.NaN,
    //                                   float.NaN, float.NaN, float.NaN, float.NaN,
    //                                   float.NaN, float.NaN, float.NaN, float.NaN,
    //                                   float.NaN, float.NaN, float.NaN, float.NaN);
    //            return false;
    //        }

    //        float invDet = 1.0f / det;

    //        result.M11 = a11 * invDet;
    //        result.M21 = a12 * invDet;
    //        result.M31 = a13 * invDet;
    //        result.M41 = a14 * invDet;

    //        result.M12 = -(b * kp_lo - c * jp_ln + d * jo_kn) * invDet;
    //        result.M22 = +(a * kp_lo - c * ip_lm + d * io_km) * invDet;
    //        result.M32 = -(a * jp_ln - b * ip_lm + d * in_jm) * invDet;
    //        result.M42 = +(a * jo_kn - b * io_km + c * in_jm) * invDet;

    //        float gp_ho = g * p - h * o;
    //        float fp_hn = f * p - h * n;
    //        float fo_gn = f * o - g * n;
    //        float ep_hm = e * p - h * m;
    //        float eo_gm = e * o - g * m;
    //        float en_fm = e * n - f * m;

    //        result.M13 = +(b * gp_ho - c * fp_hn + d * fo_gn) * invDet;
    //        result.M23 = -(a * gp_ho - c * ep_hm + d * eo_gm) * invDet;
    //        result.M33 = +(a * fp_hn - b * ep_hm + d * en_fm) * invDet;
    //        result.M43 = -(a * fo_gn - b * eo_gm + c * en_fm) * invDet;

    //        float gl_hk = g * l - h * k;
    //        float fl_hj = f * l - h * j;
    //        float fk_gj = f * k - g * j;
    //        float el_hi = e * l - h * i;
    //        float ek_gi = e * k - g * i;
    //        float ej_fi = e * j - f * i;

    //        result.M14 = -(b * gl_hk - c * fl_hj + d * fk_gj) * invDet;
    //        result.M24 = +(a * gl_hk - c * el_hi + d * ek_gi) * invDet;
    //        result.M34 = -(a * fl_hj - b * el_hi + d * ej_fi) * invDet;
    //        result.M44 = +(a * fk_gj - b * ek_gi + c * ej_fi) * invDet;

    //        return true;
    //    }

    //}





    ///// <summary>
    //// | e0 e4 e8  e12 |
    //// | e1 e5 e9  e13 |
    //// | e2 e6 e10 e14 |
    //// | e3 e7 e11 e15 |
    ///// </summary>
    //[StructLayout(LayoutKind.Sequential)]
    //public struct Matrix4x4
    //{
    // public float e0, e1, e2, e3, e4, e5, e6, e7, e8, e9, e10, e11, e12, e13, e14, e15;
    //    //                 | e0 e4 e8  e12 |
    //    //                 | e1 e5 e9  e13 |
    //    //                 | e2 e6 e10 e14 |
    //    //                 | e3 e7 e11 e15 |
    //    // | e0 e4 e8  e12 |
    //    // | e1 e5 e9  e13 |
    //    // | e2 e6 e10 e14 |
    //    // | e3 e7 e11 e15 |

    //    public static void multiply(ref Matrix4x4 a, ref Matrix4x4 b, ref Matrix4x4 t)
    //    {
    //        t.e0  = (a.e0*b.e0)  + (a.e4*b.e1)  + (a.e8*b.e2)  + (a.e12*b.e3);
    //        t.e4  = (a.e0*b.e4)  + (a.e4*b.e5)  + (a.e8*b.e6)  + (a.e12*b.e7);
    //        t.e8  = (a.e0*b.e8)  + (a.e4*b.e9)  + (a.e8*b.e10) + (a.e12*b.e11);
    //        t.e12 = (a.e0*b.e12) + (a.e4*b.e13) + (a.e8*b.e14) + (a.e12*b.e15);
    //        t.e1  = (a.e1*b.e0)  + (a.e5*b.e1)  + (a.e9*b.e2)  + (a.e13*b.e3);
    //        t.e5  = (a.e1*b.e4)  + (a.e5*b.e5)  + (a.e9*b.e6)  + (a.e13*b.e7);
    //        t.e9  = (a.e1*b.e8)  + (a.e5*b.e9)  + (a.e9*b.e10) + (a.e13*b.e11);
    //        t.e13 = (a.e1*b.e12) + (a.e5*b.e13) + (a.e9*b.e14) + (a.e13*b.e15);
    //        t.e2  = (a.e2*b.e0)  + (a.e6*b.e1)  + (a.e10*b.e2)  + (a.e14*b.e3);
    //        t.e6  = (a.e2*b.e4)  + (a.e6*b.e5)  + (a.e10*b.e6)  + (a.e14*b.e7);
    //        t.e10 = (a.e2*b.e8)  + (a.e6*b.e9)  + (a.e10*b.e10) + (a.e14*b.e11);
    //        t.e14 = (a.e2*b.e12) + (a.e6*b.e13) + (a.e10*b.e14) + (a.e14*b.e15);
    //        t.e3  = (a.e3*b.e0)  + (a.e7*b.e1)  + (a.e11*b.e2)  + (a.e15*b.e3);
    //        t.e7  = (a.e3*b.e4)  + (a.e7*b.e5)  + (a.e11*b.e6)  + (a.e15*b.e7);
    //        t.e11 = (a.e3*b.e8)  + (a.e7*b.e9)  + (a.e11*b.e10) + (a.e15*b.e11);
    //        t.e15 = (a.e3*b.e12) + (a.e7*b.e13) + (a.e11*b.e14) + (a.e15*b.e15);
    //    }


    //    public Matrix4x4(float[] elements)
    //    {
    //        e0 = e1 = e2 = e3 = e4 = e5 = e6 = e7 = e8 = e9 = e10 = e11 = e12 = e13 = e14 = e15 = 0;
    //        e15 = e10 = e5 = e0 = 1;
    //        // memcpy(e, elements, sizeof(float) * 16);
    //        setIdentity();
    //    }

    //    public Matrix4x4(float t00, float t10, float t01, float t11, float x, float y)
    //    {
    //        e0 = e1 = e2 = e3 = e4 = e5 = e6 = e7 = e8 = e9 = e10 = e11 = e12 = e13 = e14 = e15 = 0;
    //        setRawTransformation(t00, t10, t01, t11, x, y);
    //    }

    //    public Matrix4x4(ref Matrix4x4 other)
    //    {
    //        e0 = other.e0; e1 = other.e1; e2 = other.e2; e3 = other.e3;
    //        e4 = other.e4; e5 = other.e5; e6 = other.e6; e7 = other.e7;
    //        e8 = other.e8; e9 = other.e9; e10 = other.e10; e11 = other.e11;
    //        e12 = other.e12; e13 = other.e13; e14 = other.e14; e15 = other.e15;
    //    }

    //    public Matrix4x4(ref Matrix4x4 a, ref Matrix4x4 b)
    //    {
    //        e0 = e1 = e2 = e3 = e4 = e5 = e6 = e7 = e8 = e9 = e10 = e11 = e12 = e13 = e14 = e15 = 0;
    //        multiply(ref a, ref b, ref this);
    //    }

    //    public Matrix4x4(float x, float y, float angle, float sx, float sy, float ox, float oy, float kx, float ky)
    //    {
    //        e0 = e1 = e2 = e3 = e4 = e5 = e6 = e7 = e8 = e9 = e10 = e11 = e12 = e13 = e14 = e15 = 0;
    //        setTransformation(x, y, angle, sx, sy, ox, oy, kx, ky);
    //    }

    //    public void multiply(Matrix44 other)
    //    {
    //    }
    //    //public static Matrix4x4 operator* (Matrix4x4 a, Matrix4x4 m)
    //    //{
    //    //    var matrix = new Matrix4x4();
    //    //    multiply(ref this, ref m, ref matrix);
    //    //    return matrix;
    //    //}
    //    // public static void operator *=(in Matrix4x4 m)
    //    // {
    //    //     float t16;
    //    //     multiply(*this, m, t);
    //    //     memcpy(this->e, t, sizeof(float)*16);
    //    // }
    //    // const float *Matrix4x4::getElements() const
    //    // {
    //    //     return e;
    //    // }

    //    public void setIdentity()
    //    {
    //        e0 = e1 = e2 = e3 = e4 = e5 = e6 = e7 = e8 = e9 = e10 = e11 = e12 = e13 = e14 = e15 = 0;
    //        e15 = e10 = e5 = e0 = 1;
    //    }

    //    public void SetToZero()
    //    {
    //        e0 = e1 = e2 = e3 = e4 = e5 = e6 = e7 = e8 = e9 = e10 = e11 = e12 = e13 = e14 = e15 = 0;
    //    }

    //    void setTranslation(float x, float y)
    //    {
    //        setIdentity();
    //        e12 = x;
    //        e13 = y;
    //    }

    //    void setRotation(float rad)
    //    {
    //        setIdentity();
    //        float c = Mathf.Cos(rad), s = Mathf.Sin(rad);
    //        e0 = c;
    //        e4 = -s;
    //        e1 = s;
    //        e5 = c;
    //    }

    //    void setScale(float sx, float sy)
    //    {
    //        setIdentity();
    //        e0 = sx;
    //        e5 = sy;
    //    }

    //    void setShear(float kx, float ky)
    //    {
    //        setIdentity();
    //        e1 = ky;
    //        e4 = kx;
    //    }

    //    Vector2 getApproximateScale() => new Vector2(Mathf.Sqrt(e0 * e0 + e4 * e4), Mathf.Sqrt(e1 * e1 + e5 * e5));

    //    public void setRawTransformation(float t00, float t10, float t01, float t11, float x, float y)
    //    {
    //        SetToZero(); // zero out matrix
    //        e10 = e15 = 1.0f;
    //        e0 = t00;
    //        e1 = t10;
    //        e4 = t01;
    //        e5 = t11;
    //        e12 = x;
    //        e13 = y;
    //    }

    //    void setTransformation(float x, float y, float angle, float sx, float sy, float ox, float oy, float kx, float ky)
    //    {
    //        SetToZero(); // zero out matrix
    //        float c = Mathf.Cos(angle), s = Mathf.Sin(angle);
    //        // matrix multiplication carried out on paper:
    //        // |1     x| |c -s    | |sx       | | 1 ky    | |1     -ox|
    //        // |  1   y| |s  c    | |   sy    | |kx  1    | |  1   -oy|
    //        // |    1  | |     1  | |      1  | |      1  | |    1    |
    //        // |      1| |       1| |        1| |        1| |       1 |
    //        //   move      rotate      scale       skew       origin
    //        e10 = e15 = 1.0f;
    //        e0  = c * sx - ky * s * sy; // = a
    //        e1  = s * sx + ky * c * sy; // = b
    //        e4  = kx * c * sx - s * sy; // = c
    //        e5  = kx * s * sx + c * sy; // = d
    //        e12 = x - ox * e0 - oy * e4;
    //        e13 = y - ox * e1 - oy * e5;
    //    }

    //    void translate(float x, float y)
    //    {
    //        Matrix4x4 t;
    //        t.setTranslation(x, y);
    //        this->operator *=(t);
    //    }

    //    void rotate(float rad)
    //    {
    //        Matrix4x4 t;
    //        t.setRotation(rad);
    //        this->operator *=(t);
    //    }

    //    void scale(float sx, float sy)
    //    {
    //        Matrix4x4 t;
    //        t.setScale(sx, sy);
    //        this->operator *=(t);
    //    }

    //    void shear(float kx, float ky)
    //    {
    //        Matrix4x4 t;
    //        t.setShear(kx,ky);
    //        this->operator *=(t);
    //    }

    //    bool isAffine2DTransform() const
    //    {
    //        return fabsf(e2 + e3 + e6 + e7 + e8 + e9 + e11 + e14) < 0.00001f
    //            && fabsf(e10 + e15 - 2.0f) < 0.00001f;
    //    }

    //    public Matrix4x4 inverse()
    //    {
    //        Matrix4x4 inv;

    //        inv.e0 = e5  * e10 * e15 -
    //                e5  * e11 * e14 -
    //                e9  * e6  * e15 +
    //                e9  * e7  * e14 +
    //                e13 * e6  * e11 -
    //                e13 * e7  * e10;

    //        inv.e4 = -e4  * e10 * e15 +
    //                    e4  * e11 * e14 +
    //                    e8  * e6  * e15 -
    //                    e8  * e7  * e14 -
    //                    e12 * e6  * e11 +
    //                    e12 * e7  * e10;

    //        inv.e8 = e4  * e9  * e15 -
    //                e4  * e11 * e13 -
    //                e8  * e5  * e15 +
    //                e8  * e7  * e13 +
    //                e12 * e5  * e11 -
    //                e12 * e7  * e9;

    //        inv.e12 = -e4  * e9  * e14 +
    //                    e4  * e10 * e13 +
    //                    e8  * e5  * e14 -
    //                    e8  * e6  * e13 -
    //                    e12 * e5  * e10 +
    //                    e12 * e6  * e9;

    //        inv.e1 = -e1  * e10 * e15 +
    //                    e1  * e11 * e14 +
    //                    e9  * e2  * e15 -
    //                    e9  * e3  * e14 -
    //                    e13 * e2  * e11 +
    //                    e13 * e3  * e10;

    //        inv.e5 = e0  * e10 * e15 -
    //                e0  * e11 * e14 -
    //                e8  * e2  * e15 +
    //                e8  * e3  * e14 +
    //                e12 * e2  * e11 -
    //                e12 * e3  * e10;

    //        inv.e9 = -e0  * e9  * e15 +
    //                    e0  * e11 * e13 +
    //                    e8  * e1  * e15 -
    //                    e8  * e3  * e13 -
    //                    e12 * e1  * e11 +
    //                    e12 * e3  * e9;

    //        inv.e13 = e0  * e9  * e14 -
    //                    e0  * e10 * e13 -
    //                    e8  * e1  * e14 +
    //                    e8  * e2  * e13 +
    //                    e12 * e1  * e10 -
    //                    e12 * e2  * e9;

    //        inv.e2 = e1  * e6 * e15 -
    //                e1  * e7 * e14 -
    //                e5  * e2 * e15 +
    //                e5  * e3 * e14 +
    //                e13 * e2 * e7 -
    //                e13 * e3 * e6;

    //        inv.e6 = -e0  * e6 * e15 +
    //                    e0  * e7 * e14 +
    //                    e4  * e2 * e15 -
    //                    e4  * e3 * e14 -
    //                    e12 * e2 * e7 +
    //                    e12 * e3 * e6;

    //        inv.e10 = e0  * e5 * e15 -
    //                    e0  * e7 * e13 -
    //                    e4  * e1 * e15 +
    //                    e4  * e3 * e13 +
    //                    e12 * e1 * e7 -
    //                    e12 * e3 * e5;

    //        inv.e14 = -e0  * e5 * e14 +
    //                    e0  * e6 * e13 +
    //                    e4  * e1 * e14 -
    //                    e4  * e2 * e13 -
    //                    e12 * e1 * e6 +
    //                    e12 * e2 * e5;

    //        inv.e3 = -e1 * e6 * e11 +
    //                    e1 * e7 * e10 +
    //                    e5 * e2 * e11 -
    //                    e5 * e3 * e10 -
    //                    e9 * e2 * e7 +
    //                    e9 * e3 * e6;

    //        inv.e7 = e0 * e6 * e11 -
    //                e0 * e7 * e10 -
    //                e4 * e2 * e11 +
    //                e4 * e3 * e10 +
    //                e8 * e2 * e7 -
    //                e8 * e3 * e6;

    //        inv.e11 = -e0 * e5 * e11 +
    //                    e0 * e7 * e9 +
    //                    e4 * e1 * e11 -
    //                    e4 * e3 * e9 -
    //                    e8 * e1 * e7 +
    //                    e8 * e3 * e5;

    //        inv.e15 = e0 * e5 * e10 -
    //                    e0 * e6 * e9 -
    //                    e4 * e1 * e10 +
    //                    e4 * e2 * e9 +
    //                    e8 * e1 * e6 -
    //                    e8 * e2 * e5;

    //        float det = e0 * inv.e0 + e1 * inv.e4 + e2 * inv.e8 + e3 * inv.e12;

    //        float invdet = 1.0f / det;

    //        for (int i = 0; i < 16; i++)
    //            inv.e[i] *= invdet;

    //        return inv;
    //    }

    //    Matrix4x4 ortho(float left, float right, float bottom, float top, float near, float far)
    //    {
    //        Matrix4x4 m;

    //        m.e0 = 2.0f / (right - left);
    //        m.e5 = 2.0f / (top - bottom);
    //        m.e10 = -2.0f / (far - near);

    //        m.e12 = -(right + left) / (right - left);
    //        m.e13 = -(top + bottom) / (top - bottom);
    //        m.e14 = -(far + near) / (far - near);

    //        return m;
    //    }
    //}

}
