using System;
using System.Collections.Generic;
using System.Net.Mqtt;
using System.Text;

namespace MQTTBroker
{
    public class AuthProvider : IMqttAuthenticationProvider
    {
        public static List<MyMqttConnectedClient> _clients = new List<MyMqttConnectedClient>();

        public void AddClientToAuth(string clientId, string username, string password)
        {
            var client = new MyMqttConnectedClient(clientId, username, password);
            _clients.Add(client);
        }

        public bool Authenticate(string clientId, string username, string password)
        {
            foreach (MyMqttConnectedClient client in _clients)
            {
                if (clientId.Equals(client.clientId))
                    if (username.Equals(client.username))
                        if (password.Equals(client.password))
                            return true;
            }
            return false;
        }

        public partial class MyMqttConnectedClient
        {
            public string clientId;
            public string username;
            public string password;

            public MyMqttConnectedClient(string client_id, string user_name, string pass)
            {
                clientId = client_id;
                username = user_name;
                password = pass;
            }
        }
    }
}
