using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Security.Cryptography;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using System.Net;

namespace Aula01
{
    public class Aspnet
    {
        static void Main(string[] args)
        {
            var Builder = WebApplication.CreateBuilder(args);
            var app = Builder.Build();
            
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints => {
                endpoints.MapGet("/", async context =>{
                    context.Response.Redirect("/index.html");
                });
            });

            app.Run();
        }

    }
}