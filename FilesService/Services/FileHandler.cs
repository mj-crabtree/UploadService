using FilesService.Entities;
using FilesService.Services.HttpClients;
using FilesService.Services.Repositories;

namespace FilesService.Services;

public class FileHandler : IFileHandler
{
    private readonly IFilePersistenceService _filePersistenceService;
    private readonly ILogger<FileHandler> _logger;
    private readonly IMarkingServiceHttpClient _markingClient;
    private readonly IFileRepository _fileRepository;

    public FileHandler(ILogger<FileHandler> logger, IFilePersistenceService filePersistenceService,
        IMarkingServiceHttpClient markingClient, IFileRepository fileRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _filePersistenceService =
            filePersistenceService ?? throw new ArgumentNullException(nameof(filePersistenceService));
        _markingClient = markingClient ?? throw new ArgumentNullException(nameof(markingClient));
        _fileRepository = fileRepository;
    }

    public async Task HandleFile(FileEntity fileEntity)
    {
        if (!VerifyFile(fileEntity))
        {
            throw new InvalidDataException(nameof(fileEntity));
        }

        await _filePersistenceService.SaveFile(fileEntity.File);
        _markingClient.MarkFile(fileEntity.File, fileEntity.ClassificationTier);
    }

    private bool VerifyFile(FileEntity file)
    {
        return true;
    }
}