using Microsoft.Extensions.Hosting;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace mqttclientwebapi
{
    public class MeetingMsgModel
    {
        public Guid[] UserIds { get; set; }
        public string ChannelId { get; set; }
    }

    public class ChannelTokenModel
    {
        public string Token { get; set; }
        public string ChannelId { get; set; }
    }


    public class MqttClientBackService : BackgroundService
    {
        private ILogger<MqttClientBackService> _logger;
        public MqttClientBackService(ILogger<MqttClientBackService> logger)
        {
            _logger = logger;
        }
        private const string ClientId = "group_id@@@0001";
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {



            IMqttClientOptions options = new MqttClientOptionsBuilder()
                .WithClientId(ClientId)
                .WithTcpServer("localhost", 61613)
                .WithCredentials("u001", "p001")
                //.WithTls()
                .WithCleanSession()
                .Build();

            MqttClient mqttclient = new MqttFactory().CreateMqttClient() as MqttClient;
            if (null == mqttclient)
            {
                _logger.LogError("mqttclient创建失败！");
                throw new Exception("mqttclient为空");
            }

            //consuming messaging
            mqttclient.UseApplicationMessageReceivedHandler(async e =>
            {
                var receiveMsg =JsonConvert.DeserializeObject< MeetingMsgModel>(Encoding.UTF8.GetString(e.ApplicationMessage.Payload));

                var receiveTopic = e.ApplicationMessage.Topic;

                //foreach (var item in receiveMsg.UserIds)
                //{
                //    var sendTopic = $"topic/createmeeting/{item.ToString().ToLower()}";
                //    var sendMsg = JsonConvert.SerializeObject(new ChannelTokenModel
                //    {
                //        Token = "12345",
                //        ChannelId = receiveMsg.ChannelId
                //    });

                //    var msg = new MqttApplicationMessageBuilder().WithTopic(sendTopic).WithPayload(sendMsg)
                //        .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce).WithRetainFlag(false).Build();


                //    mqttclient.PublishAsync(msg);
                //}
                var msgs= receiveMsg.UserIds.Select(u =>
                {

                    var sendMsg = JsonConvert.SerializeObject(new ChannelTokenModel
                    {
                        Token = "12345",
                        ChannelId = receiveMsg.ChannelId
                    });

                    var msg = new MqttApplicationMessageBuilder()
                        .WithTopic($"topic/createmeeting/{u.ToString().ToLower()}").WithPayload(sendMsg)
                        .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce)
                        .WithRetainFlag(false).Build();
                    return msg;
                });


                mqttclient.PublishAsync(msgs);


                //SendMsg(mqttclient, Guid.Parse("7ba0d58e-ef06-4a86-8781-51f92e4287d6"), receiveMsg.ChannelId);
                //SendMsg(mqttclient, Guid.Parse("deda94d9-6d3a-4b11-82d4-379396efc22c"), receiveMsg.ChannelId);


            });

            //connected and subscribe
            mqttclient.UseConnectedHandler(async e =>
            {
                Console.WriteLine($"已经连接上：{e.AuthenticateResult.ResultCode}");
                // Subscribe to a topic
                await mqttclient.SubscribeAsync(new TopicFilterBuilder().WithTopic("topic/createmeeting").Build());
            });


            //disconnected and reconnecting
            mqttclient.UseDisconnectedHandler(async e =>
            {
                _logger.LogError($"客户端断开连接：{e.ClientWasConnected}");
                await Task.Delay(3000);
                try
                {
                    await mqttclient.ConnectAsync(options);
                }
                catch (Exception exception)
                {
                    _logger.LogError($"客户端重新连接失败:{exception.Message}");
                }
            });

            try
            {
                await mqttclient.ConnectAsync(options);
            }
            catch (Exception e)
            {
                Console.WriteLine($"连接失败：{e.Message}");
            }
            
        }

        private async Task SendMsg(MqttClient mqttclient,Guid userid,string channelId)
        {
            var sendTopic = $"topic/createmeeting/{userid.ToString().ToLower()}";
            var sendMsg = JsonConvert.SerializeObject(new ChannelTokenModel
            {
                Token = "12345",
                ChannelId = channelId
            });

            var msg = new MqttApplicationMessageBuilder().WithTopic(sendTopic).WithPayload(sendMsg).WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce).WithRetainFlag(false).Build();


            mqttclient.PublishAsync(msg);
        }
    }
}
