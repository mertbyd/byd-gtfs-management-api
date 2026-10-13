using Byd.GtfsManagement.Dtos.Frequencies;

namespace Byd.GtfsManagement.Validators.Frequencies;

// mirastan gelen: ortak frequencies.txt alan kurallari.
// burada yazilan: yok; sefer bagi guncellenmez.
// islevi: Frequency guncelleme girdisini dogrular.
// sistemdeki gorevi: Ortak kurallari Update sozlesmesine uygular.
/// <summary>Sefer sikligi guncelleme kurallarini tanimlar.</summary>
public sealed class UpdateFrequencyDtoValidator : FrequencyInputDtoValidatorBase<UpdateFrequencyDto>
{
}
