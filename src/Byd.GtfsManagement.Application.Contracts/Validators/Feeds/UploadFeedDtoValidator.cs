using System;
using FluentValidation;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.Feeds;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.Feeds;

// islevi: Feed adina ek olarak ZIP dosya adi ve dosya iceriginin public sekil kurallarini dogrular.
// sistemdeki gorevi: Gecersiz yuklemeyi file moduluna ve dogrulayiciya gitmeden reddeder.
public sealed class UploadFeedDtoValidator : FeedNameInputDtoValidatorBase<UploadFeedDto>
{
    // Ad kurallarini kaynak girdisinden alir; ZIP uzantisi ve bos olmayan icerik kurallarini ekler.
    public UploadFeedDtoValidator()
    {
        RuleFor(input => input.FileName).NotEmpty().WithErrorCode(FeedExceptionCodes.FileNameRequired)
            .MaximumLength(FeedConsts.MaxStoredFileNameLength).WithErrorCode(FeedExceptionCodes.FileNameTooLong)
            .Must(fileName => fileName is null ||
                fileName.EndsWith(GtfsValidationConsts.ArchiveExtension, StringComparison.OrdinalIgnoreCase))
            .WithErrorCode(FeedExceptionCodes.ArchiveRequired);
        RuleFor(input => input.Data).NotEmpty().WithErrorCode(FeedExceptionCodes.DataRequired);
    }
}
