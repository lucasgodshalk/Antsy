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

        }

        /// <summary>
        /// Handle a POST request at the given path.
        /// </summary>
        public void Post(string path, Action<AntsyRequest, AntsyResponse> del)
        {

        }

        /// <summary>
        /// Handle a DELETE request at the given path.
        /// </summary>
        public void Delete(string path, Action<AntsyRequest, AntsyResponse> del)
        {

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
            routeBuilder.MapGet("path", new RequestDelegate(context => Task.FromResult(0)));
            app.UseRouter(routeBuilder.Build());
        }
    }

}
