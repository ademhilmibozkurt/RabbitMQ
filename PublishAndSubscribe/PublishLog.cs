using RabbitMQ.Client;
using System.Text;

namespace PublishAndSubscribe
{
    public class PublishLog
    {
        public static void Main(string[] args)
        {
            var connectionFactory = new ConnectionFactory(){ HostName = "localhost" };

            // create connection and channel
            var connection = connectionFactory.CreateConnection();
            var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange:"logs", type:ExchangeType.Fanout);

            var message = GetMessage(args);
            var body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchange:"logs", routingKey:string.Empty, basicProperties:null, body:body);
        }    
        
        public static string GetMessage(string[] args)
        {
            return args.Length >0 ? string.Join(",", args) : "Empty";
        }
    }
}
