using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FreeWheel.MovieDb.Api.Contexts;
using FreeWheel.MovieDb.Api.Services;

namespace FreeWheel.MovieDb.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IConfigurationRoot ConfigurationRoot { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;

            var builder = new ConfigurationBuilder()
                       .SetBasePath(environment.ContentRootPath)
                       .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            ConfigurationRoot = builder.Build();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                    .AddJsonOptions(options => 
                            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore); //fix truncate issue with .net core

            //configs
            services.Configure<RatingsConfig>(Configuration.GetSection("RatingsConfig"));
            services.Configure<RatingsConfig>(Configuration);

            services.AddMvcCore().AddDataAnnotations();

            //services
            services.AddTransient<IMoviesService, MoviesService>();

            //db contexts
            services.AddDbContext<MoviesContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
