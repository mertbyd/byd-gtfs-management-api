namespace Byd.GtfsManagement.Consts;

// islevi: Agency filtre yuzeyinde disariya acilan alan adlarini tanimlar.
// sistemdeki gorevi: Filtre DTO'su ile repository sorgu motoru arasindaki kararli alan adi sozlesmesidir.
public static class AgencyFilterFields
{
    public const string FeedId = "feedId";
    public const string CemvSupportId = "cemvSupportId";
    public const string GtfsAgencyId = "gtfsAgencyId";
    public const string Name = "name";
    public const string Timezone = "timezone";
    public const string Language = "language";
    public const string CreationTime = "creationTime";

    public static class PropertyPaths
    {
        public const string FeedId = "FeedId";
        public const string CemvSupportId = "CemvSupportId";
        public const string GtfsAgencyId = "GtfsAgencyId";
        public const string Name = "Name";
        public const string Timezone = "Timezone";
        public const string Language = "Language";
        public const string CreationTime = "CreationTime";
    }
}
