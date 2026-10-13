using Byd.GtfsManagement.Dtos.StopTimes;

namespace Byd.GtfsManagement.Validators.StopTimes;

// mirastan gelen: stop_times.txt ortak sekil kurallari.
// burada yazilan: yok; sefer kimligi guncellenmez.
// islevi: StopTime guncelleme kurallarini tanimlar.
// sistemdeki gorevi: Ortak kurallari update girdisine uygular.
/// <summary>StopTime guncelleme kurallarini tanimlar.</summary>
public sealed class UpdateStopTimeDtoValidator : StopTimeInputDtoValidatorBase<UpdateStopTimeDto>
{
}
