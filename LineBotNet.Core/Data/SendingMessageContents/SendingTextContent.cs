using System;
using System.Collections.Generic;

namespace LineBotNet.Core.Data.SendingMessageContents
{
    public class SendingTextContent : SendingMessageContent
    {
        private readonly string _text;

        public SendingTextContent(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            _text = text;
        }

        public override ContentType ContentType => ContentType.Text;

        public override Dictionary<string, object> Create()
        {
            return new Dictionary<string, object>
            {
                ["contentType"] = (int)ContentType.Text,
                ["toType"] = 1,
                ["text"] = _text
            };
        }
    }
}