using System;

namespace Xamn.Validation.FluentValidation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class RuleAttribute : Attribute
    {
        public RuleType Type { get; }
        public object[] Parameters { get; }
        public int Id { get; }

        internal const int DefaultLastId = 0;
        internal static int LastId = DefaultLastId;

        public RuleAttribute(RuleType type, params object[] parameters)
        {
            Type = type;
            Parameters = parameters;

            Id = ++LastId;
        }
    }
}