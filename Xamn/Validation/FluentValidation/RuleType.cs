namespace Xamn.Validation.FluentValidation
{
    public enum RuleType
    {
        CreditCard = 1,
        EmailAddress = 2,
        Empty = 3,
        Equal = 4,
        ExclusiveBetween = 5,
        GreaterThan = 6,
        GreaterThanOrEqualTo = 7,
        InclusiveBetween = 8,
        IsInEnum = 9,
        Length = 10,
        LessThan = 11,
        LessThanOrEqualTo = 12,
        Matches = 13,
        Must = 14,
        NotEmpty = 15,
        NotEqual = 16,
        NotNull = 17,
        Null = 18
        // Unfortunately these cannot be used with attributes
        //Collection = 19,
        //PropertyValidator = 20
    }
}