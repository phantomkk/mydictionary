using AutoMapper;
using Infrastructure.Configuration;
using Infrastructure.DependencyInjection;
using Infrastructure.MongoDb;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MyDictionary.Service.Mongo.Services;
using MyDictionary.Services.Dtos;
using MyDictionary.Services.Services;
using MyDictionary.Web.Cached;
using MyDictionary.Web.Utils;

namespace MyDictionary.Web
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
            //services.AddDbContext<DictionaryDbContext>(options =>
            //options.UseSqlServer(Configuration.GetConnectionString("defaultcnnString")));

            services.AddSingleton<MongoDbConfig>(Configuration.GetConfiguration<MongoDbConfig>()); 

            services.AddSingleton<IMongoDbContext, MongoDbContext>();
            ConfigDependencyInjection(services);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            });

            services.AddControllersWithViews().AddJsonOptions(options =>
            {
                // Use camel case properties in the serializer and the spec (optional)
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            }).AddRazorRuntimeCompilation(); ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCors("AllowOrigin");
            app.UseHttpsRedirection();
            app.UseStaticFiles();


            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseRouting();

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Example}/{action=Index}/{id?}");
            });
        }
        private void ConfigDependencyInjection(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSingleton<CachedService<ExampleDto>, ExampleCached>();
            services.AddSingleton<CachedService<NewWord>, WordCached>();
            services.AddSingleton<LearningCached>();


            services.AddSingleton<IDependencyResolver, DependencyResolver>();
            services.AddScoped<IWordService, WordService>();
            services.AddScoped<IExampleService, ExampleService>();
            //services.AddScoped<IUserService, UserService>(); 
            services.AddScoped<ILearningService, LearningService>();

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AppProfiles>();
            });
            configuration.AssertConfigurationIsValid();
            services.AddSingleton(configuration.CreateMapper());
            services.AddControllers().AddNewtonsoftJson();

        }
    }
}
