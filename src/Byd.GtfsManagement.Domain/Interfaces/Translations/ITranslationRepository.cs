using Byd.CoreExtensions.Interfaces;
using Byd.GtfsManagement.Entities.Translations;

namespace Byd.GtfsManagement.Interfaces.Translations;

// islevi: Translation entity'si icin genisletilmis veri erisim sozlesmesini bildirir.
// sistemdeki gorevi: Ceviri sorgularini ve detay yuklemelerini soyutlar.
/// <summary>GTFS ceviri repository sozlesmesini tanimlar.</summary>
public interface ITranslationRepository : IExtendedRepository<Translation>
{
}
