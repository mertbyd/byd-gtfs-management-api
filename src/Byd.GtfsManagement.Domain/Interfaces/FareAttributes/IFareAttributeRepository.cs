using System;
using Byd.CoreExtensions.Interfaces;
using Byd.GtfsManagement.Entities.FareAttributes;

namespace Byd.GtfsManagement.Interfaces.FareAttributes;

// islevi: FareAttribute entity'si icin genisletilmis veri erisim sozlesmesini bildirir.
// sistemdeki gorevi: Ucret ozelligi sorgularini ve detay yuklemelerini soyutlar.
/// <summary>GTFS ucret ozelligi repository sozlesmesini tanimlar.</summary>
public interface IFareAttributeRepository : IExtendedRepository<FareAttribute>
{
}
