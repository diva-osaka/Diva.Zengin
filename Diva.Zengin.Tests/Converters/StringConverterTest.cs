using System.Text;
using Diva.Zengin.Converters;
using JetBrains.Annotations;
using Xunit;

namespace Diva.Zengin.Tests.Converters;

[TestSubject(typeof(StringConverter))]
public class StringConverterTest
{
    [Theory]
    [InlineData("0123456789", "0123456789")]
    [InlineData("ABCXYZ", "ABCXYZ")]
    [InlineData("abcxyz", "ABCXYZ")]
    [InlineData("０１２３４５６７８９", "0123456789")]
    [InlineData("ＡＢＣＸＹＺ", "ABCXYZ")]
    [InlineData("ａｂｃｘｙｚ", "ABCXYZ")]
    [InlineData("あいうえおわゐゑをんゔ", "ｱｲｳｴｵﾜｲｴｦﾝｳﾞ")]
    [InlineData("ぁぃぅぇぉっゃゅょゎゕゖ", "ｱｲｳｴｵﾂﾔﾕﾖﾜｶｹ")]
    [InlineData("ァィゥェォッャュョヮヵヶ", "ｱｲｳｴｵﾂﾔﾕﾖﾜｶｹ")]
    [InlineData("ガギグゲゴパピプペポ", "ｶﾞｷﾞｸﾞｹﾞｺﾞﾊﾟﾋﾟﾌﾟﾍﾟﾎﾟ")]
    [InlineData("ー", "-")]
    [InlineData("（）＋，－．／：？￥¥", @"()+,-./:?\\")]
    [InlineData("「」", "｢｣")]
    [InlineData("　\t", "  ")]
    public void ToHalfWidth_ShouldConvertCorrectly(string input, string expected)
    {
        // Act
        var result = input.ToHalfWidth();

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("ガギグゲゴパピプペポ", "ｶﾞｷﾞｸﾞｹﾞｺﾞﾊﾟﾋﾟﾌﾟﾍﾟﾎﾟ", NormalizationForm.FormC)]
    [InlineData("ガギグゲゴパピプペポ", "ｶﾞｷﾞｸﾞｹﾞｺﾞﾊﾟﾋﾟﾌﾟﾍﾟﾎﾟ", NormalizationForm.FormD)]
    [InlineData("ガギグゲゴパピプペポ", "ｶﾞｷﾞｸﾞｹﾞｺﾞﾊﾟﾋﾟﾌﾟﾍﾟﾎﾟ", NormalizationForm.FormKC)]
    [InlineData("ガギグゲゴパピプペポ", "ｶﾞｷﾞｸﾞｹﾞｺﾞﾊﾟﾋﾟﾌﾟﾍﾟﾎﾟ", NormalizationForm.FormKD)]
    public void ToHalfWidth_ShouldNormalizeAndConvertCorrectly(string input, string expected, NormalizationForm form)
    {
        // Arrange
        var normalizedInput = input.Normalize(form);

        // Act
        var result = normalizedInput.ToHalfWidth();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ToHalfWidth_ShouldHandleNullInput()
    {
        // Act
        var result = StringConverter.ToHalfWidth(null);

        // Assert
        Assert.Null(result);
    }
}