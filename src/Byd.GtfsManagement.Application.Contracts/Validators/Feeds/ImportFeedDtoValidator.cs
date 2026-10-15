using System;
using FluentValidation;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.Feeds;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.Feeds;

// islevi: Feed adinin zorunluluk ve uzunluk kurallarini dogrular.
// sistemdeki gorevi: Kaynak servisten cekme ucunu korur; yukleme girdisi ayni kurallari buradan alir.
public sealed class ImportFeedDtoValidator : FeedNameInputDtoValidatorBase<ImportFeedDto>
{
    // Feed adinin sekil kurallarini kaydeder.
    public ImportFeedDtoValidator()
    {
        RuleFor(input => input.Url).NotEmpty().WithErrorCode(FeedExceptionCodes.SourceNotConfigured)
            .MaximumLength(FeedConsts.MaxUrlLength).WithErrorCode(FeedExceptionCodes.SourceUrlInvalid)
            .Must(url => Uri.TryCreate(url, UriKind.Absolute, out var parsed) &&
                (parsed.Scheme == Uri.UriSchemeHttp || parsed.Scheme == Uri.UriSchemeHttps))
            .WithErrorCode(FeedExceptionCodes.SourceUrlInvalid);
    }

}
