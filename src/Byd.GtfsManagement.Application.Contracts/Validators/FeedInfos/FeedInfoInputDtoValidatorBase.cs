using System;
using System.Globalization;
using FluentValidation;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.FeedInfos;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.FeedInfos;

// islevi: feed_info.txt ortak yayimci ve zorunlu alan kurallarini tanimlar.
// sistemdeki gorevi: Create ve Update isteklerinin ayni public dogrulamayi kullanmasini saglar.
/// <summary>GTFS yayimci bilgisi ortak girdi kurallarini tanimlar.</summary>
public abstract class FeedInfoInputDtoValidatorBase<TDto> : AbstractValidator<TDto>
    where TDto : FeedInfoInputDto
{
    // Yayimci adi, URL, dil, tarih, e-posta format kurallarini kaydeder.
    protected FeedInfoInputDtoValidatorBase()
    {
        RuleFor(input => input.PublisherName)
            .NotEmpty().WithErrorCode(FeedInfoExceptionCodes.PublisherNameRequired)
            .MaximumLength(FeedInfoConsts.MaxPublisherNameLength).WithErrorCode(FeedInfoExceptionCodes.PublisherNameTooLong);

        RuleFor(input => input.PublisherUrl)
            .NotEmpty().WithErrorCode(FeedInfoExceptionCodes.PublisherUrlRequired)
            .MaximumLength(FeedInfoConsts.MaxUrlLength).WithErrorCode(FeedInfoExceptionCodes.PublisherUrlInvalid)
            .Must(BeValidAbsoluteUrl).WithErrorCode(FeedInfoExceptionCodes.PublisherUrlInvalid);

        RuleFor(input => input.Language)
            .NotEmpty().WithErrorCode(FeedInfoExceptionCodes.LanguageRequired)
            .MaximumLength(FeedInfoConsts.MaxLanguageLength).WithErrorCode(FeedInfoExceptionCodes.LanguageTooLong);

        RuleFor(input => input.DefaultLanguage)
            .MaximumLength(FeedInfoConsts.MaxLanguageLength)
            .When(input => !string.IsNullOrEmpty(input.DefaultLanguage))
            .WithErrorCode(FeedInfoExceptionCodes.LanguageTooLong);

        RuleFor(input => input.StartDate)
            .Must(BeGtfsDate)
            .When(input => !string.IsNullOrEmpty(input.StartDate))
            .WithErrorCode(FeedInfoExceptionCodes.DateFormatInvalid);

        RuleFor(input => input.EndDate)
            .Must(BeGtfsDate)
            .When(input => !string.IsNullOrEmpty(input.EndDate))
            .WithErrorCode(FeedInfoExceptionCodes.DateFormatInvalid);

        RuleFor(input => input.ContactEmail)
            .EmailAddress()
            .When(input => !string.IsNullOrEmpty(input.ContactEmail))
            .WithErrorCode(FeedInfoExceptionCodes.ContactEmailInvalid);

        RuleFor(input => input.ContactUrl)
            .Must(BeValidAbsoluteUrl)
            .When(input => !string.IsNullOrEmpty(input.ContactUrl))
            .WithErrorCode(FeedInfoExceptionCodes.ContactUrlInvalid);
    }

    private static bool BeValidAbsoluteUrl(string? url)
        => string.IsNullOrEmpty(url) || Uri.TryCreate(url, UriKind.Absolute, out _);

    // Degerin GTFS YYYYMMDD biciminde gercek bir takvim gunu oldugunu dogrular.
    private static bool BeGtfsDate(string? date)
        => DateOnly.TryParseExact(date, GtfsTimeConsts.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
}
