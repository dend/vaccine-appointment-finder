using System.Text.Json.Serialization;

namespace VaccinationFinder.Models
{
    public class AuthoritativeFacilityList
    {
        public Action[] actions { get; set; }
        public Context context { get; set; }
        public Perfsummary perfSummary { get; set; }
    }

    public class Context
    {
        public string mode { get; set; }
        public string app { get; set; }
        public string contextPath { get; set; }
        public string pathPrefix { get; set; }
        public string fwuid { get; set; }
        public int mlr { get; set; }
        public Loaded loaded { get; set; }
        public Globalvalueprovider[] globalValueProviders { get; set; }
        public bool enableAccessChecks { get; set; }
        public int apce { get; set; }
        public string dns { get; set; }
        public int ls { get; set; }
        public string lv { get; set; }
        public Mna mna { get; set; }
        public int arse { get; set; }
        public string[] services { get; set; }
    }

    public class Loaded
    {
        public string APPLICATIONmarkupsiteforcecommunityApp { get; set; }
    }

    public class Mna
    {
        public string lightning { get; set; }
    }

    public class Globalvalueprovider
    {
        public string type { get; set; }
        public Values values { get; set; }
    }

    public class Values
    {
        public Eswconfigdevelopername eswConfigDeveloperName { get; set; }
        public Isvoiceover isVoiceOver { get; set; }
        public Setupappcontextid setupAppContextId { get; set; }
        public Density density { get; set; }
        public Srcdoc srcdoc { get; set; }
        public Appcontextid appContextId { get; set; }
        public Dynamictypesize dynamicTypeSize { get; set; }
    }

    public class Eswconfigdevelopername
    {
        public bool writable { get; set; }
        public string defaultValue { get; set; }
    }

    public class Isvoiceover
    {
        public bool writable { get; set; }
        public bool defaultValue { get; set; }
    }

    public class Setupappcontextid
    {
        public bool writable { get; set; }
        public string defaultValue { get; set; }
    }

    public class Density
    {
        public bool writable { get; set; }
        public string defaultValue { get; set; }
    }

    public class Srcdoc
    {
        public bool writable { get; set; }
        public bool defaultValue { get; set; }
    }

    public class Appcontextid
    {
        public bool writable { get; set; }
        public string defaultValue { get; set; }
    }

    public class Dynamictypesize
    {
        public bool writable { get; set; }
        public string defaultValue { get; set; }
    }

    public class Perfsummary
    {
        public string version { get; set; }
        public int request { get; set; }
        public Actions actions { get; set; }
        public int actionsTotal { get; set; }
        public int overhead { get; set; }
    }

    public class Actions
    {
        public _147A _147a { get; set; }
    }

    public class _147A
    {
        public int total { get; set; }
        public int db { get; set; }
    }

    public class Action
    {
        public string id { get; set; }
        public string state { get; set; }
        [JsonPropertyName("returnValue")]
        public FacilityList Facilities { get; set; }
        public object[] error { get; set; }
        public bool storable { get; set; }
    }

    public class FacilityList
    {
        [JsonPropertyName("returnValue")]
        public Facility[] FacilityCollection { get; set; }
        public bool cacheable { get; set; }
    }

    public class Facility
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string DDH__HC_Primary_Address__c { get; set; }
        public string DDH__HC_Primary_Address_1__c { get; set; }
        public string DDH__HC_Primary_City__c { get; set; }
        public string DDH__HC_Primary_Zip_Code__c { get; set; }
        public string DDH__HC_Primary_State__c { get; set; }
        public bool DDH__HC_Parking_Available__c { get; set; }
    }

}
