using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Linq;
using System.Threading.Tasks;
using VaccinationFinder.Helpers;

namespace VaccinationFinder
{
    class Program
    {
        static int Main(string[] args)
        {
            Console.WriteLine("BC Vaccine Finder - Unofficial Tool");
            Console.WriteLine("Created by Den Delimarsky (https://den.dev)");

            var rootCommand = new RootCommand();
            rootCommand.AddOption(new Option<string>(
                   aliases: new[] { "--city", "-c" },
                   getDefaultValue: () => string.Empty,
                   description: "Determines the location for which appointment data should be obtained.")
            {
                IsRequired = true,
                AllowMultipleArgumentsPerToken = false
            });

            rootCommand.Handler = CommandHandler.Create<string>(HandleCommandLineArguments);
            return rootCommand.InvokeAsync(args).Result;
        }

        private static void HandleCommandLineArguments(string city)
        {
            if (!string.IsNullOrWhiteSpace(city))
            {
                try
                {
                    GetAppointmentDetails(city).Wait();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[ERROR] There was an error launching. Details: " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("\u001b[30m[ERROR]\u001b[0m No city was specified.");
            }
        }

        static async Task GetAppointmentDetails(string city)
        {
            var token = SystemHelper.GetTokens();

            if (token != null)
            {
                BCVaccinationAPIHelper helper = new();
                Console.WriteLine($"[INFO] Looking for facilities in the specified city: {city}");

                var facilities = await helper.GetFacilities(city, token);

                if (facilities != null)
                {
                    foreach (var facility in facilities.Entities.First().Facilities.FacilityCollection)
                    {
                        Console.WriteLine($"{facility.Name} ({facility.Address})");

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
