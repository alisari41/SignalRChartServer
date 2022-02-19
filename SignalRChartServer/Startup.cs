using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignalRChartServer.Models;
using SignalRChartServer.Subscription;
using SignalRChartServer.Subscription.Middleware;

namespace SignalRChartServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
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

            //Middleware 'i �a��r
            app.UseDatabaseSubscription<DatabaseSubscription<Satislar>>("Satislar");
            app.UseDatabaseSubscription<DatabaseSubscription<Personeller>>("Personeller");

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {

            });
        }
    }
}
