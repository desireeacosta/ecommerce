using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace ECommerce.API.Services
{
    public class MessageProducer : IMessageProducer
    {
        public void SendingMessage<T>(T message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "jackal.rmq.cloudamqp.com",
                UserName = "zebbopeh",
                Password = "CW6QcuLaGs22SGVtnPvsQToJwDnpPcli",
                VirtualHost = "zebbopeh"
            };

            var conn = factory.CreateConnection();
            using var channel = conn.CreateModel();
            channel.QueueDeclare("products", durable: true, exclusive: false);
            var jsonString = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(jsonString);
            channel.BasicPublish("", "products", body: body);
        }
    }
}