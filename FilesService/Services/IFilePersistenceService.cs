namespace FilesService.Services;

public interface IFilePersistenceService
{
    Task<string> SaveFile(IFormFile file);
}