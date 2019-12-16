using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NServiceBus;
using NServiceBus.Persistence.Sql;
using ObservableSaga.Hubs;
using ObservableSaga.NServiceBusConfig;

namespace ObservableSaga
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddSignalR();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var endpointConfig = new EndpointConfiguration("TestObservableSaga");

            
            var persistence = endpointConfig.UsePersistence<InMemoryPersistence>();
            
            //var settings = persistence.SagaSettings();
            endpointConfig.UseSerialization<NewtonsoftSerializer>();

            var transport = endpointConfig.UseTransport<LearningTransport>();
            var routing = transport.Routing();
            routing.RouteToEndpoint(
                assembly: typeof(Startup).Assembly,
                destination: "ObservableSaga.Endpoint");

            var startableEndpoint = EndpointWithExternallyManagedContainer.Create(endpointConfig, new ServiceCollectionAdapter(services));

            services.AddSingleton(_ => startableEndpoint.MessageSession.Value);
            services.AddSingleton<IHostedService>(serviceProvider => new NServiceBusService(startableEndpoint, serviceProvider));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSignalR(config =>
            {
                config.MapHub<CounterHub>("/counter");
            });
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
