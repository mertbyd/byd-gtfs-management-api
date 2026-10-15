namespace Byd.GtfsManagement.Consts;

// islevi: agency.txt kayitlarinin tablo, alan uzunlugu ve tekillik indeks adlarini tanimlar.
// sistemdeki gorevi: Entity, EF eslemesi ve validator'in ayni GTFS ajans sozlesmesini okumasini saglar.
public static class AgencyConsts
{
    public const string TableName = "Agencies";
    public const string GtfsAgencyIdIndex = "IX_Agencies_Feed_GtfsAgencyId";
    public const string MissingGtfsAgencyIdIndex = "IX_Agencies_Feed_NoGtfsAgencyId";

    // agency_id verilen ajanslar Feed icinde tekildir, verilmeyen tek ajans olabilir; filtreler veritabani kolon adiyla
    // yazilir.
    public const string GtfsAgencyIdIndexFilter = "gtfs_agency_id IS NOT NULL";
    public const string MissingGtfsAgencyIdIndexFilter = "gtfs_agency_id IS NULL";

    public const int MaxGtfsAgencyIdLength = 100;
    public const int MaxNameLength = 200;
    public const int MaxUrlLength = 500;
    public const int MaxTimezoneLength = 100;
    public const int MaxLanguageLength = 20;
    public const int MaxPhoneLength = 50;
    public const int MaxFareUrlLength = 500;
    public const int MaxEmailLength = 320;
}
