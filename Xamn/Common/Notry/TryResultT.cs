using System;

namespace Xamn.Common.Notry
{
    public class TryResult<TResult> : TryResult, IEquatable<TryResult<TResult>>
    {
        public TResult Object { get; set; }
        
        public TryResult(TResult resultObject, bool success) : base(success)
        {
            Object = resultObject;
        }
        
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var tryResult = obj as TryResult<TResult>;
            if ((object)tryResult == null)
                return false;

            return Success == tryResult.Success && Object.Equals(tryResult.Object);
        }

        public bool Equals(TryResult<TResult> tryResult)
        {
            if ((object)tryResult == null)
                return false;

            return Success == tryResult.Success && Object.Equals(tryResult.Object);
        }

        public override int GetHashCode()
        {
            return Object.GetHashCode() | base.GetHashCode();
        }

        public static bool operator ==(TryResult<TResult> left, TryResult<TResult> right)
        {
            if (ReferenceEquals(left, right))
                return true;

            if (((object)left == null) || ((object)right == null))
                return false;

            return left.Success == right.Success && left.Object.Equals(right.Object);
        }

        public static bool operator !=(TryResult<TResult> left, TryResult<TResult> right)
        {
            return !(left == right);
        }
    }
}