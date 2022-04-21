using System;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Server
{
    public class LogProducer : ProducerBase<Message>
    {
        public LogProducer(
            ConnectionFactory connectionFactory
            ) :
            base(connectionFactory)
        {
        }

        protected override string ExchangeName => "CUSTOM_HOST.LoggerExchange";
        protected override string RoutingKeyName => "log.message";
        protected override string AppId => "LogProducer";
    }
}
