using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FullSerializer;
using ProjectXyz.Api.Messaging.Core.Initialization;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Framing;
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

                var jsonSerializer = new fsSerializer();
                fsData serializedRequest;
                jsonSerializer.TrySerialize(request, out serializedRequest);
                var requestData = fsJsonPrinter.PrettyJson(serializedRequest);
                Debug.Log(requestData);

                channel.BasicPublish(
                    exchange: "",
                    routingKey: ROUTING_KEY,
                    basicProperties: new BasicProperties()
                    {
                        Headers = new Dictionary<string, object>()
                        {
                            { "Type", "InitializeWorldRequest" }
                        },
                        CorrelationId = request.Id.ToString(),
                        ReplyTo = ROUTING_KEY,
                    },
                    body: Encoding.UTF8.GetBytes(requestData));

                var consumer = new QueueingBasicConsumer(channel);
                channel.BasicConsume(queue: replyQueueName,
                                     noAck: true,
                                     consumer: consumer);

                string answer;
                while (true)
                {
                    var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
                    if (ea.BasicProperties.CorrelationId == request.Id.ToString())
                    {
                        answer = Encoding.UTF8.GetString(ea.Body);
                        Debug.Log("Got server response: " + answer);
                        break;
                    }
                }

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