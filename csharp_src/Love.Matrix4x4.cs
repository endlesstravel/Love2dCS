using System.Runtime.InteropServices;

namespace Love
{
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