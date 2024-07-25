using ShipmentService.Web.Models;
using System.Net.Http.Json;

namespace ShipmentService.Web.Services
{
    public class ShipmentServices : IShipmentService
    {
        private readonly HttpClient _httpClient;

        public ShipmentServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ShipmentDto> GetShipmentByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/shipments/{id}");

            if (!response.IsSuccessStatusCode)
            {
                // Логирование или обработка ошибки
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Request failed with status code {response.StatusCode}: {errorMessage}");
            }

            return await response.Content.ReadFromJsonAsync<ShipmentDto>();
        }

    }
}

