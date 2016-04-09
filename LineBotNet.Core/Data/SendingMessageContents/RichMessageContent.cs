using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace LineBotNet.Core.Data.SendingMessageContents
{
    public class RichMessageContent : SendingMessageContent
    {
        private readonly string _downloadUrl;
        private readonly string _altText;
        private readonly RichMessage _richMessage;

        public RichMessageContent(string downloadUrl, string altText, RichMessage richMessage)
        {
            if (downloadUrl == null)
            {
                throw new ArgumentNullException(nameof(downloadUrl));
            }
            if (altText == null)
            {
                throw new ArgumentNullException(nameof(altText));
            }
            if (richMessage == null)
            {
                throw new ArgumentNullException(nameof(richMessage));
            }

            _downloadUrl = downloadUrl;
            _altText = altText;
            _richMessage = richMessage;
        }

        public override ContentType ContentType => ContentType.Video;

        public override Dictionary<string, object> Create()
        {
            return new Dictionary<string, object>
            {
                ["contentType"] = (int)ContentType.Image,
                ["toType"] = 1,
                ["contentMetadata"] = new Dictionary<string, object>
                {
                    ["DOWNLOAD_URL"] = _downloadUrl,
                    ["SPEC_REV"] = 1,
                    ["ALT_TEXT"] = _altText,
                    ["MARKUP_JSON"] = JsonConvert.SerializeObject(_richMessage),
                }
            };
        }
    }
}