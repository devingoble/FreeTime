# FreeTime
A simple WebAPI project designed retrieve free time slots from an Exchange calendar. It was originally designed to provide appointment scheduling to a Laserfiche Forms front-end. It doesn't schedule the appointment, but relies on Laserfiche Workflow to do so in a more controlled back-end fashion. It supports more than one calendar so that 

This project is built on ASP.NET Core 3.0 and supports Exchange 2016 and above. It may also work with an Office 365 instance.

The way it works is to accept a number of parameters via a query string, and use that to query the specified Exchange server to determine free appointment slots that fit the supplied criteria.

### Required Parameters
#### Target date
A date string in a format recognized by .NET. The format MM-dd-yyyy works well.
#### Open
A datetime value that reflects the start of business hours that you want to query
#### Close
A datetime value that reflects the end of business hours that you want to query
#### Duration Minutes
The duration of the desired appointment. This works best if the entire day is divided up into slots of the same duration. For example, 8-5 with an hour for lunch and 15 minute appointments will give you 32 available appointments. FreeTime doesn't currently support mixed durations on a single calendar.
#### Calendars
A list of strings that represent the Exchange resource names for the calendars to be queried.
### Distribution Strategy
If more than one calendar is provided, FreeTime can use a couple of different methods to distribute available appointment slots.
- Fill - FreeTime will attempt to fill the calendars in the order that they appear. If a given time slot does not appear on the first available calendar, the next calendar will be used. If none of the calendars has an opening for the given time slot, then that slot will not be returned to the client.
- Balanced - Given multiple calendars, this strategy attempts to evenly divide available time slots between the calendars.

### Settings
There are two settings in appsettings.json that are global to the application:
#### CalendarSuffix
This represents the domain name of the exchange resource that will be queried. The suffix must include the @ symbol. For example, if the resource is mycalendar@mycompany.com, then the suffix is @mycompany.com.
#### TimeZone
Use this field to specify the time zone that you want to check for events. This is required since if you don't specify a time zone or specify a time zone different than one in which the events were created, you will get no results.

### Credentials
ASP.NET Core recommendations are to specify secrets in environment variables. You can do this either in your docker container, or at the OS level. An alternative solution, if you are running IIS is to uncomment the provided environment variables in the web.config and fill them in with values for your environment. The credentials are composed of a user name, password and domain. Since this is an unsecured public-facing endpoint, it's advised that these credentials be for an account that only has read-only access to the appropriate Exchange calendar(s).

# How it works
1. FreeTime queries Exchange to get a list of all appointments for each specified calendar. For security purposes, it only asks Exchange for the start and end times for each appointment.
2. A map of all possible appointments is created, given the provided parameters.
3. Appointment slots that overlap with the appointments from Exchange are removed from the map.
4. Using the specified distribution strategy the available appointment time slots are allocated to the provided calendar(s).
5. The distributed list of time slots is returned to the client.

