using System;

namespace Xamn.Common.Notry
{
    public class TryResult : IEquatable<TryResult>
    {
        public bool Success { get; set; }
        
        public TryResult(bool success)
        {
            Success = success;
        }
        
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var tryResult = obj as TryResult;
            if ((object)tryResult == null)
                return false;

            return Success == tryResult.Success;
        }

        public bool Equals(TryResult tryResult)
        {
            if ((object)tryResult == null)
                return false;

            return Success == tryResult.Success;
        }

        public override int GetHashCode()
        {
            return Success.GetHashCode();
        }

        public static bool operator ==(TryResult left, TryResult right)
        {
            if (ReferenceEquals(left, right))
                return true;

            if (((object)left == null) || ((object)right == null))
                return false;

            return left.Success == right.Success;
        }

        public static bool operator !=(TryResult left, TryResult right)
        {
            return !(left == right);
        }
    }
}