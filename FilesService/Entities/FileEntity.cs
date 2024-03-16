using FilesService.Models;

namespace FilesService.Entities;

public class FileEntity
{
    public Guid Id { get; } = Guid.NewGuid();
    public IFormFile File { get; set; }
    public ClassificationTier ClassificationTier { get; set; }
}