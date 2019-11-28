using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReactFrontend.Controllers.CommunicationModels;
using ReactFrontend.MQTTClient;

namespace ReactFrontend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PiezoController : Controller
    {
        [HttpGet]
        public ActionResult<IEnumerable<string>> Index()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpPost("postMqttMessage/song/{message}")]
        public void postMqttMessageSong([FromRoute]string message)
        {
            string topic = "song";
            RemoteMQTTClient.postMessage(topic, message);
        }

        [HttpPost("postMqttMessage/track/{message}")]
        public void postMqttMessageTrack([FromRoute]string message)
        {
            string topic = "track";
            RemoteMQTTClient.postMessage(topic, message);
        }

        [HttpGet]
        [Route("GetMessageReactClient")]
        public ActionResult<List<Message>> GetMqttMessagesReactClient()
        {
            return RemoteMQTTClient.getMessages();
        }
    }
}