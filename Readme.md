# Antsy Web Framework

This is a .NET web framework for people who just want to get going. It sacrifices most of the tools that
heavier-duty frameworks include all on the altar of keeping code to a minimum. 

Here's the obligatory Hello World:
```csharp
var app = new AntsyHost(port: 80);
app.Get("/hello", (req, res) => 
{
	res.Text("Hello World!");
});
app.Run();
```

Antsy wraps [ASP.NET Core](https://www.asp.net/core) so that it doesn't reinvent the wheel,
but its real heritage is [Express JS](http://expressjs.com/).

The name is a bit of a rib at [Nancy](http://nancyfx.org/). If this actually becomes popular, my sincerest appologies
to the Nancy maintainers. Any name confusion was both entirely avoidable, and my fault.

Please for all that is holy, do not use this in production. This should be considered a version 1.0 library.
Admittedly, because it leans heavily on aspnet as the core this framework should be relatively robust.

### API

##### Constructor

```csharp
new AntsyHost(int port = 80);
```

##### Methods (AntsyHost)

The path variable should follow asp.net [routing rules](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/routing).

```csharp
//Async or non-async, your choice.
host.Get(string path, Func<AntsyRequest, AntsyResponse, Task>);
host.Get(string path, Action<AntsyRequest, AntsyResponse>);
host.Post(string path, Func<AntsyRequest, AntsyResponse, Task>);
host.Post(string path, Action<AntsyRequest, AntsyResponse>);
host.Delete(string path, Func<AntsyRequest, AntsyResponse, Task>);
host.Delete(string path, Action<AntsyRequest, AntsyResponse>);
//folderRoot is relative to the application directory.
host.StaticFiles(string path, string folderRoot);
```

##### Request & Response

The ```req``` and ```res``` are pretty much the standard ASP.NET 
[HttpRequest](https://docs.microsoft.com/en-us/aspnet/core/api/microsoft.aspnetcore.http.httprequest#Microsoft_AspNetCore_Http_HttpRequest)
and 
[HttpResponse](https://docs.microsoft.com/en-us/aspnet/core/api/microsoft.aspnetcore.http.httpresponse#Microsoft_AspNetCore_Http_HttpResponse)
objects,
but gain helper methods to make them line up more with the Express JS API.

The helper methods on the response object are:
```csharp
//Accepts either a POCO or a string.
res.Json(object obj);
res.Text(string text);
res.Html(string html);
```
These helper methods just make sure the response is properly formatted (Content-Type, and friends).

### License

MIT
