namespace Byd.GtfsManagement.Consts;

// islevi: shapes.txt tablo adi, maksimum uzunluk ve geometri sinirlarini tanimlar.
// sistemdeki gorevi: GTFS sekil sozlesmesini Domain.Shared katmaninda sabitler.
/// <summary>GTFS sekil sabitlerini tanimlar.</summary>
public static class ShapeConsts
{
    public const string TableName = "Shapes";
    public const string KeyIndex = "IX_Shapes_FeedId_GtfsShapeId_Sequence";
    public const int MaxGtfsShapeIdLength = 256;
    public const double MinLatitude = -90.0;
    public const double MaxLatitude = 90.0;
    public const double MinLongitude = -180.0;
    public const double MaxLongitude = 180.0;
}
