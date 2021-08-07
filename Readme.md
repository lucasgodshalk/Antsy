﻿# Antsy Web Framework - Updated

This is a web framework for people who just want to get going with as few lines as possible. Ideal for quickly making small applications.

Here is a simple example:

```csharp
using Antsy;

class Program
{
    static void Main(string[] args)
    {
        var host = new AntsyHost(port: 8000);
        host.Get("/hello", (req, res) =>
        {
            res.SendText("hello world");
        });
        host.Run();
        //Hit localhost:8000/hello
    }
}
```

Antsy lets [ASP.NET Core](https://www.asp.net/core) do most of the heavy lifting so that it doesn't reinvent the wheel,
but the api surface is inspired by [Express JS](http://expressjs.com/).

The name is uncomfortably close to [Nancy](http://nancyfx.org/). If this actually becomes popular, my sincerest appologies
to the Nancy maintainers. Any confusion was both entirely avoidable, and my fault.

Note that the host will not listen for https connections (just stick your favorite reverse proxy in front and do https termination there). All json model binding uses [Json.net](https://www.newtonsoft.com/json).

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
req.ReadJson<T>();
//Returns the body as text.
req.ReadText();
```

Helper methods on the response object:
```csharp
//Accepts either a POCO or a string. Formats response as json.
res.SendJson(object obj);
//Formats the response as text.
res.SendText(string text);
//Formats the response as html (string or file).
res.SendHtml(string html);
//Serves the file as a download.
res.Download(string filepath);
res.Download(string filename, Stream filestream);
//If I can wrangle it, I'd like to see something like res.Razor(string pageName, object model) in the future.
```
These helper methods just make sure the response headers are properly formatted (Content-Type, and friends).

### License

MIT
