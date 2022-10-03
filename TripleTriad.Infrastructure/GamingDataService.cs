using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TripleTriad.Infrastructure.Models;

namespace TripleTriad.Infrastructure
{
    /// <summary>
    /// Responsable de l'interaction avec Azure Table
    /// </summary>
    public class GamingDataService : IPersistenceRepository
    {
        private readonly string _gameName;
        private readonly string _playerId;
        private HttpClient _client;
        
        public GamingDataService(string baseUrl, string gameName, string playerId)
        {
            _gameName = gameName;
            _playerId = playerId;
            _client = new HttpClient();
            _client.BaseAddress = new Uri($"{baseUrl}"); 
        }

        public async Task SaveData(Dictionary<int, string> jsonDatas)
        {
           try
           {
               var json = JsonConvert.SerializeObject(jsonDatas, Formatting.Indented);
               var content = new StringContent(json, Encoding.UTF8, "application/json");
               HttpResponseMessage result = await _client.PostAsync($"/api/gamedata/{_gameName}/{_playerId}", content);
           }
           catch (Exception ex)
           {
              string message = ex.Message; 
           }
        }

        public async Task<Dictionary<int, string>> LoadData()
        {
            HttpRequestMessage request = new HttpRequestMessage();
            var response = await _client.GetAsync($"/api/gamedata/{_gameName}/{_playerId}");
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Dictionary<int, string>>(result);
        }

        public async Task<List<CardDto>> LoadAllCards()
        {
            HttpRequestMessage request = new HttpRequestMessage();
            var response = await _client.GetAsync($"/api/cards");
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<CardDto>>(result);
        }
    }
}