namespace UploadService.Services;

class FilePersistenceService : IFilePersistenceService
{
    private const string TargetDirectory = "/app/data/files";

    public async Task<string> SaveFile(IFormFile file)
    {
        var safeFileName = CreateSafeFileName(file);
        var targetFilePath = Path.Combine(TargetDirectory, safeFileName);
        await SaveToFileSystemAsync(file, targetFilePath);
        return targetFilePath;
    }
    
    private static string CreateSafeFileName(IFormFile file)
    {
        return $"{Guid.NewGuid()}{Path.GetExtension(file.Name)}";
    }
    
    private static async Task SaveToFileSystemAsync(IFormFile file, string targetFilePath)
    {
        using (var stream = new FileStream(targetFilePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
    }
}