using MediatR;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;

namespace Server
{
    public class Message
    {
        public Message(Guid newGuid, string Msg)
        {
            this.Id = newGuid;
            this.Msg = Msg;
        }

        public Guid Id { get; set; }
        public string Msg { get; set; }
    }
}
