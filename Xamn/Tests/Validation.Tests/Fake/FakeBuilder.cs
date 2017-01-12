namespace Xamn.Validation.Tests
{
    public static class FakeBuilder
    {
        public static FakeClass GetFakeClass()
        {
            return new FakeClass
            {
                CreditCard = "5105105105105100",
                EmailAddress = "id@domain.co",
                Empty = string.Empty,
                Equal = "value",
                ExclusiveBetween = 1,
                GreaterThan = -4,
                GreaterThanOrEqualTo = 123,
                InclusiveBetween = 1,
                IsInEnum = FakeEnum.A,
                Length = new string('*', 100),
                Length2 = new string('*', 150),
                LessThan = 305.2f,
                LessThanOrEqualTo = 1000.9,
                Matches = "asd",
                Matches2 = "asd",
                NotEmpty = "a",
                NotEqual = "qwer",
                NotNull = "definetely not null",
                Null = null
            };
        }
    }
}