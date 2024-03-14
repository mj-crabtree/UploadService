using UploadService.Models;

namespace UploadService.Entities;

public class FileEntity
{
    public Guid Id { get; set; }
    public IFormFile File { get; set; }
    public ClassificationTier ClassificationTier { get; set; }
}