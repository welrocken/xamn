using System.Text.RegularExpressions;

using Xamn.Validation.FluentValidation;

namespace Xamn.Validation.Tests
{
    public enum FakeEnum
    {
        A = 0,
        B = 1
    }

    public enum FakeErrors
    {
        InvalidCreditCard = 101,
        InvalidEmailAddress = 102,
        InvalidEmpty = 103,
        InvalidEqual = 104,
        InvalidExclusiveBetween = 105,
        InvalidGreaterThan = 106,
        InvalidGreaterThanOrEqualTo = 107,
        InvalidInclusiveBetween = 108,
        InvalidIsInEnum = 109,
        InvalidLength = 110,
        InvalidLessThan = 111,
        InvalidLessThanOrEqualTo = 112,
        InvalidMatches = 113,
        InvalidNotEmpty = 114,
        InvalidNotEqual = 115,
        InvalidNotNull = 116,
        InvalidNull = 117
    }

    [Validatable]
    public class FakeClass
    {
        [Rule(RuleType.CreditCard),
            WithError(FakeErrors.InvalidCreditCard)] // .CreditCard<T>()
        public string CreditCard { get; set; }

        [Rule(RuleType.EmailAddress),
            WithError(FakeErrors.InvalidEmailAddress)] // .EmailAddress<T>()
        public string EmailAddress { get; set; }

        [Rule(RuleType.Empty),
            WithError(FakeErrors.InvalidEmpty)] // .Empty<T, T>()
        public string Empty { get; set; }

        [Rule(RuleType.Equal, "value"),
            WithError(FakeErrors.InvalidEqual)] // .Equal<T, T>(T toCompare, IEqualityComparer comparer = null)
        public string Equal { get; set; }

        [Rule(RuleType.ExclusiveBetween, 0, 301),
            WithError(FakeErrors.InvalidExclusiveBetween)] // .ExclusiveBetween<T, T>(T from, T to) where T : struct, IComparable<T>, IComparable
        public int ExclusiveBetween { get; set; }

        [Rule(RuleType.GreaterThan, -5),
            WithError(FakeErrors.InvalidGreaterThan)] // .GreaterThan<T, T>(T valueToCompare) where T : IComparable<T>, IComparable
        public int GreaterThan { get; set; }

        [Rule(RuleType.GreaterThanOrEqualTo, 123),
            WithError(FakeErrors.InvalidGreaterThanOrEqualTo)] // .GreaterThanOrEqualTo<T, T>(T valueToCompare) where T : struct, IComparable<T>, IComparable
        public int GreaterThanOrEqualTo { get; set; }

        [Rule(RuleType.InclusiveBetween, 1, 300),
            WithError(FakeErrors.InvalidInclusiveBetween)] // .InclusiveBetween<T, T>(T from, T to) where T : struct, IComparable<T>, IComparable
        public int InclusiveBetween { get; set; }

        [Rule(RuleType.IsInEnum),
            WithError(FakeErrors.InvalidIsInEnum)] // .IsInEnum<T, T>()
        public FakeEnum IsInEnum { get; set; }

        [Rule(RuleType.Length, 100),
            WithError(FakeErrors.InvalidLength)] // .Length<T>(int exactLength)
        public string Length { get; set; }

        [Rule(RuleType.Length, 100, 200),
            WithError(FakeErrors.InvalidLength)] // .Length<T>(int min, int max)
        public string Length2 { get; set; }

        [Rule(RuleType.LessThan, 305.3f),
            WithError(FakeErrors.InvalidLessThan)] // .LessThan<T, T>(T valueToCompare) where T : struct, IComparable<T>, IComparable
        public float LessThan { get; set; }

        [Rule(RuleType.LessThanOrEqualTo, 1000.9),
            WithError(FakeErrors.InvalidLessThanOrEqualTo)] // .LessThanOrEqualTo<T, T>(T valueToCompare) where T : struct, IComparable<T>, IComparable
        public double LessThanOrEqualTo { get; set; }

        [Rule(RuleType.Matches, "^[a-zA-Z0-9_]*$"),
            WithError(FakeErrors.InvalidMatches)] // .Matches<T>(string expression)
        public string Matches { get; set; }

        [Rule(RuleType.Matches, "^[a-zA-Z0-9_]*$", RegexOptions.None),
            WithError(FakeErrors.InvalidMatches)] // .Matches<T>(string expression, RegexOptions options)
        public string Matches2 { get; set; }

        [Rule(RuleType.NotEmpty),
            WithError(FakeErrors.InvalidNotEmpty)] // .NotEmpty<T, T>()
        public string NotEmpty { get; set; }

        [Rule(RuleType.NotEqual, "qwe"),
            WithError(FakeErrors.InvalidNotEqual)] // .NotEqual<T, T>(T toCompare, IEqualityComparer comparer = null)
        public string NotEqual { get; set; }

        [Rule(RuleType.NotNull),
            WithError(FakeErrors.InvalidNotNull)] // .NotNull<T, T>()
        public string NotNull { get; set; }

        [Rule(RuleType.Null),
            WithError(FakeErrors.InvalidNull)] // .Null<T, T>()
        public string Null { get; set; }
    }
}