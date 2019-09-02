// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;

namespace Love
{
    /// <summary>
    /// log about the love library
    /// </summary>
    public static class Log
    {
        public static bool IsPrintInfo = true;
        public static bool IsPrintWarnning = true;
        public static bool IsPrintError = true;
        public static TargetType Target = TargetType.Console;

        public enum TargetType
        {
            /// <summary>
            /// use Console.WriteLine to log
            /// </summary>
            Console,

            /// <summary>
            /// use System.Diagnostics.Debug.WriteLine to log
            /// </summary>
            DiagnosticsDubug,

            /// <summary>
            /// use System.Diagnostics.Trace.WriteLine to log
            /// </summary>
            DiagnosticsTrace,
        }

        public static void Info(object info)
        {
            if (IsPrintInfo == false)
                return;

            switch(Target)
            {
                case TargetType.DiagnosticsTrace:
                    System.Diagnostics.Trace.WriteLine(info);
                    break;

                case TargetType.DiagnosticsDubug:
                    System.Diagnostics.Debug.WriteLine(info);
                    break;

                default:
                    Console.WriteLine(info);
                    break;
            }
        }

        public static void Warnning(object info)
        {
            if (IsPrintWarnning == false)
                return;

            switch (Target)
            {
                case TargetType.DiagnosticsTrace:
                    System.Diagnostics.Trace.WriteLine(info);
                    break;
                case TargetType.DiagnosticsDubug:
                    System.Diagnostics.Debug.WriteLine(info);
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(info);
                    Console.ResetColor();
                    break;
            }

        }

        public static void Error(object info)
        {
            if (IsPrintError == false)
                return;

            switch (Target)
            {
                case TargetType.DiagnosticsTrace:
                    System.Diagnostics.Trace.WriteLine(info);
                    break;

                case TargetType.DiagnosticsDubug:
                    System.Diagnostics.Debug.WriteLine(info);
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine(info);
                    Console.ResetColor();
                    break;
            }

        }
    }
}