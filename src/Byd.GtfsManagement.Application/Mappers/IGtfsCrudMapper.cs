namespace Byd.GtfsManagement.Mappers;

// islevi: Ortak GTFS CRUD akisinin kullandigi uc Mapperly cevirimini tipli olarak tanimlar.
// sistemdeki gorevi: Yaprak AppService icinde tekrar eden mapper cagrilarini tabana tasir.
public interface IGtfsCrudMapper<TEntity, TCreateDto, TUpdateDto, TDto, TCreateModel, TUpdateModel>
{
    TCreateModel Map(TCreateDto input);
    TUpdateModel Map(TUpdateDto input);
    TDto Map(TEntity entity);
}
