using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace PublishAndSubscribe
{
    public class ReceiveLog
    {
        public static void Main()
        {
            var connectionFactory = new ConnectionFactory() { HostName = "localhost" };

            // create connection and channel
            var connection = connectionFactory.CreateConnection();
            var channel = connection.CreateModel();
            var queueName = channel.QueueDeclare().QueueName;

            channel.QueueBind(queue: queueName, exchange:"logs", routingKey: string.Empty);

            // create consumer
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
            };

            channel.BasicConsume(queue:queueName, autoAck: false, consumer: consumer);

            Console.ReadKey();
        }
    }
}
