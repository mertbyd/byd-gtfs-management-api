using FluentValidation;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.Agencies;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.Agencies;

// islevi: agency.txt alanlarinin zorunluluk, uzunluk ve format kurallarini tek yerde dogrular.
// sistemdeki gorevi: Olusturma ve guncelleme isteklerinin ayni sekil sozlesmesinden gecmesini saglar.
public abstract class AgencyInputDtoValidatorBase<TDto> : AbstractValidator<TDto>
    where TDto : AgencyInputDto
{
    // GTFS ajans alanlarinin sekil kurallarini kaydeder.
    protected AgencyInputDtoValidatorBase()
    {
        RuleFor(input => input.GtfsAgencyId)
            .MaximumLength(AgencyConsts.MaxGtfsAgencyIdLength)
            .WithErrorCode(AgencyExceptionCodes.GtfsAgencyIdTooLong);
        RuleFor(input => input.Name)
            .NotEmpty().WithErrorCode(AgencyExceptionCodes.NameRequired)
            .MaximumLength(AgencyConsts.MaxNameLength).WithErrorCode(AgencyExceptionCodes.NameTooLong);
        RuleFor(input => input.Url)
            .NotEmpty().WithErrorCode(AgencyExceptionCodes.UrlRequired)
            .MaximumLength(AgencyConsts.MaxUrlLength).WithErrorCode(AgencyExceptionCodes.UrlTooLong);
        RuleFor(input => input.Timezone)
            .NotEmpty().WithErrorCode(AgencyExceptionCodes.TimezoneRequired)
            .MaximumLength(AgencyConsts.MaxTimezoneLength).WithErrorCode(AgencyExceptionCodes.TimezoneTooLong);
        RuleFor(input => input.Language)
            .MaximumLength(AgencyConsts.MaxLanguageLength)
            .WithErrorCode(AgencyExceptionCodes.LanguageTooLong);
        RuleFor(input => input.Phone)
            .MaximumLength(AgencyConsts.MaxPhoneLength)
            .WithErrorCode(AgencyExceptionCodes.PhoneTooLong);
        RuleFor(input => input.FareUrl)
            .MaximumLength(AgencyConsts.MaxFareUrlLength)
            .WithErrorCode(AgencyExceptionCodes.FareUrlTooLong);
        RuleFor(input => input.Email)
            .MaximumLength(AgencyConsts.MaxEmailLength)
            .WithErrorCode(AgencyExceptionCodes.EmailTooLong)
            .EmailAddress().WithErrorCode(AgencyExceptionCodes.EmailInvalid)
            .When(input => !string.IsNullOrWhiteSpace(input.Email));
    }
}
