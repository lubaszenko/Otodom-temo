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
    }
}
