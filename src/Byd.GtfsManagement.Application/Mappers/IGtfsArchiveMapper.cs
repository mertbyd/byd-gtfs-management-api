namespace Byd.GtfsManagement.Mappers;

// islevi: ImportDto ile entity arasindaki iki yonlu Mapperly sozlesmesini tanimlar.
// sistemdeki gorevi: CRUD tabaninin mevcut entity mapper'larini ayni tipli arsiv akisinda kullanmasini saglar.
public interface IGtfsArchiveMapper<TImportDto, TEntity>
{
    // CSV DTO'sunu entity'ye esler.
    TEntity Map(TImportDto input);

    // Entity'yi export icin CSV DTO'suna esler.
    TImportDto MapToImport(TEntity entity);
}
