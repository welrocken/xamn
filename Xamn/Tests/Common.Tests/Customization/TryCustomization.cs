using Ploeh.AutoFixture;

namespace Xamn.Common.Tests
{
    public class TryCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Register(() => FakeBuilder.GetInt());
        }
    }
}