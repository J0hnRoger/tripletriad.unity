using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TripleTriad.Infrastructure
{
    public class JsonPersistenceRepository : IPersistenceRepository
    {
        private readonly string _filePath;

        public JsonPersistenceRepository(string filePath)
        {
            _filePath = filePath;
        }
        
        public async Task SaveData(Dictionary<int, string> jsonDatas)
        {
           // TODO construct JSON
           var json = JsonConvert.SerializeObject(jsonDatas);
            // For mobile
            using (StreamWriter writer = new StreamWriter(_filePath))
            {
                await writer.WriteAsync(json);
            }
        }

        public async Task<Dictionary<int, string>> LoadData()
        {
            if (!File.Exists(_filePath))
                throw new Exception($"Path doesn't exist on mobile {_filePath}");

            using (StreamReader reader = new StreamReader(_filePath))
            {
                var result = await reader.ReadToEndAsync();
                return JsonConvert.DeserializeObject<Dictionary<int, string>>(result);
            }
        }
    }
}