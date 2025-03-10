using Diva.Zengin.Converters;
using JetBrains.Annotations;
using Xunit;

namespace Diva.Zengin.Tests.Converters;

[TestSubject(typeof(NumberConverter))]
public class NumberConverterTest
{
    private enum TestEnum
    {
        One = 1,
        Two = 2,
        Three = 3
    }

    [Fact]
    public void ConvertFromString_NullOrEmptyString_ReturnsDefault()
    {
        Assert.Null(NumberConverter.ConvertFromString<int?>(null));
        Assert.Null(NumberConverter.ConvertFromString<int?>(string.Empty));
        Assert.Null(NumberConverter.ConvertFromString<int?>("   "));
    }

    [Fact]
    public void ConvertFromString_ValidInteger_ReturnsCorrectValue()
    {
        Assert.Equal(123, NumberConverter.ConvertFromString<int>("123"));
        Assert.Equal(123, NumberConverter.ConvertFromString<int?>("123"));
    }

    [Fact]
    public void ConvertFromString_ValidDecimal_ReturnsCorrectValue()
    {
        Assert.Equal(12345m, NumberConverter.ConvertFromString<decimal>("12345"));
        Assert.Equal(12345m, NumberConverter.ConvertFromString<decimal?>("12345"));
    }

    [Fact]
    public void ConvertFromString_EnumByNumber_ReturnsCorrectEnum()
    {
        Assert.Equal(TestEnum.Two, NumberConverter.ConvertFromString<TestEnum>("2"));
        Assert.Equal(TestEnum.Three, NumberConverter.ConvertFromString<TestEnum>("003"));
    }

    [Fact]
    public void ConvertToString_NullValue_ReturnsSpaces()
    {
        Assert.Equal("   ", NumberConverter.ConvertToString(null, 3));
        Assert.Equal("     ", NumberConverter.ConvertToString(null, 5));
    }

    [Fact]
    public void ConvertToString_EnumValue_ReturnsNumberWithPadding()
    {
        Assert.Equal("01", NumberConverter.ConvertToString(TestEnum.One, 2));
        Assert.Equal("002", NumberConverter.ConvertToString(TestEnum.Two, 3));
    }

    [Fact]
    public void ConvertToString_DecimalValue_ReturnsFormattedString()
    {
        Assert.Equal("123", NumberConverter.ConvertToString(123m, 3));
        Assert.Equal("0123", NumberConverter.ConvertToString(123m, 4));
    }

    [Fact]
    public void ConvertToString_IntegerValue_ReturnsFormattedString()
    {
        Assert.Equal("123", NumberConverter.ConvertToString(123, 3));
        Assert.Equal("0456", NumberConverter.ConvertToString(456, 4));
    }
}