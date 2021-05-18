using System.Text.Json.Serialization;

namespace VaccinationFinder.Models
{
    public class AuthoritativeVaccinationFacility
    {
        public AppointmentEntity[] actions { get; set; }
        public Context context { get; set; }
        public Perfsummary perfSummary { get; set; }
    }

    public class _149A
    {
        public int total { get; set; }
        public int db { get; set; }
    }

    public class AppointmentEntity
    {
        public string id { get; set; }
        public string state { get; set; }
        [JsonPropertyName("returnValue")]
        public VaccinationAppointmentList Appointments { get; set; }
        public object[] error { get; set; }
        public bool storable { get; set; }
    }

    public class VaccinationAppointmentList
    {
        [JsonPropertyName("returnValue")]
        public VaccinationFacilityAppointment[] VaccinationFacilityAppointments { get; set; }
        public bool cacheable { get; set; }
    }

    public class VaccinationFacilityAppointment
    {
        public string Id { get; set; }
        public string DDH__HC_Appointments_Date__c { get; set; }
        public string DDH__HC_Appointment_Type__c { get; set; }
    }

}
