using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Consumer.ConsoleApp
{
    public class Program
    {
        private static string connectionString = "amqp://guest:guest@localhost:5672//";

        private static string queueName;

        private static IConnection connection;

        private static IModel _channel;

        private static IModel channel => _channel ?? (_channel = CreateOrGetChannel());

        static void Main(string[] args)
        {
            queueName = args.Length > 0 ? args[0] : "test queue";

            connection = GetConnection();

            var consumerEvent = new EventingBasicConsumer(channel);

            //mesaj geldiğini anlamak için
            consumerEvent.Received += (ch, e) =>
                {
                    var messageArr = e.Body.ToArray();
                    var messageStr = Encoding.UTF8.GetString(messageArr);
                    Console.WriteLine($"Received Data: {queueName}");
                };

            channel.BasicConsume(queueName, true, consumerEvent);

            Console.WriteLine($"{queueName} Listening.. \n\n\n");
        }

        private static IConnection GetConnection()
        {
            ConnectionFactory factory = new ConnectionFactory()
            {
                Uri = new Uri(connectionString, UriKind.RelativeOrAbsolute)
            };
            return factory.CreateConnection();
        }

        private static IModel CreateOrGetChannel()
        {
            if (connection != null)
                return connection.CreateModel();
            else
                return null;
        }
    }
}
