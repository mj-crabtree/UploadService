using System.Runtime.CompilerServices;
using Microsoft.Extensions.Options;

namespace FilesService.Services;

internal class FilePersistenceService : IFilePersistenceService
{
    private readonly string _rootPath;

    public FilePersistenceService(IOptions<FileStoreSettings> options)
    {
        _rootPath = options.Value.RootPath;
    }

    public async Task<string> SaveFile(IFormFile file)
    {
        EnsureUploadFileSystemExists();
        var safeFileName = CreateSafeFileName(file);
        var targetFilePath = Path.Combine(_rootPath, safeFileName);
        await SaveToFileSystemAsync(file, targetFilePath);
        return targetFilePath;
    }

    private static string CreateSafeFileName(IFormFile file)
    {
        return $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
    }

    private static async Task SaveToFileSystemAsync(IFormFile file, string targetFilePath)
    {
        using (var stream = new FileStream(targetFilePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
    }

    private void EnsureUploadFileSystemExists()
    {
        if (!Directory.Exists(_rootPath))
        {
            Directory.CreateDirectory(_rootPath);
        }
    }
}