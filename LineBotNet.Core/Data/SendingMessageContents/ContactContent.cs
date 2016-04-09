using System;
using System.Collections.Generic;

namespace LineBotNet.Core.Data.SendingMessageContents
{
    public class ContactContent : SendingMessageContent
    {
        private readonly string _mid;
        private readonly string _displayName;

        public ContactContent(string mid, string displayName)
        {
            if (mid == null)
            {
                throw new ArgumentNullException(nameof(mid));
            }
            if (displayName == null)
            {
                throw new ArgumentNullException(nameof(displayName));
            }

            _mid = mid;
            _displayName = displayName;
        }

        public override ContentType ContentType => ContentType.Contact;

        public override Dictionary<string, object> Create()
        {
            return new Dictionary<string, object>
            {
                ["contentType"] = (int)ContentType.Contact,
                ["toType"] = 1,
                ["contentMetadata"] = new Dictionary<string, object>
                {
                    ["mid"] = _mid,
                    ["displayName"] = _displayName
                }
            };
        }
    }
}