namespace UploadService.Models;

public class FileUploadCreationDto
{
    public IFormFile File { get; set; }
    public ClassificationTier ClassificationTier { get; set; }
}

public enum ClassificationTier
{
    Official,
    Secret,
    TopSecret
}