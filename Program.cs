using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var sender = new Sender();
            var consumer = new LogConsumer(sender, factory);
            var producer = new LogProducer(factory);

            while (true)
            {
                Thread.Sleep(1000);
                producer.Publish(new Message(Guid.NewGuid(), "My message 1"));
                producer.Publish(new Message(Guid.NewGuid(), "My message 2"));
            }
            Console.ReadKey();
        }
    }
}
