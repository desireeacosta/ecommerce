using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

Console.WriteLine("Processing started... :)");

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

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, eventArgs) =>
{
  var body = eventArgs.Body.ToArray();
  var message = Encoding.UTF8.GetString(body);
  Console.WriteLine($"A message has been received: {message}");
};

channel.BasicConsume("products", true, consumer);

Console.ReadKey();
