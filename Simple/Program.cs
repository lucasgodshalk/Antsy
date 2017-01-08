using System;
using Antsy;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        AntsyHost server = new AntsyHost(8000);
        server.Get("hello", (req, res) => 
        {
            res.StatusCode = 404;
        });
        server.Run();
    }
}