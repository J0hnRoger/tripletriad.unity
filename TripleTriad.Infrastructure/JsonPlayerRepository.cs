using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TripleTriad.Infrastructure.Models;

namespace TripleTriad.Infrastructure
{
    public class JsonPlayerRepository 
    {
        private readonly string _path;

        public JsonPlayerRepository(string path)
        {
            _path = path;
        }
        
        public async Task<PlayerDto> LoadCurrentPlayer()
        {
            PlayerDto playerDto = null;
            if (!File.Exists(_path))
                throw new Exception($"Path doesn't exist on mobile {_path}");

            using (StreamReader reader = new StreamReader(_path))
            {
                var result = await reader.ReadToEndAsync();
                playerDto  = JsonConvert.DeserializeObject<PlayerDto>(result);
            }
            return playerDto;
        }

        public async Task SavePlayer(PlayerDto current)
        {
            var json = JsonConvert.SerializeObject(current);
            // For mobile
            using (StreamWriter writer = new StreamWriter(_path))
            {
                await writer.WriteAsync(json);
            }
        }
    }
}
