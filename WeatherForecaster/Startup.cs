namespace WeatherForecaster
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using WeatherForecaster.Models;
    using WeatherForecaster.Services;

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
        /// Initializes a new instance of the <see cref="Startup"/> class.
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
            services.AddDbContext<WeatherDbContext>(
                options =>
                    options.UseSqlServer(connectionString),
                    ServiceLifetime.Transient);
            services.AddSingleton<WeatherDataSaverService>();
            services.AddSingleton<StandardDeviationUpdater>();
            services.AddTransient<IStandardDeviationService, StandardDeviationService>();
            services.AddTransient<IWeatherService, WeatherService>();
            services.AddMvc();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">Application builder.</param>
        /// <param name="env">Environment variable.</param>
        /// <param name="provider">Provider of services.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            this.RunDeamonProcesses(provider);
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Weather}/{action=Index}/{id?}");
            });
        }

        /// <summary>
        /// Runs background processes.
        /// </summary>
        /// <param name="serviceProvider">Provides of registered services.</param>
        private void RunDeamonProcesses(IServiceProvider serviceProvider)
        {
            StandardDeviationUpdater updater =
                (StandardDeviationUpdater)serviceProvider.GetService(typeof(StandardDeviationUpdater));
            Task.Run(() => updater.UpdateDeviation());
            WeatherDataSaverService saver =
                (WeatherDataSaverService)serviceProvider.GetService(typeof(WeatherDataSaverService));
            Task.Run(async () => await saver.SaveWeatherData());
        }
    }
}
