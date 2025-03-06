namespace Diva.Zengin.Formats;

public enum データ区分
{
    /// <summary>
    ///  ヘッダー・レコード： データ・レコードの集まりの始まりを表わすとともに、データ・レコードの種別等を表示する。データ区分「1」。 
    /// </summary>
    Header = 1,

    /// <summary>
    /// データ・レコード： 連絡・通知する情報の1単位。データ区分「2」。 
    /// </summary>
    Data = 2,

    /// <summary>
    /// トレーラ・レコード： ヘッダー・レコードで始まるデータ・レコードの集まりの終わりを表わす。データ区分「8」。 
    /// </summary>
    Trailer = 8,

    /// <summary>
    /// エンド・レコード： ファイルの終りを表わす。データ区分「9」。 
    /// </summary>
    End = 9
}