namespace FilesService.Services.HttpClients;

public class MockMarkingClient : IMarkingStrategy
{
    private const string MockProcessedPath = "data/mockProcessed";

    public  async Task<string> MarkFile(string filePath, string classificationTier)
    {
        var fileName = Path.GetFileName(filePath);
        var destFile = Path.Combine(MockProcessedPath, fileName);
        
        File.Copy(filePath, destFile, true);
        return destFile;
    }
}