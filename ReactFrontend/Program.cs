using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mqtt;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MQTTClient;
using ReactFrontend.MQTTClient;

namespace ReactFrontend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            RemoteMQTTClient.SetupClient();
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
