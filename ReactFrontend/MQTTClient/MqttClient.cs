using ReactFrontend.MQTTClient;
using System;
using System.Collections.Generic;
using System.Net.Mqtt;
using System.Text;

namespace MQTTClient
{
    public class MqttClientLocal : IObserver<MqttApplicationMessage>
    {
        private List<Message> messages { get; set; }
        public virtual void Subscribe(IObservable<MqttApplicationMessage> mqttApplicationMessage)
        {
            mqttApplicationMessage.Subscribe(this);
        }

        public void OnCompleted()
        {
            Console.WriteLine("Additional temperature data will not be transmitted.");
        }

        public void OnError(Exception error)
        {
            Console.WriteLine($"Exception Occured. Exception Type: {error.GetType().ToString()} \n Exception message: {error.Message.ToString()}");
        }

        public void OnNext(MqttApplicationMessage value)
        {
            Console.WriteLine($"Message Recieved: {Encoding.UTF8.GetString(value.Payload)} \n From topic: {value.Topic}");
        }
    }
}
