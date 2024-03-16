using FilesService.Services;
using FilesService.Services.HttpClients;
using FilesService.Services.Repositories;
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

builder.Services.Configure<FileStoreSettings>(builder.Configuration.GetSection("FileStore"));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IFilePersistenceService, FilePersistenceService>();
builder.Services.AddScoped<IMarkingServiceHttpClient, MarkingServiceHttpClient>();
builder.Services.AddScoped<IFileHandler, FileHandler>();
builder.Services.AddScoped<IFileRepository, FileRepository>();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.MapControllers();
app.UseHttpsRedirection();
app.Run();