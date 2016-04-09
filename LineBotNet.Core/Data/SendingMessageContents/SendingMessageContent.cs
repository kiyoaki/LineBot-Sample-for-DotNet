using System.Collections.Generic;

namespace LineBotNet.Core.Data.SendingMessageContents
{
    public abstract class SendingMessageContent
    {
        public abstract ContentType ContentType { get; }

        public abstract Dictionary<string, object> Create();
    }
}