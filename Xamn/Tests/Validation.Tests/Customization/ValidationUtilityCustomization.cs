using Ploeh.AutoFixture;
using Xamn.Validation.FluentValidation;

namespace Xamn.Validation.Tests
{
    public class ValidationUtilityCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            // TODO: We would want to Moq the infrastructure of the FluentValidation here;
            // But the code was not written accordingly in the Xamn.Validation.
            // Come up with a customizable solution, write appropriate unit tests, fix code.

            var validInstance = FakeBuilder.GetFakeClass();
            var sut = Helper.GetValidator<FakeClass>();

            fixture.Register(() => validInstance);
            fixture.Register(() => sut);
        }
    }
}