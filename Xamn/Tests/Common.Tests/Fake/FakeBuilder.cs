using System;

using Ploeh.AutoFixture;

namespace Xamn.Common.Tests
{
    static class FakeBuilder
    {
        public static Action ActionThrows()
        {
            return () => { throw new Exception(); };
        }

        public static Action ActionDoesNotThrow()
        {
            return () => { return; };
        }

        public static Func<T> FuncThrows<T>()
        {
            return () => { throw new Exception(); };
        }

        public static Func<T> FuncDoesNotThrow<T>()
        {
            return () => { return default(T); };
        }

        public static Func<T> FuncDoesNotThrow<T>(IFixture fixture)
        {
            return () => { return fixture.Create<T>(); };
        }

        public static int GetInt()
        {
            return 123;
        }
    }
}