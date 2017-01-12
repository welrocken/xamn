using FluentAssertions;
using FluentValidation;
using Xamn.Testing;
using Xunit;

namespace Xamn.Validation.Tests
{
    [Trait("ValidationUtility Unit Tests", "AutoSetup")]
    public class ValidationUtilityTests
    {
        [Theory]
        [AutoMoqData(typeof(ValidationUtilityCustomization))]
        public void Successful(
            FakeClass validInstance,
            AbstractValidator<FakeClass> indirectSut)
        {
            var result = indirectSut.Validate(validInstance);

            result.IsValid.Should().Be(true);
        }

        #region Custom Rule Tests

        [Theory]
        [AutoMoqData(typeof(ValidationUtilityCustomization))]
        public void InvalidCreditCard(
            FakeClass validInstance,
            AbstractValidator<FakeClass> indirectSut)
        {
            validInstance.CreditCard = "asd";

            var result = indirectSut.Validate(validInstance);

            result.Errors[0].ErrorCode.Should()
                .Be(((int)FakeErrors.InvalidCreditCard).ToString());
            result.Errors[0].ErrorMessage.Should()
                .Be(FakeErrors.InvalidCreditCard.ToString());
        }

        [Theory]
        [AutoMoqData(typeof(ValidationUtilityCustomization))]
        public void InvalidEmailAddress(
            FakeClass validInstance,
            AbstractValidator<FakeClass> indirectSut)
        {
            validInstance.EmailAddress = "asd";

            var result = indirectSut.Validate(validInstance);

            result.Errors[0].ErrorCode.Should()
                .Be(((int)FakeErrors.InvalidEmailAddress).ToString());
            result.Errors[0].ErrorMessage.Should()
                .Be(FakeErrors.InvalidEmailAddress.ToString());
        }

        [Theory]
        [AutoMoqData(typeof(ValidationUtilityCustomization))]
        public void InvalidEmpty(
            FakeClass validInstance,
            AbstractValidator<FakeClass> indirectSut)
        {
            validInstance.Empty = "asd";

            var result = indirectSut.Validate(validInstance);

            result.Errors[0].ErrorCode.Should()
                .Be(((int)FakeErrors.InvalidEmpty).ToString());
            result.Errors[0].ErrorMessage.Should()
                .Be(FakeErrors.InvalidEmpty.ToString());
        }

        [Theory]
        [AutoMoqData(typeof(ValidationUtilityCustomization))]
        public void InvalidEqual(
            FakeClass validInstance,
            AbstractValidator<FakeClass> indirectSut)
        {
            validInstance.Equal = "asd";

            var result = indirectSut.Validate(validInstance);

            result.Errors[0].ErrorCode.Should()
                .Be(((int)FakeErrors.InvalidEqual).ToString());
            result.Errors[0].ErrorMessage.Should()
                .Be(FakeErrors.InvalidEqual.ToString());
        }

        [Theory]
        [AutoMoqData(typeof(ValidationUtilityCustomization))]
        public void InvalidExclusiveBetween(
            FakeClass validInstance,
            AbstractValidator<FakeClass> indirectSut)
        {
            validInstance.ExclusiveBetween = 0;

            var result = indirectSut.Validate(validInstance);

            result.Errors[0].ErrorCode.Should()
                .Be(((int)FakeErrors.InvalidExclusiveBetween).ToString());
            result.Errors[0].ErrorMessage.Should()
                .Be(FakeErrors.InvalidExclusiveBetween.ToString());
        }

        [Theory]
        [AutoMoqData(typeof(ValidationUtilityCustomization))]
        public void InvalidGreaterThan(
            FakeClass validInstance,
            AbstractValidator<FakeClass> indirectSut)
        {
            validInstance.GreaterThan = -5;

            var result = indirectSut.Validate(validInstance);

            result.Errors[0].ErrorCode.Should()
                .Be(((int)FakeErrors.InvalidGreaterThan).ToString());
            result.Errors[0].ErrorMessage.Should()
                .Be(FakeErrors.InvalidGreaterThan.ToString());
        }

        [Theory]
        [AutoMoqData(typeof(ValidationUtilityCustomization))]
        public void InvalidGreaterThanOrEqualTo(
            FakeClass validInstance,
            AbstractValidator<FakeClass> indirectSut)
        {
            validInstance.GreaterThanOrEqualTo = 0;

            var result = indirectSut.Validate(validInstance);

            result.Errors[0].ErrorCode.Should()
                .Be(((int)FakeErrors.InvalidGreaterThanOrEqualTo).ToString());
            result.Errors[0].ErrorMessage.Should()
                .Be(FakeErrors.InvalidGreaterThanOrEqualTo.ToString());
        }

        [Theory]
        [AutoMoqData(typeof(ValidationUtilityCustomization))]
        public void InvalidInclusiveBetween(
            FakeClass validInstance,
            AbstractValidator<FakeClass> indirectSut)
        {
            validInstance.InclusiveBetween = 0;

            var result = indirectSut.Validate(validInstance);

            result.Errors[0].ErrorCode.Should()
                .Be(((int)FakeErrors.InvalidInclusiveBetween).ToString());
            result.Errors[0].ErrorMessage.Should()
                .Be(FakeErrors.InvalidInclusiveBetween.ToString());
        }

        [Theory]
        [AutoMoqData(typeof(ValidationUtilityCustomization))]
        public void InvalidIsInEnum(
            FakeClass validInstance,
            AbstractValidator<FakeClass> indirectSut)
        {
            validInstance.IsInEnum = (FakeEnum)(-1);

            var result = indirectSut.Validate(validInstance);

            result.Errors[0].ErrorCode.Should()
                .Be(((int)FakeErrors.InvalidIsInEnum).ToString());
            result.Errors[0].ErrorMessage.Should()
                .Be(FakeErrors.InvalidIsInEnum.ToString());
        }

        [Theory]
        [AutoMoqData(typeof(ValidationUtilityCustomization))]
        public void InvalidLength(
            FakeClass validInstance,
            AbstractValidator<FakeClass> indirectSut)
        {
            validInstance.Length = "a";

            var result = indirectSut.Validate(validInstance);

            result.Errors[0].ErrorCode.Should()
                .Be(((int)FakeErrors.InvalidLength).ToString());
            result.Errors[0].ErrorMessage.Should()
                .Be(FakeErrors.InvalidLength.ToString());
        }

        [Theory]
        [AutoMoqData(typeof(ValidationUtilityCustomization))]
        public void InvalidLength2(
            FakeClass validInstance,
            AbstractValidator<FakeClass> indirectSut)
        {
            validInstance.Length2 = "a";

            var result = indirectSut.Validate(validInstance);

            result.Errors[0].ErrorCode.Should()
                .Be(((int)FakeErrors.InvalidLength).ToString());
            result.Errors[0].ErrorMessage.Should()
                .Be(FakeErrors.InvalidLength.ToString());
        }

        [Theory]
        [AutoMoqData(typeof(ValidationUtilityCustomization))]
        public void InvalidLessThan(
            FakeClass validInstance,
            AbstractValidator<FakeClass> indirectSut)
        {
            validInstance.LessThan = 306f;

            var result = indirectSut.Validate(validInstance);

            result.Errors[0].ErrorCode.Should()
                .Be(((int)FakeErrors.InvalidLessThan).ToString());
            result.Errors[0].ErrorMessage.Should()
                .Be(FakeErrors.InvalidLessThan.ToString());
        }

        [Theory]
        [AutoMoqData(typeof(ValidationUtilityCustomization))]
        public void InvalidLessThanOrEqualTo(
            FakeClass validInstance,
            AbstractValidator<FakeClass> indirectSut)
        {
            validInstance.LessThanOrEqualTo = 1001;

            var result = indirectSut.Validate(validInstance);

            result.Errors[0].ErrorCode.Should()
                .Be(((int)FakeErrors.InvalidLessThanOrEqualTo).ToString());
            result.Errors[0].ErrorMessage.Should()
                .Be(FakeErrors.InvalidLessThanOrEqualTo.ToString());
        }

        [Theory]
        [AutoMoqData(typeof(ValidationUtilityCustomization))]
        public void InvalidMatches(
            FakeClass validInstance,
            AbstractValidator<FakeClass> indirectSut)
        {
            validInstance.Matches = "-*";

            var result = indirectSut.Validate(validInstance);

            result.Errors[0].ErrorCode.Should()
                .Be(((int)FakeErrors.InvalidMatches).ToString());
            result.Errors[0].ErrorMessage.Should()
                .Be(FakeErrors.InvalidMatches.ToString());
        }

        [Theory]
        [AutoMoqData(typeof(ValidationUtilityCustomization))]
        public void InvalidMatches2(
            FakeClass validInstance,
            AbstractValidator<FakeClass> indirectSut)
        {
            validInstance.Matches2 = "-*";

            var result = indirectSut.Validate(validInstance);

            result.Errors[0].ErrorCode.Should()
                .Be(((int)FakeErrors.InvalidMatches).ToString());
            result.Errors[0].ErrorMessage.Should()
                .Be(FakeErrors.InvalidMatches.ToString());
        }

        [Theory]
        [AutoMoqData(typeof(ValidationUtilityCustomization))]
        public void InvalidNotEmpty(
            FakeClass validInstance,
            AbstractValidator<FakeClass> indirectSut)
        {
            validInstance.NotEmpty = string.Empty;

            var result = indirectSut.Validate(validInstance);

            result.Errors[0].ErrorCode.Should()
                .Be(((int)FakeErrors.InvalidNotEmpty).ToString());
            result.Errors[0].ErrorMessage.Should()
                .Be(FakeErrors.InvalidNotEmpty.ToString());
        }

        [Theory]
        [AutoMoqData(typeof(ValidationUtilityCustomization))]
        public void InvalidNotEqual(
            FakeClass validInstance,
            AbstractValidator<FakeClass> indirectSut)
        {
            validInstance.NotEqual = "qwe";

            var result = indirectSut.Validate(validInstance);

            result.Errors[0].ErrorCode.Should()
                .Be(((int)FakeErrors.InvalidNotEqual).ToString());
            result.Errors[0].ErrorMessage.Should()
                .Be(FakeErrors.InvalidNotEqual.ToString());
        }

        [Theory]
        [AutoMoqData(typeof(ValidationUtilityCustomization))]
        public void InvalidNotNull(
            FakeClass validInstance,
            AbstractValidator<FakeClass> indirectSut)
        {
            validInstance.NotNull = null;

            var result = indirectSut.Validate(validInstance);

            result.Errors[0].ErrorCode.Should()
                .Be(((int)FakeErrors.InvalidNotNull).ToString());
            result.Errors[0].ErrorMessage.Should()
                .Be(FakeErrors.InvalidNotNull.ToString());
        }

        [Theory]
        [AutoMoqData(typeof(ValidationUtilityCustomization))]
        public void InvalidNull(
            FakeClass validInstance,
            AbstractValidator<FakeClass> indirectSut)
        {
            validInstance.Null = "definetely not null";

            var result = indirectSut.Validate(validInstance);

            result.Errors[0].ErrorCode.Should()
                .Be(((int)FakeErrors.InvalidNull).ToString());
            result.Errors[0].ErrorMessage.Should()
                .Be(FakeErrors.InvalidNull.ToString());
        }

        #endregion

        #region Chain Rule Tests

        // TODO: ..

        #endregion
    }
}