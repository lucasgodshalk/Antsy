# Antsy Web Framework

This is a web framework for people who just want to get going. It sacrifices most of the tools that
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
but it's real heritage is [Express JS](http://expressjs.com/).
 
The name is a bit of a rib at [Nancy](http://nancyfx.org/). If this actually becomes popular, my sincerest appologies
to the Nancy maintainers. This was both entirely avoidable, and my fault.

MIT License
