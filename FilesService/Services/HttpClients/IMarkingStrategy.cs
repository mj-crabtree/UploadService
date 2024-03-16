namespace FilesService.Services.HttpClients;

public interface IMarkingStrategy
{
    Task<string> MarkFile(string filePath, string classificationTier);
}