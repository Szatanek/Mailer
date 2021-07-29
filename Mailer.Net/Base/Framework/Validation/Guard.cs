using System.Diagnostics;

namespace Framework.Validation
{
    public static class Guard
    {
        [DebuggerStepThrough]
        public static void NotNull(object param)
        {
            if (param == null)
            {
                throw new GuardException();
            }
        }

        [DebuggerStepThrough]
        public static void TypeOf<T>(object value)
        {
            var destinationType = typeof(T);
            if (destinationType != value.GetType())
            {
                throw new GuardException();
            }
        }

        [DebuggerStepThrough]
        public static void InRange(byte value, int min, int max)
        {
            if (value < min || value > max)
            {
                throw new GuardException();
            }
        }
    }
}