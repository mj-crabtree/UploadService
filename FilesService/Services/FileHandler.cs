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
        if (!VerifyFile(fileEntity))
        {
            _logger.LogError($"File verification failed: {nameof(fileEntity)}");
            throw new InvalidDataException(nameof(fileEntity));
        }

        fileEntity.UploadPath = await _persistenceService.SaveFile(fileEntity.File);
        fileEntity.Path = await _markingClient.MarkFile(
                fileEntity.UploadPath, 
                fileEntity.ClassificationTier.ToString());
        
        _fileRepository.Add(fileEntity);
        await _fileRepository.SaveChanges();
    }

    private bool VerifyFile(FileEntity file)
    {
        return true;
    }
}