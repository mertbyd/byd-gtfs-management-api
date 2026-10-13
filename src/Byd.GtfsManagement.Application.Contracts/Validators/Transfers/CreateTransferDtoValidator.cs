using FluentValidation;
using Byd.GtfsManagement.Dtos.Transfers;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.Transfers;

// mirastan gelen: ortak transfers.txt alan kurallari.
// burada yazilan: Feed zorunlulugu.
// islevi: Yeni aktarma isteginin kapsam anahtarini dogrular.
// sistemdeki gorevi: Manager'a Feedsuz kayit ulasmasini engeller.
/// <summary>GTFS aktarma olusturma kurallarini tanimlar.</summary>
public sealed class CreateTransferDtoValidator : TransferInputDtoValidatorBase<CreateTransferDto>
{
    // Yeni kaydin Feed bagini zorunlu tutar.
    public CreateTransferDtoValidator()
    {
        RuleFor(input => input.FeedId)
            .NotEmpty().WithErrorCode(TransferExceptionCodes.FeedRequired);
    }
}
