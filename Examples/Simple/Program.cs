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
        server.Get("/hello/json", (req, res) =>
        {
            res.Json(new
            {
                id = 5,
                name = "Sue",
                position = "engineer"
            });
        });
        server.Get("", (req, res) =>
        {
            res.File("image.jpg");
        });
        server.StaticFiles("/files", ""); //Serves all files in the application directory.
        server.Run();
    }
}