using Model.Data;
using RabbitMQ.Client;
using System.Text;

ConnectionFactory connectionFactory = new();
connectionFactory.Uri = new Uri("amqp://user:password@localhost:5672");

connectionFactory.ClientProvidedName = "Rabbit Sender App";

IConnection cnn = connectionFactory.CreateConnection();

IModel channel = cnn.CreateModel();

string exchangeName = "DemoExchange";
string routingKey = "demo-routing-key";
string queueName = "DemoQueue";

channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
channel.QueueDeclare(queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
channel.QueueBind(queueName, exchangeName, routingKey, null);


var dataGenerator = new DataGenerator();
for(int i =0; i < 60; i++)
{
    var userInfo = dataGenerator.GeneteUserInfo();
    var userInfoJson = Newtonsoft.Json.JsonConvert.SerializeObject(userInfo);

    Console.WriteLine($"Sending UserInfo { userInfo.Email }");

    byte[] messageBodyBytes = Encoding.UTF8.GetBytes(userInfoJson);
    channel.BasicPublish(exchangeName, routingKey, null, messageBodyBytes);
    
    Thread.Sleep(1000);
}

channel.Close();
cnn.Close();