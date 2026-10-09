using System;

namespace Byd.GtfsManagement.Models.Translations;

// islevi: Yeni Translation kaydi icin gerekli Feed ve ceviri alanlarini tasir.
// sistemdeki gorevi: Manager.CreateAsync isleminin model sozlesmesini olusturur.
/// <summary>GTFS ceviri olusturma domain modelini tasir.</summary>
public class TranslationCreateModel : TranslationInputModel
{
    public Guid FeedId { get; set; }
}
