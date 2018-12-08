using System;

namespace Love
{
    public static class Check
    {
        public static void ArgumentNull(object obj, string argumentName)
        {
            if (obj == null)
            {
                throw new ArgumentNullException($"bad argument #{argumentName}, not null expected, got null");
            }
        }
    }
}
