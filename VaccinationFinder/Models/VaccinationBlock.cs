using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace VaccinationFinder.Models
{

    public class VaccinationBlock
    {
        [JsonPropertyName("actions")]
        public List<AppointmentBlockInformation> Entities { get; set; }
    }

    public class AppointmentBlockInformation
    {
        public string Id { get; set; }
        public string State { get; set; }
        [JsonPropertyName("returnValue")]
        public AppointmentBlockCollection AppointmentBlockCollection { get; set; }
        public List<object> Error { get; set; }
        public bool Storable { get; set; }
    }

    public class AppointmentBlockCollection
    {
        [JsonPropertyName("returnValue")]
        public AppointmentBlock[] AppointmentBlocks { get; set; }
        public bool Cacheable { get; set; }
    }

    public class AppointmentBlock
    {
        public string Id { get; set; }
        [JsonPropertyName("DDH__HC_Start_Time__c")]
        public int StartTime { get; set; }
        [JsonPropertyName("DDH__HC_End_Time__c")]
        public int EndTime { get; set; }
        [JsonPropertyName("DDH__HC_Provider_ID__c")]
        public string ProviderId { get; set; }
        [JsonPropertyName("DDH__HC_Date__c")]
        public string Date { get; set; }
        [JsonPropertyName("DDH__HC_Appointment_Day_Management__c")]
        public string AppointmentDay { get; set; }
        [JsonPropertyName("DDH__HC_Appointment_Type__c")]
        public string AppointmentType { get; set; }
    }

}
