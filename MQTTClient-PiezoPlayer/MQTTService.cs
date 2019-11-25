using System;
using System.Collections.Generic;
using System.Net.Mqtt;
using System.Text;
using System.Threading.Tasks;

namespace MQTTClient
{
    public class MQTTService
    {
        //Privates
        private static IMqttClient client;
        private string _basepath { get; set; }
        private string _username { get; set; }
        private string _password { get; set; }
        private string _clientName { get; set; }

        //Publics
        #region publics
        public string BasePath
        {
            get
            {
                return _basepath;
            }
        }

        public string Username
        {
            get
            {
                return _username;
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
        }

        public string ClientName
        {
            get
            {
                return _clientName;
            }
        }
        #endregion

        public MQTTService()
        {

        }

        public async Task<IMqttClient> SetupClient(int port, string basepath, string username, string password, string clientname)
        {
            _basepath = basepath;
            _username = username;
            _password = password;
            _clientName = clientname;

            var configuration = new MqttConfiguration()
            {
                Port = port,
                KeepAliveSecs = 0,
                WaitTimeoutSecs = 5,
                BufferSize = 128 * 1024,
                AllowWildcardsInTopicFilters = true,
                MaximumQualityOfService = MqttQualityOfService.AtLeastOnce
            };
            var rpc = await MqttClient.CreateAsync(basepath, configuration);
            await rpc.ConnectAsync(new MqttClientCredentials(clientname, username, password));
            client = rpc;
            return rpc;
        }

        public IObservable<MqttApplicationMessage> GetMessageStream()
        {
            return client.MessageStream;
        }

        public async Task Subscribe(string topic)
        {
            await client.SubscribeAsync(topic.ToLower(), MqttQualityOfService.AtLeastOnce);
            await client.SubscribeAsync(topic, MqttQualityOfService.AtLeastOnce);
        }

        public async Task Publish(string topic, string message)
        {
            await client.PublishAsync(new MqttApplicationMessage(topic, Encoding.UTF8.GetBytes(message)), MqttQualityOfService.AtLeastOnce);
        }
    }
}
