using System.IO;
using System.Text.Json;
using VaccinationFinder.Models;

namespace VaccinationFinder.Helpers
{
    public static class SystemHelper
    {
        private const string TokensFile = "tokens.json";

        public static AuraToken GetTokens()
        {
            using StreamReader reader = new(TokensFile);
            string json = reader.ReadToEnd();
            return JsonSerializer.Deserialize<AuraToken>(json);
        }
    }
}
