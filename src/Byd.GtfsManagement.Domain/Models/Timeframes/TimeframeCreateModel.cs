using System;

namespace Byd.GtfsManagement.Models.Timeframes;

// islevi: Yeni Timeframe kaydi icin gerekli Feed ve alanlari tasir.
// sistemdeki gorevi: Manager.CreateAsync isleminin model sozlesmesini olusturur.
/// <summary>GTFS zaman araligi olusturma domain modelini tasir.</summary>
public class TimeframeCreateModel : TimeframeInputModel
{
    public Guid FeedId { get; set; }
}
