using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using System.Net;
using Microsoft.AspNetCore.Connections;
using ServerImplementation;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Server2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Connection();
        }

        static void Connection()
        {
            IConfiguration configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

            var webHostOptions = configuration.GetSection("WebHostOptions").Get<WebHostOptions>();

            var host = WebHost.CreateDefaultBuilder(Array.Empty<string>())
            .UseKestrel(options =>
            {
                options.AllowSynchronousIO = true;
                options.Listen(IPAddress.Parse(webHostOptions.ListenIPAddress), webHostOptions.ListenPort);
            }).UseStartup<Startup>()
            .Build();
            host.Run();

        }
    }

    public class WebHostOptions
    {
        public string ListenIPAddress { get; set; }
        public int ListenPort { get; set; }
    }
}
