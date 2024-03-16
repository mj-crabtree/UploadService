using FilesService.Models;

namespace FilesService.Services.HttpClients;

public interface IMarkingServiceHttpClient
{
    Task MarkFile(IFormFile file, ClassificationTier classificationTier);
}