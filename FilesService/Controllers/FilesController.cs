using AutoMapper;
using FilesService.Entities;
using FilesService.Models;
using FilesService.Services;
using FilesService.Services.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FilesService.Controllers;

[ApiController]
[Route("api/files")]
public class FilesController : ControllerBase
{
    private readonly IFileHandler _fileHandler;
    private readonly ILogger<FilesController> _logger;
    private readonly IMapper _mapper;
    private readonly IFileRepository _repository;

    public FilesController(ILogger<FilesController> logger, IFileHandler fileHandler, IMapper mapper, IFileRepository repository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _fileHandler = fileHandler ?? throw new ArgumentNullException(nameof(fileHandler));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }
    
    [HttpGet]
    [Route("{fileId}", Name = "GetFile")]
    public async Task<ActionResult<FileDto>> GetFile(Guid fileId)
    {
        if (fileId == Guid.Empty)
        {
            return NotFound();
        }
        var fileEntity = await _repository.GetFile(fileId);
        return Ok(_mapper.Map<FileDto>(fileEntity));
    }

    [HttpPost]
    public async Task<ActionResult<FileDto>> UploadFile(FileUploadCreationDto fileUploadCreationDto)
    {
        var fileEntity = _mapper.Map<FileEntity>(fileUploadCreationDto);
        await _fileHandler.HandleFile(fileEntity);
        return CreatedAtRoute(
            "GetFile",
            new { fileId = fileEntity.Id },
            _mapper.Map<FileDto>(fileEntity));
    }
}