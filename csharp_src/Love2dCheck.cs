using System;

namespace Love
{
    public static class Check
    {
        public static void ArgumentNull(object obj, string argumentName)
        {
            if (obj == null)
            {
                throw new Exception($"bad argument #{argumentName}, not null expected, got null");
            }
        }

        public static void ArgumentNull(object obj)
        {
            if (obj == null)
            {
                throw new Exception($"bad argument, not null expected, got null");
            }
        }
    }
}
