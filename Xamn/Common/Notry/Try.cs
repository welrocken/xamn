using System;

using Xamn.Common.Utilities;

namespace Xamn.Common.Notry
{
    // TODO: This needs The With, And and Or methods of Try<T> as well.
    public class Try
    {
        public TryResult[] Results { get; protected set; }
        public bool Success
        {
            get { return Results[Results.Length - 1].Success; }
            set { Results[Results.Length - 1].Success = value; }
        }
        
        protected Try(TryResult[] results)
        {
            Results = new TryResult[results.Length];
            results.CopyTo(Results, 0);
        }

        public Try(Action action)
        {
            Results = new TryResult[1];
            Results[0] = new TryResult(TryUtility.Try(action));
        }

        public Try()
        {
            Results = new TryResult[1];
            Results[0] = new TryResult(false);
        }
        
        public Try With(Action action)
        {
            var results = new TryResult[Results.Length + 1];
            Results.CopyTo(results, 0);

            results[results.Length - 1] = new Try(action).Results[0];

            return new Try(results);
        }

        public Try And(Action action)
        {
            return new Try(new TryResult[]
                {
                    new TryResult(Success && new Try(action).Success)
                });
        }

        public Try Or(Action action)
        {
            return new Try(new TryResult[]
                {
                    new TryResult(Success || new Try(action).Success)
                });
        }
        
        public static implicit operator bool(Try tryObject)
        {
            return tryObject.Success;
        }
    }
}