using System;

using Xamn.Common.Utilities;

namespace Xamn.Common.Notry
{
    public class Try<TResult> : Try
    {

        public new TryResult<object>[] Results { get; protected set; }
        public TryResult<TResult> Result
        {
            get { return new TryResult<TResult>((TResult)Results[Results.Length - 1].Object, Results[Results.Length - 1].Success); }
            set { Results[Results.Length - 1] = new TryResult<object>(value.Object, value.Success); }
        }
        
        protected Try(TryResult<object>[] results) : base(results)
        {
            Results = new TryResult<object>[results.Length];
            results.CopyTo(Results, 0);
        }

        public Try(Func<TResult> function) : base()
        {
            Results = new TryResult<object>[1];

            TResult resultObject;
            Success = TryUtility.Try(function, out resultObject);
            Results[0] = new TryResult<object>(resultObject, Success);
        }

        public Try() : base()
        {
            Results = new TryResult<object>[1];
            Results[Results.Length - 1] = new TryResult<object>(default(TResult), false);
        }
        
        public Try<TResultNew> With<TResultNew>(Func<TResultNew> function)
        {
            var results = new TryResult<object>[Results.Length + 1];
            Results.CopyTo(results, 0);

            var result = new Try<TResultNew>(function);
            results[results.Length - 1] = new TryResult<object>(result.Result.Object, result.Result.Success);

            return new Try<TResultNew>(results);
        }

        public Try<TResultNew> And<TResultNew>(Func<TResultNew> function)
        {
            var @try = new Try<TResultNew>(function);
            return new Try<TResultNew>(new TryResult<object>[]
                {
                    new TryResult<object>(@try.Result,Success && @try.Success)
                });
        }

        public Try<TResultNew> Or<TResultNew>(Func<TResultNew> function)
        {
            var @try = new Try<TResultNew>(function);
            return new Try<TResultNew>(new TryResult<object>[]
                {
                    new TryResult<object>(@try.Result,Success || @try.Success)
                });
        }
        
        public static implicit operator TResult(Try<TResult> tries)
        {
            return tries.Result.Object;
        }
    }
}