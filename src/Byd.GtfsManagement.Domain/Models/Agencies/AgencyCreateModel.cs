namespace Byd.GtfsManagement.Models.Agencies;

// mirastan gelen: CemvSupportId, GtfsAgencyId, Name, Url, Timezone, Language, Phone, FareUrl, Email.
// burada yazilan: yok; Feed istekten alinmaz, kayit organizasyonun aktif Feed'ine yazilir.
public sealed class AgencyCreateModel : AgencyInputModel
{
}
