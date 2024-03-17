using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FilesService.Models;

namespace FilesService.Entities;

public class FileEntity
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();
    public ClassificationTier ClassificationTier { get; set; }
    public string UploadPath { get; set; }
    public string UserDefinedFileName { get; set; }
    public string FileType { get; set; }
    public string Path { get; set; }
    [NotMapped] public FormFile File { get; set; }
}