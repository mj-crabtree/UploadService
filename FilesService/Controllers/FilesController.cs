using AutoMapper;
using FilesService.Entities;
using FilesService.Models;
using FilesService.Services;
using Microsoft.AspNetCore.Mvc;

namespace FilesService.Controllers;

[ApiController]
[Route("api/files")]
public class FilesController : ControllerBase
{
    private readonly IFileHandler _fileHandler;
    private readonly ILogger<FilesController> _logger;
    private readonly IMapper _mapper;

    public FilesController(ILogger<FilesController> logger, IFileHandler fileHandler, IMapper mapper)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _fileHandler = fileHandler ?? throw new ArgumentNullException(nameof(fileHandler));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    public async Task<ActionResult<FileDto>> GetFile(Guid fileId)
    {
        if (fileId == Guid.Empty)
        {
            return NotFound();
        }

        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult<FileDto>> UploadFile(FileUploadCreationDto fileUploadCreationDto)
    {
        var fileEntity = _mapper.Map<FileEntity>(fileUploadCreationDto);
        await _fileHandler.HandleFile(fileEntity);
        return CreatedAtRoute(
            "GetFile",
            new { id = fileEntity.Id },
            _mapper.Map<FileDto>(fileEntity));
    }
}