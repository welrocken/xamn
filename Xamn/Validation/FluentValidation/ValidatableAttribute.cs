using System;

namespace Xamn.Validation.FluentValidation
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ValidatableAttribute : Attribute
    {
        public ValidatableAttribute()
        {
            RuleAttribute.LastId = RuleAttribute.DefaultLastId;
            WithErrorAttribute.LastId = WithErrorAttribute.DefaultLastId;
        }
    }
}