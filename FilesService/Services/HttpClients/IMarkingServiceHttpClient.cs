using UploadService.Models;

namespace UploadService.Services;

public interface IMarkingServiceHttpClient
{
    Task MarkFile(IFormFile file, ClassificationTier classificationTier);
}