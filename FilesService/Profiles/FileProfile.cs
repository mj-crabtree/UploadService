using AutoMapper;
using FilesService.Entities;
using FilesService.Models;

namespace FilesService.Profiles;

public class FileProfile : Profile
{
    public FileProfile()
    {
        CreateMap<FileUploadCreationDto, FileEntity>();
        CreateMap<FileEntity, FileDto>();
    }
}