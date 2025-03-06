using System;
using Diva.Zengin.Converters;
using JetBrains.Annotations;
using Xunit;

namespace Diva.Zengin.Tests.Converters;

[TestSubject(typeof(和暦To西暦Converter))]
public class 和暦To西暦ConverterTest
{
    
    [Theory]
    [InlineData("070101", 2025, 1, 1)]
    [InlineData("010501", 2019, 5, 1)]
    [InlineData("310430", 2019, 4, 30)]
    [InlineData("010108", 1989, 1, 8)]
    [InlineData("640107", 1989, 1, 7)]
    public void Convert和暦日付ToDateOnly_ValidDates(string eraDate, int expectedYear, int expectedMonth, int expectedDay)
    {
        var date = 和暦To西暦Converter.Convert和暦日付ToDateOnly(eraDate);
        Assert.Equal(new DateOnly(expectedYear, expectedMonth, expectedDay), date);
    }
}