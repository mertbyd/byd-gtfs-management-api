using System;

namespace Byd.GtfsManagement.Dtos.Shapes;

// islevi: Yeni sekil noktasi olusturma HTTP istegini tasir.
// sistemdeki gorevi: Gerekli Feed kimligini girdi sozlesmesine ekler.
/// <summary>Yeni sekil noktasi olusturma DTO alanlarini tasir.</summary>
public class CreateShapeDto : ShapeInputDto
{
    public Guid FeedId { get; set; }
}
