using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.WebSockets;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace FirstResponseMP.Statistics
{
    public class Websocket
    {
        public static async void Start()
        {
            var builder = WebApplication.CreateBuilder();

            builder.Services.AddAuthentication();
            builder.Services.AddAuthorization();
            builder.Services.AddControllers();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CustomPolicy", policy =>
                {
                    policy.WithOrigins("*")
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            app.UseCertificateForwarding();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.UseWebSockets();

            if (!Program.DEV_MODE)
            {
                app.UseHsts();
                app.Use(async (context, next) =>
                {
                    context.Request.Scheme = "https";
                    await next();
                });
            }

            app.UseCors("CustomPolicy");

            Console.WriteLine($"[Statistics] Running at {(Program.DEV_MODE ? "http://localhost:54269/" : "https://frmpstats.gtapolicemods.com/")}");

            await app.RunAsync(Program.DEV_MODE ? "http://localhost:54269/" : "http://*:54269/");
        }
    }
}
