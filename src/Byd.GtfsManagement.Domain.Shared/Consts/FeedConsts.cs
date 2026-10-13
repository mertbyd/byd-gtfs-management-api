namespace Byd.GtfsManagement.Consts;

// islevi: Feed tablosunun, ad sinirinin ve tekillik indeksinin sabit sozlesmesini tanimlar.
// sistemdeki gorevi: Domain ve EF ayni Feed sozlesmesini okur.
public static class FeedConsts
{
    public const int MaxNameLength = 200;
    public const string TableName = "Feeds";
    public const string ScopeNameIndex = "IX_Feeds_Tenant_Name";
}
