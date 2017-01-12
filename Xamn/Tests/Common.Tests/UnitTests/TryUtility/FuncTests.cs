using FluentAssertions;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit2;
using Xamn.Common.Utilities;
using Xamn.Testing;
using Xunit;

namespace Xamn.Common.Tests
{
    [Trait("TryUtility Unit Tests", "Func")]
    public class FuncTests
    {
        [Fact]
        [AutoMoqData(typeof(TryUtilityCustomization))]
        public void DoesNotThrow()
        {
            try
            {
                int result;
                TryUtility.Try(FakeBuilder.FuncThrows<int>(), out result);
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
            int result;
            var success = TryUtility.Try(FakeBuilder.FuncDoesNotThrow<int>(), out result);
            success.Should().Be(true);
        }

        [Fact]
        [AutoMoqData(typeof(TryUtilityCustomization))]
        public void Returns_False_If_ExceptionIsThrown()
        {
            int result;
            var success = TryUtility.Try(FakeBuilder.FuncThrows<int>(), out result);
            success.Should().Be(false);
        }

        [Theory]
        [AutoMoqData(typeof(TryUtilityCustomization))]
        public void Returns_Value_If_NoExceptionIsThrown(
            [Frozen] IFixture fixture)
        {
            int result;
            TryUtility.Try(FakeBuilder.FuncDoesNotThrow<int>(fixture), out result);
            result.Should().Be(fixture.Create<int>());
        }

        [Fact]
        [AutoMoqData(typeof(TryUtilityCustomization))]
        public void Returns_Default_If_ExceptionIsThrown()
        {
            int result;
            TryUtility.Try(FakeBuilder.FuncThrows<int>(), out result);
            result.Should().Be(default(int));
        }
    }
}