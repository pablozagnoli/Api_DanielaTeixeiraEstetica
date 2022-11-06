using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace locacaoDeImoveis
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

      services.AddCors(options =>
      {
        options.AddPolicy("Policy1",
            policy =>
            {
              policy.WithOrigins("https://pablozagnoli.github.io/",
                                 "https://pablozagnoli.github.io",
                                 "http://localhost:4200",
                                 "https://localhost:4200/",
                                 "http://localhost:5000",
                                 "https://localhost:5000/"
                                 );
            });

        options.AddPolicy("AnotherPolicy",
            policy =>
            {
              policy.WithOrigins("https://pablozagnoli.github.io/",
                                 "https://pablozagnoli.github.io",
                                 "http://localhost:4200",
                                 "https://localhost:4200/",
                                 "http://localhost:5000",
                                 "https://localhost:5000/",
                                 "https://pablozagnoli.github.io/")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod();
            });
      });

      services.AddControllers();
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "locacaoDeImoveis", Version = "v1" });
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "locacaoDeImoveis v1"));
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseCors();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
