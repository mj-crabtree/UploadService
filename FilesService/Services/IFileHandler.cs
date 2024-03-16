using FilesService.Entities;

namespace FilesService.Services;

public interface IFileHandler
{
    Task HandleFile(FileEntity fileEntity);
}