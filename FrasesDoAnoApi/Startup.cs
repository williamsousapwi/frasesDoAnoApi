using Microsoft.OpenApi.Models;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using FrasesDoAnoApi.Dominio;
using Microsoft.EntityFrameworkCore;
using FrasesDoAnoApi.Dados.Configuracao;

namespace FrasesDoAnoApi
{
    /// Starting configuration
    [ApiExplorerSettings(IgnoreApi = true)]
    public class Startup
    {
        /// <summary>
        /// Configuração de ínicio da aplicação.
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Método de configuração.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<IISOptions>(o =>
            {
                o.ForwardClientCertificate = false;
            });

            services.AddControllers();

#if DEBUG
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(name: "v1", new OpenApiInfo { Title = "FrasesDoAnoApi", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var filePath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(filePath);

            });
#endif
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped(c =>
                new DbContextSql(Configuration.GetConnectionString("DefaultConnection")?? ""));
            services.AddScoped<FrasesDoAnoDominio>();
        }


        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.Use((context, next) =>
            {
                context.Request.EnableBuffering();
                return next();
            });

#if DEBUG
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "VixenPosApi");
            });
#endif

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
