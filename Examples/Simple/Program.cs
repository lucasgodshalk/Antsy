using System;
using Antsy;
using System.Threading.Tasks;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        AntsyHost server = new AntsyHost(8000);
        server.Get("/hello", (req, res) => 
        {
            res.Text("Hello World!");
        });
        server.Get("", (req, res) =>
        {
            res.Html(File.ReadAllText("Sample.html"));
        });
        server.StaticFiles("/files", "");
        server.StaticFiles("/bogus", "");
        server.Run();
    }
}