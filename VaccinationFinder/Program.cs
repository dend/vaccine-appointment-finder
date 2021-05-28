using System;
using System.Linq;
using System.Threading.Tasks;
using VaccinationFinder.Helpers;

namespace VaccinationFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("BC Vaccine Finder - Unofficial Tool");
            try
            {
                MainAsync().Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR] There was an error launching. Details: " + ex.Message);
            }
        }

        static async Task MainAsync()
        {
            var token = SystemHelper.GetTokens();

            if (token != null)
            {
                var region = "Vancouver";
                BCVaccinationAPIHelper helper = new BCVaccinationAPIHelper();
                Console.WriteLine($"[INFO] Looking for facilities in the specified region: {region}");

                var facilities = await helper.GetFacilities(region, token);

                if (facilities != null)
                {
                    foreach (var facility in facilities.Entities.First().Facilities.FacilityCollection)
                    {
                        Console.WriteLine($"[{facility.Name}] ({facility.Address})");

                        var facilityAppointments = await helper.GetFacilityDays(facility.Id, token);
                        if (facilityAppointments.Entities.First().Appointments.VaccinationFacilityAppointments != null &&
                            facilityAppointments.Entities.First().Appointments.VaccinationFacilityAppointments.Count > 0)
                        {
                            foreach (var day in facilityAppointments.Entities.First().Appointments.VaccinationFacilityAppointments)
                            {
                                Console.WriteLine("\x1b[4m\u001b[36m" + day.AppointmentDate + "\u001b[0m\x1b[0m");

                                var timeBlocks = await helper.GetTimeBlocks(day.Id, facility.Id, token);
                                foreach (var timeBlock in timeBlocks.Entities.First().AppointmentBlockCollection.AppointmentBlocks)
                                {
                                    var timeSpan = TimeSpan.FromMilliseconds(timeBlock.StartTime);
                                    Console.Write($"{timeSpan.Hours + ":" + timeSpan.Minutes.ToString("D2"),-10}");
                                }
                                Console.WriteLine();
                            }
                        }
                        else
                        {
                            Console.WriteLine("\u001b[33m[WARN]\u001b[0m No appointments available at this facility for the current period.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("\u001b[30m[ERROR]\u001b[0m No facilities returned from the API.");
                }
            }
            else
            {
                Console.WriteLine("\u001b[30m[ERROR]\u001b[0m Could not read the token file.");
            }
            Console.ReadKey();
        }
    }
}
