using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectXyz.Api.Messaging.Interface;
using ProjectXyz.Api.Messaging.Serialization.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Assets.Scripts.Api
{
    public sealed class RpcContainer : IRpcContainer
    {
        #region Fields
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IRpcClient _rpcClient;
        #endregion

        #region Constructors
        private RpcContainer()
        {
            var factory = new ConnectionFactory()
            {
                //HostName = startupParameters.HostName,
                //Port = startupParameters.Port,
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            const string ROUTING_KEY = "hello";
            _channel.QueueDeclare(
                queue: ROUTING_KEY,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var replyQueueName =_channel.QueueDeclare(
                queue: "Responses To Player",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var messageDiscoverer = MessageDiscoverer.Create();
            var requestTypeMapping = messageDiscoverer.Discover<IRequest>(AppDomain.CurrentDomain.GetAssemblies());
            var inverseRequestTypeMapping = requestTypeMapping.ToDictionary(x => x.Value, x => x.Key);
            var responseTypeMapping = messageDiscoverer.Discover<IResponse>(AppDomain.CurrentDomain.GetAssemblies());
            var inverseResponseTypeMapping = responseTypeMapping.ToDictionary(x => x.Value, x => x.Key);

            var consumer = new EventingBasicConsumer(_channel);

            var responseReader = JsonResponseReader.Create();
            var requestWriter = JsonRequestWriter.Create();
            var channelWriter = ChannelWriter.Create(
                _channel,
                ROUTING_KEY);

            var responseFactory = ResponseFactory.Create(
                responseReader,
                responseTypeMapping);
            var responseReceiver = ResponseReceiver.Create(
                consumer,
                responseFactory);

            var requestSender = RequestSender.Create(
                requestWriter,
                channelWriter,
                inverseRequestTypeMapping);
            _rpcClient = RpcClient.Create(
                requestSender,
                responseReceiver);

            _channel.BasicConsume(
                queue: replyQueueName,
                noAck: true,
                consumer: consumer);
        }

        ~RpcContainer()
        {
            Dispose(false);
        }
        #endregion

        #region Properties
        public IRpcClient Client
        {
            get { return _rpcClient; }
        }
        #endregion

        #region Methods
        public static IRpcContainer Create()
        {
            var container = new RpcContainer();
            return container;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            _connection.Close();
            _connection.Dispose();
            _channel.Dispose();
        }
        #endregion
    }
}
