

using System.Runtime.InteropServices;
#region License

/*
*/

#endregion License

using System;
using System.Runtime.InteropServices;
using System.Globalization;

namespace Love
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Matrix22 : IEquatable<Matrix22>
    {
        #region Public Fields

        public float M11;
        public float M12;

        public float M21;
        public float M22;

        #endregion Public Fields

        public bool Equals(Matrix22 other)
        {
            return this == other;
        }

        public static bool operator !=(Matrix22 matrix1, Matrix22 matrix2)
        {
            return !(matrix1 == matrix2);
        }

        public static bool operator ==(Matrix22 matrix1, Matrix22 matrix2)
        {
            return (matrix1.M11 == matrix2.M11) && (matrix1.M12 == matrix2.M12) &&
                    (matrix1.M21 == matrix2.M21) && (matrix1.M22 == matrix2.M22);
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, "[M11:{0} M12:{1}] [M21:{2} M22:{3}]",
                M11, M12, M21, M22);
        }
    }


}