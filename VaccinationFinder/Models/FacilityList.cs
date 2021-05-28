using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace VaccinationFinder.Models
{
    public class AuthoritativeFacilityList
    {
        [JsonPropertyName("actions")]
        public List<Action> Entities { get; set; }
    }

    public class Action
    {
        public string Id { get; set; }
        public string State { get; set; }
        [JsonPropertyName("returnValue")]
        public FacilityList Facilities { get; set; }
        public List<object> Error { get; set; }
        public bool Storable { get; set; }
    }

    public class FacilityList
    {
        [JsonPropertyName("returnValue")]
        public List<Facility> FacilityCollection { get; set; }
        public bool Cacheable { get; set; }
    }

    public class Facility
    {
        public string Id { get; set; }
        public string Name { get; set; }
        [JsonPropertyName("DDH__HC_Primary_Address__c")]
        public string ComplexId { get; set; }
        [JsonPropertyName("DDH__HC_Primary_Address_1__c")]
        public string Address { get; set; }
        [JsonPropertyName("DDH__HC_Primary_City__c")]
        public string City { get; set; }
        [JsonPropertyName("DDH__HC_Primary_Zip_Code__c")]
        public string PostalCode { get; set; }
        [JsonPropertyName("DDH__HC_Primary_State__c")]
        public string Province { get; set; }
        [JsonPropertyName("DDH__HC_Parking_Available__c")]
        public bool ParkingAvailable { get; set; }
    }

}
