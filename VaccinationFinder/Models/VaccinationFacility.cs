using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace VaccinationFinder.Models
{
    public class AuthoritativeVaccinationFacility
    {
        [JsonPropertyName("actions")]
        public List<AppointmentEntity> Entities { get; set; }
    }

    public class AppointmentEntity
    {
        public string Id { get; set; }
        public string State { get; set; }
        [JsonPropertyName("returnValue")]
        public VaccinationAppointmentList Appointments { get; set; }
        public List<object> Error { get; set; }
        public bool Storable { get; set; }
    }

    public class VaccinationAppointmentList
    {
        [JsonPropertyName("returnValue")]
        public List<VaccinationFacilityAppointment> VaccinationFacilityAppointments { get; set; }
        public bool Cacheable { get; set; }
    }

    public class VaccinationFacilityAppointment
    {
        public string Id { get; set; }
        [JsonPropertyName("DDH__HC_Appointments_Date__c")]
        public string AppointmentDate { get; set; }
        [JsonPropertyName("DDH__HC_Appointment_Type__c")]
        public string AppointmentType { get; set; }
    }

}
