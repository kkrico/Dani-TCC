using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Dani_TCC.Core.GuardClause
{
    public static class Guard
    {
        [DebuggerStepThrough]
        public static void IsNotNull(object argumentValue, string argumentName)
        {
            if (argumentValue == null)
                throw new ArgumentNullException(argumentName);
        }

        [DebuggerStepThrough]
        public static void IsNotNull(string argumentValue, string argumentName)
        {
            if (string.IsNullOrEmpty(argumentValue))
                throw new ArgumentNullException(argumentName);
        }

        [DebuggerStepThrough]
        public static void IsDefinidoNoEnum(Type enumtype, object value, string argumentName)
        {
            if (!Enum.IsDefined(enumtype, value))
                throw new InvalidEnumArgumentException(argumentName);
        }
    }
}