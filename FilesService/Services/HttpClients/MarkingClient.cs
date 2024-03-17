using System.Text;
using System.Text.Json;

namespace FilesService.Services.HttpClients;

public class MarkingClient : IMarkingStrategy
{
    private readonly IHttpClientFactory _clientFactory;
    private const string MarkingServiceUrl = "http://marking-service:8080/api/mark";

    public MarkingClient(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
    }
    public async Task<string> MarkFile(string filePath, string classificationTier)
    {
        var client = _clientFactory.CreateClient();
        var data = new
        {
            Path = filePath,
            ClassificationTier = classificationTier
        };
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var json = JsonSerializer.Serialize(data, options);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync(MarkingServiceUrl, content);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}