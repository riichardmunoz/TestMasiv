using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyCaching.Core.Configurations;
using TestCCMasiv.Repositories.Roulette;
using TestCCMasiv.Services;
using TestCCMasiv.Repositories.Bet;
using Microsoft.EntityFrameworkCore;
using TestCCMasiv.Data;

namespace TestCCMasiv
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
            services.AddControllers();
            services.AddEasyCaching(options =>
            {
                options.UseRedis(redisConfig =>
                {
                    redisConfig.DBConfig.Endpoints.Add(new ServerEndPoint("localhost", 6379));
                    redisConfig.DBConfig.AllowAdmin = true;
                },
                    "roulette");
            });
            services.AddScoped<IRouletteRepository, RouletteRepository>();
            services.AddScoped<IRouletteService, RouletteService>();
            services.AddScoped<IBetRepository, BetRepository>();
            services.AddScoped<IBetService, BetService>();

            // Prueba 1 paso a develop
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
