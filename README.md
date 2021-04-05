# WebAPI Samples

A collection of Web APIs made with ASP.NET Core 5.0 and related clients to show their usage.

**[My First Web API](src/MyFirstWebApi/)**

A sample ASP.NET Core Web API that uses [OpenWeatherMap](https://openweathermap.org) to retrieve current and forecast wheather. It features:

- Basic Swagger / OpenAPI integration
- Dependency Injection
- Application settings
- Caching
- Json serialization and deserialization with System.Text.Json 

In order to execute this sample, you need to register a free account on [OpenWeatherMap](https://openweathermap.org), generate an API Key and then set it in the [appsettings.json file](src/MyFirstWebApi/appsettings.json#L3).

This sample comes with a WPF client made with .NET 5.0, showing how to use [Refit](https://github.com/reactiveui/refit) to make API calls.

**[Raspberry Control](src/RaspberryControl/)**

A sample that shows how to control PINs on a Raspberry Pi Board running ASP.NET Core on Linux. It features:

- Basic Swagger / OpenAPI integration
- Usage of System.Devices.Gpio

**Contribute**

The project is continuously evolving. We welcome contributions. Feel free to file issues and pull requests on the repo and we'll address them as we can.
