using MQTTnet;
using MQTTnet.Protocol;
using MQTTnet.Server;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MqttServerCore1
{
    internal class Program
    {
        private static MqttServer mqttServer;

        private static async Task Main(string[] args)
        {
            //MqttNetTrace.TraceMessagePublished += MqttNetTrace_TraceMessagePublished;
            Thread thread = new Thread(StartMqttServer);
            thread.Start();


            while (true)
            {
                Console.WriteLine("按q退出：");
                string msg = Console.ReadLine();
                if (msg.ToLower() == "q")
                {
                    break;
                }
            }
        }

        //private static void MqttNetTrace_TraceMessagePublished(object sender, MqttNetTraceMessagePublishedEventArgs e)
        //{
        //    //Console.WriteLine($">> 线程ID：{e.ThreadId} 来源：{e.Source} 跟踪级别：{e.Level} 消息: {e.Message}");
        //    //if (e.Exception != null)
        //    //{
        //    //    Console.WriteLine(e.Exception);
        //    //}
        //}

        //private static void MqttServer_ApplicationMessageReceived(object sender, MqttApplicationMessageReceivedEventArgs e)
        //{
        //    Console.WriteLine($"receive msg clientid:{e.ClientId}, body:{System.Text.Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}, topic: {e.ApplicationMessage.Topic},level:{e.ApplicationMessage.QualityOfServiceLevel}");
        //}

        //private static void MqttServer_ClientDisconnected(object sender, MQTTnet.Core.Adapter.MqttClientDisconnectedEventArgs e)
        //{
        //    Console.WriteLine($"client 断开连接 clientid :{e.Client.ClientId},协议版本：{e.Client.ProtocolVersion}");
        //}

        //private static void MqttServer_ClientConnected(object sender, MQTTnet.Core.Adapter.MqttClientConnectedEventArgs e)
        //{
        //    Console.WriteLine($"client 建立连接 clientid :{e.Client.ClientId},协议版本：{e.Client.ProtocolVersion}");
        //}

        private static void StartMqttServer()
        {
            if (null == mqttServer)
            {
                try
                {

                    //mqttServer = new MqttFactory().CreateMqttServer(new MqttServerOptions()
                    //{
                    //    ConnectionValidator = options =>
                    //    {
                    //        if (options.ClientId == "c001")
                    //        {
                    //            if (options.Username != "u001" || options.Password != "p001")
                    //            {
                    //                return MQTTnet.Core.Protocol.MqttConnectReturnCode
                    //                    .ConnectionRefusedBadUsernameOrPassword;
                    //            }
                    //        }

                    //        return MQTTnet.Core.Protocol.MqttConnectReturnCode.ConnectionAccepted;
                    //    }
                    //}) as MqttServer;

                    mqttServer = new MqttFactory().CreateMqttServer() as MqttServer; ;


                    //mqttServer.ClientConnected += MqttServer_ClientConnected;
                    //mqttServer.ClientDisconnected += MqttServer_ClientDisconnected;
                    //mqttServer.ApplicationMessageReceived += MqttServer_ApplicationMessageReceived;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return;
                }
            }

            var options = new MqttServerOptionsBuilder()
                .WithStorage(new RetainedMessageHandler())
                .WithApplicationMessageInterceptor(context =>
                {
                    if (context.ApplicationMessage.Topic == "my/custom/topic")
                    {
                        context.ApplicationMessage.Payload = System.Text.Encoding.UTF8.GetBytes("the server rejected message");
                    }
                })
                .WithConnectionValidator(new MqttServerConnectionValidatorDelegate(c =>
                {
                    //if (c.ClientId.Length < 10)
                    //{
                    //    c.ReturnCode = MqttConnectReturnCode.ConnectionRefusedIdentifierRejected;
                    //    return;
                    //}

                    if (c.Username != "u001")
                    {
                        c.ReturnCode = MqttConnectReturnCode.ConnectionRefusedBadUsernameOrPassword;
                        return;
                    }

                    if (c.Password != "p001")
                    {
                        c.ReturnCode = MqttConnectReturnCode.ConnectionRefusedBadUsernameOrPassword;
                        return;
                    }

                    c.ReturnCode = MqttConnectReturnCode.ConnectionAccepted;
                }))
                .Build();
            mqttServer.StartAsync(options);
            //mqttServer.StartAsync(new MqttServerOptions()
            //{
            //    Storage = new RetainedMessageHandler(),
            //    ConnectionValidator = new MqttServerConnectionValidatorDelegate(c =>
            //     {
            //         if (c.ClientId.Length < 10)
            //         {
            //             c.ReturnCode = MqttConnectReturnCode.ConnectionRefusedIdentifierRejected;
            //             return;
            //         }

            //         if (c.Username != "u001")
            //         {
            //             c.ReturnCode = MqttConnectReturnCode.ConnectionRefusedBadUsernameOrPassword;
            //             return;
            //         }

            //         if (c.Password != "p001")
            //         {
            //             c.ReturnCode = MqttConnectReturnCode.ConnectionRefusedBadUsernameOrPassword;
            //             return;
            //         }

            //         c.ReturnCode = MqttConnectReturnCode.ConnectionAccepted;
            //     })
            //});
            Console.WriteLine("mqttserver服务启动成功！");
        }
    }
}
