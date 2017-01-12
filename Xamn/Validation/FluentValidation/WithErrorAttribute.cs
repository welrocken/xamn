using System;

namespace Xamn.Validation.FluentValidation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class WithErrorAttribute : Attribute
    {
        public string ErrorCode { get; }
        public string ErrorMessage { get; }
        public int Id { get; }

        internal const int DefaultLastId = 0;
        internal static int LastId = DefaultLastId;

        public WithErrorAttribute(string errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;

            Id = ++LastId;
        }

        public WithErrorAttribute(object enumInstance)
        {
            if (!enumInstance.GetType().IsEnum)
                throw new ArgumentException("enumInstance must be an Enum instance", "enumInstance");

            ErrorCode = ((int)enumInstance).ToString();
            ErrorMessage = enumInstance.ToString();

            Id = ++LastId;
        }
    }
}