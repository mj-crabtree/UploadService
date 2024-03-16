using FilesService.Models;

namespace FilesService.Services.HttpClients;

public class MarkingClient : IMarkingStrategy
{
    public async Task<string> MarkFile(string filePath, string classificationTier)
    {
        throw new NotImplementedException();
    }
}