using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UploadService.Entities;
using UploadService.Models;
using UploadService.Services;

namespace UploadService.Controllers;

[ApiController]
[Route("api/files")]
public class FileUploadController : ControllerBase
{
    private readonly IFileHandler _fileHandler;
    private readonly ILogger<FileUploadController> _logger;
    private readonly IMapper _mapper;

    public FileUploadController(ILogger<FileUploadController> logger, IFileHandler fileHandler, IMapper mapper)
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