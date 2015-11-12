using System;
using System.Collections.Generic;
using System.IO;
using ProjectXyz.Api.Messaging.Interface;
using ProjectXyz.Api.Messaging.Serialization.Interface;

namespace Assets.Scripts.Api
{
    public sealed class RequestSender : IRequestSender
    {
        #region Fields
        private readonly Dictionary<Type, string> _requestTypeMapping;
        private readonly IRequestWriter _requestWriter;
        private readonly IChannelWriter _channelWriter;
        #endregion

        #region Constructors
        private RequestSender(
            IRequestWriter requestWriter,
            IChannelWriter channelWriter,
            IDictionary<Type, string> requestTypeMapping)
        {
            _requestTypeMapping = new Dictionary<Type, string>(requestTypeMapping);
            _requestWriter = requestWriter;
            _channelWriter = channelWriter;
        }
        #endregion

        #region Methods
        public static IRequestSender Create(
            IRequestWriter requestWriter,
            IChannelWriter channelWriter,
            IDictionary<Type, string> requestTypeMapping)
        {
            var sender = new RequestSender(
                requestWriter,
                channelWriter,
                requestTypeMapping);
            return sender;
        }

        public void Send<TRequest>(TRequest request)
            where TRequest : IRequest
        {
            var typeKey = request.GetType();
            if (!_requestTypeMapping.ContainsKey(typeKey))
            {
                throw new NotSupportedException(string.Format("Mapping of request type '{0}' is not supported.", typeKey));
            }

            using (var requestStream = new MemoryStream())
            {
                _requestWriter.Write(
                    request,
                    requestStream);

                _channelWriter.WriteToChannel(
                    _requestTypeMapping[typeKey],
                    requestStream.GetBuffer(),
                    request.Id);
            }
            #endregion
        }

    }
}