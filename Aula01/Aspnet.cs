using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Security.Cryptography;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace Aula01
{
    public class Aspnet
    {
        static void Main(string[] args)
        {
            
        }

        public static HostBuilder CreateHostBuilder(string[] args) =>
        { // Define o método que é responsável por criar o host da aplicação 
            Host.CreateDefaultBuilder(args) // Cria o shost com configuração padrão do ambiente
            .ConfigureWebHostDefault(web);
            

        }
    }
}