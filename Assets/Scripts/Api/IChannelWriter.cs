using System;

namespace Assets.Scripts.Api
{
    public interface IChannelWriter
    {
        void WriteToChannel(
            string type,
            byte[] bytes);

        void WriteToChannel(
            string type,
            byte[] bytes,
            Guid responseId);
    }
}