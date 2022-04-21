using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;

namespace Server
{
    public abstract class ProducerBase<T> : RabbitMqClientBase, IRabbitMqProducer<T>
    {
        protected abstract string ExchangeName { get; }
        protected abstract string RoutingKeyName { get; }
        protected abstract string AppId { get; }

        protected ProducerBase(
            ConnectionFactory connectionFactory) :
            base(connectionFactory)
        { }

        public virtual void Publish(T @event)
        {
            try
            {
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event));
                var properties = Channel.CreateBasicProperties();
                Channel.BasicPublish(exchange: ExchangeName, routingKey: RoutingKeyName, body: body, basicProperties: properties);
            }
            catch (Exception ex)
            {
                var a = "string";
            }
        }
    }
}
