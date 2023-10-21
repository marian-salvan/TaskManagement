using System.Text.Json;
using TaskManagement.Core.Constants;
using TaskManagement.Core.Interfaces;
using TaskManagement.Core.Responses;

namespace TaskManagement.API.Integrations
{
    //external integrations could be kept in different class library
    //for the sake of simplicity, we are keeping it in the same project
    public class TaskSummaryGenerator : ITaskSummaryGenerator
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<TaskSummaryGenerator> _logger;

        public TaskSummaryGenerator(IHttpClientFactory httpClientFactory, ILogger<TaskSummaryGenerator> logger)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _logger = logger;
        }

        public async Task<string> GetTaskDescription()
        {
            var httpClient = _httpClientFactory.CreateClient("CatFact");
            var httpResponseMessage = await httpClient.GetAsync("fact");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var catFactResponse = await JsonSerializer.DeserializeAsync<CatFactResponse>(contentStream, options);

                return catFactResponse is not null ? catFactResponse.Fact : MessageConstants.TaskSummaryNotFound;
            }

            return MessageConstants.TaskSummaryNotFound;
        }
    }
}
