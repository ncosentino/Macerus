using System;
using System.Collections.Generic;
using Assets.Scripts.Gui;
using RabbitMQ.Client;
using RabbitMQ.Client.Framing;

namespace Assets.Scripts.Api
{
    public sealed class ChannelWriter : IChannelWriter
    {
        #region Fields
        private readonly IModel _channel;
        private readonly string _routingKey;
        #endregion

        #region Constructors
        private ChannelWriter(
            IModel channel,
            string routingKey)
        {
            _channel = channel;
            _routingKey = routingKey;
        }
        #endregion

        #region Methods
        public static IChannelWriter Create(
            IModel channel,
            string routingKey)
        {
            var writer = new ChannelWriter(
                channel,
                routingKey);
            return writer;
        }

        public void WriteToChannel(
            string type,
            byte[] bytes)
        {
            WriteToChannel(
                type,
                bytes,
                null);
        }

        public void WriteToChannel(
            string type,
            byte[] bytes,
            Guid responseId)
        {
            WriteToChannel(
                type,
                bytes,
                (Guid?)responseId);
        }

        private void WriteToChannel(
            string type,
            byte[] bytes,
            Guid? responseId)
        {
            _channel.BasicPublish(
                exchange: "",
                routingKey: _routingKey,
                basicProperties: new BasicProperties()
                {
                    CorrelationId = responseId == null ? null : responseId.ToString(),
                    Headers = new Dictionary<string, object>()
                    {
                        { "Type", type },
                    }
                },
                body: bytes);
        }
        #endregion
    }
}