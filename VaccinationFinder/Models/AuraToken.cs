using System.Text.Json.Serialization;

namespace VaccinationFinder.Models
{
    public class AuraToken
    {
        [JsonPropertyName("fwuid")]
        public string FwuId { get; set; }
        [JsonPropertyName("appid")]
        public string AppId { get; set; }
    }
}
