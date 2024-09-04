using Model.Data;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory connectionFactory = new();
connectionFactory.Uri = new Uri("amqp://user:password@localhost:5672");

connectionFactory.ClientProvidedName = "Rabbit Receiver1 App";

IConnection cnn = connectionFactory.CreateConnection();

IModel channel = cnn.CreateModel();

string exchangeName = "DemoExchange";
string routingKey = "demo-routing-key";
string queueName = "DemoQueue";

channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
channel.QueueDeclare(queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
channel.QueueBind(queueName, exchangeName, routingKey, null);
channel.BasicQos(0, 1, false);

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (sender, args) =>
{
    Task.Delay(TimeSpan.FromSeconds(5)).Wait();
    var messageBody = args.Body.ToArray();

    string userInfoJson = Encoding.UTF8.GetString(messageBody);

    var userInfoModel = Newtonsoft.Json.JsonConvert.
                            DeserializeObject<UserInfoModel>(userInfoJson);
    
    Console.WriteLine($"User from RabbitMQ:{Environment.NewLine} Guid:{userInfoModel?.Id} - Email:{userInfoModel?.Email}");

    channel.BasicAck(args.DeliveryTag, false);
};

string consumerTag = channel.BasicConsume(queueName, false, consumer);

Console.ReadLine();

channel.BasicCancel(consumerTag);


channel.Close();
cnn.Close();