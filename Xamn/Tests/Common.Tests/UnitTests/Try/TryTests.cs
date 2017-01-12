using FluentAssertions;
using Xamn.Common.Notry;
using Xamn.Testing;
using Xunit;

namespace Xamn.Common.Tests
{
    [Trait("Try Unit Tests", "Try")]
    public class TryTests
    {
        [Fact]
        [AutoMoqData(typeof(TryCustomization))]
        public void DoesNotThrow()
        {
            try
            {
                new Try(FakeBuilder.ActionThrows());
            }
            catch
            {
                Assert.True(false, "Expected no exception from Try, but got one");
            }
        }

        [Fact]
        [AutoMoqData(typeof(TryCustomization))]
        public void Success_Is_True_When_NoExceptionIsThrown()
        {
            bool success = new Try(FakeBuilder.ActionDoesNotThrow());
            success.Should().Be(true);
        }

        [Fact]
        [AutoMoqData(typeof(TryCustomization))]
        public void Success_Is_False_When_ExceptionIsThrown()
        {
            bool @try = new Try(FakeBuilder.ActionThrows());
            @try.Should().Be(false);
        }

        [Fact]
        [AutoMoqData(typeof(TryCustomization))]
        public void With_Success_Is_False_When_ExceptionIsThrown()
        {
            var @try = new Try();
            bool success = @try.With(FakeBuilder.ActionThrows());
            success.Should().Be(false);
        }

        [Fact]
        [AutoMoqData(typeof(TryCustomization))]
        public void With_Success_Is_True_When_NoExceptionIsThrown()
        {
            var @try = new Try();
            bool success = @try.With(FakeBuilder.ActionDoesNotThrow());
            success.Should().Be(true);
        }

        [Fact]
        [AutoMoqData(typeof(TryCustomization))]
        public void And_Success_Is_False_When_OneTry_IsNot_Successful()
        {
            var @try = new Try(FakeBuilder.ActionDoesNotThrow());
            bool success = @try.And(FakeBuilder.ActionThrows());
            success.Should().Be(false);
        }

        [Fact]
        [AutoMoqData(typeof(TryCustomization))]
        public void And_Success_Is_True_When_BothTries_Are_Successful()
        {
            var @try = new Try(FakeBuilder.ActionDoesNotThrow());
            bool success = @try.And(FakeBuilder.ActionDoesNotThrow());
            success.Should().Be(true);
        }

        [Fact]
        [AutoMoqData(typeof(TryCustomization))]
        public void Or_Success_Is_False_When_BothTries_AreNot_Successful()
        {
            var @try = new Try(FakeBuilder.ActionThrows());
            bool success = @try.Or(FakeBuilder.ActionThrows());
            success.Should().Be(false);
        }

        [Fact]
        [AutoMoqData(typeof(TryCustomization))]
        public void Or_Success_Is_True_When_OneTry_Is_Successful()
        {
            var @try = new Try(FakeBuilder.ActionThrows());
            bool success = @try.Or(FakeBuilder.ActionDoesNotThrow());
            success.Should().Be(true);
        }
    }
}