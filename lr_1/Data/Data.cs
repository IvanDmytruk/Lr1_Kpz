using System.Text.Json;
using System.Text.Json.Serialization;
using Lr_1.Models;

namespace Lr_1.Data
{
    public class Data
    {
        public List<Anime> Animes { get; set; } = new();

        public static Data Load(string filePath)
        {
            string jsonString = File.ReadAllText(filePath);

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            options.Converters.Add(new JsonStringEnumConverter());

            Data? data = JsonSerializer.Deserialize<Data>(jsonString,options);
            if(data == null)
            {
                throw new Exception("Failed to deserialize data.");
            }
            return data;
        }
    }
}