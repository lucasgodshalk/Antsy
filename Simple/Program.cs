using System;
using Antsy;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        AntsyHost server = new AntsyHost(8000);
        // /hello
        //server.GetJson("hello", (req, res) => 
        //{
        //    //A regular object gets serialized for you.
        //    return new
        //    {
        //        title = "boss",
        //        name = "jenny"
        //    };
        //});

        //// /goodbye
        //server.GetJson("goodbye", (req, res) =>
        //{
        //    //Or send your own json string.
        //    return "{title: \"boss\", name: \"jenny\"}";
        //});
        server.Run();
    }
}