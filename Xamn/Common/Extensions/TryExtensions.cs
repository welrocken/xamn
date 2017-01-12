using System;

using Xamn.Common.Notry;
using Xamn.Common.Utilities;

namespace Xamn.Common.Extensions
{
    public static class TryExtensions
    {
        public static bool Tries<TResult>(this object obj, Func<TResult> function, out TResult result)
        {
            return TryUtility.Try(function, out result);
        }

        public static bool Tries(this object obj, Action action)
        {
            return TryUtility.Try(action);
        }

        public static Try Tries(this object obj)
        {
            return new Try();
        }

        public static Try<TResult> Tries<TResult>(this object obj)
        {
            return new Try<TResult>();
        }
    }
}