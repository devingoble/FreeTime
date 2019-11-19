# FreeTime
A simple WebAPI project designed retrieve free time slots from an Exchange calendar. It was originally designed to provide appointment scheduling to a Laserfiche Forms front-end. It doesn't schedule the appointment, but relies on Laserfiche Workflow to do so in a more controlled back-end fashion. It supports more than one calendar so that 

This project is built on ASP.NET Core 3.0 and supports Exchange 2016 and above. It may also work with an Office 365 instance.

The way it works is to accept a number of parameters via a query string, and use that to query the specified Exchange server to determine free appointment slots that fit the supplied criteria.

For more information see [the wiki](https://github.com/devingoble/FreeTime/wiki).
