# Antsy Web Framework

[![Build status](https://ci.appveyor.com/api/projects/status/vjdj2snfeh9b454u?svg=true)](https://ci.appveyor.com/project/TrexinanF14/antsy)

This is a simple web framework for people who just want to get going with as few lines as possible.

To get started create a new console application and add via
[nuget](https://www.nuget.org/packages/Antsy/):
```
install-package antsy
[or]
dotnet add package antsy
```

Then use this to start up your web app:

```csharp
using Antsy;

class Program
{
    static void Main(string[] args)
    {
        var host = new AntsyHost(port: 8000);
        host.Get("/hello", (req, res) =>
        {
            res.Text("hello world");
        });
        host.Run();
        //Hit localhost:8000/hello
    }
}
```

Antsy lets [ASP.NET Core](https://www.asp.net/core) do most of the heavy lifting so that it doesn't reinvent the wheel,
but the api surface is inspired by [Express JS](http://expressjs.com/).

The name is a bit of a rib at [Nancy](http://nancyfx.org/). If this actually becomes popular, my sincerest appologies
to the Nancy maintainers. Any name confusion was both entirely avoidable, and my fault.

This a version 1.0 library. Because it leans heavily on aspnet this framework 
should theoretically be relatively robust. Note that the host will not listen for https connections (just stick your favorite reverse proxy in front and do https termination there). All json model binding uses [Json.net](https://www.newtonsoft.com/json).


This project targets [netstandard 2.0](https://docs.microsoft.com/en-us/dotnet/standard/net-standard) so if you're running dotnet core or a modern full framework this library should work for you.


### API

##### Constructor

```csharp
new AntsyHost(int port = 80);
```

##### Methods (AntsyHost)

The path variable should follow asp.net [routing rules](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/routing).

```csharp
//Switch between sync and async. Heaven help you if your sync call blocks.
host.Get(string path, Func<AntsyRequest, AntsyResponse, Task>);
host.Get(string path, Action<AntsyRequest, AntsyResponse>);
host.Post(string path, Func<AntsyRequest, AntsyResponse, Task>);
host.Post(string path, Action<AntsyRequest, AntsyResponse>);
host.Delete(string path, Func<AntsyRequest, AntsyResponse, Task>);
host.Delete(string path, Action<AntsyRequest, AntsyResponse>);
//folderRoot is relative to the current directory.
host.StaticFiles(string path, string folderRoot);
```

##### Request & Response

The ```req``` and ```res``` are the standard ASP.NET 
[HttpRequest](https://docs.microsoft.com/en-us/aspnet/core/api/microsoft.aspnetcore.http.httprequest#Microsoft_AspNetCore_Http_HttpRequest)
and 
[HttpResponse](https://docs.microsoft.com/en-us/aspnet/core/api/microsoft.aspnetcore.http.httpresponse#Microsoft_AspNetCore_Http_HttpResponse)
objects,
but gain helper methods to make them line up more with the Express API.

Helper methods on the request object:
```csharp
//Deserializes the body json to an object (using Json.net formatting).
req.BodyJson<T>();
//Returns the body as text.
req.BodyText();
```

Helper methods on the response object:
```csharp
//Accepts either a POCO or a string. Formats response as json.
res.Json(object obj);
//Formats the response as text.
res.Text(string text);
//Formats the response as html.
res.Html(string html);
//Serves the file as an attachment.
res.File(string filepath);
res.File(string filename, Stream filestream);
//If I can wrangle it, I'd like to see something like res.Razor(string pageName, object model) in the future.
```
These helper methods just make sure the response headers are properly formatted (Content-Type, and friends).

### License

MIT
