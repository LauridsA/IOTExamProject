using System;
using System.Collections.Generic;
using System.Net.Mqtt;
using System.Text;

namespace MQTTClient
{
    public class Client : IObserver<MqttApplicationMessage>
    {

        public virtual void Subscribe(IObservable<MqttApplicationMessage> mom)
        {
            mom.Subscribe(this);
        }

        public void OnCompleted()
        {
            Console.WriteLine("Additional temperature data will not be transmitted.");
        }

        public void OnError(Exception error)
        {
            Console.WriteLine("FUCKING IDITO.");
        }

        public void OnNext(MqttApplicationMessage value)
        {
            Console.WriteLine($"I received this  {Encoding.UTF8.GetString(value.Payload)} from my nigga at {value.Topic}");
        }
    }
}
