using System;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("BC Vaccine Finder");
            MainAsync().Wait();
        }

        static async Task MainAsync()
        {
            var region = "Vancouver";
            BCVaccinationAPIHelper helper = new BCVaccinationAPIHelper();
            Console.WriteLine($"Looking for facilities in the specified region: {region}");

            var facilities = await helper.GetFacilities(region);
            foreach (var facility in facilities.actions.First().Facilities.FacilityCollection)
            {
                Console.WriteLine($"[{facility.Name}] ({facility.DDH__HC_Primary_Address_1__c})");

                var facilityAppointments = await helper.GetFacilityDays(facility.Id);
                if (facilityAppointments.actions.First().Appointments.VaccinationFacilityAppointments != null &&
                    facilityAppointments.actions.First().Appointments.VaccinationFacilityAppointments.Length > 0)
                {
                    Console.WriteLine("For this facility, the following days are available this month:");
                    foreach (var day in facilityAppointments.actions.First().Appointments.VaccinationFacilityAppointments)
                    {
                        Console.WriteLine(day.DDH__HC_Appointments_Date__c);

                        var timeBlocks = await helper.GetTimeBlocks(day.Id, facility.Id);
                        foreach(var timeBlock in timeBlocks.actions.First().AppointmentBlockCollection.AppointmentBlocks)
                        {
                            var timeSpan = TimeSpan.FromMilliseconds(timeBlock.DDH__HC_Start_Time__c);
                            Console.Write($"{timeSpan.Hours}:{timeSpan.Minutes} ");
                        }
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("[!] No appointments available at this facility for current month.");
                }
            }
            Console.ReadKey();
        }
    }
}
