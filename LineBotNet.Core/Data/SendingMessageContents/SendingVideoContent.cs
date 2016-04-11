using System;
using System.Collections.Generic;

namespace LineBotNet.Core.Data.SendingMessageContents
{
    public class SendingVideoContent : SendingMessageContent
    {
        private readonly string _originalContentUrl;
        private readonly string _previewImageUrl;

        public SendingVideoContent(string originalContentUrl, string previewImageUrl)
        {
            if (originalContentUrl == null)
            {
                throw new ArgumentNullException(nameof(originalContentUrl));
            }

            _originalContentUrl = originalContentUrl;
            _previewImageUrl = previewImageUrl;
        }

        public override ContentType ContentType => ContentType.Video;

        public override Dictionary<string, object> Create()
        {
            return new Dictionary<string, object>
            {
                ["contentType"] = (int)ContentType.Image,
                ["toType"] = 1,
                ["originalContentUrl"] = _originalContentUrl,
                ["previewImageUrl"] = _previewImageUrl
            };
        }
    }
}