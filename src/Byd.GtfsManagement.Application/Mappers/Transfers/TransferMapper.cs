using Byd.GtfsManagement.Dtos.Transfers;
using Byd.GtfsManagement.Entities.Transfers;
using Byd.GtfsManagement.Models.Transfers;
using Riok.Mapperly.Abstractions;

namespace Byd.GtfsManagement.Mappers.Transfers;

// islevi: Transfer DTO, domain model ve entity eslemelerini uretir.
// sistemdeki gorevi: Application sinirinda manuel alan kopyalamayi onler.
/// <summary>GTFS aktarma DTO ve modellerini esler.</summary>
[Mapper]
public partial class TransferMapper
    : IGtfsArchiveMapper<TransferImportDto, Transfer>, IGtfsCrudMapper<Transfer, CreateTransferDto, UpdateTransferDto, TransferDto,
        TransferCreateModel, TransferUpdateModel>
{
    // Olusturma istegini domain modeline donusturur.
    public partial TransferCreateModel Map(CreateTransferDto input);

    // Guncelleme istegini domain modeline donusturur.
    public partial TransferUpdateModel Map(UpdateTransferDto input);

    // Kaydi public cevap sozlesmesine donusturur.
    public partial TransferDto Map(Transfer entity);

    // CSV satirini entity'ye esler.
    public partial Transfer Map(TransferImportDto input);

    // Entity'yi disa aktarim icin CSV satirina esler.
    public partial TransferImportDto MapToImport(Transfer entity);
}
