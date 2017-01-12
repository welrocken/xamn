using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

using FluentValidation;
using Xamn.Common.Reflection;

namespace Xamn.Validation.FluentValidation
{
    public static class ValidationUtility
    {
        private static object InvokeRuleFor(Type type, object validator, PropertyInfo property)
        {
            var ruleForMethod = ReflectionUtility.GetGenericMethod(
                    validator.GetType(),
                    "RuleFor",
                    new Dictionary<string, Type> { { "TProperty", property.PropertyType } },
                    new Type[] { typeof(Expression<>) });

            var expression = ReflectionUtility.GetPropertyExpression(
                type,
                property);

            var ruleBuilderInitial = ReflectionUtility.InvokeMethod(validator, ruleForMethod, new object[] { expression });

            return ruleBuilderInitial;
        }

        private static object InvokeRuleMethod(Type type, object ruleBuilder, RuleAttribute rule, PropertyInfo property)
        {
            var parameterTypes = ReflectionUtility.GetTypes(rule.Parameters);

            var method = Helper.GetMethod(
                rule.Type,
                new Dictionary<string, Type> { { "T", type }, { "TProperty", property.PropertyType } },
                parameterTypes);

            var methodToInvoke = method;
            if (method.ContainsGenericParameters)
            {
                var genericArgumentTypes = new Type[method.GetGenericArguments().Length];

                genericArgumentTypes[0] = type;

                if (genericArgumentTypes.Length == 2)
                    genericArgumentTypes[1] = property.PropertyType;

                methodToInvoke = method.MakeGenericMethod(genericArgumentTypes);
            }

            var ruleBuilderOptions = ReflectionUtility.InvokeExtensionMethod(
                ruleBuilder,
                methodToInvoke,
                rule.Parameters);

            return ruleBuilderOptions;
        }

        private static object InvokeRuleOptionMethod(Type type, string methodName, object ruleBuilder, RuleAttribute rule, PropertyInfo property, params object[] additionalParameters)
        {
            var method = ReflectionUtility.GetGenericExtensionMethod(
                typeof(DefaultValidatorOptions),
                typeof(IRuleBuilderOptions<,>),
                methodName,
                new Dictionary<string, Type> { { "T", type }, { "TProperty", property.PropertyType } },
                new Type[] { typeof(string) });

            var methodToInvoke = method;
            if (method.ContainsGenericParameters)
            {
                var genericArgumentTypes = new Type[method.GetGenericArguments().Length];

                genericArgumentTypes[0] = type;

                if (genericArgumentTypes.Length == 2)
                    genericArgumentTypes[1] = property.PropertyType;

                methodToInvoke = method.MakeGenericMethod(genericArgumentTypes);
            }

            var ruleBuilderOptions = ReflectionUtility.InvokeExtensionMethod(
                ruleBuilder,
                methodToInvoke,
                additionalParameters);

            return ruleBuilderOptions;
        }

        public static void AutoSetup<T>(AbstractValidator<T> validator)
        {
            var type = typeof(T);

            var validatableAttribute = Attribute.GetCustomAttribute(type, typeof(ValidatableAttribute)) as ValidatableAttribute;
            if (validatableAttribute == null)
                throw new ArgumentException("The generic type parameter T must have ValidatableAttribute");

            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in properties)
            {
                var ruleBuilderOptions = InvokeRuleFor(type, validator, property);

                var withErrors = Attribute.GetCustomAttributes(property, typeof(WithErrorAttribute));
                foreach (RuleAttribute rule in Attribute.GetCustomAttributes(property, typeof(RuleAttribute)))
                {
                    ruleBuilderOptions = InvokeRuleMethod(type, ruleBuilderOptions, rule, property);

                    foreach (WithErrorAttribute withError in withErrors)
                    {
                        if (rule.Id != withError.Id)
                            continue;

                        if (withError.ErrorCode != null)
                            ruleBuilderOptions = InvokeRuleOptionMethod(type, "WithErrorCode", ruleBuilderOptions, rule, property, withError.ErrorCode);

                        if (withError.ErrorMessage != null)
                            ruleBuilderOptions = InvokeRuleOptionMethod(type, "WithMessage", ruleBuilderOptions, rule, property, withError.ErrorMessage);
                    }
                }
            }
        }
    }
}