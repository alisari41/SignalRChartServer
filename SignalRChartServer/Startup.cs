using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignalRChartServer.Hubs;
using SignalRChartServer.Models;
using SignalRChartServer.Subscription;
using SignalRChartServer.Subscription.Middleware;

namespace SignalRChartServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //Taray�c�da �al��mas� i�in Cors k�t�phanesini ekle
            services.AddCors(options => options.AddDefaultPolicy(policy =>
                policy.AllowCredentials()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .SetIsOriginAllowed(x => true)));
            //SignalR k�t�phanesini dahil ediyoruz
            services.AddSignalR();
            //Service Broker
            services.AddSingleton<DatabaseSubscription<Satislar>>();//Uygulama bazl� tekil nesne olu�turmam�z� sa�lar.Uygulama a��k kalana kadar
            services.AddSingleton<DatabaseSubscription<Personeller>>();//Uygulama bazl�  tekil nesne olu�turmam�z� sa�lar.Uygulama a��k kalana kadar
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Cros
            app.UseCors();
            //Middleware 'i �a��r
            app.UseDatabaseSubscription<DatabaseSubscription<Satislar>>("Satislar");
            app.UseDatabaseSubscription<DatabaseSubscription<Personeller>>("Personeller");


            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                //SignalR entpiont olu�turuldu
                endpoints.MapHub<SatisHub>("/satishub");
            });
        }
    }
}
