﻿using ApiConsumer.DTO;
using System.Net.Http.Json;

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

        public async Task<OgloszenieResponse> GetOgloszenieznieruchomosciaById(int id)
        {
            return await _httpClient.GetFromJsonAsync<OgloszenieResponse>($"/ogloszenies/{id}");
        }

        public async Task<List<OgloszenieResponse>> GetOgloszenieznieruchomoscia()
        {
            return await _httpClient.GetFromJsonAsync<List<OgloszenieResponse>>("/ogloszenies");
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
