using Serilog;
using UploadService.Services;

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

builder.Services.AddScoped<IFilePersistenceService, FilePersistenceService>();
builder.Services.AddScoped<IMarkingServiceHttpClient, MarkingServiceHttpClient>();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.MapControllers();
app.UseHttpsRedirection();
app.Run();