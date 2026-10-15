namespace Byd.GtfsManagement.Consts;

// islevi: Feed filtre yuzeyinde disariya acilan alan adlarini tanimlar.
// sistemdeki gorevi: Filtre DTO'su ile repository sorgu motoru arasindaki kararli alan adi sozlesmesidir.
public static class FeedFilterFields
{
    public const string FeedId = "feedId";
    public const string Name = "name";
    public const string IsActive = "isActive";
    public const string Status = "status";
    public const string CreationTime = "creationTime";

    public static class PropertyPaths
    {
        public const string FeedId = "Id";
        public const string Name = "Name";
        public const string IsActive = "IsActive";
        public const string Status = "Status";
        public const string CreationTime = "CreationTime";
    }
}
