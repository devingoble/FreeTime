using FreeTime.Core.Calendar;
using FreeTime.Core.Schedule;
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
            services.AddTransient<IScheduleBuilderFactory, ScheduleBuilderFactory>();
            services.AddTransient<ICalendarClientFactory, CalendarClientFactory>();

            services.AddHttpClient("exchange", client =>
            {
                var timeZone = Configuration.GetValue<string>("Exchange:TimeZone");
                client.DefaultRequestHeaders.Add("prefer", $"outlook.timezone=\"{timeZone}\"");
            }).ConfigurePrimaryHttpMessageHandler(() =>
            {
                var handler = new HttpClientHandler();
                var creds = new NetworkCredential(Configuration["Credentials:UserName"], Configuration["Credentials:Password"], Configuration["Credentials:Domain"]);
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
