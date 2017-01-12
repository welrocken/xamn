using FluentValidation;

namespace Xamn.Validation.FluentValidation
{
    internal class GenericValidator<T> : AbstractValidator<T>
    {
        public GenericValidator() : base()
        {
            ValidationUtility.Setup(this);
        }
    }
}