using FluentAssertions;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit2;
using Xamn.Common.Notry;
using Xamn.Testing;
using Xunit;

namespace Xamn.Common.Tests
{
    [Trait("Try Unit Tests", "Try<T>")]
    public class TryTTests
    {
        [Fact]
        [AutoMoqData(typeof(TryCustomization))]
        public void DoesNotThrow()
        {
            try
            {
                new Try<int>(FakeBuilder.FuncThrows<int>());
            }
            catch
            {
                Assert.True(false, "Expected no exception from TryUtility, but got one");
            }
        }

        [Fact]
        [AutoMoqData(typeof(TryCustomization))]
        public void Success_Is_True_When_NoExceptionIsThrown()
        {
            bool success = new Try<int>(FakeBuilder.FuncDoesNotThrow<int>());
            success.Should().Be(true);
        }

        [Fact]
        [AutoMoqData(typeof(TryCustomization))]
        public void Success_Is_False_When_ExceptionIsThrown()
        {
            bool success = new Try<int>(FakeBuilder.FuncThrows<int>());
            success.Should().Be(false);
        }

        [Theory]
        [AutoMoqData(typeof(TryCustomization))]
        public void Object_Is_Value_When_NoExceptionIsThrown(
            [Frozen] IFixture fixture)
        {
            int result = new Try<int>(FakeBuilder.FuncDoesNotThrow<int>(fixture));
            result.Should().Be(fixture.Create<int>());
        }

        [Fact]
        [AutoMoqData(typeof(TryCustomization))]
        public void Object_Is_Default_When_ExceptionIsThrown()
        {
            int result = new Try<int>(FakeBuilder.FuncThrows<int>());
            result.Should().Be(default(int));
        }

        [Fact]
        [AutoMoqData(typeof(TryCustomization))]
        public void With_Success_Is_False_When_ExceptionIsThrown()
        {
            var @try = new Try<int>();
            bool success = @try.With(FakeBuilder.FuncThrows<int>());
            success.Should().Be(false);
        }

        [Fact]
        [AutoMoqData(typeof(TryCustomization))]
        public void With_Success_Is_True_When_NoExceptionIsThrown()
        {
            var @try = new Try<int>();
            bool success = @try.With(FakeBuilder.FuncDoesNotThrow<int>());
            success.Should().Be(true);
        }

        [Fact]
        [AutoMoqData(typeof(TryCustomization))]
        public void And_Success_Is_False_When_OneTry_IsNot_Successful()
        {
            var @try = new Try<int>(FakeBuilder.FuncThrows<int>());
            bool success = @try.And(FakeBuilder.FuncDoesNotThrow<int>());
            success.Should().Be(false);
        }

        [Fact]
        [AutoMoqData(typeof(TryCustomization))]
        public void And_Success_Is_True_When_BothTries_Are_Successful()
        {
            var @try = new Try<int>(FakeBuilder.FuncDoesNotThrow<int>());
            bool success = @try.And(FakeBuilder.FuncDoesNotThrow<int>());
            success.Should().Be(true);
        }

        [Fact]
        [AutoMoqData(typeof(TryCustomization))]
        public void Or_Success_Is_False_When_BothTries_AreNot_Successful()
        {
            var @try = new Try<int>(FakeBuilder.FuncThrows<int>());
            bool success = @try.Or(FakeBuilder.FuncThrows<int>());
            success.Should().Be(false);
        }

        [Fact]
        [AutoMoqData(typeof(TryCustomization))]
        public void Or_Success_True_When_OneTry_Is_Successful()
        {
            var @try = new Try<int>(FakeBuilder.FuncThrows<int>());
            bool success = @try.Or(FakeBuilder.FuncDoesNotThrow<int>());
            success.Should().Be(true);
        }

        // TODO: With, And, Or Result Object test
    }
}