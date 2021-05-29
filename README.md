<div align="center">
	<img alt="Vaccine Appointment Finder icon" src="images/logo-256.png" width="200" height="200" />
	<h1>💉 BC COVID-19 Vaccine Appointment Finder</h1>
	<p>
		<b>Find a vaccine appointment in British Columbia near you.</b>
	</p>
	<br>
	<br>
	<br>
</div>

To find a vaccine appointment in British Columbia, and especially if you have one booked - requires canceling the existing one. Not the most optimal approach if you are worried about losing your spot in line.

This tool allows finding all available vaccine appointments in a specified city in BC without canceling the existing time slot. I wrote about this in detail [in my blog post](https://den.dev/blog/vaccine/).

<div align="center">
	<img alt="Demo of the Vaccine Appointment Finder" src="images/vac-demo.gif" width="800">
</div>

## Building

Prerequisite: `dotnet` should be installed on your machine (see https://aka.ms/net for details on how to aquire and install the tools).

1. Open a cmd shell or PowerShell instance
2. Navigate to where you cloned this repo
3. Build the binaries. `> dotnet build -c Release -o bin`
4. Copy your `tokens.json` file to the bin folder: `> cp tokens.json bin/`

## Running

To run the application, you will need to create a `tokens.json` file in the project folder, that matches the following content:

```json
{
	"fwuid": "YOUR_FWUID_HERE",
	"appid": "YOUR_APPID_HERE"
}
```

You can get both values when you navigate to `https://www.getvaccinated.gov.bc.ca/s/booking`, and looking at the traffic through the network inspector in your favorite web browser ("F12 tools" in Firefox, Chrome, Edge). Search headers for `aura.context` form data in a POST request.

To run the application, you can use:

```bash
vacfind.exe --city Vancouver
```

The `--city` parameter should contain a city in the province of British Columbia, and is required.

## FAQ

### Does this tool allow me to pick which vaccine I will get?

No. This is determined by the BC government.

### Does this tool guarantee an appointment slot?

No. It only shows available times at the time of the query.

### Does this tool allow me to book an appointment?

No. It only shows available times. To book an appointment, you will need to [register for a vaccination](https://www2.gov.bc.ca/gov/content/covid-19/vaccine/register).

### Does this tool require me to provide any personally identifiable information?

No. The tool only queries the official BC vaccination appointment service to get the times. That's it. No telemetry, no data being collected.

### The tool showed an available time slot, but when I cancelled and went to register, it was gone. Why is that?

The tool only shows available times at the time of the query. Between running the application and booking a new appointment, the time slot might be gone. As mentioned earlier, the tool does not guarantee an appointment.
