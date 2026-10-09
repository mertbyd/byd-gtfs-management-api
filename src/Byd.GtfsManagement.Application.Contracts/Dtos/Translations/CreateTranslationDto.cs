using System;

namespace Byd.GtfsManagement.Dtos.Translations;

// mirastan gelen: GTFS ceviri tablosu, alani, dili ve hedef metinleri.
// burada yazilan: hedef Feed.
// islevi: Yeni ceviri kaydinin Feed'ini ve alanlarini tasir.
// sistemdeki gorevi: Manager'in kapsamli kayit kurmasini saglar.
/// <summary>GTFS ceviri olusturma istegini tasir.</summary>
public sealed class CreateTranslationDto : TranslationInputDto
{
    public Guid FeedId { get; set; }
}
