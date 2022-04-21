using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Sender : ISender
    {
        public Task Send(Object request)
        {
            Console.WriteLine(request.ToString());
            return Task.CompletedTask;
        }
    }
}
