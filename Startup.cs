using FreeTime.Core;
using FreeTime.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Net.Http;

namespace FreeTime
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ExchangeOptions>(Configuration.GetSection("Exchange"));
            services.AddControllers();
            services.AddTransient<IScheduleBuilder, ScheduleBuilder>();
            services.AddTransient<ICalendarClientFactory, CalendarClientFactory>();

            services.AddHttpClient("exchange", client =>
            {
                var timeZone = Configuration.GetValue<string>("Exchange:TimeZone");
                client.DefaultRequestHeaders.Add("prefer", $"outlook.timezone=\"{timeZone}\"");
            }).ConfigurePrimaryHttpMessageHandler(() =>
            {
                var config = new ConfigurationBuilder().AddEnvironmentVariables("FREETIME_").Build().GetSection("Credentials");
                var opt = new CredentialsOptions();
                config.Bind(opt);

                var handler = new HttpClientHandler();
                var creds = new NetworkCredential(opt.UserName, opt.Password, opt.Domain);
                handler.Credentials = creds;

                return handler;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
