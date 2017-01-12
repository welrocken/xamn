using System;

namespace Xamn.Common.Utilities
{
    public static class TryUtility
    {
        public static bool Try<TResult>(Func<TResult> function, out TResult result)
        {
            try
            {
                result = function();
                return true;
            }
            catch
            {
                result = default(TResult);
                return false;
            }
        }

        public static bool Try(Action action)
        {
            try
            {
                action();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}