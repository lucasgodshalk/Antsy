using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using Microsoft.Extensions.FileProviders;

namespace Antsy
{
    /// <summary>
    /// For those who want are eager to get going.
    /// </summary>
    public class AntsyHost
    {
        private readonly List<Tuple<string, Func<AntsyRequest, AntsyResponse, Task>>> getList = 
            new List<Tuple<string, Func<AntsyRequest, AntsyResponse, Task>>>();

        private readonly List<Tuple<string, Func<AntsyRequest, AntsyResponse, Task>>> postList = 
            new List<Tuple<string, Func<AntsyRequest, AntsyResponse, Task>>>();

        private readonly List<Tuple<string, Func<AntsyRequest, AntsyResponse, Task>>> deleteList = 
            new List<Tuple<string, Func<AntsyRequest, AntsyResponse, Task>>>();

        private readonly List<Tuple<string, string>> staticFileList = 
            new List<Tuple<string, string>>();

        private readonly IWebHostBuilder _builder;

        public AntsyHost(int port)
        {
            _builder = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseKestrel()
                .UseUrls($"http://*:{port}")
                .ConfigureServices(ConfigureServices)
                .Configure(Configure);
        }

        /// <summary>
        /// Handle a GET request at the given path.
        /// </summary>
        public void Get(string path, Action<AntsyRequest, AntsyResponse> del)
        {
            Get(path, (req, res) =>
            {
                del(req, res);
                return Task.CompletedTask;
            });
        }

        /// <summary>
        /// Handle a GET request at the given path.
        /// </summary>
        public void Get(string path, Func<AntsyRequest, AntsyResponse, Task> del)
        {
            getList.Add(Tuple.Create(path, del));
        }

        /// <summary>
        /// Handle a POST request at the given path.
        /// </summary>
        public void Post(string path, Action<AntsyRequest, AntsyResponse> del)
        {
            Post(path, (req, res) =>
            {
                del(req, res);
                return Task.CompletedTask;
            });
        }

        /// <summary>
        /// Handle a POST request at the given path.
        /// </summary>
        public void Post(string path, Func<AntsyRequest, AntsyResponse, Task> del)
        {
            postList.Add(Tuple.Create(path, del));
        }

        /// <summary>
        /// Handle a DELETE request at the given path.
        /// </summary>
        public void Delete(string path, Action<AntsyRequest, AntsyResponse> del)
        {
            Delete(path, (req, res) =>
            {
                del(req, res);
                return Task.CompletedTask;
            });
        }

        /// <summary>
        /// Handle a DELETE request at the given path.
        /// </summary>
        public void Delete(string path, Func<AntsyRequest, AntsyResponse, Task> del)
        {
            deleteList.Add(Tuple.Create(path, del));
        }

        /// <summary>
        /// Serve files that are located in the root folder that align with the path.
        /// </summary>
        public void StaticFiles(string pathRoot, string folderRoot)
        {
            staticFileList.Add(Tuple.Create(pathRoot, folderRoot));
        }

        /// <summary>
        /// Starts the server and blocks the current thread.
        /// </summary>
        public void Run()
        {
            var host = _builder.Build();
            host.Run();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        private void Configure(IApplicationBuilder app)
        {
            foreach (var item in staticFileList)
            {
                app.UseStaticFiles(new StaticFileOptions()
                {
                    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), item.Item2)),
                    RequestPath = new PathString(item.Item1)
                });
            }

            var routeBuilder = new RouteBuilder(app);
            LoadRoutes(routeBuilder.MapGet, getList);
            LoadRoutes(routeBuilder.MapPost, postList);
            LoadRoutes(routeBuilder.MapDelete, deleteList);
            app.UseRouter(routeBuilder.Build());
        }

        private void LoadRoutes(Func<string, RequestDelegate, IRouteBuilder> map, List<Tuple<string, Func<AntsyRequest, AntsyResponse, Task>>> list)
        {
            foreach (var item in list)
            {
                var path = item.Item1;
                if (path.StartsWith("/"))
                {
                    path = path.Remove(0, 1);
                }
                map(path, new RequestDelegate(context =>
                {
                    return item.Item2(new AntsyRequest(context.Request), new AntsyResponse(context.Response));
                }));
            }
        }
    }

}
