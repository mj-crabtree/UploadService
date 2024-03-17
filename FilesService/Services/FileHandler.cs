using FilesService.Entities;
using FilesService.Services.HttpClients;
using FilesService.Services.Repositories;

namespace FilesService.Services;

public class FileHandler : IFileHandler
{
    private readonly IFilePersistenceService _persistenceService;
    private readonly ILogger<FileHandler> _logger;
    private readonly IMarkingStrategy _markingClient;
    private readonly IFileRepository _fileRepository;

    public FileHandler(ILogger<FileHandler> logger, IFilePersistenceService persistenceService,
        IMarkingStrategy markingClient, IFileRepository fileRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _persistenceService =
            persistenceService ?? throw new ArgumentNullException(nameof(persistenceService));
        _markingClient = markingClient ?? throw new ArgumentNullException(nameof(markingClient));
        _fileRepository = fileRepository;
    }

    public async Task HandleFile(FileEntity fileEntity)
    {
        try
        {
            VerifyFile(fileEntity);
            SetFileProperties(fileEntity);
            await PersistUnmarkedFile(fileEntity);
            await AddTorepository(fileEntity);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred when handling the file");
            throw;
        }
    }

    private void VerifyFile(FileEntity fileEntity)
    {
        if (!Validate(fileEntity))
        {
            _logger.LogError($"File verification failed: {nameof(fileEntity)}");
            throw new InvalidDataException(nameof(fileEntity));
        }
    }

    private async Task AddTorepository(FileEntity fileEntity)
    {
        _fileRepository.Add(fileEntity);
        await _fileRepository.SaveChanges();
    }

    private void SetFileProperties(FileEntity fileEntity)
    {
        fileEntity.FileType = Path.GetExtension(fileEntity.File.FileName);
        fileEntity.UserDefinedFileName = Path.GetFileName(fileEntity.File.FileName);
    }

    private async Task PersistUnmarkedFile(FileEntity fileEntity)
    {
        fileEntity.UploadPath = await _persistenceService.SaveFile(fileEntity.File);
        fileEntity.Path = await _markingClient.MarkFile(
            fileEntity.UploadPath, 
            fileEntity.ClassificationTier.ToString());
    }

    private bool Validate(FileEntity file)
    {
        return true;
    }
}