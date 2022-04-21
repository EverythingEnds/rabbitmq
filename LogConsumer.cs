using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    public class LogConsumer : ConsumerBase
    {
        protected override string QueueName => "CUSTOM_HOST.log.message";

        public LogConsumer(
            ISender mediator,
            ConnectionFactory connectionFactory) :
            base(mediator, connectionFactory)
        {
            try
            {
                var consumer = new EventingBasicConsumer(Channel);
                consumer.Received += OnEventReceived<Message>;
                Channel.BasicConsume(queue: QueueName, autoAck: false, consumer: consumer);
            }
            //}
            catch (Exception ex)
            {
                var a = "dasda";
            }
        }

        public virtual Task StartAsync() => Task.CompletedTask;

        public virtual Task StopAsync()
        {
            Dispose();
            return Task.CompletedTask;
        }
    }
}
