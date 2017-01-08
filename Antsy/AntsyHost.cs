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

namespace Antsy
{
    /// <summary>
    /// For those who want are eager to get going.
    /// </summary>
    public class AntsyHost
    {
        private List<Tuple<string, Func<AntsyRequest, AntsyResponse, Task>>> getList = new List<Tuple<string, Func<AntsyRequest, AntsyResponse, Task>>>();
        private List<Tuple<string, Func<AntsyRequest, AntsyResponse, Task>>> postList = new List<Tuple<string, Func<AntsyRequest, AntsyResponse, Task>>>();
        private List<Tuple<string, Func<AntsyRequest, AntsyResponse, Task>>> deleteList = new List<Tuple<string, Func<AntsyRequest, AntsyResponse, Task>>>();

        private IWebHostBuilder _builder;

        public AntsyHost(int port)
        {
            _builder = new WebHostBuilder()
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
                return Task.FromResult(0);
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
                return Task.FromResult(0);
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
                return Task.FromResult(0);
            });
        }

        /// <summary>
        /// Handle a DELETE request at the given path.
        /// </summary>
        public void Delete(string path, Func<AntsyRequest, AntsyResponse, Task> del)
        {
            deleteList.Add(Tuple.Create(path, del));
        }

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
            var routeBuilder = new RouteBuilder(app);
            foreach (var item in getList)
            {
                routeBuilder.MapGet(item.Item1, new RequestDelegate(context => 
                {
                    return item.Item2(new AntsyRequest(context.Request), new AntsyResponse(context.Response));
                }));
            }
            foreach (var item in postList)
            {
                routeBuilder.MapPost(item.Item1, new RequestDelegate(context =>
                {
                    return item.Item2(new AntsyRequest(context.Request), new AntsyResponse(context.Response));
                }));
            }
            foreach (var item in deleteList)
            {
                routeBuilder.MapDelete(item.Item1, new RequestDelegate(context =>
                {
                    return item.Item2(new AntsyRequest(context.Request), new AntsyResponse(context.Response));
                }));
            }
            routeBuilder.MapGet("path", new RequestDelegate(context => Task.FromResult(0)));
            app.UseRouter(routeBuilder.Build());
        }
    }

}
