using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMQ
{
    class EmitLog
    {
        static void Main(string[] args)
        {
            string resp="sim";
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
                do{
                    {
                        channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);

                        var message = GetMessage(args);
                        var body = Encoding.UTF8.GetBytes(message);
                        channel.BasicPublish(exchange: "logs",
                                             routingKey: "",
                                             basicProperties: null,
                                             body: body);
                        Console.WriteLine(" [x] Sent {0}", message);
                    }
                    //Console.WriteLine("Deseja continuar?");
                    //resp = Console.ReadLine();

                }while( resp == "sim");

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }

        private static string GetMessage(string[] args)
        {
            Console.WriteLine("Digite:");
            string message = Console.ReadLine();

            return ((args.Length > 0)
                   ? string.Join(" ", args)
                   : message );
        }
    }
}

