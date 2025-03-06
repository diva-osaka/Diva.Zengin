using Diva.Zengin.Formats;
using Riok.Mapperly.Abstractions;

namespace Diva.Zengin.Mappers;

[Mapper]
internal static partial class 総合振込DataMapper
{
    [MapperIgnoreSource(nameof(総合振込Data.顧客コード1))]
    [MapperIgnoreSource(nameof(総合振込Data.顧客コード2))]
    public static partial 総合振込WriteData1 ToWriteData1(this 総合振込Data data);

    [MapperIgnoreSource(nameof(総合振込Data.EDI情報))]
    public static partial 総合振込WriteData2 ToWriteData2(this 総合振込Data data);
}