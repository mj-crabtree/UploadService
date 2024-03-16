namespace FilesService.Models;

public class FileUploadCreationDto
{
    public IFormFile File { get; set; }
    public ClassificationTier ClassificationTier { get; set; }
}