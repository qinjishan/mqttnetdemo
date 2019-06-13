using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{



    public partial class Form1 : Form
    {
        private MqttClient mqttClient;
        public Form1()
        {
            InitializeComponent();
            Task.Run(async () => await ConnectMqttServer());
        }

        private async Task ConnectMqttServer()
        {
            string clientid = Guid.NewGuid().ToString().Substring(0, 5);
            // Create TCP based options using the builder.
            IMqttClientOptions options = new MqttClientOptionsBuilder()
                .WithClientId(clientid)
                .WithTcpServer("localhost", 61613)
                .WithCredentials("u001", "p001")
                //.WithTls()
                .WithCleanSession()
                .Build();

            if (null == mqttClient)
            {
                mqttClient = new MqttFactory().CreateMqttClient() as MqttClient;

                mqttClient.UseDisconnectedHandler(async e =>
                {
                    await Task.Delay(5000);
                    await mqttClient.ConnectAsync(options);
                });
                mqttClient.UseConnectedHandler(async e =>
                {
                    Invoke(new Action(() =>
                    {
                        txtReceiveMsg.AppendText("建立连接" + Environment.NewLine);
                    }));
                });
                mqttClient.UseDisconnectedHandler(async e =>
                {
                    Invoke(new Action(() =>
                    {
                        txtReceiveMsg.AppendText("断开连接" + Environment.NewLine);
                    }));
                });

                mqttClient.UseApplicationMessageReceivedHandler(async e =>
                {

                    Invoke(new Action(() =>
                    {
                        txtReceiveMsg.AppendText($"接收到消息：{System.Text.Encoding.UTF8.GetString(e.ApplicationMessage.Payload)},topic :{e.ApplicationMessage.Topic}" + Environment.NewLine);
                    }));

                    //Console.WriteLine("### RECEIVED APPLICATION MESSAGE ###");
                    //Console.WriteLine($"+ Topic = {e.ApplicationMessage.Topic}");
                    //Console.WriteLine($"+ Payload = {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}");
                    //Console.WriteLine($"+ QoS = {e.ApplicationMessage.QualityOfServiceLevel}");
                    //Console.WriteLine($"+ Retain = {e.ApplicationMessage.Retain}");

                });

                //mqttClient.Connected += MqttClient_Connected;
                //mqttClient.Disconnected += MqttClient_Disconnected;
                //mqttClient.ApplicationMessageReceived += MqttClient_ApplicationMessageReceived;
            }

            try
            {
                //var options = new MqttClientTcpOptions()
                //{
                //    ClientId =Guid.NewGuid().ToString().Substring(0,5),
                //    UserName="u001",
                //    Password="p001",
                //    Server="127.0.0.1",
                //    CleanSession=true,
                //    TlsOptions=new MqttClientTlsOptions()
                //    {
                //         UseTls=false
                //    }
                //};

                await mqttClient.ConnectAsync(options);
            }
            catch (Exception exception)
            {
                Invoke(new Action(() =>
                {
                    txtReceiveMsg.AppendText($"建立连接失败!" + Environment.NewLine + exception.Message + Environment.NewLine);
                }));
            }
        }

        private async void MqttClient_ApplicationMessageReceived(object sender, MqttApplicationMessageReceivedEventArgs e)
        {
            Invoke(new Action(() =>
            {
                txtReceiveMsg.AppendText($"接收到消息：{System.Text.Encoding.UTF8.GetString(e.ApplicationMessage.Payload)},topic :{e.ApplicationMessage.Topic}" + Environment.NewLine);
            }));
        }

        private async void MqttClient_Disconnected(object sender, EventArgs e)
        {
            Invoke(new Action(() =>
            {
                txtReceiveMsg.AppendText("断开连接" + Environment.NewLine);
            }));
        }

        private async void MqttClient_Connected(object sender, EventArgs e)
        {
            Invoke(new Action(() =>
            {
                txtReceiveMsg.AppendText("建立连接" + Environment.NewLine);
            }));
        }

        private void btnSubscribe_Click(object sender, EventArgs e)
        {
            btnSubscribe.Enabled = false;

            string topic = txtSubcribeTopic.Text.Trim();
            if (string.IsNullOrWhiteSpace(topic))
            {
                MessageBox.Show("订阅主题不能为空");
                return;
            }

            if (!mqttClient.IsConnected)
            {
                MessageBox.Show("当前客户端未连接");
                return;
            }
            //mqttClient.SubscribeAsync(new TopicFilter(topic,qualityOfServiceLevel:MQTTnet.Core.Protocol.MqttQualityOfServiceLevel.AtMostOnce));

            mqttClient.SubscribeAsync(new TopicFilterBuilder().WithTopic(topic).WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtMostOnce).Build());


            Invoke(new Action(() =>
            {

                txtReceiveMsg.AppendText($"订阅主题：{topic}" + Environment.NewLine);

            }));
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            btnSend.Enabled = false;
            btnSubscribe.Enabled = false;

            string topic = txtPublishTopic.Text.Trim();
            string txtmsg = txtSendMsg.Text.Trim();
            if (string.IsNullOrWhiteSpace(txtmsg))
            {
                MessageBox.Show("发布内容不能为空");
                return;
            }
            //var msg = new MqttApplicationMessage(topic,Encoding.UTF8.GetBytes(txtmsg),MQTTnet.Core.Protocol.MqttQualityOfServiceLevel.AtMostOnce,false);

            MqttApplicationMessage msg = new MqttApplicationMessageBuilder().WithTopic(topic).WithPayload(txtmsg)
                .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtMostOnce).WithRetainFlag(false).Build();

            await mqttClient.PublishAsync(msg);

        }

        private async void btnmeeting_Click(object sender, EventArgs e)
        {
            string channelId = txtChannel.Text.Trim();
            var currentUserId = txtCurrentUserId.Text.Trim();
            var targetUserId = txtTargetUserId.Text.Trim();
            string msgContent = JsonConvert.SerializeObject(new MeetingMsgModel
            {
                ChannelId = channelId,
                UserIds = new Guid[] { Guid.Parse(currentUserId), Guid.Parse(targetUserId) }
            });
            MqttApplicationMessage msg = new MqttApplicationMessageBuilder().WithTopic($"topic/createmeeting").WithPayload(msgContent)
                .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtMostOnce).WithRetainFlag(false).Build();
            await mqttClient.PublishAsync(msg);
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
