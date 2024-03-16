using FilesService.Contexts;
using FilesService.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilesService.Services.Repositories;

public interface IFileRepository
{
    Task<FileEntity> GetFile(Guid fileId);
    void Add(FileEntity file);
    Task<bool> SaveChanges();
}

public class FileRepository : IFileRepository
{
    private readonly FileDbContext _context;

    public FileRepository(FileDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<FileEntity> GetFile(Guid fileId)
    {
        if (fileId == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(fileId));
        }

        return (await _context.Files.FirstOrDefaultAsync(f => f.Id == fileId))!;
    }

    public void Add(FileEntity file)
    {
        if (file == null)
        {
            throw new ArgumentNullException(nameof(file));
        }

        _context.Files.Add(file);
    }

    public async Task<bool> SaveChanges()
    {
        return await _context.SaveChangesAsync() >= 0;
    }
}