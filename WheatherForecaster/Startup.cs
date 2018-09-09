namespace WhetherForecaster
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using WheatherForecaster.Services;
    using WhetherForecaster.Models;
    using WhetherForecaster.Services;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Startup class.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Name of connection string.
        /// </summary>
        private const string ConnectionStringName = "DefaultConnection";

        /// <summary>
        /// Initializes a new instance of <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">Configuration object.</param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// Gets configuration of the system.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        /// </summary>
        /// <param name="services">Services collection. </param>
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = this.Configuration.GetConnectionString(Startup.ConnectionStringName);
            services.AddDbContext<WheatherDbContext>(options =>
                    options.UseSqlServer(connectionString),
                    ServiceLifetime.Transient);
            services.AddSingleton<WheatherSaverService>();
            services.AddSingleton<StandartDeviationUpdater>();
            services.AddTransient<IStandartDeviationService, StandardDeviationService>();
            services.AddTransient<IWeatherService, WheatherService>();
            services.AddMvc();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">Application builder.</param>
        /// <param name="env">Environment variable.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            RunDeamonProcesses(provider);
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void RunDeamonProcesses(IServiceProvider services)
        {
            StandartDeviationUpdater updater =
                (StandartDeviationUpdater)services.GetService(typeof(StandartDeviationUpdater));
            Task.Run(() => updater.UpdateDeviation());
            WheatherSaverService saver =
                (WheatherSaverService)services.GetService(typeof(WheatherSaverService));
            Task.Run(async () => await saver.SaveWheatherData());
        }
    }
}
