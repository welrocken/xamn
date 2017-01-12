using System;
using System.Collections.Generic;
using System.Reflection;

using FluentValidation;
using Xamn.Common.Reflection;

namespace Xamn.Validation.FluentValidation
{
    public class Helper
    {
        private static Dictionary<RuleType, string> MethodNames = new Dictionary<RuleType, string>
        {
            //{ RuleType.Collection, "SetCollectionValidator" },
            { RuleType.CreditCard, "CreditCard" },
            { RuleType.EmailAddress, "EmailAddress" },
            { RuleType.Empty, "Empty" },
            { RuleType.Equal, "Equal" },
            { RuleType.ExclusiveBetween, "ExclusiveBetween" },
            { RuleType.GreaterThan, "GreaterThan" },
            { RuleType.GreaterThanOrEqualTo, "GreaterThanOrEqualTo" },
            { RuleType.InclusiveBetween, "InclusiveBetween" },
            { RuleType.IsInEnum, "IsInEnum" },
            { RuleType.Length, "Length" },
            { RuleType.LessThan, "LessThan" },
            { RuleType.LessThanOrEqualTo, "LessThanOrEqualTo" },
            { RuleType.Matches, "Matches" },
            { RuleType.Must, "Must" },
            { RuleType.NotEmpty, "NotEmpty" },
            { RuleType.NotEqual, "NotEqual" },
            { RuleType.NotNull, "NotNull" },
            { RuleType.Null, "Null" }
            //{ RuleType.PropertyValidator, "SetValidator" }
        };

        public static MethodInfo GetMethod(RuleType ruleType, Dictionary<string, Type> genericArguments, Type[] types)
        {
            // TODO: Rewrite using GetExtensionMethod
            var methodName = MethodNames[ruleType];

            MethodInfo methodInfo = null;

            methodInfo = ReflectionUtility.GetGenericExtensionMethod(
                typeof(DefaultValidatorExtensions),
                typeof(IRuleBuilder<,>),
                methodName,
                genericArguments,
                types);

            if (methodInfo != null)
                return methodInfo;

            methodInfo = ReflectionUtility.GetGenericExtensionMethod(
                typeof(CollectionValidatorExtensions),
                typeof(IRuleBuilder<,>),
                methodName,
                genericArguments,
                types);

            return methodInfo;
        }

        public static AbstractValidator<T> GetValidator<T>()
        {
            return new GenericValidator<T>();
        }
    }
}