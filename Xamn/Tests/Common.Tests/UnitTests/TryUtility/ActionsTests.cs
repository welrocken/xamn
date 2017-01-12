using FluentAssertions;
using Xamn.Common.Utilities;
using Xamn.Testing;
using Xunit;

namespace Xamn.Common.Tests
{
    [Trait("TryUtility Unit Tests", "Action")]
    public class ActionTests
    {
        [Fact]
        [AutoMoqData(typeof(TryUtilityCustomization))]
        public void DoesNotThrow()
        {
            try
            {
                TryUtility.Try(FakeBuilder.ActionThrows());
            }
            catch
            {
                Assert.True(false, "Expected no exception from TryUtility, but got one");
            }
        }

        [Fact]
        [AutoMoqData(typeof(TryUtilityCustomization))]
        public void Returns_True_If_NoExceptionIsThrown()
        {
            var success = TryUtility.Try(FakeBuilder.ActionDoesNotThrow());
            success.Should().Be(true);
        }

        [Fact]
        [AutoMoqData(typeof(TryUtilityCustomization))]
        public void Returns_False_If_ExceptionIsThrown()
        {
            var success = TryUtility.Try(FakeBuilder.ActionThrows());
            success.Should().Be(false);
        }
    }
}