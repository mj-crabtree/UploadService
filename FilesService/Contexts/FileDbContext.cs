using FilesService.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilesService.Contexts;

public class FileDbContext : DbContext
{
    public FileDbContext(DbContextOptions<FileDbContext> options) : base(options)
    {
    }
    public DbSet<FileEntity> Files { get; set; }
}