using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Antsy
{
    /// <summary>
    /// For those who want to get started.
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
