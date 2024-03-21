using RabbitMQ.Client;
using System.Text;

public class Program
{
    // AMQP : Advanced Message Queuing Protocol. RabbitMQ message protocol
    public static void Main(string[] args)
    {
        // RabbitMQ.Client package using
        // send message to queue then connectionFactory -> connection -> channel
        // create connectionFactory
        var connectionFactory = new ConnectionFactory()
        {
            HostName = "localhost",
            // remote connection
            //Uri = new Uri(""),
            //UserName = "root",
            //Password = "password",
        };

        // create connection and channel
        var connection = connectionFactory.CreateConnection();
        var channel = connection.CreateModel();

        // advert new queue
        channel.QueueDeclare(queue: "rabbit", durable: false, exclusive: false, autoDelete: false, arguments: null);

        // create and convert message
        var message = "in rabbit queue";
        var byteMessage = Encoding.UTF8.GetBytes(message);

        // send message to mailbox
        channel.BasicPublish(exchange: "", routingKey: "rabbit", basicProperties: null, body: byteMessage);


        Console.ReadKey();
    }
}