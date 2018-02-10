using System;
using System.Threading.Tasks;
using System.IO;
using Antsy;

class Program
{
    static void Main(string[] args)
    {
        AntsyHost server = new AntsyHost(8000);
        server.Get("/hello", (req, res) => 
        {
            res.Text("Hello World!");
        });
        server.Get("/hello/json", (req, res) =>
        {
            res.Json(new
            {
                id = 5,
                name = "Sue",
                position = "engineer"
            });
        });
        server.Get("/html", (req, res) => 
        {
            res.Html("Sample.html");
        });
        server.Get("/somefile", (req, res) =>
        {
            res.Download("image.jpg");
        });
        //Serves all files in the application directory.
        //Navigate to /files/sample.html.
        server.StaticFiles("/files", ""); 
        server.Run();
    }
}