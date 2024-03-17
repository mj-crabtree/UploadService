using FilesService.Contexts;
using FilesService.Services;
using FilesService.Services.HttpClients;
using FilesService.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/uploadInfo.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.UseSerilog();

#region CustomMiddleware

builder.Services.AddHttpClient();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<FileDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection")));

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddScoped<IMarkingStrategy, MockMarkingClient>();
}
else
{
    builder.Services.AddScoped<IMarkingStrategy, MarkingClient>();
}

builder.Services.Configure<FileStoreSettings>(builder.Configuration.GetSection("FileStore"));
builder.Services.AddScoped<IFilePersistenceService, FilePersistenceService>();
builder.Services.AddScoped<IFileHandler, FileHandler>();
builder.Services.AddScoped<IFileRepository, FileRepository>();

#endregion

var app = builder.Build();

app.MapControllers();
app.UseHttpsRedirection();
app.Run();