using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

public class Program
{
    public static void Main(string[] args)
    {
        var connectionFactory = new ConnectionFactory()
        {
            HostName = "localhost",
        };

        // create connection and channel
        var connection = connectionFactory.CreateConnection();
        var channel = connection.CreateModel();

        // create consumer
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var byteMessage = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(byteMessage);
            channel.BasicAck(ea.DeliveryTag, multiple:false);
        };


        // send message to mailbox
        channel.BasicConsume(queue:"rabbit", autoAck:false, consumer:consumer);


        Console.ReadKey();

    }
}