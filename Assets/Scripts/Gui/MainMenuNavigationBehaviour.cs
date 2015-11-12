using System;
using System.Linq;
using Assets.Scripts.Api;
using ProjectXyz.Api.Messaging.Core.General;
using ProjectXyz.Api.Messaging.Core.Initialization;
using ProjectXyz.Api.Messaging.Interface;
using ProjectXyz.Api.Messaging.Serialization.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using UnityEngine;

namespace Assets.Scripts.Gui
{
    public sealed class MainMenuNavigationBehaviour : MonoBehaviour
    {
        #region Methods
        private void RequestInitialization()
        {
            var factory = new ConnectionFactory()
            {
                //HostName = startupParameters.HostName,
                //Port = startupParameters.Port,
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                const string ROUTING_KEY = "hello";
                channel.QueueDeclare(
                    queue: ROUTING_KEY,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var replyQueueName =
                    channel.QueueDeclare(
                    queue: "Responses To Player",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var request = new InitializeWorldRequest()
                {
                    Id = Guid.NewGuid(),
                    PlayerId = Guid.NewGuid(),
                };

                var messageDiscoverer = MessageDiscoverer.Create();
                var requestTypeMapping = messageDiscoverer.Discover<IRequest>(AppDomain.CurrentDomain.GetAssemblies());
                var inverseRequestTypeMapping = requestTypeMapping.ToDictionary(x => x.Value, x => x.Key);
                var responseTypeMapping = messageDiscoverer.Discover<IResponse>(AppDomain.CurrentDomain.GetAssemblies());
                var inverseResponseTypeMapping = responseTypeMapping.ToDictionary(x => x.Value, x => x.Key);

                var consumer = new EventingBasicConsumer(channel);

                var responseReader = JsonResponseReader.Create();
                var requestWriter = JsonRequestWriter.Create();
                var channelWriter = ChannelWriter.Create(
                    channel,
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
                var rpcClient = RpcClient.Create(
                    requestSender,
                    responseReceiver);

                channel.BasicConsume(
                    queue: replyQueueName,
                    noAck: true,
                    consumer: consumer);

                var response = rpcClient.Send<InitializeWorldRequest, BooleanResultResponse>(
                    request, 
                    TimeSpan.FromSeconds(5));

                Application.LoadLevel("Explore");
            }
        }
        #endregion

        #region Event Handlers
        public void NewGameButton_OnClick()
        {
            Debug.Log("New game clicked.");
            RequestInitialization();
        }

        public void ExitButton_OnClick()
        {
            Debug.Log("Exit clicked.");
            Application.Quit();
        }
        #endregion
    }
}