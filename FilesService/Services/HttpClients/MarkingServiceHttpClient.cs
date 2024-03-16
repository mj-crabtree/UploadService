using FilesService.Models;

namespace FilesService.Services.HttpClients;

public class MarkingServiceHttpClient : IMarkingServiceHttpClient
{
    public Task MarkFile(IFormFile file, ClassificationTier classificationTier)
    {
        throw new NotImplementedException();
    }
}