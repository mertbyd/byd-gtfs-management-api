using FluentValidation;
using Byd.GtfsManagement.Dtos.FeedInfos;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.FeedInfos;

// mirastan gelen: ortak feed_info.txt alan kurallari.
// burada yazilan: Feed zorunlulugu.
// islevi: Yeni yayimci bilgisi isteginin kapsam anahtarini dogrular.
// sistemdeki gorevi: Manager'a Feedsuz kayit ulasmasini engeller.
/// <summary>Yeni yayimci bilgisi olusturma dogrulayicisini tanimlar.</summary>
public sealed class CreateFeedInfoDtoValidator : FeedInfoInputDtoValidatorBase<CreateFeedInfoDto>
{
    // Yeni kaydin Feed bagini zorunlu tutar.
    public CreateFeedInfoDtoValidator()
    {
        RuleFor(input => input.FeedId)
            .NotEmpty().WithErrorCode(FeedInfoExceptionCodes.FeedRequired);
    }
}
