using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;

namespace Server
{
    public abstract class RabbitMqClientBase : IDisposable
    {
        protected const string VirtualHost = "CUSTOM_HOST";
        protected readonly string LoggerExchange = $"{VirtualHost}.LoggerExchange";
        protected readonly string LoggerQueue = $"{VirtualHost}.log.message";
        protected const string LoggerQueueAndExchangeRoutingKey = "log.message";

        protected IModel Channel { get; private set; }
        private IConnection _connection;
        private readonly ConnectionFactory _connectionFactory;

        protected RabbitMqClientBase(
            ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
            ConnectToRabbitMq();
        }

        private void ConnectToRabbitMq()
        {
            if (_connection == null || _connection.IsOpen == false)
            {
                _connection = _connectionFactory.CreateConnection();
            }

            if (Channel == null || Channel.IsOpen == false)
            {
                Channel = _connection.CreateModel();
                Channel.ExchangeDeclare(exchange: LoggerExchange, type: "direct", durable: true, autoDelete: false);
                Channel.QueueDeclare(queue: LoggerQueue, durable: true, exclusive: false, autoDelete: false);
                Channel.QueueBind(queue: LoggerQueue, exchange: LoggerExchange, routingKey: LoggerQueueAndExchangeRoutingKey);
            }
        }

        public void Dispose()
        {
            try
            {
                Channel?.Close();
                Channel?.Dispose();
                Channel = null;

                _connection?.Close();
                _connection?.Dispose();
                _connection = null;
            }
            catch (Exception ex)
            {
            }
        }
    }
}
