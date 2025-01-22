using ApiConsumer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ApiConsumer
{
    public class ApiClientService
    {
        private readonly HttpClient _httpClient;

        public ApiClientService(ApiClientOptions apiClientOptions)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new System.Uri(apiClientOptions.ApiBaseAddress);
        }

        public async Task<List<AgencjaResponse>> GetAgencjas()
        {
            return await _httpClient.GetFromJsonAsync<List<AgencjaResponse>>("/agencjas");
        }

        public async Task<OgloszenieResponse> PostOgloszenieZnieruchomoscia(OgloszenieRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("/ogloszenies", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<OgloszenieResponse>();
        }

        public async Task<bool> DeleteOgloszenieAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"/ogloszenies/{id}");
            response.EnsureSuccessStatusCode();
            return response.IsSuccessStatusCode;
        }
    }
}
