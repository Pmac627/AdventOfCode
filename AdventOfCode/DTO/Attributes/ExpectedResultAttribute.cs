using System;

namespace AdventOfCode.DTO.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ExpectedResultAttribute : Attribute
    {
        public ExpectedResultAttribute(string expectedValue)
        {
            ExpectedValue = expectedValue?.ToString();
        }

        public string ExpectedValue { get; init; }
    }
}