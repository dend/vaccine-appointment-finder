using System.Text.Json.Serialization;

namespace VaccinationFinder.Models
{

    public class VaccinationBlock
    {
        public AppointmentBlockInformation[] actions { get; set; }
        public Context context { get; set; }
        public Perfsummary perfSummary { get; set; }
    }

    public class AppointmentBlockInformation
    {
        public string id { get; set; }
        public string state { get; set; }
        [JsonPropertyName("returnValue")]
        public AppointmentBlockCollection AppointmentBlockCollection { get; set; }
        public object[] error { get; set; }
        public bool storable { get; set; }
    }

    public class AppointmentBlockCollection
    {
        [JsonPropertyName("returnValue")]
        public AppointmentBlock[] AppointmentBlocks { get; set; }
        public bool cacheable { get; set; }
    }

    public class AppointmentBlock
    {
        public string Id { get; set; }
        public int DDH__HC_Start_Time__c { get; set; }
        public int DDH__HC_End_Time__c { get; set; }
        public string DDH__HC_Provider_ID__c { get; set; }
        public string DDH__HC_Date__c { get; set; }
        public string DDH__HC_Appointment_Day_Management__c { get; set; }
        public string DDH__HC_Appointment_Type__c { get; set; }
    }

}
