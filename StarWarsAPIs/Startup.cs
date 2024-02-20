using APIInterfaces;
using APIServices;
using Microsoft.OpenApi.Models;

namespace StarWarsAPIs
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration _configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader());
            });

            string baseuri = _configuration.GetValue<string>("StarWarsBaseUrl:baseUri");

            services.AddHttpClient("starWarsClient", httpClient =>
            {
                httpClient.BaseAddress = new Uri(baseuri);
                httpClient.Timeout = new TimeSpan(0, 0, 30);
                httpClient.DefaultRequestHeaders.Clear();
            });

            services.AddTransient(typeof(IStarWarsAPIService<>), typeof(StarWarsAPIService<>));

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Proxy Api"));
            }
            else
            {
                app.UseHsts();
            }

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseCors("AllowAll");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

