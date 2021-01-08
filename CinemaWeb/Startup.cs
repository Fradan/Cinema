using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Application;
using Application.Exceptions;
using FluentValidation.AspNetCore;
using FluentValidation;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using CinemaWeb.Validation;
using CinemaWeb.Mapping;

namespace CinemaWeb
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
            services.AddControllers()
                .AddFluentValidation();

            services.AddTransient<ProblemDetailsFactory, CustomProblemDetailsFactory>(provider =>
            {
                var options = provider.GetRequiredService<IOptions<ApiBehaviorOptions>>();
                var problemDetailsFactory = new CustomProblemDetailsFactory(options);
                problemDetailsFactory.Map<BusinessRuleValidationException>((ex, context) =>
                    new ProblemDetails()
                    {
                        Title = ex.Message,
                        Detail = ex.Details,
                        Status = StatusCodes.Status409Conflict,
                    });


                return problemDetailsFactory;
            });

            services.AddSwaggerGen(options =>
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My WEB Api", Version = "v1" }));

            services.AddScoped<ICinemaService, CinemaService>();
            services.AddScoped<ICinemaRepository, CinemaRepository>();
            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<ISessionRepository, SessionRepository>();
            services.AddScoped<IFilmService, FilmService>();
            services.AddScoped<IFilmRepository, FilmRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<SessionProfile>();
                cfg.AddProfile<CinemaProfile>();
                cfg.AddProfile<FilmProfile>();
            });
            services.AddTransient<IValidator<SessionViewModel>, SessionValidator>();

            string connection = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(connection));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseExceptionHandler("/error");
            }
            else
            {
                app.UseExceptionHandler("/error");
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
