using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.AspNetCore;
using MQTTnet.Protocol;
using MQTTnet.Server;

namespace mqttwebapi
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var hostIp = Configuration["MqttOption:HostIp"];
            var hostPort =int.Parse(Configuration["MqttOption:HostPort"]);
            var timeout =int.Parse(Configuration["MqttOption:TimeOut"]);
            var username = Configuration["MqttOption:UserName"];
            var password = Configuration["MqttOption:Password"];


            var options = new MqttServerOptionsBuilder()
                .WithDefaultEndpointBoundIPAddress(System.Net.IPAddress.Parse(hostIp))
                .WithDefaultEndpointPort(hostPort)
                .WithDefaultCommunicationTimeout(TimeSpan.FromMilliseconds(timeout))
                .WithConnectionValidator(t =>
                {
                    if (t.Username != username || t.Password != password)
                    {
                        t.ReturnCode = MqttConnectReturnCode.ConnectionRefusedBadUsernameOrPassword;
                    }
                    t.ReturnCode = MqttConnectReturnCode.ConnectionAccepted;
                })
                .Build();

            //services.AddHostedMqttServer(options)
            //    .AddMqttConnectionHandler()
            //    .AddConnections();



            //this adds a hosted mqtt server to the services
            services.AddHostedMqttServer(options);

            //this adds tcp server support based on Microsoft.AspNetCore.Connections.Abstractions
            services.AddMqttConnectionHandler();

            //this adds websocket support
            services.AddMqttWebSocketServerAdapter();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMqttEndpoint();
            
            app.UseMvc();
        }
    }
}
