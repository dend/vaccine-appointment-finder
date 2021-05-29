using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using VaccinationFinder.Models;

namespace VaccinationFinder
{
    public class BCVaccinationAPIHelper
    {
        private readonly string MessageKey = "message";
        private readonly string ContextKey = "aura.context";
        private readonly string TokenKey = "aura.token";

        private readonly string? VaccinationUrl = "https://www.getvaccinated.gov.bc.ca/s/sfsites/aura?r=11&aura.ApexAction.execute=1";
        private readonly string FacilityMessage = "{{\"actions\":[{{\"id\":\"147;a\",\"descriptor\":\"aura://ApexActionController/ACTION$execute\",\"callingDescriptor\":\"UNKNOWN\",\"params\":{{\"namespace\":\"\",\"classname\":\"BCH_SchedulerController\",\"method\":\"getFacilities\",\"params\":{{\"territory\":\"{0}\",\"priorityCode\":\"\"}},\"cacheable\":true,\"isContinuation\":false}}}}]}}";
        private readonly string SpecificFacilityMessage = "{{\"actions\":[{{\"id\":\"149;a\",\"descriptor\":\"aura://ApexActionController/ACTION$execute\",\"callingDescriptor\":\"UNKNOWN\",\"params\":{{\"namespace\":\"\",\"classname\":\"BCH_SchedulerController\",\"method\":\"getAppointmentDays\",\"params\":{{\"facility\":\"{0}\",\"appointmentType\":\"COVID-19 Vaccination\"}},\"cacheable\":true,\"isContinuation\":false}}}}]}}";
        private readonly string AppointmentBlockMessage = "{{\"actions\":[{{\"id\":\"156;a\",\"descriptor\":\"aura://ApexActionController/ACTION$execute\",\"callingDescriptor\":\"UNKNOWN\",\"params\":{{\"namespace\":\"\",\"classname\":\"BCH_SchedulerController\",\"method\":\"getAppointmentBlocks\",\"params\":{{\"appointmentDay\":\"{0}\", \"facility\":\"{1}\",\"appointmentType\":\"COVID-19 Vaccination\"}},\"cacheable\":true,\"isContinuation\":false}}}}]}}";
        private readonly string AuraContext = "{{\"mode\":\"PROD\",\"fwuid\":\"{0}\",\"app\":\"siteforce:communityApp\",\"loaded\":{{\"APPLICATION@markup://siteforce:communityApp\":\"{1}\"}},\"dn\":[],\"globals\":{{}},\"uad\":false}}";
        private readonly string AuraToken = "undefined";

        public async Task<AuthoritativeFacilityList> GetFacilities(string region, AuraToken token)
        {
            List<KeyValuePair<string, string>> _requestValues = new()
            {
                new KeyValuePair<string, string>(MessageKey, string.Format(FacilityMessage, region)),
                new KeyValuePair<string, string>(ContextKey, string.Format(AuraContext, token.FwuId, token.AppId)),
                new KeyValuePair<string, string>(TokenKey, AuraToken)
            };

            return await SendRequest<AuthoritativeFacilityList>(_requestValues);
        }

        public async Task<AuthoritativeVaccinationFacility> GetFacilityDays(string facilityId, AuraToken token)
        {
            List<KeyValuePair<string, string>> _requestValues = new()
            {
                new KeyValuePair<string, string>(MessageKey, string.Format(SpecificFacilityMessage, facilityId)),
                new KeyValuePair<string, string>(ContextKey, string.Format(AuraContext, token.FwuId, token.AppId)),
                new KeyValuePair<string, string>(TokenKey, AuraToken)
            };

            return await SendRequest<AuthoritativeVaccinationFacility>(_requestValues);
        }

        public async Task<VaccinationBlock> GetTimeBlocks(string dayId, string facility, AuraToken token)
        {
            List<KeyValuePair<string, string>> _requestValues = new()
            {
                new KeyValuePair<string, string>(MessageKey, string.Format(AppointmentBlockMessage, dayId, facility)),
                new KeyValuePair<string, string>(ContextKey, string.Format(AuraContext, token.FwuId, token.AppId)),
                new KeyValuePair<string, string>(TokenKey, AuraToken)
            };

            return await SendRequest<VaccinationBlock>(_requestValues);
        }

        private async Task<T> SendRequest<T>(List<KeyValuePair<string, string>> messages)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, VaccinationUrl)
            {
                Content = new FormUrlEncodedContent(messages)
            };

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                return JsonSerializer.Deserialize<T>(result);
            }
            else
            {
                return default;
            }
        }
    }
}
