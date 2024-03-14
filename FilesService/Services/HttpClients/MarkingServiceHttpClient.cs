using UploadService.Models;

namespace UploadService.Services;

public class MarkingServiceHttpClient : IMarkingServiceHttpClient
{

    public Task MarkFile(IFormFile file, ClassificationTier classificationTier)
    {
        throw new NotImplementedException();
    }
}