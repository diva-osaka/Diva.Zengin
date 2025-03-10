using Diva.Zengin.Converters;
using JetBrains.Annotations;
using Xunit;

namespace Diva.Zengin.Tests.Converters;

[TestSubject(typeof(CharacterConverter))]
public class CharacterConverterTest
{
    [Theory]
    [InlineData("test", "test")]
    [InlineData("test  ", "test")]
    [InlineData("", "", false)]
    [InlineData("", null, true)]
    [InlineData(null, "", false)]
    [InlineData(null, null, true)]
    [InlineData("   ", "", false)]
    [InlineData("   ", null, true)]
    public void ConvertFromString_HandlesVariousInputs(string? input, string? expected, bool nullable = false)
    {
        var result = CharacterConverter.ConvertFromString(input, nullable);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("test", 4, "test")]
    [InlineData("test", 6, "test  ")]
    [InlineData("test", 2, "test")] // Note: This will truncate as PadRight doesn't shrink
    [InlineData("", 3, "   ")]
    [InlineData(null, 3, "   ")]
    [InlineData(123, 5, "123  ")]
    public void ConvertToString_HandlesVariousInputs(object? input, int count, string expected)
    {
        var result = CharacterConverter.ConvertToString(input, count);
        Assert.Equal(expected, result);
    }
}