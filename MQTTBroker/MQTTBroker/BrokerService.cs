using System;
using System.Collections.Generic;
using System.Net.Mqtt;
using System.Net.Mqtt.Sdk.Bindings;
using System.Text;
using System.Threading;

namespace MQTTBroker
{
    public class BrokerService
    {
        //Privates
        private static IMqttServer _server;
        private static AuthProvider _authProv;
        private MqttQualityOfService _qos { get; set; }

        public BrokerService()
        {
            _authProv = new AuthProvider();
        }

        public void AddClientToAuth(string clientId, string username, string password)
        {
            _authProv.AddClientToAuth(clientId, username, password);
        }

        public void Setup(int port)
        {
            MqttConfiguration config = new MqttConfiguration()
            {
                AllowWildcardsInTopicFilters = true,
                ConnectionTimeoutSecs = 10,
                KeepAliveSecs = 10,
                WaitTimeoutSecs = 10,
                Port = port,
                MaximumQualityOfService = MqttQualityOfService.AtLeastOnce
            };
            _server = MqttServer.Create(config, null, _authProv);

            _authProv.AddClientToAuth("PiezoPlayer", "player", "piezopass");
            _authProv.AddClientToAuth("Frontend", "front", "frontpass");
            _authProv.AddClientToAuth("FlickClient", "flick", "flickpass");
            _server.MessageUndelivered += _server_MessageUndelivered;
            _server.ClientConnected += _server_ClientConnected;
            _server.ClientDisconnected += _server_ClientDisconnected;
            _server.Stopped += _server_Stopped;
            _server.Start();
        }

        private void _server_Stopped(object sender, MqttEndpointDisconnected e)
        {
            Console.WriteLine($"Server stopped {e}");
        }

        private void _server_ClientConnected(object sender, string e)
        {
            Console.WriteLine($"Client {e} connected to broker");
        }

        private void _server_ClientDisconnected(object sender, string e)
        {
            Console.WriteLine($"Client {e} disconnected from broker");
        }

        private void _server_MessageUndelivered(object sender, MqttUndeliveredMessage e)
        {
            Console.WriteLine($"message not delivered {e} ");
        }

        public IMqttServer GetServer()
        {
            return _server;
        }
    }
}
